using Rift.Help.DAL;
using Rift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rift.Help.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UsuarioAutenticado"] == null)
            {
                return RedirectToAction("ValidarLogin", "Colaborador"); 
            }
            ViewBag.Nome = ((Colaborador)Session["UsuarioAutenticado"]).Nome;

            var homeDal = new HomeDAL();
            ViewBag.QuantidadeChamado = homeDal.RetornarQuantidadeChamados().QuantidadeChamado;
            ViewBag.QuantidadeClientes = homeDal.RetornarQuantidadeClientes().QuantidadeClientes;
            ViewBag.QuantidadeColaboradores = homeDal.RetornarQuantidadeColaborador().QuantidadeColaboradores;

            return View();
        }

        
    }
}