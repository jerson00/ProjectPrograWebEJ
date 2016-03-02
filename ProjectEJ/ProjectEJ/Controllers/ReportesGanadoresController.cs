using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectEJ.Models;
using ProjectEJ.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectEJ.Controllers
{
    public class ReportesGanadoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        // GET: ReportesGanadores
        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (!currentUser.Is_Admin)
            {
                return Redirect("Apuestas");
            }
            if (!string.IsNullOrEmpty(Request["sorteos"]))
            {
                var sorteo_id = Convert.ToInt32(Request["sorteos"].ToString());
                ViewBag.Sorteo = db.Sorteos.Where(s => s.Id == sorteo_id).First();
                ViewBag.Ganadores = db.Ganadores.Where(g => g.Sorteos.Id == sorteo_id).First();
                ViewBag.ListaGanadores = getGanadores(sorteo_id);
            }
            ViewBag.Sorteos = db.Sorteos.Where(s => s.Is_Finished == true);

            return View();

        }

        public List<NumerosApostados> getGanadores(int sorteo_id)
        {
            var users = db.Users.ToList();
            var Apuestas = db.Apuestas.Where(a => a.Sorteos.Id == sorteo_id).ToList();
            var Ganadores = db.Ganadores.Where(g => g.Sorteos.Id == sorteo_id).First();
            List<NumerosApostados> listGanadores = new List<NumerosApostados>();
            foreach (var apuesta in Apuestas)
            {
                var is_ganador = false;
                var posicion = 0;
                double premio = 0;
                string email = "";
                if (Ganadores.PrimerNumero == apuesta.Numero)
                {
                    is_ganador = true;
                    posicion = 1;
                    premio = Convert.ToDouble(apuesta.Monto) * 60;
                }
                else if (Ganadores.SegundoNumero == apuesta.Numero)
                {
                    is_ganador = true;
                    posicion = 2;
                    premio = Convert.ToDouble(apuesta.Monto) * 10;
                }
                else if (Ganadores.TercerNumero == apuesta.Numero)
                {
                    is_ganador = true;
                    posicion = 3;
                    premio = Convert.ToDouble(apuesta.Monto) * 5;
                }

                foreach (var usuario in users)
                {
                    if (usuario.Id == apuesta.Usuario_Id)
                    {
                        email = usuario.Email;
                    }
                }

                if (is_ganador)
                {
                    var objNumeros = new NumerosApostados
                    {
                        Numero = apuesta.Numero,
                        Monto = apuesta.Monto,
                        Ganador = is_ganador,
                        Puesto = posicion,
                        Premio = premio,
                        Email = email
                    };

                    listGanadores.Add(objNumeros);
                }
                
            }

            return listGanadores;
        }
    }

}
