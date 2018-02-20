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
                        else
                        {
                            if (Policyincllist.Exists(p => p.name == "Mobile Farm Property"))
                            {
                                return RedirectToAction("FarmContents", "MobileFarm", new { cid = cid, PcId = PcId });
                            }
                            else if (Policyincllist.Exists(p => p.name == "Farm Interuption"))
                            {
                                return RedirectToAction("FarmInterruption", "FarmPolicyFarmInterruption", new { cid = cid, PcId = PcId });
                            }
                            else if (Policyincllist.Exists(p => p.name == "Farm Liability"))
                            {
                                return RedirectToAction("FarmLiability", "FarmPolicyFarmLiability", new { cid = cid, PcId = PcId });
                            }
                            else if (Policyincllist.Exists(p => p.name == "Burglary"))
                            {
                                return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid, PcId = PcId });
                            }
                            else if (Policyincllist.Exists(p => p.name == "Electronics"))
                            {
                                return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid, PcId = PcId });
                            }
                            else if (Policyincllist.Exists(p => p.name == "Money"))
                            {
                                return RedirectToAction("Money", "FarmPolicyMoney", new { cid = cid, PcId = PcId });
                            }
                            else if (Policyincllist.Exists(p => p.name == "Transit"))
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
            FarmDetails.ExcesshcObj = new HarvestedCropsExcess();
            FarmDetails.ExcesshcObj.ExcessHCList = ExcessRateList;
            FarmDetails.ExcesshcObj.EiId = 62315;
            #endregion
            #region HarvestedCrops
            List<SelectListItem> ExcessRateLists = new List<SelectListItem>();
            ExcessRateLists = FarmDetailsmodel.excessRate();

            FarmDetails.SuminsuredhcObj = new HarvestedCropsSumInsured();
            FarmDetails.SuminsuredhcObj.EiId = 62329;
            FarmDetails.ExcesshcObj = new HarvestedCropsExcess();
            FarmDetails.ExcesshcObj.ExcessHCList = ExcessRateLists;
            FarmDetails.ExcesshcObj.EiId = 62331;
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
                if (unitdetails.ProfileData != null)
                {
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.ConstructionFBObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.ConstructionFBObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.ConstructionFBObj.Construction = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.ContaincoolroomObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.ContaincoolroomObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.ContaincoolroomObj.Coolroom = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.DescriptionFBObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.DescriptionFBObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.DescriptionFBObj.Description = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.ExcesshcObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.ExcesshcObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.ExcesshcObj.Excess = val;
                    }
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.ExcesshcObjH.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.ExcesshcObjH.EiId).Select(p => p.Value).FirstOrDefault();
                    //    FarmDetails.ExcesshcObjH.Excess = val;
                    //}
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.OtherstructurefcObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.OtherstructurefcObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.OtherstructurefcObj.Otherstructure = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.PartylocationObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.PartylocationObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.PartylocationObj.Location = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.PartynameObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.PartynameObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.PartynameObj.Name = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.RoofwallsObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.RoofwallsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.RoofwallsObj.Roofwalls = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.SublimitObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.SublimitObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.SublimitObj.Sublimit = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.SuminsuredFBObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.SuminsuredFBObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.SuminsuredFBObj.Suminsured = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.SuminsuredFBObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.SuminsuredFBObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.SuminsuredFBObj.Suminsured = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.SuminsuredFBObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.SuminsuredFBObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.SuminsuredFBObj.Suminsured = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.SuminsuredhcObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.SuminsuredhcObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.SuminsuredhcObj.Suminsured = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.TotalcoverObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.TotalcoverObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.TotalcoverObj.Totalcover = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.UnrepaireddamageObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.UnrepaireddamageObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.UnrepaireddamageObj.Unrepaireddamage = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmDetails.YearconstructedFBObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmDetails.YearconstructedFBObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmDetails.YearconstructedFBObj.Year = val;
                    }
                }
            }
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

            return RedirectToAction("FarmContents", "MobileFarm", new { cid = FarmDetails.CustomerId, PcId = FarmDetails.PolicyId });

            return View(FarmDetails);
        }

    }
}