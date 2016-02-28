using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectEJ.Models.Entidades
{
    public class GanadoresQuery
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int PrimerNumero { get; set; }
        public int SegundoNumero { get; set; }
        public int TercerNumero { get; set; }
        public DateTime Fecha_Expiracion { get; set; }

    }
}