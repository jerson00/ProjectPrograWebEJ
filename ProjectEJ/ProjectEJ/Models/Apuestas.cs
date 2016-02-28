﻿using ProjectEJ.Models.Entidades;
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

        public static List<ApuestasQuery> getApuestasByNum(ApplicationDbContext db)
        {
            var apuestas = (from a in db.Apuestas
                                       join s in db.Sorteos on a.Sorteos.Id equals s.Id
                                       where s.Is_Active == true && s.Is_Finished == false && s.Fecha_Expiracion > DateTime.Now
                                       group a by a.Numero into g
                                        select new ApuestasQuery
                                        {
                                            Numero = g.Key,
                                            Monto = g.Sum(a => a.Monto)
                                        }).OrderByDescending(o => o.Monto).ToList();
            return apuestas;
        }

        public static double getTotalPeorCaso(List<ApuestasQuery> listaApuestas)
        {
            double Total = 0;
            Total += Convert.ToInt32(listaApuestas[0].Monto) * 60;
            Total += Convert.ToInt32(listaApuestas[1].Monto) * 10;
            Total += Convert.ToInt32(listaApuestas[2].Monto) * 5;
            return Total;
        }

        public static List<ApuestasQuery> getVitualList(List<ApuestasQuery> listaApuestas, int numero, double monto)
        {
            for (int i = 0; i < listaApuestas.Count(); i++)
            {
                if (listaApuestas[i].Numero == numero)
                {
                    listaApuestas[i].Monto += monto;
                }
            }

            listaApuestas.OrderByDescending(x => x.Monto);
            return listaApuestas;
        }
    }
}