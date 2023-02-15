using projeto_vendaCarros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projeto_vendaCarros.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Autorizado"] != null)
            {
                return View();
            }
            else
            {
                //Response.Redirect("/Login/Index");
                //return null;
                return RedirectToAction("Index","Login");
            }
        } 
        

        public ActionResult Veiculo()
        {
            ViewBag.Title = "CARROS A VENDA";
            ViewBag.Message = "Listagem de Carros";

            if (Session["Autorizado"] != null)
            {
                var lista = VeiculosModel.GetCarros();
                ViewBag.Lista = lista;

                return View();
            }
            else
            {
                //Response.Redirect("/Login/Index");
                //return null;
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Contact()
        {

            if (Session["Autorizado"] != null)
            {
                ViewBag.Title = "Título da página Contatos";
                ViewBag.Message = "Seus contatos";

                return View();
            }
            else
            {
                //Response.Redirect("/Login/Index");
                //return null;
                return RedirectToAction("Index", "Login");
            }
        }
    }
}