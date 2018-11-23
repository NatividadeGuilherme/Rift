using Rift.Models;
using Rift.Help.BLL;
using System.Web.Mvc;
using Rift.Help.DAL;
using System;
using System.Data.SqlClient;

namespace Rift.Help.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }

            var bllCliente = new ClienteBLL();
            return View(bllCliente.RetornarTodosClientes());
        }
        public ActionResult Cadastrar()
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }
            if (TempData["Mensagem"] != null)
            {
                ViewBag.Mensagem = TempData["Mensagem"];
            }


            var DALGeograficos = new GeograficoDAL();
            ViewBag.Estados = DALGeograficos.RetornarTodosEstados();
            return View();
        }
        [HttpPost]
        public ActionResult Cadastrar(Cliente cliente)
        {
            try
            {
                var DALGeograficos = new GeograficoDAL();
                ViewBag.Estados = DALGeograficos.RetornarTodosEstados();
                //ViewBag.Cidades = DALGeograficos.RetornarTodasCidades(idEstado);
                var bllCliente = new ClienteBLL();
                bllCliente.IncluirCliente(cliente);
                TempData["Mensagem"] = "Cliente cadastrado com sucesso";
                return RedirectToAction("Cadastrar");
            }
            catch (SqlException)
            {
                ViewBag.Error = "Já existe um cliente vinculado a este CNPJ!";
                return View();
            }
                
            
        }
        public JsonResult BuscaCep(string Cep)
        {
            try
            {
                var correios = new Correios.CorreiosApi();
                var consultarCep = correios.consultaCEP(Cep);
                return Json(new
                {
                    Endereco = consultarCep.end,
                    Bairro = consultarCep.bairro,
                    Cidade = consultarCep.cidade,
                    Estado = consultarCep.uf
                }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Endereco = "",
                    Bairro = "",
                    Cidade = "",
                    Estado = ""
                }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult BuscarCidadesPorEstado(string uf)
        {
            try
            {
                var listaCidades = new GeograficoDAL();
                var lista = listaCidades.RetornarTodasCidades(uf);
                return Json(new { Cidade = lista }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Cidade = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult Alterar(int IdCliente)
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }

            var DALGeograficos = new GeograficoDAL();
            ViewBag.Estados = DALGeograficos.RetornarTodosEstados();

            var bllCliente = new ClienteBLL();

            return View(bllCliente.RetornarClientePorId(IdCliente));
        }
        [HttpPost]
        public ActionResult Alterar(Cliente cliente, string uf )
        {
                var DALGeofraficos = new GeograficoDAL();
                ViewBag.Estados = DALGeofraficos.RetornarTodosEstados();
                ViewBag.Cidades = DALGeofraficos.RetornarTodasCidades(uf);

                var bllCliente = new ClienteBLL();

                var registrosAlterados = bllCliente.AlterarCliente(cliente);
                TempData["Mensagem"] = "Alteração realizada com sucesso";
                return RedirectToAction("Index");
        }
        public ActionResult Excluir(int IdCliente)
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }
            return View(IdCliente);
        }
        [HttpPost]
        public ActionResult Excluir(Cliente cliente)
        {
            var bllCliente = new ClienteBLL();
            var registrosExcluidos = bllCliente.ExcluirCliente(cliente.IdCliente);
            return RedirectToAction("Index");
        }
      
    }
}