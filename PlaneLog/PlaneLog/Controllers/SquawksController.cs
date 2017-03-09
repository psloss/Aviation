using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlaneLog.Models;


namespace PlaneLog.Controllers
{
    public class SquawksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Squawks access squawks/?planeId=1
        public ActionResult Index(int? planeId)
        {
            var squawks = db.Squawks.Include(f => f.Plane);
            if (planeId.HasValue && planeId > 0)
            {
                squawks = squawks.Where(x => x.PlaneId == 1);
            }

            squawks = squawks.OrderBy(x => x.DateRecorded);
            var tailNumbers = db.Planes.ToDictionary(x => x.Id, x => x.TailNumber.ToUpper()).OrderBy(x => x.Value).ToList();
            tailNumbers.Insert(0, (new KeyValuePair<int, string>(1, "N562D")));
            ViewBag.TailNumbers = tailNumbers;
            return View(squawks.ToList());
        }

        // GET: Squawks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Squawk squawk = db.Squawks.Find(id);
            if (squawk == null)
            {
                return HttpNotFound();
            }
            return View(squawk);
        }

        // GET: Squawks/Create
        public ActionResult Create()
        {
            ViewBag.PlaneId = new SelectList(db.Planes, "Id", "TailNumber");
            var squawk = new Squawk();
            squawk.PlaneId = 1;
            squawk.DateRecorded = DateTime.Now;
            var latestSquawk = db.Squawks.Where(x => x.PlaneId == squawk.PlaneId).OrderByDescending(x => x.DateRecorded).FirstOrDefault();
            if (latestSquawk != null)
            { }
            return View(squawk);
        }

        // POST: Squawks/Create THIS SECTION IS COMPLETED
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaneId,Issue,DateRecorded,Resolved,DateResolved,WhoResolved,CostToResolve,ContactPhone,ContactName")] Squawk squawk)
        {
            if (ModelState.IsValid)
            {
                db.Squawks.Add(squawk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaneId = new SelectList(db.Planes, "Id", "TailNumber", squawk.PlaneId);
            return View(squawk);
        }

        // GET: Squawks/Edit/5 SECTION DONE
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Squawk squawk = db.Squawks.Find(id);
            if (squawk == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaneId = new SelectList(db.Planes, "Id", "TailNumber", squawk.PlaneId);
            return View(squawk);
        }

        // POST: Squawks/Edit/5 Section done
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PlaneId,Issue,DateRecorded,Resolved,DateResolved,WhoResolved,CostToResolve,ContactPhone,ContactName")] Squawk squawk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(squawk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaneId = new SelectList(db.Planes, "Id", "TailNumber", squawk.PlaneId);
            return View(squawk);
        }

        // GET: Squawks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Squawk squawk = db.Squawks.Find(id);
            if (squawk == null)
            {
                return HttpNotFound();
            }
            return View(squawk);
        }

        // POST: Squawks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Squawk squawk = db.Squawks.Find(id);
            db.Squawks.Remove(squawk);
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
