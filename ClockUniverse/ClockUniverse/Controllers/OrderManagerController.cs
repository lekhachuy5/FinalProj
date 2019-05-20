﻿using System;
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
        public ActionResult Index()
        {
            var model = db.Order_Detail.ToList();
            return View(model);

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
        public ActionResult Edit(int id1, int id2)
        {

            Order_Detail order = db.Order_Detail.Find(id1, id2);
            Order od = db.Orders.Find(id1);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.DS = new SelectList(
            new List<SelectListItem>
             {
                 new SelectListItem { Text = "Đang xử lý", Value = "1"},
                 new SelectListItem { Text = "Đã tiếp nhận", Value = "2"},
                 new SelectListItem { Text = "Đang vận chuyển", Value = "3"},
                 new SelectListItem { Text = "Đã giao hàng", Value = "4"}
            }, "Value", "Text", od.Deliver_Status);
            ViewBag.WatchT_ID = new SelectList(db.ProductTables, "Watch_ID", "Watch_Name", order.Watch_ID);
            return View(order);

        }

        // POST: /OrderManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order_Detail order, int Deliver_Status, int Amount)
        {


            if (ModelState.IsValid)
                using (var scope = new TransactionScope())
                {
                    order = db.Order_Detail.Find(order.Order_ID, order.Watch_ID);
                    var product = db.ProductTables.Find(order.Watch_ID);
                    var od = db.Orders.Find(order.Order_ID);
                    if (Amount <= 0)
                    {
                        ModelState.AddModelError("Amount", Resource1.AmountLess0);
                    }
                    else
                    {


                        
                        if (Amount >( product.InStock + order.Amount))
                        {
                            ModelState.AddModelError("Amount", Resource1.OverInStock);
                        }
                        else
                        {
                            product.InStock = product.InStock + order.Amount - Amount;
                            db.Entry(product).State = EntityState.Modified;
                            order.Amount = Amount;
                            order.Price = order.Amount * order.ProductTable.Selling_Price;
                            db.Entry(order).State = EntityState.Modified;


                            
                            string datetime = DateTime.Now.ToShortDateString();
                            od.Order_ChangeDate = Convert.ToDateTime(datetime);
                            od.Deliver_Status = Deliver_Status;
                            db.Entry(od).State = EntityState.Modified;
                            db.SaveChanges();


                            scope.Complete();
                            return RedirectToAction("Index");
                        }

                       
                    }
                }
            ViewBag.DS = new SelectList(
            new List<SelectListItem>
             {
                 new SelectListItem { Text = "Đang xử lý", Value = "1"},
                 new SelectListItem { Text = "Đã tiếp nhận", Value = "2"},
                 new SelectListItem { Text = "Đang vận chuyển", Value = "3"},
                 new SelectListItem { Text = "Đã giao hàng", Value = "4"}
            }, "Value", "Text");

            return View(order);
        }
        private void ValidateClock(Order_Detail model)
        {
            if (model.Amount <= 0)
                ModelState.AddModelError("Amount", Resource1.AmountLess0);
        }

        // GET: /OrderManager/Delete/5
        public ActionResult Delete(int? id1, int? id2)
        {
            if (id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order od = db.Orders.Find(id1);
            Order_Detail order = db.Order_Detail.Find(id1, id2);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /OrderManager/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id1, int id2)
        {
            Order order = db.Orders.Find(id1);
            Order_Detail od = db.Order_Detail.Find(id1, id2);
            db.Order_Detail.Remove(od);
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
