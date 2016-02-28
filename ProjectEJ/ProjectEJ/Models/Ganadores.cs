using ProjectEJ.Models.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectEJ.Models
{
    public class Ganadores
    {
        [Key]
        public int Id { get; set; }
        public Sorteos Sorteos { get; set; }
        public int PrimerNumero { get; set; }
        public int SegundoNumero { get; set; }
        public int TercerNumero { get; set; }

        public static void updateSorteo(ApplicationDbContext db, int id)
        {
            var sorteos = db.Sorteos.Find(id);
            sorteos.Is_Finished = true;
            sorteos.Is_Active = false;
            db.SaveChanges();

        }

        public static List<GanadoresQuery> getGanadores(ApplicationDbContext db)
        {
            List<GanadoresQuery> ganadores = (from g in db.Ganadores
                            join s in db.Sorteos on g.Sorteos.Id equals s.Id
                            select new GanadoresQuery
                            {
                                Id = g.Id,
                                PrimerNumero = g.PrimerNumero,
                                SegundoNumero = g.SegundoNumero,
                                TercerNumero = g.TercerNumero,
                                Descripcion = s.Descripcion,
                                Fecha_Expiracion = s.Fecha_Expiracion
                            }).ToList();

            return ganadores;

        }

    }
}