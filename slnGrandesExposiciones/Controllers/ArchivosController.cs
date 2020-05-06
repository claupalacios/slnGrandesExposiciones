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

        // GET: Parametros
        public ActionResult ExpIndividual()
        {

            //var listaAlias = new[] {db.Campos
            // .Where(c => c.ID_TABLA == 3)
            // .Select(c => new
            // {
            //     Alias = c.ALIAS
            // }).ToList()};

            //var listaValores = new[] {db.Valor_Campos
            // .Where(c => c.ID_PERIODO == 3)
            // .Select(c => new
            // {
            //     Alias = c.VALOR
            // }).ToList()};

            //return View(listaAlias);

            var valores = new TablaValoresDto();

            var valoresDataBase =  db.Valor_Campos.ToList();
            var camposDataBase =  db.Campos.ToList();


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
                list.Add(new TablaValoresDto { Titulo = elemento.Titulo , Valor = elemento.Valor });

            }
            


            return View("ExpIndividual", list);
        }

     
        public IEnumerable<Valor_Campos> GetValoresExpInd()
        {

            IEnumerable<Valor_Campos> valores = db.Valor_Campos;

            return valores;
        }


        public IEnumerable<Valor_Campos> GetValoresGrupoInd()
        {

            IEnumerable<Valor_Campos> valores = db.Valor_Campos;

            return valores;
        }


    }
}
