using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_1.Models;

namespace Proyecto_1.Controllers
{
    public class marcasController : Controller
    {
        private contextLlantera db = new contextLlantera();

        // GET: marcas
        public ActionResult Index()
        {
            return View(db.marcas.ToList());
        }

        // GET: marcas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marcas marcas = db.marcas.Find(id);
            if (marcas == null)
            {
                return HttpNotFound();
            }
            ViewData["imagenM"] = marcas.imagen;
            return View(marcas);
        }

        // GET: marcas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: marcas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre")] marcas marcas, HttpPostedFileBase imagenMarca)
        {
            Debug.WriteLine(imagenMarca.FileName);
            string img = "";
            if (ModelState.IsValid && imagenMarca!= null && imagenMarca.ContentLength > 0)
            {
                string nombreImagen = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + imagenMarca.FileName).ToLower();
                imagenMarca.SaveAs(Server.MapPath("~/Imagenes/Marcas/" + nombreImagen));
                img = nombreImagen;
                marcas.imagen = img;


                db.marcas.Add(marcas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("mensajeImagen", "Ingrese una imagen");
            return View(marcas);
        }

        // GET: marcas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marcas marcas = db.marcas.Find(id);
            if (marcas == null)
            {
                return HttpNotFound();
            }
            ViewData["imagenM"] = marcas.imagen;

            return View(marcas);
        }

        // POST: marcas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nombre")] marcas marcas, HttpPostedFileBase imagenMarca)
        {
            string img = "";
            string imgAnteriorm;
            if (ModelState.IsValid)
            {
                int id = marcas.Id;
                var marca = db.marcas.Find(id);
                imgAnteriorm = marca.imagen;

                if (imagenMarca != null && imagenMarca.ContentLength > 0)
                {
                    string nombreImagen = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + imagenMarca.FileName).ToLower();
                    imagenMarca.SaveAs(Server.MapPath("~/Imagenes/Marcas/" + nombreImagen));
                    img = nombreImagen;
                    marca.imagen = img;
                    System.IO.File.Delete(Path.Combine(@"C:\Users\ivans\source\repos\Cristopher240598\LlanterasHalcon\Proyecto_1\Imagenes\Marcas", imgAnteriorm));
                }

             //   db.Entry(marcas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marcas);
        }

        // GET: marcas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marcas marcas = db.marcas.Find(id);
            if (marcas == null)
            {
                return HttpNotFound();
            }
            ViewData["imagenM"] = marcas.imagen;
            return View(marcas);
        }

        // POST: marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            marcas marcas = db.marcas.Find(id);
            System.IO.File.Delete(Path.Combine(@"C:\Users\ivans\source\repos\Cristopher240598\LlanterasHalcon\Proyecto_1\Imagenes\Marcas", marcas.imagen));
            
            db.marcas.Remove(marcas);
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
