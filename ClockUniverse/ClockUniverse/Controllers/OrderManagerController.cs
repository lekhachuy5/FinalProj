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
    public class OrderManagerController : Controller
    {
        private ClockUniverseEntities db = new ClockUniverseEntities();

        // GET: /OrderManager/
        public ActionResult Index()
        {
            var model = db.QLdonHangs.ToList();
            return View(model);
        }

        // GET: /OrderManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QLdonHang qldonhang = db.QLdonHangs.Find(id);
            if (qldonhang == null)
            {
                return HttpNotFound();
            }
            return View(qldonhang);
        }

        // GET: /OrderManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /OrderManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,TenDH,SoLuong,DonGia,TongTien,HinhAnh")] QLdonHang qldonhang)
        {
            if (ModelState.IsValid)
            {
                db.QLdonHangs.Add(qldonhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(qldonhang);
        }

        // GET: /OrderManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QLdonHang qldonhang = db.QLdonHangs.Find(id);
            if (qldonhang == null)
            {
                return HttpNotFound();
            }
            return View(qldonhang);
        }

        // POST: /OrderManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,TenDH,SoLuong,DonGia,TongTien,HinhAnh")] QLdonHang qldonhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qldonhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qldonhang);
        }

        // GET: /OrderManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QLdonHang qldonhang = db.QLdonHangs.Find(id);
            if (qldonhang == null)
            {
                return HttpNotFound();
            }
            return View(qldonhang);
        }

        // POST: /OrderManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QLdonHang qldonhang = db.QLdonHangs.Find(id);
            db.QLdonHangs.Remove(qldonhang);
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
