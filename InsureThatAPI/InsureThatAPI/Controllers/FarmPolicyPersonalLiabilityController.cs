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
    public class FarmPolicyPersonalLiabilityController : Controller
    {
        // GET: FarmPolicyPersonalLiability
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PersonalLiability(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();

            List<SelectListItem> PersonalLiabilityexcessToPay = new List<SelectListItem>();
            PersonalLiabilityexcessToPay = commonModel.excessRate();

            PersonalLiability PersonalLiability = new PersonalLiability();
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                        if (Policyincllist.Contains("PersonalLiabilitiesFarm"))
                        {
                           
                        }
                        else{ if (Policyincllist.Contains("HomeBuildingFarm"))
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
                PersonalLiability.CustomerId = cid.Value;
            }
            PersonalLiability.LimitindemnityObj = new LimitOfIndemnity();
            PersonalLiability.LimitindemnityObj.EiId = 63663;

            PersonalLiability.ExcessplObj = new ExcessPL();
            PersonalLiability.ExcessplObj.ExcessList = PersonalLiabilityexcessToPay;
            PersonalLiability.ExcessplObj.EiId = 63667;

            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.PersonalLiabilitiesFarm) , Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == PersonalLiability.LimitindemnityObj.EiId))
                {
                    PersonalLiability.LimitindemnityObj.Limitindemnity = Convert.ToString(details.Where(q => q.QuestionId == PersonalLiability.LimitindemnityObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == PersonalLiability.ExcessplObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == PersonalLiability.ExcessplObj.EiId).FirstOrDefault();
                    PersonalLiability.ExcessplObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

            }
            return View(PersonalLiability);
        }

        [HttpPost]
        public ActionResult PersonalLiability(int? cid, PersonalLiability PersonalLiability)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();

            List<SelectListItem> PersonalLiabilityexcessToPay = new List<SelectListItem>();
            PersonalLiabilityexcessToPay = commonModel.excessRate();
            PersonalLiability.ExcessplObj.ExcessList = PersonalLiabilityexcessToPay;
            if (cid != null)
            {
                ViewBag.cid = cid;
                PersonalLiability.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = PersonalLiability.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (PersonalLiability.LimitindemnityObj != null && PersonalLiability.LimitindemnityObj.EiId > 0 && PersonalLiability.LimitindemnityObj.Limitindemnity != null)
                {
                    db.IT_InsertCustomerQnsData(PersonalLiability.CustomerId, Convert.ToInt32(FarmPolicySection.PersonalLiabilitiesFarm), PersonalLiability.LimitindemnityObj.EiId, PersonalLiability.LimitindemnityObj.Limitindemnity.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (PersonalLiability.ExcessplObj != null && PersonalLiability.ExcessplObj.EiId > 0 && PersonalLiability.ExcessplObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(PersonalLiability.CustomerId, Convert.ToInt32(FarmPolicySection.PersonalLiabilitiesFarm), PersonalLiability.ExcessplObj.EiId, PersonalLiability.ExcessplObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
            }
            return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = cid });
        }
    }
}