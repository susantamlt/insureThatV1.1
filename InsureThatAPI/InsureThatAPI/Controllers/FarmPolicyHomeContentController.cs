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
    public class FarmPolicyHomeContentController : Controller
    {
        // GET: FarmPolicyHomeContent
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult HomeContents(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> DescriptionListHomeContents = new List<SelectListItem>();
            DescriptionListHomeContents = commonModel.descriptionListFC();

            FPHomeContents FPHomeContents = new FPHomeContents();

            FPHomeContents.CoverForUnspecifiedContentsFPObj = new CoverForUnspecifiedContentsFP();
            FPHomeContents.CoverForUnspecifiedContentsFPObj.EiId = 63551;

            FPHomeContents.DescriptionFPObj = new DescriptionsFP();
            FPHomeContents.DescriptionFPObj.DescriptionList = DescriptionListHomeContents;
            FPHomeContents.DescriptionFPObj.EiId = 63563;

            FPHomeContents.SumInsuredFPObj = new SumInsuredFP();
            FPHomeContents.SumInsuredFPObj.EiId = 63565;

            FPHomeContents.OptHCcoverOptionsFPObj = new OptHCcoverOptionsFP();
            FPHomeContents.OptHCcoverOptionsFPObj.EiId = 63549;

            FPHomeContents.OptHCLastPaidInsuranceFPObj = new OptHCLastPaidInsuranceFP();
            FPHomeContents.OptHCLastPaidInsuranceFPObj.EiId = 11111;

            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.HomeContent), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == FPHomeContents.CoverForUnspecifiedContentsFPObj.EiId))
                {
                    FPHomeContents.CoverForUnspecifiedContentsFPObj.CoverUnspecifiedContent = Convert.ToString(details.Where(q => q.QuestionId == FPHomeContents.CoverForUnspecifiedContentsFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPHomeContents.DescriptionFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPHomeContents.DescriptionFPObj.EiId).FirstOrDefault();
                    FPHomeContents.DescriptionFPObj.Description = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPHomeContents.SumInsuredFPObj.EiId))
                {
                    FPHomeContents.SumInsuredFPObj.SumInsured = Convert.ToString(details.Where(q => q.QuestionId == FPHomeContents.SumInsuredFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPHomeContents.OptHCcoverOptionsFPObj.EiId))
                {
                    FPHomeContents.OptHCcoverOptionsFPObj.CoverOptions = Convert.ToString(details.Where(q => q.QuestionId == FPHomeContents.CoverForUnspecifiedContentsFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPHomeContents.OptHCLastPaidInsuranceFPObj.EiId))
                {
                    FPHomeContents.OptHCLastPaidInsuranceFPObj.LastpaidInsurance = Convert.ToString(details.Where(q => q.QuestionId == FPHomeContents.OptHCLastPaidInsuranceFPObj.EiId).FirstOrDefault().Answer);
                }               
            }
            return View(FPHomeContents);
        }

        [HttpPost]
        public ActionResult HomeContents(int? cid, FPHomeContents FPHomeContents)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> DescriptionListHomeContents = new List<SelectListItem>();
            DescriptionListHomeContents = commonModel.descriptionListFC();
            FPHomeContents.DescriptionFPObj.DescriptionList = DescriptionListHomeContents;
            string policyid = null;

            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (FPHomeContents.CoverForUnspecifiedContentsFPObj != null && FPHomeContents.CoverForUnspecifiedContentsFPObj.EiId > 0 && FPHomeContents.CoverForUnspecifiedContentsFPObj.CoverUnspecifiedContent != null)
                {
                    db.IT_InsertCustomerQnsData(FPHomeContents.CustomerId, Convert.ToInt32(FarmPolicySection.HomeContent), FPHomeContents.CoverForUnspecifiedContentsFPObj.EiId, FPHomeContents.CoverForUnspecifiedContentsFPObj.CoverUnspecifiedContent.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPHomeContents.DescriptionFPObj != null && FPHomeContents.DescriptionFPObj.EiId > 0 && FPHomeContents.DescriptionFPObj.Description != null)
                {
                    db.IT_InsertCustomerQnsData(FPHomeContents.CustomerId, Convert.ToInt32(FarmPolicySection.HomeContent), FPHomeContents.DescriptionFPObj.EiId, FPHomeContents.DescriptionFPObj.Description.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPHomeContents.SumInsuredFPObj != null && FPHomeContents.SumInsuredFPObj.EiId > 0 && FPHomeContents.SumInsuredFPObj.SumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPHomeContents.CustomerId, Convert.ToInt32(FarmPolicySection.HomeContent), FPHomeContents.SumInsuredFPObj.EiId, FPHomeContents.SumInsuredFPObj.SumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPHomeContents.OptHCcoverOptionsFPObj != null && FPHomeContents.OptHCcoverOptionsFPObj.EiId > 0 && FPHomeContents.OptHCcoverOptionsFPObj.CoverOptions != null)
                {
                    db.IT_InsertCustomerQnsData(FPHomeContents.CustomerId, Convert.ToInt32(FarmPolicySection.HomeContent), FPHomeContents.OptHCcoverOptionsFPObj.EiId, FPHomeContents.OptHCcoverOptionsFPObj.CoverOptions.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPHomeContents.OptHCLastPaidInsuranceFPObj != null && FPHomeContents.OptHCLastPaidInsuranceFPObj.EiId > 0 && FPHomeContents.OptHCLastPaidInsuranceFPObj.LastpaidInsurance != null)
                {
                    db.IT_InsertCustomerQnsData(FPHomeContents.CustomerId, Convert.ToInt32(FarmPolicySection.HomeContent), FPHomeContents.OptHCLastPaidInsuranceFPObj.EiId, FPHomeContents.OptHCLastPaidInsuranceFPObj.LastpaidInsurance.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }                
            }
            return View(FPHomeContents);
        }
    }
}