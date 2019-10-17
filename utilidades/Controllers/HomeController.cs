using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using utilidades.Servicios.Interfaces;

namespace utilidades.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var lista=ServiceManager<ProcedimientoSvc>.Provider.ListaProcedimiento();
            return View(lista);
        }
    }
}