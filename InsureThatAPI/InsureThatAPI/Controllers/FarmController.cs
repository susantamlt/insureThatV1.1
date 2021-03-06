﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;
using InsureThatAPI.CommonMethods;
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace InsureThatAPI.Controllers
{
    public class FarmController : Controller
    {
        // GET: Farm
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> FarmContents(int? cid, int? PcId)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> DescriptionListFContent = new List<SelectListItem>();
            DescriptionListFContent = commonModel.descriptionListFC();
            List<SelectListItem> constructionTypeFC = new List<SelectListItem>();
            constructionTypeFC = commonModel.constructionType();
            List<SelectListItem> excessToPay = new List<SelectListItem>();
            excessToPay = commonModel.excessRate();
            List<SelectListItem> desList = new List<SelectListItem>();
            desList = commonModel.descriptionLS();
            FarmContents FarmContents = new FarmContents();
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                FarmContents.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FarmContents.CustomerId;
            }
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            NewPolicyDetailsClass commonmethods = new NewPolicyDetailsClass();
            var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
            string apikey = null;
            bool policyinclusion = policyinclusions.Exists(p => p.Name == "Farm Property");
            CommonUseFunctionClass cmn = new CommonUseFunctionClass();
            FarmContents.NewSections = new List<string>();
            if (Session["apiKey"] != null)
            {
                apikey = Session["apiKey"].ToString();
                FarmContents.ApiKey = Session["apiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            string policyid = null;
            var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                policyid = PcId.ToString();
                FarmContents.PolicyId = policyid;
            }
            else if (Session["Policyinclustions"] != null)
            {
                #region Policy Selected or not testing
                List<SessionModel> PolicyInclustions = new List<SessionModel>();
                FarmContents.PolicyInclusions = new List<SessionModel>();
                FarmContents.PolicyInclusions = Policyincllist;
                FarmContents.NewSections = cmn.NewSectionHome(FarmContents.PolicyInclusions);
                if (Policyincllist != null)
                {

                    if (Policyincllist.Exists(p => p.name == "Farm Property"))
                    {

                    }
                    else if (Policyincllist.Exists(p => p.name == "Liability"))
                    {
                        return RedirectToAction("LiabilityCover", "Liabilities", new { cid = cid });
                    }
                    else if (Policyincllist.Exists(p => p.name == "Motor" || p.name == "Motors"))
                    {
                        return RedirectToAction("VehicleDescription", "MotorCover", new { cid = cid });
                    }
                    else if (Policyincllist.Exists(p => p.name == "Boat"))
                    {
                        return RedirectToAction("BoatDetails", "Boat", new { cid = cid });
                    }

                    else if (Policyincllist.Exists(p => p.name == "Pet" || p.name == "Pets"))
                    {
                        return RedirectToAction("PetsCover", "Pets", new { cid = cid });
                    }
                    else if (Policyincllist.Exists(p => p.name == "Travel"))
                    {
                        return RedirectToAction("TravelCover", "Travel", new { cid = cid });
                    }

                    if (Policyincllist.Exists(p => p.name == "Farm Property"))
                    {
                        if (Session["unId"] == null && Session["profileId"] == null)
                        {
                            Session["unId"] = Policyincllist.Where(p => p.name == "Farm Property").Select(p => p.UnitId).First();
                            Session["profileId"] = Policyincllist.Where(p => p.name == "Farm Property").Select(p => p.ProfileId).First();
                        }
                    }
                    else
                    {
                        return RedirectToAction("DisclosureDetails", "Disclosure", new { cid = cid, PcId = PcId });
                    }
                    //else
                    //{
                    //    return RedirectToAction("PremiumDetails", "Customer", new { cid = cid, PcId = PcId });
                    //}
                }
                #endregion
            }
            #region FarmContents
            FarmContents.ExistingPolicyInclustions = new List<usp_GetUnit_Result>();
            FarmContents.DescriptionFCObj = new DescriptionsFC();
            FarmContents.DescriptionFCObj.DescriptionList = DescriptionListFContent;
            FarmContents.DescriptionFCObj.EiId = 60467;
            FarmContents.YearObj = new YearFPC();
            FarmContents.YearObj.EiId = 60468;
            FarmContents.MaterialsObj = new MaterialsFC();
            FarmContents.MaterialsObj.MaterialsList = constructionTypeFC;
            FarmContents.MaterialsObj.EiId = 60470;
            FarmContents.CoolroomFcObj = new CoolroomsFC();
            FarmContents.CoolroomFcObj.EiId = 60469;
            FarmContents.SuminsuredObj = new SumOfInsured();
            FarmContents.SuminsuredObj.EiId = 60471;
            FarmContents.confirmfsObj = new confirmFarmStructures();
            FarmContents.confirmfsObj.EiId = 60491;
            FarmContents.FarmfencingObj = new FarmFencingFC();
            FarmContents.FarmfencingObj.EiId = 60495;
            FarmContents.FarmcencingTcObj = new FarmFencingTC();
            FarmContents.FarmcencingTcObj.EiId = 60499;
            FarmContents.FarmstructuresObj = new OtherFarmStructures();
            FarmContents.FarmstructuresObj.EiId = 60501;
            FarmContents.FarmContentFcObj = new FarmContentsFC();
            FarmContents.FarmContentFcObj.EiId = 60507;
            FarmContents.ExcessesFpcObj = new ExcessesFPC();
            FarmContents.ExcessesFpcObj.ExcessList = excessToPay;
            FarmContents.ExcessesFpcObj.EiId = 60509;
            #endregion
            #region FarmMachinery
            FarmContents.FarmContentFMObj = new FarmContentsFC();
            FarmContents.FarmContentFMObj.EiId = 60523;
            FarmContents.ExcessUMObj = new ExcessforUM();
            FarmContents.ExcessUMObj.ExcessumList = excessToPay;
            FarmContents.ExcessUMObj.EiId = 60525;
            FarmContents.DescriptionFmObj = new DescriptionsFM();
            FarmContents.DescriptionFmObj.EiId = 60529;
            FarmContents.YearFMObj = new YearFPC();
            FarmContents.YearFMObj.EiId = 60531;
            FarmContents.SerialnumberObj = new SerialNumbers();
            FarmContents.SerialnumberObj.EiId = 60533;
            FarmContents.ExcessesFMObj = new ExcessesFPC();
            FarmContents.ExcessesFMObj.ExcessList = excessToPay;
            FarmContents.ExcessesFMObj.EiId = 60535;
            FarmContents.SuminsuredFMObj = new SumOfInsured();
            FarmContents.SuminsuredFMObj.EiId = 60537;
            #endregion
            #region Livestock
            FarmContents.DescriptionLSObj = new DescriptionsFC();
            FarmContents.DescriptionLSObj.DescriptionList = desList;
            FarmContents.DescriptionLSObj.EiId = 60565;
            FarmContents.NumberanimalObj = new NumberOfAnimals();
            FarmContents.NumberanimalObj.EiId = 60567;
            FarmContents.SuminsuredperObj = new SumInsuredPerAnimals();
            FarmContents.SuminsuredperObj.EiId = 60569;
            FarmContents.SuminsuredLSObj = new SumOfInsured();
            FarmContents.SuminsuredLSObj.EiId = 60571;
            FarmContents.DogattackObj = new DogAttackOption();
            FarmContents.DogattackObj.EiId = 60575;
            FarmContents.ExcessesLSObj = new ExcessesFPC();
            FarmContents.ExcessesLSObj.ExcessList = excessToPay;
            FarmContents.ExcessesLSObj.EiId = 60577;
            #endregion
            #region HarvestedCropsBeehives
            FarmContents.SuminsureHCVdObj = new SumOfInsured();
            FarmContents.SuminsureHCVdObj.EiId = 60593;
            FarmContents.ExcessesHCVObj = new ExcessesFPC();
            FarmContents.ExcessesHCVObj.ExcessList = excessToPay;
            FarmContents.ExcessesHCVObj.EiId = 60595;
            FarmContents.SuminsuredHbcObj = new SumOfInsuredHCB();
            FarmContents.SuminsuredHbcObj.EiId = 60601;
            FarmContents.NumberhiveObj = new NumberOfHives();
            FarmContents.NumberhiveObj.EiId = 60603;
            FarmContents.ExcessBObj = new ExcessesBeehives();
            FarmContents.ExcessBObj.ExcessBList = excessToPay;
            FarmContents.ExcessBObj.EiId = 60605;
            #endregion

            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = 0;
            int profileid = 0;
            if (Session["unId"] != null && Session["profileId"] != null)
            {
                unid = Convert.ToInt32(Session["unId"]);
                profileid = Convert.ToInt32(Session["profileId"]);
            }
            if (PcId != null && PcId.HasValue)
            {
                FarmContents.NewSections = cmn.NewSectionP(policyinclusions);             
                    if (policyinclusions.Exists(p => p.Name == "Farm Property"))
                    {
                        unid = policyinclusions.Where(p => p.Name == "Farm Property").Select(p => p.UnId).FirstOrDefault();
                        profileid = policyinclusions.Where(p => p.Name == "Farm Property").Select(p => p.UnId).FirstOrDefault();

                    }
                    else
                    {
                        return RedirectToAction("LiabilityCover", "Liabilities", new { cid = cid, PcId = PcId });
                    }                
                if (unid == null || unid == 0)
                {
                    unid = unitdetails.SectionData.UnId;
                    profileid = unitdetails.SectionData.ProfileUnId;
                    Session["unId"] = unid;
                    Session["profileId"] = profileid;
                }
                FarmContents.ExistingPolicyInclustions = policyinclusions;
                FarmContents.PolicyInclusion = new List<usp_GetUnit_Result>();
                FarmContents.PolicyInclusion = policyinclusions;
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
                    int HProfileId = 0;
                    if (Session["HProfileId"] != null)
                    {
                        HProfileId = Convert.ToInt32(Session["HprofileId"]);
                    }
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Farm Property&SectionUnId=&ProfileUnId=" + HProfileId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails.ErrorMessage != null && unitdetails.ErrorMessage.Count() > 0)
                        {
                            bool exists = FarmContents.PolicyInclusions.Exists(p => p.name == "Farm Property");
                            if (exists == true)
                            {
                                List<SessionModel> values = new List<SessionModel>();
                                values = (List<SessionModel>)Session["Policyinclustions"];
                                for (int k = 0; k < values.Count(); k++)
                                {
                                    if (values[k].name == "Farm Property" && values[k].UnitId == null && values[k].ProfileId == null)
                                    {
                                        values.RemoveAt(k);
                                    }
                                }
                                Session["Policyinclustions"] = values;
                            }
                            var errormessage = "First please provide cover for Home Buildings.";
                            if (unitdetails.ErrorMessage.Contains(errormessage))
                            {
                                TempData["Error"] = errormessage;
                                return RedirectToAction("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = PcId });
                            }
                        }
                        if (unitdetails != null && unitdetails.SectionData != null)
                        {
                            Session["unId"] = unitdetails.SectionData.UnId;
                            Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                            if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Farm Property"))
                            {
                                var policyhomelist = Policyincllist.FindAll(p => p.name == "Farm Property").ToList();
                                if (policyhomelist != null && policyhomelist.Count() > 0)
                                {
                                    if (Policyincllist.FindAll(p => p.name == "Farm Property").Exists(p => p.UnitId == null))
                                    {
                                        Policyincllist.FindAll(p => p.name == "Farm Property").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;
                                    }
                                    if (Policyincllist.FindAll(p => p.name == "Farm Property").Exists(p => p.ProfileId == null))
                                    {
                                        Policyincllist.FindAll(p => p.name == "Farm Property").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                    }
                                }
                                else
                                {
                                    Policyincllist.FindAll(p => p.name == "Farm Property").First().UnitId = unitdetails.SectionData.UnId;
                                    Policyincllist.FindAll(p => p.name == "Farm Property").First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                                FarmContents.PolicyInclusions = Policyincllist;
                                Session["Policyinclustions"] = Policyincllist;
                            }
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
                if (unitdetails.SectionData != null && unitdetails.SectionData.ValueData != null)
                {
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.confirmfsObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.confirmfsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.confirmfsObj.Confirm = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.DescriptionFCObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.DescriptionFCObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmContents.DescriptionFCObj.Description = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmContents.DescriptionFCObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var DescriptionList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.DescriptionFCObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < DescriptionList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60467;
                                vds.Element.ItId = DescriptionList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.DescriptionFCObj.EiId && p.Element.ItId == DescriptionList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmContents.DescriptionFCObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.YearObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.YearObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmContents.YearObj.Year = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmContents.YearObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var YearFCList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.YearObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < YearFCList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60468;
                                vds.Element.ItId = YearFCList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.YearObj.EiId && p.Element.ItId == YearFCList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmContents.YearObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.MaterialsObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.MaterialsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmContents.MaterialsObj.Materials = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmContents.MaterialsObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var MaterialsList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.MaterialsObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < MaterialsList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60470;
                                vds.Element.ItId = MaterialsList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.MaterialsObj.EiId && p.Element.ItId == MaterialsList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmContents.MaterialsObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.CoolroomFcObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.CoolroomFcObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmContents.CoolroomFcObj.Coolroom = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmContents.CoolroomFcObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var CoolroomList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.CoolroomFcObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < CoolroomList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60469;
                                vds.Element.ItId = CoolroomList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.CoolroomFcObj.EiId && p.Element.ItId == CoolroomList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmContents.CoolroomFcObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.SuminsuredObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmContents.SuminsuredObj.Suminsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmContents.SuminsuredObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var SuminsuredList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < SuminsuredList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60471;
                                vds.Element.ItId = SuminsuredList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredObj.EiId && p.Element.ItId == SuminsuredList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmContents.SuminsuredObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.DescriptionFmObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.DescriptionFmObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmContents.DescriptionFmObj.Description = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmContents.DescriptionFmObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var DescriptionFmList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.DescriptionFmObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < DescriptionFmList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60529;
                                vds.Element.ItId = DescriptionFmList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.DescriptionFmObj.EiId && p.Element.ItId == DescriptionFmList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmContents.DescriptionFmObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.YearFMObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.YearFMObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmContents.YearFMObj.Year = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmContents.YearFMObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var YearFmList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.YearFMObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < YearFmList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60531;
                                vds.Element.ItId = YearFmList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.YearFMObj.EiId && p.Element.ItId == YearFmList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmContents.YearFMObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.SerialnumberObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SerialnumberObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmContents.SerialnumberObj.Serialnumber = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmContents.SerialnumberObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var SerialnumberFmList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SerialnumberObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < SerialnumberFmList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60533;
                                vds.Element.ItId = SerialnumberFmList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SerialnumberObj.EiId && p.Element.ItId == SerialnumberFmList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmContents.SerialnumberObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.ExcessesFMObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.ExcessesFMObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmContents.ExcessesFMObj.Excess = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmContents.ExcessesFMObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var ExcessFmList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.ExcessesFMObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < ExcessFmList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60535;
                                vds.Element.ItId = ExcessFmList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.ExcessesFMObj.EiId && p.Element.ItId == ExcessFmList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmContents.ExcessesFMObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.SuminsuredFMObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredFMObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmContents.SuminsuredFMObj.Suminsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmContents.SuminsuredFMObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var SuminsuredFmList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredFMObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < SuminsuredFmList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60537;
                                vds.Element.ItId = SuminsuredFmList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredFMObj.EiId && p.Element.ItId == SuminsuredFmList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmContents.SuminsuredFMObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.DescriptionLSObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.DescriptionLSObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmContents.DescriptionLSObj.Description = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmContents.DescriptionLSObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var DescriptionLsList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.DescriptionLSObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < DescriptionLsList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60565;
                                vds.Element.ItId = DescriptionLsList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.DescriptionLSObj.EiId && p.Element.ItId == DescriptionLsList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmContents.DescriptionLSObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.NumberanimalObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.NumberanimalObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmContents.NumberanimalObj.Numberanimal = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmContents.NumberanimalObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var NumberanimalLsList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.NumberanimalObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < NumberanimalLsList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60567;
                                vds.Element.ItId = NumberanimalLsList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.NumberanimalObj.EiId && p.Element.ItId == NumberanimalLsList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmContents.NumberanimalObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.SuminsuredperObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredperObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmContents.SuminsuredperObj.Suminsuredper = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmContents.SuminsuredperObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var SuminsuredperLsList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredperObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < SuminsuredperLsList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60569;
                                vds.Element.ItId = SuminsuredperLsList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredperObj.EiId && p.Element.ItId == SuminsuredperLsList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmContents.SuminsuredperObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.SuminsuredLSObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredLSObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FarmContents.SuminsuredLSObj.Suminsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FarmContents.SuminsuredLSObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var SuminsuredLsList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredLSObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < SuminsuredLsList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60571;
                                vds.Element.ItId = SuminsuredLsList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredLSObj.EiId && p.Element.ItId == SuminsuredLsList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FarmContents.SuminsuredLSObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.DogattackObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.DogattackObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.DogattackObj.Dogattack = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.ExcessBObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.ExcessBObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.ExcessBObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.ExcessesFpcObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.ExcessesFpcObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.ExcessesFpcObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.ExcessesHCVObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.ExcessesHCVObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.ExcessesHCVObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.ExcessesLSObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.ExcessesLSObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.ExcessesLSObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.ExcessUMObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.ExcessUMObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.ExcessUMObj.Excessum = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.FarmcencingTcObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.FarmcencingTcObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.FarmcencingTcObj.Totalcover = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.FarmContentFcObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.FarmContentFcObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.FarmContentFcObj.Farmcontents = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.FarmContentFMObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.FarmContentFMObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.FarmContentFMObj.Farmcontents = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.FarmfencingObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.FarmfencingObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.FarmfencingObj.Farmfencing = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.FarmstructuresObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.FarmstructuresObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.FarmstructuresObj.Farmstructures = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.NumberhiveObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.NumberhiveObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.NumberhiveObj.Numberhive = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.SuminsureHCVdObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsureHCVdObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.SuminsureHCVdObj.Suminsured = val;
                    }
                }
            }
            if (unitdetails != null && unitdetails.ReferralList != null)
            {
                FarmContents.ReferralList = unitdetails.ReferralList;
                FarmContents.ReferralList.Replace("&nbsp;&nbsp;&nbsp;&nbsp", "");
                FarmContents.Referels = new List<string>();
                string[] delim = { "<br/>" };
                string[] spltd = FarmContents.ReferralList.Split(delim, StringSplitOptions.None);
                if (spltd != null && spltd.Count() > 0)
                {
                    for (int i = 0; i < spltd.Count(); i++)
                    {
                        FarmContents.Referels.Add(spltd[i].Replace("&nbsp;&nbsp;&nbsp;&nbsp", " "));
                    }
                }
            }
            if (cid != null)
            {
                FarmContents.CustomerId = cid.Value;
            }
            if (PcId != null && PcId > 0)
            {
                FarmContents.PcId = PcId;
            }
            Session["Controller"] = "Farm";
            Session["ActionName"] = "FarmContents";
            return View(FarmContents);
        }
        [HttpPost]
        public ActionResult FarmContents(FarmContents FarmContents, int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> DescriptionListFContent = new List<SelectListItem>();
            DescriptionListFContent = commonModel.descriptionListFC();
            List<SelectListItem> constructionTypeFC = new List<SelectListItem>();
            constructionTypeFC = commonModel.constructionType();
            List<SelectListItem> excessToPay = new List<SelectListItem>();
            excessToPay = commonModel.excessRate();
            FarmContents.DescriptionFCObj.DescriptionList = DescriptionListFContent;
            FarmContents.MaterialsObj.MaterialsList = constructionTypeFC;
            FarmContents.ExcessesFpcObj.ExcessList = excessToPay;
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                FarmContents.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FarmContents.CustomerId;
            }
            Session["profileId"] = null;
            Session["RLSFrm"] = 1;
            Session["UnId"] = null;
            string actionname = null;
            string controllername = null;
            if (Session["Actname"] != null)
            {
                actionname = Session["Actname"].ToString();
            }
            if (Session["controller"] != null)
            {
                controllername = Session["controller"].ToString();
            }
            return RedirectToAction("LiabilityCover", "Liabilities", new { cid = FarmContents.CustomerId, PcId = FarmContents.PcId });
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
                    excessforUMPay = commonModel.excessRate();
                    return Json(new { status = true, des = excessforUMPay });
                }
                else if (content == "Livestock")
                {
                    List<SelectListItem> desListB = new List<SelectListItem>();
                    desListB = commonModel.descriptionLS();
                    return Json(new { status = true, des = desListB });
                }
                else if (content == "farmDetails")
                {
                    List<SelectListItem> desList = new List<SelectListItem>();
                    List<SelectListItem> desListB = new List<SelectListItem>();
                    desList.Add(new SelectListItem { Value = "", Text = "--Select--" });
                    desListB = commonModel.constructionType();
                    desList.AddRange(desListB);
                    return Json(new { status = true, des = desList });
                }
                else if (content == "FarmHomeValuables")
                {
                    List<SelectListItem> desList = new List<SelectListItem>();
                    List<SelectListItem> desListB = new List<SelectListItem>();
                    desList.Add(new SelectListItem { Value = "", Text = "--Select--" });
                    desListB = commonModel.descriptionListFC();
                    desList.AddRange(desListB);
                    return Json(new { status = true, des = desList });
                }
                else if (content == "FarmHomeContent")
                {
                    List<SelectListItem> desList = new List<SelectListItem>();
                    List<SelectListItem> desListB = new List<SelectListItem>();
                    desList.Add(new SelectListItem { Value = "", Text = "--Select--" });
                    desListB = commonModel.descriptionListFC();
                    desList.AddRange(desListB);
                    return Json(new { status = true, des = desList });
                }
                else if (content == "FarmMachineryTM")
                {
                    List<SelectListItem> desList = new List<SelectListItem>();
                    List<SelectListItem> desListB = new List<SelectListItem>();
                    desList = commonModel.TypeOfMachinery();
                    desListB = commonModel.Power();
                    return Json(new { status = true, des = desList, con = desListB });
                }
                else if (content == "FarmMachineryVVL")
                {
                    List<SelectListItem> desList = new List<SelectListItem>();
                    desList = commonModel.VolumeOfVat();
                    return Json(new { status = true, des = desList });
                }
                else if (content == "LivestockFP")
                {
                    List<SelectListItem> desList = new List<SelectListItem>();
                    desList = commonModel.descriptionLS();
                    return Json(new { status = true, des = desList });
                }
                else if (content == "FarmMachineryTU")
                {
                    List<SelectListItem> desList = new List<SelectListItem>();
                    desList = commonModel.MachineryTypeOfUnit();
                    return Json(new { status = true, des = desList });
                }
                else if (content == "FarmMachinerySM")
                {
                    List<SelectListItem> desList = new List<SelectListItem>();
                    desList = commonModel.MachineryTypeOfUnitSpefic();
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