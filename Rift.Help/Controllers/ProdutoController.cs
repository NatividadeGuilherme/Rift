using Rift.Help.BLL;
using Rift.Help.DAL;
using Rift.Models;
using System;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Rift.Help.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
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
            var dalProduto = new ProdutoDAL();
            return View(dalProduto.TodosProdutos());
        }

        public ActionResult ListaProdutosClientes(int IdCliente, string NomeCliente)

        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }


            var dalProdutoCliente = new ProdutoClienteDAL();
            ViewBag.Id = IdCliente;
            ViewBag.Nome = NomeCliente;
            var listaProdCli = dalProdutoCliente.RetornarProdutosPorCliente(IdCliente);
          
            return View(listaProdCli);
        }
        public ActionResult CadastrarProduto()
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }

            return View();
        }
        [HttpPost]
        public ActionResult CadastrarProduto(Produto produto)
        {
            try
            {
                var bllProduto = new ProdutoBLL();
                bllProduto.CadastrarProduto(produto);
                ViewBag.Mensagem = "Produto Cadastrado com sucesso!";
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Mensagem = "Produto Já cadastrado!";
                return View();
            }
           
        }
        public ActionResult AlterarProduto(int IdProduto)
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }

            var dalProduto = new ProdutoDAL();
            var produto = dalProduto.BuscarProdutoPorId(IdProduto);

            return View(produto);
        }
        [HttpPost]
        public ActionResult AlterarProduto(Produto produto)
        {
            try
            {
                var bllProduto = new ProdutoBLL();
                bllProduto.AlterarProduto(produto);
                ViewBag.Mensagem = "Alteração efetuada";

            }
            catch(Exception e)
            {
                ViewBag.Mensagem = e.Message;

            }

            return View();
        }
        [HttpPost]
        public ActionResult ProcessarExclusao(int IdProduto)
        {
            try
            {

                var bllProduto = new ProdutoBLL();
                bllProduto.ExcluirProduto(IdProduto);
                TempData["Mensagem"]= "Registro Apagado";
               
                
            }
            catch(Exception e)
            {
                TempData["Mensagem"] =e.Message;
            }
            return RedirectToAction("Index");

        }
        public ActionResult IncluirProdutoNoCliente(int IdCliente, string NomeCliente)
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }


            var dalProduto = new ProdutoDAL();
            TempData["Id"] = IdCliente;
            TempData["NomeCliente"] = NomeCliente;
            ViewBag.ListaProdutos = dalProduto.TodosProdutos();
            //ViewBag.NomeCliente = NomeCliente;
            return View();
        }
        [HttpPost]
        public ActionResult IncluirProdutoNoCliente(ProdutoCliente prodCli)
        {

            var dalProduto = new ProdutoDAL();
            try
            {
                
                var bllProdutoCliente = new ProdutoClienteBLL();
                bllProdutoCliente.InserirProdutoCliente(prodCli);
                ViewBag.ListaProdutos = dalProduto.TodosProdutos();
                ViewBag.Mensagem = "Produto adicionado com sucesso!";
            }
            catch (SqlException)
            {
                ViewBag.ListaProdutos = dalProduto.TodosProdutos();
                ViewBag.Mensagem = "Produto já adicionado para este cliente!";
            }
            

            return View();


        }
        [HttpPost]
        public JsonResult ActivateProdutoCliente(int IdProdutoCliente)
        {
            var bllProdutoCliente = new ProdutoClienteBLL();
            var resultStatusProdutoCliente = bllProdutoCliente.RetornarStatusProduto(IdProdutoCliente);
            bllProdutoCliente.ActivateProdutoCliente(IdProdutoCliente, resultStatusProdutoCliente.Status);
            return Json(new { Sucesso = resultStatusProdutoCliente.Status },JsonRequestBehavior.AllowGet);
            
        }
        [HttpPost]
        public JsonResult RemoverProdutoCliente( int idProdutoCliente)
        {
            try
            {
                var bllProdutoCliente = new ProdutoClienteBLL();
                var resultProdutoCliente = bllProdutoCliente.RemoverProdutoCliente(idProdutoCliente);
                return Json(new { Resultado = resultProdutoCliente }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new { Resultado = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}