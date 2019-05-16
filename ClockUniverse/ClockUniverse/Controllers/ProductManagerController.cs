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

        public ActionResult Index(string id)
        {
            
            var pro = from o in db.ProductTables select o;
           

            if (!string.IsNullOrEmpty(id))
            {
               
                var strI = Convert.ToInt32(id.Trim());
                pro = db.ProductTables.Where(o => o.Watch_ID == strI);
            }

            ViewBag.SearchTerm = id;
            return View(pro);
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
        public ActionResult Create([Bind(Include = "Watch_ID,Watch_Name,Watch_Description,Watch_Static,WatchType_ID,Original_Price,Selling_Price,InStock")] ProductTable producttable)
        {

            ValidateClock(producttable);
            
            
                if (ModelState.IsValid)
                {
                    using (var scope = new TransactionScope())
                    {

                        // add model to database
                        db.ProductTables.Add(producttable);
                        db.SaveChanges();
                        // save file to app_data
                        var path = Server.MapPath("~/App_Data");
                        path = System.IO.Path.Combine(path, producttable.Watch_ID.ToString());
                        Request.Files["Image"].SaveAs(path + "_0");
                        Request.Files["Image1"].SaveAs(path + "_1");
                        Request.Files["Image2"].SaveAs(path + "_2");
                        // all done successfully
                        scope.Complete();
                        return RedirectToAction("Index");
                    }
                }
            
          


                //ViewBag.WatchType_ID = new SelectList(db.ProductTypes, "ProductType_ID", "ProductType_Name", model.WatchType_ID.ToString());
                return View("Create", producttable);
            
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
        public ActionResult Edit([Bind(Include = "Watch_ID,Watch_Name,Watch_Description,WatchType_ID,Original_Price,Selling_Price,InStock")] ProductTable producttable)
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
            return File(path + "_0", "image/jpg/*");

        }

        public ActionResult Image1(string id)
        {
            var path = Server.MapPath("~/App_Data");
            path = System.IO.Path.Combine(path, id);
            return File(path + "_1", "image/jpg/*");

        }

        public ActionResult Image2(string id)
        {
            var path = Server.MapPath("~/App_Data");
            path = System.IO.Path.Combine(path, id);
            return File(path + "_2", "image/jpg/*");

        }

        private void ValidateClock(ProductTable model)
        {
            if (model.Original_Price <= 0 && model.Selling_Price <= 0 )
                ModelState.AddModelError("price", Resource1.priceLess0);
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
