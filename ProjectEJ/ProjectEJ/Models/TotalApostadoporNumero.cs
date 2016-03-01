using ProjectEJ.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectEJ.Models
{
    public class TotalApostadoporNumero
    {
        public static List<ApuestasQuery> getApuestasByNum(ApplicationDbContext db, int sorteoId)
        {
            var apuestas = (from a in db.Apuestas
                            join s in db.Sorteos on a.Sorteos.Id equals s.Id
                            where s.Id == a.Id
                            group a by a.Numero into g
                            select new ApuestasQuery
                            {
                                Numero = g.Key,
                                Monto = g.Sum(a => a.Monto)
                            }).OrderByDescending(o => o.Monto).ToList();
            return apuestas;
        }
    }
}
