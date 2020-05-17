using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using slnGrandesExposiciones.Models;


namespace slnGrandesExposiciones.Controllers
{
    public class ParametrosController : Controller {
        private GrandesExposicionesEntities db = new GrandesExposicionesEntities();

        // GET: Parametros
        public ActionResult Index() {
            var periodos = db.Periodos.ToList();

            return View(db.Parametros_Exposiciones.ToList());
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
                        var oTabla = new Parametros_Exposiciones();
                        oTabla.TIER_1 = model.TIER_1;
                        oTabla.TIER_2 = model.TIER_2;
                        oTabla.TIER_3 = model.TIER_3;
                        oTabla.SMVM = model.SMVM;

                        db.Parametros_Exposiciones.Add(oTabla);
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
                var oTabla = db.Parametros_Exposiciones.Find(Id);
                model.ID_PERIODO = oTabla.ID_PERIODO;
                model.TIER_1 = oTabla.TIER_1;
                model.TIER_2 = oTabla.TIER_2;
                model.TIER_3 = oTabla.TIER_3;
                model.SMVM = oTabla.SMVM;
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
