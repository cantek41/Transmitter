﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TransmitterWEB.Controllers
{
    public class HomeController : _baseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "DashBoard";
            var a = User.Identity.Name;
            return View();
        }
    }
}
