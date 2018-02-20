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
    public class FarmPolicyMotorController : Controller
    {
        // GET: FarmPolicyMotor
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult VehicleDescription(int? cid)
        {
            MCVehicleDescription MCVehicleDescription = new MCVehicleDescription();
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Contains("MotorFarm"))
                    {

                    }
                    else
                    {
                        if (Policyincllist.Contains("Electronics"))
                        {
                            return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Money"))
                        {
                            return RedirectToAction("Money", "FarmPolicyMoney", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Transit"))
                        {
                            return RedirectToAction("FarmPolicyTransit", "Transit", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("ValuablesFarm"))
                        {
                            return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("LiveStockFarm"))
                        {
                            return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("PersonalLiabilitiesFarm"))
                        {
                            return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeBuildingFarm"))
                        {
                            return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeContent"))
                        {
                            return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Machinery"))
                        {
                            return RedirectToAction("Machinery", "FarmPolicyMachinery", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Burglary"))
                        {
                            return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid });
                        }
                    }
                }
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 2 });
            }
            
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCVehicleDescription.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCVehicleDescription.CustomerId;
            }
            NewPolicyDetailsClass MCVehicleDescriptionmodel = new NewPolicyDetailsClass();
            List<SelectListItem> VehicleMakeList = new List<SelectListItem>();
            VehicleMakeList = MCVehicleDescriptionmodel.VehicleMake();
            List<SelectListItem> FamilyLists = new List<SelectListItem>();
            FamilyLists = MCVehicleDescriptionmodel.MotorCoverFamily();

            MCVehicleDescription.LscategoryObj = new GLVCategory();
            MCVehicleDescription.LscategoryObj.EiId = 63807;
            MCVehicleDescription.McmakeObj = new GLVMake();
            MCVehicleDescription.McmakeObj.MakeList = VehicleMakeList;
            MCVehicleDescription.McmakeObj.EiId = 63809;
            MCVehicleDescription.McyearObj = new GLVYear();
            MCVehicleDescription.McyearObj.EiId = 63811;
            MCVehicleDescription.MCfamilyObj = new GLVFamily();
            MCVehicleDescription.MCfamilyObj.FamilyList = FamilyLists;
            MCVehicleDescription.MCfamilyObj.EiId = 63813;
            MCVehicleDescription.MCscdObj = new GLVSelectCorDetails();
            MCVehicleDescription.MCscdObj.EiId = 63815;
            MCVehicleDescription.FmmcmakeObj = new FMMCMake();
            MCVehicleDescription.FmmcmakeObj.EiId = 63823;
            MCVehicleDescription.FmmcyearObj = new FMMCYear();
            MCVehicleDescription.FmmcyearObj.EiId = 63825;
            MCVehicleDescription.FmmctypeObj = new FMMCType();
            MCVehicleDescription.FmmctypeObj.FmFamilyList = FamilyLists;
            MCVehicleDescription.FmmctypeObj.EiId = 63835;
            MCVehicleDescription.FmmcscdObj = new FMMCSelectCorDetails();
            MCVehicleDescription.FmmcscdObj.EiId = 63837;
            if (Session["completionTrackMC"] != null)
            {
                Session["completionTrackMC"] = Session["completionTrackMC"];
                MCVehicleDescription.completionTrackMC = Session["completionTrackMC"].ToString();
            }
            else
            {
                Session["completionTrackMC"] = "0-0-0-0-0-0"; ;
                MCVehicleDescription.completionTrackMC = Session["completionTrackMC"].ToString();
            }

            var db = new MasterDataEntities();
            string policyid = null;            
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.MotorFarm), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == MCVehicleDescription.LscategoryObj.EiId))
                {
                    MCVehicleDescription.LscategoryObj.Category = Convert.ToString(details.Where(q => q.QuestionId == MCVehicleDescription.LscategoryObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MCVehicleDescription.McmakeObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MCVehicleDescription.McmakeObj.EiId).FirstOrDefault();
                    MCVehicleDescription.McmakeObj.Make = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == MCVehicleDescription.McyearObj.EiId))
                {
                    MCVehicleDescription.McyearObj.Year = Convert.ToString(details.Where(q => q.QuestionId == MCVehicleDescription.McyearObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MCVehicleDescription.MCfamilyObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MCVehicleDescription.MCfamilyObj.EiId).FirstOrDefault();
                    MCVehicleDescription.MCfamilyObj.Family = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == MCVehicleDescription.MCscdObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MCVehicleDescription.MCscdObj.EiId).FirstOrDefault();
                    MCVehicleDescription.MCscdObj.Scd = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == MCVehicleDescription.FmmcmakeObj.EiId))
                {
                    MCVehicleDescription.FmmcmakeObj.FmMake = Convert.ToString(details.Where(q => q.QuestionId == MCVehicleDescription.FmmcmakeObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MCVehicleDescription.FmmcyearObj.EiId))
                {
                    MCVehicleDescription.FmmcyearObj.FmYear = Convert.ToString(details.Where(q => q.QuestionId == MCVehicleDescription.FmmcyearObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MCVehicleDescription.FmmctypeObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MCVehicleDescription.FmmctypeObj.EiId).FirstOrDefault();
                    MCVehicleDescription.FmmctypeObj.FmFamily = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == MCVehicleDescription.FmmcscdObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MCVehicleDescription.FmmcscdObj.EiId).FirstOrDefault();
                    MCVehicleDescription.FmmcscdObj.FmScd = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
            }
            return View(MCVehicleDescription);
        }
        [HttpPost]
        public ActionResult VehicleDescription(int? cid, MCVehicleDescription MCVehicleDescription)
        {
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCVehicleDescription.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCVehicleDescription.CustomerId;
            }
            NewPolicyDetailsClass MCVehicleDescriptionmodel = new NewPolicyDetailsClass();
            List<SelectListItem> VehicleMakeList = new List<SelectListItem>();
            VehicleMakeList = MCVehicleDescriptionmodel.VehicleMake();
            List<SelectListItem> FamilyLists = new List<SelectListItem>();
            FamilyLists = MCVehicleDescriptionmodel.MotorCoverFamily();

            MCVehicleDescription.MCfamilyObj.FamilyList = FamilyLists;
            MCVehicleDescription.McmakeObj.MakeList = VehicleMakeList;
            MCVehicleDescription.FmmctypeObj.FmFamilyList = FamilyLists;

            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (MCVehicleDescription.LscategoryObj != null && MCVehicleDescription.LscategoryObj.EiId > 0 && MCVehicleDescription.LscategoryObj.Category != null)
                {
                    db.IT_InsertCustomerQnsData(MCVehicleDescription.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCVehicleDescription.LscategoryObj.EiId, MCVehicleDescription.LscategoryObj.Category.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCVehicleDescription.McmakeObj != null && MCVehicleDescription.McmakeObj.EiId > 0 && MCVehicleDescription.McmakeObj.Make != null)
                {
                    db.IT_InsertCustomerQnsData(MCVehicleDescription.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCVehicleDescription.McmakeObj.EiId, MCVehicleDescription.McmakeObj.Make.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCVehicleDescription.McyearObj != null && MCVehicleDescription.McyearObj.EiId > 0 && MCVehicleDescription.McyearObj.Year != null)
                {
                    db.IT_InsertCustomerQnsData(MCVehicleDescription.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCVehicleDescription.McyearObj.EiId, MCVehicleDescription.McyearObj.Year.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCVehicleDescription.MCfamilyObj != null && MCVehicleDescription.MCfamilyObj.EiId > 0 && MCVehicleDescription.MCfamilyObj.Family != null)
                {
                    db.IT_InsertCustomerQnsData(MCVehicleDescription.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCVehicleDescription.MCfamilyObj.EiId, MCVehicleDescription.MCfamilyObj.Family.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCVehicleDescription.MCscdObj != null && MCVehicleDescription.MCscdObj.EiId > 0 && MCVehicleDescription.MCscdObj.Scd != null)
                {
                    db.IT_InsertCustomerQnsData(MCVehicleDescription.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCVehicleDescription.MCscdObj.EiId, MCVehicleDescription.MCscdObj.Scd.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCVehicleDescription.FmmcmakeObj != null && MCVehicleDescription.FmmcmakeObj.EiId > 0 && MCVehicleDescription.FmmcmakeObj.FmMake != null)
                {
                    db.IT_InsertCustomerQnsData(MCVehicleDescription.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCVehicleDescription.FmmcmakeObj.EiId, MCVehicleDescription.FmmcmakeObj.FmMake.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCVehicleDescription.FmmcyearObj != null && MCVehicleDescription.FmmcyearObj.EiId > 0 && MCVehicleDescription.FmmcyearObj.FmYear != null)
                {
                    db.IT_InsertCustomerQnsData(MCVehicleDescription.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCVehicleDescription.FmmcyearObj.EiId, MCVehicleDescription.FmmcyearObj.FmYear.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCVehicleDescription.FmmctypeObj != null && MCVehicleDescription.FmmctypeObj.EiId > 0 && MCVehicleDescription.FmmctypeObj.FmFamily != null)
                {
                    db.IT_InsertCustomerQnsData(MCVehicleDescription.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCVehicleDescription.FmmctypeObj.EiId, MCVehicleDescription.FmmctypeObj.FmFamily.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCVehicleDescription.FmmcscdObj != null && MCVehicleDescription.FmmcscdObj.EiId > 0 && MCVehicleDescription.FmmcscdObj.FmScd != null)
                {
                    db.IT_InsertCustomerQnsData(MCVehicleDescription.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCVehicleDescription.FmmcscdObj.EiId, MCVehicleDescription.FmmcscdObj.FmScd.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (Session["completionTrackMC"] != null)
                {
                    Session["completionTrackMC"] = Session["completionTrackMC"];
                    MCVehicleDescription.completionTrackMC = Session["completionTrackMC"].ToString();
                    if (MCVehicleDescription.completionTrackMC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = MCVehicleDescription.completionTrackMC.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 0)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackMC"] = Completionstring;
                        MCVehicleDescription.completionTrackMC = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackMC"] = "1-0-0-0-0-0"; ;
                    MCVehicleDescription.completionTrackMC = Session["completionTrackMC"].ToString();
                }
                return RedirectToAction("AdditionalDetails", new { cid = MCVehicleDescription.CustomerId });
            }
            return View(MCVehicleDescription);
        }
        [HttpGet]
        public ActionResult AdditionalDetails(int? cid)
        {
            NewPolicyDetailsClass MCAdditionalDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> AddressList = new List<SelectListItem>();
            AddressList = MCAdditionalDetailsmodel.MCADAddress();
            List<SelectListItem> VNList = new List<SelectListItem>();
            VNList = MCAdditionalDetailsmodel.MCADVinNumber();
            List<SelectListItem> ENList = new List<SelectListItem>();
            ENList = MCAdditionalDetailsmodel.MCADEngineNumber();
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Contains("MotorFarm"))
                    {

                    }
                    else
                    {
                        if (Policyincllist.Contains("Electronics"))
                        {
                            return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Money"))
                        {
                            return RedirectToAction("Money", "FarmPolicyMoney", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Transit"))
                        {
                            return RedirectToAction("FarmPolicyTransit", "Transit", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("ValuablesFarm"))
                        {
                            return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("LiveStockFarm"))
                        {
                            return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("PersonalLiabilitiesFarm"))
                        {
                            return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeBuildingFarm"))
                        {
                            return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeContent"))
                        {
                            return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Machinery"))
                        {
                            return RedirectToAction("Machinery", "FarmPolicyMachinery", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Burglary"))
                        {
                            return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid });
                        }
                    }
                }
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 2 });
            }


            MCAdditionalDetails MCAdditionalDetails = new MCAdditionalDetails();
            MCAdditionalDetails.KeptnightObj = new MCADKeptAtNight();
            MCAdditionalDetails.KeptnightObj.EiId = 63851;
            MCAdditionalDetails.AdaddressObj = new MCADAddress();
            MCAdditionalDetails.AdaddressObj.AddressList = AddressList;
            MCAdditionalDetails.AdaddressObj.EiId = 0;
            MCAdditionalDetails.VregisterObj = new MCADVehicleRegistered();
            MCAdditionalDetails.VregisterObj.EiId = 63855;
            MCAdditionalDetails.RnumberObj = new MCADRegistrationNumber();
            MCAdditionalDetails.RnumberObj.EiId = 63857;
            MCAdditionalDetails.VnumberObj = new MCADVinNumber();
            MCAdditionalDetails.VnumberObj.VnumberList = VNList;
            MCAdditionalDetails.VnumberObj.EiId = 63859;
            MCAdditionalDetails.EnumberObj = new MCADEngineNumber();
            MCAdditionalDetails.EnumberObj.EnumberList = ENList;
            MCAdditionalDetails.EnumberObj.EiId = 63861;
            MCAdditionalDetails.VmodifiedObj = new MCADVehicleModified();
            MCAdditionalDetails.VmodifiedObj.EiId = 63863;
            MCAdditionalDetails.DmodifiedObj = new MCADdescribeModified();
            MCAdditionalDetails.DmodifiedObj.EiId = 63865;
            MCAdditionalDetails.SFinstalledObj = new MCADSecurityFeaturesInstalled();
            MCAdditionalDetails.SFinstalledObj.EiId = 63867;
            MCAdditionalDetails.VusedObj = new MCADVehicleUsed();
            MCAdditionalDetails.VusedObj.EiId = 63879;
            MCAdditionalDetails.CcapacityObj = new MCADCarryingCapacity();
            MCAdditionalDetails.CcapacityObj.EiId = 63887;
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCAdditionalDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCAdditionalDetails.CustomerId;
            }
            if (Session["completionTrackMC"] != null)
            {
                Session["completionTrackMC"] = Session["completionTrackMC"];
                MCAdditionalDetails.completionTrackMC = Session["completionTrackMC"].ToString();
            }
            else
            {
                Session["completionTrackMC"] = "0-0-0-0-0-0"; ;
                MCAdditionalDetails.completionTrackMC = Session["completionTrackMC"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
           
            return View(MCAdditionalDetails);
        }
        [HttpPost]
        public ActionResult AdditionalDetails(int? cid, MCAdditionalDetails MCAdditionalDetails)
        {
            NewPolicyDetailsClass MCAdditionalDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> AddressList = new List<SelectListItem>();
            AddressList = MCAdditionalDetailsmodel.MCADAddress();
            List<SelectListItem> VNList = new List<SelectListItem>();
            VNList = MCAdditionalDetailsmodel.MCADVinNumber();
            List<SelectListItem> ENList = new List<SelectListItem>();
            ENList = MCAdditionalDetailsmodel.MCADEngineNumber();

            MCAdditionalDetails.AdaddressObj.AddressList = AddressList;
            MCAdditionalDetails.VnumberObj.VnumberList = VNList;
            MCAdditionalDetails.EnumberObj.EnumberList = ENList;

            if (cid != null)
            {
                ViewBag.cid = cid;
                MCAdditionalDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCAdditionalDetails.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (MCAdditionalDetails.KeptnightObj != null && MCAdditionalDetails.KeptnightObj.EiId > 0 && MCAdditionalDetails.KeptnightObj.Keptnight != null)
                {
                    db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCAdditionalDetails.KeptnightObj.EiId, MCAdditionalDetails.KeptnightObj.Keptnight.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCAdditionalDetails.AdaddressObj != null && MCAdditionalDetails.AdaddressObj.EiId > 0 && MCAdditionalDetails.AdaddressObj.Address != null)
                {
                    db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCAdditionalDetails.AdaddressObj.EiId, MCAdditionalDetails.AdaddressObj.Address.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCAdditionalDetails.VregisterObj != null && MCAdditionalDetails.VregisterObj.EiId > 0 && MCAdditionalDetails.VregisterObj.Register != null)
                {
                    db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCAdditionalDetails.VregisterObj.EiId, MCAdditionalDetails.VregisterObj.Register.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCAdditionalDetails.RnumberObj != null && MCAdditionalDetails.RnumberObj.EiId > 0 && MCAdditionalDetails.RnumberObj.Rnumber != null)
                {
                    db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCAdditionalDetails.RnumberObj.EiId, MCAdditionalDetails.RnumberObj.Rnumber.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCAdditionalDetails.VnumberObj != null && MCAdditionalDetails.VnumberObj.EiId > 0 && MCAdditionalDetails.VnumberObj.Vnumber != null)
                {
                    db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCAdditionalDetails.VnumberObj.EiId, MCAdditionalDetails.VnumberObj.Vnumber.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCAdditionalDetails.EnumberObj != null && MCAdditionalDetails.EnumberObj.EiId > 0 && MCAdditionalDetails.EnumberObj.Enumber != null)
                {
                    db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCAdditionalDetails.EnumberObj.EiId, MCAdditionalDetails.EnumberObj.Enumber.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCAdditionalDetails.VmodifiedObj != null && MCAdditionalDetails.VmodifiedObj.EiId > 0 && MCAdditionalDetails.VmodifiedObj.Vmodified != null)
                {
                    db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCAdditionalDetails.VmodifiedObj.EiId, MCAdditionalDetails.VmodifiedObj.Vmodified.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCAdditionalDetails.DmodifiedObj != null && MCAdditionalDetails.DmodifiedObj.EiId > 0 && MCAdditionalDetails.DmodifiedObj.Dmodified != null)
                {
                    db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCAdditionalDetails.DmodifiedObj.EiId, MCAdditionalDetails.DmodifiedObj.Dmodified.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCAdditionalDetails.SFinstalledObj != null && MCAdditionalDetails.SFinstalledObj.EiId > 0 && MCAdditionalDetails.SFinstalledObj.Installed != null)
                {
                    db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCAdditionalDetails.SFinstalledObj.EiId, MCAdditionalDetails.SFinstalledObj.Installed.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCAdditionalDetails.VusedObj != null && MCAdditionalDetails.VusedObj.EiId > 0 && MCAdditionalDetails.VusedObj.Vused != null)
                {
                    db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCAdditionalDetails.VusedObj.EiId, MCAdditionalDetails.VusedObj.Vused.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCAdditionalDetails.CcapacityObj != null && MCAdditionalDetails.CcapacityObj.EiId > 0 && MCAdditionalDetails.CcapacityObj.Ccapacity != null)
                {
                    db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCAdditionalDetails.CcapacityObj.EiId, MCAdditionalDetails.CcapacityObj.Ccapacity.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (Session["completionTrackMC"] != null)
                {
                    Session["completionTrackMC"] = Session["completionTrackMC"];
                    MCAdditionalDetails.completionTrackMC = Session["completionTrackMC"].ToString();
                    if (MCAdditionalDetails.completionTrackMC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = MCAdditionalDetails.completionTrackMC.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 2)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackMC"] = Completionstring;
                        MCAdditionalDetails.completionTrackMC = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackMC"] = "0-1-0-0-0-0"; ;
                    MCAdditionalDetails.completionTrackMC = Session["completionTrackMC"].ToString();
                }
                return RedirectToAction("Drivers", new { cid = MCAdditionalDetails.CustomerId });
            }
            return View(MCAdditionalDetails);
        }
        [HttpGet]
        public ActionResult Drivers(int? cid)
        {
            List<SelectListItem> DriversGendarList = new List<SelectListItem>();
            DriversGendarList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            DriversGendarList.Add(new SelectListItem { Value = "1", Text = "Male" });
            DriversGendarList.Add(new SelectListItem { Value = "2", Text = "Female" });
            MCDrivers MCDrivers = new MCDrivers();
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Contains("MotorFarm"))
                    {

                    }
                    else
                    {
                        if (Policyincllist.Contains("Electronics"))
                        {
                            return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Money"))
                        {
                            return RedirectToAction("Money", "FarmPolicyMoney", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Transit"))
                        {
                            return RedirectToAction("FarmPolicyTransit", "Transit", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("ValuablesFarm"))
                        {
                            return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("LiveStockFarm"))
                        {
                            return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("PersonalLiabilitiesFarm"))
                        {
                            return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeBuildingFarm"))
                        {
                            return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeContent"))
                        {
                            return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Machinery"))
                        {
                            return RedirectToAction("Machinery", "FarmPolicyMachinery", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Burglary"))
                        {
                            return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid });
                        }
                    }
                }
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 2 });
            }

            if (cid != null)
            {
                ViewBag.cid = cid;
                MCDrivers.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCDrivers.CustomerId;
            }
            MCDrivers.DrivernameObj = new DriverName();
            MCDrivers.DrivernameObj.EiId = 63901;
            MCDrivers.DriverageObj = new DriverAge();
            MCDrivers.DriverageObj.EiId = 63903;
            MCDrivers.DrivergenderObj = new DriverGender();
            MCDrivers.DrivergenderObj.GenderList = DriversGendarList;
            MCDrivers.DrivergenderObj.EiId = 60843;
            MCDrivers.DriveramicObj = new DriverAmic();
            MCDrivers.DriveramicObj.EiId = 60843;
            MCDrivers.UsevehicleObj = new UseOfVehicle();
            MCDrivers.UsevehicleObj.EiId = 60845;
            if (Session["completionTrackMC"] != null)
            {
                Session["completionTrackMC"] = Session["completionTrackMC"];
                MCDrivers.completionTrackMC = Session["completionTrackMC"].ToString();
            }
            else
            {
                Session["completionTrackMC"] = "0-0-0-0-0-0"; ;
                MCDrivers.completionTrackMC = Session["completionTrackMC"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.MotorFarm), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == MCDrivers.DrivernameObj.EiId))
                {
                    MCDrivers.DrivernameObj.Name = details.Where(q => q.QuestionId == MCDrivers.DrivernameObj.EiId).FirstOrDefault().Answer;
                }
                if (details.Exists(q => q.QuestionId == MCDrivers.DriverageObj.EiId))
                {
                    MCDrivers.DriverageObj.Age = details.Where(q => q.QuestionId == MCDrivers.DriverageObj.EiId).FirstOrDefault().Answer;
                }
                if (details.Exists(q => q.QuestionId == MCDrivers.DrivergenderObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MCDrivers.DrivergenderObj.EiId).FirstOrDefault();
                    MCDrivers.DrivergenderObj.Gender = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == MCDrivers.DriveramicObj.EiId))
                {
                    MCDrivers.DriveramicObj.Amic = details.Where(q => q.QuestionId == MCDrivers.DriveramicObj.EiId).FirstOrDefault().Answer;
                }
                if (details.Exists(q => q.QuestionId == MCDrivers.UsevehicleObj.EiId))
                {
                    MCDrivers.UsevehicleObj.Usevehicle = details.Where(q => q.QuestionId == MCDrivers.UsevehicleObj.EiId).FirstOrDefault().Answer;
                }
            }
            return View(MCDrivers);
        }
        [HttpPost]
        public ActionResult Drivers(int? cid, MCDrivers MCDrivers)
        {
            List<SelectListItem> DriversGendarList = new List<SelectListItem>();
            DriversGendarList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            DriversGendarList.Add(new SelectListItem { Value = "1", Text = "Male" });
            DriversGendarList.Add(new SelectListItem { Value = "2", Text = "Female" });
            MCDrivers.DrivergenderObj.GenderList = DriversGendarList;
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCDrivers.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCDrivers.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (MCDrivers.DrivernameObj != null && MCDrivers.DrivernameObj.EiId > 0 && MCDrivers.DrivernameObj.Name != null)
                {
                    db.IT_InsertCustomerQnsData(MCDrivers.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCDrivers.DrivernameObj.EiId, MCDrivers.DrivernameObj.Name.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCDrivers.DriverageObj != null && MCDrivers.DriverageObj.EiId > 0 && MCDrivers.DriverageObj.Age != null)
                {
                    db.IT_InsertCustomerQnsData(MCDrivers.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCDrivers.DriverageObj.EiId, MCDrivers.DriverageObj.Age.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCDrivers.DrivergenderObj != null && MCDrivers.DrivergenderObj.EiId > 0 && MCDrivers.DrivergenderObj.Gender != null)
                {
                    db.IT_InsertCustomerQnsData(MCDrivers.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCDrivers.DrivergenderObj.EiId, MCDrivers.DrivergenderObj.Gender.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCDrivers.DriveramicObj != null && MCDrivers.DriveramicObj.EiId > 0 && MCDrivers.DriveramicObj.Amic != null)
                {
                    db.IT_InsertCustomerQnsData(MCDrivers.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCDrivers.DriveramicObj.EiId, MCDrivers.DriveramicObj.Amic.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCDrivers.UsevehicleObj != null && MCDrivers.UsevehicleObj.EiId > 0 && MCDrivers.UsevehicleObj.Usevehicle != null)
                {
                    db.IT_InsertCustomerQnsData(MCDrivers.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCDrivers.UsevehicleObj.EiId, MCDrivers.UsevehicleObj.Usevehicle.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (Session["completionTrackMC"] != null)
                {
                    Session["completionTrackMC"] = Session["completionTrackMC"];
                    MCDrivers.completionTrackMC = Session["completionTrackMC"].ToString();
                    if (MCDrivers.completionTrackMC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = MCDrivers.completionTrackMC.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 4)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackMC"] = Completionstring;
                        MCDrivers.completionTrackMC = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackMC"] = "0-0-1-0-0-0"; ;
                    MCDrivers.completionTrackMC = Session["completionTrackMC"].ToString();
                }
                return RedirectToAction("CoverDetails", new { cid = MCDrivers.CustomerId });
            }
            return View();
        }
        [HttpGet]
        public ActionResult CoverDetails(int? cid)
        {
            NewPolicyDetailsClass CoverDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> descriptionList = new List<SelectListItem>();
            descriptionList = CoverDetailsmodel.MCCDDescription();
            MCCoverDetails MCCoverDetails = new MCCoverDetails();
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Contains("MotorFarm"))
                    {

                    }
                    else
                    {
                        if (Policyincllist.Contains("Electronics"))
                        {
                            return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Money"))
                        {
                            return RedirectToAction("Money", "FarmPolicyMoney", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Transit"))
                        {
                            return RedirectToAction("FarmPolicyTransit", "Transit", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("ValuablesFarm"))
                        {
                            return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("LiveStockFarm"))
                        {
                            return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("PersonalLiabilitiesFarm"))
                        {
                            return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeBuildingFarm"))
                        {
                            return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeContent"))
                        {
                            return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Machinery"))
                        {
                            return RedirectToAction("Machinery", "FarmPolicyMachinery", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Burglary"))
                        {
                            return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid });
                        }
                    }
                }
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 2 });
            }

            if (cid != null)
            {
                ViewBag.cid = cid;
                MCCoverDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCCoverDetails.CustomerId;
            }
            MCCoverDetails.CoveroptionObj = new CoverOptionCD();
            MCCoverDetails.CoveroptionObj.EiId = 63919;
            MCCoverDetails.CovertypeObj = new CoverTypeCD();
            MCCoverDetails.CovertypeObj.EiId = 63921;
            MCCoverDetails.MaxMarvalObj = new MaximumMarketValue();
            MCCoverDetails.MaxMarvalObj.EiId = 63925;
            MCCoverDetails.CaravanannexObj = new CaravanAnnex();
            MCCoverDetails.CaravanannexObj.EiId = 63931;
            MCCoverDetails.UnspecifieditemsObj = new UnspecifiedItems();
            MCCoverDetails.UnspecifieditemsObj.EiId = 63933;
            MCCoverDetails.AccessoriesObj = new NonStandardAccessories();
            MCCoverDetails.AccessoriesObj.EiId = 63937;
            MCCoverDetails.DescriptionObj = new AccessoryDescriptionCD();
            MCCoverDetails.DescriptionObj.DescriptionList = descriptionList;
            MCCoverDetails.DescriptionObj.EiId = 63951;
            MCCoverDetails.SumnsuredObj = new SumInsuredCD();
            MCCoverDetails.SumnsuredObj.EiId = 63953;
            MCCoverDetails.LimitindemnityObj = new LimitOfIndemnityDC();
            MCCoverDetails.LimitindemnityObj.EiId = 63965;
            MCCoverDetails.RatingObj = new RatingDC();
            MCCoverDetails.RatingObj.EiId = 63977;
            MCCoverDetails.NoclaimbonusObj = new NoClaimBonus();
            MCCoverDetails.NoclaimbonusObj.EiId = 63979;
            if (Session["completionTrackMC"] != null)
            {
                Session["completionTrackMC"] = Session["completionTrackMC"];
                MCCoverDetails.completionTrackMC = Session["completionTrackMC"].ToString();
            }
            else
            {
                Session["completionTrackMC"] = "0-0-0-0-0-0"; ;
                MCCoverDetails.completionTrackMC = Session["completionTrackMC"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.MotorFarm), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == MCCoverDetails.CoveroptionObj.EiId))
                {
                    MCCoverDetails.CoveroptionObj.Coveroption = details.Where(q => q.QuestionId == MCCoverDetails.CoveroptionObj.EiId).FirstOrDefault().Answer;
                }
                if (details.Exists(q => q.QuestionId == MCCoverDetails.CovertypeObj.EiId))
                {
                    MCCoverDetails.CovertypeObj.Covertype = details.Where(q => q.QuestionId == MCCoverDetails.CovertypeObj.EiId).FirstOrDefault().Answer;
                }
                if (details.Exists(q => q.QuestionId == MCCoverDetails.MaxMarvalObj.EiId))
                {
                    MCCoverDetails.MaxMarvalObj.Marketvalue = details.Where(q => q.QuestionId == MCCoverDetails.MaxMarvalObj.EiId).FirstOrDefault().Answer;
                }
                if (details.Exists(q => q.QuestionId == MCCoverDetails.CaravanannexObj.EiId))
                {
                    MCCoverDetails.CaravanannexObj.Annex = details.Where(q => q.QuestionId == MCCoverDetails.CaravanannexObj.EiId).FirstOrDefault().Answer;
                }
                if (details.Exists(q => q.QuestionId == MCCoverDetails.DescriptionObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MCCoverDetails.DescriptionObj.EiId).FirstOrDefault();
                    MCCoverDetails.DescriptionObj.Description = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

            }
            return View(MCCoverDetails);
        }
        [HttpPost]
        public ActionResult CoverDetails(int? cid, MCCoverDetails MCCoverDetails)
        {
            NewPolicyDetailsClass CoverDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> descriptionList = new List<SelectListItem>();
            descriptionList = CoverDetailsmodel.MCCDDescription();
            MCCoverDetails.DescriptionObj.DescriptionList = descriptionList;
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCCoverDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCCoverDetails.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (MCCoverDetails.CoveroptionObj != null && MCCoverDetails.CoveroptionObj.EiId > 0 && MCCoverDetails.CoveroptionObj.Coveroption != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCCoverDetails.CoveroptionObj.EiId, MCCoverDetails.CoveroptionObj.Coveroption.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCCoverDetails.CovertypeObj != null && MCCoverDetails.CovertypeObj.EiId > 0 && MCCoverDetails.CovertypeObj.Covertype != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCCoverDetails.CovertypeObj.EiId, MCCoverDetails.CovertypeObj.Covertype.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCCoverDetails.MaxMarvalObj != null && MCCoverDetails.MaxMarvalObj.EiId > 0 && MCCoverDetails.MaxMarvalObj.Marketvalue != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCCoverDetails.MaxMarvalObj.EiId, MCCoverDetails.MaxMarvalObj.Marketvalue.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCCoverDetails.CaravanannexObj != null && MCCoverDetails.CaravanannexObj.EiId > 0 && MCCoverDetails.CaravanannexObj.Annex != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCCoverDetails.CaravanannexObj.EiId, MCCoverDetails.CaravanannexObj.Annex.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCCoverDetails.UnspecifieditemsObj != null && MCCoverDetails.UnspecifieditemsObj.EiId > 0 && MCCoverDetails.UnspecifieditemsObj.Item != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCCoverDetails.UnspecifieditemsObj.EiId, MCCoverDetails.UnspecifieditemsObj.Item.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCCoverDetails.AccessoriesObj != null && MCCoverDetails.AccessoriesObj.EiId > 0 && MCCoverDetails.AccessoriesObj.Accessories != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCCoverDetails.AccessoriesObj.EiId, MCCoverDetails.AccessoriesObj.Accessories.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCCoverDetails.DescriptionObj != null && MCCoverDetails.DescriptionObj.EiId > 0 && MCCoverDetails.DescriptionObj.Description != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCCoverDetails.DescriptionObj.EiId, MCCoverDetails.DescriptionObj.Description.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCCoverDetails.SumnsuredObj != null && MCCoverDetails.SumnsuredObj.EiId > 0 && MCCoverDetails.SumnsuredObj.Suminsured != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCCoverDetails.SumnsuredObj.EiId, MCCoverDetails.SumnsuredObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCCoverDetails.LimitindemnityObj != null && MCCoverDetails.LimitindemnityObj.EiId > 0 && MCCoverDetails.LimitindemnityObj.Indemnity != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCCoverDetails.LimitindemnityObj.EiId, MCCoverDetails.LimitindemnityObj.Indemnity.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCCoverDetails.RatingObj != null && MCCoverDetails.RatingObj.EiId > 0 && MCCoverDetails.RatingObj.Rating != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCCoverDetails.RatingObj.EiId, MCCoverDetails.RatingObj.Rating.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCCoverDetails.NoclaimbonusObj != null && MCCoverDetails.NoclaimbonusObj.EiId > 0 && MCCoverDetails.NoclaimbonusObj.Bonus != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCCoverDetails.NoclaimbonusObj.EiId, MCCoverDetails.NoclaimbonusObj.Bonus.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (Session["completionTrackMC"] != null)
                {
                    Session["completionTrackMC"] = Session["completionTrackMC"];
                    MCCoverDetails.completionTrackMC = Session["completionTrackMC"].ToString();
                    if (MCCoverDetails.completionTrackMC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = MCCoverDetails.completionTrackMC.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 6)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackMC"] = Completionstring;
                        MCCoverDetails.completionTrackMC = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackMC"] = "0-0-0-1-0-0"; ;
                    MCCoverDetails.completionTrackMC = Session["completionTrackMC"].ToString();
                }
                return RedirectToAction("OptionalExtrasExcesses", new { cid = MCCoverDetails.CustomerId });
            }
            return View(MCCoverDetails);
        }
        [HttpGet]
        public ActionResult OptionalExtrasExcesses(int? cid)
        {
            List<SelectListItem> BasicExcessList = new List<SelectListItem>();
            BasicExcessList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            BasicExcessList.Add(new SelectListItem { Value = "60953", Text = "Under 21 Excess" });
            BasicExcessList.Add(new SelectListItem { Value = "60955", Text = "Under 25 Excess" });
            MCOptionalExtrasExcesses MCOptionalExtrasExcesses = new MCOptionalExtrasExcesses();
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Contains("MotorFarm"))
                    {

                    }
                    else
                    {
                        if (Policyincllist.Contains("Electronics"))
                        {
                            return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Money"))
                        {
                            return RedirectToAction("Money", "FarmPolicyMoney", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Transit"))
                        {
                            return RedirectToAction("FarmPolicyTransit", "Transit", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("ValuablesFarm"))
                        {
                            return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("LiveStockFarm"))
                        {
                            return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("PersonalLiabilitiesFarm"))
                        {
                            return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeBuildingFarm"))
                        {
                            return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeContent"))
                        {
                            return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Machinery"))
                        {
                            return RedirectToAction("Machinery", "FarmPolicyMachinery", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Burglary"))
                        {
                            return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid });
                        }
                    }
                }
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 2 });
            }

            if (cid != null)
            {
                ViewBag.cid = cid;
                MCOptionalExtrasExcesses.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCOptionalExtrasExcesses.CustomerId;
            }
            MCOptionalExtrasExcesses.CaroptionObj = new HireCarOption();
            MCOptionalExtrasExcesses.CaroptionObj.EiId = 64003;
            MCOptionalExtrasExcesses.ExcessObj = new BasicExcess();
            MCOptionalExtrasExcesses.ExcessObj.ExcessList = BasicExcessList;
            MCOptionalExtrasExcesses.ExcessObj.EiId = 64009;
            if (Session["completionTrackMC"] != null)
            {
                Session["completionTrackMC"] = Session["completionTrackMC"];
                MCOptionalExtrasExcesses.completionTrackMC = Session["completionTrackMC"].ToString();
            }
            else
            {
                Session["completionTrackMC"] = "0-0-0-0-0-0"; ;
                MCOptionalExtrasExcesses.completionTrackMC = Session["completionTrackMC"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.MotorFarm), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == MCOptionalExtrasExcesses.CaroptionObj.EiId))
                {
                    MCOptionalExtrasExcesses.CaroptionObj.Caroption = details.Where(q => q.QuestionId == MCOptionalExtrasExcesses.CaroptionObj.EiId).FirstOrDefault().Answer;
                }
                if (details.Exists(q => q.QuestionId == MCOptionalExtrasExcesses.ExcessObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MCOptionalExtrasExcesses.ExcessObj.EiId).FirstOrDefault();
                    MCOptionalExtrasExcesses.ExcessObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
            }
            return View(MCOptionalExtrasExcesses);
        }
        [HttpPost]
        public ActionResult OptionalExtrasExcesses(int? cid, MCOptionalExtrasExcesses MCOptionalExtrasExcesses)
        {
            List<SelectListItem> BasicExcessList = new List<SelectListItem>();
            BasicExcessList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            BasicExcessList.Add(new SelectListItem { Value = "60953", Text = "Under 21 Excess" });
            BasicExcessList.Add(new SelectListItem { Value = "60955", Text = "Under 25 Excess" });
            MCOptionalExtrasExcesses.ExcessObj.ExcessList = BasicExcessList;
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCOptionalExtrasExcesses.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCOptionalExtrasExcesses.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (MCOptionalExtrasExcesses.CaroptionObj != null && MCOptionalExtrasExcesses.CaroptionObj.EiId > 0 && MCOptionalExtrasExcesses.CaroptionObj.Caroption != null)
                {
                    db.IT_InsertCustomerQnsData(MCOptionalExtrasExcesses.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCOptionalExtrasExcesses.CaroptionObj.EiId, MCOptionalExtrasExcesses.CaroptionObj.Caroption.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCOptionalExtrasExcesses.ExcessObj != null && MCOptionalExtrasExcesses.ExcessObj.EiId > 0 && MCOptionalExtrasExcesses.ExcessObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(MCOptionalExtrasExcesses.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCOptionalExtrasExcesses.ExcessObj.EiId, MCOptionalExtrasExcesses.ExcessObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (Session["completionTrackMC"] != null)
                {
                    Session["completionTrackMC"] = Session["completionTrackMC"];
                    MCOptionalExtrasExcesses.completionTrackMC = Session["completionTrackMC"].ToString();
                    if (MCOptionalExtrasExcesses.completionTrackMC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = MCOptionalExtrasExcesses.completionTrackMC.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 8)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackMC"] = Completionstring;
                        MCOptionalExtrasExcesses.completionTrackMC = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackMC"] = "0-0-0-0-1-0"; ;
                    MCOptionalExtrasExcesses.completionTrackMC = Session["completionTrackMC"].ToString();
                }
                return RedirectToAction("InterestedParties", new { cid = MCOptionalExtrasExcesses.CustomerId });
            }
            return View(MCOptionalExtrasExcesses);
        }
        [HttpGet]
        public ActionResult InterestedParties(int? cid)
        {
            MCInterestedParties MCInterestedParties = new MCInterestedParties();
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Contains("MotorFarm"))
                    {

                    }
                    else
                    {
                        if (Policyincllist.Contains("Electronics"))
                        {
                            return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Money"))
                        {
                            return RedirectToAction("Money", "FarmPolicyMoney", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Transit"))
                        {
                            return RedirectToAction("FarmPolicyTransit", "Transit", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("ValuablesFarm"))
                        {
                            return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("LiveStockFarm"))
                        {
                            return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("PersonalLiabilitiesFarm"))
                        {
                            return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeBuildingFarm"))
                        {
                            return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeContent"))
                        {
                            return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Machinery"))
                        {
                            return RedirectToAction("Machinery", "FarmPolicyMachinery", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Burglary"))
                        {
                            return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid });
                        }
                    }
                }
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 2 });
            }

            if (cid != null)
            {
                ViewBag.cid = cid;
                MCInterestedParties.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCInterestedParties.CustomerId;
            }
            MCInterestedParties.MCPartynameObj = new MCInterestedPartyName();
            MCInterestedParties.MCPartynameObj.EiId = 64025;
            MCInterestedParties.MCPartyLocationObj = new MCInterestedPartyLocation();
            MCInterestedParties.MCPartyLocationObj.EiId = 64027;
            if (Session["completionTrackMC"] != null)
            {
                Session["completionTrackMC"] = Session["completionTrackMC"];
                MCInterestedParties.completionTrackMC = Session["completionTrackMC"].ToString();
            }
            else
            {
                Session["completionTrackMC"] = "0-0-0-0-0-0"; ;
                MCInterestedParties.completionTrackMC = Session["completionTrackMC"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.MotorFarm), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == MCInterestedParties.MCPartynameObj.EiId))
                {
                    MCInterestedParties.MCPartynameObj.Name = details.Where(q => q.QuestionId == MCInterestedParties.MCPartyLocationObj.EiId).FirstOrDefault().Answer;
                }
                if (details.Exists(q => q.QuestionId == MCInterestedParties.MCPartyLocationObj.EiId))
                {
                    MCInterestedParties.MCPartyLocationObj.Location = details.Where(q => q.QuestionId == MCInterestedParties.MCPartyLocationObj.EiId).FirstOrDefault().Answer;
                }
            }
            return View(MCInterestedParties);
        }
        [HttpPost]
        public ActionResult InterestedParties(int? cid, MCInterestedParties MCInterestedParties)
        {
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCInterestedParties.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCInterestedParties.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (MCInterestedParties.MCPartynameObj != null && MCInterestedParties.MCPartynameObj.EiId > 0 && MCInterestedParties.MCPartynameObj.Name != null)
                {
                    db.IT_InsertCustomerQnsData(MCInterestedParties.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCInterestedParties.MCPartynameObj.EiId, MCInterestedParties.MCPartynameObj.Name.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MCInterestedParties.MCPartyLocationObj != null && MCInterestedParties.MCPartyLocationObj.EiId > 0 && MCInterestedParties.MCPartyLocationObj.Location != null)
                {
                    db.IT_InsertCustomerQnsData(MCInterestedParties.CustomerId, Convert.ToInt32(FarmPolicySection.MotorFarm), MCInterestedParties.MCPartyLocationObj.EiId, MCInterestedParties.MCPartyLocationObj.Location.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (Session["completionTrackMC"] != null)
                {
                    Session["completionTrackMC"] = Session["completionTrackMC"];
                    MCInterestedParties.completionTrackMC = Session["completionTrackMC"].ToString();
                    if (MCInterestedParties.completionTrackMC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = MCInterestedParties.completionTrackMC.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 10)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackMC"] = Completionstring;
                        MCInterestedParties.completionTrackMC = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackMC"] = "0-0-0-0-0-1"; ;
                    MCInterestedParties.completionTrackMC = Session["completionTrackMC"].ToString();
                }
                return RedirectToAction("VehicleDescription", new { cid = MCInterestedParties.CustomerId });
            }
            return View(MCInterestedParties);
        }
        [HttpPost]
        public ActionResult MotorAjaxcontent(int id, string content)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            if (Request.IsAjaxRequest())
            {
                int cid = 1;
                ViewBag.cid = cid;
                if (content == "coverDetails")
                {
                    List<SelectListItem> descriptionList = new List<SelectListItem>();
                    descriptionList = commonModel.MCCDDescription();

                    return Json(new { status = true, des = descriptionList });
                }
                else
                {
                    return Json(new { status = false, des = "" });
                }
            }
            return Json(new { status = false, des = "" });
        }
    }
}