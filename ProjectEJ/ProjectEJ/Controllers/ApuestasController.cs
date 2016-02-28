using Microsoft.AspNet.Identity;
using ProjectEJ.Models;
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
                db.Apuestas.Add(objApuesta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Apuestas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Apuestas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Apuestas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Apuestas/Delete/5
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
