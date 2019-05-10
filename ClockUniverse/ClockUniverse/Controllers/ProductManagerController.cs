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
        private CsK23T3bEntities db = new CsK23T3bEntities();

        // GET: /ProductManager/
        public ActionResult Index(string searchTerm)
        {
            var model = db.ProductTables.ToList();
            var pro = from o in db.ProductTables select o;
            if (String.IsNullOrEmpty(searchTerm))
            {

                return HttpNotFound();
        }
            else
            {
                pro = db.ProductTables.Where(o => o.Watch_ID.ToString().Contains(searchTerm));
            }
            ViewBag.SearchTerm = searchTerm;
            return View(pro.ToList());
        }

        // GET: /ProductManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTable producttable = db.ProductTables.Find(id);
            if (producttable == null)
            {
                return HttpNotFound();
            }
            return View(producttable);
        }

        // GET: /ProductManager/Create
        public ActionResult Create()
        {
            ViewBag.WatchType_ID = new SelectList(db.ProductTypes, "ProductType_ID", "ProductType_Name");
            return View();
        }

        // POST: /ProductManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Watch_ID,Watch_Name,Watch_Description,WatchType_ID,Original_Price,Selling_Price,InStock")] ProductTable model)
        {
           
            if (ModelState.IsValid)
            {
                db.ProductTables.Add(model);
                db.SaveChanges();
                 
            }
            

            ViewBag.WatchType_ID = new SelectList(db.ProductTypes, "ProductType_ID", "ProductType_Name", model.WatchType_ID.ToString());
            return View(model);
        }

        // GET: /ProductManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTable producttable = db.ProductTables.Find(id);
            if (producttable == null)
            {
                return HttpNotFound();
            }
            ViewBag.WatchType_ID = new SelectList(db.ProductTypes, "ProductType_ID", "ProductType_Name", producttable.WatchType_ID);
            return View(producttable);
        }

        // POST: /ProductManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Watch_ID,Watch_Name,Watch_Description,WatchType_ID,Original_Price,Selling_Price,InStock")] ProductTable producttable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producttable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WatchType_ID = new SelectList(db.ProductTypes, "ProductType_ID", "ProductType_Name", producttable.WatchType_ID);
            return View(producttable);
        }

        public ActionResult Image(string id)
        {
            var path = Server.MapPath("~/App_Data");
            path = System.IO.Path.Combine(path, id);
            return File(path, "image/jpg/*");
        }

        // GET: /ProductManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTable producttable = db.ProductTables.Find(id);
            if (producttable == null)
            {
                return HttpNotFound();
            }
            return View(producttable);
        }

        // POST: /ProductManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductTable producttable = db.ProductTables.Find(id);
            db.ProductTables.Remove(producttable);
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
