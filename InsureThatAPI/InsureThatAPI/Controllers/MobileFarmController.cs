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
    public class MobileFarmController : Controller
    {
        // GET: MobileFarm
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> FarmContents(int? cid, int? PcId)
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
                        if (Policyincllist.Exists(p => p.name == "Mobile Farm Property"))
                        {

                        } else  if (Policyincllist.Exists(p => p.name == "Burglary"))
                            {
                                return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid, PcId = PcId });
                            }
                            else if(Policyincllist.Exists(p => p.name == "Farm Interuption"))
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
                            if (Policyincllist.Exists(p => p.name == "Mobile Farm Property"))
                            {
                                if (Session["unId"] == null && Session["profileId"] == null)
                                {
                                    Session["unId"] = Policyincllist.Where(p => p.name == "Mobile Farm Property").Select(p => p.UnitId).First();
                                    Session["profileId"] = Policyincllist.Where(p => p.name == "Mobile Farm Property").Select(p => p.ProfileId).First();
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
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> desList = new List<SelectListItem>();
            desList = commonModel.descriptionLS();
            List<SelectListItem> excessforUMPay = new List<SelectListItem>();
            excessforUMPay = commonModel.excessRate();
            MobileFarmContents MobileFarmContents = new MobileFarmContents();
            #region Farm Contents details
            MobileFarmContents.FarmContentsSumInsuredFPObj = new FarmContentsSumInsuredFP();
            MobileFarmContents.FarmContentsSumInsuredFPObj.EiId = 62403;
            MobileFarmContents.OptPortableItemsFarmContentFPObj = new OptPortableItemsFarmContentFP();
            MobileFarmContents.OptPortableItemsFarmContentFPObj.EiId = 60405;
            MobileFarmContents.ExcessFarmContentFPObj = new ExcessFarmContentFP();
            MobileFarmContents.ExcessFarmContentFPObj.ExcessList = excessforUMPay;
            MobileFarmContents.ExcessFarmContentFPObj.EiId = 62407;
            #endregion
            #region Farm Machinery
            MobileFarmContents.FPUnspecifiedMachineryFMObj = new FPUnspecifiedMachineryFM();
            MobileFarmContents.FPUnspecifiedMachineryFMObj.EiId = 62423;
            MobileFarmContents.FPExcessforUMFMObj = new FPExcessforUMFM();
            MobileFarmContents.FPExcessforUMFMObj.ExcessUMList = excessforUMPay;
            MobileFarmContents.FPExcessforUMFMObj.EiId = 62425;
            MobileFarmContents.FPDescriptionsFMObj = new FPDescriptionsFM();
            MobileFarmContents.FPDescriptionsFMObj.EiId = 62431;
            MobileFarmContents.FPYearFMObj = new FPYearFM();
            MobileFarmContents.FPYearFMObj.EiId = 62433;
            MobileFarmContents.FPSerialNumberFMObj = new FPSerialNumberFM();
            MobileFarmContents.FPSerialNumberFMObj.EiId = 62435;
            MobileFarmContents.FPExcessFMObj = new FPExcessFM();
            MobileFarmContents.FPExcessFMObj.ExcessList = excessforUMPay;
            MobileFarmContents.FPExcessFMObj.EiId = 62437;
            MobileFarmContents.FPSumOfInsuredFMObj = new FPSumOfInsuredFM();
            MobileFarmContents.FPSumOfInsuredFMObj.EiId = 62439;
            #endregion
            #region Livestock
            MobileFarmContents.FPDescriptionLivestockObj = new FPDescriptionLivestock();
            MobileFarmContents.FPDescriptionLivestockObj.DescriptionList = desList;
            MobileFarmContents.FPDescriptionLivestockObj.EiId = 62467;
            MobileFarmContents.FPNumberOfAnimalsLivestockObj = new FPNumberOfAnimalsLivestock();
            MobileFarmContents.FPNumberOfAnimalsLivestockObj.EiId = 62469;
            MobileFarmContents.FPSumInsuredPerAnimalsLivestockObj = new FPSumInsuredPerAnimalsLivestock();
            MobileFarmContents.FPSumInsuredPerAnimalsLivestockObj.EiId = 62471;
            MobileFarmContents.FPTotalSumOfInsuredLivestockObj = new FPTotalSumOfInsuredLivestock();
            MobileFarmContents.FPTotalSumOfInsuredLivestockObj.EiId = 62473;
            MobileFarmContents.OptDogAttackLivestockObj = new OptDogAttackLivestock();
            MobileFarmContents.OptDogAttackLivestockObj.EiId = 62475;
            MobileFarmContents.FPExcessLivestockObj = new FPExcessLivestock();
            MobileFarmContents.FPExcessLivestockObj.ExcessList = excessforUMPay;
            MobileFarmContents.FPExcessLivestockObj.EiId = 62477;
            #endregion
            #region Working Dogs Beehives
            MobileFarmContents.FPSumOfInsuredPerDogObj = new FPSumOfInsuredPerDog();
            MobileFarmContents.FPSumOfInsuredPerDogObj.EiId = 62491;
            MobileFarmContents.FPNoOfWorkingDogsObj = new FPNoOfWorkingDogs();
            MobileFarmContents.FPNoOfWorkingDogsObj.EiId = 62493;
            MobileFarmContents.FPTotalSumInsuredWDBObj = new FPTotalSumInsuredWDB();
            MobileFarmContents.FPTotalSumInsuredWDBObj.EiId = 62495;
            MobileFarmContents.FPExcessWorkingDogsObj = new FPExcessWorkingDogs();
            MobileFarmContents.FPExcessWorkingDogsObj.ExcessList = excessforUMPay;
            MobileFarmContents.FPExcessWorkingDogsObj.EiId = 62497;
            MobileFarmContents.FPBeehivesSumInsuredObj = new FPBeehivesSumInsured();
            MobileFarmContents.FPBeehivesSumInsuredObj.EiId = 62503;
            MobileFarmContents.FPNumberOfHivesObj = new FPNumberOfHives();
            MobileFarmContents.FPNumberOfHivesObj.EiId = 62505;
            MobileFarmContents.FPExcessBeehivesObj = new FPExcessBeehives();
            MobileFarmContents.FPExcessBeehivesObj.ExcessList = excessforUMPay;
            MobileFarmContents.FPExcessBeehivesObj.EiId = 62507;
            #endregion
            if (cid != null)
            {
                ViewBag.cid = cid;
                MobileFarmContents.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MobileFarmContents.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = 0;
            int? profileid = 0;
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
                MobileFarmContents.PolicyInclusions = Policyincllist;
            }
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
                MobileFarmContents.PolicyInclusion = new List<usp_GetUnit_Result>();
                if (PcId != null && PcId.HasValue && PcId > 0)
                {
                    MobileFarmContents.PolicyInclusion = policyinclusions;
                }
                MobileFarmContents.PolicyInclusions = new List<SessionModel>();
                if (PcId != null && PcId > 0)
                {
                    policyid = PcId.ToString();
                    MobileFarmContents.PolicyId = policyid;
                }
                bool policyinclusion = policyinclusions.Exists(p => p.Name == "Mobile Farm Property");
                if (policyinclusion == true && PcId != null && PcId.HasValue)
                {
                    unid = policyinclusions.Where(p => p.Name == "Mobile Farm Property").Select(p => p.UnId).FirstOrDefault();
                    profileid = policyinclusions.Where(p => p.Name == "Mobile Farm Property").Select(p => p.ProfileUnId).FirstOrDefault();
                    HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                    var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    }
                }
                else
                {
                    return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                if (PcId == null && Session["unId"] == null && Session["profileId"] == null)
                {
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Mobile Farm Property&SectionUnId=&ProfileUnId=");
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails != null && unitdetails.SectionData != null)
                        {
                            if (unitdetails != null && unitdetails.ErrorMessage != null && unitdetails.ErrorMessage.Count() > 0 && unitdetails.Status=="Failure")
                            {
                                bool exists = MobileFarmContents.PolicyInclusions.Exists(p => p.name == "Mobile Farm Property");
                                if (exists == true)
                                {
                                    List<SessionModel> values = new List<SessionModel>();
                                    values = (List<SessionModel>)Session["Policyinclustions"];
                                    for (int k = 0; k < values.Count(); k++)
                                    {
                                        if (values[k].name == "Mobile Farm Property" && values[k].UnitId == null && values[k].ProfileId == null)
                                        {
                                            values.RemoveAt(k);
                                        }
                                    }
                                    Session["Policyinclustions"] = values;
                                }
                            }
                            Session["unId"] = unitdetails.SectionData.UnId;
                            //Session["FprofileId"] = unitdetails.SectionData.ProfileUnId;
                            Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                            if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Mobile Farm Property"))
                            {
                                var policyhomelist = Policyincllist.FindAll(p => p.name == "Mobile Farm Property").ToList();
                                if (policyhomelist != null && policyhomelist.Count() > 0)
                                {
                                    Policyincllist.FindAll(p => p.name == "Mobile Farm Property").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;

                                    Policyincllist.FindAll(p => p.name == "Mobile Farm Property").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                                else
                                {
                                    Policyincllist.FindAll(p => p.name == "Mobile Farm Property").First().UnitId = unitdetails.SectionData.UnId;

                                    Policyincllist.FindAll(p => p.name == "Mobile Farm Property").First().ProfileId = unitdetails.SectionData.ProfileUnId;
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
                if (unitdetails.SectionData!=null && unitdetails.SectionData.AddressData != null)
                {
                    MobileFarmContents.AddressObj = new AddressForFP();
                    MobileFarmContents.LocationObj = new LocatioForFP();
                    MobileFarmContents.AddressObj.Address = unitdetails.SectionData.AddressData.AddressLine1;
                    MobileFarmContents.LocationObj.Location = unitdetails.SectionData.AddressData.State + ", " + unitdetails.SectionData.AddressData.Suburb + ", " + unitdetails.SectionData.AddressData.Postcode; 
                }
                if (unitdetails.SectionData != null && unitdetails.SectionData.ValueData != null)
                {
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FarmContentsSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FarmContentsSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MobileFarmContents.FarmContentsSumInsuredFPObj.SumInsuredFC = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.OptPortableItemsFarmContentFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.OptPortableItemsFarmContentFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MobileFarmContents.OptPortableItemsFarmContentFPObj.OptPortalableItems = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.ExcessFarmContentFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.ExcessFarmContentFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MobileFarmContents.ExcessFarmContentFPObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPUnspecifiedMachineryFMObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPUnspecifiedMachineryFMObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MobileFarmContents.FPUnspecifiedMachineryFMObj.UnspecifiedMachinery = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPExcessforUMFMObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPExcessforUMFMObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MobileFarmContents.FPExcessforUMFMObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPDescriptionsFMObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPDescriptionsFMObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            MobileFarmContents.FPDescriptionsFMObj.DescriptionFM = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == MobileFarmContents.FPDescriptionsFMObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var FPDescriptionsList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPDescriptionsFMObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < FPDescriptionsList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62431;
                                vds.Element.ItId = FPDescriptionsList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPDescriptionsFMObj.EiId && p.Element.ItId == FPDescriptionsList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            MobileFarmContents.FPDescriptionsFMList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPYearFMObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPYearFMObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            MobileFarmContents.FPYearFMObj.YearFM = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == MobileFarmContents.FPYearFMObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var FPYearList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPYearFMObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < FPYearList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62433;
                                vds.Element.ItId = FPYearList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPYearFMObj.EiId && p.Element.ItId == FPYearList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            MobileFarmContents.FPYearFMList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPSerialNumberFMObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPSerialNumberFMObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            MobileFarmContents.FPSerialNumberFMObj.SerialNumberFM = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == MobileFarmContents.FPSerialNumberFMObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var FPSerialNumberList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPSerialNumberFMObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < FPSerialNumberList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62435;
                                vds.Element.ItId = FPSerialNumberList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPSerialNumberFMObj.EiId && p.Element.ItId == FPSerialNumberList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            MobileFarmContents.FPSerialNumberFMList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPExcessFMObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPExcessFMObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            MobileFarmContents.FPExcessFMObj.Excess = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == MobileFarmContents.FPExcessFMObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var FPExcessList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPExcessFMObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < FPExcessList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62437;
                                vds.Element.ItId = FPExcessList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPExcessFMObj.EiId && p.Element.ItId == FPExcessList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            MobileFarmContents.FPExcessFMList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPSumOfInsuredFMObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPSumOfInsuredFMObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            MobileFarmContents.FPSumOfInsuredFMObj.SuminsuredFM = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == MobileFarmContents.FPSumOfInsuredFMObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var FPSumOfInsuredList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPSumOfInsuredFMObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < FPSumOfInsuredList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62439;
                                vds.Element.ItId = FPSumOfInsuredList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPSumOfInsuredFMObj.EiId && p.Element.ItId == FPSumOfInsuredList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            MobileFarmContents.FPSumOfInsuredFMList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPDescriptionLivestockObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPDescriptionLivestockObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            MobileFarmContents.FPDescriptionLivestockObj.Description = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == MobileFarmContents.FPDescriptionLivestockObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var FPDLivestockList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPDescriptionLivestockObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < FPDLivestockList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62467;
                                vds.Element.ItId = FPDLivestockList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPDescriptionLivestockObj.EiId && p.Element.ItId == FPDLivestockList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            MobileFarmContents.FPDescriptionLivestockList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPNumberOfAnimalsLivestockObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPNumberOfAnimalsLivestockObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            MobileFarmContents.FPNumberOfAnimalsLivestockObj.NumberOfanimals = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == MobileFarmContents.FPNumberOfAnimalsLivestockObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var FPNMLivestockList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPNumberOfAnimalsLivestockObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < FPNMLivestockList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62469;
                                vds.Element.ItId = FPNMLivestockList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPNumberOfAnimalsLivestockObj.EiId && p.Element.ItId == FPNMLivestockList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            MobileFarmContents.FPNumberOfAnimalsLivestockList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPNumberOfAnimalsLivestockObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPSumInsuredPerAnimalsLivestockObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            MobileFarmContents.FPSumInsuredPerAnimalsLivestockObj.SumInsuredPerAnimal = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == MobileFarmContents.FPSumInsuredPerAnimalsLivestockObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var FPSPALivestockList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPSumInsuredPerAnimalsLivestockObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < FPSPALivestockList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62471;
                                vds.Element.ItId = FPSPALivestockList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPSumInsuredPerAnimalsLivestockObj.EiId && p.Element.ItId == FPSPALivestockList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            MobileFarmContents.FPSumInsuredPerAnimalsLivestockList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPTotalSumOfInsuredLivestockObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPTotalSumOfInsuredLivestockObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            MobileFarmContents.FPTotalSumOfInsuredLivestockObj.TotalSumOfInsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == MobileFarmContents.FPTotalSumOfInsuredLivestockObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var FPTSLivestockList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPTotalSumOfInsuredLivestockObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < FPTSLivestockList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 62473;
                                vds.Element.ItId = FPTSLivestockList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPTotalSumOfInsuredLivestockObj.EiId && p.Element.ItId == FPTSLivestockList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            MobileFarmContents.FPTotalSumOfInsuredLivestockList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.OptDogAttackLivestockObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.OptDogAttackLivestockObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MobileFarmContents.OptDogAttackLivestockObj.OptDogAttack = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPExcessLivestockObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPExcessLivestockObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MobileFarmContents.FPExcessLivestockObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPSumOfInsuredPerDogObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPSumOfInsuredPerDogObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MobileFarmContents.FPSumOfInsuredPerDogObj.SumInsuredPerDog = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPNoOfWorkingDogsObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPNoOfWorkingDogsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MobileFarmContents.FPNoOfWorkingDogsObj.NoOfWorkingDogs = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPTotalSumInsuredWDBObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPTotalSumInsuredWDBObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MobileFarmContents.FPTotalSumInsuredWDBObj.TotalSumInsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MobileFarmContents.FPExcessWorkingDogsObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MobileFarmContents.FPExcessWorkingDogsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MobileFarmContents.FPExcessWorkingDogsObj.Excess = val;
                    }
                }
            }
            if (cid != null && cid.HasValue)
            {
                MobileFarmContents.CustomerId = cid.Value;
            }
            if (PcId != null && PcId.HasValue)
            {
                MobileFarmContents.PcId = PcId;
            }
            Session["Controller"] = "FarmPolicyMobileFarm";
            Session["ActionName"] = "FarmContents";
            return View(MobileFarmContents);
        }
        [HttpPost]
        public ActionResult FarmContents(MobileFarmContents MobileFarmContents, int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();

            List<SelectListItem> excessToPay = new List<SelectListItem>();
            excessToPay = commonModel.excessRate();
            MobileFarmContents.ExcessFarmContentFPObj.ExcessList = excessToPay;

            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                MobileFarmContents.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MobileFarmContents.CustomerId;
            }
            string policyid = null;
            Session["unId"] = null;
            Session["profileId"] = null;
            Session["farmmobile"] = 1;
            return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = MobileFarmContents.CustomerId, PcId = MobileFarmContents.PcId });

            return View(MobileFarmContents);
        }
        [HttpPost]
        public ActionResult FarmAjaxcontent(int id, string content)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            if (Request.IsAjaxRequest())
            {
                int cid = 1;
                ViewBag.cid = cid;
                if (content == "farmContents")
                {
                    List<SelectListItem> DescriptionListFContent = new List<SelectListItem>();
                    List<SelectListItem> DescriptionListFContentb = new List<SelectListItem>();
                    DescriptionListFContent.Add(new SelectListItem { Value = "", Text = "--Select--" });
                    DescriptionListFContentb = commonModel.descriptionListFC();
                    DescriptionListFContent.AddRange(DescriptionListFContentb);

                    List<SelectListItem> constructionTypeFC = new List<SelectListItem>();
                    List<SelectListItem> constructionTypeFCb = new List<SelectListItem>();
                    constructionTypeFC.Add(new SelectListItem { Value = "", Text = "--Select--" });
                    constructionTypeFCb = commonModel.constructionType();
                    constructionTypeFC.AddRange(constructionTypeFCb);
                    return Json(new { status = true, des = DescriptionListFContent, con = constructionTypeFC });
                }
                else if (content == "farmMachinery")
                {
                    List<SelectListItem> excessforUMPay = new List<SelectListItem>();
                    List<SelectListItem> excessforUMPayB = new List<SelectListItem>();
                    excessforUMPay.Add(new SelectListItem { Value = "", Text = "--Select--" });
                    excessforUMPayB = commonModel.excessRate();
                    excessforUMPay.AddRange(excessforUMPayB);
                    return Json(new { status = true, des = excessforUMPay });
                }
                else if (content == "Livestock")
                {
                    List<SelectListItem> desList = new List<SelectListItem>();
                    List<SelectListItem> desListB = new List<SelectListItem>();
                    desList.Add(new SelectListItem { Value = "", Text = "--Select--" });
                    desListB = commonModel.descriptionLS();
                    desList.AddRange(desListB);
                    return Json(new { status = true, des = desList });
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