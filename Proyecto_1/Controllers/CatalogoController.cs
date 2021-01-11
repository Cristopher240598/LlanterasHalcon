using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_1.Models;

namespace Proyecto_1.Controllers
{
    public class CatalogoController : Controller
    {
        private contextLlantera db = new contextLlantera();
        
        // GET: Catalogo
        public ActionResult Index()
        {           
            return View();
        } 
    }
}