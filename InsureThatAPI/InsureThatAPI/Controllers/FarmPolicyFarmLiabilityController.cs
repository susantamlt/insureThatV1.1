using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;
using InsureThatAPI.CommonMethods;
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;

namespace InsureThatAPI.Controllers
{
    public class FarmPolicyFarmLiabilityController : Controller
    {
        // GET: FarmPolicyFarmLiability
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FarmLiability(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> GLLimitOfIndemnity = new List<SelectListItem>();
            GLLimitOfIndemnity = commonModel.LimitOfIndemnity();
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Contains("FixedFarmProperty"))
                    {

                    }
                    else
                    {
                        if (Policyincllist.Contains("MobileFarmProperty"))
                        {
                            return RedirectToAction("FarmContents", "MobileFarm", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("FarmInteruption"))
                        {
                            return RedirectToAction("FarmInterruption", "FarmPolicyFarmInterruption", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("FarmLiability"))
                        {
                            return RedirectToAction("FarmLiability", "FarmPolicyFarmLiability", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Burglary"))
                        {
                            return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Electronics"))
                        {
                            return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Money"))
                        {
                            return RedirectToAction("Money", "FarmPolicyMoney", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Transit"))
                        {
                            return RedirectToAction("FarmPolicyTransit", "Transit", new { cid = cid });
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
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 2 });
            }
          
            List<SelectListItem> excessToPayFarmLiability = new List<SelectListItem>();
            excessToPayFarmLiability = commonModel.excessRate();

            List<SelectListItem> GLTypeOfAccommodation = new List<SelectListItem>();
            GLTypeOfAccommodation = commonModel.TypeOfAccommodation();

            FPFarmliability FPFarmliability = new FPFarmliability();
            ViewBag.cid = cid;
            if (cid != null)
            {
                FPFarmliability.CustomerId = cid.Value;
            }
            FPFarmliability.GenLiabilityLimitOfIndemnityFPObj = new GenLiabilityLimitOfIndemnityFP();
            FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.GLLimitOfIndemnityFPList = GLLimitOfIndemnity;
            FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.EiId = 62871;

            FPFarmliability.ProdLiabilityLimitOfIndemnityFPObj = new ProdLiabilityLimitOfIndemnityFP();
            FPFarmliability.ProdLiabilityLimitOfIndemnityFPObj.EiId = 62875;

            FPFarmliability.ListOfProductsSoldFPObj = new ListOfProductsSoldFP();
            FPFarmliability.ListOfProductsSoldFPObj.EiId = 62877;

            FPFarmliability.OptPayingGuestFP = new OptPayingGuestFP();
            FPFarmliability.OptPayingGuestFP.EiId = 62883;

            FPFarmliability.TypeOfAccomGuestsStayingInFPObj = new TypeOfAccomGuestsStayingInFP();
            FPFarmliability.TypeOfAccomGuestsStayingInFPObj.TypeOfAccommodationGuestsStayingInList = GLTypeOfAccommodation;
            FPFarmliability.TypeOfAccomGuestsStayingInFPObj.EiId = 62887;

            FPFarmliability.DescriptionOfAccommodationFPObj = new DescriptionOfAccommodationFP();
            FPFarmliability.DescriptionOfAccommodationFPObj.EiId = 62889;

            FPFarmliability.OptAccomComplyRegulationFPObj = new OptAccomComplyRegulationFP();
            FPFarmliability.OptAccomComplyRegulationFPObj.EiId = 62899;

            FPFarmliability.OptHolidayFarmsFPObj = new OptHolidayFarmsFP();
            FPFarmliability.OptHolidayFarmsFPObj.EiId = 62901;


            FPFarmliability.ExcessFPFarmLiabilityObj = new ExcessFPFarmLiability();
            FPFarmliability.ExcessFPFarmLiabilityObj.ExcessList = excessToPayFarmLiability;
            FPFarmliability.ExcessFPFarmLiabilityObj.EiId = 62907;
            string policyid = null;
            var db = new MasterDataEntities();
            var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(FarmPolicySection.FarmLiability) , Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {

                if (details.Exists(q => q.QuestionId == FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.EiId).FirstOrDefault();
                    FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.LimitOfIndemnity = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPFarmliability.ProdLiabilityLimitOfIndemnityFPObj.EiId))
                {
                    FPFarmliability.ProdLiabilityLimitOfIndemnityFPObj.LimitOfIndemnity = Convert.ToString(details.Where(q => q.QuestionId == FPFarmliability.ProdLiabilityLimitOfIndemnityFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPFarmliability.ListOfProductsSoldFPObj.EiId))
                {
                    FPFarmliability.ListOfProductsSoldFPObj.ListOfProductsSold = Convert.ToString(details.Where(q => q.QuestionId == FPFarmliability.ListOfProductsSoldFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPFarmliability.OptPayingGuestFP.EiId))
                {
                    FPFarmliability.OptPayingGuestFP.PayingGuestOption = Convert.ToString(details.Where(q => q.QuestionId == FPFarmliability.OptPayingGuestFP.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPFarmliability.TypeOfAccomGuestsStayingInFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPFarmliability.TypeOfAccomGuestsStayingInFPObj.EiId).FirstOrDefault();
                    FPFarmliability.TypeOfAccomGuestsStayingInFPObj.TypeOfAccommodation = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPFarmliability.DescriptionOfAccommodationFPObj.EiId))
                {
                    FPFarmliability.DescriptionOfAccommodationFPObj.DescOfAccommodation = Convert.ToString(details.Where(q => q.QuestionId == FPFarmliability.DescriptionOfAccommodationFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPFarmliability.OptAccomComplyRegulationFPObj.EiId))
                {
                    FPFarmliability.OptAccomComplyRegulationFPObj.AccomComplyRegulations = Convert.ToString(details.Where(q => q.QuestionId == FPFarmliability.OptAccomComplyRegulationFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPFarmliability.OptHolidayFarmsFPObj.EiId))
                {
                    FPFarmliability.OptHolidayFarmsFPObj.HolidayFarms = Convert.ToString(details.Where(q => q.QuestionId == FPFarmliability.OptHolidayFarmsFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPFarmliability.ExcessFPFarmLiabilityObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPFarmliability.ExcessFPFarmLiabilityObj.EiId).FirstOrDefault();
                    FPFarmliability.ExcessFPFarmLiabilityObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }   
            }
            return View(FPFarmliability);
        }

        [HttpPost]
        public ActionResult FarmLiability(int? cid, FPFarmliability FPFarmliability)
        {

            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> GLLimitOfIndemnity = new List<SelectListItem>();
            GLLimitOfIndemnity = commonModel.LimitOfIndemnity();
            FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.GLLimitOfIndemnityFPList = GLLimitOfIndemnity;
            List<SelectListItem> excessToPayFarmLiability = new List<SelectListItem>();
            excessToPayFarmLiability = commonModel.excessRate();
            FPFarmliability.ExcessFPFarmLiabilityObj.ExcessList = excessToPayFarmLiability;
            List<SelectListItem> GLTypeOfAccommodation = new List<SelectListItem>();
            GLTypeOfAccommodation = commonModel.TypeOfAccommodation();
            FPFarmliability.TypeOfAccomGuestsStayingInFPObj.TypeOfAccommodationGuestsStayingInList = GLTypeOfAccommodation;
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (FPFarmliability.GenLiabilityLimitOfIndemnityFPObj != null && FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.EiId > 0 && FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.LimitOfIndemnity != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmliability.CustomerId, Convert.ToInt32(FarmPolicySection.FarmLiability), FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.EiId, FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.LimitOfIndemnity.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmliability.ProdLiabilityLimitOfIndemnityFPObj != null && FPFarmliability.ProdLiabilityLimitOfIndemnityFPObj.EiId > 0 && FPFarmliability.ProdLiabilityLimitOfIndemnityFPObj.LimitOfIndemnity != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmliability.CustomerId, Convert.ToInt32(FarmPolicySection.FarmLiability), FPFarmliability.ProdLiabilityLimitOfIndemnityFPObj.EiId, FPFarmliability.ProdLiabilityLimitOfIndemnityFPObj.LimitOfIndemnity.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmliability.OptPayingGuestFP != null && FPFarmliability.OptPayingGuestFP.EiId > 0 && FPFarmliability.OptPayingGuestFP.PayingGuestOption != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmliability.CustomerId, Convert.ToInt32(FarmPolicySection.FarmLiability), FPFarmliability.OptPayingGuestFP.EiId, FPFarmliability.OptPayingGuestFP.PayingGuestOption.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmliability.TypeOfAccomGuestsStayingInFPObj != null && FPFarmliability.TypeOfAccomGuestsStayingInFPObj.EiId > 0 && FPFarmliability.TypeOfAccomGuestsStayingInFPObj.TypeOfAccommodation != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmliability.CustomerId, Convert.ToInt32(FarmPolicySection.FarmLiability), FPFarmliability.TypeOfAccomGuestsStayingInFPObj.EiId, FPFarmliability.TypeOfAccomGuestsStayingInFPObj.TypeOfAccommodation.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmliability.DescriptionOfAccommodationFPObj != null && FPFarmliability.DescriptionOfAccommodationFPObj.EiId > 0 && FPFarmliability.DescriptionOfAccommodationFPObj.DescOfAccommodation != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmliability.CustomerId, Convert.ToInt32(FarmPolicySection.FarmLiability), FPFarmliability.DescriptionOfAccommodationFPObj.EiId, FPFarmliability.DescriptionOfAccommodationFPObj.DescOfAccommodation.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmliability.OptAccomComplyRegulationFPObj != null && FPFarmliability.OptAccomComplyRegulationFPObj.EiId > 0 && FPFarmliability.OptAccomComplyRegulationFPObj.AccomComplyRegulations != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmliability.CustomerId, Convert.ToInt32(FarmPolicySection.FarmLiability), FPFarmliability.OptAccomComplyRegulationFPObj.EiId, FPFarmliability.OptAccomComplyRegulationFPObj.AccomComplyRegulations.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmliability.OptHolidayFarmsFPObj != null && FPFarmliability.OptHolidayFarmsFPObj.EiId > 0 && FPFarmliability.OptHolidayFarmsFPObj.HolidayFarms != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmliability.CustomerId, Convert.ToInt32(FarmPolicySection.FarmLiability), FPFarmliability.OptHolidayFarmsFPObj.EiId, FPFarmliability.OptHolidayFarmsFPObj.HolidayFarms.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmliability.ExcessFPFarmLiabilityObj != null && FPFarmliability.ExcessFPFarmLiabilityObj.EiId > 0 && FPFarmliability.ExcessFPFarmLiabilityObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmliability.CustomerId, Convert.ToInt32(FarmPolicySection.FarmLiability), FPFarmliability.ExcessFPFarmLiabilityObj.EiId, FPFarmliability.ExcessFPFarmLiabilityObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                
            }
            return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid });
        }

    }
}