using InsureThatAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Controllers
{
    public class FloodController : Controller
    {
        // GET: Flood
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FloodArea(int? cid, int? type, int? insureId)
        {
            Floodarea model = new Floodarea();
            if(Session["ApiKey"]!=null)
            {

            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            model.CustomerId = cid;
            model.policyType = type;
            model.insureId = insureId;
            model.FloodArea = -1;
            model.FldDefault = -1;
            return View(model);
        }
        [HttpPost]
        public ActionResult FloodArea(Floodarea model)
        {
            if (model != null)
            {        
                
                if (model.FloodArea == 0)
                {
                    return RedirectToAction("CustomerRegistration", "Customer", new { cid = model.CustomerId ,type=model.policyType});
                }
                else if (model.FloodArea == 1)
                {
                    ViewBag.error = "Sorry we do not offer flood cover at this time. Click No to continue without flood cover.";
                }
                
            }
            return View(model);
        }
    }
}