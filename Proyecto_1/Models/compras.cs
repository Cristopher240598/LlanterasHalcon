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
    
    public partial class compras
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public compras()
        {
            this.detallesCompras = new HashSet<detallesCompras>();
        }
    
        public int Id { get; set; }
        public System.DateTime fecha { get; set; }
        public decimal iva { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }
        public int id_proveedor { get; set; }
    
        public virtual proveedores proveedores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detallesCompras> detallesCompras { get; set; }
    }
}
