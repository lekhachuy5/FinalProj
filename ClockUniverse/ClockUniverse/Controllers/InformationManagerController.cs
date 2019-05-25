using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Transactions;

namespace ClockUniverse.Controllers
{
    
    public class InformationManagerController : Controller
    {
        private CsK23T3bEntities db = new CsK23T3bEntities();

        // GET: /InformationManager/
        public ActionResult Index()
        {
            var model = db.Contacts.ToList();
            return View(model);
        }

        // GET: /InformationManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            ContactsDetail cdt = db.ContactsDetails.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tito = cdt.Title;
            ViewBag.FBR = cdt.Feedback_Detail;
            ViewBag.Date = cdt.Date;
            return View(contact);
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
        public ActionResult Create(Contact contact, string Title, string Feedback_Detail)
        {
            if (ModelState.IsValid)
                using (var scope = new TransactionScope())
                {
                    
                        contact.Status = 1;
                        db.Contacts.Add(contact);
                        db.SaveChanges();

                        var detail = new ContactsDetail();
                        detail.Title = Title;
                        detail.Feedback_Detail = Feedback_Detail;
                        detail.Feedback_ID = contact.Contact_ID;
                        detail.Date = DateTime.Now;
                        detail.Contact_ID = contact.Contact_ID;
                        db.ContactsDetails.Add(detail);
                        db.SaveChanges();

                        scope.Complete();
                        return RedirectToAction("Index", "Home");
                    
                   
                }

            return View("~/Views/Home/Contact.cshtml");
        }

        // GET: /InformationManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            ContactsDetail cdt = db.ContactsDetails.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.DS = new SelectList(
          new List<SelectListItem>
           {
                 new SelectListItem { Text = "Chưa xử lý", Value = "1"},
                 new SelectListItem { Text = "Đã xử lý", Value = "2"}
                 
          }, "Value", "Text");
            ViewBag.Tito = cdt.Title;
            ViewBag.FBR = cdt.Feedback_Detail;
            ViewBag.Date = cdt.Date;
            return View(contact);
            
        }

        // POST: /InformationManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Contact_ID,Customer_Name,Phone,Email,Address,Status")] Contact contact, int Status, string Feedback_Reply)
        {
            if (ModelState.IsValid)
            {
                contact = db.Contacts.Find(contact.Contact_ID);
                contact.Status = Status;
                
                db.Entry(contact).State = EntityState.Modified;
                ContactsDetail contd = db.ContactsDetails.Find(contact.Contact_ID);
                contd.Feedback_Reply = Feedback_Reply;
                contd.Date = DateTime.Now;
                db.Entry(contd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DS = new SelectList(
        new List<SelectListItem>
         {
                 new SelectListItem { Text = "Chưa xử lý", Value = "1"},
                 new SelectListItem { Text = "Đã xử lý", Value = "2"}

        }, "Value", "Text");
            return View(contact);
        }

        // GET: /InformationManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: /InformationManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
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
