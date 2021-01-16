using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_1.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult historialPedidos()
        {
            return View();
        }
    }
}