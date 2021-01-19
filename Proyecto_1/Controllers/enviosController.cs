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
            List<EnviosVenUsu> lstEnvios;
            using (db)
            {
                lstEnvios = (from en in db.envios
                             join p in db.paqueterias on en.id_paqueteria equals p.Id
                             join v in db.ventas on en.id_venta equals v.Id
                             join u in db.usuarios on v.id_usuario equals u.Id
                             where en.Id == id
                             select new EnviosVenUsu
                             {
                                 Id = en.Id,
                                 fechaCreacion = en.fechaCreacion,
                                 fechaActualizacion = en.fechaEnvio,
                                 estadoEnvio = en.estado,
                                 paqueteria = p.nombre,
                                 id_venta = en.id_venta,
                                 fechaVenta = v.fechaVenta,
                                 total = v.total,
                                 id_usuario = v.id_usuario,
                                 nombre = u.nombre,
                                 apellidoPaterno = u.apellidoPaterno,
                                 apellidoMaterno = u.apellidoMaterno,
                                 correoElectronico = u.correoElectronico,
                                 telefono = u.telefono,
                                 estado = u.estado,
                                 municipio = u.municipio,
                                 colonia = u.colonia,
                                 calle = u.calle,
                                 numeroCasa = u.numeroCasa,
                                 cp = u.cp
                             }).ToList();
            }
            ViewBag.listaEnvios = lstEnvios.First();
            return View();
        }

        // GET: envios/Create
        public ActionResult Create()
        {
            ViewBag.id_paqueteria = new SelectList(db.paqueterias, "Id", "nombre");
            var envios = db.envios.Select(x => x.id_venta).ToArray();
            ViewBag.id_venta = new SelectList(db.ventas.Where(v => !envios.Contains(v.Id)), "Id", "Id");
            return View();
        }

        // POST: envios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "estado,id_paqueteria,id_venta")] envios envios)
        {
            if (ModelState.IsValid)
            {
                DateTime hoy = DateTime.Now;
                envios.fechaCreacion = hoy;
                envios.fechaEnvio = hoy;
                envios.estado = "Se creo el pedido";
                db.envios.Add(envios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_paqueteria = new SelectList(db.paqueterias, "Id", "nombre", envios.id_paqueteria);
            ViewBag.id_venta = new SelectList(db.ventas, "Id", "Id", envios.id_venta);
            return View(envios);
        }

        [HttpGet]
        public JsonResult obtenerVentaUsuario(int ventaID)
        {
            List<ElementJsonIntKey> lst = new List<ElementJsonIntKey>();
            using (db)
            {
                lst = (from v in db.ventas
                       join u in db.usuarios on v.id_usuario equals u.Id
                       where v.Id == ventaID
                       select new ElementJsonIntKey
                       {
                           fechaVenta = v.fechaVenta,
                           total = v.total,
                           id_usuario = v.id_usuario,
                           nombre = u.nombre,
                           apellidoPaterno = u.apellidoPaterno,
                           apellidoMaterno = u.apellidoMaterno,
                           correoElectronico = u.correoElectronico,
                           telefono = u.telefono,
                           estado = u.estado,
                           municipio = u.municipio,
                           colonia = u.colonia,
                           calle = u.colonia,
                           numeroCasa = u.numeroCasa,
                           cp = u.cp
                       }).ToList();
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public class ElementJsonIntKey
        {
            public System.DateTime fechaVenta { get; set; }
            public decimal total { get; set; }
            public int id_usuario { get; set; }
            public string nombre { get; set; }
            public string apellidoPaterno { get; set; }
            public string apellidoMaterno { get; set; }
            public string correoElectronico { get; set; }
            public string telefono { get; set; }
            public string estado { get; set; }
            public string municipio { get; set; }
            public string colonia { get; set; }
            public string calle { get; set; }
            public int? numeroCasa { get; set; }
            public int? cp { get; set; }
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
            List<EnviosVenUsu> lstEnvios;
            using (db)
            {
                lstEnvios = (from en in db.envios
                             join p in db.paqueterias on en.id_paqueteria equals p.Id
                             join v in db.ventas on en.id_venta equals v.Id
                             join u in db.usuarios on v.id_usuario equals u.Id
                             where en.Id == id
                             select new EnviosVenUsu
                             {
                                 Id = en.Id,
                                 fechaCreacion = en.fechaCreacion,
                                 fechaActualizacion = en.fechaEnvio,
                                 estadoEnvio = en.estado,
                                 paqueteria = p.nombre,
                                 id_venta = en.id_venta,
                                 fechaVenta = v.fechaVenta,
                                 total = v.total,
                                 id_usuario = v.id_usuario,
                                 nombre = u.nombre,
                                 apellidoPaterno = u.apellidoPaterno,
                                 apellidoMaterno = u.apellidoMaterno,
                                 correoElectronico = u.correoElectronico,
                                 telefono = u.telefono,
                                 estado = u.estado,
                                 municipio = u.municipio,
                                 colonia = u.colonia,
                                 calle = u.calle,
                                 numeroCasa = u.numeroCasa,
                                 cp = u.cp
                             }).ToList();
            }
            ViewBag.listaEnvios = lstEnvios.First();
            ViewBag.id_paqueteria = new SelectList(db.paqueterias, "Id", "nombre", envios.id_paqueteria);
            ViewBag.id_venta = new SelectList(db.ventas, "Id", "Id", envios.id_venta);
            return View(envios);
        }

        // POST: envios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,estado")] envios envios)
        {
            if (ModelState.IsValid)
            {
                if (envios.estado == null || envios.estado.Equals(""))
                {
                    List<EnviosVenUsu> lstEnvios;
                    using (db)
                    {
                        lstEnvios = (from en in db.envios
                                     join p in db.paqueterias on en.id_paqueteria equals p.Id
                                     join v in db.ventas on en.id_venta equals v.Id
                                     join u in db.usuarios on v.id_usuario equals u.Id
                                     where en.Id == envios.Id
                                     select new EnviosVenUsu
                                     {
                                         Id = en.Id,
                                         fechaCreacion = en.fechaCreacion,
                                         fechaActualizacion = en.fechaEnvio,
                                         estadoEnvio = en.estado,
                                         paqueteria = p.nombre,
                                         id_venta = en.id_venta,
                                         fechaVenta = v.fechaVenta,
                                         total = v.total,
                                         id_usuario = v.id_usuario,
                                         nombre = u.nombre,
                                         apellidoPaterno = u.apellidoPaterno,
                                         apellidoMaterno = u.apellidoMaterno,
                                         correoElectronico = u.correoElectronico,
                                         telefono = u.telefono,
                                         estado = u.estado,
                                         municipio = u.municipio,
                                         colonia = u.colonia,
                                         calle = u.calle,
                                         numeroCasa = u.numeroCasa,
                                         cp = u.cp
                                     }).ToList();
                    }
                    ViewBag.listaEnvios = lstEnvios.First();
                    ModelState.AddModelError("mensajeEstadoE", "Ingrese un estado de envío");
                    return View(envios);
                }
                int id = envios.Id;
                var envio = db.envios.Find(id);
                DateTime hoy = DateTime.Now;
                envio.fechaEnvio = hoy;
                envio.estado = envios.estado;
                //db.Entry(envios).State = EntityState.Modified;
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
            List<EnviosVenUsu> lstEnvios;
            using (db)
            {
                lstEnvios = (from en in db.envios
                             join p in db.paqueterias on en.id_paqueteria equals p.Id
                             join v in db.ventas on en.id_venta equals v.Id
                             join u in db.usuarios on v.id_usuario equals u.Id
                             where en.Id == id
                             select new EnviosVenUsu
                             {
                                 Id = en.Id,
                                 fechaCreacion = en.fechaCreacion,
                                 fechaActualizacion = en.fechaEnvio,
                                 estadoEnvio = en.estado,
                                 paqueteria = p.nombre,
                                 id_venta = en.id_venta,
                                 fechaVenta = v.fechaVenta,
                                 total = v.total,
                                 id_usuario = v.id_usuario,
                                 nombre = u.nombre,
                                 apellidoPaterno = u.apellidoPaterno,
                                 apellidoMaterno = u.apellidoMaterno,
                                 correoElectronico = u.correoElectronico,
                                 telefono = u.telefono,
                                 estado = u.estado,
                                 municipio = u.municipio,
                                 colonia = u.colonia,
                                 calle = u.calle,
                                 numeroCasa = u.numeroCasa,
                                 cp = u.cp
                             }).ToList();
            }
            ViewBag.listaEnvios = lstEnvios.First();
            return View();
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
