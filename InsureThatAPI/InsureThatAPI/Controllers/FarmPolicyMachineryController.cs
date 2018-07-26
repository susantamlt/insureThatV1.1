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
            FPMachinery.BolierRatedPowerFPObj.EiId = 63071;
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
                    int sectionId = policyinclusions.Where(p => p.Name == "Machinery").Select(p => p.UnId).FirstOrDefault();
                    int? profileunid = policyinclusions.Where(p => p.Name == "Machinery").Select(p => p.ProfileUnId).FirstOrDefault();
                    HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + sectionId + "&ProfileUnId=" + profileunid);
                    var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    }
                }
                else
                {
                    return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                if (PcId == null && Session["unId"] == null && Session["profileId"] == null)
                {
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Machinery&SectionUnId=&ProfileUnId=0");
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
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.UnSpecTypeOfMachineryFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecTypeOfMachineryFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.UnSpecTypeOfMachineryFPObj.TypeofMachinery = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.UnSpecTypeOfMachineryFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var UnSpecTypeOfMachineryList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecTypeOfMachineryFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < UnSpecTypeOfMachineryList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62967;
                                vds.Element.ItId = UnSpecTypeOfMachineryList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecTypeOfMachineryFPObj.EiId && p.Element.ItId == UnSpecTypeOfMachineryList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.UnSpecTypeOfMachineryFPList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.UnSpecPowerFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecPowerFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.UnSpecPowerFPObj.Power = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.UnSpecPowerFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var UnSpecPowerList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecPowerFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < UnSpecPowerList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62969;
                                vds.Element.ItId = UnSpecPowerList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecPowerFPObj.EiId && p.Element.ItId == UnSpecPowerList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.UnSpecPowerFPList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.UnSpecMachNoOfUnitsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecMachNoOfUnitsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.UnSpecMachNoOfUnitsFPObj.NoOfUnits = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.UnSpecMachNoOfUnitsFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var UnSpecMachNoOfUnitList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecMachNoOfUnitsFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < UnSpecMachNoOfUnitList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62971;
                                vds.Element.ItId = UnSpecMachNoOfUnitList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecMachNoOfUnitsFPObj.EiId && p.Element.ItId == UnSpecMachNoOfUnitList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.UnSpecMachNoOfUnitsFPList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.UnSpecMachSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecMachSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.UnSpecMachSumInsuredFPObj.SumInsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.UnSpecMachSumInsuredFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var UnSpecMachSumInsuredList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecMachSumInsuredFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < UnSpecMachSumInsuredList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62973;
                                vds.Element.ItId = UnSpecMachSumInsuredList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecMachSumInsuredFPObj.EiId && p.Element.ItId == UnSpecMachSumInsuredList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.UnSpecMachSumInsuredFPList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.UnSpecMachTotalSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecMachTotalSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.UnSpecMachTotalSumInsuredFPObj.TotalSumInsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.UnSpecMachTotalSumInsuredFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var UnSpecMachTotalSumInsuredList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecMachTotalSumInsuredFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < UnSpecMachTotalSumInsuredList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62975;
                                vds.Element.ItId = UnSpecMachTotalSumInsuredList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.UnSpecMachTotalSumInsuredFPObj.EiId && p.Element.ItId == UnSpecMachTotalSumInsuredList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.UnSpecMachTotalSumInsuredFPList = elmnts;
                        }
                    }

                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.MilkingVolumeOfVatFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingVolumeOfVatFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.MilkingVolumeOfVatFPObj.VolumeOfVat = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.MilkingVolumeOfVatFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var MilkingVolumeOfVatList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingVolumeOfVatFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < MilkingVolumeOfVatList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62995;
                                vds.Element.ItId = MilkingVolumeOfVatList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingVolumeOfVatFPObj.EiId && p.Element.ItId == MilkingVolumeOfVatList[i]).Select(p => p.Value).FirstOrDefault();
                                if (vds.Value != null && vds.Value != "")
                                {
                                    elmnts.Add(vds);
                                }
                            }
                            FPMachinery.MilkingVolumeOfVatFPList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.MilkingNoOfVatsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingNoOfVatsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.MilkingNoOfVatsFPObj.NoOfVats = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.MilkingNoOfVatsFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var MilkingNoOfVatsList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingNoOfVatsFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < MilkingNoOfVatsList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62997;
                                vds.Element.ItId = MilkingNoOfVatsList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingNoOfVatsFPObj.EiId && p.Element.ItId == MilkingNoOfVatsList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.MilkingNoOfVatsFPList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.MilkingSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.MilkingSumInsuredFPObj.SumInsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.MilkingSumInsuredFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var MilkingSumInsuredList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingSumInsuredFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < MilkingSumInsuredList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62999;
                                vds.Element.ItId = MilkingSumInsuredList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingSumInsuredFPObj.EiId && p.Element.ItId == MilkingSumInsuredList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.MilkingSumInsuredFPList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.MilkingTotalSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingTotalSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.MilkingTotalSumInsuredFPObj.TotalSumInsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.MilkingTotalSumInsuredFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var MilkingTotalSumInsuredList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingTotalSumInsuredFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < MilkingTotalSumInsuredList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63001;
                                vds.Element.ItId = MilkingTotalSumInsuredList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.MilkingTotalSumInsuredFPObj.EiId && p.Element.ItId == MilkingTotalSumInsuredList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.MilkingTotalSumInsuredFPList = elmnts;
                        }
                    }

                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.SMTypeObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMTypeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.SMTypeObj.Typesm = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.SMTypeObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var SMTypeList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMTypeObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < SMTypeList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63039;
                                vds.Element.ItId = SMTypeList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMTypeObj.EiId && p.Element.ItId == SMTypeList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.SMTypeList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.SMRatedpowerObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMRatedpowerObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.SMRatedpowerObj.Ratedpowersm = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.SMRatedpowerObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var SMRatedpowerList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMRatedpowerObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < SMRatedpowerList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63041;
                                vds.Element.ItId = SMRatedpowerList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMRatedpowerObj.EiId && p.Element.ItId == SMRatedpowerList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.SMRatedpowerList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.SMNumberOfUnitsObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMNumberOfUnitsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.SMNumberOfUnitsObj.Numberunitssm = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.SMNumberOfUnitsObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var SMNumberOfUnitsList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMNumberOfUnitsObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < SMNumberOfUnitsList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63043;
                                vds.Element.ItId = SMNumberOfUnitsList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMNumberOfUnitsObj.EiId && p.Element.ItId == SMNumberOfUnitsList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.SMNumberOfUnitsList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.SMSumInsuredPerUnitObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMSumInsuredPerUnitObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.SMSumInsuredPerUnitObj.SumInsuredperunit = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.SMSumInsuredPerUnitObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var SMSumInsuredPerUnitList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMSumInsuredPerUnitObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < SMSumInsuredPerUnitList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63045;
                                vds.Element.ItId = SMSumInsuredPerUnitList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMSumInsuredPerUnitObj.EiId && p.Element.ItId == SMSumInsuredPerUnitList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.SMSumInsuredPerUnitList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.SMTotalSumInsuredObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMTotalSumInsuredObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.SMTotalSumInsuredObj.SumInsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.SMTotalSumInsuredObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var SMTotalSumInsuredList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMTotalSumInsuredObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < SMTotalSumInsuredList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63047;
                                vds.Element.ItId = SMTotalSumInsuredList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.SMTotalSumInsuredObj.EiId && p.Element.ItId == SMTotalSumInsuredList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.SMTotalSumInsuredList = elmnts;
                        }
                    }

                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.ShearingNoOfStandsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.ShearingNoOfStandsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.ShearingNoOfStandsFPObj.NoOfStands = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.ShearingNoOfStandsFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var ShearingNoOfStandsList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.ShearingNoOfStandsFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < ShearingNoOfStandsList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63021;
                                vds.Element.ItId = ShearingNoOfStandsList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.ShearingNoOfStandsFPObj.EiId && p.Element.ItId == ShearingNoOfStandsList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.ShearingNoOfStandsFPList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.ShearingSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.ShearingSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.ShearingSumInsuredFPObj.SumInsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.ShearingSumInsuredFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var ShearingSumInsuredList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.ShearingSumInsuredFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < ShearingSumInsuredList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63023;
                                vds.Element.ItId = ShearingSumInsuredList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.ShearingSumInsuredFPObj.EiId && p.Element.ItId == ShearingSumInsuredList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.ShearingSumInsuredFPList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.ShearingTotalSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.ShearingTotalSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.ShearingTotalSumInsuredFPObj.TotalSumInsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.ShearingTotalSumInsuredFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var ShearingTotalSumInsuredList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.ShearingTotalSumInsuredFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < ShearingTotalSumInsuredList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63025;
                                vds.Element.ItId = ShearingTotalSumInsuredList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.ShearingTotalSumInsuredFPObj.EiId && p.Element.ItId == ShearingTotalSumInsuredList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.ShearingTotalSumInsuredFPList = elmnts;
                        }
                    }

                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.BolierTypeOfUnitFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierTypeOfUnitFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.BolierTypeOfUnitFPObj.TypeofUnit = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.BolierTypeOfUnitFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var BolierTypeOfUnitList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierTypeOfUnitFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < BolierTypeOfUnitList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63067;
                                vds.Element.ItId = BolierTypeOfUnitList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierTypeOfUnitFPObj.EiId && p.Element.ItId == BolierTypeOfUnitList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.BolierTypeOfUnitFPList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.BolierMakeAndModelFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierMakeAndModelFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.BolierMakeAndModelFPObj.MakeAndModel = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.BolierMakeAndModelFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var BolierMakeAndModelList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierMakeAndModelFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < BolierMakeAndModelList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63069;
                                vds.Element.ItId = BolierMakeAndModelList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierMakeAndModelFPObj.EiId && p.Element.ItId == BolierMakeAndModelList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.BolierMakeAndModelFPList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.BolierRatedPowerFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierRatedPowerFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.BolierRatedPowerFPObj.RatedPower = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.BolierRatedPowerFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var BolierRatedPowerList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierRatedPowerFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < BolierRatedPowerList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63071;
                                vds.Element.ItId = BolierRatedPowerList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierRatedPowerFPObj.EiId && p.Element.ItId == BolierRatedPowerList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.BolierRatedPowerFPList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.BolierPipeLengthFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierPipeLengthFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.BolierPipeLengthFPObj.PipeLength = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.BolierPipeLengthFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var BolierPipeLengthList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierPipeLengthFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < BolierPipeLengthList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63073;
                                vds.Element.ItId = BolierPipeLengthList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierPipeLengthFPObj.EiId && p.Element.ItId == BolierPipeLengthList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.BolierPipeLengthFPList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.BolierNoOfUnitsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierNoOfUnitsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.BolierNoOfUnitsFPObj.NoOfUnits = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.BolierNoOfUnitsFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var BolierNoOfUnitsList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierNoOfUnitsFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < BolierNoOfUnitsList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63075;
                                vds.Element.ItId = BolierNoOfUnitsList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierNoOfUnitsFPObj.EiId && p.Element.ItId == BolierNoOfUnitsList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.BolierNoOfUnitsFPList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.BolierSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.BolierSumInsuredFPObj.SumInsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.BolierSumInsuredFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var BolierSumInsuredList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierSumInsuredFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < BolierSumInsuredList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63077;
                                vds.Element.ItId = BolierSumInsuredList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierSumInsuredFPObj.EiId && p.Element.ItId == BolierSumInsuredList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.BolierSumInsuredFPList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPMachinery.BolierTotalSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierTotalSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPMachinery.BolierTotalSumInsuredFPObj.TotalSumInsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPMachinery.BolierTotalSumInsuredFPObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var BolierTotalSumInsuredList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierTotalSumInsuredFPObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < BolierTotalSumInsuredList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63079;
                                vds.Element.ItId = BolierTotalSumInsuredList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPMachinery.BolierTotalSumInsuredFPObj.EiId && p.Element.ItId == BolierTotalSumInsuredList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPMachinery.BolierTotalSumInsuredFPList = elmnts;
                        }
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
                }
                if (unitdetails.SectionData != null && unitdetails.SectionData.AddressData != null)
                {
                    if (unitdetails.SectionData.AddressData != null)
                    {
                        FPMachinery.AddressObj.Address = unitdetails.SectionData.AddressData.AddressLine1 + ", " + unitdetails.SectionData.AddressData.Suburb + " ," + unitdetails.SectionData.AddressData.State + ", " + unitdetails.SectionData.AddressData.Postcode;
                    }
                }
            }
            if (cid != null && cid.HasValue)
            {
                FPMachinery.CustomerId = cid.Value;
            }
            if (PcId != null && PcId.HasValue)
            {
                FPMachinery.PcId = PcId;
            }
            Session["Controller"] = "FarmPolicyMachinery";
            Session["ActionName"] = "Machinery";
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
            Session["unId"] = null;
            Session["profileId"] = null;
            return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = FPMachinery.CustomerId, PcId = FPMachinery.PcId });
            
        }

    }
}