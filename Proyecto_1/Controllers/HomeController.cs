using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_1.Models;

namespace Proyecto_1.Controllers
{
    public class HomeController : Controller
    {
        private contextLlantera db = new contextLlantera();

        public ActionResult Index()
        {
            ObtenerMarcasAleatorias(6);
            ObtenerLLantasMasVendidas(6);            
            return View();
        }

        private void ObtenerMarcasAleatorias(int numMarcas)
        {
            //Obtener todos las marcas
            var marcasQuery = from m in db.marcas
                              select m;
            var marcas = marcasQuery.ToList();
            int idAux;
            Random rdm = new Random();
            //Eliminar registros aleatorios hasta obtener el número de marcas deseadas
            while (marcas.Count > numMarcas) 
            {
                idAux = rdm.Next(0, marcas.Count);
                if (idAux >= 0 && idAux < marcas.Count)
                {
                    marcas.RemoveAt(idAux);
                }
            }
            ViewBag.marcas = marcas;
        }

        private void ObtenerLLantasMasVendidas(int numLlantas)
        {
            //Obtener todas los detalles de ventas
            var ventasQuery = from v in db.detallesVentas
                              select v;
            var ventas = ventasQuery.ToList();
            List<LlantaContador> lista = new List<LlantaContador>();
            int idx = 0;
            //Organizar por id de llanta y sus ventas de cada una
            foreach (detallesVentas dVenta in ventas)
            {
                idx = -1;
                for (int l = 0; l < lista.Count; l++)
                {
                    if (lista[l].idLlanta == dVenta.id_llanta)
                    {
                        idx = l; break;
                    }
                }
                if (idx == -1) //Agregar nuevo registro
                {
                    lista.Add(new LlantaContador(dVenta.id_llanta, dVenta.cantidad));
                }
                else //Sumar cantidad
                {
                    lista[idx].contador += dVenta.cantidad;
                }
            }
            //Ordenar la lista por ventas (menor a mayor)
            List<LlantaContador> listaOrdenada = lista.OrderBy(x => x.contador).ToList();
            //Mantener lista hasta un máximo de 6 llantas
            while (listaOrdenada.Count > numLlantas)
            {
                listaOrdenada.RemoveAt(0);
            }
            //Obtener los registros de las llantas más vendidas
            List<llantas> llantasMasVendidas = new List<llantas>();
            List<int> llantasVentas = new List<int>();
            int idAux;
            while (listaOrdenada.Count > 0)
            {
                idAux = listaOrdenada[listaOrdenada.Count - 1].idLlanta; //Obtener id de la llanta más vendida de la lista
                var llanta = from ll in db.llantas
                             where ll.Id == idAux
                             select ll; //Obtener el registro de la llanta
                llantasMasVendidas.Add(llanta.ToList()[0]); //Agregar registro a una nueva lista
                llantasVentas.Add(listaOrdenada[listaOrdenada.Count - 1].contador); //Agregar ventas del registro de llantas
                listaOrdenada.RemoveAt(listaOrdenada.Count - 1); //Eliminar el id de la llanta obtenida
            }
            ViewBag.llantasMasVendidas = llantasMasVendidas;
            ViewBag.llantasVentas = llantasVentas;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";    
            return View();
        }

        public ActionResult catalogo()
        {          
            Session["lstLlantas"] = new List<llantas>();
            Session["typeOrden"] = 1; //Por defecto el orden es alfabético
            Session["filtroMarcas"] = new List<string>();
            Session["filtroRines"] = new List<int>();
            Session["filtroAnchos"] = new List<int>();
            ObtenerCategorias();
            return View();
        }

        private void GuardarLlantas(List<llantas> ll, int to)
        {
            Session["lstLlantas"] = ll;
            Session["typeOrden"] = to;
        }

