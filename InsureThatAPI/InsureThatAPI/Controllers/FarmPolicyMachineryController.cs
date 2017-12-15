using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;
using InsureThatAPI.CommonMethods;
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;

namespace InsureThatAPI.Controllers
{
    public class FarmPolicyMachineryController : Controller
    {
        // GET: FarmPolicyMachinery
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Machinery(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> MachineryItemTypeofUnit = new List<SelectListItem>();
            List<SelectListItem> PowerMachinery = new List<SelectListItem>();
            List<SelectListItem> VolumeOfVatMachinery = new List<SelectListItem>();
            List<SelectListItem> MachineryTypeOfUnit = new List<SelectListItem>();
            List<SelectListItem> excessToPay = new List<SelectListItem>();

            MachineryItemTypeofUnit = commonModel.TypeOfMachinery();
            PowerMachinery = commonModel.Power();
            VolumeOfVatMachinery = commonModel.VolumeOfVat();
            MachineryTypeOfUnit = commonModel.MachineryTypeOfUnit();
            excessToPay = commonModel.excessRate();

            FPMachinery FPMachinery = new FPMachinery();
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Contains("Machinery"))
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
                        else if (Policyincllist.Contains("Burglary"))
                        {
                              return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("MotorFarm"))
                        {
                             return RedirectToAction("VehicleDescription", "FarmPolicyMotor", new { cid = cid });
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
                FPMachinery.CustomerId = cid.Value;
            }

            FPMachinery.UnSpecTypeOfMachineryFPObj = new UnSpecTypeOfMachineryFP();
            FPMachinery.UnSpecTypeOfMachineryFPObj.TypeofMachineryList = MachineryItemTypeofUnit;
            FPMachinery.UnSpecTypeOfMachineryFPObj.EiId = 62967;

            FPMachinery.UnSpecPowerFPObj = new UnSpecPowerFP();
            FPMachinery.UnSpecPowerFPObj.UnSpecPowerList = PowerMachinery;
            FPMachinery.UnSpecPowerFPObj.EiId = 62969;

            FPMachinery.UnSpecMachNoOfUnitsFPObj = new UnSpecMachNoOfUnitsFP();
            FPMachinery.UnSpecMachNoOfUnitsFPObj.EiId = 62971;

            FPMachinery.UnSpecMachSumInsuredFPObj = new UnSpecMachSumInsuredFP();
            FPMachinery.UnSpecMachSumInsuredFPObj.EiId = 62973;

            FPMachinery.UnSpecMachTotalSumInsuredFPObj = new UnSpecMachTotalSumInsuredFP();
            FPMachinery.UnSpecMachTotalSumInsuredFPObj.EiId = 62975;

            FPMachinery.MilkingVolumeOfVatFPObj = new MilkingVolumeOfVatFP();
            FPMachinery.MilkingVolumeOfVatFPObj.VolumeOfVatList = VolumeOfVatMachinery;
            FPMachinery.MilkingVolumeOfVatFPObj.EiId = 62995;

            FPMachinery.MilkingNoOfVatsFPObj = new MilkingNoOfVatsFP();
            FPMachinery.MilkingNoOfVatsFPObj.EiId = 62997;

            FPMachinery.MilkingSumInsuredFPObj = new MilkingSumInsuredFP();
            FPMachinery.MilkingSumInsuredFPObj.EiId = 62999;

            FPMachinery.MilkingTotalSumInsuredFPObj = new MilkingTotalSumInsuredFP();
            FPMachinery.MilkingTotalSumInsuredFPObj.EiId = 63001;

            FPMachinery.ShearingNoOfStandsFPObj = new ShearingNoOfStandsFP();
            FPMachinery.ShearingNoOfStandsFPObj.EiId = 63021;

            FPMachinery.ShearingSumInsuredFPObj = new ShearingSumInsuredFP();
            FPMachinery.ShearingSumInsuredFPObj.EiId = 63023;

            FPMachinery.ShearingTotalSumInsuredFPObj = new ShearingTotalSumInsuredFP();
            FPMachinery.ShearingTotalSumInsuredFPObj.EiId = 63025;

