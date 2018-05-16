using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;//append model
using InsureThatAPI.CommonMethods;//append Common Methods
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

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
        public async System.Threading.Tasks.Task<ActionResult> PersonalLiability(int? cid, int? PcId)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> PersonalLiabilityexcessToPay = new List<SelectListItem>();
            PersonalLiability PersonalLiability = new PersonalLiability();
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                PersonalLiability.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = PersonalLiability.CustomerId;
            }
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            NewPolicyDetailsClass commonmethods = new NewPolicyDetailsClass();
            var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
            string apikey = null;
            bool policyinclusion = policyinclusions.Exists(p => p.Name == "Personal Liabilities Farm");
            if (Session["apiKey"] != null)
            {
                apikey = Session["apiKey"].ToString();
                PersonalLiability.ApiKey = Session["apiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            string policyid = null;
            List<SessionModel> PolicyInclustions = new List<SessionModel>();
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                policyid = PcId.ToString();
                PersonalLiability.PolicyId = policyid;
            }
            else if (Session["Policyinclustions"] != null)
            {
                #region Policy Selected or not testing
                PersonalLiability.PolicyInclusions = new List<SessionModel>();
                var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                PersonalLiability.PolicyInclusions = Policyincllist;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Exists(p => p.name == "Personal Liabilities Farm"))
                    {

                    }
                    else if (Policyincllist.Exists(p => p.name == "Valuables"))
                    {
                        return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid, PcId = PcId });
                    }
                    else if (Policyincllist.Exists(p => p.name == "Motor"))
                    {
                        return RedirectToAction("VehicleDescription", "FarmMotors", new { cid = cid, PcId = PcId });
                    }
                    if (Policyincllist.Exists(p => p.name == "Personal Liabilities Farm"))
                        {
                            if (Session["unId"] == null && Session["profileId"] == null)
                            {
                                Session["unId"] = Policyincllist.Where(p => p.name == "Personal Liabilities Farm").Select(p => p.UnitId).First();
                                Session["profileId"] = Policyincllist.Where(p => p.name == "Personal Liabilities Farm").Select(p => p.ProfileId).First();
                            }
                        }
                        else
                        {
                            return RedirectToAction("DisclosureDetails", "Disclosure", new { cid = cid, PcId = PcId });
                        }
                    
                }
                #endregion
            }
            PersonalLiability.LimitindemnityObj = new LimitOfIndemnity();
            PersonalLiability.LimitindemnityObj.EiId = 63663;
            PersonalLiability.ExcessplObj = new ExcessPL();
            PersonalLiability.ExcessplObj.ExcessList = PersonalLiabilityexcessToPay;
            PersonalLiability.ExcessplObj.EiId = 63667;

            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = Convert.ToInt32(Session["unId"]);
            int profileid = Convert.ToInt32(Session["profileId"]);
            if (policyinclusion == true && PcId != null && PcId.HasValue)
            {
                PersonalLiability.ExistingPolicyInclustions = policyinclusions;

                HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                if (EmpResponse != null)
                {
                    unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                }
            }
            else
            {
                if (PcId == null && Session["unId"] == null && Session["profileId"] == null)
                {
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Personal Liability&SectionUnId=&ProfileUnId=0");
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails != null && unitdetails.SectionData != null)
                        {
                            Session["unId"] = unitdetails.SectionData.UnId;
                            Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                        }
                    }
                }
                else if (PcId == null && Session["unId"] != null && Session["profileId"] != null)
                {
                    HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                    var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails != null && unitdetails.SectionData != null)
                        {
                            Session["unId"] = unitdetails.SectionData.UnId;
                            Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                        }
                    }
                }
            }
            if (unitdetails != null)
            {
                if (unitdetails.SectionData != null && unitdetails.SectionData.ValueData!=null)
                {
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == PersonalLiability.LimitindemnityObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == PersonalLiability.LimitindemnityObj.EiId).Select(p => p.Value).FirstOrDefault();
                        PersonalLiability.LimitindemnityObj.Limitindemnity = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == PersonalLiability.ExcessplObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == PersonalLiability.ExcessplObj.EiId).Select(p => p.Value).FirstOrDefault();
                        PersonalLiability.ExcessplObj.Excess = val;
                    }
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
           
            Session["unId"] = null;
            Session["profileId"] = null;
            return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = PersonalLiability.CustomerId, PcId = PersonalLiability.PcId });
        }
    }
}