using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;
using InsureThatAPI.CommonMethods;
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
        public async System.Threading.Tasks.Task<ActionResult> HomeContents(int? cid, int? PcId)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> DescriptionListHomeContents = new List<SelectListItem>();
            DescriptionListHomeContents = commonModel.descriptionListFC();

            FPHomeContents FPHomeContents = new FPHomeContents();
            string ApiKey = null;
            if (Session["ApiKey"] != null)
            {
                ApiKey = Session["ApiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            var Policyincllist = new List<SessionModel>();
            FPValuables FPValuables = new FPValuables();
            if (Session["Policyinclustions"] != null)
            {
                Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                if (Policyincllist != null)
                {
                    if (Policyincllist != null)
                    {
                        if (Policyincllist.Exists(p => p.name == "Home Content" || p.name== "Home Contents"))
                        {

                        }
                        else if (Policyincllist.Exists(p => p.name == "Personal Liability"))
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
                        if (Policyincllist.Exists(p => p.name == "Home Content" || p.name == "Home Contents"))
                        {
                            if (Session["unId"] == null && Session["profileId"] == null)
                            {
                                Session["unId"] = Policyincllist.Where(p => p.name == "Home Content" ||  p.name == "Home Contents").Select(p => p.UnitId).First();
                                Session["profileId"] = Policyincllist.Where(p => p.name == "Home Content" || p.name == "Home Contents").Select(p => p.ProfileId).First();
                            }
                        }
                        else
                        {
                            return RedirectToAction("DisclosureDetails", "Disclosure", new { cid = cid, PcId = PcId });
                        }

                    }
                }
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 1021 });
            }
            if (cid != null && cid > 0)
            {
                ViewBag.cid = cid;
                FPHomeContents.CustomerId = cid.Value;
            }

            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            FPHomeContents.CoveroptionFPObj = new CoverOptionFP();
            FPHomeContents.CoveroptionFPObj.EiId = 63549;
            FPHomeContents.UnspecifiedFPObj = new UnspecifiedFP();
            FPHomeContents.UnspecifiedFPObj.EiId = 63551;
            FPHomeContents.DescriptionFPObj = new DescriptionsFP();
            FPHomeContents.DescriptionFPObj.DescriptionList = DescriptionListHomeContents;
            FPHomeContents.DescriptionFPObj.EiId = 63563;
            FPHomeContents.SumInsuredFPObj = new SumInsuredFP();
            FPHomeContents.SumInsuredFPObj.EiId = 63565;
            FPHomeContents.ClaimperiodFPObj = new ClaimFreePeriodFP();
            FPHomeContents.ClaimperiodFPObj.EiId = 63579;
            FPHomeContents.ExcessFPHContentObj = new ExcessFPHContent();
            FPHomeContents.ExcessFPHContentObj.EiId = 63581;
            FPHomeContents.discountFPObj = new NoClaimDiscountFP();
            FPHomeContents.discountFPObj.EiId = 63587;
            FPHomeContents.AgediscountObj = new AgeDiscountFP();
            FPHomeContents.AgediscountObj.EiId = 63591;

            var db = new MasterDataEntities();
            string policyid = null;
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPHomeContents.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPHomeContents.CustomerId;
            }
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = 0;
            int? profileid = 0;
            int Fprofileid = 0;
            if (Session["unId"] != null && Session["ProfileId"] != null)
            {
                unid = Convert.ToInt32(Session["unId"]);
                profileid = Convert.ToInt32(Session["profileId"]);
            }
            if (Session["FProfileId"] != null)
            {
                Fprofileid = Convert.ToInt32(Session["FprofileId"]);
            }
            if (Session["Policyinclustions"] != null)
            {
                FPHomeContents.PolicyInclusions = Policyincllist;
            }
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
                FPHomeContents.PolicyInclusion = new List<usp_GetUnit_Result>();
                if (PcId != null && PcId.HasValue && PcId > 0)
                {
                    FPHomeContents.PolicyInclusion = policyinclusions;
                }
                FPHomeContents.PolicyInclusions = new List<SessionModel>();
                if (PcId != null && PcId > 0)
                {
                    policyid = PcId.ToString();
                    FPHomeContents.PolicyId = policyid;
                }
                bool policyinclusion = policyinclusions.Exists(p => p.Name == "Home Contents");
                if (policyinclusion == true && PcId != null && PcId.HasValue)
                {
                    unid = policyinclusions.Where(p => p.Name == "Home Contents").Select(p => p.UnId).FirstOrDefault();
                    profileid = policyinclusions.Where(p => p.Name == "Home Contents").Select(p => p.ProfileUnId).FirstOrDefault();
                    HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                    var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    }
                }
                else
                {
                    return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                if (PcId == null && Session["unId"] == null && Session["profileId"] == null)
                {
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Home Contents&SectionUnId=&ProfileUnId=-1");
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails != null && unitdetails.SectionData != null)
                        {
                            Session["unId"] = unitdetails.SectionData.UnId;
                            Session["FprofileId"] = unitdetails.SectionData.ProfileUnId;
                            Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                            if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Home Contents"))
                            {
                                var policyhomelist = Policyincllist.FindAll(p => p.name == "Home Contents").ToList();
                                if (policyhomelist != null && policyhomelist.Count() > 0)
                                {
                                    Policyincllist.FindAll(p => p.name == "Home Contents").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;
                                    Policyincllist.FindAll(p => p.name == "Home Contents").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                                else
                                {
                                    Policyincllist.FindAll(p => p.name == "Home Contents").First().UnitId = unitdetails.SectionData.UnId;
                                    Policyincllist.FindAll(p => p.name == "Home Contents").First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (PcId == null && Session["unId"] != null && (Session["profileId"] != null || (Fprofileid != null && Fprofileid < 0)))
                    {
                        if (profileid == null || profileid == 0)
                        {
                            profileid = Fprofileid;
                        }
                        HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                        var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                        if (EmpResponse != null)
                        {
                            unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                            if (unitdetails != null && unitdetails.SectionData != null)
                            {
                                Session["unId"] = unitdetails.SectionData.UnId;
                                Session["FprofileId"] = unitdetails.SectionData.ProfileUnId;
                            }
                        }
                    }
                }
            }
            if (unitdetails != null)
            {
                if (unitdetails.SectionData != null && unitdetails.SectionData.ValueData!=null)
                {
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPHomeContents.CoveroptionFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPHomeContents.CoveroptionFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPHomeContents.CoveroptionFPObj.Coveroption = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPHomeContents.UnspecifiedFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPHomeContents.UnspecifiedFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPHomeContents.UnspecifiedFPObj.Unspecified = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPHomeContents.DescriptionFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPHomeContents.DescriptionFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPHomeContents.DescriptionFPObj.Description = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPHomeContents.ExcessFPHContentObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPHomeContents.ExcessFPHContentObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPHomeContents.ExcessFPHContentObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPHomeContents.ClaimperiodFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPHomeContents.ClaimperiodFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPHomeContents.ClaimperiodFPObj.Claimfreeperiod = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPHomeContents.discountFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPHomeContents.discountFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPHomeContents.discountFPObj.discount = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPHomeContents.AgediscountObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPHomeContents.AgediscountObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPHomeContents.AgediscountObj.Agediscount = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPHomeContents.SumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPHomeContents.SumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPHomeContents.SumInsuredFPObj.SumInsured = val;
                    }
                }
            }
            if (cid != null && cid.HasValue)
            {
                FPHomeContents.CustomerId = cid.Value;
            }
            if (PcId != null && PcId.HasValue)
            {
                FPHomeContents.PcId = PcId;
            }
            Session["Controller"] = "FarmPolicyHomeContent";
            Session["ActionName"] = "HomeContents";
            return View(FPHomeContents);
        }

        [HttpPost]
        public ActionResult HomeContents(int? cid, FPHomeContents FPHomeContents)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> DescriptionListHomeContents = new List<SelectListItem>();
            DescriptionListHomeContents = commonModel.descriptionListFC();
            FPHomeContents.DescriptionFPObj.DescriptionList = DescriptionListHomeContents;
            if(FPHomeContents.CustomerId!=null && FPHomeContents.CustomerId>0)
            {
                cid = FPHomeContents.CustomerId;
            }
            Session["unId"] = null;
            Session["profileId"] = null;
            return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = FPHomeContents.CustomerId, PcId = FPHomeContents.PcId });
         }
    }
}