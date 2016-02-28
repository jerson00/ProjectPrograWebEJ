using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectEJ.Models.Entidades
{
    public class ApuestasQuery
    {
        public int Id { get; set; }
        public string Usuario_Id { get; set; }
        public int Sorteo_Id { get; set; }
        public int Numero { get; set; }
        public double Monto { get; set; }
    }
}