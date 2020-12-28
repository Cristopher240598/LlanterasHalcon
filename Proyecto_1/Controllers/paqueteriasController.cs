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
    public class paqueteriasController : Controller
    {
        private contextLlantera db = new contextLlantera();

        // GET: paqueterias
        public ActionResult Index()
        {
            return View(db.paqueterias.ToList());
        }

        // GET: paqueterias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paqueterias paqueterias = db.paqueterias.Find(id);
            if (paqueterias == null)
            {
                return HttpNotFound();
            }
            return View(paqueterias);
        }

        // GET: paqueterias/Create
        public ActionResult Create()
        {
            return View();
        }
        //ejeemplo 
        // POST: paqueterias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nombre,web,telefono")] paqueterias paqueterias)
        {
            if (ModelState.IsValid)
            {
                db.paqueterias.Add(paqueterias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paqueterias);
        }

        // GET: paqueterias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paqueterias paqueterias = db.paqueterias.Find(id);
            if (paqueterias == null)
            {
                return HttpNotFound();
            }
            return View(paqueterias);
        }

        // POST: paqueterias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nombre,web,telefono")] paqueterias paqueterias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paqueterias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paqueterias);
        }

        // GET: paqueterias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paqueterias paqueterias = db.paqueterias.Find(id);
            if (paqueterias == null)
            {
                return HttpNotFound();
            }
            return View(paqueterias);
        }

        // POST: paqueterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            paqueterias paqueterias = db.paqueterias.Find(id);
            db.paqueterias.Remove(paqueterias);
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
