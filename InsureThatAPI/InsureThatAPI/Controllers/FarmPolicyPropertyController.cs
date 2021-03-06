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
    public class FarmPolicyPropertyController : Controller
    {
        // GET: FarmPolicyProperty
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FarmLocationDetails(int? cid, int? PcId)
        {
            List<SessionModel> PolicyInclustions = new List<SessionModel>();

            FarmLocationDetails FarmLocationDetails = new FarmLocationDetails();
            ViewBag.cid = cid;
            if (cid != null)
            {
                FarmLocationDetails.CustomerId = cid.Value;
            }
            var db = new MasterDataEntities();
            string policyid = null;

            return View(FarmLocationDetails);
        }
        [HttpPost]
        public ActionResult FarmLocationDetails(int? cid, FarmLocationDetails FarmLocationDetails)
        {
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                FarmLocationDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FarmLocationDetails.CustomerId;
            }
            if (cid.HasValue && cid > 0)
            {
                //if (Session["completionTrackPFP"] != null)
                //{
                //    Session["completionTrackPFP"] = Session["completionTrackPFP"];
                //    FarmLocationDetails.completionTrackPFP = Session["completionTrackPFP"].ToString();
                //    if (FarmLocationDetails.completionTrackPFP != null)
                //    {
                //        string Completionstring = string.Empty;
                //        char[] arr = FarmLocationDetails.completionTrackPFP.ToCharArray();
                //        for (int i = 0; i < arr.Length; i++)
                //        {
                //            char a = arr[i];
                //            if (i == 0)
                //            {
                //                a = '1';
                //            }
                //            Completionstring = Completionstring + a;
                //        }
                //        Session["completionTrackPFP"] = Completionstring;
                //        FarmLocationDetails.completionTrackPFP = Completionstring;
                //    }
                //}
                //else
                //{
                //    Session["completionTrackPFP"] = "1-0-0-0-0"; ;
                //    FarmLocationDetails.completionTrackPFP = Session["completionTrackPFP"].ToString();
                //}
                return RedirectToAction("FarmDetails", new { cid = cid });
            }
            return View(FarmLocationDetails);
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> FarmDetails(int? cid, int? PcId)
        {
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
                        if (Policyincllist.Exists(p => p.name == "Fixed Farm Property"))
                        {

                        }
                       
                        else  if (Policyincllist.Exists(p => p.name == "Mobile Farm Property"))
                        {
                            return RedirectToAction("FarmContents", "MobileFarm", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Burglary"))
                        {
                            return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid, PcId = PcId });
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
                        if (Policyincllist.Exists(p => p.name == "Fixed Farm Property"))
                        {
                            if (Session["unId"] == null && Session["profileId"] == null)
                            {
                                Session["unId"] = Policyincllist.Where(p => p.name == "Fixed Farm Property").Select(p => p.UnitId).First();
                                Session["profileId"] = Policyincllist.Where(p => p.name == "Fixed Farm Property").Select(p => p.ProfileId).First();
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
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            NewPolicyDetailsClass FarmDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> constructionTypeList = new List<SelectListItem>();
            constructionTypeList = FarmDetailsmodel.constructionType();
            FarmDetails FarmDetails = new FarmDetails();
            #region Farm location details
            FarmDetails.AddressObj = new AddressForFP();
            FarmDetails.LocationObj = new LocatioForFP();
            #endregion
            #region Farm Details
            FarmDetails.DescriptionFBObj = new DetailedDescription();
            FarmDetails.DescriptionFBObj.EiId = 62255;
            FarmDetails.YearconstructedFBObj = new YearConstructedFB();
            FarmDetails.YearconstructedFBObj.EiId = 62257;
            FarmDetails.ConstructionFBObj = new ConstructionFB();
            FarmDetails.ConstructionFBObj.ConstructionHCList = constructionTypeList;
            FarmDetails.ConstructionFBObj.EiId = 62259;
            FarmDetails.ContaincoolroomObj = new ContainCoolroomFB();
            FarmDetails.ContaincoolroomObj.EiId = 62261;
            FarmDetails.SuminsuredFBObj = new SumInsuredsFB();
            FarmDetails.SuminsuredFBObj.EiId = 62263;
            FarmDetails.UnrepaireddamageObj = new UnrepairedDamageFS();
            FarmDetails.UnrepaireddamageObj.EiId = 62309;
            FarmDetails.ExcessFBObj = new HarvestedCropsExcess();
            FarmDetails.ExcessFBObj.EiId = 62315;
            FarmDetails.TypeoffarmObj = new typeoffarmInFP();
            FarmDetails.TypeoffarmObj.EiId = 62005;
            FarmDetails.FarmsizeObj = new farmsizeInFP();
            FarmDetails.FarmsizeObj.EiId = 62007;
            #endregion
            #region Farm Structures
            List<SelectListItem> ExcessRateList = new List<SelectListItem>();
            ExcessRateList = FarmDetailsmodel.ExcessRateFS();
            FarmDetails.SublimitObj = new SubLimitFarmFencing();
            FarmDetails.SublimitObj.EiId = 62283;
            FarmDetails.TotalcoverObj = new TotalCoverFarmFencing();
            FarmDetails.TotalcoverObj.EiId = 62287;
            FarmDetails.OtherstructurefcObj = new OtherFarmStructuresFC();
            FarmDetails.OtherstructurefcObj.EiId = 62291;
            FarmDetails.RoofwallsObj = new RoofAndWallsFS();
            FarmDetails.RoofwallsObj.EiId = 62297;
            //FarmStructures.UnrepaireddamageObj = new UnrepairedDamageFS();
            //FarmStructures.UnrepaireddamageObj.EiId = 62309;
            FarmDetails.ExcesshcFSObj = new HarvestedCropsExcess();
            FarmDetails.ExcesshcFSObj.ExcessHCList = ExcessRateList;
            FarmDetails.ExcesshcFSObj.EiId = 62315;
            #endregion
            #region HarvestedCrops
            List<SelectListItem> ExcessRateLists = new List<SelectListItem>();
            ExcessRateLists = FarmDetailsmodel.excessRate();
            FarmDetails.SuminsuredhcObj = new HarvestedCropsSumInsured();
            FarmDetails.SuminsuredhcObj.EiId = 62329;
            FarmDetails.ExcesshcHCObj = new HarvestedCropsExcess();
            FarmDetails.ExcesshcHCObj.ExcessHCList = ExcessRateLists;
            FarmDetails.ExcesshcHCObj.EiId = 62331;
            #endregion
            #region Interested Parties
            FarmDetails.PartynameObj = new InterestedPartyName();
            FarmDetails.PartynameObj.EiId = 62345;
            FarmDetails.PartylocationObj = new InterestedPartyLocation();
            FarmDetails.PartylocationObj.EiId = 62347;
            FarmDetails.TotalsuminsuredObj = new InterestedTotalSumInsured();
            FarmDetails.TotalsuminsuredObj.EiId = 62349;
            #endregion
            if (cid != null)
            {
                ViewBag.cid = cid;
                FarmDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FarmDetails.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = 0;
            int profileid = 0;
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
                FarmDetails.PolicyInclusions = Policyincllist;
            }
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
                FarmDetails.PolicyInclusion = new List<usp_GetUnit_Result>();
                if (PcId != null && PcId.HasValue && PcId > 0)
                {
                    FarmDetails.PolicyInclusion = policyinclusions;
                }
                FarmDetails.PolicyInclusions = new List<SessionModel>();
                if (PcId != null && PcId > 0)
                {
                    policyid = PcId.ToString();
                    FarmDetails.PolicyId = policyid;
                }
                bool policyinclusion = policyinclusions.Exists(p => p.Name == "Fixed Farm Property");
                if (policyinclusion == true && PcId != null && PcId.HasValue)
                {

                    int sectionId = policyinclusions.Where(p => p.Name == "Fixed Farm Property" && p.UnitNumber == unid).Select(p => p.UnId).FirstOrDefault();
                    int? profileunid = policyinclusions.Where(p => p.Name == "Fixed Farm Property" && p.ProfileUnId == profileid).Select(p => p.ProfileUnId).FirstOrDefault();
                    HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                    var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    }
                }
            }
            else
            {
                if (PcId == null && Session["unId"] == null && Session["profileId"] == null)
                {
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Fixed Farm Property&SectionUnId=&ProfileUnId=");
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails != null && unitdetails.SectionData != null)
                        {
                            Session["unId"] = unitdetails.SectionData.UnId;
                            Session["FprofileId"] = unitdetails.SectionData.ProfileUnId;
                            Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                            if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Fixed Farm Property"))
                            {
                                var policyhomelist = Policyincllist.FindAll(p => p.name == "Fixed Farm Property").ToList();
                                if (policyhomelist != null && policyhomelist.Count() > 0)
                                {
                                    Policyincllist.FindAll(p => p.name == "Fixed Farm Property").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;

                                    Policyincllist.FindAll(p => p.name == "Fixed Farm Property").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                                else
                                {
                                    Policyincllist.FindAll(p => p.name == "Fixed Farm Property").First().UnitId = unitdetails.SectionData.UnId;

                                    Policyincllist.FindAll(p => p.name == "Fixed Farm Property").First().ProfileId = unitdetails.SectionData.ProfileUnId;
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
                if (unitdetails.SectionData.AddressData != null)
                {
                    FarmDetails.AddressObj = new AddressForFP();
                    FarmDetails.LocationObj = new LocatioForFP();
                    FarmDetails.AddressObj.Address = unitdetails.SectionData.AddressData.AddressLine1;
                    FarmDetails.LocationObj.Location = unitdetails.SectionData.AddressData.State+", "+ unitdetails.SectionData.AddressData.Suburb+", "+ unitdetails.SectionData.AddressData.Postcode;
                    //homebuilding.Postcode = unitdetails.AddressData.Select(p => p.Postcode).First();
                   // FarmDetails.Suburb = unitdetails.AddressData.Select(p => p.Suburb).First();
                    //for (int add = 0; add <= unitdetails.AddressData.Count(); add++)
                    //{
                    //    if(unitdetails.AddressData[add].Suburb=="LAMBTON")
                    //    {
                    //        homebuilding.Sub = "WA";
                    //    }
                    //  //  homebuilding.Sub = unitdetails.AddressData[add].Suburb;
                    //    homebuilding.LocationObj.Location = unitdetails.AddressData[add].AddressLine1;
                    //    homebuilding.Pincode = unitdetails.AddressData[add].Postcode;
                    //    break;
                    //}
                }
                if (unitdetails.SectionData != null && unitdetails.SectionData.ValueData!=null)
                {
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.DescriptionFBObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.DescriptionFBObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmDetails.DescriptionFBObj.Description = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmDetails.DescriptionFBObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var DescriptionFBList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.DescriptionFBObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < DescriptionFBList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62255;
                                vds.Element.ItId = DescriptionFBList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.DescriptionFBObj.EiId && p.Element.ItId == DescriptionFBList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmDetails.DescriptionFBObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.YearconstructedFBObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.YearconstructedFBObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmDetails.YearconstructedFBObj.Year = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmDetails.YearconstructedFBObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var YearconstructedFBList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.YearconstructedFBObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < YearconstructedFBList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62257;
                                vds.Element.ItId = YearconstructedFBList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.YearconstructedFBObj.EiId && p.Element.ItId == YearconstructedFBList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmDetails.YearconstructedFBObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.ConstructionFBObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.ConstructionFBObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmDetails.ConstructionFBObj.Construction = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmDetails.ConstructionFBObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var ConstructionFBList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.ConstructionFBObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < ConstructionFBList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62259;
                                vds.Element.ItId = ConstructionFBList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.ConstructionFBObj.EiId && p.Element.ItId == ConstructionFBList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmDetails.ConstructionFBObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.ContaincoolroomObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.ContaincoolroomObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null)
                        {
                            FarmDetails.ContaincoolroomObj.Coolroom = true;
                        }
                        else
                        {
                            FarmDetails.ContaincoolroomObj.Coolroom = false;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmDetails.ContaincoolroomObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var ContaincoolroomFBList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.ContaincoolroomObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < ContaincoolroomFBList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62261;
                                vds.Element.ItId = ContaincoolroomFBList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.ContaincoolroomObj.EiId && p.Element.ItId == ContaincoolroomFBList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmDetails.ContaincoolroomFBObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.SuminsuredFBObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.SuminsuredFBObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmDetails.SuminsuredFBObj.Suminsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmDetails.SuminsuredFBObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var SuminsuredFBList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.SuminsuredFBObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < SuminsuredFBList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62263;
                                vds.Element.ItId = SuminsuredFBList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.SuminsuredFBObj.EiId && p.Element.ItId == SuminsuredFBList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmDetails.SuminsuredFBObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.ExcessFBObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.ExcessFBObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.ExcessFBObj.Excess = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.FarmsizeObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.FarmsizeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.FarmsizeObj.Farmsize = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.TypeoffarmObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.TypeoffarmObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.TypeoffarmObj.typeoffarm = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.ExcesshcFSObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.ExcesshcFSObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.ExcesshcFSObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.ExcesshcHCObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.ExcesshcHCObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.ExcesshcHCObj.Excess = val;
                    }
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.ExcesshcObjH.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.ExcesshcObjH.EiId).Select(p => p.Value).FirstOrDefault();
                    //    FarmDetails.ExcesshcObjH.Excess = val;
                    //}
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.OtherstructurefcObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.OtherstructurefcObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.OtherstructurefcObj.Otherstructure = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.PartynameObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.PartynameObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmDetails.PartynameObj.Name = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmDetails.PartynameObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var PartynameFBList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.PartynameObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < PartynameFBList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62345;
                                vds.Element.ItId = PartynameFBList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.PartynameObj.EiId && p.Element.ItId == PartynameFBList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmDetails.PartynameObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.PartylocationObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.PartylocationObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmDetails.PartylocationObj.Location = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmDetails.PartylocationObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var PartylocationFBList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.PartylocationObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < PartylocationFBList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62347;
                                vds.Element.ItId = PartylocationFBList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.PartylocationObj.EiId && p.Element.ItId == PartylocationFBList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmDetails.PartylocationObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.RoofwallsObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.RoofwallsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.RoofwallsObj.Roofwalls = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.SublimitObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.SublimitObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.SublimitObj.Sublimit = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.SuminsuredhcObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.SuminsuredhcObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.SuminsuredhcObj.Suminsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.TotalcoverObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.TotalcoverObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.TotalcoverObj.Totalcover = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmDetails.UnrepaireddamageObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmDetails.UnrepaireddamageObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.UnrepaireddamageObj.Unrepaireddamage = val;
                    }
                }
            }
            if (cid != null && cid.HasValue && cid>0)
            {
                FarmDetails.CustomerId = cid.Value;
            }
            if (PcId != null && PcId > 0)
            {
                FarmDetails.PcId = PcId;
            }
            Session["Controller"] = "FarmPolicyProperty";
            Session["ActionName"] = "FarmDetails";
            return View(FarmDetails);
        }
        [HttpPost]
        public ActionResult FarmDetails(int? cid, FarmDetails FarmDetails)
        {
            NewPolicyDetailsClass FarmDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> constructionTypeList = new List<SelectListItem>();
            constructionTypeList = FarmDetailsmodel.constructionType();
            FarmDetails.ConstructionFBObj.ConstructionHCList = constructionTypeList;
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                FarmDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FarmDetails.CustomerId;
            }
            string policyid = null;
            Session["unId"] = null;
            Session["profileId"] = null;
            Session["FarmFxd"] = 1;

            return RedirectToAction("FarmContents", "MobileFarm", new { cid = FarmDetails.CustomerId, PcId = FarmDetails.PcId });

            return View(FarmDetails);
        }

    }
}