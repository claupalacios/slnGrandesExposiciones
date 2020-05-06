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

        // GET: Parametros/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Parametros_Exposiciones parametros = db.Parametros_Exposiciones.Find(id);
            if (parametros == null) {
                return HttpNotFound();
            }

            return View(parametros);
        }

        // GET: Parametros/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Parametros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PERIODO,TIR_1,TIR_2,TIR_3,SMVM")] Parametros_Exposiciones parametros) {
            if (ModelState.IsValid) {
                db.Parametros_Exposiciones.Add(parametros);
                db.Entry(db.Parametros_Exposiciones.Last()).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parametros);
        }

        public ActionResult Configurar()
        {
            List<Parametros_Exposiciones> parametros = db.Parametros_Exposiciones.ToList();
          
            return View("Configurar",parametros);
        }

        // POST: Parametros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Configurar([Bind(Include = "ID,PERIODO,TIR_1,TIR_2,TIR_3,SMVM")] Parametros_Exposiciones parametros) {
            if (ModelState.IsValid) {
                db.Entry(parametros).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToRoute(new {
                    controller = "Exposiciones",
                    action = "Detalles"
                });
            }
            return View(parametros);
        }

        // GET: Parametros/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Parametros_Exposiciones parametros = db.Parametros_Exposiciones.Find(id);
            if (parametros == null) {
                return HttpNotFound();
            }
            return View(parametros);
        }

        // POST: Parametros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Parametros_Exposiciones parametros = db.Parametros_Exposiciones.Find(id);
            db.Parametros_Exposiciones.Remove(parametros);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
