using Rift.Help.DAL;
using Rift.Models;
using System;
using System.Data.SqlTypes;
using System.Web.Mvc;

namespace Rift.Help.Controllers
{
    public class RelatorioController : Controller
    {
        // GET: Relatorio
        public ActionResult Index()
        {
            //if (Session["UsuarioAutenticado"] == null)
            //{
            //    return RedirectToAction("ValidarLogin", "Colaborador");
            //}
            var dalListaClientes = new ClienteDAL();
            var dalListaProdutos = new ProdutoDAL();
            ViewBag.ListaClientes = dalListaClientes.RetornarTodosClientes();
            ViewBag.ListaProdutos = dalListaProdutos.TodosProdutos();
            return View();
        }
        [HttpPost]
        public ActionResult Index(DateTime dataInicial, DateTime dataFinal, int codigoDoCliente=0, int codigoDoProduto=0  )
        {
           
            try
            {
                var dalListaClientes = new ClienteDAL();
                var dalListaProdutos = new ProdutoDAL();
                ViewBag.ListaClientes = dalListaClientes.RetornarTodosClientes();
                ViewBag.ListaProdutos = dalListaProdutos.TodosProdutos();
                var listaRelatorio = new RelatoriosDAL();
                var listaChamados = listaRelatorio.ObterRelatorioChamados(dataInicial, dataFinal, codigoDoCliente, codigoDoProduto);
                var relatorioGerado = Relatorios.GerarRelatorio<RelatorioChamados>(System.Web.HttpContext.Current.Server.MapPath("~/Relatorios/relatoriochamado.frx"), listaChamados, "Dados", TiposDeRelatorios.PDF, null);
                return File(relatorioGerado, "application/pdf", "relatoriochamado.pdf");
            }
            catch (SqlTypeException)
            {
                ViewBag.Error= "Informe uma data válida!";
                return View();
            }
            
        }

        public JsonResult BuscarProdutosCliente(int idCliente)
        {
            try
            {
                var dalClientesProduto = new ProdutoClienteDAL();

                var listaProdutosDoCliente = dalClientesProduto.RetornarProdutosPorCliente(idCliente);

                return Json(new { Resultado = listaProdutosDoCliente }, JsonRequestBehavior.AllowGet);

            } catch(Exception e)
            {
                return Json(new { Resultado = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}