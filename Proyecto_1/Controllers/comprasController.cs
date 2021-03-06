﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_1.Models;

namespace Proyecto_1.Controllers
{
    public class comprasController : Controller
    {
        private contextLlantera db = new contextLlantera();

        // GET: compras
        public ActionResult Index()
        {
            var compras = db.compras.Include(c => c.proveedores);
            return View(compras.ToList());
        }

        // GET: compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compras compras = db.compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }

        // GET: compras/Create
        public ActionResult Create()
        {
            ViewBag.id_proveedor = new SelectList(db.proveedores, "Id", "Id");
            return View();
        }

        // POST: compras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fecha,iva,subtotal,total,id_proveedor")] compras compras)
        {
            if (ModelState.IsValid)
            {
                
                
                db.compras.Add(compras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            string fechaCreacion = DateTime.Today.ToShortDateString();
            
            ViewBag.id_proveedor = new SelectList(db.proveedores, "Id", "Id", compras.id_proveedor);
            return View(compras);
        }

        // GET: compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compras compras = db.compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_proveedor = new SelectList(db.proveedores, "Id", "Id", compras.id_proveedor);
            return View(compras);
        }

        // POST: compras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fecha,iva,subtotal,total,id_proveedor")] compras compras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_proveedor = new SelectList(db.proveedores, "Id", "Id", compras.id_proveedor);
            return View(compras);
        }

        // GET: compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compras compras = db.compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }

        // POST: compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            compras compras = db.compras.Find(id);
            db.compras.Remove(compras);
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
