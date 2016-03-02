using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (!currentUser.Is_Admin)
            {
                return Redirect("Apuestas");
            }
            var listApuestas = Apuestas.getApuestasByNum(db);
            double montoPremios = Apuestas.getTotalPeorCaso(listApuestas);
            
            var montoCaja = 0;
            var cajas = db.Caja.ToList();
            foreach (var caja in cajas)
            {
                montoCaja = Convert.ToInt32(caja.Monto);
            }
            double montoGananciaMinima = montoCaja - montoPremios;
            ViewBag.MontoPremiosPeorCaso = montoPremios;
            ViewBag.MontoCaja = montoCaja;
            ViewBag.MontoGananciaMinima = montoGananciaMinima;
            ViewBag.MontoGananciaMaxima = montoCaja;
            return View();
        }



      

    }
}