        private void ObtenerCategorias()
        {
            //Obtener todas las categorías
            var categoriasQuery = from c in db.categorias
                                  select c;
            var categorias = categoriasQuery.ToList();
            ViewBag.categorias = categorias;
        }

        public ActionResult ObtenerSubcategorias(int idCategoria)
        {
            return Json(db.subcategorias.Where(s => s.id_categoria == idCategoria).Select(s => new {
                id = s.Id,
                nombre = s.nombre
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerLlantas(int idSubcategoria)
        {
            List<llantas> lstLlantas = (List<llantas>)Session["lstLlantas"];
            int typeOrden = (int)Session["typeOrden"];
            //Obtener llantas de una subcategoría
            var llanta = from ll in db.llantas
                         where ll.id_subcategoria == idSubcategoria
                         select ll;
            lstLlantas.Clear(); lstLlantas = llanta.ToList();
            GuardarLlantas(lstLlantas, typeOrden); //Guardar en sesión
            //Ordenar lista
            switch (typeOrden)
            {
                case 1: //Modelo A-Z
                    lstLlantas = lstLlantas.OrderBy(x => x.modelo).ToList();
                    break;
                case 2: //Precio ASC
                    lstLlantas = lstLlantas.OrderBy(x => x.precioVenta).ToList();
                    break;
                case 3: //Precio DES
                    lstLlantas = lstLlantas.OrderBy(x => x.precioVenta).ToList();
                    lstLlantas.Reverse();
                    break;
            }
            //Obtener listas de filtros
            List<string> marcas = (List<string>)Session["filtroMarcas"]; //Marcas
            marcas.Clear();
            if (lstLlantas.Count > 0)
            {
                foreach (llantas ll in lstLlantas)
                {
                    marcas.Add(ll.marcas.nombre);
                }
            }
            marcas = marcas.Distinct().ToList(); marcas.Sort();
            Session["filtroMarcas"] = marcas;
            List<int> rines = (List<int>)Session["filtroRines"]; //Rines
            rines.Clear();
            if (lstLlantas.Count > 0)
            {
                foreach (llantas ll in lstLlantas)
                {
                    rines.Add(ll.rin);
                }
            }
            rines = rines.Distinct().ToList(); rines.Sort();
            Session["filtroRines"] = rines;
            List<int> anchos = (List<int>)Session["filtroAnchos"]; //Anchos
            anchos.Clear();
            if (lstLlantas.Count > 0)
            {
                foreach (llantas ll in lstLlantas)
                {
                    anchos.Add(ll.ancho);
                }
            }
            anchos = anchos.Distinct().ToList(); anchos.Sort();
            Session["filtroAnchos"] = anchos;
            //Retornar lista en formato JSON
            return Json(lstLlantas.Select(l => new {
                id = l.Id,
                modelo = l.modelo,
                marca = l.marcas.nombre,
                rin = l.rin,
                ancho = l.ancho,
                precio = l.precioVenta,
                img = l.imagen
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrdenarLlantas(int valOrden)
        {
            List<llantas> lstLlantas = (List<llantas>)Session["lstLlantas"];
            int typeOrden = (int)Session["typeOrden"];
            typeOrden = valOrden;
            if (lstLlantas != null)
            {
                switch (typeOrden)
                {
                    case 1: //Modelo A-Z
                        lstLlantas = lstLlantas.OrderBy(x => x.modelo).ToList();
                        break;
                    case 2: //Precio ASC
                        lstLlantas = lstLlantas.OrderBy(x => x.precioVenta).ToList();
                        break;
                    case 3: //Precio DES
                        lstLlantas = lstLlantas.OrderBy(x => x.precioVenta).ToList();
                        lstLlantas.Reverse();
                        break;
                }
            }
            GuardarLlantas(lstLlantas, typeOrden); //Guardar en sesión
            //Retornar lista en formato JSON
            return Json(lstLlantas.Select(l => new {
                id = l.Id,
                modelo = l.modelo,
                marca = l.marcas.nombre,
                rin = l.rin,
                ancho = l.ancho,
                precio = l.precioVenta,
                img = l.imagen
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerFiltrosMarcas()
        {
            return Json(((List<string>)Session["filtroMarcas"]).Select(m => new {
                nombre = m
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerFiltrosRines()
        {
            return Json(((List<int>)Session["filtroRines"]).Select(a => new {
                nombre = a
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerFiltrosAnchos()
        {
            return Json(((List<int>)Session["filtroAnchos"]).Select(a => new {
                nombre = a
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FiltrarLlantas(int marca, int rin, int ancho)
        {
            List<llantas> lstLlantas = (List<llantas>)Session["lstLlantas"];
            List<string> marcas = (List<string>)Session["filtroMarcas"];
            List<int> rines = (List<int>)Session["filtroRines"];
            List<int> anchos = (List<int>)Session["filtroAnchos"];
            //Llenar lista temporal
            List<llantas> lstFiltro = new List<llantas>();
            foreach (llantas ll in lstLlantas)
            {
                lstFiltro.Add(ll);
            }
            //Filtrar por marca seleccionada
            if (marca != -1)
            {
                List<llantas> lst = new List<llantas>();
                foreach (llantas ll in lstFiltro)
                {
                    if (ll.marcas.nombre.Equals(marcas[marca]))
                    {
                        lst.Add(ll);
                    }
                }
                lstFiltro = lst;
            }
            //Filtrar por rin seleccionado
            if (rin != -1)
            {
                List<llantas> lst = new List<llantas>();
                foreach (llantas ll in lstFiltro)
                {
                    if (ll.rin == rines[rin])
                    {
                        lst.Add(ll);
                    }
                }
                lstFiltro = lst;
            }
            //Filtrar por ancho seleccionado
            if (ancho != -1)
            {
                List<llantas> lst = new List<llantas>();
                foreach (llantas ll in lstFiltro)
                {
                    if (ll.ancho == anchos[ancho])
                    {
                        lst.Add(ll);
                    }
                }
                lstFiltro = lst;
            }
            //Ordenar lista
            switch ((int)Session["typeOrden"])
            {
                case 1: //Modelo A-Z
                    lstFiltro = lstFiltro.OrderBy(x => x.modelo).ToList();
                    break;
                case 2: //Precio ASC
                    lstFiltro = lstFiltro.OrderBy(x => x.precioVenta).ToList();
                    break;
                case 3: //Precio DES
                    lstFiltro = lstFiltro.OrderBy(x => x.precioVenta).ToList();
                    lstFiltro.Reverse();
                    break;
            }
            //Retornar lista en formato JSON
            return Json(lstFiltro.Select(l => new {
                id = l.Id,
                modelo = l.modelo,
                marca = l.marcas.nombre,
                rin = l.rin,
                ancho = l.ancho,
                precio = l.precioVenta,
                img = l.imagen
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detalle(int id)
        {
            //Obtener registro de llanta de la lista
            List<llantas> lstLlantas = (List<llantas>)Session["lstLlantas"];
            llantas llanta = null;
            foreach (llantas ll in lstLlantas)
            {
                if (ll.Id == id)
                {
                    llanta = ll; break;
                }
            }
            if (llanta != null)
            {
                ViewBag.detalleId = llanta.Id;
                ViewBag.detalleModelo = llanta.modelo;
                ViewBag.detalleDescripcion = llanta.descripcion;
                ViewBag.detalleRin = llanta.rin;
                ViewBag.detalleAncho = llanta.ancho;
                ViewBag.detallePerfil = llanta.perfil;
                ViewBag.detalleCarga = llanta.carga;
                ViewBag.detallePrecio = llanta.precioVenta;
                ViewBag.detalleMarca = llanta.marcas.nombre;
                ViewBag.detalleImg = llanta.imagen;
            }
            return View();
        }
    }
}