using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LojaJogos.Models;
using LojaJogos.Repositorio;

namespace LojaJogos.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Cliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cliente(Cliente cli)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var verificacao = cli.CadastrarCliente(cli);
                    if(verificacao == true)
                    {
                        var listarCliente = cli.ListarCliente();
                        return RedirectToAction("ListarCliente", listarCliente);
                    }
                }

                return View(cli);
            }
            catch
            {
                return View("ListarCliente");
            }
        }

        public ActionResult ListarCliente()
        {
            var ExibirCli = new Cliente();
            var TodosCli = ExibirCli.ListarCliente();
            return View(TodosCli);
        }
    }
}