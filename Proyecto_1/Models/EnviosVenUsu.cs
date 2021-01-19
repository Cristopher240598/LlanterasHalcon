using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_1.Models
{
    public class EnviosVenUsu
    {

        public int Id { get; set; }
        public System.DateTime fechaCreacion { get; set; }
        public Nullable<System.DateTime> fechaActualizacion { get; set; }
        public string estadoEnvio { get; set; }
        public string paqueteria { get; set; }
        public int id_venta { get; set; }
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
}