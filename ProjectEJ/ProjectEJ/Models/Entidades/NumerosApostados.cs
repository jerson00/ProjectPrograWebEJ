using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectEJ.Models.Entidades
{
    public class NumerosApostados
    {
        public int Numero { get; set; }
        public double Monto { get; set; }
        public bool Ganador { get; set; }
        public double Premio { get; set; }
        public int Puesto { get; set; }
        public string Email { get; set; }
    }
}