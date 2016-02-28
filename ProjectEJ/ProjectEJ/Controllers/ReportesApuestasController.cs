using ProjectEJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectEJ.Controllers
{
    public class ReportesApuestasController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        // GET: ReporteApuestas
        public ActionResult Index()
        {
            return View();
        }
    }
}