using projeto_vendaCarros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projeto_vendaCarros.Controllers
{
    public class VeiculosController : Controller
    {
        // GET: Veiculos
        public ActionResult Adicionar()
        {
            ViewBag.Title = "Veiculos";
            ViewBag.Message = "Adicionar Veiculos";
            return View(); 
        }

        public ActionResult Alterar(int id)
        {
            ViewBag.Title = "Alterar Veiculo";
            ViewBag.Message = "Id: " + id;

            var veiculo = new VeiculosModel();
            veiculo.GetVeiculo(id);
            //ViewBag.Veiculo = veiculo;
            return View(veiculo);
        }

        public ActionResult Excluir(int id)
        {
            ViewBag.Title = "Excluir Veiculo";
            ViewBag.Message = "Id: " + id;

            var veiculo = new VeiculosModel();
            veiculo.GetVeiculo(id);
            //ViewBag.Veiculo = veiculo;
            return View(veiculo);
        }

        [HttpPost]
        public ActionResult Salvar( VeiculosModel veiculo)
        { 
            if(ModelState.IsValid)
            {
                veiculo.Salvar();
                return RedirectToAction("Veiculo","Home");
            }
            else
            {
                ViewBag.Title = "Veiculos";
                if (Convert.ToInt32("0" + Request["id"]) == 0)
                {
                    ViewBag.Message = "Adicionar veículos";
                    return View("Adicionar");
                }
                else
                {
                    ViewBag.Veiculo = veiculo;
                    ViewBag.Message = "Altera veiculo" + veiculo.Id;
                    return View("Adicionar");
                }
            }
        }

        [HttpPost]
        public void Excluir()
        {
            var veiculo = new VeiculosModel();
            veiculo.Id = Convert.ToInt32("0" + Request["id"]);
            veiculo.Excluir();
            Response.Redirect("/Home/Veiculo");
        }
    }
}