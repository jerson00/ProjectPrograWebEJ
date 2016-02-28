using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectEJ.Models
{
    public class Apuestas
    {
        [Key]
        public int Id { get; set; }
        public string Usuario_Id { get; set; }
        public Sorteos Sorteos { get; set; }
        public int Numero { get; set; }
        public double Monto { get; set; }


    }
}