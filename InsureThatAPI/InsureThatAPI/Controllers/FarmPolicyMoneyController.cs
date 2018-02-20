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
    public class FarmPolicyMoneyController : Controller
    {
        // GET: FarmPolicyMoney
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Money(int? cid, int? PcId)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> FPMoneyExcessToPay = new List<SelectListItem>();
            FPMoneyExcessToPay = commonModel.excessRate();
            FPMoney FPMoney = new FPMoney();
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPMoney.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPMoney.CustomerId;
            }
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            NewPolicyDetailsClass commonmethods = new NewPolicyDetailsClass();
            var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
            string apikey = null;
            bool policyinclusion = policyinclusions.Exists(p => p.Name == "Money");
            if (Session["apiKey"] != null)
            {
                apikey = Session["apiKey"].ToString();
                FPMoney.ApiKey = Session["apiKey"].ToString();
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
                FPMoney.PolicyId = policyid;
            }
            else if (Session["Policyinclustions"] != null)
            {
                #region Policy Selected or not testing
            
                FPMoney.PolicyInclusions = new List<SessionModel>();
                var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                FPMoney.PolicyInclusions = Policyincllist;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Exists(p => p.name == "Money"))
                    {
                     
                    }
                    else
                    {
                        if (Policyincllist.Exists(p => p.name == "Transit"))
                        {
                            return RedirectToAction("Transit", "FarmPolicyTransit", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Valuables"))
                        {
                            return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "LiveStock"))
                        {
                            return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = cid, PcId = PcId });

                        }
                        else if (Policyincllist.Exists(p => p.name == "Personal Liabilities Farm"))
                        {
                            return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Home Building"))
                        {
                            return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Home Content"))
                        {
                            return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Machinery"))
                        {
                            return RedirectToAction("Machinery", "FarmPolicyMachinery", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Motor"))
                        {
                            return RedirectToAction("VehicleDescription", "FarmPolicyMotor", new { cid = cid, PcId = PcId });
                        }
                        if (Policyincllist.Exists(p => p.name == "Money"))
                        {
                            if (Session["unId"] == null && Session["profileId"] == null)
                            {
                                Session["unId"] = Policyincllist.Where(p => p.name == "Money").Select(p => p.UnitId).First();
                                Session["profileId"] = Policyincllist.Where(p => p.name == "Money").Select(p => p.ProfileId).First();
                            }
                        }
                        else
                        {
                            return RedirectToAction("DisclosureDetails", "Disclosure", new { cid = cid, PcId = PcId });
                        }
                    }
            }
            #endregion
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

            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = Convert.ToInt32(Session["unId"]);
            int profileid = Convert.ToInt32(Session["profileId"]);
            if (policyinclusion == true && PcId != null && PcId.HasValue)
            {
                FPMoney.ExistingPolicyInclustions = policyinclusions;

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
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Money&SectionUnId=&ProfileUnId=");
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
                if (unitdetails.ProfileData != null)
                {
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPMoney.AtTheLocationObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPMoney.AtTheLocationObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMoney.AtTheLocationObj.atLocation = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPMoney.LockedSafeAtLocationObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPMoney.LockedSafeAtLocationObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMoney.LockedSafeAtLocationObj.lockedsafeatlocation = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPMoney.BankorOtherFinanInstObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPMoney.BankorOtherFinanInstObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMoney.BankorOtherFinanInstObj.bankorotherFinanInst = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPMoney.ExcessFPMoneyObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPMoney.ExcessFPMoneyObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMoney.ExcessFPMoneyObj.Excess = val;
                    }
                }
            }
            



            //var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.FixedFarmProperty), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            //if (details != null && details.Any())
            //{
            //    if (details.Exists(q => q.QuestionId == FPMoney.AtTheLocationObj.EiId))
            //    {
            //        FPMoney.AtTheLocationObj.atLocation = Convert.ToString(details.Where(q => q.QuestionId == FPMoney.AtTheLocationObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == FPMoney.LockedSafeAtLocationObj.EiId))
            //    {
            //        FPMoney.LockedSafeAtLocationObj.lockedsafeatlocation = Convert.ToString(details.Where(q => q.QuestionId == FPMoney.LockedSafeAtLocationObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == FPMoney.BankorOtherFinanInstObj.EiId))
            //    {
            //        FPMoney.BankorOtherFinanInstObj.bankorotherFinanInst = Convert.ToString(details.Where(q => q.QuestionId == FPMoney.BankorOtherFinanInstObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == FPMoney.ExcessFPMoneyObj.EiId))
            //    {
            //        var loc = details.Where(q => q.QuestionId == FPMoney.ExcessFPMoneyObj.EiId).FirstOrDefault();
            //        FPMoney.ExcessFPMoneyObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
            //    }
            //}
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
            return RedirectToAction("Transit","FarmPolicyTransit" , new { cid = cid });
        }
    }
}