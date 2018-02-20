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
    public class FarmController : Controller
    {
        // GET: Farm
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> FarmContents (int? cid, int? PcId)
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
                if (Policyincllist != null)
                {

                    if (Policyincllist.Exists(p => p.name == "Farm Property"))
                    {
                       
                    }
                  
                    else if (Policyincllist.Exists(p => p.name == "Liability"))
                    {
                        return RedirectToAction("LiabilityCover", "Liabilities", new { cid = cid });
                    }
                    else if (Policyincllist.Exists(p => p.name == "Motor"))
                    {
                        return RedirectToAction("VehicleDescription", "MotorCover", new { cid = cid });
                    }
                    else if (Policyincllist.Exists(p => p.name == "Boat"))
                    {
                        return RedirectToAction("BoatDetails", "Boat", new { cid = cid });
                    }

                    else if (Policyincllist.Exists(p => p.name == "Pet"))
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
            FarmContents.ExcessBObj.EiId = 60595;
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
            if (policyinclusion == true && PcId != null && PcId.HasValue)
            {
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
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Farm Property&SectionUnId=&ProfileUnId="+HProfileId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails.ErrorMessage != null && unitdetails.ErrorMessage.Count() > 0)
                        {
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
                if (unitdetails.SectionData != null)
                {
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.confirmfsObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.confirmfsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.confirmfsObj.Confirm = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.CoolroomFcObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.CoolroomFcObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.CoolroomFcObj.Coolroom = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.DescriptionFCObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.DescriptionFCObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.DescriptionFCObj.Description = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.DescriptionFmObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.DescriptionFmObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.DescriptionFmObj.Description = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.DescriptionLSObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.DescriptionLSObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.DescriptionLSObj.Description = val;
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
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.ExcessesFMObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.ExcessesFMObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.ExcessesFMObj.Excess = val;
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
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.MaterialsObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.MaterialsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.MaterialsObj.Materials = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.NumberanimalObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.NumberanimalObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.NumberanimalObj.Numberanimal = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.NumberhiveObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.NumberhiveObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.NumberhiveObj.Numberhive = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.SerialnumberObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SerialnumberObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.SerialnumberObj.Serialnumber = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.SuminsuredFMObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredFMObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.SuminsuredFMObj.Suminsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.SuminsuredLSObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredLSObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.SuminsuredLSObj.Suminsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.SuminsuredObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.SuminsuredObj.Suminsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.SuminsuredperObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsuredperObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.SuminsuredperObj.Suminsuredper = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.SuminsureHCVdObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.SuminsureHCVdObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.SuminsureHCVdObj.Suminsured = val;
                    }
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmContents.TotallivestockObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmContents.TotallivestockObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    FarmContents.TotallivestockObj.Totallivestock = val;
                    //}
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmContents.TotalspecifieditemObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmContents.TotalspecifieditemObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    FarmContents.TotalspecifieditemObj.Totalspecifieditem = val;
                    //}
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FarmContents.YearFMObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FarmContents.YearFMObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    FarmContents.YearFMObj.Year = val;
                    //}
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FarmContents.YearObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FarmContents.YearObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FarmContents.YearObj.Year = val;
                    }
                }
            }
            if (unitdetails.ReferralList != null)
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
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                //if (FarmContents.DescriptionFCObj!=null && FarmContents.DescriptionFCObj.EiId > 0 && FarmContents.DescriptionFCObj.Description != null)
                //{
                //    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.DescriptionFCObj.EiId, FarmContents.DescriptionFCObj.Description.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (FarmContents.YearObj!=null && FarmContents.YearObj.EiId > 0 && FarmContents.YearObj.Year != null)
                //{
                //    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.YearObj.EiId, FarmContents.YearObj.Year.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (FarmContents.MaterialsObj!=null && FarmContents.MaterialsObj.EiId > 0 && FarmContents.MaterialsObj.Materials != null)
                //{
                //    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.MaterialsObj.EiId, FarmContents.MaterialsObj.Materials.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (FarmContents.CoolroomFcObj!=null && FarmContents.CoolroomFcObj.EiId > 0 && FarmContents.CoolroomFcObj.Coolroom != null)
                //{
                //    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.CoolroomFcObj.EiId, FarmContents.CoolroomFcObj.Coolroom.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (FarmContents.SuminsuredObj!=null && FarmContents.SuminsuredObj.EiId > 0 && FarmContents.SuminsuredObj.Suminsured != null)
                //{
                //    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.SuminsuredObj.EiId, FarmContents.SuminsuredObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (FarmContents.confirmfsObj!=null && FarmContents.confirmfsObj.EiId > 0 && FarmContents.confirmfsObj.Confirm != null)
                //{
                //    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.confirmfsObj.EiId, FarmContents.confirmfsObj.Confirm.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (FarmContents.FarmfencingObj!=null && FarmContents.FarmfencingObj.EiId > 0 && FarmContents.FarmfencingObj.Farmfencing != null)
                //{
                //    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.FarmfencingObj.EiId, FarmContents.FarmfencingObj.Farmfencing.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (FarmContents.FarmcencingTcObj!=null && FarmContents.FarmcencingTcObj.EiId > 0 && FarmContents.FarmcencingTcObj.Totalcover != null)
                //{
                //    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.FarmcencingTcObj.EiId, FarmContents.FarmcencingTcObj.Totalcover.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (FarmContents.FarmstructuresObj!=null && FarmContents.FarmstructuresObj.EiId > 0 && FarmContents.FarmstructuresObj.Farmstructures != null)
                //{
                //    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.FarmstructuresObj.EiId, FarmContents.FarmstructuresObj.Farmstructures.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (FarmContents.FarmContentFcObj!=null && FarmContents.FarmContentFcObj.EiId > 0 && FarmContents.FarmContentFcObj.Farmcontents != null)
                //{
                //    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.FarmContentFcObj.EiId, FarmContents.FarmContentFcObj.Farmcontents.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (FarmContents.ExcessesFpcObj!=null && FarmContents.ExcessesFpcObj.EiId > 0 && FarmContents.ExcessesFpcObj.Excess != null)
                //{
                //    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.ExcessesFpcObj.EiId, FarmContents.ExcessesFpcObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                Session["profileId"] = null;
                Session["UnId"] = null;
                return RedirectToAction("LiabilityCover", "Liabilities", new { cid = FarmContents.CustomerId });
            }

            return RedirectToAction("LiabilityCover", "Liabilities", new { cid = FarmContents.CustomerId });
        }
        //[HttpGet]
        //public ActionResult FarmMachinery(int? cid)
        //{
        //    NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();

        //    List<SelectListItem> excessforUMPay = new List<SelectListItem>();
        //    excessforUMPay = commonModel.excessRate();
        //    FarmMachinery FarmMachinery = new FarmMachinery();
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        FarmMachinery.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = FarmMachinery.CustomerId;
        //    }
        //    FarmMachinery.FarmContentFcObj = new FarmContentsFC();
        //    FarmMachinery.FarmContentFcObj.EiId = 60523;
        //    FarmMachinery.ExcessUMObj = new ExcessforUM();
        //    FarmMachinery.ExcessUMObj.ExcessumList = excessforUMPay;
        //    FarmMachinery.ExcessUMObj.EiId = 60525;
        //    FarmMachinery.DescriptionFmObj = new DescriptionsFM();
        //    FarmMachinery.DescriptionFmObj.EiId = 60529;
        //    FarmMachinery.YearObj = new YearFPC();
        //    FarmMachinery.YearObj.EiId = 60531;
        //    FarmMachinery.SerialnumberObj = new SerialNumbers();
        //    FarmMachinery.SerialnumberObj.EiId = 60533;
        //    FarmMachinery.ExcessesFpcObj = new ExcessesFPC();
        //    FarmMachinery.ExcessesFpcObj.ExcessList = excessforUMPay;
        //    FarmMachinery.ExcessesFpcObj.EiId = 60535;
        //    FarmMachinery.SuminsuredObj = new SumOfInsured();
        //    FarmMachinery.SuminsuredObj.EiId = 60537;
        //    if (Session["CompletionTrackFPC"] != null)
        //    {
        //        Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
        //        FarmMachinery.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
        //    }
        //    else
        //    {
        //        Session["CompletionTrackFPC"] = "0-0-0-0"; ;
        //        FarmMachinery.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
        //    }
        //    string policyid = null;
        //    var db = new MasterDataEntities();
        //    var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.FarmProperty), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
        //    if (details != null && details.Any())
        //    {
        //        if (details.Exists(q => q.QuestionId == FarmMachinery.FarmContentFcObj.EiId))
        //        {
        //            FarmMachinery.FarmContentFcObj.Farmcontents = Convert.ToString(details.Where(q => q.QuestionId == FarmMachinery.FarmContentFcObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == FarmMachinery.ExcessUMObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == FarmMachinery.ExcessUMObj.EiId).FirstOrDefault();
        //            FarmMachinery.ExcessUMObj.Excessum = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //        if (details.Exists(q => q.QuestionId == FarmMachinery.DescriptionFmObj.EiId))
        //        {
        //            FarmMachinery.DescriptionFmObj.Description = Convert.ToString(details.Where(q => q.QuestionId == FarmMachinery.YearObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == FarmMachinery.YearObj.EiId))
        //        {
        //            FarmMachinery.YearObj.Year = Convert.ToString(details.Where(q => q.QuestionId == FarmMachinery.YearObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == FarmMachinery.SerialnumberObj.EiId))
        //        {
        //            FarmMachinery.SerialnumberObj.Serialnumber = Convert.ToString(details.Where(q => q.QuestionId == FarmMachinery.SerialnumberObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == FarmMachinery.ExcessesFpcObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == FarmMachinery.ExcessesFpcObj.EiId).FirstOrDefault();
        //            FarmMachinery.ExcessesFpcObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //        if (details.Exists(q => q.QuestionId == FarmMachinery.SuminsuredObj.EiId))
        //        {
        //            FarmMachinery.SuminsuredObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == FarmMachinery.SuminsuredObj.EiId).FirstOrDefault().Answer);
        //        }
        //    }
        //    return View(FarmMachinery);
        //}
        //[HttpPost]
        //public ActionResult FarmMachinery(FarmMachinery FarmMachinery, int? cid)
        //{
        //    NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
        //    List<SelectListItem> excessforUMPay = new List<SelectListItem>();
        //    excessforUMPay = commonModel.excessRate();
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        FarmMachinery.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = FarmMachinery.CustomerId;
        //    }
        //    FarmMachinery.ExcessUMObj.ExcessumList = excessforUMPay;
        //    FarmMachinery.ExcessesFpcObj.ExcessList = excessforUMPay;
        //    string policyid = null;
        //    var db = new MasterDataEntities();
        //    if (cid.HasValue && cid > 0)
        //    {
        //        if (FarmMachinery.FarmContentFcObj!=null && FarmMachinery.FarmContentFcObj.EiId > 0 && FarmMachinery.FarmContentFcObj.Farmcontents != null)
        //        {
        //            db.IT_InsertCustomerQnsData(FarmMachinery.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmMachinery.FarmContentFcObj.EiId, FarmMachinery.FarmContentFcObj.Farmcontents.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (FarmMachinery.ExcessUMObj!=null && FarmMachinery.ExcessUMObj.EiId > 0 && FarmMachinery.ExcessUMObj.Excessum != null)
        //        {
        //            db.IT_InsertCustomerQnsData(FarmMachinery.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmMachinery.ExcessUMObj.EiId, FarmMachinery.ExcessUMObj.Excessum.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (FarmMachinery.DescriptionFmObj!=null && FarmMachinery.DescriptionFmObj.EiId > 0 && FarmMachinery.DescriptionFmObj.Description != null)
        //        {
        //            db.IT_InsertCustomerQnsData(FarmMachinery.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmMachinery.DescriptionFmObj.EiId, FarmMachinery.DescriptionFmObj.Description.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (FarmMachinery.YearObj!=null && FarmMachinery.YearObj.EiId > 0 && FarmMachinery.YearObj.Year != null)
        //        {
        //            db.IT_InsertCustomerQnsData(FarmMachinery.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmMachinery.YearObj.EiId, FarmMachinery.YearObj.Year.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (FarmMachinery.SerialnumberObj!=null && FarmMachinery.SerialnumberObj.EiId > 0 && FarmMachinery.SerialnumberObj.Serialnumber != null)
        //        {
        //            db.IT_InsertCustomerQnsData(FarmMachinery.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmMachinery.SerialnumberObj.EiId, FarmMachinery.SerialnumberObj.Serialnumber.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (FarmMachinery.ExcessesFpcObj!=null && FarmMachinery.ExcessesFpcObj.EiId > 0 && FarmMachinery.ExcessesFpcObj.Excess != null)
        //        {
        //            db.IT_InsertCustomerQnsData(FarmMachinery.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmMachinery.ExcessesFpcObj.EiId, FarmMachinery.ExcessesFpcObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (FarmMachinery.SuminsuredObj!=null && FarmMachinery.SuminsuredObj.EiId > 0 && FarmMachinery.SuminsuredObj.Suminsured != null)
        //        {
        //            db.IT_InsertCustomerQnsData(FarmMachinery.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmMachinery.SuminsuredObj.EiId, FarmMachinery.SuminsuredObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Session["CompletionTrackFPC"] != null)
        //        {
        //            Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
        //            FarmMachinery.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
        //            if (FarmMachinery.CompletionTrackFPC != null)
        //            {
        //                string Completionstring = string.Empty;
        //                char[] arr = FarmMachinery.CompletionTrackFPC.ToCharArray();

        //                for (int i = 0; i < arr.Length; i++)
        //                {
        //                    char a = arr[i];
        //                    if (i == 2)
        //                    {
        //                        a = '1';
        //                    }
        //                    Completionstring = Completionstring + a;
        //                }
        //                Session["CompletionTrackFPC"] = Completionstring;
        //                FarmMachinery.CompletionTrackFPC = Completionstring;
        //            }
        //        }
        //        else
        //        {
        //            Session["CompletionTrackFPC"] = "0-1-0-0"; ;
        //            FarmMachinery.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
        //        }
        //        return RedirectToAction("Livestock", new { cid = FarmMachinery.CustomerId });
        //    }
        //    return View(FarmMachinery);
        //}
        //[HttpGet]
        //public ActionResult Livestock(int? cid)
        //{
        //    NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
        //    List<SelectListItem> desList = new List<SelectListItem>();
        //    desList = commonModel.descriptionLS();

        //    List<SelectListItem> excessforUMPay = new List<SelectListItem>();
        //    excessforUMPay = commonModel.excessRate();
        //    Livestock Livestock = new Livestock();
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        Livestock.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = Livestock.CustomerId;
        //    }
        //    Livestock.DescriptionFCObj = new DescriptionsFC();
        //    Livestock.DescriptionFCObj.DescriptionList = desList;
        //    Livestock.DescriptionFCObj.EiId = 60565;
        //    Livestock.NumberanimalObj = new NumberOfAnimals();
        //    Livestock.NumberanimalObj.EiId = 60567;
        //    Livestock.SuminsuredperObj = new SumInsuredPerAnimals();
        //    Livestock.SuminsuredperObj.EiId = 60569;
        //    Livestock.SuminsuredObj = new SumOfInsured();
        //    Livestock.SuminsuredObj.EiId = 60571;
        //    Livestock.DogattackObj = new DogAttackOption();
        //    Livestock.DogattackObj.EiId = 60575;
        //    Livestock.ExcessesFpcObj = new ExcessesFPC();
        //    Livestock.ExcessesFpcObj.ExcessList = excessforUMPay;
        //    Livestock.ExcessesFpcObj.EiId = 60577;
        //    if (Session["CompletionTrackFPC"] != null)
        //    {
        //        Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
        //        Livestock.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
        //    }
        //    else
        //    {
        //        Session["CompletionTrackFPC"] = "0-0-0-0"; ;
        //        Livestock.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(RLSSection.FarmProperty), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
        //    if (details != null && details.Any())
        //    {
        //        if (details.Exists(q => q.QuestionId == Livestock.DescriptionFCObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == Livestock.DescriptionFCObj.EiId).FirstOrDefault();
        //            Livestock.DescriptionFCObj.Description = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //        if (details.Exists(q => q.QuestionId == Livestock.NumberanimalObj.EiId))
        //        {
        //            Livestock.NumberanimalObj.Numberanimal = Convert.ToString(details.Where(q => q.QuestionId == Livestock.NumberanimalObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == Livestock.SuminsuredperObj.EiId))
        //        {
        //            Livestock.SuminsuredperObj.Suminsuredper = Convert.ToString(details.Where(q => q.QuestionId == Livestock.SuminsuredperObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == Livestock.SuminsuredObj.EiId))
        //        {
        //            Livestock.SuminsuredObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == Livestock.SuminsuredObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == Livestock.DogattackObj.EiId))
        //        {
        //            Livestock.DogattackObj.Dogattack = Convert.ToString(details.Where(q => q.QuestionId == Livestock.DogattackObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == Livestock.ExcessesFpcObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == Livestock.ExcessesFpcObj.EiId).FirstOrDefault();
        //            Livestock.ExcessesFpcObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //    }
        //    return View(Livestock);
        //}
        //[HttpPost]
        //public ActionResult Livestock(Livestock Livestock, int? cid)
        //{
        //    NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
        //    List<SelectListItem> desList = new List<SelectListItem>();
        //    desList = commonModel.descriptionLS();
        //    List<SelectListItem> excessforUMPay = new List<SelectListItem>();
        //    excessforUMPay = commonModel.excessRate();
        //    Livestock.DescriptionFCObj.DescriptionList = desList;
        //    Livestock.ExcessesFpcObj.ExcessList = excessforUMPay;
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        Livestock.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = Livestock.CustomerId;
        //    }
        //    string policyid = null;
        //    var db = new MasterDataEntities();
        //    if (cid.HasValue && cid > 0)
        //    {
        //        if (Livestock.DescriptionFCObj!=null && Livestock.DescriptionFCObj.EiId > 0 && Livestock.DescriptionFCObj.Description != null)
        //        {
        //            db.IT_InsertCustomerQnsData(Livestock.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), Livestock.DescriptionFCObj.EiId, Livestock.DescriptionFCObj.Description.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Livestock.NumberanimalObj!=null && Livestock.NumberanimalObj.EiId > 0 && Livestock.NumberanimalObj.Numberanimal != null)
        //        {
        //            db.IT_InsertCustomerQnsData(Livestock.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), Livestock.NumberanimalObj.EiId, Livestock.NumberanimalObj.Numberanimal.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Livestock.SuminsuredperObj!=null && Livestock.SuminsuredperObj.EiId > 0 && Livestock.SuminsuredperObj.Suminsuredper != null)
        //        {
        //            db.IT_InsertCustomerQnsData(Livestock.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), Livestock.SuminsuredperObj.EiId, Livestock.SuminsuredperObj.Suminsuredper.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Livestock.SuminsuredObj!=null && Livestock.SuminsuredObj.EiId > 0 && Livestock.SuminsuredObj.Suminsured != null)
        //        {
        //            db.IT_InsertCustomerQnsData(Livestock.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), Livestock.SuminsuredObj.EiId, Livestock.SuminsuredObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Livestock.DogattackObj!=null && Livestock.DogattackObj.EiId > 0 && Livestock.DogattackObj.Dogattack != null)
        //        {
        //            db.IT_InsertCustomerQnsData(Livestock.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), Livestock.DogattackObj.EiId, Livestock.DogattackObj.Dogattack.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Livestock.ExcessesFpcObj != null && Livestock.ExcessesFpcObj.EiId > 0 && Livestock.ExcessesFpcObj.Excess != null)
        //        {
        //            db.IT_InsertCustomerQnsData(Livestock.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), Livestock.ExcessesFpcObj.EiId, Livestock.ExcessesFpcObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Session["CompletionTrackFPC"] != null)
        //        {
        //            Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
        //            Livestock.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
        //            if (Livestock.CompletionTrackFPC != null)
        //            {
        //                string Completionstring = string.Empty;
        //                char[] arr = Livestock.CompletionTrackFPC.ToCharArray();

        //                for (int i = 0; i < arr.Length; i++)
        //                {
        //                    char a = arr[i];
        //                    if (i == 4)
        //                    {
        //                        a = '1';
        //                    }
        //                    Completionstring = Completionstring + a;
        //                }
        //                Session["CompletionTrackFPC"] = Completionstring;
        //                Livestock.CompletionTrackFPC = Completionstring;
        //            }
        //        }
        //        else
        //        {
        //            Session["CompletionTrackFPC"] = "0-0-1-0"; ;
        //            Livestock.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
        //        }
        //        return RedirectToAction("HarvestedCropsBeehives", new { cid = Livestock.CustomerId });
        //    }
        //    return View(Livestock);
        //}
        //[HttpGet]
        //public ActionResult HarvestedCropsBeehives(int? cid)
        //{
        //    NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
        //    List<SelectListItem> excessforUMPay = new List<SelectListItem>();
        //    excessforUMPay = commonModel.excessRate();
        //    HarvestedCropsBeehives HarvestedCropsBeehives = new HarvestedCropsBeehives();
        //    HarvestedCropsBeehives.SuminsuredObj = new SumOfInsured();
        //    HarvestedCropsBeehives.SuminsuredObj.EiId = 60593;
        //    HarvestedCropsBeehives.ExcessesFpcObj = new ExcessesFPC();
        //    HarvestedCropsBeehives.ExcessesFpcObj.ExcessList = excessforUMPay;
        //    HarvestedCropsBeehives.ExcessesFpcObj.EiId = 60595;
        //    HarvestedCropsBeehives.SuminsuredHbcObj = new SumOfInsuredHCB();
        //    HarvestedCropsBeehives.SuminsuredHbcObj.EiId = 60601;
        //    HarvestedCropsBeehives.NumberhiveObj = new NumberOfHives();
        //    HarvestedCropsBeehives.NumberhiveObj.EiId = 60603;
        //    HarvestedCropsBeehives.ExcessBObj = new ExcessesBeehives();
        //    HarvestedCropsBeehives.ExcessBObj.ExcessBList = excessforUMPay;
        //    HarvestedCropsBeehives.ExcessBObj.EiId = 60595;
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        HarvestedCropsBeehives.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = HarvestedCropsBeehives.CustomerId;
        //    }
        //    if (Session["CompletionTrackFPC"] != null)
        //    {
        //        Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
        //        HarvestedCropsBeehives.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
        //    }
        //    else
        //    {
        //        Session["CompletionTrackFPC"] = "0-0-0-0"; ;
        //        HarvestedCropsBeehives.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(RLSSection.FarmProperty),Convert.ToInt32(PolicyType.RLS) ,policyid).ToList();
        //    if (details != null && details.Any())
        //    {
        //        if (details.Exists(q => q.QuestionId == HarvestedCropsBeehives.SuminsuredObj.EiId))
        //        {
        //            HarvestedCropsBeehives.SuminsuredObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == HarvestedCropsBeehives.SuminsuredObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == HarvestedCropsBeehives.ExcessesFpcObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == HarvestedCropsBeehives.ExcessesFpcObj.EiId).FirstOrDefault();
        //            HarvestedCropsBeehives.ExcessesFpcObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //        if (details.Exists(q => q.QuestionId == HarvestedCropsBeehives.SuminsuredHbcObj.EiId))
        //        {
        //            HarvestedCropsBeehives.SuminsuredHbcObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == HarvestedCropsBeehives.SuminsuredHbcObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == HarvestedCropsBeehives.NumberhiveObj.EiId))
        //        {
        //            HarvestedCropsBeehives.NumberhiveObj.Numberhive = Convert.ToString(details.Where(q => q.QuestionId == HarvestedCropsBeehives.NumberhiveObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == HarvestedCropsBeehives.ExcessBObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == HarvestedCropsBeehives.ExcessBObj.EiId).FirstOrDefault();
        //            HarvestedCropsBeehives.ExcessBObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //    }
        //    return View(HarvestedCropsBeehives);
        //}
        //[HttpPost]
        //public ActionResult HarvestedCropsBeehives(HarvestedCropsBeehives HarvestedCropsBeehives, int? cid)
        //{
        //    NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
        //    List<SelectListItem> excessforUMPay = new List<SelectListItem>();
        //    excessforUMPay = commonModel.excessRate();
        //    HarvestedCropsBeehives.ExcessesFpcObj.ExcessList = excessforUMPay;
        //    HarvestedCropsBeehives.ExcessBObj.ExcessBList = excessforUMPay;
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        HarvestedCropsBeehives.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = HarvestedCropsBeehives.CustomerId;
        //    }
        //    string policyid = null;
        //    var db = new MasterDataEntities();
        //    if (cid.HasValue && cid > 0)
        //    {
        //        if (HarvestedCropsBeehives.SuminsuredObj!=null && HarvestedCropsBeehives.SuminsuredObj.EiId > 0 && HarvestedCropsBeehives.SuminsuredObj.Suminsured != null)
        //        {
        //            db.IT_InsertCustomerQnsData(HarvestedCropsBeehives.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), HarvestedCropsBeehives.SuminsuredObj.EiId, HarvestedCropsBeehives.SuminsuredObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS),policyid);
        //        }
        //        if (HarvestedCropsBeehives.ExcessesFpcObj!=null && HarvestedCropsBeehives.ExcessesFpcObj.EiId > 0 && HarvestedCropsBeehives.ExcessesFpcObj.Excess != null)
        //        {
        //            db.IT_InsertCustomerQnsData(HarvestedCropsBeehives.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), HarvestedCropsBeehives.ExcessesFpcObj.EiId, HarvestedCropsBeehives.ExcessesFpcObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (HarvestedCropsBeehives.SuminsuredHbcObj != null && HarvestedCropsBeehives.SuminsuredHbcObj.EiId > 0 && HarvestedCropsBeehives.SuminsuredHbcObj.Suminsured != null)
        //        {
        //            db.IT_InsertCustomerQnsData(HarvestedCropsBeehives.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), HarvestedCropsBeehives.SuminsuredHbcObj.EiId, HarvestedCropsBeehives.SuminsuredHbcObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (HarvestedCropsBeehives.NumberhiveObj != null && HarvestedCropsBeehives.NumberhiveObj.EiId > 0 && HarvestedCropsBeehives.NumberhiveObj.Numberhive != null)
        //        {
        //            db.IT_InsertCustomerQnsData(HarvestedCropsBeehives.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), HarvestedCropsBeehives.NumberhiveObj.EiId, HarvestedCropsBeehives.NumberhiveObj.Numberhive.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (HarvestedCropsBeehives.ExcessBObj!=null && HarvestedCropsBeehives.ExcessBObj.EiId > 0 && HarvestedCropsBeehives.ExcessBObj.Excess != null)
        //        {
        //            db.IT_InsertCustomerQnsData(HarvestedCropsBeehives.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), HarvestedCropsBeehives.ExcessBObj.EiId, HarvestedCropsBeehives.ExcessBObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        //if (Session["CompletionTrackFPC"] != null)
        //        //{
        //        //    Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
        //        //    HarvestedCropsBeehives.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
        //        //    if (HarvestedCropsBeehives.CompletionTrackFPC != null)
        //        //    {
        //        //        string Completionstring = string.Empty;
        //        //        char[] arr = HarvestedCropsBeehives.CompletionTrackFPC.ToCharArray();

        //        //        for (int i = 0; i < arr.Length; i++)
        //        //        {
        //        //            char a = arr[i];
        //        //            if (i == 6)
        //        //            {
        //        //                a = '1';
        //        //            }
        //        //            Completionstring = Completionstring + a;
        //        //        }
        //        //        Session["CompletionTrackFPC"] = Completionstring;
        //        //        HarvestedCropsBeehives.CompletionTrackFPC = Completionstring;
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    Session["CompletionTrackFPC"] = "0-0-0-1"; ;
        //        //    HarvestedCropsBeehives.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
        //        //}
        //        return RedirectToAction("VehicleDescription", "MotorCover", new { cid = HarvestedCropsBeehives.CustomerId });
        //        return RedirectToAction("PetsCover","Pets", new { cid = HarvestedCropsBeehives.CustomerId });
        //    }
        //    return View(HarvestedCropsBeehives);
        //}
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
                else if (content == "liveStock")
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