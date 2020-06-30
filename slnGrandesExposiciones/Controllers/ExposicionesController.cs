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

        public ActionResult Index()
        {
            try
            {
                using (GrandesExposicionesEntities db = new GrandesExposicionesEntities())
                {
                    Log.Information("ExposicionesController - Obteniendo Exposiciones");

                    var tablaPeriodos = db.Periodos.ToList();
                    var tablaExposiciones = db.Exposiciones.ToList();

                    var query =
                        (from e in tablaExposiciones
                         join p in tablaPeriodos on e.ID_PERIODO equals p.ID
                         select new ExposicionesDto()
                         {
                             Id = e.ID,
                             ID_PERIODO = p.PERIODO,
                             FECHA_ALTA = e.FECHA_ALTA,
                             FECHA_MOD = e.FECHA_MOD,
                             Legajo = "L0693677",
                             Validado = e.VALIDADO ? "Validado" : "Sin Validar"
                         }).ToList();


                    return View(query);

                }
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

        public ActionResult Nuevo(Archivos_Exposiciones archivoModel, HttpPostedFileBase ExposicionExcel)
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


                        byte[] imgBinaryData = new byte[ExposicionExcel.ContentLength];
                        int readresult = ExposicionExcel.InputStream.Read(imgBinaryData, 0, ExposicionExcel.ContentLength);
                        //archivoExposicion.ID_PERIODO = archivoModel.ID_PERIODO; //ARREGLAR

                        var periodo = periodos.Where(P => P.PERIODO == archivoModel.ID_PERIODO).Select(Per => Per.ID).ToList();

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


        public ActionResult Validar(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (GrandesExposicionesEntities db = new GrandesExposicionesEntities())
                    {
                        var tablaExposiciones = db.Exposiciones;

                        var exposicion = tablaExposiciones.Where(Exp => Exp.ID == id).FirstOrDefault();

                        exposicion.VALIDADO = true;
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


        public FileContentResult Descargar(int id)
        {
            try
            {
                using (GrandesExposicionesEntities db = new GrandesExposicionesEntities())
                {
                    var tablaExposiciones = db.Exposiciones;
                    var tablaArchivos = db.Archivos_Exposiciones;
                    var tablaPeriodos = db.Periodos;

                    var idPeriodo = tablaPeriodos.Where(Per => Per.PERIODO == id).Select(Peri => Peri.ID).FirstOrDefault();

                    var archivo = tablaArchivos.Where(Exp => Exp.ID_PERIODO == idPeriodo).Select(Expo => Expo.CONTENIDO).FirstOrDefault();

                    
                    return File(archivo, System.Net.Mime.MediaTypeNames.Application.Octet, "Archivo.xlsx");


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}