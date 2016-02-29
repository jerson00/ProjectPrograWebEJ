using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectEJ.Models.Entidades
{
    public class ApuestaResponse
    {
        public string Status { get; set; }
        public string MontoSugerido { get; set; }
        public string Numero { get; set; }
        public string Monto { get; set; }
    }
}