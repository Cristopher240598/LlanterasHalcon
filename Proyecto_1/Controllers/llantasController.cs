using System;
using System.IO;
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
            var llantas = db.llantas.OrderBy(l => l.modelo).Include(l => l.marcas).Include(l => l.proveedores).Include(l => l.subcategorias);
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
            ViewData["imagenL"] = llantas.imagen;
            ViewData["fechaUlt"] = llantas.ultActualizacion.ToString("dd/MM/yyyy");
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
        public ActionResult Create([Bind(Include = "modelo,descripcion,rin,ancho,perfil,carga,stock,precioVenta,precioCompra,id_proveedor,id_subcategoria,id_marca")] llantas llantas, HttpPostedFileBase imagenLlanta)
        {
            string img = "";
            if (ModelState.IsValid && imagenLlanta != null && imagenLlanta.ContentLength > 0)
            {
                string nombreImagen = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + imagenLlanta.FileName).ToLower();
                imagenLlanta.SaveAs(Server.MapPath("~/Imagenes/llantas/" + nombreImagen));
                img = nombreImagen;
                llantas.imagen = img;
                llantas.existencia = 0;

                DateTime hoy = DateTime.Now;
                llantas.ultActualizacion = hoy;

                db.llantas.Add(llantas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("mensajeImagen", "Ingrese una imagen");
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
            ViewData["imagenL"] = llantas.imagen;
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
        public ActionResult Edit([Bind(Include = "Id,modelo,descripcion,rin,ancho,perfil,carga,stock,precioVenta,precioCompra,id_proveedor,id_subcategoria,id_marca")] llantas llantas, HttpPostedFileBase imagenLlanta)
        {
            string img = "";
            string imgAnterior;
            if (ModelState.IsValid)
            {
                int id = llantas.Id;
                var llanta = db.llantas.Find(id);
                imgAnterior = llanta.imagen;
                ViewBag.img = imgAnterior;
                llanta.modelo = llantas.modelo;
                llanta.descripcion = llantas.descripcion;
                llanta.rin = llantas.rin;
                llanta.ancho = llantas.ancho;
                llanta.perfil = llantas.perfil;
                llanta.carga = llantas.carga;
                llanta.stock = llantas.stock;
                llanta.precioVenta = llantas.precioVenta;
                llanta.precioCompra = llantas.precioCompra;
                DateTime hoy = DateTime.Now;
                llanta.ultActualizacion = hoy;
                llanta.id_proveedor = llantas.id_proveedor;
                llanta.id_subcategoria = llantas.id_subcategoria;
                llanta.id_marca = llantas.id_marca;
                if (imagenLlanta != null && imagenLlanta.ContentLength > 0)
                {
                    string nombreImagen = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + imagenLlanta.FileName).ToLower();
                    imagenLlanta.SaveAs(Server.MapPath("~/Imagenes/llantas/" + nombreImagen));
                    img = nombreImagen;
                    llanta.imagen = img;
                    //Cambiar ruta
                    System.IO.File.Delete(Path.Combine(@"C:\Users\pablo\Source\Repos\Cristopher240598\LlanterasHalcon\Proyecto_1\Imagenes\llantas", imgAnterior));
                  //System.IO.File.Delete(Path.Combine(@"D:\VS\LlanterasHalcon\Proyecto_1\Imagenes\llantas", imgAnterior));
                }
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
            ViewData["imagenL"] = llantas.imagen;
            ViewData["fechaUlt"] = llantas.ultActualizacion.ToString("dd/MM/yyyy");
            return View(llantas);
        }

        // POST: llantas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            llantas llantas = db.llantas.Find(id);
            //Cambiar ruta
            System.IO.File.Delete(Path.Combine(@"C:\Users\pablo\Source\Repos\Cristopher240598\LlanterasHalcon\Proyecto_1\Imagenes\llantas", llantas.imagen));
         //   System.IO.File.Delete(Path.Combine(@"D:\VS\LlanterasHalcon\Proyecto_1\Imagenes\llantas", llantas.imagen));
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
