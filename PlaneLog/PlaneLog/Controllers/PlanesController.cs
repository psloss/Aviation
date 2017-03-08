using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlaneLog.Models;
using System.Text;


namespace PlaneLog.Controllers
{
    public class PlanesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Planes
        public ActionResult Index()
        {
            return View(db.Planes.ToList());


        }

        private Plane GetDefaultPlane()
        {
            Plane plane = db.Planes.Include(x => x.Flights).FirstOrDefault(x => x.Id == 1);
            return plane;
        }

        private Flight GetLastFlight(int planeId)
        {
            Flight f = db.Flights.Where(x => x.PlaneId == planeId).OrderByDescending(x => x.HobbsOut).FirstOrDefault();
            return f;
        }

        // GET: Planes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plane plane = db.Planes.Include(x => x.Flights).FirstOrDefault(x => x.Id == id);
            if (plane == null)
            {
                return HttpNotFound();
            }
            ViewBag.OilStatus = "normal";
            //TryParse
            if (Decimal.Parse(plane.OilHours) > 50)
            {
                ViewBag.OilStatus = "danger";
            }
            else if (Decimal.Parse(plane.OilHours) > 40)
            {
                ViewBag.OilStatus = "alert";
            }

            //ViewBag.OilAddedTable = CalcOilAdded(plane.Id);
            var oilChanges =  CalcOilAdded(plane.Id);


            ViewBag.OilAdded = oilChanges[plane.LastOilChangeHours.Value];

            ViewBag.LastFlight = GetLastFlight(plane.Id);
            return View(plane);
        }

        public Dictionary<decimal, int> CalcOilAdded(int planeId)
        {
            Dictionary<decimal, int> result = new Dictionary<decimal, int>();

            var oilChanges = db.Flights
                .Where(x => x.PlaneId == planeId && x.OilChange == true)
                .OrderByDescending(x => x.HobbsOut)
                .Select(x => x.HobbsOut).ToList();

            var upperLimit = Decimal.MaxValue;

            for (int i = 0; i < oilChanges.Count(); i++)
            {
                var oilChange = oilChanges[i];
                if (oilChange == null) oilChange = 0;

                var flights = db.Flights.Where(x => x.PlaneId == planeId && x.HobbsOut > oilChange && x.HobbsOut < upperLimit && x.AddedOil == true);

                result.Add(oilChange.Value, flights.Count());
                upperLimit = oilChange.Value;
            }

            var flightsBeforeFirstOilChange = db.Flights.Where(x => x.PlaneId == planeId && x.HobbsOut < upperLimit && x.AddedOil == true);
            result.Add(0, flightsBeforeFirstOilChange.Count());

            //StringBuilder returnValue = new StringBuilder();
            //foreach (var k in result.Keys)
            //{
            //    returnValue.AppendFormat("[{0}: {1}] ", k, result[k]);
            //    returnValue.AppendLine();
            //}
            //return returnValue.ToString();

            return result;
        }
        // GET: Planes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Planes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TailNumber,Manufacturer,Model,SerialNumber,YearManufactured,EngineManufacturer," +
            "AnnualDueDate,HoursLastAnnual,LastOilChangeHours,EngineHours,RegistrationExpirationDate")] Plane plane)
        {
            if (ModelState.IsValid)
            {
                db.Planes.Add(plane);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(plane);
        }

        // GET: Planes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plane plane = db.Planes.Find(id);
            if (plane == null)
            {
                return HttpNotFound();
            }
            return View(plane);
        }

        // POST: Planes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TailNumber,Manufacturer,Model,SerialNumber,YearManufactured,EngineManufacturer," +
            "AnnualDueDate,HoursLastAnnual,LastOilChangeHours,EngineHours,RegistrationExpirationDate")] Plane plane)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plane).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plane);
        }

        // GET: Planes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plane plane = db.Planes.Find(id);
            if (plane == null)
            {
                return HttpNotFound();
            }
            return View(plane);
        }

        // POST: Planes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plane plane = db.Planes.Find(id);
            db.Planes.Remove(plane);
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
