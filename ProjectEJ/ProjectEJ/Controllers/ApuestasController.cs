using Microsoft.AspNet.Identity;
using ProjectEJ.Models;
using ProjectEJ.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectEJ.Controllers
{
    public class ApuestasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        // GET: Apuestas
        public ActionResult Index()
        {      
            ViewBag.Sorteos = db.Sorteos.Where(s => s.Is_Active == true && s.Is_Finished == false && s.Fecha_Expiracion > DateTime.Now);
            return View();
        }

        // GET: Apuestas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Apuestas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Apuestas/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                var sorteo = db.Sorteos.Find(Convert.ToInt32(collection["sorteo"].ToString()));
                var objApuesta = new Apuestas
                {
                    Numero = Convert.ToInt32(collection["numero"].ToString()),
                    Monto = Convert.ToInt32(collection["monto"].ToString()),
                    Sorteos = sorteo,
                    Usuario_Id = userId
                };

                var apostar = validarApuesta(db, objApuesta.Numero, objApuesta.Monto, sorteo, userId);
                if (apostar == null)
                {
                    db.Apuestas.Add(objApuesta);
                    db.SaveChanges();
                    var ApuestaResponse = new ApuestaResponse
                    {
                        Status = "1",
                        MontoSugerido = "0",
                        Monto = objApuesta.Monto.ToString(),
                        Numero = objApuesta.Numero.ToString()
                    };

                    return Json(ApuestaResponse, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var ApuestaResponse = new ApuestaResponse
                    {
                        Status = "0",
                        MontoSugerido = apostar,
                        Monto = "",
                        Numero = ""
                    };

                    return Json(ApuestaResponse, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception e)
            {
                return View();
            }
        }


       
        // GET: Apuestas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        public string validarApuesta(ApplicationDbContext db, int numero, double monto, Sorteos sorteo, string userId)
        {
            var listApuestas = Apuestas.getApuestasByNum(db);
            double total = Apuestas.getTotalPeorCaso(listApuestas);
            var listVirtual = Apuestas.getVitualList(listApuestas, numero, monto, sorteo, userId);
            double totalVirtual = Apuestas.getTotalPeorCaso(listVirtual);
            double montoCaja = 0;
            int cajaId = 0;
            var cajas = db.Caja.ToList();
            foreach (var caja in cajas)
            {
                cajaId = caja.Id;
                montoCaja = Convert.ToDouble(caja.Monto);
            }

            montoCaja += monto;
            
            if (totalVirtual <= montoCaja)
            {
                var caja = db.Caja.Find(cajaId);
                caja.Monto = montoCaja;
                db.SaveChanges();
                return null;
            }
            else
            {
                double apuestaSugerida = getApuestaSugerida(numero, montoCaja, total, listApuestas);
                return apuestaSugerida.ToString();
            }
        }

        public double getApuestaSugerida(double numero, double montoCaja, double totalApuestas, List<ApuestasQuery> listaApuestas)
        {
            var exists = false;
            double montoSugerido = 0;
            int posicion = 0;
            foreach (var apuesta in listaApuestas)
            {
                if (apuesta.Numero == numero)
                {
                    if (posicion == 0)
                    {
                        exists = true;
                        montoSugerido = (montoCaja - totalApuestas) / 60;
                        break;
                    } else if (posicion == 1)
                    {
                        exists = true;
                        montoSugerido = (montoCaja - totalApuestas) / 10;
                        break;
                    } else if (posicion == 2)
                    {
                        exists = true;
                        montoSugerido = (montoCaja - totalApuestas) / 5;
                        break;
                    }
                }

                posicion++;
            }

            if (!exists)
            {
                exists = true;
                montoSugerido = (montoCaja - totalApuestas) / 60;
            }

            var monto = Math.Round(montoSugerido, 2);
            return monto;
        }
    }
}
