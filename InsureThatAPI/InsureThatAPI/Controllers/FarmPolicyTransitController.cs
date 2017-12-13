using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;//append model
using InsureThatAPI.CommonMethods;//append Common Methods
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;

namespace InsureThatAPI.Controllers
{
    public class FarmPolicyTransitController : Controller
    {
        // GET: FarmPolicyTransit
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Transit(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> TransitExcessToPay = new List<SelectListItem>();
            TransitExcessToPay = commonModel.excessRate();


            FPTransit FPTransit = new FPTransit();
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {

                    if (Policyincllist.Contains("Transit"))
                    {

                    }
                    else
                    {
                        if (Policyincllist.Contains("ValuablesFarm"))
                        {
                              return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("LiveStockFarm"))
                        {
                            return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("PersonalLiabilitiesFarm"))
                        {
                            return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeBuildingFarm"))
                        {
                            return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeContent"))
                        {
                            return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Machinery"))
                        {
                            //  return RedirectToAction("", "", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("MotorFarm"))
                        {
                            // return RedirectToAction("", "", new { cid = cid });
                        }
                    }
                }
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 2 });
            }
            ViewBag.cid = cid;
            if (cid != null)
            {
                FPTransit.CustomerId = cid.Value;
            }
            FPTransit.LivestockMaxValOneLoadObj = new LivestockMaximumValOfOneload();
            FPTransit.LivestockMaxValOneLoadObj.EiId = 63249;

            FPTransit.FarmProduceMaxValOneLoadObj = new FarmProduceMaxValOfOneLoad();
            FPTransit.FarmProduceMaxValOneLoadObj.EiId = 63253;

            FPTransit.ExcessFPTransitObj = new ExcessFPTransit();
            FPTransit.ExcessFPTransitObj.ExcessList = TransitExcessToPay;
            FPTransit.ExcessFPTransitObj.EiId = 63257;

            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(FarmPolicySection.Transit), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == FPTransit.LivestockMaxValOneLoadObj.EiId))
                {
                    FPTransit.LivestockMaxValOneLoadObj.livestockMaxValoneload = Convert.ToString(details.Where(q => q.QuestionId == FPTransit.LivestockMaxValOneLoadObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPTransit.FarmProduceMaxValOneLoadObj.EiId))
                {
                    FPTransit.FarmProduceMaxValOneLoadObj.farmproduceMaxValoneload = Convert.ToString(details.Where(q => q.QuestionId == FPTransit.FarmProduceMaxValOneLoadObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPTransit.ExcessFPTransitObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPTransit.ExcessFPTransitObj.EiId).FirstOrDefault();
                    FPTransit.ExcessFPTransitObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

            }
            return View(FPTransit);
        }

        [HttpPost]
        public ActionResult Transit(int? cid, FPTransit FPTransit)
        {

            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> TransitExcessToPay = new List<SelectListItem>();
            TransitExcessToPay = commonModel.excessRate();
            FPTransit.ExcessFPTransitObj.ExcessList = TransitExcessToPay;
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (FPTransit.LivestockMaxValOneLoadObj != null && FPTransit.LivestockMaxValOneLoadObj.EiId > 0 && FPTransit.LivestockMaxValOneLoadObj.livestockMaxValoneload != null)
                {
                    db.IT_InsertCustomerQnsData(FPTransit.CustomerId, Convert.ToInt32(FarmPolicySection.Transit), FPTransit.LivestockMaxValOneLoadObj.EiId, FPTransit.LivestockMaxValOneLoadObj.livestockMaxValoneload.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPTransit.FarmProduceMaxValOneLoadObj != null && FPTransit.FarmProduceMaxValOneLoadObj.EiId > 0 && FPTransit.FarmProduceMaxValOneLoadObj.farmproduceMaxValoneload != null)
                {
                    db.IT_InsertCustomerQnsData(FPTransit.CustomerId, Convert.ToInt32(FarmPolicySection.Transit), FPTransit.LivestockMaxValOneLoadObj.EiId, FPTransit.LivestockMaxValOneLoadObj.livestockMaxValoneload.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPTransit.ExcessFPTransitObj != null && FPTransit.ExcessFPTransitObj.EiId > 0 && FPTransit.ExcessFPTransitObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FPTransit.CustomerId, Convert.ToInt32(FarmPolicySection.Transit), FPTransit.ExcessFPTransitObj.EiId, FPTransit.ExcessFPTransitObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

            }
            return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid });
        }
    }
}