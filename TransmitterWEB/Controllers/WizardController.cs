﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TransmitterWEB.Controllers
{
    public class WizardController : _baseController
    {
        // GET: Wizard
        public ActionResult Index()
        {
            return View();
        }
       
    }
}