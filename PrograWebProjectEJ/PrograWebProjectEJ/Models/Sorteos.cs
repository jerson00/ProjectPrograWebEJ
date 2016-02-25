using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrograWebProjectEJ.Models
{
    public class Sorteos
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha_Expiracion { get; set; }
        public string Descripcion { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Finished { get; set; }

    }
}