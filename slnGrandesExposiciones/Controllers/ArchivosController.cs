using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using slnGrandesExposiciones.Models;
using slnGrandesExposiciones.Dtos;

namespace slnGrandesExposiciones.Controllers
{
    public class ArchivosController : Controller
    {
        private GrandesExposicionesEntities db = new GrandesExposicionesEntities();
        private TablaValoresDto tablaValores = new TablaValoresDto();
        
        public ActionResult ExpIndividual()
        {
            var valores = new TablaValoresDto();

            var valoresDataBase =  db.Valor_Campos.ToList();
            var camposDataBase =  db.Campos.ToList();


            var query =
                (from v in valoresDataBase
                 join c in camposDataBase on v.ID_CAMPOS equals c.ID
                 select new TablaValoresDto()
                 {
                     Titulo = c.ALIAS,
                     Valor = v.Valor
                 }).ToList();
           
            return View("ExpIndividual", query);
        }


        public ActionResult GrupoIndividual()
        {
            var valores = new TablaValoresDto();

            var valoresDataBase = db.Valor_Campos.ToList();
            var camposDataBase = db.Campos.ToList();


            var query =
                (from v in valoresDataBase
                 join c in camposDataBase on v.ID_CAMPOS equals c.ID
                 select new
                 {
                     Titulo = c.ALIAS,
                     Valor = v.Valor
                 }).ToList();

            List<TablaValoresDto> list = new List<TablaValoresDto>();

            foreach (var elemento in query)
            {
                list.Add(new TablaValoresDto { Titulo = elemento.Titulo, Valor = elemento.Valor });

            }

            return View("GrupoIndividual", list);
        }


        public ActionResult TotalGrupo()
        {
            var valores = new TablaValoresDto();

            var valoresDataBase = db.Valor_Campos.ToList();
            var camposDataBase = db.Campos.ToList();


            var query =
                (from v in valoresDataBase
                 join c in camposDataBase on v.ID_CAMPOS equals c.ID
                 select new
                 {
                     Titulo = c.ALIAS,
                     Valor = v.Valor
                 }).ToList();

            List<TablaValoresDto> list = new List<TablaValoresDto>();

            foreach (var elemento in query)
            {
                list.Add(new TablaValoresDto { Titulo = elemento.Titulo, Valor = elemento.Valor });

            }

            return View("TotalGrupo", list);
        }


    }
}