            FPMachinery.ExcessMachineryFPObj = new ExcessMachineryFP();
            FPMachinery.ExcessMachineryFPObj.ExcessList = excessToPay;
            FPMachinery.ExcessMachineryFPObj.EiId = 63051;


            FPMachinery.BolierTypeOfUnitFPObj = new BolierTypeOfUnitFP();
            FPMachinery.BolierTypeOfUnitFPObj.TypeofUnitList = MachineryTypeOfUnit;
            FPMachinery.BolierTypeOfUnitFPObj.EiId = 63067;

            FPMachinery.BolierMakeAndModelFPObj = new BolierMakeAndModelFP();
            FPMachinery.BolierMakeAndModelFPObj.EiId = 63069;

            FPMachinery.BolierRatedPowerFPObj = new BolierRatedPowerFP();
            FPMachinery.BolierMakeAndModelFPObj.EiId = 63071;

            FPMachinery.BolierPipeLengthFPObj = new BolierPipeLengthFP();
            FPMachinery.BolierPipeLengthFPObj.EiId = 63073;

            FPMachinery.BolierNoOfUnitsFPObj = new BolierNoOfUnitsFP();
            FPMachinery.BolierNoOfUnitsFPObj.EiId = 63075;

            FPMachinery.BolierSumInsuredFPObj = new BolierSpecMachSumInsuredFP();
            FPMachinery.BolierSumInsuredFPObj.EiId = 63077;

            FPMachinery.BolierTotalSumInsuredFPObj = new BolierTotalSumInsuredFP();
            FPMachinery.BolierTotalSumInsuredFPObj.EiId = 63079;

            FPMachinery.ExcessBolierFPObj = new ExcessBolierFP();
            FPMachinery.ExcessBolierFPObj.ExcessList = excessToPay;
            FPMachinery.ExcessBolierFPObj.EiId = 63083;

            FPMachinery.CoverMilkInVatsFPObj = new CoverMilkInVatsFP();
            FPMachinery.CoverMilkInVatsFPObj.EiId = 63087;

            FPMachinery.CoverAllOtherProduceFPObj = new CoverAllOtherProduceFP();
            FPMachinery.CoverAllOtherProduceFPObj.EiId = 63097;

