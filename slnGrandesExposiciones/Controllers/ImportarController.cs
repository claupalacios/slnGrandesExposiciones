using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Mvc;
using slnGrandesExposiciones.Models;
using slnGrandesExposiciones.Helpers;

namespace slnGrandesExposiciones.Controllers
{
    public class ImportarController : Controller {
        private GrandesExposicionesEntities db = new GrandesExposicionesEntities();
        //private List<Formulas> lista_formulas = null;
        //private List<Campos> lista_campos = null;

        [HttpGet]
        public ActionResult Archivo() {
            return View();
        }
        
        [HttpPost]
        public ActionResult Archivo(object txt_no) {
            string periodo = AltaExposiciones();
            
            return RedirectToRoute(new {
                controller = "Exposiciones",
                action = "Detalles"
            });
        }

        private void InicializarObjDBSet()
        {
            FormulaHelper.LISTA_FORMULAS = db.Formulas.ToList();
            FormulaHelper.LISTA_CAMPOS = db.Campos.ToList();
        }

        private string AltaExposiciones()
        {
            string periodo = "";

            if (Request.Files.Count > 0)
            {
                if (Request.Files[0] != null)
                {
                    var txt = Request.Files[0];

                    //try
                    //{
                        if (txt != null && txt.ContentLength > 0)
                        {
                            InicializarObjDBSet();

                            StreamReader sr = new StreamReader(txt.InputStream);
                            //Exposiciones exp_formulas = CargarFormulas();
                            
                            while (!sr.EndOfStream)
                            {
                                string linea = sr.ReadLine();
                            }

                            periodo = db.Exposiciones.Local.Last().ID_PERIODO.ToString();

                            sr.Close();
                            db.SaveChanges();
                        }
                        else
                        {
                            //ViewBag.Message = "No se cargó un archivo.";
                        }
                    //}
                    //catch (Exception ex) { }
                }
            }

            return periodo;
        }

        private Exposiciones CargarFormulas()
        {
            Exposiciones exp_formulas = new Exposiciones();

            if (FormulaHelper.LISTA_FORMULAS != null)
            {
                IEnumerable<System.Reflection.PropertyInfo> propiedades = exp_formulas.GetType().GetProperties().Where(p => p.Name.IndexOf("FORMULA_") > -1);
                
                foreach(Formulas f in FormulaHelper.LISTA_FORMULAS)
                {
                    foreach(var p in propiedades)
                    {
                        if(f.NOMBRE == p.Name)
                        {
                            p.SetValue(exp_formulas, (double?)f.ID);
                            break;
                        }
                    }
                }
            }

            return exp_formulas;
        }

        [HttpPost]
        public object getFechaMMYYYY()
        {
            object fecha_mmyyyy = null;

            var txt = Request.Files[0];

            try
            {
                if (txt != null && txt.ContentLength > 0)
                {
                    StreamReader sr = new StreamReader(txt.InputStream);

                    string linea = sr.ReadLine();

                    if (linea.Substring(0, 6).All(c => Char.IsNumber(c)))
                    {
                        fecha_mmyyyy = new
                        {
                            FECHA_MMYYYY = linea.Substring(0, 6),
                            NOMBRE_ARCHIVO = new FileInfo(txt.FileName).Name
                        };
                    }
                    else
                    {
                        fecha_mmyyyy = new
                        {
                            FECHA_MMYYYY = "",
                            NOMBRE_ARCHIVO = "EL ARCHIVO NO ES VÁLIDO"
                        };
                    }
                    
                    sr.Close();
                }
            }
            catch (Exception ex) { }

            var jsonRet = Json(fecha_mmyyyy);
            jsonRet.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return jsonRet;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
