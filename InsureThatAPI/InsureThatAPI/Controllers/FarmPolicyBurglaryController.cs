﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;//append model
using InsureThatAPI.CommonMethods;//append Common Methods
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace InsureThatAPI.Controllers
{
    public class FarmPolicyBurglaryController : Controller
    {
        // GET: FarmPolicyBurglary
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Burglary(int? cid, int? PcId)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> BurglaryExcessToPay = new List<SelectListItem>();
            BurglaryExcessToPay = commonModel.excessRate();
            FPBurglary FPBurglary = new FPBurglary();
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
            if (Session["Policyinclustions"] != null)
            {
                Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                if (Policyincllist != null)
                {
                    if (Policyincllist != null)
                    {
                        if (Policyincllist.Exists(p => p.name == "Burglary"))
                        {

                        }
                        else if (Policyincllist.Exists(p => p.name == "Farm Interuption"))
                            {
                                return RedirectToAction("FarmInterruption", "FarmPolicyFarmInterruption", new { cid = cid, PcId = PcId });
                            }
                            else if (Policyincllist.Exists(p => p.name == "Money"))
                            {
                                return RedirectToAction("Money", "FarmPolicyMoney", new { cid = cid, PcId = PcId });
                            }
                            else if (Policyincllist.Exists(p => p.name == "Farm Liability"))
                            {
                                return RedirectToAction("FarmLiability", "FarmPolicyFarmLiability", new { cid = cid, PcId = PcId });
                            }
                            else if (Policyincllist.Exists(p => p.name == "Machinery"))
                            {
                                return RedirectToAction("Machinery", "FarmPolicyMachinery", new { cid = cid, PcId = PcId });
                            }

                            else if (Policyincllist.Exists(p => p.name == "Electronics"))
                            {
                                return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid, PcId = PcId });
                            }

                            else if (Policyincllist.Exists(p => p.name == "Transit"))
                            {
                                return RedirectToAction("Transit", "FarmPolicyTransit", new { cid = cid, PcId = PcId });
                            }
                            else if (Policyincllist.Exists(p => p.name == "Livestock"))
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
                            if (Policyincllist.Exists(p => p.name == "Burglary"))
                            {
                                if (Session["unId"] == null && Session["profileId"] == null)
                                {
                                    Session["unId"] = Policyincllist.Where(p => p.name == "Burglary").Select(p => p.UnitId).First();
                                    Session["profileId"] = Policyincllist.Where(p => p.name == "Burglary").Select(p => p.ProfileId).First();
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
            ViewBag.cid = cid;
            if (cid != null)
            {
                FPBurglary.CustomerId = cid.Value;
            }
            FPBurglary.CoverYourPropOptionFPObj = new CoverYourPropOptionFP();
            FPBurglary.CoverYourPropOptionFPObj.EiId = 62581;

            FPBurglary.FarmBuildingExCoolRoomsFPObj = new FarmBuildingExcCoolRoomsFP();
            FPBurglary.FarmBuildingExCoolRoomsFPObj.EiId = 62585;

            FPBurglary.CoolRoomsFPObj = new CoolRoomsFP();
            FPBurglary.CoolRoomsFPObj.EiId = 62587;

            FPBurglary.OtherFarmStructuresFPObj = new OtherFarmStructuresFP();
            FPBurglary.OtherFarmStructuresFPObj.EiId = 62591;

            FPBurglary.FarmContentsFPObj = new FarmContentsFP();
            FPBurglary.FarmContentsFPObj.EiId = 62595;

            FPBurglary.HailNettingStoredFPObj = new HailNettingStoredFP();
            FPBurglary.HailNettingStoredFPObj.EiId = 62597;

            FPBurglary.UnspecifiedMachineryFPObj = new UnspecifiedMachineryFP();
            FPBurglary.UnspecifiedMachineryFPObj.EiId = 62609;

            FPBurglary.SpecifiedItemsOver5KFPObj = new SpecifiedItemsOver5KFP();
            FPBurglary.SpecifiedItemsOver5KFPObj.EiId = 62613;

            FPBurglary.ExcessFPBurglaryObj = new ExcessFPBurglary();
            FPBurglary.ExcessFPBurglaryObj.ExcessList = BurglaryExcessToPay;
            FPBurglary.ExcessFPBurglaryObj.EiId = 62615;

            FPBurglary.OptFPCoverTheftFSAndFCObj = new FPCoverTheftFSAndFC();
            FPBurglary.OptFPCoverTheftFSAndFCObj.EiId = 62619;
            FPBurglary.FarmFencingFPObj = new FarmFencingFP();
            FPBurglary.FarmFencingFPObj.EiId = 62589;

            FPBurglary.OptFPCoverTheftFarmMachineryObj = new FPCoverTheftFarmMachinery();
            FPBurglary.OptFPCoverTheftFarmMachineryObj.EiId = 62621;

            FPBurglary.OptFPPortalableItemsOptObj = new FPPortalableItemsOpt();
            FPBurglary.OptFPPortalableItemsOptObj.EiId = 62623;
            FPBurglary.AddressObj = new AddressAddress();
            FPBurglary.AddressObj.EiId = 0;

            var db = new MasterDataEntities();
            string policyid = null;
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPBurglary.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPBurglary.CustomerId;
            }
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = 0;
            int? profileid = 0;
            int Fprofileid = 0;
            if (Session["unId"] != null && Session["profileId"] != null)
            {
                unid = Convert.ToInt32(Session["unId"]);
                profileid = Convert.ToInt32(Session["profileId"]);
            }
            if (Session["FprofileId"] != null)
            {
                Fprofileid = Convert.ToInt32(Session["FprofileId"]);
            }
            if (Session["Policyinclustions"] != null)
            {
                FPBurglary.PolicyInclusions = Policyincllist;
            }
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
                FPBurglary.PolicyInclusion = new List<usp_GetUnit_Result>();
                if (PcId != null && PcId.HasValue && PcId > 0)
                {
                    FPBurglary.PolicyInclusion = policyinclusions;
                }
                FPBurglary.PolicyInclusions = new List<SessionModel>();
                if (PcId != null && PcId > 0)
                {
                    policyid = PcId.ToString();
                    FPBurglary.PolicyId = policyid;
                }
                bool policyinclusion = policyinclusions.Exists(p => p.Name == "Burglary");
                if (policyinclusion == true && PcId != null && PcId.HasValue)
                {
                    unid = policyinclusions.Where(p => p.Name == "Burglary").Select(p => p.UnId).FirstOrDefault();
                    profileid = policyinclusions.Where(p => p.Name == "Burglary").Select(p => p.ProfileUnId).FirstOrDefault();
                    HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                    var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    }
                }
                else
                {
                    return RedirectToAction("FarmInterruption", "FarmPolicyFarmInterruption", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                if (PcId == null && Session["unId"] == null && Session["profileId"] == null)
                {
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Burglary&SectionUnId=&ProfileUnId=" + Fprofileid);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails != null && unitdetails.SectionData != null)
                        {
                            Session["unId"] = unitdetails.SectionData.UnId;
                            Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                            if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Burglary"))
                            {
                                var policyhomelist = Policyincllist.FindAll(p => p.name == "Burglary").ToList();
                                if (policyhomelist != null && policyhomelist.Count() > 0)
                                {
                                    Policyincllist.FindAll(p => p.name == "Burglary").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;
                                    Policyincllist.FindAll(p => p.name == "Burglary").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                                else
                                {
                                    Policyincllist.FindAll(p => p.name == "Burglary").First().UnitId = unitdetails.SectionData.UnId;
                                    Policyincllist.FindAll(p => p.name == "Burglary").First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (PcId == null && Session["unId"] != null && (Session["profileId"] != null || (profileid != null && profileid < 0)))
                    {
                        HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
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
            }
            if (unitdetails != null)
            {
                if (unitdetails.SectionData != null && unitdetails.SectionData.ValueData!=null)
                {
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPBurglary.CoolRoomsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPBurglary.CoolRoomsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPBurglary.CoolRoomsFPObj.CoolRooms = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPBurglary.CoverYourPropOptionFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPBurglary.CoverYourPropOptionFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPBurglary.CoverYourPropOptionFPObj.Cover = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPBurglary.ExcessFPBurglaryObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPBurglary.ExcessFPBurglaryObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPBurglary.ExcessFPBurglaryObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPBurglary.FarmBuildingExCoolRoomsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPBurglary.FarmBuildingExCoolRoomsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPBurglary.FarmBuildingExCoolRoomsFPObj.FarmBuildingExcCoolRooms = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPBurglary.FarmContentsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPBurglary.FarmContentsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPBurglary.FarmContentsFPObj.FarmContents = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPBurglary.FarmFencingFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPBurglary.FarmFencingFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPBurglary.FarmFencingFPObj.FarmFencing = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPBurglary.HailNettingStoredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPBurglary.HailNettingStoredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPBurglary.HailNettingStoredFPObj.HailNettingStored = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPBurglary.OptFPCoverTheftFarmMachineryObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPBurglary.OptFPCoverTheftFarmMachineryObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPBurglary.OptFPCoverTheftFarmMachineryObj.CoverTheftFarmMachinery = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPBurglary.OptFPCoverTheftFSAndFCObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPBurglary.OptFPCoverTheftFSAndFCObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPBurglary.OptFPCoverTheftFSAndFCObj.CoverTheftFSandFC = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPBurglary.OptFPPortalableItemsOptObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPBurglary.OptFPPortalableItemsOptObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPBurglary.OptFPPortalableItemsOptObj.PortalbleItemsOpt = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPBurglary.OtherFarmStructuresFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPBurglary.OtherFarmStructuresFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPBurglary.OtherFarmStructuresFPObj.OtherFarmStructures = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPBurglary.SpecifiedItemsOver5KFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPBurglary.SpecifiedItemsOver5KFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPBurglary.SpecifiedItemsOver5KFPObj.SpecifiedItemOver5K = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPBurglary.UnspecifiedMachineryFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPBurglary.UnspecifiedMachineryFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPBurglary.UnspecifiedMachineryFPObj.UnspecifiedMachinery = val;
                    }
                   
                }
                if (unitdetails.SectionData != null && unitdetails.SectionData.AddressData != null)
                {
                    if (unitdetails.SectionData.AddressData != null)
                    {
                        FPBurglary.AddressObj.Address = unitdetails.SectionData.AddressData.AddressLine1 + ", " + unitdetails.SectionData.AddressData.Suburb + " ," + unitdetails.SectionData.AddressData.State + ", " + unitdetails.SectionData.AddressData.Postcode;                        
                    }
                }                
            }
            if (cid != null && cid.HasValue)
            {
                FPBurglary.CustomerId = cid.Value;
            }
            if (PcId != null && PcId.HasValue)
            {
                FPBurglary.PcId = PcId;
            }
            Session["Controller"] = "FarmPolicyBurglary";
            Session["ActionName"] = "Burglary";
            return View(FPBurglary);
        }

        [HttpPost]
        public ActionResult Burglary(int? cid, FPBurglary FPBurglary)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> BurglaryExcessToPay = new List<SelectListItem>();
            BurglaryExcessToPay = commonModel.excessRate();
            FPBurglary.ExcessFPBurglaryObj.ExcessList = BurglaryExcessToPay;
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPBurglary.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPBurglary.CustomerId;
            }
            string policyid = null;
            Session["unId"] = null;
            Session["profileId"] = null;
            return RedirectToAction("FarmInterruption", "FarmPolicyFarmInterruption", new { cid = FPBurglary.CustomerId, PcId = FPBurglary.PcId });
        }
    }
}