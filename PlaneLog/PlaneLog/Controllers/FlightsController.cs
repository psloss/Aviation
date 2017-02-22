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
                flights = flights.Where(x => x.PlaneId == planeId);
            }

            flights = flights.OrderByDescending(x => x.FlightDate);
            var tailNumbers = db.Planes.ToDictionary(x => x.Id, x => x.TailNumber.ToUpper()).OrderBy(x => x.Value).ToList();
            tailNumbers.Insert(0,(new KeyValuePair<int, string>(0, "All")));
            ViewBag.TailNumbers = tailNumbers;
            return View(flights.ToList());
        }

        // GET: Report
        public ActionResult Report(int? planeId)
        {
            var flights = db.Flights.Include(f => f.Plane);//.OrderByDescending(x => x.FlightDate);
            if (planeId.HasValue)
            {
                flights = flights.Where(x => x.PlaneId == planeId);
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
            Flight flight = db.Flights.Find(id);
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
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PlaneId,FlightDate,HobbsOut,HobbsIn,FuelOut,FuelIn,FuelPurchased," +
            "FuelCostGallon,FuelCostTotal,AddedOil,OilChange,Remarks")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Flights.Add(flight);
                db.SaveChanges();
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
            "FuelCostGallon,FuelCostTotal,AddedOil,OilChange,Remarks")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;

                //flight.Plane.EngineHours = flight.HobbsIn;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaneId = new SelectList(db.Planes, "Id", "TailNumber", flight.PlaneId);
            return View(flight);
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
