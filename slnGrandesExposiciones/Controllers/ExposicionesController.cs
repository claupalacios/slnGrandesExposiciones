﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using PagedList;
using slnGrandesExposiciones.Models;
using slnGrandesExposiciones.Contracts.Business;
using slnGrandesExposiciones.Dtos;
using Serilog;
using slnGrandesExposiciones.Business;
using slnGrandesExposiciones.Contracts.Repository;

namespace slnGrandesExposiciones.Controllers
{
    public class ExposicionesController : Controller
    {
        private GrandesExposicionesEntities db = new GrandesExposicionesEntities();
   
        public ActionResult Index()
        {
            try
            {     
            Log.Information("ExposicionesController - Obteniendo Exposiciones");
            var query = db.Exposiciones;
            return View(query);
                
            }
        
            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al obtener los Estados: ", ex);
                return null; //Escribir mensaje de error
            }
        }

        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        //public PartialViewResult IndexGrid(String name)
        //{
        //    return PartialView("_IndexGrid", db.Exposiciones.Find(name));
        //}

    }
}