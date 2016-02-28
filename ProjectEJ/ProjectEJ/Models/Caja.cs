using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectEJ.Models
{
    public class Caja
    {
        [Key]
        public int Id { get; set; }
        public double Monto { get; set; }

    }
}