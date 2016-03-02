using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectEJ.Models;
using ProjectEJ.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectEJ.Controllers
{
    public class TotalApostadoporNumeroController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        // GET: ReportesGanadores
        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (!currentUser.Is_Admin)
            {
                return Redirect("Apuestas");
            }
            if (!string.IsNullOrEmpty(Request["sorteos"]))
            {
                var sorteo_id = Convert.ToInt32(Request["sorteos"].ToString());
                ViewBag.Sorteo = db.Sorteos.Where(s => s.Id == sorteo_id).First();
                ViewBag.totalApuestas = TotalApostadoporNumero.getApuestasByNum(db, sorteo_id);
              //  ViewBag.totalApuestas = db.Apuestas.Where(a => a.Id == sorteo_id);
                
            }
            ViewBag.Sorteos = db.Sorteos.Where(s => s.Is_Active == true);

            return View();
        }
    }
}
