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
    public class FarmPolicyHomeController : Controller
    {
        // GET: FarmPolicyHome
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MainDetails(int? cid)
        {
            NewPolicyDetailsClass commonmethods = new NewPolicyDetailsClass();
            List<SelectListItem> ExternalWallsMadeList = new List<SelectListItem>();
            ExternalWallsMadeList = commonmethods.ExternalWallsMadeList();
            List<SelectListItem> IsRoofMadeOfList = new List<SelectListItem>();
            IsRoofMadeOfList = commonmethods.RoofMadesList();
            HB2HomeDescription HB2HomeDescription = new HB2HomeDescription();
            #region VehicleDescription
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
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Contains("HomeBuildingFarm"))
                    {
                      
                    }
                    else
                    {
                        if (Policyincllist.Contains("HomeContent"))
                        {
                            return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Machinery"))
                        {
                            //  return RedirectToAction("", "", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("MotorFarm"))
                        {
                            // return RedirectToAction("", "", new { cid = cid });
                        }
                    }
                }
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 2 });
            }
            ViewBag.cid = cid;
            if (cid != null)
            {
                HB2HomeDescription.CustomerId = cid.Value;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm),Convert.ToInt32(PolicyType.FarmPolicy),policyid).ToList();
            //if (details != null && details.Any())
            //{
            //    if (details.Exists(q => q.QuestionId == HB2HomeDescription.PhysicaladdresObj.EiId))
            //    {
            //        HB2HomeDescription.PhysicaladdresObj.Physicaladdres = Convert.ToString(details.Where(q => q.QuestionId == HB2HomeDescription.PhysicaladdresObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == HB2HomeDescription.UNBushlandObj.EiId))
            //    {
            //        HB2HomeDescription.UNBushlandObj.UNBushland = Convert.ToString(details.Where(q => q.QuestionId == HB2HomeDescription.UNBushlandObj.EiId).FirstOrDefault().Answer);
            //    }
            //}
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
            //if (cid.HasValue && cid > 0)
            //{
            //    if (HB2HomeDescription.PhysicaladdresObj != null && HB2HomeDescription.PhysicaladdresObj.EiId > 0 && HB2HomeDescription.PhysicaladdresObj.Physicaladdres != null)
            //    {
            //        db.IT_InsertCustomerQnsData(HB2HomeDescription.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), HB2HomeDescription.PhysicaladdresObj.EiId, HB2HomeDescription.PhysicaladdresObj.Physicaladdres.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
            //    }
            //    if (HB2HomeDescription.UNBushlandObj != null && HB2HomeDescription.UNBushlandObj.EiId > 0 && HB2HomeDescription.UNBushlandObj.UNBushland != null)
            //    {
            //        db.IT_InsertCustomerQnsData(HB2HomeDescription.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), HB2HomeDescription.UNBushlandObj.EiId, HB2HomeDescription.UNBushlandObj.UNBushland.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
            //    }
            //    if (Session["CompletionTrackFPHB"] != null)
            //    {
            //        Session["CompletionTrackFPHB"] = Session["CompletionTrackFPHB"];
            //        HB2HomeDescription.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
            //        if (HB2HomeDescription.CompletionTrackFPHB != null)
            //        {
            //            string Completionstring = string.Empty;
            //            char[] arr = HB2HomeDescription.CompletionTrackFPHB.ToCharArray();
            //            for (int i = 0; i < arr.Length; i++)
            //            {
            //                char a = arr[i];
            //                if (i == 0)
            //                {
            //                    a = '1';
            //                }
            //                Completionstring = Completionstring + a;
            //            }
            //            Session["CompletionTrackFPHB"] = Completionstring;
            //            HB2HomeDescription.CompletionTrackFPHB = Completionstring;
            //        }
            //    }
            //    else
            //    {
            //        Session["CompletionTrackFPHB"] = "1-0-0-0-0"; ;
            //        HB2HomeDescription.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
            //    }
            //    return RedirectToAction("ConstructionDetails", new { cid = cid });
            //}
            return View(HB2HomeDescription);
        }
        [HttpGet]
        public ActionResult ConstructionDetails(int? cid)
        {
            NewPolicyDetailsClass FPHBmodel = new NewPolicyDetailsClass();
            List<SelectListItem> Extwallmadelist = new List<SelectListItem>();
            Extwallmadelist = FPHBmodel.ExternalWallsMadeList();
            List<SelectListItem> RoofMadeList = new List<SelectListItem>();
            RoofMadeList = FPHBmodel.RoofMadesList();
            ConstructionDetails ConstructionDetails = new ConstructionDetails();
            if (cid != null)
            {
                ViewBag.cid = cid;
                ConstructionDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = ConstructionDetails.CustomerId;
            }
        
            ConstructionDetails.ExtwallmadeObj = new ExternalWallsMade();
            ConstructionDetails.ExtwallmadeObj.Extwallmadelist = Extwallmadelist;
            ConstructionDetails.ExtwallmadeObj.EiId = 62085;
            ConstructionDetails.DescribeextwallObj = new DescribeExternalWalls();
            ConstructionDetails.DescribeextwallObj.EiId = 62087;
            ConstructionDetails.RoofmadeObj = new IsRoofMadeOf();
            ConstructionDetails.RoofmadeObj.Roofmadelist = RoofMadeList;
            ConstructionDetails.RoofmadeObj.EiId = 62089;
            ConstructionDetails.DescriberoofObj = new DescribeTheRoofs();
            ConstructionDetails.DescriberoofObj.EiId = 62091;
            ConstructionDetails.YearObj = new YearCDFP();
            ConstructionDetails.YearObj.EiId = 62093;
            ConstructionDetails.LastrewiredObj = new LastRewireds();
            ConstructionDetails.LastrewiredObj.EiId = 62095;
            ConstructionDetails.LastreplumbedObj = new LastReplumbs();
            ConstructionDetails.LastreplumbedObj.EiId = 62097;
            ConstructionDetails.WatertightObj = new WatertightsMaintained();
            ConstructionDetails.WatertightObj.EiId = 62099;
            ConstructionDetails.HeritagelegislationObj = new HeritageLegislationsCouncil();
            ConstructionDetails.HeritagelegislationObj.EiId = 62109;
            ConstructionDetails.UnderconstructionObj = new UnderConstructionsAlteration();
            ConstructionDetails.UnderconstructionObj.EiId = 62111;
            ConstructionDetails.DomesticdwellingObj = new FitnessDomesticDwellings();
            ConstructionDetails.DomesticdwellingObj.EiId = 62113;
            if (Session["CompletionTrackFPHB"] != null)
            {
                Session["CompletionTrackFPHB"] = Session["CompletionTrackFPHB"];
                ConstructionDetails.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
            }
            else
            {
                Session["CompletionTrackFPHB"] = "0-0-0-0-0"; ;
                ConstructionDetails.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == ConstructionDetails.ExtwallmadeObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == ConstructionDetails.ExtwallmadeObj.EiId).FirstOrDefault();
                    ConstructionDetails.ExtwallmadeObj.Extwallmade = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == ConstructionDetails.DescribeextwallObj.EiId))
                {
                    ConstructionDetails.DescribeextwallObj.Describeextwall = Convert.ToString(details.Where(q => q.QuestionId == ConstructionDetails.DescribeextwallObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == ConstructionDetails.RoofmadeObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == ConstructionDetails.RoofmadeObj.EiId).FirstOrDefault();
                    ConstructionDetails.RoofmadeObj.Roofmade = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == ConstructionDetails.DescriberoofObj.EiId))
                {
                    ConstructionDetails.DescriberoofObj.Describeroof = Convert.ToString(details.Where(q => q.QuestionId == ConstructionDetails.DescriberoofObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == ConstructionDetails.YearObj.EiId))
                {
                    ConstructionDetails.YearObj.Year = Convert.ToString(details.Where(q => q.QuestionId == ConstructionDetails.YearObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == ConstructionDetails.LastrewiredObj.EiId))
                {
                    ConstructionDetails.LastrewiredObj.Lastrewired = Convert.ToString(details.Where(q => q.QuestionId == ConstructionDetails.LastrewiredObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == ConstructionDetails.LastreplumbedObj.EiId))
                {
                    ConstructionDetails.LastreplumbedObj.Lastreplumbed = Convert.ToString(details.Where(q => q.QuestionId == ConstructionDetails.LastreplumbedObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == ConstructionDetails.WatertightObj.EiId))
                {
                    ConstructionDetails.WatertightObj.Watertight = Convert.ToString(details.Where(q => q.QuestionId == ConstructionDetails.WatertightObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == ConstructionDetails.HeritagelegislationObj.EiId))
                {
                    ConstructionDetails.HeritagelegislationObj.Heritagelegislation = Convert.ToString(details.Where(q => q.QuestionId == ConstructionDetails.HeritagelegislationObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == ConstructionDetails.UnderconstructionObj.EiId))
                {
                    ConstructionDetails.UnderconstructionObj.Underconstruction = Convert.ToString(details.Where(q => q.QuestionId == ConstructionDetails.UnderconstructionObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == ConstructionDetails.DomesticdwellingObj.EiId))
                {
                    ConstructionDetails.DomesticdwellingObj.Domesticdwelling = Convert.ToString(details.Where(q => q.QuestionId == ConstructionDetails.DomesticdwellingObj.EiId).FirstOrDefault().Answer);
                }
            }
            return View(ConstructionDetails);
        }
        [HttpPost]
        public ActionResult ConstructionDetails(int? cid, ConstructionDetails ConstructionDetails)
        {
            NewPolicyDetailsClass FPHBmodel = new NewPolicyDetailsClass();
            List<SelectListItem> Extwallmadelist = new List<SelectListItem>();
            Extwallmadelist = FPHBmodel.ExternalWallsMadeList();
            List<SelectListItem> RoofMadeList = new List<SelectListItem>();
            RoofMadeList = FPHBmodel.RoofMadesList();
            ConstructionDetails.ExtwallmadeObj.Extwallmadelist = Extwallmadelist;
            ConstructionDetails.RoofmadeObj.Roofmadelist = RoofMadeList;
            if (cid != null)
            {
                ViewBag.cid = cid;
                ConstructionDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = ConstructionDetails.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (ConstructionDetails.ExtwallmadeObj != null && ConstructionDetails.ExtwallmadeObj.EiId > 0 && ConstructionDetails.ExtwallmadeObj.Extwallmade != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), ConstructionDetails.ExtwallmadeObj.EiId, ConstructionDetails.ExtwallmadeObj.Extwallmade.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (ConstructionDetails.DescribeextwallObj != null && ConstructionDetails.DescribeextwallObj.EiId > 0 && ConstructionDetails.DescribeextwallObj.Describeextwall != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), ConstructionDetails.DescribeextwallObj.EiId, ConstructionDetails.DescribeextwallObj.Describeextwall.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (ConstructionDetails.RoofmadeObj != null && ConstructionDetails.RoofmadeObj.EiId > 0 && ConstructionDetails.RoofmadeObj.Roofmade != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), ConstructionDetails.RoofmadeObj.EiId, ConstructionDetails.RoofmadeObj.Roofmade.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (ConstructionDetails.DescriberoofObj != null && ConstructionDetails.DescriberoofObj.EiId > 0 && ConstructionDetails.DescriberoofObj.Describeroof != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), ConstructionDetails.DescriberoofObj.EiId, ConstructionDetails.DescriberoofObj.Describeroof.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (ConstructionDetails.YearObj != null && ConstructionDetails.YearObj.EiId > 0 && ConstructionDetails.YearObj.Year != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), ConstructionDetails.YearObj.EiId, ConstructionDetails.YearObj.Year.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (ConstructionDetails.LastrewiredObj != null && ConstructionDetails.LastrewiredObj.EiId > 0 && ConstructionDetails.LastrewiredObj.Lastrewired != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), ConstructionDetails.LastrewiredObj.EiId, ConstructionDetails.LastrewiredObj.Lastrewired.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (ConstructionDetails.LastreplumbedObj != null && ConstructionDetails.LastreplumbedObj.EiId > 0 && ConstructionDetails.LastreplumbedObj.Lastreplumbed != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), ConstructionDetails.LastreplumbedObj.EiId, ConstructionDetails.LastreplumbedObj.Lastreplumbed.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (ConstructionDetails.WatertightObj != null && ConstructionDetails.WatertightObj.EiId > 0 && ConstructionDetails.WatertightObj.Watertight != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), ConstructionDetails.WatertightObj.EiId, ConstructionDetails.WatertightObj.Watertight.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (ConstructionDetails.HeritagelegislationObj != null && ConstructionDetails.HeritagelegislationObj.EiId > 0 && ConstructionDetails.HeritagelegislationObj.Heritagelegislation != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), ConstructionDetails.HeritagelegislationObj.EiId, ConstructionDetails.HeritagelegislationObj.Heritagelegislation.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (ConstructionDetails.UnderconstructionObj != null && ConstructionDetails.UnderconstructionObj.EiId > 0 && ConstructionDetails.UnderconstructionObj.Underconstruction != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), ConstructionDetails.UnderconstructionObj.EiId, ConstructionDetails.UnderconstructionObj.Underconstruction.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (ConstructionDetails.DomesticdwellingObj != null && ConstructionDetails.DomesticdwellingObj.EiId > 0 && ConstructionDetails.DomesticdwellingObj.Domesticdwelling != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), ConstructionDetails.DomesticdwellingObj.EiId, ConstructionDetails.DomesticdwellingObj.Domesticdwelling.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (Session["CompletionTrackFPHB"] != null)
                {
                    Session["CompletionTrackFPHB"] = Session["CompletionTrackFPHB"];
                    ConstructionDetails.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
                    if (ConstructionDetails.CompletionTrackFPHB != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = ConstructionDetails.CompletionTrackFPHB.ToCharArray();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 2)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["CompletionTrackFPHB"] = Completionstring;
                        ConstructionDetails.CompletionTrackFPHB = Completionstring;
                    }
                }
                else
                {
                    Session["CompletionTrackFPHB"] = "0-1-0-0-0"; ;
                    ConstructionDetails.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
                }
                return RedirectToAction("OccupancyDetails", new { cid = cid });
            }
            return View(ConstructionDetails);
        }
        [HttpGet]
        public ActionResult OccupancyDetails(int? cid)
        {
            OccupancyDetails OccupancyDetails = new OccupancyDetails();
            OccupancyDetails.WholivesObj = new WhoLivesHome();
            OccupancyDetails.WholivesObj.EiId = 62127;
            OccupancyDetails.IsbuildingObj = new IsTheBuildingFPHO();
            OccupancyDetails.IsbuildingObj.EiId = 62129;
            OccupancyDetails.ConsecutiveObj = new ConsecutiveDaysFPHO();
            OccupancyDetails.ConsecutiveObj.EiId = 62131;
            OccupancyDetails.UsedbusinessObj = new UsedForBusinessFPHO();
            OccupancyDetails.UsedbusinessObj.EiId = 62133;
            OccupancyDetails.DesbusinessObj = new DescribeBusinessFPHO();
            OccupancyDetails.DesbusinessObj.EiId = 62135;
            if (cid != null)
            {
                ViewBag.cid = cid;
                OccupancyDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = OccupancyDetails.CustomerId;
            }
            if (Session["CompletionTrackFPHB"] != null)
            {
                Session["CompletionTrackFPHB"] = Session["CompletionTrackFPHB"];
                OccupancyDetails.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
            }
            else
            {
                Session["CompletionTrackFPHB"] = "0-0-0-0-0"; ;
                OccupancyDetails.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm),Convert.ToInt32(PolicyType.FarmPolicy),policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == OccupancyDetails.WholivesObj.EiId))
                {
                    OccupancyDetails.WholivesObj.Wholives = Convert.ToString(details.Where(q => q.QuestionId == OccupancyDetails.WholivesObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == OccupancyDetails.IsbuildingObj.EiId))
                {
                    OccupancyDetails.IsbuildingObj.Isbuilding = Convert.ToString(details.Where(q => q.QuestionId == OccupancyDetails.IsbuildingObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == OccupancyDetails.ConsecutiveObj.EiId))
                {
                    OccupancyDetails.ConsecutiveObj.Consecutive = Convert.ToString(details.Where(q => q.QuestionId == OccupancyDetails.ConsecutiveObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == OccupancyDetails.UsedbusinessObj.EiId))
                {
                    OccupancyDetails.UsedbusinessObj.Usedbusiness = Convert.ToString(details.Where(q => q.QuestionId == OccupancyDetails.UsedbusinessObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == OccupancyDetails.DesbusinessObj.EiId))
                {
                    OccupancyDetails.DesbusinessObj.Desbusiness = Convert.ToString(details.Where(q => q.QuestionId == OccupancyDetails.DesbusinessObj.EiId).FirstOrDefault().Answer);
                }
            }
            return View(OccupancyDetails);
        }
        [HttpPost]
        public ActionResult OccupancyDetails(int? cid, OccupancyDetails OccupancyDetails)
        {
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                OccupancyDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = OccupancyDetails.CustomerId;
            }
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (OccupancyDetails.WholivesObj != null && OccupancyDetails.WholivesObj.EiId > 0 && OccupancyDetails.WholivesObj.Wholives != null)
                {
                    db.IT_InsertCustomerQnsData(OccupancyDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), OccupancyDetails.WholivesObj.EiId, OccupancyDetails.WholivesObj.Wholives.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (OccupancyDetails.IsbuildingObj != null && OccupancyDetails.IsbuildingObj.EiId > 0 && OccupancyDetails.IsbuildingObj.Isbuilding != null)
                {
                    db.IT_InsertCustomerQnsData(OccupancyDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), OccupancyDetails.IsbuildingObj.EiId, OccupancyDetails.IsbuildingObj.Isbuilding.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (OccupancyDetails.ConsecutiveObj != null && OccupancyDetails.ConsecutiveObj.EiId > 0 && OccupancyDetails.ConsecutiveObj.Consecutive != null)
                {
                    db.IT_InsertCustomerQnsData(OccupancyDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), OccupancyDetails.ConsecutiveObj.EiId, OccupancyDetails.ConsecutiveObj.Consecutive.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (OccupancyDetails.UsedbusinessObj != null && OccupancyDetails.UsedbusinessObj.EiId > 0 && OccupancyDetails.UsedbusinessObj.Usedbusiness != null)
                {
                    db.IT_InsertCustomerQnsData(OccupancyDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), OccupancyDetails.UsedbusinessObj.EiId, OccupancyDetails.UsedbusinessObj.Usedbusiness.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (OccupancyDetails.DesbusinessObj != null && OccupancyDetails.DesbusinessObj.EiId > 0 && OccupancyDetails.DesbusinessObj.Desbusiness != null)
                {
                    db.IT_InsertCustomerQnsData(OccupancyDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), OccupancyDetails.DesbusinessObj.EiId, OccupancyDetails.DesbusinessObj.Desbusiness.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (Session["CompletionTrackFPHB"] != null)
                {
                    Session["CompletionTrackFPHB"] = Session["CompletionTrackFPHB"];
                    OccupancyDetails.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
                    if (OccupancyDetails.CompletionTrackFPHB != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = OccupancyDetails.CompletionTrackFPHB.ToCharArray();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 4)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["CompletionTrackFPHB"] = Completionstring;
                        OccupancyDetails.CompletionTrackFPHB = Completionstring;
                    }
                }
                else
                {
                    Session["CompletionTrackFPHB"] = "0-0-1-0-0"; ;
                    OccupancyDetails.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
                }
                return RedirectToAction("HomeBuilding", new { cid = cid });
            }
            return View(OccupancyDetails);
        }
        [HttpGet]
        public ActionResult HomeBuilding(int? cid)
        {
            HomeBuilding HomeBuilding = new HomeBuilding();
            if (cid != null)
            {
                ViewBag.cid = cid;
                HomeBuilding.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = HomeBuilding.CustomerId;
            }
            if (Session["CompletionTrackFPHB"] != null)
            {
                Session["CompletionTrackFPHB"] = Session["CompletionTrackFPHB"];
                HomeBuilding.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
            }
            else
            {
                Session["CompletionTrackFPHB"] = "0-0-0-0-0"; ;
                HomeBuilding.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
            }
            return View(HomeBuilding);
        }
        [HttpPost]
        public ActionResult HomeBuilding(int? cid, HomeBuilding HomeBuilding)
        {
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                HomeBuilding.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = HomeBuilding.CustomerId;
            }
            if (cid.HasValue && cid > 0)
            {
                if (Session["CompletionTrackFPHB"] != null)
                {
                    Session["CompletionTrackFPHB"] = Session["CompletionTrackFPHB"];
                    HomeBuilding.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
                    if (HomeBuilding.CompletionTrackFPHB != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = HomeBuilding.CompletionTrackFPHB.ToCharArray();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 6)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["CompletionTrackFPHB"] = Completionstring;
                        HomeBuilding.CompletionTrackFPHB = Completionstring;
                    }
                }
                else
                {
                    Session["CompletionTrackFPHB"] = "0-0-0-1-0"; ;
                    HomeBuilding.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
                }
                return RedirectToAction("InterestedParty", new { cid = cid });
            }
            return View(HomeBuilding);
        }
        [HttpGet]
        public ActionResult InterestedParty(int? cid)
        {
            InterestedPartyFPIP InterestedPartyFPIP = new InterestedPartyFPIP();
            InterestedPartyFPIP.PartynameObj = new InterestedPartyNameFPIP();
            InterestedPartyFPIP.PartynameObj.EiId = 62187;
            InterestedPartyFPIP.PartylocationObj = new InterestedPartyLocationFPIP();
            InterestedPartyFPIP.PartylocationObj.EiId = 62189;
            if (cid != null)
            {
                ViewBag.cid = cid;
                InterestedPartyFPIP.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = InterestedPartyFPIP.CustomerId;
            }
            if (Session["CompletionTrackFPHB"] != null)
            {
                Session["CompletionTrackFPHB"] = Session["CompletionTrackFPHB"];
                InterestedPartyFPIP.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
            }
            else
            {
                Session["CompletionTrackFPHB"] = "0-0-0-0-0"; ;
                InterestedPartyFPIP.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == InterestedPartyFPIP.PartynameObj.EiId))
                {
                    InterestedPartyFPIP.PartynameObj.Name = Convert.ToString(details.Where(q => q.QuestionId == InterestedPartyFPIP.PartynameObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == InterestedPartyFPIP.PartylocationObj.EiId))
                {
                    InterestedPartyFPIP.PartylocationObj.Location = Convert.ToString(details.Where(q => q.QuestionId == InterestedPartyFPIP.PartylocationObj.EiId).FirstOrDefault().Answer);
                }
            }
            return View(InterestedPartyFPIP);
        }
        [HttpPost]
        public ActionResult InterestedParty(int? cid, InterestedPartyFPIP InterestedPartyFPIP)
        {
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                InterestedPartyFPIP.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = InterestedPartyFPIP.CustomerId;
            }
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (InterestedPartyFPIP.PartynameObj != null && InterestedPartyFPIP.PartynameObj.EiId > 0 && InterestedPartyFPIP.PartynameObj.Name != null)
                {
                    db.IT_InsertCustomerQnsData(InterestedPartyFPIP.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), InterestedPartyFPIP.PartynameObj.EiId, InterestedPartyFPIP.PartynameObj.Name.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (InterestedPartyFPIP.PartylocationObj != null && InterestedPartyFPIP.PartylocationObj.EiId > 0 && InterestedPartyFPIP.PartylocationObj.Location != null)
                {
                    db.IT_InsertCustomerQnsData(InterestedPartyFPIP.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), InterestedPartyFPIP.PartylocationObj.EiId, InterestedPartyFPIP.PartylocationObj.Location.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (Session["CompletionTrackFPHB"] != null)
                {
                    Session["CompletionTrackFPHB"] = Session["CompletionTrackFPHB"];
                    InterestedPartyFPIP.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
                    if (InterestedPartyFPIP.CompletionTrackFPHB != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = InterestedPartyFPIP.CompletionTrackFPHB.ToCharArray();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 8)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["CompletionTrackFPHB"] = Completionstring;
                        InterestedPartyFPIP.CompletionTrackFPHB = Completionstring;
                    }
                }
                else
                {
                    Session["CompletionTrackFPHB"] = "0-0-0-0-1"; ;
                    InterestedPartyFPIP.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
                }
                return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid });
               // return RedirectToAction("HB2HomeDescription", new { cid = cid });
            }
            return View(InterestedPartyFPIP);
        }
    }
}