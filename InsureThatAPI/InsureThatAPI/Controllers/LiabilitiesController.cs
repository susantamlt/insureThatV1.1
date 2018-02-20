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
    public class LiabilitiesController : Controller
    {
        // GET: Liabilities
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> LiabilityCover(int? cid, int? PcId)
        {
            LiabilityCover LiabilityCover = new LiabilityCover();
            if (cid.HasValue && cid > 0)
            {
                ViewBag.cid = cid;
                LiabilityCover.CustomerId = cid.Value;
            }
            string apikey = "";
            if (Session["ApiKey"] != null)
            {
                apikey = Session["ApiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
            if (Session["Policyinclustions"] != null)
            {
                List<SessionModel> PolicyInclustions = new List<SessionModel>();
            
                LiabilityCover.PolicyInclusions = new List<SessionModel>();
                LiabilityCover.PolicyInclusions = Policyincllist;
                if (Policyincllist != null)
                {
                    //var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                    // var Suburb = new List<KeyValuePair<string, string>>();
                    // List<SelectListItem> listItems = new List<SelectListItem>();               

                    if (Policyincllist != null)
                    {
                        if (Policyincllist.Exists(p => p.name == "Liability"))
                        {

                        }
                        else
                        {
                        if (Policyincllist.Exists(p => p.name == "Motor"))
                            {
                                return RedirectToAction("VehicleDescription", "MotorCover", new { cid = cid });
                            }
                            else if (Policyincllist.Exists(p => p.name == "Boat"))
                            {
                                return RedirectToAction("BoatDetails", "Boat", new { cid = cid });
                            }

                            else if (Policyincllist.Exists(p => p.name == "Pet"))
                            {
                                return RedirectToAction("PetsCover", "Pets", new { cid = cid });
                            }
                            else if (Policyincllist.Exists(p => p.name == "Travel"))
                            {
                                return RedirectToAction("TravelCover", "Travel", new { cid = cid });
                            }

                            if (Policyincllist.Exists(p => p.name == "Liability"))
                            {
                                if (Session["unId"] == null && Session["profileId"] == null)
                                {
                                    Session["unId"] = Policyincllist.Where(p => p.name == "Liability").Select(p => p.UnitId).First();
                                    Session["profileId"] = Policyincllist.Where(p => p.name == "Liability").Select(p => p.ProfileId).First();
                                }
                            }
                            else
                            {
                                return RedirectToAction("DisclosureDetails", "Disclosure", new { cid = cid, PcId = PcId });
                            }
                        }
                    }
                }
            }
            //else
            //{
            //    RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid,type=1 });
            //}
            var db = new MasterDataEntities();
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            unitdetails.AddressData = new List<AddressData>();
            var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
            LiabilityCover.FarmliabiltyObj = new FarmLiabiltys();
            LiabilityCover.FarmliabiltyObj.EiId = 60691;
            LiabilityCover.LimitindemnityObj = new LimitofIndemnity();
            LiabilityCover.LimitindemnityObj.EiId = 60671;
            LiabilityCover.ExcessLCObj = new ExcessLC();
            LiabilityCover.ExcessLCObj.EiId = 60681;
            List<SelectListItem> ExcList = new List<SelectListItem>();
            ExcList.Add(new SelectListItem { Text = "$250", Value = "1" });
            LiabilityCover.ExcessLCObj.ExcessList = ExcList;

            string policyid = null;
            bool policyinclusion = policyinclusions.Exists(p => p.Name == "Liability");
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (policyinclusion == true && PcId != null && PcId.HasValue)
            {
                LiabilityCover.PolicyInclusion = policyinclusions;
                LiabilityCover.ExistingPolicyInclustions = policyinclusions;
                int unid = Convert.ToInt32(Session["unId"]);
                int profileid = Convert.ToInt32(Session["profileId"]);
                //int sectionId = policyinclusions.Where(p => p.Name == "Home Contents" && p.UnitNumber == unid).Select(p => p.UnId).FirstOrDefault();
                //int? profileunid = policyinclusions.Where(p => p.Name == "Home Contents" && p.ProfileUnId == profileid).Select(p => p.ProfileUnId).FirstOrDefault();
                HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                if (EmpResponse != null)
                {
                    unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                }
            }
            else
            {
                int HprofileId = 0;
                int unid = 0;
                int profileid = 0;
                if (Session["unId"] != null && Session["profileId"] != null)
                {
                    unid = Convert.ToInt32(Session["unId"]);
                    profileid = Convert.ToInt32(Session["profileId"]);
                }
                if (Session["HProfileId"] != null)
                {
                    HprofileId = Convert.ToInt32(Session["HprofileId"]);
                }
                if (HprofileId != null && HprofileId < 0 && unid!=null && unid<0)
                {
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
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

                else
                {
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Liability&SectionUnId=&ProfileUnId="+HprofileId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails != null && unitdetails.SectionData != null)
                        {
                            Session["unId"] = unitdetails.SectionData.UnId;
                            Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                            if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Liability"))
                            {
                                var policyhomelist = Policyincllist.FindAll(p => p.name == "Liability").ToList();
                                if (policyhomelist != null && policyhomelist.Count() > 0)
                                {
                                    if (Policyincllist.FindAll(p => p.name == "Liability").Exists(p => p.UnitId == null))
                                    {
                                        Policyincllist.FindAll(p => p.name == "Liability").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;
                                    }
                                    if (Policyincllist.FindAll(p => p.name == "Liability").Exists(p => p.ProfileId == null))
                                    {
                                        Policyincllist.FindAll(p => p.name == "Liability").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                    }
                                }
                                else
                                {
                                    Policyincllist.FindAll(p => p.name == "Liability").First().UnitId = unitdetails.SectionData.UnId;
                                    Policyincllist.FindAll(p => p.name == "Liability").First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                                LiabilityCover.PolicyInclusions = Policyincllist;
                                Session["Policyinclustions"] = Policyincllist;
                            }
                        }
                    }
                }
            }
            if (unitdetails != null)
            {
                if (unitdetails.ProfileData != null)
                {
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == LiabilityCover.ExcessLCObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == LiabilityCover.ExcessLCObj.EiId).Select(p => p.Value).FirstOrDefault();
                        LiabilityCover.ExcessLCObj.Excess = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == LiabilityCover.FarmliabiltyObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == LiabilityCover.FarmliabiltyObj.EiId).Select(p => p.Value).FirstOrDefault();
                        LiabilityCover.FarmliabiltyObj.Farmliabilty = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == LiabilityCover.LimitindemnityObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == LiabilityCover.LimitindemnityObj.EiId).Select(p => p.Value).FirstOrDefault();
                        LiabilityCover.LimitindemnityObj.Limitindemnity = Convert.ToInt32(val);
                    }
                    else
                    {
                        //if (Session["CoverAmount"]!=null)
                        //{
                            LiabilityCover.LimitindemnityObj.Limitindemnity =3000000;
                        //}
                    }
                }
            }
            //var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Liability), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
            //if (details != null && details.Any())
            //{
            //    if (details.Exists(q => q.QuestionId == LiabilityCover.FarmliabiltyObj.EiId))
            //    {
            //        LiabilityCover.FarmliabiltyObj.Farmliabilty = Convert.ToString(details.Where(q => q.QuestionId == LiabilityCover.FarmliabiltyObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == LiabilityCover.LimitindemnityObj.EiId))
            //    {
            //        LiabilityCover.LimitindemnityObj.Limitindemnity = Convert.ToString(details.Where(q => q.QuestionId == LiabilityCover.LimitindemnityObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == LiabilityCover.ExcessLCObj.EiId))
            //    {
            //        var loc = details.Where(q => q.QuestionId == LiabilityCover.ExcessLCObj.EiId).FirstOrDefault();
            //        LiabilityCover.ExcessLCObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
            //    }
            //}
            if (unitdetails.ReferralList != null)
            {
                LiabilityCover.ReferralList = unitdetails.ReferralList;
                LiabilityCover.ReferralList.Replace("&nbsp;&nbsp;&nbsp;&nbsp", "");
                LiabilityCover.Referels = new List<string>();
                string[] delim = { "<br/>" };

                string[] spltd = LiabilityCover.ReferralList.Split(delim, StringSplitOptions.None);
                if (spltd != null && spltd.Count() > 0)
                {
                    for (int i = 0; i < spltd.Count(); i++)
                    {
                        LiabilityCover.Referels.Add(spltd[i].Replace("&nbsp;&nbsp;&nbsp;&nbsp", " "));
                    }
                }
            }
            return View(LiabilityCover);
        }
        [HttpPost]
        public ActionResult LiabilityCover(LiabilityCover LiabilityCover, int? cid)
        {
            List<SelectListItem> ExcList = new List<SelectListItem>();
            ExcList.Add(new SelectListItem { Text = "$250", Value = "1" });
            LiabilityCover.ExcessLCObj.ExcessList = ExcList;

            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                //if (LiabilityCover.FarmliabiltyObj != null && LiabilityCover.FarmliabiltyObj.EiId > 0 && LiabilityCover.FarmliabiltyObj.Farmliabilty != null)
                //{
                //    db.IT_InsertCustomerQnsData(LiabilityCover.CustomerId, Convert.ToInt32(RLSSection.Liability), LiabilityCover.FarmliabiltyObj.EiId, LiabilityCover.FarmliabiltyObj.Farmliabilty.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (LiabilityCover.LimitindemnityObj != null && LiabilityCover.LimitindemnityObj.EiId > 0 && LiabilityCover.LimitindemnityObj.Limitindemnity != null)
                //{
                //    db.IT_InsertCustomerQnsData(LiabilityCover.CustomerId, Convert.ToInt32(RLSSection.Liability), LiabilityCover.LimitindemnityObj.EiId, LiabilityCover.LimitindemnityObj.Limitindemnity.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (LiabilityCover.ExcessLCObj != null && LiabilityCover.ExcessLCObj.EiId > 0 && LiabilityCover.ExcessLCObj.Excess != null)
                //{
                //    db.IT_InsertCustomerQnsData(LiabilityCover.CustomerId, Convert.ToInt32(RLSSection.Liability), LiabilityCover.ExcessLCObj.EiId, LiabilityCover.ExcessLCObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
            }
            Session["profileId"] = null;
            Session["UnId"] = null;
            return RedirectToAction("VehicleDescription", "MotorCover", new { cid = LiabilityCover.CustomerId });
        }
    }

}