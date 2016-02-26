using PrograWebProjectEJ.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PrograWebProjectEJ.Controllers
{
    public class CajaController : Controller

    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Caja
        public ActionResult Index()
        {
            return View();
        }

        // GET: Caja/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Caja/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Caja/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Monto")] Caja caja)
        {
            if (ModelState.IsValid)
            {
                db.Caja.Add(caja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(caja);
        }

        // GET: Caja/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caja caja = db.Caja.Find(id);
            if (caja == null)
            {
                return HttpNotFound();
            }
            return View(caja);
        }

        // POST: Caja/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Monto")] Caja caja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(caja);
        }
    

        // GET: Caja/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Caja/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
