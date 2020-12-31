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
        List<LlantaContador> lista;

        public ActionResult Index()
        {
            //Obtener todas los detalles de ventas
            var ventasQuery = from v in db.detallesVentas
                                select v;
            var ventas = ventasQuery.ToList();
            lista = new List<LlantaContador>();
            //Organizar por id de llanta y sus ventas de cada una
            foreach (detallesVentas dVenta in ventas)
            {
                AgregarEnLista(dVenta.id_llanta, dVenta.cantidad);
            }
            //Ordenar la lista por ventas (menor a mayor)
            List<LlantaContador> listaOrdenada = lista.OrderBy(x => x.contador).ToList();
            //Mantener lista hasta un máximo de 6 llantas
            while (listaOrdenada.Count > 6) 
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
            return View();
        }

        private void AgregarEnLista(int id, int n) //Organizar ventas de cada llanta
        {
            int idx = -1; //Obtener índice de la llanta
            for (int l = 0; l < lista.Count; l++)
            {
                if (lista[l].idLlanta == id)
                {
                    idx = l; break;
                }
            }
            if (idx == -1) //Agregar nuevo registro
            {
                lista.Add(new LlantaContador(id, n));
            } 
            else //Sumar cantidad
            {
                lista[idx].contador += n;
            }
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
            return View();
        }

    }
}