﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaneLog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Plane and Flight logging program";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Planes()
        {
            ViewBag.Message = "Plane";
            return View();
        }

        public ActionResult Reports()
        {
            ViewBag.Message = "Reports";
            return View();
        }
    }
}