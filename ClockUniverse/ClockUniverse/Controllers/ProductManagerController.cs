using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClockUniverse;
using System.Transactions;
namespace ClockUniverse.Controllers
{
    public class ProductManagerController : Controller
    {
        private ClockUniverseEntities db = new ClockUniverseEntities();

        // GET: /ProductManager/
        public ActionResult Index()
        {
            return View(db.QuanLyDHs.ToList());
        }

        // GET: /ProductManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanLyDH quanlydh = db.QuanLyDHs.Find(id);
            if (quanlydh == null)
            {
                return HttpNotFound();
            }
            return View(quanlydh);
        }

        // GET: /ProductManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ProductManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuanLyDH model)
        {
            ValidateClock(model);
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {

                    // add model to database
                    db.QuanLyDHs.Add(model);
                    db.SaveChanges();
                    // save file to app_data
                    var path = Server.MapPath("~/App_Data");
                    path = System.IO.Path.Combine(path, model.ID.ToString());
                    Request.Files["Image"].SaveAs(path);
                    // all done successfully
                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }

            return View("Create", model);
        }

        // GET: /ProductManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanLyDH quanlydh = db.QuanLyDHs.Find(id);
            if (quanlydh == null)
            {
                return HttpNotFound();
            }
            return View(quanlydh);
        }

        // POST: /ProductManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,TenDH,LoaiDH,ThongTinDH,HinhAnhDH,GiaTien,SoLuong")] QuanLyDH quanlydh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quanlydh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quanlydh);
        }

        public ActionResult Image(string id)
        {
            var path = Server.MapPath("~/App_Data");
            path = System.IO.Path.Combine(path, id);
            return File(path, "image/jpg/*");
        }
        private void ValidateClock(QuanLyDH model)
        {
            if (model.GiaTien <= 0)
                ModelState.AddModelError("GiaTien", Resource1.priceLess0);
        }


        // GET: /ProductManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanLyDH quanlydh = db.QuanLyDHs.Find(id);
            if (quanlydh == null)
            {
                return HttpNotFound();
            }
            return View(quanlydh);
        }

        // POST: /ProductManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuanLyDH quanlydh = db.QuanLyDHs.Find(id);
            db.QuanLyDHs.Remove(quanlydh);
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
