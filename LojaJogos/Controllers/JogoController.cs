using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LojaJogos.Models;
using LojaJogos.Repositorio;
using System.Collections.ObjectModel;

namespace LojaJogos.Controllers
{
    public class JogoController : Controller
    {
        public ActionResult Jogo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Jogo(Jogo jogo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var verificacao = jogo.CadastrarJogo(jogo);
                    if (verificacao)
                    {
                        var listarJogo = jogo.ListarJogo();
                        return RedirectToAction("ListarJogo", listarJogo);
                    }
                }
                return View(jogo);
            }
            catch
            {
                return View("ListarJogo");
            }
        }

        public ActionResult ListarJogo()
        {
            var ExibirJg = new Jogo();
            var TodosJg = ExibirJg.ListarJogo();
            return View(TodosJg);
        }
    }
}