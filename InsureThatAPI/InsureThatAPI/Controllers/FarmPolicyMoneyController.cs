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
    public class FarmPolicyMoneyController : Controller
    {
        // GET: FarmPolicyMoney
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Money(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> FPMoneyExcessToPay = new List<SelectListItem>();
            FPMoneyExcessToPay = commonModel.excessRate();
            var db = new MasterDataEntities();
            FPMoney FPMoney = new FPMoney();
            string policyid = null;
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {


                    if (Policyincllist.Contains("Money"))
                    {

                    }
                    else
                    {
                        if (Policyincllist.Contains("Transit"))
                        {
                            return RedirectToAction("Transit","FarmPolicyTransit", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("ValuablesFarm"))
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
                var policyinclusionslist = db.IT_GetPolicyInclusions(cid, policyid, Convert.ToInt32(PolicyType.FarmPolicy)).FirstOrDefault();
                if (policyinclusionslist.PolicyInclusions != null)
                {
                    if (policyinclusionslist.PolicyInclusions.Length > 1)
                    {
                        var policyinclusions = policyinclusionslist.PolicyInclusions.Split('-');
                        if (policyinclusions != null && policyinclusions.Length > 0)
                        {
                            for (int i = 1; i < policyinclusions.Length; i++)
                            {
                                if (i == 7 && policyinclusions[i] == "1")
                                {

                                }
                                else
                                {
                                    if (i == 8 && policyinclusions[i] == "1")
                                    {
                                        return RedirectToAction("Transit", "FarmPolicyTransit", new { cid = cid });
                                    }
                                    else if (i == 9 && policyinclusions[i] == "1")
                                    {
                                        return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid });
                                    }
                                    else if (i == 10 && policyinclusions[i] == "1")
                                    {
                                        return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = cid });
                                    }
                                    else if (i == 11 && policyinclusions[i] == "1")
                                    {
                                        return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid });
                                    }
                                    else if (i == 12 && policyinclusions[i] == "1")
                                    {
                                        return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = cid });
                                    }
                                    else if (i == 13 && policyinclusions[i] == "1")
                                    {
                                        return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid });
                                    }
                                    else if (i == 14 && policyinclusions[i] == "1")
                                    {
                                        //  return RedirectToAction("", "", new { cid = cid });
                                    }
                                    else if (i == 15 && policyinclusions[i] == "1")
                                    {
                                        // return RedirectToAction("", "", new { cid = cid });
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 2 });
                }
            }
            ViewBag.cid = cid;
            if (cid != null)
            {
                FPMoney.CustomerId = cid.Value;
            }
            FPMoney.AtTheLocationObj = new AtTheLocation();
            FPMoney.AtTheLocationObj.EiId = 62791;

            FPMoney.LockedSafeAtLocationObj = new LockedSafeAtLocation();
            FPMoney.LockedSafeAtLocationObj.EiId = 62795;

            FPMoney.BankorOtherFinanInstObj = new BankorOtherFinanInst();
            FPMoney.BankorOtherFinanInstObj.EiId = 62799;

            FPMoney.ExcessFPMoneyObj = new ExcessFPMoney();
            FPMoney.ExcessFPMoneyObj.ExcessList = FPMoneyExcessToPay;
            FPMoney.ExcessFPMoneyObj.EiId = 62801;



            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.FixedFarmProperty), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == FPMoney.AtTheLocationObj.EiId))
                {
                    FPMoney.AtTheLocationObj.atLocation = Convert.ToString(details.Where(q => q.QuestionId == FPMoney.AtTheLocationObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMoney.LockedSafeAtLocationObj.EiId))
                {
                    FPMoney.LockedSafeAtLocationObj.lockedsafeatlocation = Convert.ToString(details.Where(q => q.QuestionId == FPMoney.LockedSafeAtLocationObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMoney.BankorOtherFinanInstObj.EiId))
                {
                    FPMoney.BankorOtherFinanInstObj.bankorotherFinanInst = Convert.ToString(details.Where(q => q.QuestionId == FPMoney.BankorOtherFinanInstObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMoney.ExcessFPMoneyObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPMoney.ExcessFPMoneyObj.EiId).FirstOrDefault();
                    FPMoney.ExcessFPMoneyObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

            }
            return View(FPMoney);
        }

        [HttpPost]
        public ActionResult Money(int? cid, FPMoney FPMoney)
        {

            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> MoneyExcessToPay = new List<SelectListItem>();
            MoneyExcessToPay = commonModel.excessRate();
            FPMoney.ExcessFPMoneyObj.ExcessList = MoneyExcessToPay;
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (FPMoney.AtTheLocationObj != null && FPMoney.AtTheLocationObj.EiId > 0 && FPMoney.AtTheLocationObj.atLocation != null)
                {
                    db.IT_InsertCustomerQnsData(FPMoney.CustomerId, Convert.ToInt32(FarmPolicySection.FixedFarmProperty), FPMoney.AtTheLocationObj.EiId, FPMoney.AtTheLocationObj.atLocation.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMoney.LockedSafeAtLocationObj != null && FPMoney.LockedSafeAtLocationObj.EiId > 0 && FPMoney.LockedSafeAtLocationObj.lockedsafeatlocation != null)
                {
                    db.IT_InsertCustomerQnsData(FPMoney.CustomerId, Convert.ToInt32(FarmPolicySection.FixedFarmProperty), FPMoney.LockedSafeAtLocationObj.EiId, FPMoney.LockedSafeAtLocationObj.lockedsafeatlocation.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMoney.BankorOtherFinanInstObj != null && FPMoney.BankorOtherFinanInstObj.EiId > 0 && FPMoney.BankorOtherFinanInstObj.bankorotherFinanInst != null)
                {
                    db.IT_InsertCustomerQnsData(FPMoney.CustomerId, Convert.ToInt32(FarmPolicySection.FixedFarmProperty), FPMoney.BankorOtherFinanInstObj.EiId, FPMoney.BankorOtherFinanInstObj.bankorotherFinanInst.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMoney.ExcessFPMoneyObj != null && FPMoney.ExcessFPMoneyObj.EiId > 0 && FPMoney.ExcessFPMoneyObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FPMoney.CustomerId, Convert.ToInt32(FarmPolicySection.FixedFarmProperty), FPMoney.ExcessFPMoneyObj.EiId, FPMoney.ExcessFPMoneyObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
            }
            return RedirectToAction("FarmPolicyTransit", "Transit", new { cid = cid });
        }
    }
}