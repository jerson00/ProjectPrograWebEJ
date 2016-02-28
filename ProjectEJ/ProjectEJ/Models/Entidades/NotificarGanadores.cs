using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectEJ.Models.Entidades
{
    public class NotificarGanadores
    {
        public string Email { get; set; }
        public int Sorteo_Id { get; set; }
        public int Numero { get; set; }
        public int Posicion { get; set; }
        public double Premio { get; set; }
    }
}