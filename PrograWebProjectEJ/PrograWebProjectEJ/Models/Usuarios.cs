using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrograWebProjectEJ.Models
{
    public class Usuarios
    {
        [Key]
        public int ID { get; set; }
        public string Correo { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public bool Is_Admin { get; set; }

    }
}