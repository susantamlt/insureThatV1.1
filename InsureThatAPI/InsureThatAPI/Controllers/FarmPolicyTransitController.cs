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
    public class FarmPolicyTransitController : Controller
    {
        // GET: FarmPolicyTransit
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Transit(int? cid, int? PcId)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> TransitExcessToPay = new List<SelectListItem>();
            TransitExcessToPay = commonModel.excessRate();
            FPTransit FPTransit = new FPTransit();
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPTransit.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPTransit.CustomerId;
            }
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            NewPolicyDetailsClass commonmethods = new NewPolicyDetailsClass();
            var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
            string apikey = null;
            bool policyinclusion = policyinclusions.Exists(p => p.Name == "Transit");
            List<SessionModel> PolicyInclustions = new List<SessionModel>();
            if (Session["apiKey"] != null)
            {
                apikey = Session["apiKey"].ToString();
                FPTransit.ApiKey = Session["apiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            string policyid = null;
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                policyid = PcId.ToString();
                FPTransit.PolicyId = policyid;
            }
            else if (Session["Policyinclustions"] != null)
            {
                #region Policy Selected or not testing
             
                FPTransit.PolicyInclusions = new List<SessionModel>();
                var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                FPTransit.PolicyInclusions = Policyincllist;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Exists(p => p.name == "Transit"))
                    {
                       
                    }
                    else if (Policyincllist.Exists(p => p.name == "LiveStock"))
                    {
                        return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = cid, PcId = PcId });
                    }
                    else if (Policyincllist.Exists(p => p.name == "Home Buildings"))
                    {
                        return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = cid, PcId = PcId });
                    }
                    else if (Policyincllist.Exists(p => p.name == "Home Contents"))
                    {
                        return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid, PcId = PcId });
                    }
                    else if (Policyincllist.Exists(p => p.name == "Personal Liabilities Farm"))
                    {
                        return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid, PcId = PcId });
                    }
                    else if (Policyincllist.Exists(p => p.name == "Valuables"))
                    {
                        return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid, PcId = PcId });
                    }
                    else if (Policyincllist.Exists(p => p.name == "Motor"))
                    {
                        return RedirectToAction("VehicleDescription", "FarmMotors", new { cid = cid, PcId = PcId });
                    }
                    if (Policyincllist.Exists(p => p.name == "Transit"))
                    {
                        if (Session["unId"] == null && Session["profileId"] == null)
                        {
                            Session["unId"] = Policyincllist.Where(p => p.name == "Transit").Select(p => p.UnitId).First();
                            Session["profileId"] = Policyincllist.Where(p => p.name == "Transit").Select(p => p.ProfileId).First();
                        }
                    }
                    else
                    {
                        return RedirectToAction("DisclosureDetails", "Disclosure", new { cid = cid, PcId = PcId });
                    }
                
                }
                #endregion
            }
            FPTransit.LivestockMaxValOneLoadObj = new LivestockMaximumValOfOneload();
            FPTransit.LivestockMaxValOneLoadObj.EiId = 63249;

            FPTransit.FarmProduceMaxValOneLoadObj = new FarmProduceMaxValOfOneLoad();
            FPTransit.FarmProduceMaxValOneLoadObj.EiId = 63253;

            FPTransit.ExcessFPTransitObj = new ExcessFPTransit();
            FPTransit.ExcessFPTransitObj.ExcessList = TransitExcessToPay;
            FPTransit.ExcessFPTransitObj.EiId = 63257;
            FPTransit.AddressObj = new AddressTsAddress();
            FPTransit.AddressObj.EiId = 0;

            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = Convert.ToInt32(Session["unId"]);
            int profileid = Convert.ToInt32(Session["profileId"]);
            if (policyinclusion == true && PcId != null && PcId.HasValue)
            {
                FPTransit.ExistingPolicyInclustions = policyinclusions;

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
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Transit&SectionUnId=&ProfileUnId=");
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
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPTransit.LivestockMaxValOneLoadObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPTransit.LivestockMaxValOneLoadObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPTransit.LivestockMaxValOneLoadObj.livestockMaxValoneload = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPTransit.LivestockMaxValOneLoadObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPTransit.LivestockMaxValOneLoadObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPTransit.LivestockMaxValOneLoadObj.livestockMaxValoneload = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPTransit.ExcessFPTransitObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPTransit.ExcessFPTransitObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPTransit.ExcessFPTransitObj.Excess = val;
                    }
                }
                if (unitdetails.SectionData != null && unitdetails.SectionData.AddressData != null)
                {
                    if (unitdetails.SectionData.AddressData != null)
                    {
                        FPTransit.AddressObj.Address = unitdetails.SectionData.AddressData.AddressLine1 + ", " + unitdetails.SectionData.AddressData.Suburb + " ," + unitdetails.SectionData.AddressData.State + ", " + unitdetails.SectionData.AddressData.Postcode;
                    }
                }
            }            

            //var db = new MasterDataEntities();
            //string policyid = null;
            //var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(FarmPolicySection.Transit), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            //if (details != null && details.Any())
            //{
            //    if (details.Exists(q => q.QuestionId == FPTransit.LivestockMaxValOneLoadObj.EiId))
            //    {
            //        FPTransit.LivestockMaxValOneLoadObj.livestockMaxValoneload = Convert.ToString(details.Where(q => q.QuestionId == FPTransit.LivestockMaxValOneLoadObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == FPTransit.FarmProduceMaxValOneLoadObj.EiId))
            //    {
            //        FPTransit.FarmProduceMaxValOneLoadObj.farmproduceMaxValoneload = Convert.ToString(details.Where(q => q.QuestionId == FPTransit.FarmProduceMaxValOneLoadObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == FPTransit.ExcessFPTransitObj.EiId))
            //    {
            //        var loc = details.Where(q => q.QuestionId == FPTransit.ExcessFPTransitObj.EiId).FirstOrDefault();
            //        FPTransit.ExcessFPTransitObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
            //    }
            //}
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
    
            Session["unId"] = null;
            Session["profileId"] = null;
            return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = FPTransit.CustomerId, PcId = FPTransit.PcId });
        }
    }
}