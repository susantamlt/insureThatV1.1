﻿using System;
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
            CommonUseFunctionClass cmn = new CommonUseFunctionClass();
            LiabilityCover.NewSections = new List<string>();
            if (Session["Policyinclustions"] != null)
            {
                List<SessionModel> PolicyInclustions = new List<SessionModel>();
            
                LiabilityCover.PolicyInclusions = new List<SessionModel>();
                LiabilityCover.PolicyInclusions = Policyincllist;
                LiabilityCover.NewSections = cmn.NewSectionHome(LiabilityCover.PolicyInclusions);
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
                        if (Policyincllist.Exists(p => p.name == "Motor" || p.name == "Motors"))
                            {
                                return RedirectToAction("VehicleDescription", "MotorCover", new { cid = cid });
                            }
                            else if (Policyincllist.Exists(p => p.name == "Boat"))
                            {
                                return RedirectToAction("BoatDetails", "Boat", new { cid = cid });
                            }

                            else if (Policyincllist.Exists(p => p.name == "Pet" || p.name == "Pets"))
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
            LiabilityCover.FarmingactivitiesObj = new Farmingactivities();
            LiabilityCover.FarmingactivitiesObj.EiId = 60673;
            LiabilityCover.FarmProduceObj = new ProductsCoveredFP();
            LiabilityCover.FarmProduceObj.EiId = 60675;
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
            if (PcId != null && PcId.HasValue)
            {
                int? unid = null;
                int? profileid = null;
                if (Session["unId"] != null && Session["profileId"] != null)
                {
                    unid = Convert.ToInt32(Session["unId"]);
                    profileid = Convert.ToInt32(Session["profileId"]);
                }
                else
                {
                    if (policyinclusions.Exists(p => p.Name == "Liability"))
                    {
                        unid = policyinclusions.Where(p => p.Name == "Liability").Select(p => p.UnId).FirstOrDefault();
                        profileid = policyinclusions.Where(p => p.Name == "Liability").Select(p => p.UnId).FirstOrDefault();

                    }
                    else
                    {
                        return RedirectToAction("VehicleDescription", "MotorCover", new { cid = cid , PcId = PcId });
                    }
                }
                LiabilityCover.NewSections = cmn.NewSectionP(policyinclusions);
                if (unid == null || unid == 0)
                {
                    unid = unitdetails.SectionData.UnId;
                    profileid = unitdetails.SectionData.ProfileUnId;
                }
                LiabilityCover.PolicyInclusion = policyinclusions;
                LiabilityCover.ExistingPolicyInclustions = policyinclusions;
             
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
                if (unitdetails.SectionData != null && unitdetails.SectionData.ValueData!=null)
                {
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == LiabilityCover.ExcessLCObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == LiabilityCover.ExcessLCObj.EiId).Select(p => p.Value).FirstOrDefault();
                        LiabilityCover.ExcessLCObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == LiabilityCover.FarmliabiltyObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == LiabilityCover.FarmliabiltyObj.EiId).Select(p => p.Value).FirstOrDefault();
                        LiabilityCover.FarmliabiltyObj.Farmliabilty = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == LiabilityCover.LimitindemnityObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == LiabilityCover.LimitindemnityObj.EiId).Select(p => p.Value).FirstOrDefault();
                        LiabilityCover.LimitindemnityObj.Limitindemnity = Convert.ToInt32(val);
                    }
                    else
                    {
                        //if (Session["CoverAmount"]!=null)
                        //{
                            LiabilityCover.LimitindemnityObj.Limitindemnity =3000000;
                        //}
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == LiabilityCover.FarmingactivitiesObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == LiabilityCover.FarmingactivitiesObj.EiId).Select(p => p.Value).FirstOrDefault();
                        LiabilityCover.FarmingactivitiesObj.Activities = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == LiabilityCover.FarmProduceObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == LiabilityCover.FarmProduceObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val == "1")
                        {
                            LiabilityCover.FarmProduceObj.Farmproduce = true;
                            LiabilityCover.allproducts = "1";
                        }
                        else if (val == "2")
                        {
                            LiabilityCover.FarmProduceObj.Farmproduce = true;
                            LiabilityCover.allproducts = "2";
                        }
                        else if (val == "1,2")
                        {
                            LiabilityCover.FarmProduceObj.Farmproduce = true;
                            LiabilityCover.allproducts = "1,2";
                        }
                        else if (val == "0")
                        {
                            LiabilityCover.FarmProduceObj.Farmproduce = false;
                            LiabilityCover.allproducts = "0";
                        }
                    }
                }
            }       
            if (unitdetails!=null &&  unitdetails.ReferralList != null)
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
            if(cid!=null && cid.HasValue)
            {
                LiabilityCover.CustomerId = cid.Value;

            }
            if(PcId!=null && PcId.HasValue)
            {
                LiabilityCover.PcId = PcId;
            }
            Session["Controller"] = "Liabilities";
            Session["ActionName"] = "LiabilityCover";
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
            Session["profileId"] = null;
            Session["UnId"] = null;
            string actionname = null;
            string controllername = null;
            if (Session["Actname"] != null)
            {
                actionname = Session["Actname"].ToString();
            }
            if (Session["controller"] != null)
            {
                controllername = Session["controller"].ToString();
            }
            //if (actionname != null && controllername != null)
            //{
            //    return RedirectToAction(actionname, controllername, new { cid = LiabilityCover.CustomerId, PcId = LiabilityCover.PcId });
            //}
            return RedirectToAction("VehicleDescription", "MotorCover", new { cid = LiabilityCover.CustomerId, PcId = LiabilityCover.PcId });
        }
    }

}