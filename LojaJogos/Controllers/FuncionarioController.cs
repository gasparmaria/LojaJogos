using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LojaJogos.Models;
using System.Collections.ObjectModel;

namespace LojaJogos.Controllers
{
    public class FuncionarioController : Controller
    {
        public ActionResult CadastrodeFuncionarios()
        {
            return View();
        }
        [HttpPost]

        public ActionResult CadastrodeFuncionarios(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                return View("ResultadoFuncionario", funcionario);
            }
            return View(funcionario);
        }

        public ActionResult ResultadoFuncionario(Funcionario funcionario)
        {
            return View(funcionario);
        }
    }
}