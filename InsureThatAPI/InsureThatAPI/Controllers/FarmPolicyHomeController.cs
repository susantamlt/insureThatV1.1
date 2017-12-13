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
            MainDetails MainDetails = new MainDetails();
            MainDetails.PhysicaladdresObj = new PhysicalAddress();
            MainDetails.PhysicaladdresObj.EiId = 0;
            MainDetails.UNBushlandObj = new UnclearedNaturalBushland();
            MainDetails.UNBushlandObj.EiId = 62067;
            if (Session["CompletionTrackFPHB"] != null)
            {
                Session["CompletionTrackFPHB"] = Session["CompletionTrackFPHB"];
                MainDetails.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
            }
            else
            {
                Session["CompletionTrackFPHB"] = "0-0-0-0-0"; ;
                MainDetails.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
            }
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
                MainDetails.CustomerId = cid.Value;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm),Convert.ToInt32(PolicyType.FarmPolicy),policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == MainDetails.PhysicaladdresObj.EiId))
                {
                    MainDetails.PhysicaladdresObj.Physicaladdres = Convert.ToString(details.Where(q => q.QuestionId == MainDetails.PhysicaladdresObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MainDetails.UNBushlandObj.EiId))
                {
                    MainDetails.UNBushlandObj.UNBushland = Convert.ToString(details.Where(q => q.QuestionId == MainDetails.UNBushlandObj.EiId).FirstOrDefault().Answer);
                }
            }
            return View(MainDetails);
        }
        [HttpPost]
        public ActionResult MainDetails(int? cid, MainDetails MainDetails)
        {
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                MainDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MainDetails.CustomerId;
            }
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (MainDetails.PhysicaladdresObj != null && MainDetails.PhysicaladdresObj.EiId > 0 && MainDetails.PhysicaladdresObj.Physicaladdres != null)
                {
                    db.IT_InsertCustomerQnsData(MainDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), MainDetails.PhysicaladdresObj.EiId, MainDetails.PhysicaladdresObj.Physicaladdres.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MainDetails.UNBushlandObj != null && MainDetails.UNBushlandObj.EiId > 0 && MainDetails.UNBushlandObj.UNBushland != null)
                {
                    db.IT_InsertCustomerQnsData(MainDetails.CustomerId, Convert.ToInt32(FarmPolicySection.HomeBuildingFarm), MainDetails.UNBushlandObj.EiId, MainDetails.UNBushlandObj.UNBushland.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (Session["CompletionTrackFPHB"] != null)
                {
                    Session["CompletionTrackFPHB"] = Session["CompletionTrackFPHB"];
                    MainDetails.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
                    if (MainDetails.CompletionTrackFPHB != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = MainDetails.CompletionTrackFPHB.ToCharArray();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 0)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["CompletionTrackFPHB"] = Completionstring;
                        MainDetails.CompletionTrackFPHB = Completionstring;
                    }
                }
                else
                {
                    Session["CompletionTrackFPHB"] = "1-0-0-0-0"; ;
                    MainDetails.CompletionTrackFPHB = Session["CompletionTrackFPHB"].ToString();
                }
                return RedirectToAction("ConstructionDetails", new { cid = cid });
            }
            return View(MainDetails);
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
                return RedirectToAction("MainDetails", new { cid = cid });
            }
            return View(InterestedPartyFPIP);
        }
    }
}