using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Serilog;
using slnGrandesExposiciones.Dtos;
using slnGrandesExposiciones.Models;


namespace slnGrandesExposiciones.Controllers
{
    public class ParametrosController : Controller {
        private GrandesExposicionesEntities db = new GrandesExposicionesEntities();

        // GET: Parametros
        public ActionResult Index() {

            using (GrandesExposicionesEntities db = new GrandesExposicionesEntities())
            {
                Log.Information("ParametrosController - Obteniendo Parametros Exposiciones");

                var tablaPeriodos = db.Periodos.ToList();
                var tablaParametros = db.Parametros_Exposiciones.ToList();

                var query =
                    (from pa in tablaParametros
                     join pe in tablaPeriodos on pa.ID_PERIODO equals pe.ID
                     select new ParametrosDto()
                     {
                         Id = pa.ID,
                         ID_PERIODO = pe.PERIODO,
                         TIER_1 = pa.TIER_1,
                         TIER_2 = pa.TIER_2,
                         TIER_3 = pa.TIER_3,
                         SMVM = pa.SMVM
                     }).ToList();


                return View(query);

            }
        }

        public ActionResult Nuevo()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(Parametros_Exposiciones model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (GrandesExposicionesEntities db = new GrandesExposicionesEntities())
                    {
                        var tablaPeriodos = db.Periodos;

                        var idPeriodo = tablaPeriodos.Where(Per => Per.PERIODO == model.ID_PERIODO).Select(Per => Per.ID).ToList();
              
                        
                        var exposicionNuevo = new Parametros_Exposiciones();

                        exposicionNuevo.ID_PERIODO = idPeriodo[0];
                        exposicionNuevo.TIER_1 = model.TIER_1;
                        exposicionNuevo.TIER_2 = model.TIER_2;
                        exposicionNuevo.TIER_3 = model.TIER_3;
                        exposicionNuevo.SMVM = model.SMVM;

                        db.Parametros_Exposiciones.Add(exposicionNuevo);
                        db.SaveChanges();
                    }

                    return Redirect("~/Parametros/Index/");
                }

                return View(model);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Editar(int Id)
        {
            Parametros_Exposiciones model = new Parametros_Exposiciones();
            using (GrandesExposicionesEntities db = new GrandesExposicionesEntities())
            {
                var tablaPeriodos = db.Periodos.ToList();
                var parametroAEditar = db.Parametros_Exposiciones.Find(Id);
                var periodo = tablaPeriodos.Where(P => P.ID == parametroAEditar.ID_PERIODO).Select(Per => Per.PERIODO).ToList();


                model.ID_PERIODO = periodo[0];
                model.TIER_1 = parametroAEditar.TIER_1;
                model.TIER_2 = parametroAEditar.TIER_2;
                model.TIER_3 = parametroAEditar.TIER_3;
                model.SMVM = parametroAEditar.SMVM;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(Parametros_Exposiciones model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (GrandesExposicionesEntities db = new GrandesExposicionesEntities())
                    {
                        var oTabla = db.Parametros_Exposiciones.Find(model.ID);
                        oTabla.TIER_1 = model.TIER_1;
                        oTabla.TIER_2 = model.TIER_2;
                        oTabla.TIER_3 = model.TIER_3;
                        oTabla.SMVM = model.SMVM;

                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return Redirect("~/Parametros/Index/");
                }

                return View(model);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public void Eliminar(int Id)
        {
            try
            {
                    using (GrandesExposicionesEntities db = new GrandesExposicionesEntities())
            {

                var elemento = db.Parametros_Exposiciones.Find(Id);
                db.Parametros_Exposiciones.Remove(elemento);
                db.SaveChanges();
            }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
