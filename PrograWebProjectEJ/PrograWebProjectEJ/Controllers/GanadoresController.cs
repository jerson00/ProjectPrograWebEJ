using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrograWebProjectEJ.Controllers
{
    public class GanadoresController : Controller
    {
        // GET: Ganadores
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ganadores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ganadores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ganadores/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ganadores/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ganadores/Edit/5
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

        // GET: Ganadores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ganadores/Delete/5
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
