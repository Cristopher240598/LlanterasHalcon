//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class envios
    {
        public int Id { get; set; }
        public System.DateTime fechaCreacion { get; set; }
        public Nullable<System.DateTime> fechaEnvio { get; set; }                
        public string estado { get; set; }
        public int id_paqueteria { get; set; }
        public int id_venta { get; set; }
    
        public virtual paqueterias paqueterias { get; set; }
        public virtual ventas ventas { get; set; }
    }
}
