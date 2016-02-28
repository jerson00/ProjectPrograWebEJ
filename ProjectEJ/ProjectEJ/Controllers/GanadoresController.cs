using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectEJ.Models;

namespace ProjectEJ.Controllers
{
    public class GanadoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
            ViewBag.Sorteos = db.Sorteos.Where(s => s.Is_Active == true && s.Is_Finished == false && s.Fecha_Expiracion < DateTime.Now);
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
                Ganadores.updateSorteo(db, sorteo.Id);
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
    }
}
