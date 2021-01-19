using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_1.Models;

namespace Proyecto_1.Controllers
{
    public class enviosController : Controller
    {
        private contextLlantera db = new contextLlantera();

        // GET: envios
        public ActionResult Index()
        {
            var envios = db.envios.Include(e => e.paqueterias).Include(e => e.ventas);
            return View(envios.ToList());
        }

        // GET: envios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            envios envios = db.envios.Find(id);
            if (envios == null)
            {
                return HttpNotFound();
            }
            return View(envios);
        }

        // GET: envios/Create
        public ActionResult Create()
        {
            ViewBag.id_paqueteria = new SelectList(db.paqueterias, "Id", "nombre");
            ViewBag.id_venta = new SelectList(db.ventas, "Id", "Id");
            return View();
        }

        // POST: envios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fechaCreacion,fechaEnvio,estado,id_paqueteria,id_venta")] envios envios)
        {
            if (ModelState.IsValid)
            {
                db.envios.Add(envios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_paqueteria = new SelectList(db.paqueterias, "Id", "nombre", envios.id_paqueteria);
            ViewBag.id_venta = new SelectList(db.ventas, "Id", "Id", envios.id_venta);
            return View(envios);
        }

        // GET: envios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            envios envios = db.envios.Find(id);
            if (envios == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_paqueteria = new SelectList(db.paqueterias, "Id", "nombre", envios.id_paqueteria);
            ViewBag.id_venta = new SelectList(db.ventas, "Id", "Id", envios.id_venta);
            return View(envios);
        }

        // POST: envios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fechaCreacion,fechaEnvio,estado,id_paqueteria,id_venta")] envios envios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(envios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_paqueteria = new SelectList(db.paqueterias, "Id", "nombre", envios.id_paqueteria);
            ViewBag.id_venta = new SelectList(db.ventas, "Id", "Id", envios.id_venta);
            return View(envios);
        }

        // GET: envios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            envios envios = db.envios.Find(id);
            if (envios == null)
            {
                return HttpNotFound();
            }
            return View(envios);
        }

        // POST: envios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            envios envios = db.envios.Find(id);
            db.envios.Remove(envios);
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
