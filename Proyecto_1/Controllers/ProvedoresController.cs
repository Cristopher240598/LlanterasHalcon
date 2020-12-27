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
    public class ProvedoresController : Controller
    {

        private contextLlantera db = new contextLlantera();
        // GET: Provedores
        public ActionResult Index()
        {
            return View(db.proveedores.ToList());
        }

        // GET: Provedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proveedores provedores = db.proveedores.Find(id);
            if (provedores == null)
            {
                return HttpNotFound();
            }
            return View(provedores);
        }

        // GET: Provedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Provedores/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,RazonSocial,rfc,direccion,telefono,correoElectronico")] proveedores provedores)
        {
            if (ModelState.IsValid)
            {
                db.proveedores.Add(provedores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(provedores);
        }

        // GET: Provedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proveedores provedores = db.proveedores.Find(id);
            if (provedores == null)
            {
                return HttpNotFound();
            }
            return View(provedores);
        }

        // POST: Provedores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,razonSocial,rfc,direccion,telefono,correoElectronico")] proveedores provedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(provedores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(provedores);
        }

        // GET: Provedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proveedores provedores = db.proveedores.Find(id);
            if (provedores == null)
            {
                return HttpNotFound();
            }
            return View(provedores);
        }

        // POST: Provedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            proveedores provedores = db.proveedores.Find(id);
            db.proveedores.Remove(provedores);
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
