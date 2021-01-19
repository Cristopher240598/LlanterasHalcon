using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_1.Models
{
    public class PedidoCliente
    {
        private contextLlantera db = new contextLlantera();
        private List<ventas> detalle_orden;

        public PedidoCliente()
        {
            detalle_orden = db.ventas.ToList();
        }
        public ventas Orden
        {
            get;
            set;
        }
        public string Fecha
        {
            get;
            set;
        }
        public string envio
        {
            get;
            set;
        }
     public string status
        {
            get;
            set;
        }
        public string Total
        {
            get;
            set;
        }
        public List<ventas> ordenProductos
        {
            get;
            set;
        }
    }
}