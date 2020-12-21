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
    public class subcategoriasController : Controller
    {
        private contextLlantera db = new contextLlantera();

        // GET: subcategorias
        public ActionResult Index()
        {
            var subcategorias = db.subcategorias.Include(s => s.categorias);
            return View(subcategorias.ToList());
        }

        // GET: subcategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subcategorias subcategorias = db.subcategorias.Find(id);
            if (subcategorias == null)
            {
                return HttpNotFound();
            }
            return View(subcategorias);
        }

        // GET: subcategorias/Create
        public ActionResult Create()
        {
            ViewBag.id_categoria = new SelectList(db.categorias, "Id", "nombre");
            return View();
        }

        // POST: subcategorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nombre,descripcion,id_categoria")] subcategorias subcategorias)
        {
            if (ModelState.IsValid)
            {
                db.subcategorias.Add(subcategorias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_categoria = new SelectList(db.categorias, "Id", "nombre", subcategorias.id_categoria);
            return View(subcategorias);
        }

        // GET: subcategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subcategorias subcategorias = db.subcategorias.Find(id);
            if (subcategorias == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_categoria = new SelectList(db.categorias, "Id", "nombre", subcategorias.id_categoria);
            return View(subcategorias);
        }

        // POST: subcategorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nombre,descripcion,id_categoria")] subcategorias subcategorias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subcategorias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_categoria = new SelectList(db.categorias, "Id", "nombre", subcategorias.id_categoria);
            return View(subcategorias);
        }

        // GET: subcategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subcategorias subcategorias = db.subcategorias.Find(id);
            if (subcategorias == null)
            {
                return HttpNotFound();
            }
            return View(subcategorias);
        }

        // POST: subcategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            subcategorias subcategorias = db.subcategorias.Find(id);
            db.subcategorias.Remove(subcategorias);
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