            FPMachinery.ExcessCoverFPObj = new ExcessCoverFP();
            FPMachinery.ExcessCoverFPObj.ExcessList = excessToPay;
            FPMachinery.ExcessCoverFPObj.EiId = 63099;

            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.Machinery), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == FPMachinery.UnSpecTypeOfMachineryFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPMachinery.UnSpecTypeOfMachineryFPObj.EiId).FirstOrDefault();
                    FPMachinery.UnSpecTypeOfMachineryFPObj.TypeofMachinery = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.UnSpecPowerFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPMachinery.UnSpecPowerFPObj.EiId).FirstOrDefault();
                    FPMachinery.UnSpecPowerFPObj.Power = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.UnSpecMachNoOfUnitsFPObj.EiId))
                {
                    FPMachinery.UnSpecMachNoOfUnitsFPObj.NoOfUnits = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.UnSpecMachNoOfUnitsFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.UnSpecMachSumInsuredFPObj.EiId))
                {
                    FPMachinery.UnSpecMachSumInsuredFPObj.SumInsured = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.UnSpecMachSumInsuredFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.UnSpecMachTotalSumInsuredFPObj.EiId))
                {
                    FPMachinery.UnSpecMachTotalSumInsuredFPObj.TotalSumInsured = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.UnSpecMachTotalSumInsuredFPObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FPMachinery.MilkingVolumeOfVatFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPMachinery.MilkingVolumeOfVatFPObj.EiId).FirstOrDefault();
                    FPMachinery.MilkingVolumeOfVatFPObj.VolumeOfVat = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.MilkingNoOfVatsFPObj.EiId))
                {
                    FPMachinery.MilkingNoOfVatsFPObj.NoOfVats = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.MilkingNoOfVatsFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.MilkingSumInsuredFPObj.EiId))
                {
                    FPMachinery.MilkingSumInsuredFPObj.SumInsured = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.MilkingSumInsuredFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.MilkingTotalSumInsuredFPObj.EiId))
                {
                    FPMachinery.MilkingTotalSumInsuredFPObj.TotalSumInsured = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.MilkingTotalSumInsuredFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.ShearingNoOfStandsFPObj.EiId))
                {
                    FPMachinery.ShearingNoOfStandsFPObj.NoOfStands = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.ShearingNoOfStandsFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.ShearingSumInsuredFPObj.EiId))
                {
                    FPMachinery.ShearingSumInsuredFPObj.SumInsured = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.ShearingSumInsuredFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.ShearingTotalSumInsuredFPObj.EiId))
                {
                    FPMachinery.ShearingTotalSumInsuredFPObj.TotalSumInsured = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.ShearingTotalSumInsuredFPObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FPMachinery.ExcessMachineryFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPMachinery.ExcessMachineryFPObj.EiId).FirstOrDefault();
                    FPMachinery.ExcessMachineryFPObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == FPMachinery.BolierTypeOfUnitFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPMachinery.BolierTypeOfUnitFPObj.EiId).FirstOrDefault();
                    FPMachinery.BolierTypeOfUnitFPObj.TypeofUnit = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.BolierMakeAndModelFPObj.EiId))
                {
                    FPMachinery.BolierMakeAndModelFPObj.MakeAndModel = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.BolierMakeAndModelFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.BolierRatedPowerFPObj.EiId))
                {
                    FPMachinery.BolierRatedPowerFPObj.RatedPower = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.BolierRatedPowerFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.BolierPipeLengthFPObj.EiId))
                {
                    FPMachinery.BolierPipeLengthFPObj.PipeLength = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.BolierPipeLengthFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.BolierNoOfUnitsFPObj.EiId))
                {
                    FPMachinery.BolierNoOfUnitsFPObj.NoOfUnits = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.BolierNoOfUnitsFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.BolierSumInsuredFPObj.EiId))
                {
                    FPMachinery.BolierSumInsuredFPObj.SumInsured = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.BolierSumInsuredFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.BolierTotalSumInsuredFPObj.EiId))
                {
                    FPMachinery.BolierTotalSumInsuredFPObj.TotalSumInsured = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.BolierTotalSumInsuredFPObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FPMachinery.ExcessBolierFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPMachinery.ExcessBolierFPObj.EiId).FirstOrDefault();
                    FPMachinery.ExcessBolierFPObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.CoverMilkInVatsFPObj.EiId))
                {
                    FPMachinery.CoverMilkInVatsFPObj.MilkInVats = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.CoverMilkInVatsFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPMachinery.CoverAllOtherProduceFPObj.EiId))
                {
                    FPMachinery.CoverAllOtherProduceFPObj.AllOtherProduce = Convert.ToString(details.Where(q => q.QuestionId == FPMachinery.CoverAllOtherProduceFPObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FPMachinery.ExcessCoverFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPMachinery.ExcessCoverFPObj.EiId).FirstOrDefault();
                    FPMachinery.ExcessCoverFPObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

            }

            return View(FPMachinery);
        }

        [HttpPost]
        public ActionResult Machinery(int? cid, FPMachinery FPMachinery)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> MachineryItemTypeofUnit = new List<SelectListItem>();
            List<SelectListItem> PowerMachinery = new List<SelectListItem>();
            List<SelectListItem> VolumeOfVatMachinery = new List<SelectListItem>();
            List<SelectListItem> MachineryTypeOfUnit = new List<SelectListItem>();
            List<SelectListItem> excessToPay = new List<SelectListItem>();

            MachineryItemTypeofUnit = commonModel.TypeOfMachinery();
            PowerMachinery = commonModel.Power();
            VolumeOfVatMachinery = commonModel.VolumeOfVat();
            MachineryTypeOfUnit = commonModel.MachineryTypeOfUnit();
            excessToPay = commonModel.excessRate();

            FPMachinery.ExcessMachineryFPObj.ExcessList = excessToPay;
            FPMachinery.ExcessBolierFPObj.ExcessList = excessToPay;
            FPMachinery.ExcessCoverFPObj.ExcessList = excessToPay;

            FPMachinery.UnSpecTypeOfMachineryFPObj.TypeofMachineryList = MachineryItemTypeofUnit;
            FPMachinery.UnSpecPowerFPObj.UnSpecPowerList = PowerMachinery;
            FPMachinery.MilkingVolumeOfVatFPObj.VolumeOfVatList = VolumeOfVatMachinery;
            FPMachinery.BolierTypeOfUnitFPObj.TypeofUnitList = MachineryTypeOfUnit;

            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPMachinery.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPMachinery.CustomerId;
            }
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (FPMachinery.UnSpecTypeOfMachineryFPObj != null && FPMachinery.UnSpecTypeOfMachineryFPObj.EiId > 0 && FPMachinery.UnSpecTypeOfMachineryFPObj.TypeofMachinery != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.UnSpecTypeOfMachineryFPObj.EiId, FPMachinery.UnSpecTypeOfMachineryFPObj.TypeofMachinery.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.UnSpecPowerFPObj != null && FPMachinery.UnSpecPowerFPObj.EiId > 0 && FPMachinery.UnSpecPowerFPObj.Power != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.UnSpecPowerFPObj.EiId, FPMachinery.UnSpecPowerFPObj.Power.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.UnSpecMachNoOfUnitsFPObj != null && FPMachinery.UnSpecMachNoOfUnitsFPObj.EiId > 0 && FPMachinery.UnSpecMachNoOfUnitsFPObj.NoOfUnits != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.UnSpecMachNoOfUnitsFPObj.EiId, FPMachinery.UnSpecMachNoOfUnitsFPObj.NoOfUnits.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.UnSpecMachSumInsuredFPObj != null && FPMachinery.UnSpecMachSumInsuredFPObj.EiId > 0 && FPMachinery.UnSpecMachSumInsuredFPObj.SumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.UnSpecMachSumInsuredFPObj.EiId, FPMachinery.UnSpecMachSumInsuredFPObj.SumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.UnSpecMachTotalSumInsuredFPObj != null && FPMachinery.UnSpecMachTotalSumInsuredFPObj.EiId > 0 && FPMachinery.UnSpecMachTotalSumInsuredFPObj.TotalSumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.UnSpecMachTotalSumInsuredFPObj.EiId, FPMachinery.UnSpecMachTotalSumInsuredFPObj.TotalSumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.MilkingVolumeOfVatFPObj != null && FPMachinery.MilkingVolumeOfVatFPObj.EiId > 0 && FPMachinery.MilkingVolumeOfVatFPObj.VolumeOfVat != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.MilkingVolumeOfVatFPObj.EiId, FPMachinery.MilkingVolumeOfVatFPObj.VolumeOfVat.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.MilkingNoOfVatsFPObj != null && FPMachinery.MilkingNoOfVatsFPObj.EiId > 0 && FPMachinery.MilkingNoOfVatsFPObj.NoOfVats != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.MilkingNoOfVatsFPObj.EiId, FPMachinery.MilkingNoOfVatsFPObj.NoOfVats.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.MilkingSumInsuredFPObj != null && FPMachinery.MilkingSumInsuredFPObj.EiId > 0 && FPMachinery.MilkingSumInsuredFPObj.SumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.MilkingSumInsuredFPObj.EiId, FPMachinery.MilkingSumInsuredFPObj.SumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.MilkingTotalSumInsuredFPObj != null && FPMachinery.MilkingTotalSumInsuredFPObj.EiId > 0 && FPMachinery.MilkingTotalSumInsuredFPObj.TotalSumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.MilkingTotalSumInsuredFPObj.EiId, FPMachinery.MilkingTotalSumInsuredFPObj.TotalSumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.ShearingNoOfStandsFPObj != null && FPMachinery.ShearingNoOfStandsFPObj.EiId > 0 && FPMachinery.ShearingNoOfStandsFPObj.NoOfStands != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.ShearingNoOfStandsFPObj.EiId, FPMachinery.ShearingNoOfStandsFPObj.NoOfStands.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.ShearingSumInsuredFPObj != null && FPMachinery.ShearingSumInsuredFPObj.EiId > 0 && FPMachinery.ShearingSumInsuredFPObj.SumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.ShearingSumInsuredFPObj.EiId, FPMachinery.ShearingSumInsuredFPObj.SumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (FPMachinery.MilkingTotalSumInsuredFPObj != null && FPMachinery.MilkingTotalSumInsuredFPObj.EiId > 0 && FPMachinery.MilkingTotalSumInsuredFPObj.TotalSumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.MilkingTotalSumInsuredFPObj.EiId, FPMachinery.MilkingTotalSumInsuredFPObj.TotalSumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.ExcessMachineryFPObj != null && FPMachinery.ExcessMachineryFPObj.EiId > 0 && FPMachinery.ExcessMachineryFPObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.ExcessMachineryFPObj.EiId, FPMachinery.ExcessMachineryFPObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.BolierTypeOfUnitFPObj != null && FPMachinery.BolierTypeOfUnitFPObj.EiId > 0 && FPMachinery.BolierTypeOfUnitFPObj.TypeofUnit != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.BolierTypeOfUnitFPObj.EiId, FPMachinery.BolierTypeOfUnitFPObj.TypeofUnit.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (FPMachinery.BolierMakeAndModelFPObj != null && FPMachinery.BolierMakeAndModelFPObj.EiId > 0 && FPMachinery.BolierMakeAndModelFPObj.MakeAndModel != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.BolierMakeAndModelFPObj.EiId, FPMachinery.BolierMakeAndModelFPObj.MakeAndModel.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.BolierRatedPowerFPObj != null && FPMachinery.BolierRatedPowerFPObj.EiId > 0 && FPMachinery.BolierRatedPowerFPObj.RatedPower != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.BolierRatedPowerFPObj.EiId, FPMachinery.BolierRatedPowerFPObj.RatedPower.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.BolierPipeLengthFPObj != null && FPMachinery.BolierPipeLengthFPObj.EiId > 0 && FPMachinery.BolierPipeLengthFPObj.PipeLength != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.BolierPipeLengthFPObj.EiId, FPMachinery.BolierPipeLengthFPObj.PipeLength.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.BolierNoOfUnitsFPObj != null && FPMachinery.BolierNoOfUnitsFPObj.EiId > 0 && FPMachinery.BolierNoOfUnitsFPObj.NoOfUnits != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.BolierNoOfUnitsFPObj.EiId, FPMachinery.BolierNoOfUnitsFPObj.NoOfUnits.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.BolierSumInsuredFPObj != null && FPMachinery.BolierSumInsuredFPObj.EiId > 0 && FPMachinery.BolierSumInsuredFPObj.SumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.BolierSumInsuredFPObj.EiId, FPMachinery.BolierSumInsuredFPObj.SumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (FPMachinery.BolierTotalSumInsuredFPObj != null && FPMachinery.BolierTotalSumInsuredFPObj.EiId > 0 && FPMachinery.BolierTotalSumInsuredFPObj.TotalSumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.BolierTotalSumInsuredFPObj.EiId, FPMachinery.BolierTotalSumInsuredFPObj.TotalSumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.ExcessBolierFPObj != null && FPMachinery.ExcessBolierFPObj.EiId > 0 && FPMachinery.ExcessBolierFPObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.ExcessBolierFPObj.EiId, FPMachinery.ExcessBolierFPObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.CoverMilkInVatsFPObj != null && FPMachinery.CoverMilkInVatsFPObj.EiId > 0 && FPMachinery.CoverMilkInVatsFPObj.MilkInVats != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.CoverMilkInVatsFPObj.EiId, FPMachinery.CoverMilkInVatsFPObj.MilkInVats.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (FPMachinery.CoverAllOtherProduceFPObj != null && FPMachinery.CoverAllOtherProduceFPObj.EiId > 0 && FPMachinery.CoverAllOtherProduceFPObj.AllOtherProduce != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.CoverAllOtherProduceFPObj.EiId, FPMachinery.CoverAllOtherProduceFPObj.AllOtherProduce.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPMachinery.ExcessCoverFPObj != null && FPMachinery.ExcessCoverFPObj.EiId > 0 && FPMachinery.ExcessCoverFPObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FPMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.Machinery), FPMachinery.ExcessCoverFPObj.EiId, FPMachinery.ExcessCoverFPObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

            }
            return View(FPMachinery);
        }

    }
}