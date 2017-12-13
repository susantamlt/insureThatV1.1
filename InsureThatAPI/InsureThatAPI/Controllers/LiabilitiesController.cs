using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using InsureThatAPI.CommonMethods;
using Newtonsoft.Json;
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;

namespace InsureThatAPI.Controllers
{
    public class LiabilitiesController : Controller
    {
        // GET: Liabilities
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LiabilityCover(int? cid)
        {
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Contains("Liability"))
                        {
                           
                        }
                        else {if (Policyincllist.Contains("Travel"))
                        {
                            return RedirectToAction("TravelCover", "Travel", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Boat"))
                        {
                            return RedirectToAction("BoatDetails", "Boat", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Motor"))
                        {
                            return RedirectToAction("VehicleDescription", "MotorCover", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Pet"))
                        {
                            return RedirectToAction("PetsCover", "Pets", new { cid = cid });
                        }
                    }
                }
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid,type=1 });
            }
            LiabilityCover LiabilityCover = new LiabilityCover();
            LiabilityCover.FarmliabiltyObj = new FarmLiabiltys();
            LiabilityCover.FarmliabiltyObj.EiId = 60691;
            LiabilityCover.LimitindemnityObj = new LimitofIndemnity();
            LiabilityCover.LimitindemnityObj.EiId = 60671;
            LiabilityCover.ExcessLCObj = new ExcessLC();
            LiabilityCover.ExcessLCObj.EiId = 60681;
            List<SelectListItem> ExcList = new List<SelectListItem>();
            ExcList.Add(new SelectListItem { Text = "$250", Value = "1" });
            LiabilityCover.ExcessLCObj.ExcessList = ExcList;
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Liability), Convert.ToInt32(PolicyType.RLS), policyid).ToList();

            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == LiabilityCover.FarmliabiltyObj.EiId))
                {
                    LiabilityCover.FarmliabiltyObj.Farmliabilty = Convert.ToString(details.Where(q => q.QuestionId == LiabilityCover.FarmliabiltyObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == LiabilityCover.LimitindemnityObj.EiId))
                {
                    LiabilityCover.LimitindemnityObj.Limitindemnity = Convert.ToString(details.Where(q => q.QuestionId == LiabilityCover.LimitindemnityObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == LiabilityCover.ExcessLCObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == LiabilityCover.ExcessLCObj.EiId).FirstOrDefault();
                    LiabilityCover.ExcessLCObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
            }
            return View(LiabilityCover);
        }
        [HttpPost]
        public ActionResult LiabilityCover(LiabilityCover LiabilityCover, int? cid)
        {
            List<SelectListItem> ExcList = new List<SelectListItem>();
            ExcList.Add(new SelectListItem { Text = "$250", Value = "1" });
            LiabilityCover.ExcessLCObj.ExcessList = ExcList;

            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (LiabilityCover.FarmliabiltyObj != null && LiabilityCover.FarmliabiltyObj.EiId > 0 && LiabilityCover.FarmliabiltyObj.Farmliabilty !=null)
                {
                    db.IT_InsertCustomerQnsData(LiabilityCover.CustomerId, Convert.ToInt32(RLSSection.Liability), LiabilityCover.FarmliabiltyObj.EiId, LiabilityCover.FarmliabiltyObj.Farmliabilty.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (LiabilityCover.LimitindemnityObj != null && LiabilityCover.LimitindemnityObj.EiId > 0 && LiabilityCover.LimitindemnityObj.Limitindemnity != null)
                {
                    db.IT_InsertCustomerQnsData(LiabilityCover.CustomerId, Convert.ToInt32(RLSSection.Liability), LiabilityCover.LimitindemnityObj.EiId, LiabilityCover.LimitindemnityObj.Limitindemnity.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (LiabilityCover.ExcessLCObj != null && LiabilityCover.ExcessLCObj.EiId > 0 && LiabilityCover.ExcessLCObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(LiabilityCover.CustomerId, Convert.ToInt32(RLSSection.Liability), LiabilityCover.ExcessLCObj.EiId, LiabilityCover.ExcessLCObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
            }
            return View(LiabilityCover);
        }
    }
    
}