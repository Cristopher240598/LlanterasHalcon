using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Proyecto_1.Models;

namespace Proyecto_1.Controllers
{
    [Authorize]
    public class PagoController : Controller
    {
        private contextLlantera db = new contextLlantera();
        private string NumConfirPago;

        // GET: Pago
        public ActionResult Index()
        {
            return View();
       }





        [AllowAnonymous]
        public ActionResult CrearOrden()
        {
            if (!User.Identity.IsAuthenticated)
            {
                Session["CrearOrden"] = "pend";
                return RedirectToAction("Login", "Account");
            }
            string correo = User.Identity.Name;

            var db = new contextLlantera();
            string fechaCreacion = DateTime.Today.ToShortDateString();
            string fechaProbEntrega = DateTime.Today.AddDays(3).ToShortDateString();
            var usuarios = (from c in db.usuarios
                            where c.correoElectronico == correo
                            select c).ToList().FirstOrDefault();

            Session["dirUsuario"] = usuarios.calle + " " + usuarios.colonia + " " + usuarios.estado;
            Session["fechaOrden"] = fechaCreacion;
            Session["fPEntrega"] = fechaProbEntrega;


            if (usuarios.tarjetaCredito.StartsWith("4"))
                Session["tTarj"] = "1";
            if (usuarios.tarjetaCredito.StartsWith("5"))
                Session["tTarj"] = "2";
            if (usuarios.tarjetaCredito.StartsWith("3"))
                Session["tTarj"] = "3";
            Session["nTarj"] = usuarios.tarjetaCredito;

            return View();

        }

        public ActionResult Pagar(string tipoPago)
        {
            string correo = User.Identity.Name;

            DateTime fechaCreacion = DateTime.Today;
            DateTime fechaProbEntrega = fechaCreacion.AddDays(3);
            var usuarios = (from c in db.usuarios
                           where c.correoElectronico == correo
                           select c).ToList().FirstOrDefault();
            int idUsuario = usuarios.Id;
                             //int idClient = cliente.Id_Cliente;


            if (tipoPago.Equals("T"))
            {
                if (!validaPago(usuarios))
                {
                    return RedirectToAction("pagoNoAceptado");
                }
                else
                {
                    var dirEnt = (from d in db.usuarios
                                  where d.Id == usuarios.Id
                                  select d).ToList().FirstOrDefault();


                    int idDir = dirEnt.Id;

                    //chechar aqui los Id de usuario -vs.--idCliente
                    return RedirectToAction("pagoAceptado", routeValues: new { idC = idUsuario, idD = idDir });

                }
            }
            if (tipoPago.Equals("P"))
            {
               
                    var dirEnt = (from d in db.usuarios
                                  where d.Id == usuarios.Id
                                  select d).ToList().FirstOrDefault();


                    int idDir = dirEnt.Id;
                validaPago(usuarios);


                    //chechar aqui los Id de usuario -vs.--idCliente
                    return RedirectToAction("pagoPaypal", routeValues: new { idC = idUsuario, idD = idDir });

                
            }

            return View();
        }

        public ActionResult pagoNoAceptado()
        {
            return View();
        }





        /*


        //CHECAR NOMBRES DE VARIABLES CLIENTE VS. USUARIOS******************
        public ActionResult pagoAceptado(int idC, int idD)
        {
            orden orden_usuario = new orden();
            int id = 0;
            if (!(DBNull.orden.Max(o => (int?)o.Id_orden) == null))
            {
                id = DBNull.orden.Max(o => object.Id_orden);
            }
            else
            {
                id = 0;
            }
            id++;
            orden_usuario.Id_orden = id;
            orden_usuario.fecha_creacion = DateTime.Today;
            orden_usuario.num_confirmacion = Session["nConfirma"].ToString();
            var carro = Session["cart"] as List<Item>;
            var total = carro.Sum(item => item.llantas.precioVenta * item.cantidad);
            orden_usuario.Total = total;
            orden_usuario.Id_usuario = idC;
            orden_usuario.id_dirEntrega = idD;
            db.orden.Add(orden_usuario);

            db.SaveChanges();


            orden_llantas ordenLlantas;
            List<orden_llantas> listaProdOrd = new List<orden_llantas>();
            foreach (Item linea in carro)

            {
                ordenLlantas = new orden_llantas();
                ordenLlantas.id_orden = ordenLlantas.Id_orden;
                ordenLlantas.cantidad = linea.cantidad;
                db.orden_llantas.Add(ordenLlantas);
            }
            db.SaveChanges();
            Session["cart"] = null;
            Session["nConfirma"] = null;
            Session["itemTotal"] = 0;
            return View();
        }
        */


            public ActionResult pagoPaypal(int idC, int idD)
        {
            Session["idDir"] = idC;
            Session["idUsuario"] = idD;
            return View();
        }




        public ActionResult Tarjetas()
        {
            return View();
        }
      
       
        public ActionResult Paypal()
        {
            return View();
        }

        public ActionResult Correcto()
        {
            return View();
        }

        private bool validaPago(usuarios usuarios)
        {
            bool retorna = true;
            int randomvalue;
            using (RNGCryptoServiceProvider crypto=new RNGCryptoServiceProvider())
            {
                byte[] val = new byte[6];
                crypto.GetBytes(val);
                randomvalue = BitConverter.ToInt32(val, 1);
            }
            NumConfirPago = Math.Abs(randomvalue * 1000).ToString();
            Session["nConfirma"] = NumConfirPago;
            return retorna;

        }

    }
}