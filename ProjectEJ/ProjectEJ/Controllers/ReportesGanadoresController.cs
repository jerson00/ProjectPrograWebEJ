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
            if (!string.IsNullOrEmpty(Request["sorteos"]))
            {
                var sorteo_id = Convert.ToInt32(Request["sorteos"].ToString());
                ViewBag.Sorteo = db.Sorteos.Where(s => s.Id == sorteo_id).First();
            }
            ViewBag.Sorteos = db.Sorteos.Where(s => s.Is_Finished == true);

            return View();

        }
    }

}
