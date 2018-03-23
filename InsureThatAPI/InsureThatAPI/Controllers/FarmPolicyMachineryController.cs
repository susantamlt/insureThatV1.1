using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;
using InsureThatAPI.CommonMethods;
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
        public async System.Threading.Tasks.Task<ActionResult> Machinery(int? cid, int? PcId)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> MachineryItemTypeofUnit = new List<SelectListItem>();
            List<SelectListItem> PowerMachinery = new List<SelectListItem>();
            List<SelectListItem> VolumeOfVatMachinery = new List<SelectListItem>();
            List<SelectListItem> MachineryTypeOfUnit = new List<SelectListItem>();
            List<SelectListItem> SMachineryTypeOfUnit = new List<SelectListItem>();
            List<SelectListItem> excessToPay = new List<SelectListItem>();
            string ApiKey = null;
            if (Session["ApiKey"] != null)
            {
                ApiKey = Session["ApiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            MachineryItemTypeofUnit = commonModel.TypeOfMachinery();
            PowerMachinery = commonModel.Power();
            VolumeOfVatMachinery = commonModel.VolumeOfVat();
            MachineryTypeOfUnit = commonModel.MachineryTypeOfUnit();
            excessToPay = commonModel.excessRate();
            SMachineryTypeOfUnit = commonModel.MachineryTypeOfUnitSpefic();

            FPMachinery FPMachinery = new FPMachinery();

            var Policyincllist = new List<SessionModel>();
            if (Session["Policyinclustions"] != null)
            {
                Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Exists(p => p.name == "Machinery"))
                    {

                    }                 
                          else if (Policyincllist.Exists(p => p.name == "Electronics"))
                        {
                            return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Transit"))
                        {
                            return RedirectToAction("Transit", "FarmPolicyTransit", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "LiveStock"))
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
                        if (Policyincllist.Exists(p => p.name == "Machinery"))
                        {
                            if (Session["unId"] == null && Session["profileId"] == null)
                            {
                                Session["unId"] = Policyincllist.Where(p => p.name == "Machinery").Select(p => p.UnitId).First();
                                Session["profileId"] = Policyincllist.Where(p => p.name == "Machinery").Select(p => p.ProfileId).First();
                            }
                        }
                        else
                        {
                            return RedirectToAction("DisclosureDetails", "Disclosure", new { cid = cid, PcId = PcId });

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

            FPMachinery.SMTypeObj = new SpecifiedMachineryType();
            FPMachinery.SMTypeObj.TypeSMList = SMachineryTypeOfUnit;
            FPMachinery.SMTypeObj.EiId = 63039;
            FPMachinery.SMRatedpowerObj = new SpecifiedMachineryRatedpower();
            FPMachinery.SMRatedpowerObj.EiId = 63041;
            FPMachinery.SMNumberOfUnitsObj = new SpecifiedMachineryNumberOfUnits();
            FPMachinery.SMNumberOfUnitsObj.EiId = 63043;
            FPMachinery.SMSumInsuredPerUnitObj = new SpecifiedMachinerySumInsuredPerUnit();
            FPMachinery.SMSumInsuredPerUnitObj.EiId = 63045;
            FPMachinery.SMTotalSumInsuredObj = new SpecifiedMachineryTotalSumInsured();
            FPMachinery.SMTotalSumInsuredObj.EiId = 63047;

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
            FPMachinery.AddressObj = new AddressMAddress();
            FPMachinery.AddressObj.EiId = 0;

            var db = new MasterDataEntities();
            string policyid = null;
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();

            if (cid != null)
            {
                ViewBag.cid = cid;
                FPMachinery.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPMachinery.CustomerId;
            }
            
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = 0;
            int profileid = 0;
            int Fprofileid = 0;

            if (cid != null)
            {
                ViewBag.cid = cid;
                FPMachinery.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPMachinery.CustomerId;
            }
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
                FPMachinery.PolicyInclusions = Policyincllist;
            }
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
                FPMachinery.PolicyInclusion = new List<usp_GetUnit_Result>();
                if (PcId != null && PcId.HasValue && PcId > 0)
                {
                    FPMachinery.PolicyInclusion = policyinclusions;
                }
                FPMachinery.PolicyInclusions = new List<SessionModel>();
                if (PcId != null && PcId > 0)
                {
                    policyid = PcId.ToString();
                    FPMachinery.PolicyId = policyid;
                }
                bool policyinclusion = policyinclusions.Exists(p => p.Name == "Machinery");
                if (policyinclusion == true && PcId != null && PcId.HasValue)
                {

                    int sectionId = policyinclusions.Where(p => p.Name == "Machinery" && p.UnitNumber == unid).Select(p => p.UnId).FirstOrDefault();
                    int? profileunid = policyinclusions.Where(p => p.Name == "Machinery" && p.ProfileUnId == profileid).Select(p => p.ProfileUnId).FirstOrDefault();
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
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Machinery&SectionUnId=&ProfileUnId=" + Fprofileid);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails != null && unitdetails.SectionData != null)
                        {
                            Session["unId"] = unitdetails.SectionData.UnId;

                            Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                            if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Machinery"))
                            {
                                var policyhomelist = Policyincllist.FindAll(p => p.name == "Machinery").ToList();
                                if (policyhomelist != null && policyhomelist.Count() > 0)
                                {
                                    Policyincllist.FindAll(p => p.name == "Machinery").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;

                                    Policyincllist.FindAll(p => p.name == "Machinery").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                                else
                                {
                                    Policyincllist.FindAll(p => p.name == "Machinery").First().UnitId = unitdetails.SectionData.UnId;

                                    Policyincllist.FindAll(p => p.name == "Machinery").First().ProfileId = unitdetails.SectionData.ProfileUnId;
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
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.SMTypeObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMTypeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.SMTypeObj.Typesm = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.SMRatedpowerObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMRatedpowerObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.SMRatedpowerObj.Ratedpowersm = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.SMNumberOfUnitsObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMNumberOfUnitsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.SMNumberOfUnitsObj.Numberunitssm = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.SMSumInsuredPerUnitObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMSumInsuredPerUnitObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.SMSumInsuredPerUnitObj.SumInsuredperunit = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.SMTotalSumInsuredObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMTotalSumInsuredObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.SMTotalSumInsuredObj.SumInsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.BolierMakeAndModelFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierMakeAndModelFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.BolierMakeAndModelFPObj.MakeAndModel = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.BolierNoOfUnitsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierNoOfUnitsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.BolierNoOfUnitsFPObj.NoOfUnits = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.BolierPipeLengthFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierPipeLengthFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.BolierPipeLengthFPObj.PipeLength = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.BolierRatedPowerFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierRatedPowerFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.BolierRatedPowerFPObj.RatedPower = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.BolierSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.BolierSumInsuredFPObj.SumInsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.BolierTotalSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierTotalSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.BolierTotalSumInsuredFPObj.TotalSumInsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.BolierTypeOfUnitFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierTypeOfUnitFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.BolierTypeOfUnitFPObj.TypeofUnit = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.CoverAllOtherProduceFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.CoverAllOtherProduceFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.CoverAllOtherProduceFPObj.AllOtherProduce = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.CoverMilkInVatsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.CoverMilkInVatsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.CoverMilkInVatsFPObj.MilkInVats = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.ExcessBolierFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.ExcessBolierFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.ExcessBolierFPObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.ExcessCoverFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.ExcessCoverFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.ExcessCoverFPObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.ExcessMachineryFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.ExcessMachineryFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.ExcessMachineryFPObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.MilkingNoOfVatsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingNoOfVatsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.MilkingNoOfVatsFPObj.NoOfVats = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.MilkingSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.MilkingSumInsuredFPObj.SumInsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.MilkingTotalSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingTotalSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.MilkingTotalSumInsuredFPObj.TotalSumInsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.MilkingVolumeOfVatFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingVolumeOfVatFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.MilkingVolumeOfVatFPObj.VolumeOfVat = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.ShearingNoOfStandsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.ShearingNoOfStandsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.ShearingNoOfStandsFPObj.NoOfStands = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.ShearingSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.ShearingSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.ShearingSumInsuredFPObj.SumInsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.ShearingTotalSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.ShearingTotalSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.ShearingTotalSumInsuredFPObj.TotalSumInsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.UnSpecMachNoOfUnitsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecMachNoOfUnitsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.UnSpecMachNoOfUnitsFPObj.NoOfUnits = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.UnSpecMachSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecMachSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.UnSpecMachSumInsuredFPObj.SumInsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.UnSpecMachTotalSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecMachTotalSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.UnSpecMachTotalSumInsuredFPObj.TotalSumInsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.UnSpecPowerFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecPowerFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.UnSpecPowerFPObj.Power = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.UnSpecTypeOfMachineryFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecTypeOfMachineryFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPMachinery.UnSpecTypeOfMachineryFPObj.TypeofMachinery = val;
                    }
                }
                if (unitdetails.SectionData != null && unitdetails.SectionData.AddressData != null)
                {
                    if (unitdetails.SectionData.AddressData != null)
                    {
                        FPMachinery.AddressObj.Address = unitdetails.SectionData.AddressData.AddressLine1 + ", " + unitdetails.SectionData.AddressData.Suburb + " ," + unitdetails.SectionData.AddressData.State + ", " + unitdetails.SectionData.AddressData.Postcode;
                    }
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
                cid = FPMachinery.CustomerId;
            }
            string policyid = null;
            Session["unId"] = null;
            Session["profileId"] = null;
            return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = FPMachinery.CustomerId, PcId = FPMachinery.PcId });
            
        }

    }
}