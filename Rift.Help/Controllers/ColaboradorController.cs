
using Rift.Help.BLL;
using Rift.Help.DAL;
using Rift.Models;
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web.Mvc;

namespace Rift.Help.Controllers
{
    public class ColaboradorController : Controller
    {
        // GET: Colaborador
        public ActionResult Index()
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }

            var bllColaborador = new ColaboradorBLL();

            
            return View(bllColaborador.RetornarTodosColaboradores());
        }
        public ActionResult Incluir()
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }
            if (TempData["Mensagem"] != null)
                ViewBag.Mensagem = TempData["Mensagem"];

            var dalGeograficos = new GeograficoDAL();
            ViewBag.Estados = dalGeograficos.RetornarTodosEstados();
            return View();
        }
        [HttpPost]
        public ActionResult Incluir(Colaborador colaborador)
        {
            try
            {
                var bllColaborador = new ColaboradorBLL();
                var dalGeograficos = new GeograficoDAL();
                ViewBag.Estados = dalGeograficos.RetornarTodosEstados();
                bllColaborador.IncluirColaborador(colaborador);

                TempData["Mensagem"] = "Funcionário cadastrado com sucesso";

                return RedirectToAction("Incluir");
            }catch(SqlTypeException ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            catch (SqlException)
            {
                ViewBag.Error = "Cpf já utilizado!";
                return View();
            }
           
        }
        public ActionResult Alterar(int IdColaborador)
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }

            var bllColaborador = new ColaboradorBLL();
            var dalGeografico = new GeograficoDAL();
            ViewBag.Estados = dalGeografico.RetornarTodosEstados();
            var colaborador = bllColaborador.RetornarColaboradorPorId(IdColaborador);
            return View(colaborador);
        }
        [HttpPost]
        public ActionResult Alterar(Colaborador colaborador)
        {
            var bllColaborador = new ColaboradorBLL();
            bllColaborador.Alterar(colaborador);
            return View("Index", bllColaborador.RetornarTodosColaboradores());
           
        }
        public ActionResult Excluir(int IdColaborador)
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador");
            }

            var bllColaborador = new ColaboradorBLL();
            var colaborador = bllColaborador.RetornarColaboradorPorId(IdColaborador);
            return View(colaborador);
        }
        [HttpPost]
        public ActionResult Excluir(Colaborador colaborador)
        {
            var bllColaborador = new ColaboradorBLL();
            bllColaborador.Excluir(colaborador.IdColaborador);
            return RedirectToAction("Index");
        }
        public ActionResult ValidarLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ValidarLogin(Colaborador colaborador)
        {
            try
            {
                var bllColaborador = new ColaboradorBLL();
                var result = bllColaborador.ValidarLogin(colaborador);

                Session["UsuarioAutenticado"] = result;
                return RedirectToAction("Index", "Home");
            }
            catch (NullReferenceException e)
            {
                ViewBag.Mensagem = e.Message;
                return View();
            }
        }
        [HttpPost]
        public JsonResult Desconectar()
        {
            Session.Abandon();
            return Json(new { Resultado = "desconectando" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult VerificarEmailCadastrado(string email)
        {
            var bllColaborador = new ColaboradorBLL();
            var verificacaoEmail = bllColaborador.VerificarEmailCadastrado(email);
            return Json(new { Result = verificacaoEmail }, JsonRequestBehavior.AllowGet); 
        }

        public JsonResult RecuperarSenha(string email)
        {
            var bllColaborador = new ColaboradorBLL();
            var passwordColaborador = bllColaborador.RetornarSenha(email);
            if (passwordColaborador != "")
            {
                var Email = new Email(email,passwordColaborador);
            }

            return Json(new { Result = passwordColaborador }, JsonRequestBehavior.AllowGet);
        }
       
    }
}