using Rift.Help.BLL;
using Rift.Help.DAL;
using Rift.Models;
using System;
using System.Data.SqlTypes;
using System.Web.Mvc;

namespace Rift.Help.Controllers
{
    public class ChamadoController : Controller
    {

        // GET: Chamado
        public ActionResult Index()
         {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }

            if (TempData["Mensagem"] != null)
            {
                ViewBag.Mensagem = TempData["Mensagem"];
            }
            var chamadoDAL = new ChamadoDAL();

            var listaProdutos = new ProdutoDAL();
            var listaClientes = new ClienteDAL();

            ViewBag.Clientes = listaClientes.RetornarTodosClientes();
            ViewBag.Produtos = listaProdutos.TodosProdutos();

            return View(chamadoDAL.RetornarTodosChamadosPendentes());
        }
        public ActionResult CriarChamado()
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }
            var dalClientes = new ClienteDAL();
            ViewBag.TodosClientes = dalClientes.RetornarTodosClientes();
            return View();
        }
        [HttpPost]
        public ActionResult CriarChamado(Chamado chamado) 
        {

            var idColaborador = ((Colaborador)Session["UsuarioAutenticado"]).IdColaborador;
            var bllChamado = new ChamadoBLL();
            bllChamado.CriarChamado(idColaborador, chamado);
            TempData["Mensagem"] = "Chamado criado!";

            return RedirectToAction("Index");
        }
        public JsonResult BuscarProdutosCliente(int idCliente)
        {
            try
            {
                var dalProdutoCliente = new ProdutoClienteDAL();
                var lista = dalProdutoCliente.RetornarProdutosAtivos(idCliente);

                return Json(new { ProdutosPorCliente = lista }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return Json(new { ProdutosPorCliente = "" }, JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult AlterChamado(int idChamado, string titulo)
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }
            try
            {
                var bllChamado = new ChamadoBLL();
                var chamado = bllChamado.ConsultarChamado(idChamado);
                return View(chamado);
            }
            catch (SqlTypeException e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction("Index");

            }            
        }
        
        [HttpPost]
        public ActionResult AlterChamado(Chamado chamado,string titulo)
        {
            try
            {
                var bllChamado = new ChamadoBLL();
             
                bllChamado.AlterarChamado(chamado);

                return RedirectToAction("Index", new { IdChamado = chamado.IdChamado, Titulo=titulo});
            }
            catch(SqlTypeException e)
            {
                ViewBag.Error = e.Message;
                return View(chamado);
            }
            
        }

        [HttpPost]
        public JsonResult ExcluirChamado(int idChamado)
        {
            try
            {
                var bllChamado = new ChamadoBLL();
                var resultExclusao = bllChamado.ExcluirChamado(idChamado);

                return Json(new { Result = resultExclusao }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                return Json(new { Result = e.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult RetornarRegistrosPorClienteProduto(int IdProduto=0, int IdCliente=0)
         {
            var dal = new ChamadoDAL();
            var registrosEncontrados = dal.RetornarTodosChamadosPendentes(IdProduto, IdCliente);

            return PartialView("_ListaDeChamados", registrosEncontrados);
            //try
            //{
            //    var dal = new ChamadoDAL();
            //    var registrosEncontrados = dal.RetornarTodosChamadosPendentes(IdProduto, IdCliente);

            //    if (registrosEncontrados != null)
            //    {
            //        return Json(new { Resultado = registrosEncontrados }, JsonRequestBehavior.AllowGet );
            //    }
            //    else
            //    {
            //        registrosEncontrados = dal.RetornarTodosChamadosPendentes();
            //        return Json(new { Resultado = registrosEncontrados, JsonRequestBehavior.AllowGet });
            //    }
            //}
            //catch (Exception e)
            //{
            //    return Json(new { Resultado = "", JsonRequestBehavior.AllowGet });
            //}

        }
    }
}