using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectEJ.Models;
using System.Web.Helpers;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using ProjectEJ.Models.Entidades;

namespace ProjectEJ.Controllers
{
    public class GanadoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]

        // GET: Ganadores
        public ActionResult Index()
        {
            ViewBag.Ganadores = Ganadores.getGanadores(db);
            return View();
        }

        // GET: Ganadores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ganadores ganadores = db.Ganadores.Find(id);
            if (ganadores == null)
            {
                return HttpNotFound();
            }
            return View(ganadores);
        }

        // GET: Ganadores/Create
        public ActionResult Create()
        {
            ViewBag.Sorteos = db.Sorteos.Where(s => s.Is_Active == true && s.Is_Finished == false && s.Fecha_Expiracion <= DateTime.Now);
            return View();
        }

        // POST: Ganadores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var primero = Convert.ToInt32(collection["PrimerNumero"].ToString());
                var segundo = Convert.ToInt32(collection["SegundoNumero"].ToString());
                var tercero = Convert.ToInt32(collection["TercerNumero"].ToString());

                if (primero != segundo && segundo != tercero && primero != tercero)
                {
                    var data = collection["Sorteos"].ToString();
                    var sorteo = db.Sorteos.Find(Convert.ToInt32(collection["Sorteos"].ToString()));
                    var objGanadores = new Ganadores
                    {
                        PrimerNumero = Convert.ToInt32(collection["PrimerNumero"].ToString()),
                        SegundoNumero = Convert.ToInt32(collection["SegundoNumero"].ToString()),
                        TercerNumero = Convert.ToInt32(collection["TercerNumero"].ToString()),
                        Sorteos = sorteo
                    };
                    db.Ganadores.Add(objGanadores);
                    db.SaveChanges();
                    notificarGanadores(sorteo.Id, objGanadores.PrimerNumero, 1);
                    notificarGanadores(sorteo.Id, objGanadores.SegundoNumero, 2);
                    notificarGanadores(sorteo.Id, objGanadores.TercerNumero, 3);
                    Ganadores.updateSorteo(db, sorteo.Id);
                    TempData["Success"] = "Ganadores registrados correctamente.";
                    return RedirectToAction("Index");
                }

                TempData["Error"] = "Los números ganadores no pueden ser iguales";
                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");
        }

        // GET: Ganadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ganadores ganadores = db.Ganadores.Find(id);
            if (ganadores == null)
            {
                return HttpNotFound();
            }
            return View(ganadores);
        }

        // POST: Ganadores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PrimerNumero,SegundoNumero,TercerNumero")] Ganadores ganadores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ganadores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ganadores);
        }

        // GET: Ganadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ganadores ganadores = db.Ganadores.Find(id);
            if (ganadores == null)
            {
                return HttpNotFound();
            }
            return View(ganadores);
        }

        // POST: Ganadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ganadores ganadores = db.Ganadores.Find(id);
            db.Ganadores.Remove(ganadores);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void notificarGanadores(int sorteo, int numero, int posicion)
        {
            List<NotificarGanadores> listaGanadores = new List<NotificarGanadores>();
            var users = db.Users.ToList();
            var apuestasGanadores = db.Apuestas.Where(a => a.Sorteos.Id == sorteo && a.Numero == numero);
            foreach (var apuestas in apuestasGanadores)
            {
                double premio = 0;
                var email = "";

                foreach (var usuario in users)
                {
                    if (usuario.Id == apuestas.Usuario_Id)
                    {
                        email = usuario.Email;
                    }
                }

                if (posicion == 1)
                {
                    premio = apuestas.Monto * 60;
                    var objGanador = new NotificarGanadores
                    {
                        Email = email,
                        Numero = numero,
                        Posicion = posicion,
                        Premio = premio,
                        Sorteo_Id = sorteo
                    };
                    listaGanadores.Add(objGanador);
                }
                if (posicion == 2)
                {
                    premio = apuestas.Monto * 10;
                    var objGanador = new NotificarGanadores
                    {
                        Email = email,
                        Numero = numero,
                        Posicion = posicion,
                        Premio = premio,
                        Sorteo_Id = sorteo
                    };
                    listaGanadores.Add(objGanador);
                }
                if (posicion == 3)
                {
                    premio = apuestas.Monto * 5;
                    var objGanador = new NotificarGanadores
                    {
                        Email = email,
                        Numero = numero,
                        Posicion = posicion,
                        Premio = premio,
                        Sorteo_Id = sorteo
                    };
                    listaGanadores.Add(objGanador);
                }           
            }

            DescontarPremios(listaGanadores);
            EnviarNotificaciones(db, listaGanadores);
        }

        public void EnviarNotificaciones(ApplicationDbContext db, List<NotificarGanadores> listaNotificarGanadores)
        {
            try
            {
                foreach (var notificarGanador in listaNotificarGanadores)
                {
                    var sorteo = db.Sorteos.Where(s => s.Id == notificarGanador.Sorteo_Id).First();
                    string subject = "Notificación de números favorecidos en los tiempos";
                    string message = "Usted es uno de los ganadores del sorteo: " + sorteo.Descripcion + " - " + sorteo.Fecha_Expiracion + ", su número favorecido es el: " + notificarGanador.Numero + " en la " + notificarGanador.Posicion + "° posición y su premio es de: " + notificarGanador.Premio;
                    WebMail.SmtpServer = "smtp.gmail.com";
                    WebMail.SmtpPort = 587;
                    WebMail.EnableSsl = true;
                    WebMail.UserName = "jersonarroyo24@gmail.com";
                    WebMail.From = "jersonarroyo24@gmail.com";
                    WebMail.Password = "stvnas23";
                    WebMail.Send(to: notificarGanador.Email, subject: subject, body: message);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            
        }


        public void DescontarPremios(List<NotificarGanadores> listaNotificarGanadores)
        {
            double descuento = 0;
            int cajaId = 0;
            double montoCaja = 0;
            foreach (var apuesta in listaNotificarGanadores)
            {
                descuento += apuesta.Premio;
            }
            
            var cajas = db.Caja.ToList();
            foreach (var caja in cajas)
            {
                cajaId = caja.Id;
                montoCaja = caja.Monto;
            }

            var cajaEdit = db.Caja.Find(cajaId);
            cajaEdit.Monto = montoCaja - descuento;
            db.SaveChanges();
        }

    }
}
