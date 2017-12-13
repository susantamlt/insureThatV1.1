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
    public class FarmPolicyValuablesController : Controller
    {
        // GET: FarmPolicyValuables
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Valuables(int? cid)
        {
            FPValuables FPValuables = new FPValuables();
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {

                   if (Policyincllist.Contains("ValuablesFarm"))
                        {
                          
                        }
                        else {
                        if (Policyincllist.Contains("LiveStockFarm"))
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
                FPValuables.CustomerId = cid.Value;
            }
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> DescriptionSpeciItemsList = new List<SelectListItem>();
            DescriptionSpeciItemsList = commonModel.descriptionListFC();

            List<SelectListItem> ValuablesexcessToPay = new List<SelectListItem>();
            ValuablesexcessToPay = commonModel.excessRate();

            FPValuables.CoverUnspecifiedValuablesObj = new CoverForUnspecifiedValuables();
            FPValuables.CoverUnspecifiedValuablesObj.EiId = 63725;

            FPValuables.SpecifiedItemDescriptionObj = new SpecifiedItemDescription();
            FPValuables.SpecifiedItemDescriptionObj.SpecItemDescriptionList = DescriptionSpeciItemsList;
            FPValuables.SpecifiedItemDescriptionObj.EiId = 63733;

            FPValuables.SpecifiedItemSumInsuredObj = new SpecifiedItemSumInsured();
            FPValuables.SpecifiedItemSumInsuredObj.EiId = 63735;

            FPValuables.ExcessValuablesObj = new ExcessValuables();
            FPValuables.ExcessValuablesObj.ExcessList = ValuablesexcessToPay;
            FPValuables.ExcessValuablesObj.EiId = 63741;

            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.ValuablesFarm),Convert.ToInt32(PolicyType.FarmPolicy),policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == FPValuables.CoverUnspecifiedValuablesObj.EiId))
                {
                    FPValuables.CoverUnspecifiedValuablesObj.CoverUnspecifiedValuables = Convert.ToString(details.Where(q => q.QuestionId == FPValuables.CoverUnspecifiedValuablesObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPValuables.SpecifiedItemDescriptionObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPValuables.SpecifiedItemDescriptionObj.EiId).FirstOrDefault();
                    FPValuables.SpecifiedItemDescriptionObj.ItemDescription = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPValuables.SpecifiedItemSumInsuredObj.EiId))
                {
                    FPValuables.SpecifiedItemSumInsuredObj.ItemSumInsured= Convert.ToString(details.Where(q => q.QuestionId == FPValuables.SpecifiedItemSumInsuredObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPValuables.ExcessValuablesObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPValuables.ExcessValuablesObj.EiId).FirstOrDefault();
                    FPValuables.ExcessValuablesObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

            }
            return View(FPValuables);
        }

        [HttpPost]
        public ActionResult Valuables(int? cid, FPValuables FPValuables)
        {

            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> DescriptionSpeciItemsList = new List<SelectListItem>();
            DescriptionSpeciItemsList = commonModel.descriptionListFC();
            FPValuables.SpecifiedItemDescriptionObj.SpecItemDescriptionList = DescriptionSpeciItemsList;
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPValuables.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPValuables.CustomerId;
            }
            List<SelectListItem> ValuablesexcessToPay = new List<SelectListItem>();
            ValuablesexcessToPay = commonModel.excessRate();
            FPValuables.ExcessValuablesObj.ExcessList = ValuablesexcessToPay;
            string policyid = null;
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (FPValuables.CoverUnspecifiedValuablesObj != null && FPValuables.CoverUnspecifiedValuablesObj.EiId > 0 && FPValuables.CoverUnspecifiedValuablesObj.CoverUnspecifiedValuables != null)
                {
                    db.IT_InsertCustomerQnsData(FPValuables.CustomerId, Convert.ToInt32(FarmPolicySection.ValuablesFarm), FPValuables.CoverUnspecifiedValuablesObj.EiId, FPValuables.CoverUnspecifiedValuablesObj.CoverUnspecifiedValuables.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPValuables.SpecifiedItemDescriptionObj != null && FPValuables.SpecifiedItemDescriptionObj.EiId > 0 && FPValuables.SpecifiedItemDescriptionObj.ItemDescription != null)
                {
                    db.IT_InsertCustomerQnsData(FPValuables.CustomerId, Convert.ToInt32(FarmPolicySection.ValuablesFarm), FPValuables.SpecifiedItemDescriptionObj.EiId, FPValuables.SpecifiedItemDescriptionObj.ItemDescription.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPValuables.SpecifiedItemSumInsuredObj != null && FPValuables.SpecifiedItemSumInsuredObj.EiId > 0 && FPValuables.SpecifiedItemSumInsuredObj.ItemSumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPValuables.CustomerId, Convert.ToInt32(FarmPolicySection.ValuablesFarm), FPValuables.SpecifiedItemSumInsuredObj.EiId, FPValuables.SpecifiedItemSumInsuredObj.ItemSumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPValuables.ExcessValuablesObj != null && FPValuables.ExcessValuablesObj.EiId > 0 && FPValuables.ExcessValuablesObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FPValuables.CustomerId, Convert.ToInt32(FarmPolicySection.ValuablesFarm), FPValuables.ExcessValuablesObj.EiId, FPValuables.ExcessValuablesObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
            }
            return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = cid });
        }
    }
}