//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuarios()
        {
            this.ventas = new HashSet<ventas>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Ingrese el apellido paterno")]
        public string apellidoPaterno { get; set; }
        [Required(ErrorMessage = "Ingrese el apellido materno")]
        public string apellidoMaterno { get; set; }
        [Required(ErrorMessage = "Ingrese el teléfono")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Ingrese un número telefónico válido")]
        public string telefono { get; set; }
        [Required(ErrorMessage = "Ingrese el correo electrónico")]
        [Remote("validarCorreoUnico", "Empleados", HttpMethod = "POST", ErrorMessage = "Este correo electrónico ya existe")]
        [RegularExpression("(^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:.[a-zA-Z0-9-]+)*$)", ErrorMessage = "Ingrese un correo electrónico válido")]
        public string correoElectronico { get; set; }
        public string contrasenia { get; set; }
        [Required(ErrorMessage = "Ingrese el estado")]
        public string estado { get; set; }
        [Required(ErrorMessage = "Ingrese el municipio")]
        public string municipio { get; set; }
        [Required(ErrorMessage = "Ingrese la colonia")]
        public string colonia { get; set; }
        [Required(ErrorMessage = "Ingrese la calle")]
        public string calle { get; set; }
        [Required(ErrorMessage = "Ingrese el número de casa")]
        public Nullable<int> numeroCasa { get; set; }
        [Required(ErrorMessage = "Ingrese el C.P.")]
        public Nullable<int> cp { get; set; }
        public string tarjetaCredito { get; set; }
        public string tipoTarjeta { get; set; }
        public Nullable<int> anio { get; set; }
        public Nullable<int> mes { get; set; }
        public Nullable<int> cvv { get; set; }
        public int id_rol { get; set; }

        public virtual roles roles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ventas> ventas { get; set; }
    }
}
