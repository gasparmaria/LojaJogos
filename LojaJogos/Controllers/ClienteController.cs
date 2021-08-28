using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LojaJogos.Models;
using System.Collections.ObjectModel;

namespace LojaJogos.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult CadastrodeClientes()
        {
            return View();
        }
        [HttpPost]

        public ActionResult CadastrodeClientes(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                return View("ResultadoCliente", cliente);
            }
            return View(cliente);
        }

        public ActionResult ResultadoCliente(Cliente cliente)
        {
            return View(cliente);
        }
    }
}