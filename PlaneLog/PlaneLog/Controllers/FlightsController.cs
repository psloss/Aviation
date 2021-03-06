﻿using System;
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
    public class FlightsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Flights - Access flights/?planeId=1
        public ActionResult Index(int? planeId)

        {
            var flights = db.Flights.Include(f => f.Plane);
            if (planeId.HasValue && planeId > 0)
            {
                flights = flights.Where(x => x.PlaneId == 1);
            }

            flights = flights.OrderByDescending(x => x.HobbsOut);
            var tailNumbers = db.Planes.ToDictionary(x => x.Id, x => x.TailNumber.ToUpper()).OrderBy(x => x.Value).ToList();
            tailNumbers.Insert(0, (new KeyValuePair<int, string>(1, "N562D")));
            ViewBag.TailNumbers = tailNumbers;
            return View(flights.ToList());
        }

        // GET: Report
        public ActionResult Report(int? planeId)
        {
            var flights = db.Flights.Include(f => f.Plane);//.OrderByDescending(x => x.FlightDate);
            if (planeId.HasValue)
            {
                flights = flights.Where(x => x.PlaneId == 1);// planeId changed to 1
            }

            flights = flights.OrderByDescending(x => x.FlightDate);

            ViewBag.TailNumbers = db.Planes.ToDictionary(x => x.Id, x => x.TailNumber).OrderBy(x => x.Value);
            return View(flights.ToList());
        }

        // GET: Flights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Include(f => f.Plane).FirstOrDefault(x => x.Id == id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // GET: Flights/Create
        public ActionResult Create()
        {
            ViewBag.PlaneId = new SelectList(db.Planes, "Id", "TailNumber");
            var flight = new Flight();
            flight.PlaneId = 1;
            flight.FlightDate = DateTime.Now;
            var latestFlight = db.Flights.Where(x => x.PlaneId == flight.PlaneId).OrderByDescending(x => x.HobbsIn).FirstOrDefault();
            if (latestFlight != null)
            {
                flight.HobbsOut = latestFlight.HobbsIn;
                flight.FuelOut = latestFlight.FuelIn;
            }
            return View(flight);
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaneId,FlightDate,HobbsOut,HobbsIn,FuelOut,FuelIn,FuelPurchased," +
            "FuelCostGallon,FuelCostTotal,AddedOil,OilDipstick,OilChange,FlightHours,Remarks,FuelUseHour")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Flights.Add(flight);
                db.SaveChanges();
                UpdatePlaneEngineHours(flight.PlaneId);
                UpdateEngineOilHours(flight.PlaneId);

                return RedirectToAction("Index");
            }

            ViewBag.PlaneId = new SelectList(db.Planes, "Id", "TailNumber", flight.PlaneId);
            return View(flight);
        }

        // GET: Flights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaneId = new SelectList(db.Planes, "Id", "TailNumber", flight.PlaneId);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PlaneId,FlightDate,HobbsOut,HobbsIn,FuelOut,FuelIn,FuelPurchased," +
            "FuelCostGallon,FuelCostTotal,AddedOil,OilDipstick,OilChange,FlightHours,Remarks,FuelUseHour")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                UpdatePlaneEngineHours(flight.PlaneId);
                UpdateEngineOilHours(flight.PlaneId);
                return RedirectToAction("Index");
            }
            ViewBag.PlaneId = new SelectList(db.Planes, "Id", "TailNumber", flight.PlaneId);
            return View(flight);
        }

        public void UpdatePlaneEngineHours(int planeId)
        {
            var plane = db.Planes.Include(x => x.Flights)
                .FirstOrDefault(x => x.Id == planeId);
            if (null == plane || plane.Flights.Count == 0)
                return;
            var latestFlight = plane.Flights.OrderByDescending(x => x.HobbsOut).First(); // changed x.FlightDate to x.HobbsOut
            plane.EngineHours = latestFlight.HobbsIn;
            db.SaveChanges();
                    }

        //public void UpdateFuelIn(int planeId)
        //{
        //    var plane = db.Planes.Include(x => x.Flights)
        //        .FirstOrDefault(x => x.Id == planeId);
        //    if (null == plane || plane.Flights.Count == 0)
        //        return;
        //    var latestFlight = plane.Flights.OrderByDescending(x => x.HobbsOut).First();
        //    plane.Fuel = latestFlight.FuelIn;
        //   // db.SaveChanges();
        //}

        public void UpdateFuelCostTotal(decimal FuelP, decimal FuelC)
        {
            var FuelCostTotal = FuelP * FuelC;
            return; // Fuel

        }

        public void UpdateEngineOilHours(int planeId)
        {
            // find last instance of an oil change and store hours in OilTime
            var plane = db.Planes.Include(x => x.Flights)
                .FirstOrDefault(x => x.Id == planeId);
            if (null == plane || plane.Flights.Count == 0)
                return;
            //var latestOilChange = plane.Flights.Where(x => x.OilChange == true).OrderByDescending(x => x.HobbsOut).FirstOrDefault();
            //plane.LastOilChangeHours = latestOilChange.HobbsOut;

            plane.UpdateLastOilChangeHours();
            db.SaveChanges();

        }

        public void SaveOilAdded(int planeId)
        {
            var plane = db.Planes.Include(x => x.Flights).FirstOrDefault(x => x.Id == planeId);
            var lastOilChange = plane.LastOilChangeHours;

            var flights = db.Flights.Where(x => x.PlaneId == planeId
            && x.HobbsOut > lastOilChange
            && x.AddedOil == true);

            var numberOfOilAdded = flights.Count();
        }

       
        // GET: Flights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
            db.SaveChanges();
            UpdatePlaneEngineHours(flight.PlaneId);
            UpdateEngineOilHours(flight.PlaneId);
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
