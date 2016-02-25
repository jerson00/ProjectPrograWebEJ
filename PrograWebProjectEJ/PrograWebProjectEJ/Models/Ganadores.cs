using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrograWebProjectEJ.Models
{
    public class Ganadores
    {
        [Key]
        public int Id { get; set; }
        public Sorteos Sorteos { get; set; }
        public int PrimerNumero { get; set; }
        public int SegundoNumero { get; set; }
        public int TercerNumero { get; set; }

    }
}