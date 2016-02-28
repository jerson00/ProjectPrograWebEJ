using ProjectEJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectEJ.Controllers
{
    public class ReportesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        // GET: Reporte
        public ActionResult Index()
        {
            var montoCaja = 0;
            var cajas = db.Caja.ToList();
            foreach (var caja in cajas)
            {
                montoCaja = Convert.ToInt32(caja.Monto);
            }

            ViewBag.MontoCaja = montoCaja;
            return View();
        }

    }
}