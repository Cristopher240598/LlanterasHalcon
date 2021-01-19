using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_1.Models;

namespace Proyecto_1.Controllers
{
    [Authorize]
    public class PedidosController : Controller
    {
        contextLlantera db = new contextLlantera();
        public ActionResult historialPedidos()
        {
            return View();
        }
        // GET: Pedidos
        public ActionResult Captura()
        {
            string correo = User.Identity.Name;
            usuarios cl = (from c in db.usuarios
                           where c.correoElectronico == correo
                           select c).ToList().FirstOrDefault();

            int id = cl.Id;

            var query = from o in db.ventas
                        where o.id_usuario == id
                        orderby o.fechaVenta ascending
                        select o;

            List<ventas> ventas = query.ToList();

          
            PedidoCliente pedido;
            List<ventas> ordPed;
            List<ItemPedido> itemPed = new List<ItemPedido>();

            ItemPedido iPed;

            foreach (ventas o in ventas)
            {
                pedido = new PedidoCliente();
                pedido.Orden = o;
                pedido.Fecha = o.fechaVenta.ToShortDateString();
                Session["Pedido"] = itemPed;
            }
            return View();
        }
            public ActionResult Index()
            {
                return View();
            }
        }
}


