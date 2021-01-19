using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_1.Models;
using System.Diagnostics;
//aksdjkasd
namespace Proyecto_1.Controllers
{
    [Authorize]
    public class EmpleadosController : Controller
    {
        private contextLlantera db = new contextLlantera();

        // GET: Empleados
        public ActionResult Index()
        {
            var usuarios = db.usuarios.Where(u => u.id_rol == 2).OrderBy(u => u.apellidoPaterno).Include(u => u.roles);
            return View(usuarios.ToList());
        }

        // GET: Empleados/Details/5
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

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.id_rol = new SelectList(db.roles, "Id", "nombre");
            return View();
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre,apellidoPaterno,apellidoMaterno,telefono,correoElectronico,estado,municipio,colonia,calle,numeroCasa,cp")] usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.contrasenia = null;
                usuarios.tarjetaCredito = null;
                usuarios.tipoTarjeta = null;
                usuarios.anio = null;
                usuarios.mes = null;
                usuarios.cvv = null;
                usuarios.id_rol = 2;
                db.usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_rol = new SelectList(db.roles, "Id", "nombre", usuarios.id_rol);
            return View(usuarios);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.id_rol = new SelectList(db.roles, "Id", "nombre", usuarios.id_rol);
            return View(usuarios);
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nombre,apellidoPaterno,apellidoMaterno,telefono,correoElectronico,estado,municipio,colonia,calle,numeroCasa,cp")] usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(usuarios).State = EntityState.Modified;
                int id = usuarios.Id;
                var empleado = db.usuarios.Find(id);

                empleado.nombre = usuarios.nombre;
                empleado.apellidoPaterno = usuarios.apellidoPaterno;
                empleado.apellidoMaterno = usuarios.apellidoMaterno;
                empleado.telefono = usuarios.telefono;
                empleado.estado = usuarios.estado;
                empleado.municipio = usuarios.municipio;
                empleado.colonia = usuarios.colonia;
                empleado.calle = usuarios.calle;
                empleado.numeroCasa = usuarios.numeroCasa;
                empleado.cp = usuarios.cp;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_rol = new SelectList(db.roles, "Id", "nombre", usuarios.id_rol);
            return View(usuarios);
        }

        // GET: Empleados/Delete/5
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

        // POST: Empleados/Delete/5
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

        [HttpPost]
        public JsonResult validarCorreoUnico(String correoElectronico)
        {
            //Debug.WriteLine("-----Entro");
            using (contextLlantera db = new contextLlantera())
            {
                try
                {
                    var cE = db.usuarios.Single(m => m.correoElectronico == correoElectronico);
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}
