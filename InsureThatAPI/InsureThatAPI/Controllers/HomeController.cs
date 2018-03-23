﻿using InsureThatAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MasterDataEntities db = new MasterDataEntities();

            ViewBag.Title = "Home Page";

            return RedirectToAction("AgentLogin","Login");
        }
        public ActionResult Index1()
        {
           ViewBag.Title = "Home Page";
            return View();
        }
    }
}
