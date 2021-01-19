using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_1.Models;
using Microsoft.AspNet.Identity;
using System.Text;
using WebGrease.Css.Extensions;
using System.Web.Security;
using System.Diagnostics;

namespace Proyecto_1.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private contextLlantera db = new contextLlantera();

        // GET: Clientes
        public ActionResult Index()
        {
            var usuarios = db.usuarios.Include(u => u.roles);
            return View(usuarios.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuarios usuarios = db.usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.id_rol = new SelectList(db.roles, "Id", "nombre");
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,nombre,apellidoPaterno,apellidoMaterno,telefono,correoElectronico,contrasenia,estado,municipio,colonia,calle,numeroCasa,cp,tarjetaCredito,tipoTarjeta,anio,mes,cvv,id_rol")] usuarios usuarios)
        public ActionResult Create(string nombre, string apellidoPaterno, string apellidoMaterno, string telefono, string correoElectronico, string estado, string municipio, string colonia, string calle, int numeroCasa, int cp, string tarjetaCredito, string tipoTarjeta, int anio = 0, int mes = 0, int cvv = 0)
        {
            /*if (ModelState.IsValid)
            {
                db.usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_rol = new SelectList(db.roles, "Id", "nombre", usuarios.id_rol);
            return View(usuarios);*/
            usuarios cliente = new usuarios();

            if (tarjetaCredito == null || tarjetaCredito.Equals(""))
            {
                ModelState.AddModelError("mensajeTarjetaCredito", "Ingrese el número de tarjeta");
                return View(cliente);
            }
            if (tipoTarjeta == null || tipoTarjeta.Equals(""))
            {
                ModelState.AddModelError("mensajeTipoTarjeta", "Seleccione un tipo de tarjeta");
                return View(cliente);
            }
            if (anio == 0)
            {
                ModelState.AddModelError("mensajeAnioTarjeta", "Ingrese el año de la tarjeta");
                return View(cliente);
            }
            if (mes == 0)
            {
                ModelState.AddModelError("mensajeMesTarjeta", "Ingrese el mes de la tarjeta");
                return View(cliente);
            }
            if (cvv == 0)
            {
                ModelState.AddModelError("mensajeCVVTarjeta", "Ingrese el CVV de la tarjeta");
                return View(cliente);
            }
            int id = 0;
            if (!(db.usuarios.Max(c => (int?)c.Id) == null))
            {
                id = db.usuarios.Max(c => c.Id);
            }
            else
            {
                id = 0;
            }
            id++;

            if (comprobarTarjeta(tarjetaCredito, tipoTarjeta, mes, anio, cvv))
            {
                if (validarPago(nombre, calle, colonia, estado, tarjetaCredito, mes, anio, cvv))
                {
                    cliente.Id = id;
                    cliente.nombre = nombre;
                    cliente.apellidoPaterno = apellidoPaterno;
                    cliente.apellidoMaterno = apellidoMaterno;
                    cliente.telefono = telefono;
                    cliente.correoElectronico = Session["correo"].ToString();
                    cliente.contrasenia = null;
                    cliente.estado = estado;
                    cliente.municipio = municipio;
                    cliente.colonia = colonia;
                    cliente.calle = calle;
                    cliente.numeroCasa = numeroCasa;
                    cliente.cp = cp;
                    cliente.tarjetaCredito = tarjetaCredito;
                    cliente.tipoTarjeta = tipoTarjeta;
                    cliente.anio = anio;
                    cliente.mes = mes;
                    cliente.cvv = cvv;
                    cliente.id_rol = 3;
                    db.usuarios.Add(cliente);
                    db.SaveChanges();
                    string[] nombres = cliente.nombre.ToString().Split(' ');
                    Session["name"] = nombres[0];
                    Session["usr"] = cliente.nombre;
                    Session["idUsuarioActual"] = cliente.Id;
                    if (Session["CrearOrden"] != null)
                    {
                        if (Session["CrearOrden"].Equals("pend"))
                        {
                            return RedirectToAction("CrearOrden", "Pago");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("mostrarMensajeInvalida");
                }
            }
            else
            {
                return RedirectToAction("mostrarMensajeInvalida");
            }

            return View(cliente);
        }

        private bool validarPago(string nombre, string calle, string colonia, string estado, string tarjetaCredito, int? mes, int? anio, int? cvv)
        {
            return true;
        }

        private bool comprobarTarjeta(string tarjetaCredito, string tipoTarjeta, int? mes, int? anio, int? cvv)
        {
            //Método luhn
            bool retorna = validarTarjeta(tarjetaCredito);
            if (retorna)
            {
                if ((tarjetaCredito.StartsWith("4")) && (tipoTarjeta.Equals("Visa")))
                {
                    retorna = true;
                }
                else
                {
                    if ((tarjetaCredito.StartsWith("5")) && (tipoTarjeta.Equals("Masterd")))
                    {
                        retorna = true;
                    }
                    else
                    {
                        if ((tarjetaCredito.StartsWith("3")) && (tipoTarjeta.Equals("American")))
                        {
                            retorna = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                DateTime hoy = DateTime.Now;
                if (anio >= hoy.Year)
                {
                    if (anio == hoy.Year)
                    {
                        if (mes > hoy.Month)
                        {
                            retorna = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        retorna = true;
                    }
                }
                else
                {
                    return false;
                }
            }
            return retorna;
        }

        //Método luhn
        private bool validarTarjeta(string tarjetaCredito)
        {
            StringBuilder digitsOnly = new StringBuilder();
            tarjetaCredito.ForEach(c =>
            {
                if (Char.IsDigit(c)) digitsOnly.Append(c);
            });

            if (digitsOnly.Length > 18 || digitsOnly.Length < 15)
            {
                return false;
            };

            int sum = 0;
            int digit = 0;
            int addend = 0;
            bool timesTwo = false;

            for (int i = digitsOnly.Length - 1; i >= 0; i--)
            {
                digit = Int32.Parse(digitsOnly.ToString(i, 1));
                if (timesTwo)
                {
                    addend = digit * 2;
                    if (addend > 9)
                        addend -= 9;
                }
                else
                    addend = digit;

                sum += addend;

                timesTwo = !timesTwo;

            }
            return (sum % 10) == 0;
        }

        public ActionResult mostrarMensajeInvalida()
        {
            return View();
        }

        public ActionResult borrarUsuario()
        {
            string idUsuario = User.Identity.GetUserId();
            return RedirectToAction("Delete", "Account", routeValues: new { id = idUsuario });
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit()
        {
            string cadId = Session["idUsuarioActual"].ToString();
            if (cadId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Debug.WriteLine("--------------Entro" + cadId);
            int id = int.Parse(cadId);
            usuarios usuarios = db.usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_rol = new SelectList(db.roles, "Id", "nombre", usuarios.id_rol);
            return View(usuarios);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nombre,apellidoPaterno,apellidoMaterno,telefono,correoElectronico,estado,municipio,colonia,calle,numeroCasa,cp,tarjetaCredito,tipoTarjeta,anio,mes,cvv")] usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                if (usuarios.tarjetaCredito == null || usuarios.tarjetaCredito.Equals(""))
                {
                    ModelState.AddModelError("mensajeTarjetaCreditoE", "Ingrese el número de tarjeta");
                    return View(usuarios);
                }
                if (usuarios.tipoTarjeta == null || usuarios.tipoTarjeta.Equals(""))
                {
                    ModelState.AddModelError("mensajeTipoTarjetaE", "Seleccione un tipo de tarjeta");
                    return View(usuarios);
                }
                if (usuarios.anio == null)
                {
                    ModelState.AddModelError("mensajeAnioTarjetaE", "Ingrese el año de la tarjeta");
                    return View(usuarios);
                }
                if (usuarios.mes == null)
                {
                    ModelState.AddModelError("mensajeMesTarjetaE", "Ingrese el mes de la tarjeta");
                    return View(usuarios);
                }
                if (usuarios.cvv == null)
                {
                    ModelState.AddModelError("mensajeCVVTarjetaE", "Ingrese el CVV de la tarjeta");
                    return View(usuarios);
                }
                if (comprobarTarjeta(usuarios.tarjetaCredito, usuarios.tipoTarjeta, usuarios.mes, usuarios.anio, usuarios.cvv))
                {
                    if (validarPago(usuarios.nombre, usuarios.calle, usuarios.colonia, usuarios.estado, usuarios.tarjetaCredito, usuarios.mes, usuarios.anio, usuarios.cvv))
                    {
                        int id = usuarios.Id;
                        var usuario = db.usuarios.Find(id);
                        usuario.nombre = usuarios.nombre;
                        usuario.apellidoPaterno = usuarios.apellidoPaterno;
                        usuario.apellidoMaterno = usuarios.apellidoMaterno;
                        usuario.telefono = usuarios.telefono;
                        usuario.estado = usuarios.estado;
                        usuario.municipio = usuarios.municipio;
                        usuario.colonia = usuarios.colonia;
                        usuario.calle = usuarios.calle;
                        usuario.numeroCasa = usuarios.numeroCasa;
                        usuario.cp = usuarios.cp;
                        usuario.tarjetaCredito = usuarios.tarjetaCredito;
                        usuario.tipoTarjeta = usuarios.tipoTarjeta;
                        usuario.anio = usuarios.anio;
                        usuario.mes = usuarios.mes;
                        usuario.cvv = usuarios.cvv;
                        db.SaveChanges();
                        ModelState.AddModelError("datosAct", "Datos actualizados");
                        return View(usuarios);
                    }
                    else
                    {
                        ModelState.AddModelError("mensajeTarjetaCreditoEE", "Revise los datos de la tarjeta de crédito");
                        return View(usuarios);
                    }
                }
                else
                {
                    ModelState.AddModelError("mensajeTarjetaCreditoEE", "Revise los datos de la tarjeta de crédito");
                    return View(usuarios);
                }
            }
            ViewBag.id_rol = new SelectList(db.roles, "Id", "nombre", usuarios.id_rol);
            return View(usuarios);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuarios usuarios = db.usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usuarios usuarios = db.usuarios.Find(id);
            db.usuarios.Remove(usuarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
