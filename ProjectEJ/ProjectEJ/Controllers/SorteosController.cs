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
    public class SorteosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]

        // GET: Sorteos
        public ActionResult Index()
        {
            return View(db.Sorteos.ToList());
        }

        // GET: Sorteos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sorteos sorteos = db.Sorteos.Find(id);
            if (sorteos == null)
            {
                return HttpNotFound();
            }
            return View(sorteos);
        }

        // GET: Sorteos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sorteos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha_Expiracion,Descripcion,Is_Active,Is_Finished")] Sorteos sorteos)
        {
            if (ModelState.IsValid)
            {
                db.Sorteos.Add(sorteos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sorteos);
        }

        // GET: Sorteos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sorteos sorteos = db.Sorteos.Find(id);
            if (sorteos == null)
            {
                return HttpNotFound();
            }
            return View(sorteos);
        }

        // POST: Sorteos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha_Expiracion,Descripcion,Is_Active,Is_Finished")] Sorteos sorteos)
        {
            if (ModelState.IsValid)
            {
                if (sorteos.Is_Finished != true)
                {
                    db.Entry(sorteos).State = EntityState.Modified;
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }
            return View(sorteos);
        }

        // GET: Sorteos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sorteos sorteos = db.Sorteos.Find(id);
            if (sorteos == null)
            {
                return HttpNotFound();
            }
            return View(sorteos);
        }

        // POST: Sorteos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sorteos sorteos = db.Sorteos.Find(id);
            db.Sorteos.Remove(sorteos);
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
