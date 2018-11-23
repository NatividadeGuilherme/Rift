using Rift.Help.BLL;
using Rift.Help.DAL;
using Rift.Models;
using System.Web.Mvc;

namespace Rift.Help.Controllers
{
    public class AcoesController : Controller
    {
        // GET: Acoes
        public ActionResult Index(int idChamado, string titulo, string status)
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }
            if (TempData["Mensagem"] != null)
            {
                ViewBag.Mensagem = TempData["Mensagem"];
            }
            var dalAcoes = new AcoesChamadoDAL();
            ViewBag.IdChamado = idChamado;
            ViewBag.Titulo = titulo;
            ViewBag.Status = status;
            var listaAcoesChamados = dalAcoes.RetornarTodasAcoesChamado(idChamado);
            return View(listaAcoesChamados);
        }
        public ActionResult CriarAcoesChamado(int IdChamado, string titulo)
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }
            var acoesChamado = new AcoesChamado();
            ViewBag.Titulo = titulo;
            acoesChamado.IdChamado = IdChamado;
            return View(acoesChamado);
        }
        [HttpPost]
        public ActionResult CriarAcoesChamado(AcoesChamado acoes, string titulo)
        {
            var dalAcoesChamado = new AcoesChamadoDAL();
            var idColaborador = ((Colaborador)Session["UsuarioAutenticado"]).IdColaborador;

            acoes.IdColaborador = idColaborador;
            dalAcoesChamado.IncluirAcoes(acoes);
            TempData["Mensagem"] = "Ação registrada!";
            return RedirectToAction("Index", new { idChamado = acoes.IdChamado, Titulo= titulo });
        }
        public ActionResult AlterarAcoes(int idAcoes, string titulo)
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }
            var dalAcoes = new AcoesChamadoDAL();
            ViewBag.Titulo = titulo;
            var acao = dalAcoes.RetornarAcao(idAcoes);
            return View(acao);
        }
        [HttpPost]
        public ActionResult AlterarAcoes(AcoesChamado acoes, string titulo)
        {
            var dalAcoes = new AcoesChamadoDAL();
            dalAcoes.AlterarAcoes(acoes);
            TempData["Mensagem"] = "Ação alterada!";
            return RedirectToAction("Index", new { IdChamado = acoes.IdChamado, Titulo = titulo });
        }
        public JsonResult ExcluirAcao(int idAcoes)
        {
         
            var dalAcoesChamado = new AcoesChamadoDAL();
            var resultExclusao = dalAcoesChamado.ExcluirAcoes(idAcoes);
            TempData["Mensagem"] = "Registro Excluído com sucesso";
            return Json(new { Result = resultExclusao }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult FinalizarChamado(int idChamado)
        {
            
                var bllChamado = new ChamadoBLL();
                var resultChamadoFinalizado = bllChamado.FinalizarChamado(idChamado);
                return Json(new { Sucess = resultChamadoFinalizado }, JsonRequestBehavior.AllowGet);
            
            
        }
    }
}