using System;
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
using System.IO;

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

        public ActionResult Nuevo()
        {

            return View();
        }

        [HttpPost]

        public ActionResult Nuevo(Archivos_Exposiciones archivoModel , HttpPostedFileBase Foto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (GrandesExposicionesEntities db = new GrandesExposicionesEntities())
                    {
                        var periodos = db.Periodos;
                        var archivoExposicion = new Archivos_Exposiciones();
                        var exposicion = new Exposiciones();

                        DateTime hoy = DateTime.Now;


                        byte[] imgBinaryData = new byte[Foto.ContentLength];
                        int readresult = Foto.InputStream.Read(imgBinaryData, 0, Foto.ContentLength);
                        //archivoExposicion.ID_PERIODO = archivoModel.ID_PERIODO; //ARREGLAR

                        var periodo = periodos.Where(P => P.PERIODO == archivoModel.ID_PERIODO.ToString()).Select(Per => Per.ID).ToList();

                        archivoExposicion.ID_PERIODO = periodo[0];
                        archivoExposicion.TIPOTABLA = 1;
                        archivoExposicion.NOMBRE = archivoModel.NOMBRE;
                        archivoExposicion.CONTENIDO = imgBinaryData;

                        exposicion.ID_PERIODO = 1;
                        exposicion.FECHA_ALTA = hoy;
                        exposicion.FECHA_MOD = hoy;
                        exposicion.LEGAJO = "L0693677";          //Arreglar
                        exposicion.VALIDADO = false;





                        db.Archivos_Exposiciones.Add(archivoExposicion);
                        db.Exposiciones.Add(exposicion);




                        db.SaveChanges();
                    }

                    return Redirect("~/Exposiciones/Index/");
                }

                return Redirect("~/Exposiciones/Index/");


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}