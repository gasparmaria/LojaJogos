using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LojaJogos.Models;
using LojaJogos.Repositorio;

namespace LojaJogos.Controllers
{
    public class FuncionarioController : Controller
    {
        public ActionResult Funcionario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Funcionario(Funcionario fun)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var verificacao = fun.CadastrarFuncionario(fun);
                    if(verificacao == true)
                    {
                        var listarFuncionario = fun.ListarFuncionario();
                        return RedirectToAction("ListarFuncionario", listarFuncionario);
                    }
                }

                return View(fun);
            }
            catch
            {
                return View("ListarFuncionario");
            }
        }

        public ActionResult ListarFuncionario()
        {
            var ExibirFunc = new Funcionario();
            var TodosFunc = ExibirFunc.ListarFuncionario();
            return View(TodosFunc);
        }
    }
}