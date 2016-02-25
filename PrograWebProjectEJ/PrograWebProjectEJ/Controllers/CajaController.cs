using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrograWebProjectEJ.Controllers
{
    public class CajaController : Controller
    {
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

        // GET: Caja/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Caja/Edit/5
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
