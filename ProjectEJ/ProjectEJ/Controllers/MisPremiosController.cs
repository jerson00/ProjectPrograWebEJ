using Microsoft.AspNet.Identity;
using ProjectEJ.Models;
using ProjectEJ.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectEJ.Controllers
{
    public class MisPremiosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        // GET: MisPremios
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(Request["sorteos"]))
            {
                string userId = User.Identity.GetUserId();
                var sorteo_id = Convert.ToInt32(Request["sorteos"].ToString());
                ViewBag.Sorteo = db.Sorteos.Where(s => s.Id == sorteo_id).First();
                ViewBag.Ganadores = db.Ganadores.Where(g => g.Sorteos.Id == sorteo_id).First();
                ViewBag.ListaApostados = getNumerosApostados(userId, sorteo_id);
            }

            ViewBag.Sorteos = db.Sorteos.Where(s => s.Is_Finished == true);

            return View();
        }

        public List<NumerosApostados> getNumerosApostados(string userId, int sorteo_id)
        {
            var Apuestas = db.Apuestas.Where(a => a.Usuario_Id == userId && a.Sorteos.Id == sorteo_id).ToList();
            List<NumerosApostados> listNumerosApostados = new List<NumerosApostados>();
            foreach (var apuesta in Apuestas)
            {
                var is_ganador = false;
                var posicion = 0;
                double premio = 0;
                if (ViewBag.Ganadores.PrimerNumero == apuesta.Numero)
                {
                    is_ganador = true;
                    posicion = 1;
                    premio = Convert.ToDouble(apuesta.Monto) * 60;
                }
                else if (ViewBag.Ganadores.SegundoNumero == apuesta.Numero)
                {
                    is_ganador = true;
                    posicion = 2;
                    premio = Convert.ToDouble(apuesta.Monto) * 10;
                }
                else if (ViewBag.Ganadores.TercerNumero == apuesta.Numero)
                {
                    is_ganador = true;
                    posicion = 3;
                    premio = Convert.ToDouble(apuesta.Monto) * 5;
                }

                var objNumeros = new NumerosApostados
                {
                    Numero = apuesta.Numero,
                    Monto = apuesta.Monto,
                    Ganador = is_ganador,
                    Puesto = posicion,
                    Premio = premio
                };

                listNumerosApostados.Add(objNumeros);
            }

            return listNumerosApostados;
        }
    }
}