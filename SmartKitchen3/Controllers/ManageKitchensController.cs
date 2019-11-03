using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartKitchen3.Models;
using SmartKitchen3.Utils;

namespace SmartKitchen3.Controllers
{
    [SmartKitchenAuthorizeAttribute(AdminOnly = "true")]
    public class ManageKitchensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ManageKitchens
        public ActionResult Index()
        {
            return View(db.Kitchens.ToList());
        }

        // GET: ManageKitchens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitchen kitchen = db.Kitchens.Find(id);
            if (kitchen == null)
            {
                return HttpNotFound();
            }
            return View(kitchen);
        }

        // GET: ManageKitchens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageKitchens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KitchenId,Name,LocationLatitudes,LocationLongitude")] Kitchen kitchen)
        {
            if (ModelState.IsValid)
            {
                db.Kitchens.Add(kitchen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kitchen);
        }

        // GET: ManageKitchens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitchen kitchen = db.Kitchens.Find(id);
            if (kitchen == null)
            {
                return HttpNotFound();
            }
            return View(kitchen);
        }

        // POST: ManageKitchens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KitchenId,Name,LocationLatitudes,LocationLongitude")] Kitchen kitchen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kitchen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kitchen);
        }

        // GET: ManageKitchens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitchen kitchen = db.Kitchens.Find(id);
            if (kitchen == null)
            {
                return HttpNotFound();
            }
            return View(kitchen);
        }

        // POST: ManageKitchens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kitchen kitchen = db.Kitchens.Find(id);
            db.Kitchens.Remove(kitchen);
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
