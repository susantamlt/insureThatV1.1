using System;
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
    public class FarmPolicyLivestockController : Controller
    {
        // GET: FarmPolicyLivestock
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Livestock(int? cid, int? PcId)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> ClassofAnimalLivestock = new List<SelectListItem>();
            ClassofAnimalLivestock = commonModel.ClassofAnimalLivestock();

            List<SelectListItem> TypeofAnimalLivestock = new List<SelectListItem>();
            TypeofAnimalLivestock = commonModel.TypeofAnimalLivestock();

            List<SelectListItem> AgeofAnimalLivestock = new List<SelectListItem>();
            AgeofAnimalLivestock = commonModel.AgeofAnimalLivestock();

            List<SelectListItem> UseofAnimalLivestock = new List<SelectListItem>();
            UseofAnimalLivestock = commonModel.UseofAnimalLivestock();

            List<SelectListItem> excessToPayLiveStock = new List<SelectListItem>();
            excessToPayLiveStock = commonModel.excessRate();

            FPLivestock FPLivestock = new FPLivestock();
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPLivestock.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPLivestock.CustomerId;
            }
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            NewPolicyDetailsClass commonmethods = new NewPolicyDetailsClass();
            var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
            string apikey = null;
            bool policyinclusion = policyinclusions.Exists(p => p.Name == "LiveStock");
            if (Session["apiKey"] != null)
            {
                apikey = Session["apiKey"].ToString();
                FPLivestock.ApiKey = Session["apiKey"].ToString();
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
                FPLivestock.PolicyId = policyid;
            }
            else if (Session["Policyinclustions"] != null)
            {
                #region Policy Selected or not testing
                List<SessionModel> PolicyInclustions = new List<SessionModel>();
                FPLivestock.PolicyInclusions = new List<SessionModel>();
                FPLivestock.PolicyInclusions = Policyincllist;

                if (Policyincllist != null)
                {
                    if (Policyincllist != null)
                    {
                          if (Policyincllist.Exists(p => p.name == "Livestock"))
                        {
                          

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
                        if (Policyincllist.Exists(p => p.name == "Livestock"))
                        {
                            if (Session["unId"] == null && Session["profileId"] == null)
                            {
                                Session["unId"] = Policyincllist.Where(p => p.name == "Livestock").Select(p => p.UnitId).First();
                                Session["profileId"] = Policyincllist.Where(p => p.name == "Livestock").Select(p => p.ProfileId).First();
                            }
                        }
                        else
                        {
                            return RedirectToAction("DisclosureDetails", "Disclosure", new { cid = cid, PcId = PcId });
                        }
                    }
                }
                #endregion
            }
            FPLivestock.ClassOfAnimalFPObj = new ClassOfAnimalFP();
            FPLivestock.ClassOfAnimalFPObj.ClassOfAnimalList = ClassofAnimalLivestock;
            FPLivestock.ClassOfAnimalFPObj.EiId = 63321;

            FPLivestock.TypeOfAnimalFPObj = new TypeOfAnimalFP();
            FPLivestock.TypeOfAnimalFPObj.TypeOfAnimalList = TypeofAnimalLivestock;
            FPLivestock.TypeOfAnimalFPObj.EiId = 63323;

            FPLivestock.BreedOfAnimalFPObj = new BreedOfAnimalFP();
            FPLivestock.BreedOfAnimalFPObj.EiId = 63325;

            FPLivestock.AgeOfAnimalFPObj = new AgeOfAnimalFP();
            FPLivestock.AgeOfAnimalFPObj.AgeOfAnimalList = AgeofAnimalLivestock;
            FPLivestock.AgeOfAnimalFPObj.EiId = 63327;

            FPLivestock.ColourOfAnimalFPObj = new ColourOfAnimalFP();
            FPLivestock.ColourOfAnimalFPObj.EiId = 63329;

            FPLivestock.UseOfAnimalFPObj = new UseOfAnimalFP();
            FPLivestock.UseOfAnimalFPObj.UseOfAnimalList = UseofAnimalLivestock;
            FPLivestock.UseOfAnimalFPObj.EiId = 63331;

            FPLivestock.DescBrandOfAnimalFPObj = new DescBrandOfAnimalFP();
            FPLivestock.DescBrandOfAnimalFPObj.EiId = 63333;

            FPLivestock.DescMarksOfAnimalFPObj = new DescMarksOfAnimalFP();
            FPLivestock.DescMarksOfAnimalFPObj.EiId = 63335;

            FPLivestock.OptSoundHealthofAnimalFPObj = new OptSoundHealthofAnimalFP();
            FPLivestock.OptSoundHealthofAnimalFPObj.EiId = 63345;

            FPLivestock.DescSoundHealthofAnimalFPObj = new DescSoundHealthofAnimalFP();
            FPLivestock.DescSoundHealthofAnimalFPObj.EiId = 63347;

            FPLivestock.OptDiseaseOfAnimalFPObj = new OptDiseaseOfAnimalFP();
            FPLivestock.OptDiseaseOfAnimalFPObj.EiId = 63349;

            FPLivestock.DescDiseaseOfAnimalFPObj = new DescDiseaseOfAnimalFP();
            FPLivestock.DescDiseaseOfAnimalFPObj.EiId = 63351;

            FPLivestock.OptAnimalSyndicatedFPObj = new OptAnimalSyndicatedFP();
            FPLivestock.OptAnimalSyndicatedFPObj.EiId = 63353;

            FPLivestock.DescAnimalSyndicatedFPObj = new DescAnimalSyndicatedFP();
            FPLivestock.DescAnimalSyndicatedFPObj.EiId = 11111;

            FPLivestock.SumInsuredLivestockFPObj = new SumInsuredLivestockFP();
            FPLivestock.SumInsuredLivestockFPObj.EiId = 63355;

            FPLivestock.OptInfertilityFPObj = new OptInfertilityFP();
            FPLivestock.OptInfertilityFPObj.EiId = 63359;

            FPLivestock.OptLossofUseLivestockFPObj = new OptLossofUseLivestockFP();
            FPLivestock.OptLossofUseLivestockFPObj.EiId = 63361;

            FPLivestock.OptTheftLivestockFPObj = new OptTheftLivestockFP();
            FPLivestock.OptTheftLivestockFPObj.EiId = 63363;

            FPLivestock.OptUnbornFoalFPObj = new OptUnbornFoalFP();
            FPLivestock.OptUnbornFoalFPObj.EiId = 63365;

            FPLivestock.ExcessLivestockFPObj = new ExcessLivestockFP();
            FPLivestock.ExcessLivestockFPObj.ExcessList = excessToPayLiveStock;
            FPLivestock.ExcessLivestockFPObj.EiId = 63375;


            FPLivestock.NoOfContainersFPObj = new NoOfContainersFP();
            FPLivestock.NoOfContainersFPObj.EiId = 63383;

            FPLivestock.MaxStrawsandAmpoulesFPObj = new MaxStrawsandAmpoulesFP();
            FPLivestock.MaxStrawsandAmpoulesFPObj.EiId = 63385;

            FPLivestock.MaxValOneContainerFPObj = new MaxValOneContainerFP();
            FPLivestock.MaxValOneContainerFPObj.EiId = 63387;

            FPLivestock.AnnualStrawsandAmpoulesFPObj = new AnnualStrawsandAmpoulesFP();
            FPLivestock.AnnualStrawsandAmpoulesFPObj.EiId = 63389;

            FPLivestock.ExcessLivestockFPBObj = new ExcessLivestockFP();
            FPLivestock.ExcessLivestockFPBObj.EiId = 63393;

            FPLivestock.CoverforsemenLSObj = new CoverforsemenLS();
            FPLivestock.CoverforsemenLSObj.EiId = 63391;

            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = Convert.ToInt32(Session["unId"]);
            int profileid = Convert.ToInt32(Session["profileId"]);
            if (policyinclusion == true && PcId != null && PcId.HasValue)
            {
                FPLivestock.ExistingPolicyInclustions = policyinclusions;
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
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=LiveStock&SectionUnId=&ProfileUnId=0");
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
                if (unitdetails.SectionData != null && unitdetails.SectionData.ValueData!=null)
                {
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.ClassOfAnimalFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.ClassOfAnimalFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.ClassOfAnimalFPObj.ClassofAnimal = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.TypeOfAnimalFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.TypeOfAnimalFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.TypeOfAnimalFPObj.TypeofAnimal = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.BreedOfAnimalFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.BreedOfAnimalFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.BreedOfAnimalFPObj.BreedofAnimal = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.AgeOfAnimalFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.AgeOfAnimalFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.AgeOfAnimalFPObj.AgeofAnimal = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.ColourOfAnimalFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.ColourOfAnimalFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.ColourOfAnimalFPObj.ColourofAnimal = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.UseOfAnimalFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.UseOfAnimalFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.UseOfAnimalFPObj.UseofAnimal = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.DescBrandOfAnimalFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.DescBrandOfAnimalFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.DescBrandOfAnimalFPObj.BrandofAnimal = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.DescMarksOfAnimalFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.DescMarksOfAnimalFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.DescMarksOfAnimalFPObj.MarkingsofAnimal = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.OptSoundHealthofAnimalFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.OptSoundHealthofAnimalFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.OptSoundHealthofAnimalFPObj.HealthofAnimal = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.DescSoundHealthofAnimalFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.DescSoundHealthofAnimalFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.DescSoundHealthofAnimalFPObj.Description = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.OptDiseaseOfAnimalFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.OptDiseaseOfAnimalFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.OptDiseaseOfAnimalFPObj.DiseaseofAnimal = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.DescDiseaseOfAnimalFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.DescDiseaseOfAnimalFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.DescDiseaseOfAnimalFPObj.Description = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.OptAnimalSyndicatedFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.OptAnimalSyndicatedFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.OptAnimalSyndicatedFPObj.AnimalSyndicated = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.DescAnimalSyndicatedFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.DescAnimalSyndicatedFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.DescAnimalSyndicatedFPObj.Description = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.SumInsuredLivestockFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.SumInsuredLivestockFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.SumInsuredLivestockFPObj.SumInsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.OptInfertilityFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.OptInfertilityFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.OptInfertilityFPObj.Infertility = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.OptLossofUseLivestockFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.OptLossofUseLivestockFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.OptLossofUseLivestockFPObj.LossofUse = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.OptTheftLivestockFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.OptTheftLivestockFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.OptTheftLivestockFPObj.TheftOption = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.OptUnbornFoalFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.OptUnbornFoalFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.OptUnbornFoalFPObj.UnbornFoal = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.ExcessLivestockFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.ExcessLivestockFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.ExcessLivestockFPObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.NoOfContainersFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.NoOfContainersFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.NoOfContainersFPObj.NumberOfContainers = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.MaxStrawsandAmpoulesFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.MaxStrawsandAmpoulesFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.MaxStrawsandAmpoulesFPObj.StrawAndAmpoules = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.MaxValOneContainerFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.MaxValOneContainerFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.MaxValOneContainerFPObj.MaxValoneContainer = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.AnnualStrawsandAmpoulesFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.AnnualStrawsandAmpoulesFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.AnnualStrawsandAmpoulesFPObj.AnnualStrawandAmpoules = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.CoverforsemenLSObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.CoverforsemenLSObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.CoverforsemenLSObj.Coverforsemen = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPLivestock.ExcessLivestockFPBObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPLivestock.ExcessLivestockFPBObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPLivestock.ExcessLivestockFPBObj.Excess = val;
                    }
                }
            }

            return View(FPLivestock);
        }

        [HttpPost]
        public ActionResult Livestock(int? cid, FPLivestock FPLivestock)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> ClassofAnimalLivestock = new List<SelectListItem>();
            ClassofAnimalLivestock = commonModel.ClassofAnimalLivestock();
            FPLivestock.ClassOfAnimalFPObj.ClassOfAnimalList = ClassofAnimalLivestock;

            List<SelectListItem> TypeofAnimalLivestock = new List<SelectListItem>();
            TypeofAnimalLivestock = commonModel.TypeofAnimalLivestock();
            FPLivestock.TypeOfAnimalFPObj.TypeOfAnimalList = TypeofAnimalLivestock;


            List<SelectListItem> AgeofAnimalLivestock = new List<SelectListItem>();
            AgeofAnimalLivestock = commonModel.AgeofAnimalLivestock();
            FPLivestock.AgeOfAnimalFPObj.AgeOfAnimalList = AgeofAnimalLivestock;

            List<SelectListItem> UseofAnimalLivestock = new List<SelectListItem>();
            UseofAnimalLivestock = commonModel.UseofAnimalLivestock();
            FPLivestock.UseOfAnimalFPObj.UseOfAnimalList = UseofAnimalLivestock;

            List<SelectListItem> excessToPayLiveStock = new List<SelectListItem>();
            excessToPayLiveStock = commonModel.excessRate();
            FPLivestock.ExcessLivestockFPObj.ExcessList = excessToPayLiveStock;
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPLivestock.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPLivestock.CustomerId;
            }
            string policyid = null;
            Session["unId"] = null;
            Session["profileId"] = null;
            return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = FPLivestock.CustomerId, PcId = FPLivestock.PcId });
        }
    }
}