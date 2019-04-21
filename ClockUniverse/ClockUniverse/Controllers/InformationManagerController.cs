using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClockUniverse;

namespace ClockUniverse.Controllers
{
    public class InformationManagerController : Controller
    {
        private ClockUniverseEntities db = new ClockUniverseEntities();

        // GET: /InformationManager/
        public ActionResult Index()
        {
            return View(db.QLTTs.ToList());
        }

        // GET: /InformationManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QLTT qltt = db.QLTTs.Find(id);
            if (qltt == null)
            {
                return HttpNotFound();
            }
            return View(qltt);
        }

        // GET: /InformationManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /InformationManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,FullName,SDT,DiaChi")] QLTT qltt)
        {
            if (ModelState.IsValid)
            {
                db.QLTTs.Add(qltt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(qltt);
        }

        // GET: /InformationManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QLTT qltt = db.QLTTs.Find(id);
            if (qltt == null)
            {
                return HttpNotFound();
            }
            return View(qltt);
        }

        // POST: /InformationManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,FullName,SDT,DiaChi")] QLTT qltt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qltt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qltt);
        }

        // GET: /InformationManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QLTT qltt = db.QLTTs.Find(id);
            if (qltt == null)
            {
                return HttpNotFound();
            }
            return View(qltt);
        }

        // POST: /InformationManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QLTT qltt = db.QLTTs.Find(id);
            db.QLTTs.Remove(qltt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
