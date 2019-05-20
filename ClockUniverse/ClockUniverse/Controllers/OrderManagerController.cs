using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Caching;
using ClockUniverse;

namespace ClockUniverse.Controllers
{
    public class OrderManagerController : Controller
    {
        private CsK23T3bEntities db = new CsK23T3bEntities();
        
        // GET: /OrderManager/
        public ActionResult Index(string id)
        {
            var model = db.Order_Detail.ToList();
            
            var od = from o in db.Order_Detail select o;
            if (!String.IsNullOrEmpty(id))
            {
                var strI = Convert.ToInt32(id.Trim());
                od = db.Order_Detail.Where(o => o.Order_ID == strI);
            }
            ViewBag.SearchTerm = id;
            return View(od.ToList());
        }

        // GET: /OrderManager/Details/5
        public ActionResult Details(int? id1, int? id2)
        {
            if (id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Detail order = db.Order_Detail.Find(id1, id2);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public ActionResult Create2()
        {
            return View();
        }
        // GET: /OrderManager/Create
        public ActionResult Create()
        {
            ViewBag.Watch_ID = new SelectList(db.ProductTables, "Watch_ID", "Watch_Name");
            return View();
        }

        // POST: /OrderManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( int id)
        {
            Order_Detail order = new Order_Detail();
            if (ModelState.IsValid)
            {  
               
                order.Order_ID = id;
                db.Order_Detail.Add(order);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            ViewBag.Watch_ID = new SelectList(db.ProductTables, "Watch_ID", "Watch_Name", order.Watch_ID.ToString());
            return View(order);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(Order od)
        {
            if (ModelState.IsValid)
            {
                od.Order_Date = DateTime.Now;
                od.Delivery_Date = DateTime.Now;
                db.Orders.Add(od);
                db.SaveChanges();
                return RedirectToAction("Create", new {id = od.Order_ID});
            }
            return View(od);
        }

        // GET: /OrderManager/Edit/5
        public ActionResult Edit(int? id1, int? id2)
        {
            if (id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Detail order = db.Order_Detail.Find(id1, id2);
            
            if (order == null ) 
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /OrderManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Order_ID,Order_Date,Delivery_Date,Customer_Name,Customer_Phone,Customer_Email,Deliver_Address,Deliver_Status,Order_ChangeDate,Total_Price")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: /OrderManager/Delete/5
        public ActionResult Delete(int? id1, int? id2)
        {
            if (id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Detail order = db.Order_Detail.Find(id1, id2);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /OrderManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
