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
    public class llantasController : Controller
    {
        private contextLlantera db = new contextLlantera();

        // GET: llantas
        public ActionResult Index()
        {
            var llantas = db.llantas.Include(l => l.marcas).Include(l => l.proveedores).Include(l => l.subcategorias);
            return View(llantas.ToList());
        }

        // GET: llantas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            llantas llantas = db.llantas.Find(id);
            if (llantas == null)
            {
                return HttpNotFound();
            }
            return View(llantas);
        }

        // GET: llantas/Create
        public ActionResult Create()
        {
            ViewBag.id_marca = new SelectList(db.marcas, "Id", "nombre");
            ViewBag.id_proveedor = new SelectList(db.proveedores, "Id", "razonSocial");
            ViewBag.id_subcategoria = new SelectList(db.subcategorias, "Id", "nombre");
            return View();
        }

        // POST: llantas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,modelo,descripcion,rin,ancho,perfil,carga,imagen,stock,existencia,precioVenta,precioCompra,ultActualizacion,id_proveedor,id_subcategoria,id_marca")] llantas llantas)
        {
            if (ModelState.IsValid)
            {
                db.llantas.Add(llantas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_marca = new SelectList(db.marcas, "Id", "nombre", llantas.id_marca);
            ViewBag.id_proveedor = new SelectList(db.proveedores, "Id", "razonSocial", llantas.id_proveedor);
            ViewBag.id_subcategoria = new SelectList(db.subcategorias, "Id", "nombre", llantas.id_subcategoria);
            return View(llantas);
        }

        // GET: llantas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            llantas llantas = db.llantas.Find(id);
            if (llantas == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_marca = new SelectList(db.marcas, "Id", "nombre", llantas.id_marca);
            ViewBag.id_proveedor = new SelectList(db.proveedores, "Id", "razonSocial", llantas.id_proveedor);
            ViewBag.id_subcategoria = new SelectList(db.subcategorias, "Id", "nombre", llantas.id_subcategoria);
            return View(llantas);
        }

        // POST: llantas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,modelo,descripcion,rin,ancho,perfil,carga,imagen,stock,existencia,precioVenta,precioCompra,ultActualizacion,id_proveedor,id_subcategoria,id_marca")] llantas llantas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(llantas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_marca = new SelectList(db.marcas, "Id", "nombre", llantas.id_marca);
            ViewBag.id_proveedor = new SelectList(db.proveedores, "Id", "razonSocial", llantas.id_proveedor);
            ViewBag.id_subcategoria = new SelectList(db.subcategorias, "Id", "nombre", llantas.id_subcategoria);
            return View(llantas);
        }

        // GET: llantas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            llantas llantas = db.llantas.Find(id);
            if (llantas == null)
            {
                return HttpNotFound();
            }
            return View(llantas);
        }

        // POST: llantas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            llantas llantas = db.llantas.Find(id);
            db.llantas.Remove(llantas);
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
