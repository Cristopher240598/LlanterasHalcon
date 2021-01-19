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
    public class detallesComprasController : Controller
    {
        private contextLlantera db = new contextLlantera();

        // GET: detallesCompras
        public ActionResult Index()
        {
            var detallesCompras = db.detallesCompras.Include(d => d.compras).Include(d => d.llantas);
            return View(detallesCompras.ToList());
        }

        // GET: detallesCompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detallesCompras detallesCompras = db.detallesCompras.Find(id);
            if (detallesCompras == null)
            {
                return HttpNotFound();
            }
            return View(detallesCompras);
        }

        // GET: detallesCompras/Create
        public ActionResult Create()
        {
            ViewBag.id_compra = new SelectList(db.compras, "Id", "Id");
            ViewBag.id_llanta = new SelectList(db.llantas, "Id", "modelo");
            return View();
        }

        // POST: detallesCompras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,cantidad,precioCompra,precioVenta,id_compra,id_llanta")] detallesCompras detallesCompras)
        {
            if (ModelState.IsValid)
            {
                db.detallesCompras.Add(detallesCompras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            string fechaCreacion = DateTime.Today.ToShortDateString();
            
            ViewBag.id_compra = new SelectList(db.compras, "Id", "Id", detallesCompras.id_compra);
            ViewBag.id_llanta = new SelectList(db.llantas, "Id", "modelo", detallesCompras.id_llanta);
            return View(detallesCompras);
        }

        // GET: detallesCompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detallesCompras detallesCompras = db.detallesCompras.Find(id);
            if (detallesCompras == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_compra = new SelectList(db.compras, "Id", "Id", detallesCompras.id_compra);
            ViewBag.id_llanta = new SelectList(db.llantas, "Id", "modelo", detallesCompras.id_llanta);
            return View(detallesCompras);
        }

        // POST: detallesCompras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,cantidad,precioCompra,precioVenta,id_compra,id_llanta")] detallesCompras detallesCompras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detallesCompras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_compra = new SelectList(db.compras, "Id", "Id", detallesCompras.id_compra);
            ViewBag.id_llanta = new SelectList(db.llantas, "Id", "modelo", detallesCompras.id_llanta);
            return View(detallesCompras);
        }

        // GET: detallesCompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detallesCompras detallesCompras = db.detallesCompras.Find(id);
            if (detallesCompras == null)
            {
                return HttpNotFound();
            }
            return View(detallesCompras);
        }

        // POST: detallesCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            detallesCompras detallesCompras = db.detallesCompras.Find(id);
            db.detallesCompras.Remove(detallesCompras);
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
