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
    public class FarmPolicyHomeController : Controller
    {
        // GET: FarmPolicyHome
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> MainDetails(int? cid, int? PcId)
        {
            NewPolicyDetailsClass commonmethods = new NewPolicyDetailsClass();
            List<SelectListItem> ExternalWallsMadeList = new List<SelectListItem>();
            ExternalWallsMadeList = commonmethods.ExternalWallsMadeList();
            List<SelectListItem> IsRoofMadeOfList = new List<SelectListItem>();
            IsRoofMadeOfList = commonmethods.RoofMadesList();
            HB2HomeDescription HB2HomeDescription = new HB2HomeDescription();
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                HB2HomeDescription.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = HB2HomeDescription.CustomerId;
            }
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
            string apikey = null;
            bool policyinclusion = policyinclusions.Exists(p => p.Name == "Money");
            if (Session["apiKey"] != null)
            {
                apikey = Session["apiKey"].ToString();
                HB2HomeDescription.ApiKey = Session["apiKey"].ToString();
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
                HB2HomeDescription.PolicyId = policyid;
            }
            else if (Session["Policyinclustions"] != null)
            {
                #region Policy Selected or not testing

                HB2HomeDescription.PolicyInclusions = new List<SessionModel>();
                var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                HB2HomeDescription.PolicyInclusions = Policyincllist;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Exists(p => p.name == "Home Buildings"))
                    {
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
                        if (Policyincllist.Exists(p => p.name == "Home Buildings"))
                        {
                            if (Session["unId"] == null && Session["profileId"] == null)
                            {
                                Session["unId"] = Policyincllist.Where(p => p.name == "Home Buildings").Select(p => p.UnitId).First();
                                Session["profileId"] = Policyincllist.Where(p => p.name == "Home Buildings").Select(p => p.ProfileId).First();
                            }
                        }
                        else
                        {
                            return RedirectToAction("DisclosureDetails", "Disclosure", new { cid = cid, PcId = PcId });
                        }
                  
                }
                #endregion
            }
            #region homeDescription
            HB2HomeDescription.LocationObj = new Locations();
            HB2HomeDescription.LocationObj.EiId = 60133;
            HB2HomeDescription.LocationObj.Location = "";
            HB2HomeDescription.IsbuildinglocatedObj = new IsBuildingLocateds();
            HB2HomeDescription.IsbuildinglocatedObj.EiId = 62067;
            HB2HomeDescription.DescribeaddressObj = new DescribeAddresses();
            HB2HomeDescription.DescribeaddressObj.EiId = 0;
            HB2HomeDescription.locatedfarmObj = new locatedOnAFarm();
            HB2HomeDescription.locatedfarmObj.EiId = 62069;
            HB2HomeDescription.TypeOfBuildingObj = new TypeOfBuilding();
            HB2HomeDescription.TypeOfBuildingObj.EiId = 62081;
            HB2HomeDescription.DescribeBuildingObj = new DescribeBuilding();
            HB2HomeDescription.DescribeBuildingObj.EiId = 62083;
            #endregion
            #region Constrution Details         
            HB2HomeDescription.ExtwallsmadeObj = new ExtWallsMades();
            HB2HomeDescription.ExtwallsmadeObj.ExtwallsmadeList = ExternalWallsMadeList;
            HB2HomeDescription.ExtwallsmadeObj.EiId = 62085;
            HB2HomeDescription.DescribeexternalwallsObj = new Describeexternalwalls();
            HB2HomeDescription.DescribeexternalwallsObj.EiId = 62087;
            HB2HomeDescription.RoofmadeObj = new RoofMades();
            HB2HomeDescription.RoofmadeObj.RoofmadeList = IsRoofMadeOfList;
            HB2HomeDescription.RoofmadeObj.EiId = 62089;
            HB2HomeDescription.DescribeRoofMadeOffObj = new DescribeRoofMadeof();
            HB2HomeDescription.DescribeRoofMadeOffObj.EiId = 62091;
            HB2HomeDescription.YearofBuiltObj = new YearOfBuilt();
            HB2HomeDescription.YearofBuiltObj.EiId = 62093;
            HB2HomeDescription.LastRewiredObj = new LastRewired();
            HB2HomeDescription.LastRewiredObj.EiId = 62095;
            HB2HomeDescription.LastReplumbedObj = new LastReplumbed();
            HB2HomeDescription.LastReplumbedObj.EiId = 62097;
            HB2HomeDescription.WatertightObj = new Watertights();
            HB2HomeDescription.WatertightObj.EiId = 62099;
            HB2HomeDescription.HeritagelegislationObj = new HeritageLegislations();
            HB2HomeDescription.HeritagelegislationObj.EiId = 62109;
            HB2HomeDescription.UnderconstructionObj = new UnderConstructions();
            HB2HomeDescription.UnderconstructionObj.EiId = 62111;
            HB2HomeDescription.DomesticdwellingObj = new DomesticDwellings();
            HB2HomeDescription.DomesticdwellingObj.EiId = 62113;
            HB2HomeDescription.UnrepaireddamageObj = new UnrepairedDamage();
            HB2HomeDescription.UnrepaireddamageObj.EiId = 62115;

            HB2HomeDescription.RustDamageObj = new RustDamage();
            HB2HomeDescription.RustDamageObj.EiId = 62090;
            #endregion
            #region Occupancy Details
            HB2HomeDescription.WholivesObj = new WhoLives();
            HB2HomeDescription.WholivesObj.EiId = 62127;
            HB2HomeDescription.WholivesObj.Wholives = 0;
            HB2HomeDescription.IsbuildingObj = new IsBuildings();
            HB2HomeDescription.IsbuildingObj.EiId = 62129;
            HB2HomeDescription.IsbuildingObj.Isbuilding = 0;
            HB2HomeDescription.ConsecutivedayObj = new Consecutivedays();
            HB2HomeDescription.ConsecutivedayObj.EiId = 62131;
            HB2HomeDescription.ConsecutivedayObj.Consecutiveday = 0;
            HB2HomeDescription.IsusedbusinessObj = new IsusedBusinesses();
            HB2HomeDescription.IsusedbusinessObj.EiId = 62133;
            HB2HomeDescription.DescribebusinessObj = new DescribeBusinesses();
            HB2HomeDescription.DescribebusinessObj.EiId = 62135;
            #endregion
            #region Interested Parties
            HB2HomeDescription.LocationObjs = new Locations();
            HB2HomeDescription.LocationObjs.EiId = 62189;
            HB2HomeDescription.NameInstitutionsObj = new NameOfInstitutionsRls();
            HB2HomeDescription.NameInstitutionsObj.EiId = 62187;
            #endregion
            #region Home Building
            HB2HomeDescription.CoverhomebuildingObj = new CoverHomeBuildings();
            HB2HomeDescription.CoverhomebuildingObj.EiId = 63459;
            HB2HomeDescription.CostforRebuildingObj = new CostForRebuilding();
            HB2HomeDescription.CostforRebuildingObj.EiId = 63461;
            HB2HomeDescription.ClaimfreeperiodObj = new ClaimFreePeriods();
            HB2HomeDescription.ClaimfreeperiodObj.EiId = 63471;
            HB2HomeDescription.ExcessObj = new Excesses();
            HB2HomeDescription.ExcessObj.EiId = 63473;
            HB2HomeDescription.WindowCoveringsObj = new WindowCoverings();
            HB2HomeDescription.WindowCoveringsObj.EiId = 63465;
            HB2HomeDescription.ProtectionCoverObj = new MortgageeProtectionCover();
            HB2HomeDescription.ProtectionCoverObj.EiId = 63467;
            #endregion
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = Convert.ToInt32(Session["unId"]);
            int profileid = Convert.ToInt32(Session["profileId"]);
            if (policyinclusion == true && PcId != null && PcId.HasValue)
            {
                HB2HomeDescription.ExistingPolicyInclustions = policyinclusions;

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
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Home Buildings&SectionUnId=&ProfileUnId=0");
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
                if (unitdetails.ProfileData != null && unitdetails.ProfileData.ValueData != null)
                {
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.ExtwallsmadeObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.ExtwallsmadeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        HB2HomeDescription.ExtwallsmadeObj.Extwallsmade = val;
                    }
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.AddressObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.AddressObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    HB2HomeDescription.AddressObj.Address = val;
                    //}
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.AreapropertyObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.AreapropertyObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                    //    {
                    //        HB2HomeDescription.AreapropertyObj.Areaproperty = Convert.ToInt32(val);
                    //    }
                    //}
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.IsbuildinglocatedObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.IsbuildinglocatedObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.IsbuildinglocatedObj.Isbuildinglocated = Convert.ToInt32(val);
                        }
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.NameInstitutionsObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.NameInstitutionsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.NameInstitutionsObj.Name = val;
                        }
                        if (unitdetails.ProfileData.ValueData.Select(p => p.Element.ElId == HB2HomeDescription.NameInstitutionsObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var institutelist = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.NameInstitutionsObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < institutelist.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62187;
                                vds.Element.ItId = institutelist[i];
                                vds.Value = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.NameInstitutionsObj.EiId && p.Element.ItId == institutelist[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            HB2HomeDescription.NameInstitutionsObjList = elmnts;
                        }
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.LocationObjs.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.LocationObjs.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.LocationObjs.Location = val;
                        }
                        if (unitdetails.ProfileData.ValueData.Select(p => p.Element.ElId == HB2HomeDescription.LocationObjs.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmntsts = new List<ValueDatas>();
                            var institutelst = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.LocationObjs.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < institutelst.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62189;
                                vds.Element.ItId = institutelst[i];
                                vds.Value = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.LocationObjs.EiId && p.Element.ItId == institutelst[i]).Select(p => p.Value).FirstOrDefault();
                                elmntsts.Add(vds);
                            }
                            HB2HomeDescription.LocationObjsList = elmntsts;
                        }
                    }
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.AgediscountObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.AgediscountObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                    //    {
                    //        HB2HomeDescription.AgediscountObj.Agediscount = val;
                    //    }
                    //}

                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.ConsecutivedayObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.ConsecutivedayObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.ConsecutivedayObj.Consecutiveday = Convert.ToInt32(val);
                        }
                    }

                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.DescribeaddressObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.DescribeaddressObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.DescribeaddressObj.Describeaddress = Convert.ToInt32(val);
                        }
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.DescribebusinessObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.DescribebusinessObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.DescribebusinessObj.Describebusiness = val;
                        }
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.DescribeexternalwallsObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.DescribeexternalwallsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.DescribeexternalwallsObj.Describeexternalwall = val;
                        }
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.DescribeRoofMadeOffObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.DescribeRoofMadeOffObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.DescribeRoofMadeOffObj.DescribeRoofMade = val;
                        }
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.RoofmadeObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.RoofmadeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.RoofmadeObj.Roofmade = val;
                        }
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.RustDamageObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.RustDamageObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.RustDamageObj.RustDamages = Convert.ToInt32(val);
                        }
                        else
                        {
                            HB2HomeDescription.RustDamageObj.RustDamages = -1;
                        }
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.DomesticdwellingObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.DomesticdwellingObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            if (val == "1")
                            {
                                HB2HomeDescription.DomesticdwellingObj.Domesticdwelling = 1;
                            }
                            else if (val == "0")
                            {
                                HB2HomeDescription.DomesticdwellingObj.Domesticdwelling = 0;
                            }
                            else
                            {
                                HB2HomeDescription.DomesticdwellingObj.Domesticdwelling = -1;
                            }
                        }
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.LastRewiredObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.LastRewiredObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.LastRewiredObj.Rewired = val;
                        }
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.LastReplumbedObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.LastReplumbedObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.LastReplumbedObj.Replumbed = val;
                        }
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.ExtwallsmadeObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.ExtwallsmadeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.ExtwallsmadeObj.Extwallsmade = val;
                        }
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.HeritagelegislationObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.HeritagelegislationObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            if (val == "0")
                            {
                                HB2HomeDescription.HeritagelegislationObj.Heritagelegislation = 0;
                            }
                            else if (val == "1")
                            {
                                HB2HomeDescription.HeritagelegislationObj.Heritagelegislation = 1;
                            }
                            else
                            {
                                HB2HomeDescription.HeritagelegislationObj.Heritagelegislation = -1;
                            }
                        }
                    }


                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.ImposedObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.ImposedObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                    //    {
                    //        HB2HomeDescription.ImposedObj.Imposed = val;
                    //    }
                    //}
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.IsbuildinglocatedObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.IsbuildinglocatedObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                    //    {                            
                    //            HB2HomeDescription.IsbuildinglocatedObj.Isbuildinglocated = Convert.ToInt32(val);                          
                    //    }
                    //}
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.IsusedbusinessObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.IsusedbusinessObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.IsusedbusinessObj.Isusedbusiness = Convert.ToInt32(val);
                        }
                    }
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.NoclaimdiscountObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.NoclaimdiscountObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                    //    {
                    //        HB2HomeDescription.NoclaimdiscountObj.Noclaimdiscount = val;
                    //    }
                    //}
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.PropertytypeObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.PropertytypeObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                    //    {
                    //        HB2HomeDescription.PropertytypeObj.Propertytype = val;
                    //    }
                    //}
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.UnderconstructionObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.UnderconstructionObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            if (val == "0")
                            {
                                HB2HomeDescription.UnderconstructionObj.Underconstruction = 0;
                            }
                            else if (val == "1")
                            {
                                HB2HomeDescription.UnderconstructionObj.Underconstruction = 1;
                            }
                            else
                                HB2HomeDescription.UnderconstructionObj.Underconstruction = -1;
                        }
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.WatertightObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.WatertightObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            if (val == "0")
                            {
                                HB2HomeDescription.WatertightObj.Watertight = 0;
                            }
                            else if (val == "1")
                            {
                                HB2HomeDescription.WatertightObj.Watertight = 1;
                            }
                            else
                                HB2HomeDescription.WatertightObj.Watertight = -1;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.WholivesObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.WholivesObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "2" && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.WholivesObj.Wholives = Convert.ToInt32(val);
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.YearofBuiltObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.YearofBuiltObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.YearofBuiltObj.YearBuilt = val;
                        }
                    }
                    //if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription..EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.YearofBuiltObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                    //    {
                    //        HB2HomeDescription.YearofBuiltObj.YearBuilt = val;
                    //    }
                    //}
                }
                if (unitdetails.SectionData != null && unitdetails.SectionData.ValueData != null)
                {
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.ClaimfreeperiodObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.ClaimfreeperiodObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && (val == "0" || val == "1" || val == "2"))
                        {
                            HB2HomeDescription.ClaimfreeperiodObj.Claimfreeperiod = Convert.ToInt32(val);
                        }
                    }
                    else
                    {
                        HB2HomeDescription.ClaimfreeperiodObj.Claimfreeperiod = -1;

                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.CostforRebuildingObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.CostforRebuildingObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.CostforRebuildingObj.CostforRebuilding = Convert.ToInt32(val);
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == HB2HomeDescription.ExcessObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == HB2HomeDescription.ExcessObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && val != "0" && !string.IsNullOrEmpty(val))
                        {
                            HB2HomeDescription.ExcessObj.Excess = val;
                        }
                    }
                    if (unitdetails.SectionData.AddressData != null)
                    {
                        string address = unitdetails.SectionData.AddressData.AddressLine1 + ", " + unitdetails.SectionData.AddressData.Suburb + ", " + unitdetails.SectionData.AddressData.State + ", " + unitdetails.SectionData.AddressData.Postcode;
                        HB2HomeDescription.addressCom = address;
                    }
                }
            }
            return View(HB2HomeDescription);
        }
        [HttpPost]
        public ActionResult MainDetails(int? cid, HB2HomeDescription HB2HomeDescription)
        {
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                HB2HomeDescription.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = HB2HomeDescription.CustomerId;
            }
            string policyid = null;
     
            Session["unId"] = null;
            Session["profileId"] = null;
            return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = HB2HomeDescription.CustomerId, PcId = HB2HomeDescription.PcId });

        }
 
  
    }
}