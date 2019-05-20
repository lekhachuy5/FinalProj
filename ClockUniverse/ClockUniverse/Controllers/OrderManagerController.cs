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
using System.Transactions;
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

       
        // GET: /OrderManager/Create
       

        // POST: /OrderManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        // GET: /OrderManager/Edit/5
        public ActionResult Edit(int? id1, int? id2)
        {
            if (id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Detail order = db.Order_Detail.Find(id1, id2);
            Order od = db.Orders.Find(id1);
            if (order == null)
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
        public ActionResult Edit(Order_Detail order)
        {
            if (ModelState.IsValid)
            {
                using(var scope = new TransactionScope())
                {
                    order.Price = order.Amount * order.ProductTable.Selling_Price;
                    db.Entry(order).State = EntityState.Modified;
                    
                    db.SaveChanges();
                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }
           
            return View(order);
        }

        // GET: /OrderManager/Delete/5
        public ActionResult Delete(int? id1,int? id2)
        {
            if (id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order od = db.Orders.Find(id1);
            Order_Detail order = db.Order_Detail.Find(id1,id2);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /OrderManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id1 , int id2)
        {
            Order order = db.Orders.Find(id1);
            Order_Detail od = db.Order_Detail.Find(id1, id2);
            db.Orders.Remove(order);
            db.Order_Detail.Remove(od);
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
