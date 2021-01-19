using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_1.Models
{
    public class llantasSubCat
    {
        public int Id { get; set; }
        public string modelo { get; set; }
        public string descripcion { get; set; }
        public int rin { get; set; }
        public int ancho { get; set; }
        public int perfil { get; set; }
        public int carga { get; set; }
        public string imagen { get; set; }
        public int stock { get; set; }
        public int existencia { get; set; }
        public decimal precioVenta { get; set; }
        public decimal precioCompra { get; set; }
        public System.DateTime ultActualizacion { get; set; }
        public string proveedor { get; set; }
        public string categoria { get; set; }
        public string subcategoria { get; set; }
        public string marca { get; set; }
    }
}