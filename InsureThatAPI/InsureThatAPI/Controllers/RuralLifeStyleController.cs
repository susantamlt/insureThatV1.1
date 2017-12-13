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
    public class RuralLifeStyleController : Controller
    {
        // GET: RuralLifeStyle
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Valuables()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Valuables(Valuables valuable)
        {
            return View();
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> HomeDescription(int? cid, int? PcId)
        {
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Contains("Home"))
                    {

                    }
                    else
                    {
                        if (Policyincllist.Contains("HomeContents"))
                        {
                            return RedirectToAction("HomeContent", "HomeContentValuable", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Valuables"))
                        {
                            return RedirectToAction("Valuables", "HomeContentValuable", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("FarmProperty"))
                        {
                            return RedirectToAction("FarmContents", "Farm", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Liability"))
                        {
                            return RedirectToAction("LiabilityCover", "Liabilities", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Travel"))
                        {
                            return RedirectToAction("TravelCover", "Travel", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Boat"))
                        {
                            return RedirectToAction("BoatDetails", "Boat", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Motor"))
                        {
                            return RedirectToAction("VehicleDescription", "MotorCover", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Pet"))
                        {
                            return RedirectToAction("PetsCover", "Pets", new { cid = cid });
                        }
                    }
                }
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 1 });
            }
            HB2HomeDescription homebuilding = new HB2HomeDescription();
            homebuilding.LocationObj = new Locations();
            homebuilding.LocationObj.EiId = 60133;
            homebuilding.LocationObj.Location = "";
            if (Session["apiKey"] != null)
            {
                homebuilding.ApiKey = Session["apiKey"].ToString();
            }
            if (Session["completionTrack"] != null)
            {
                Session["completionTrack"] = Session["completionTrack"];
                homebuilding.CompletionTrack = Session["completionTrack"].ToString();
            }
            else
            {
                Session["completionTrack"] = "0-0-0-0-0"; ;
                homebuilding.CompletionTrack = Session["completionTrack"].ToString();
            }
            ViewBag.cid = cid;
            if (cid != null)
            {
                homebuilding.CustomerId = cid.Value;
            }
            homebuilding.AreapropertyObj = new Areapropertys();
            homebuilding.AreapropertyObj.EiId = 60009;
            //homebuilding.AreapropertyObj.Areaproper
            homebuilding.IsbuildinglocatedObj = new IsBuildingLocateds();
            homebuilding.IsbuildinglocatedObj.EiId = 60013;

            homebuilding.DescribeaddressObj = new DescribeAddresses();
            homebuilding.DescribeaddressObj.EiId = 60007;

            var db = new MasterDataEntities();
            string policyid = null;
            if (PcId != null && PcId > 0)
            {
                policyid = PcId.ToString();
            }
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.HomeBuilding), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
            if (details != null && details.Any())
            {

                if (details.Exists(q => q.QuestionId == homebuilding.LocationObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == homebuilding.LocationObj.EiId).FirstOrDefault();
                    homebuilding.LocationObj.Location = loc.Answer;
                }
                if (details.Exists(q => q.QuestionId == homebuilding.AreapropertyObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == homebuilding.AreapropertyObj.EiId).FirstOrDefault();
                    homebuilding.AreapropertyObj.Areaproperty = !string.IsNullOrEmpty(loc.Answer) ? (int?)Convert.ToInt32(loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == homebuilding.IsbuildinglocatedObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == homebuilding.IsbuildinglocatedObj.EiId).FirstOrDefault();
                    homebuilding.IsbuildinglocatedObj.Isbuildinglocated = !string.IsNullOrEmpty(loc.Answer) ? (int?)Convert.ToInt32(loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == homebuilding.DescribeaddressObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == homebuilding.DescribeaddressObj.EiId).FirstOrDefault();
                    homebuilding.DescribeaddressObj.Describeaddress = !string.IsNullOrEmpty(loc.Answer) ? (int?)Convert.ToInt32(loc.Answer) : null;
                }

            }
            else
            {
                HttpClient hclient = new HttpClient();
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string PlainTextEncrpted = string.Empty;
                string ApiKey = string.Empty;
                ApiKey = Session["apiKey"].ToString();
                HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Home Buildings&SectionUnId=2&ProfileUnId=1");/*insureddetails.InsuredID */
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                SectionD sdata = new SectionD();
                homebuilding.SectionDatas = JsonConvert.DeserializeObject<SectionD>(EmpResponse);
                if (homebuilding.SectionDatas.ErrorMessage != null && homebuilding.SectionDatas.ErrorMessage.Count > 0 && homebuilding.SectionDatas.ErrorMessage.Contains("API Session Expired"))
                {
                    return RedirectToAction("AgentLogin", "Login");
                }
                if(homebuilding.SectionDatas!=null && homebuilding.SectionDatas.SectionData.ValueData!=null)
                {
                    for(int value=0; value<=homebuilding.SectionDatas.SectionData.ValueData.Count; value++ )
                    {
                        db.IT_InsertCustomerQnsData(cid, Convert.ToInt32(RLSSection.HomeBuilding), homebuilding.SectionDatas.SectionData.ValueData[value].Element.ElId, homebuilding.SectionDatas.SectionData.ValueData[value].Value, Convert.ToInt32(PolicyType.RLS), policyid);//need to change
                        if (homebuilding.SectionDatas.SectionData.ValueData[value].Element.ElId == homebuilding.LocationObj.EiId)
                        {
                        
                            homebuilding.LocationObj.Location = homebuilding.SectionDatas.SectionData.ValueData[value].Value;
                        }
                        if (homebuilding.SectionDatas.SectionData.ValueData[value].Element.ElId == homebuilding.AreapropertyObj.EiId && homebuilding.SectionDatas.SectionData.ValueData[value].Value!=null )
                        {
                        
                            homebuilding.AreapropertyObj.Areaproperty = Convert.ToInt32(homebuilding.SectionDatas.SectionData.ValueData[value].Value);
                        }
                        if (homebuilding.SectionDatas.SectionData.ValueData[value].Element.ElId == homebuilding.IsbuildinglocatedObj.EiId && homebuilding.SectionDatas.SectionData.ValueData[value].Value!=null)
                        {
                         
                            homebuilding.IsbuildinglocatedObj.Isbuildinglocated  = Convert.ToInt32(homebuilding.SectionDatas.SectionData.ValueData[value].Value);
                        }
                        if (homebuilding.SectionDatas.SectionData.ValueData[value].Element.ElId == homebuilding.DescribeaddressObj.EiId)
                        {
                         
                            homebuilding.DescribeaddressObj.Describeaddress = Convert.ToInt32(homebuilding.SectionDatas.SectionData.ValueData[value].Value);
                        }
                    }
                }
            }
            var suburblist = db.IT_Master_GetSuburbList().ToList();

            homebuilding.SubUrb = suburblist.Where(s => !string.IsNullOrEmpty(s)).Select(s => new SelectListItem() { Text = s, Value = s }).ToList();
            return View(homebuilding);
        }
        [HttpPost]
        public ActionResult HomeDescription(int? cid, HB2HomeDescription homebuilding)
        {
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                homebuilding.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = homebuilding.CustomerId;
            }
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (homebuilding.AreapropertyObj.Areaproperty != null)
                {
                    db.IT_InsertCustomerQnsData(homebuilding.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), homebuilding.AreapropertyObj.EiId, homebuilding.AreapropertyObj.Areaproperty.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                //if (homebuilding.LocationObj.Location != null)
                //{
                //    db.IT_InsertCustomerQnsData(homebuilding.CustomerId, 1, homebuilding.LocationObj.EiId, homebuilding.LocationObj.Location);
                //}
                if (homebuilding.IsbuildinglocatedObj.Isbuildinglocated != null)
                {
                    db.IT_InsertCustomerQnsData(homebuilding.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), homebuilding.IsbuildinglocatedObj.EiId, homebuilding.IsbuildinglocatedObj.Isbuildinglocated.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (homebuilding.DescribeaddressObj.Describeaddress != null)
                {

                    db.IT_InsertCustomerQnsData(homebuilding.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), homebuilding.DescribeaddressObj.EiId, homebuilding.DescribeaddressObj.Describeaddress.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);//need to change
                }
                if (Session["completionTrack"] != null)
                {
                    Session["completionTrack"] = Session["completionTrack"];
                    homebuilding.CompletionTrack = Session["completionTrack"].ToString();
                    if (homebuilding.CompletionTrack != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = homebuilding.CompletionTrack.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 0)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrack"] = Completionstring;
                        homebuilding.CompletionTrack = Completionstring;
                    }

                }
                else
                {
                    Session["completionTrack"] = "1-0-0-0-0"; ;
                    homebuilding.CompletionTrack = Session["completionTrack"].ToString();
                }
                // homebuilding.CompletionTrack = "1-0-0-0-0";
                return RedirectToAction("ConstructionDetails", new { cid = cid });
            }
            return View(homebuilding);
        }
        #region AJAX call to get the state and pincode list on suburb selection
        [HttpPost]
        public ActionResult SubUrbStatePincode(string suburb, int? pincode)
        {
            if (Request.IsAjaxRequest())
            {

                var db = new MasterDataEntities();
                if (pincode.HasValue && pincode > 0)
                {
                    var statepincodelist = db.IT_Master_GetSuburbDetails(suburb, pincode.ToString()).ToList();
                }
                else
                {

                    var statepincodelist = db.IT_Master_GetSuburbDetails(suburb, null).ToList();
                }
                // return Json(new { results = insureddetails.Insureds.Select(p => new InsuredListDDL() { id = p.InsuredID, text = p.CompanyBusinessName + p.FirstName + " " + p.MiddleName + " " + p.LastName }).ToList() });

            }
            return View();//Json({ });
        }
        #endregion
        [HttpGet]
        public ActionResult ConstructionDetails(int? cid)
        {
            NewPolicyDetailsClass ConstructionDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> ExternalWallsMadeList = new List<SelectListItem>();
            ExternalWallsMadeList = ConstructionDetailsmodel.ExternalWallsMadeList();
            List<SelectListItem> IsRoofMadeOfList = new List<SelectListItem>();
            IsRoofMadeOfList = ConstructionDetailsmodel.RoofMadesList();

            HB2ConstructionDetails constructionDetails = new HB2ConstructionDetails();
            if (cid != null)
            {
                ViewBag.cid = cid;
                constructionDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = constructionDetails.CustomerId;
            }
            if (Session["completionTrack"] != null)
            {
                Session["completionTrack"] = Session["completionTrack"];
                constructionDetails.CompletionTrack = Session["completionTrack"].ToString();
            }
            else
            {
                Session["completionTrack"] = "0-0-0-0-0"; ;
                constructionDetails.CompletionTrack = Session["completionTrack"].ToString();
            }
            if (Session["apiKey"] != null)
            {
                constructionDetails.ApiKey = Session["apiKey"].ToString();
            }
            constructionDetails.ExtwallsmadeObj = new ExtWallsMades();
            constructionDetails.ExtwallsmadeObj.ExtwallsmadeList = ExternalWallsMadeList;
            constructionDetails.ExtwallsmadeObj.EiId = 60029;

            constructionDetails.DescribeexternalwallsObj = new Describeexternalwalls();
            constructionDetails.DescribeexternalwallsObj.EiId = 60031;

            constructionDetails.RoofmadeObj = new RoofMades();
            constructionDetails.RoofmadeObj.RoofmadeList = IsRoofMadeOfList;
            constructionDetails.RoofmadeObj.EiId = 60033;

            constructionDetails.DescribeRoofMadeOffObj = new DescribeRoofMadeof();
            constructionDetails.DescribeRoofMadeOffObj.EiId = 60035;

            constructionDetails.YearofBuiltObj = new YearOfBuilt();
            constructionDetails.YearofBuiltObj.EiId = 60037;

            constructionDetails.HeritagelegislationObj = new HeritageLegislations();
            constructionDetails.HeritagelegislationObj.EiId = 60045;

            constructionDetails.UnderconstructionObj = new UnderConstructions();
            constructionDetails.UnderconstructionObj.EiId = 60055;

            constructionDetails.WatertightObj = new Watertights();
            constructionDetails.WatertightObj.EiId = 60043;

            constructionDetails.DomesticdwellingObj = new DomesticDwellings();
            constructionDetails.DomesticdwellingObj.EiId = 60057;
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(RLSSection.HomeBuilding),Convert.ToInt32(PolicyType.RLS),policyid).ToList();
            if (details != null && details.Any())
            {

                if (details.Exists(q => q.QuestionId == constructionDetails.ExtwallsmadeObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == constructionDetails.ExtwallsmadeObj.EiId).FirstOrDefault();
                    constructionDetails.ExtwallsmadeObj.Extwallsmade = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == constructionDetails.DescribeexternalwallsObj.EiId))
                {
                    constructionDetails.DescribeexternalwallsObj.Describeexternalwall = Convert.ToString(details.Where(q => q.QuestionId == constructionDetails.DescribeexternalwallsObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == constructionDetails.RoofmadeObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == constructionDetails.RoofmadeObj.EiId).FirstOrDefault();
                    constructionDetails.RoofmadeObj.Roofmade = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == constructionDetails.DescribeRoofMadeOffObj.EiId))
                {
                    constructionDetails.DescribeRoofMadeOffObj.DescribeRoofMade = Convert.ToString(details.Where(q => q.QuestionId == constructionDetails.DescribeRoofMadeOffObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == constructionDetails.YearofBuiltObj.EiId))
                {
                    constructionDetails.YearofBuiltObj.YearBuilt = Convert.ToString(details.Where(q => q.QuestionId == constructionDetails.YearofBuiltObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == constructionDetails.WatertightObj.EiId))
                {
                    constructionDetails.WatertightObj.Watertight = Convert.ToBoolean(details.Where(q => q.QuestionId == constructionDetails.WatertightObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == constructionDetails.HeritagelegislationObj.EiId))
                {
                    constructionDetails.HeritagelegislationObj.Heritagelegislation = Convert.ToBoolean(details.Where(q => q.QuestionId == constructionDetails.HeritagelegislationObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == constructionDetails.UnderconstructionObj.EiId))
                {
                    constructionDetails.UnderconstructionObj.Underconstruction = Convert.ToBoolean(details.Where(q => q.QuestionId == constructionDetails.UnderconstructionObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == constructionDetails.DomesticdwellingObj.EiId))
                {
                    constructionDetails.DomesticdwellingObj.Domesticdwelling = Convert.ToBoolean(details.Where(q => q.QuestionId == constructionDetails.DomesticdwellingObj.EiId).FirstOrDefault().Answer);
                }
            }
            return View(constructionDetails);
        }
        [HttpPost]
        public ActionResult ConstructionDetails(HB2ConstructionDetails ConstructionDetails, int? cid)
        {
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                ConstructionDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = ConstructionDetails.CustomerId;
            }
            NewPolicyDetailsClass ConstructionDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> ExternalWallsMadeList = new List<SelectListItem>();
            ExternalWallsMadeList = ConstructionDetailsmodel.ExternalWallsMadeList();
            List<SelectListItem> IsRoofMadeOfList = new List<SelectListItem>();
            IsRoofMadeOfList = ConstructionDetailsmodel.RoofMadesList();
            ConstructionDetails.ExtwallsmadeObj.ExtwallsmadeList = ExternalWallsMadeList;
            ConstructionDetails.RoofmadeObj.RoofmadeList = IsRoofMadeOfList;
            string policyid = null;
            if (ConstructionDetails.CustomerId != null && ConstructionDetails.CustomerId != 0)
            {
                if (ConstructionDetails.DescribeexternalwallsObj.Describeexternalwall != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), ConstructionDetails.DescribeexternalwallsObj.EiId, ConstructionDetails.DescribeexternalwallsObj.Describeexternalwall.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (ConstructionDetails.DescribeRoofMadeOffObj.DescribeRoofMade != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), ConstructionDetails.DescribeRoofMadeOffObj.EiId, ConstructionDetails.DescribeRoofMadeOffObj.DescribeRoofMade, Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (ConstructionDetails.DomesticdwellingObj.Domesticdwelling != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), ConstructionDetails.DomesticdwellingObj.EiId, ConstructionDetails.DomesticdwellingObj.Domesticdwelling.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (ConstructionDetails.ExtwallsmadeObj.Extwallsmade != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), ConstructionDetails.ExtwallsmadeObj.EiId, ConstructionDetails.ExtwallsmadeObj.Extwallsmade.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (ConstructionDetails.HeritagelegislationObj.Heritagelegislation != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), ConstructionDetails.HeritagelegislationObj.EiId, ConstructionDetails.HeritagelegislationObj.Heritagelegislation.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (ConstructionDetails.RoofmadeObj.Roofmade != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), ConstructionDetails.RoofmadeObj.EiId, ConstructionDetails.RoofmadeObj.Roofmade.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);//need to change
                }
                if (ConstructionDetails.UnderconstructionObj.Underconstruction != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), ConstructionDetails.UnderconstructionObj.EiId, ConstructionDetails.UnderconstructionObj.Underconstruction.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (ConstructionDetails.WatertightObj.Watertight != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), ConstructionDetails.WatertightObj.EiId, ConstructionDetails.WatertightObj.Watertight.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (ConstructionDetails.YearofBuiltObj.YearBuilt != null)
                {
                    db.IT_InsertCustomerQnsData(ConstructionDetails.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), ConstructionDetails.YearofBuiltObj.EiId, ConstructionDetails.YearofBuiltObj.YearBuilt.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Session["completionTrack"] != null)
                {
                    Session["completionTrack"] = Session["completionTrack"];
                    ConstructionDetails.CompletionTrack = Session["completionTrack"].ToString();
                    if (ConstructionDetails.CompletionTrack != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = ConstructionDetails.CompletionTrack.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 2)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrack"] = Completionstring;
                        ConstructionDetails.CompletionTrack = Completionstring;
                    }

                }
                else
                {
                    Session["completionTrack"] = "0-1-0-0-0"; ;
                    ConstructionDetails.CompletionTrack = Session["completionTrack"].ToString();
                }
            }
            return RedirectToAction("OccupancyDetails", new { cid = ConstructionDetails.CustomerId });
        }
        [HttpGet]
        public ActionResult OccupancyDetails(int? cid)
        {
            HB2OccupancyDetails occupancydetails = new HB2OccupancyDetails();
            occupancydetails.WholivesObj = new WhoLives();
            occupancydetails.WholivesObj.EiId = 60071;
            occupancydetails.WholivesObj.Wholives = 0;
            if (cid != null)
            {
                ViewBag.cid = cid;
                occupancydetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = occupancydetails.CustomerId;
            }
            if (Session["completionTrack"] != null)
            {
                Session["completionTrack"] = Session["completionTrack"];
                occupancydetails.CompletionTrack = Session["completionTrack"].ToString();
            }
            else
            {
                Session["completionTrack"] = "0-0-0-0-0"; ;
                occupancydetails.CompletionTrack = Session["completionTrack"].ToString();
            }
            occupancydetails.IsbuildingObj = new IsBuildings();
            occupancydetails.IsbuildingObj.EiId = 60073;
            occupancydetails.IsbuildingObj.Isbuilding = 0;


            occupancydetails.ConsecutivedayObj = new Consecutivedays();
            occupancydetails.ConsecutivedayObj.EiId = 60075;
            occupancydetails.ConsecutivedayObj.Consecutiveday = 0;

            occupancydetails.IsusedbusinessObj = new IsusedBusinesses();
            occupancydetails.IsusedbusinessObj.EiId = 60077;

            occupancydetails.DescribebusinessObj = new DescribeBusinesses();
            occupancydetails.DescribebusinessObj.EiId = 60079;

            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.HomeBuilding),Convert.ToInt32(PolicyType.RLS),policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == occupancydetails.WholivesObj.EiId))
                {
                    occupancydetails.WholivesObj.Wholives = Convert.ToInt32(details.Where(q => q.QuestionId == occupancydetails.WholivesObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == occupancydetails.DescribebusinessObj.EiId))
                {
                    occupancydetails.DescribebusinessObj.Describebusiness = Convert.ToInt32(details.Where(q => q.QuestionId == occupancydetails.DescribebusinessObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == occupancydetails.IsusedbusinessObj.EiId))
                {
                    occupancydetails.IsusedbusinessObj.Isusedbusiness = Convert.ToInt32(details.Where(q => q.QuestionId == occupancydetails.IsusedbusinessObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == occupancydetails.ConsecutivedayObj.EiId))
                {
                    occupancydetails.ConsecutivedayObj.Consecutiveday = Convert.ToInt32(details.Where(q => q.QuestionId == occupancydetails.ConsecutivedayObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == occupancydetails.IsbuildingObj.EiId))
                {
                    occupancydetails.IsbuildingObj.Isbuilding = Convert.ToInt32(details.Where(q => q.QuestionId == occupancydetails.IsbuildingObj.EiId).FirstOrDefault().Answer);
                }

            }
            //occupancydetails.CustomerId = cid.Value;
            return View(occupancydetails);
        }
        [HttpPost]
        public ActionResult OccupancyDetails(int? cid, HB2OccupancyDetails OccupancyDetails)
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
            if (OccupancyDetails.CustomerId != null && OccupancyDetails.CustomerId != 0)
            {
                if (OccupancyDetails.ConsecutivedayObj.Consecutiveday != null)
                {
                    db.IT_InsertCustomerQnsData(OccupancyDetails.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), OccupancyDetails.ConsecutivedayObj.EiId, OccupancyDetails.ConsecutivedayObj.Consecutiveday.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (OccupancyDetails.DescribebusinessObj.Describebusiness != null)
                {
                    db.IT_InsertCustomerQnsData(OccupancyDetails.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), OccupancyDetails.DescribebusinessObj.EiId, OccupancyDetails.DescribebusinessObj.Describebusiness.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (OccupancyDetails.IsbuildingObj.Isbuilding != null)
                {
                    db.IT_InsertCustomerQnsData(OccupancyDetails.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), OccupancyDetails.IsbuildingObj.EiId, OccupancyDetails.IsbuildingObj.Isbuilding.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (OccupancyDetails.IsusedbusinessObj.Isusedbusiness != null)
                {
                    db.IT_InsertCustomerQnsData(OccupancyDetails.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), OccupancyDetails.IsusedbusinessObj.EiId, OccupancyDetails.IsusedbusinessObj.Isusedbusiness.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (OccupancyDetails.WholivesObj.Wholives != null)
                {
                    db.IT_InsertCustomerQnsData(OccupancyDetails.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), OccupancyDetails.WholivesObj.EiId, OccupancyDetails.WholivesObj.Wholives.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }

                if (Session["completionTrack"] != null)
                {
                    Session["completionTrack"] = Session["completionTrack"];
                    OccupancyDetails.CompletionTrack = Session["completionTrack"].ToString();
                    if (OccupancyDetails.CompletionTrack != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = OccupancyDetails.CompletionTrack.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 4)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrack"] = Completionstring;
                        OccupancyDetails.CompletionTrack = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrack"] = "0-0-1-0-0"; ;
                    OccupancyDetails.CompletionTrack = Session["completionTrack"].ToString();
                }


            }
            return RedirectToAction("InterestedParties", new { cid = OccupancyDetails.CustomerId });
        }
        [HttpGet]
        public ActionResult InterestedParties(int? cid)
        {
            HB2InterestedParties interestedparties = new HB2InterestedParties();
            interestedparties.LocationObj = new Locations();
            interestedparties.LocationObj.EiId = 60031;
            if (cid != null)
            {
                ViewBag.cid = cid;
                interestedparties.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = interestedparties.CustomerId;
            }
            if (Session["completionTrack"] != null)
            {
                Session["completionTrack"] = Session["completionTrack"];
                interestedparties.CompletionTrack = Session["completionTrack"].ToString();
            }
            else
            {
                Session["completionTrack"] = "0-0-0-0-0"; ;
                interestedparties.CompletionTrack = Session["completionTrack"].ToString();
            }
            interestedparties.CoverhomebuildingObj = new CoverHomeBuildings();
            interestedparties.CoverhomebuildingObj.EiId = 60133;
            MasterDataEntities db = new MasterDataEntities();
            // interestedparties.CustomerId = cid.Value;
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(RLSSection.HomeBuilding),Convert.ToInt32(PolicyType.RLS),policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == interestedparties.CoverhomebuildingObj.EiId))
                {
                    interestedparties.CoverhomebuildingObj.Coverhomebuilding = details.Where(q => q.QuestionId == interestedparties.CoverhomebuildingObj.EiId).FirstOrDefault().Answer.ToString();
                }
                if (details.Exists(q => q.QuestionId == interestedparties.LocationObj.EiId))
                {
                    interestedparties.LocationObj.Location = details.Where(q => q.QuestionId == interestedparties.LocationObj.EiId).FirstOrDefault().Answer;
                }
            }
            return View(interestedparties);
        }
        [HttpPost]
        public ActionResult InterestedParties(int? cid, HB2InterestedParties InterestedParties)
        {
            MasterDataEntities db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                InterestedParties.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = InterestedParties.CustomerId;
            }
            string policyid = null;
            if (InterestedParties.CustomerId != null && InterestedParties.CustomerId != 0)
            {
                if (InterestedParties.LocationObj.Location != null)
                {
                    db.IT_InsertCustomerQnsData(InterestedParties.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), InterestedParties.LocationObj.EiId, InterestedParties.LocationObj.Location.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (InterestedParties.CoverhomebuildingObj.Coverhomebuilding != null)
                {
                    db.IT_InsertCustomerQnsData(InterestedParties.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), InterestedParties.CoverhomebuildingObj.EiId, InterestedParties.CoverhomebuildingObj.Coverhomebuilding.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Session["completionTrack"] != null)
                {
                    Session["completionTrack"] = Session["completionTrack"];
                    InterestedParties.CompletionTrack = Session["completionTrack"].ToString();
                    if (InterestedParties.CompletionTrack != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = InterestedParties.CompletionTrack.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 6)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrack"] = Completionstring;
                        InterestedParties.CompletionTrack = Completionstring;
                    }

                }
                else
                {
                    Session["completionTrack"] = "0-0-0-1-0"; ;
                    InterestedParties.CompletionTrack = Session["completionTrack"].ToString();
                }
                return RedirectToAction("HomeBuilding", new { cid = InterestedParties.CustomerId });
            }
            return RedirectToAction("HomeBuilding", new { cid = InterestedParties.CustomerId });

        }
        [HttpGet]
        public ActionResult HomeBuilding(int? cid)
        {
            HB2HomeBuilding homebuilding = new HB2HomeBuilding();
            if (cid != null)
            {
                ViewBag.cid = cid;
                homebuilding.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = homebuilding.CustomerId;
            }
            homebuilding.CostforRebuildingObj = new CostForRebuilding();
            homebuilding.CostforRebuildingObj.EiId = 60187;
            if (Session["completionTrack"] != null)
            {
                Session["completionTrack"] = Session["completionTrack"];
                homebuilding.completiontrack = Session["completionTrack"].ToString();
            }
            else
            {
                Session["completionTrack"] = "0-0-0-0-0"; ;
                homebuilding.completiontrack = Session["completionTrack"].ToString();
            }
            homebuilding.ClaimfreeperiodObj = new ClaimFreePeriods();
            homebuilding.ClaimfreeperiodObj.EiId = 60195;
            homebuilding.ExcessObj = new Excesses();
            homebuilding.ExcessObj.EiId = 60197;
            MasterDataEntities db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.HomeBuilding),Convert.ToInt32(PolicyType.RLS),policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == homebuilding.CostforRebuildingObj.EiId))
                {
                    homebuilding.CostforRebuildingObj.CostforRebuilding = Convert.ToInt32(details.Where(q => q.QuestionId == homebuilding.CostforRebuildingObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == homebuilding.ClaimfreeperiodObj.EiId))
                {
                    homebuilding.ClaimfreeperiodObj.Claimfreeperiod = Convert.ToInt32(details.Where(q => q.QuestionId == homebuilding.ClaimfreeperiodObj.EiId).FirstOrDefault().Answer).ToString();
                }
                if (details.Exists(q => q.QuestionId == homebuilding.ExcessObj.EiId))
                {
                    homebuilding.ExcessObj.Excess = Convert.ToInt32(details.Where(q => q.QuestionId == homebuilding.ExcessObj.EiId).FirstOrDefault().Answer).ToString();
                }
            }
            return View(homebuilding);
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> HomeBuilding(int? cid, HB2HomeBuilding homebuilding)
        {
            MasterDataEntities db = new MasterDataEntities();
            HttpClient hclient = new HttpClient();
            if (cid != null)
            {
                ViewBag.cid = cid;
                homebuilding.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = homebuilding.CustomerId;
            }
            string policyid = null;
            if (homebuilding.CustomerId != null && homebuilding.CustomerId > 0)
            {
                if (homebuilding.CostforRebuildingObj.CostforRebuilding != null)
                {
                    db.IT_InsertCustomerQnsData(homebuilding.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), homebuilding.CostforRebuildingObj.EiId, homebuilding.CostforRebuildingObj.CostforRebuilding.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (homebuilding.ClaimfreeperiodObj.Claimfreeperiod != null)
                {
                    db.IT_InsertCustomerQnsData(homebuilding.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), homebuilding.ClaimfreeperiodObj.EiId, homebuilding.ClaimfreeperiodObj.Claimfreeperiod.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (homebuilding.ExcessObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(homebuilding.CustomerId, Convert.ToInt32(RLSSection.HomeBuilding), homebuilding.ExcessObj.EiId, homebuilding.ExcessObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }

                LogInDetailsClass logindetails = new LogInDetailsClass();
                string loginKey = string.Empty;
                string PlainTextEncrpted = string.Empty;
                int IyId = 9262;
                string UserName = string.Empty;
                UserName = "Joseph.Antony";
                string EncrptForLogin = String.Format("{0:ddddyyyyMMdd}", DateTime.Now);
                PlainTextEncrpted = IyId + "|" + UserName + "|InsureThatDirect";
                loginKey = logindetails.APIkeyEncrypt(PlainTextEncrpted, EncrptForLogin);

                var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.HomeBuilding),Convert.ToInt32(PolicyType.RLS),policyid).ToList();
                var Qlist = new List<KeyValuePair<string, int>>();
                foreach (var row in details)
                {
                    Qlist.Add(new KeyValuePair<string, int>(row.Answer, row.QuestionId));
                    // Qlist.Add(row.QuestionId, row.Answer);
                }
                string Completionstring = string.Empty;
                char[] arr = homebuilding.completiontrack.ToCharArray();

                //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                //  logindetailsmodel = JsonConvert.DeserializeObject<LogInDetails>(EmpResponse);

                if (Session["completionTrack"] != null)
                {
                    Session["completionTrack"] = Session["completionTrack"];
                    homebuilding.completiontrack = Session["completionTrack"].ToString();
                    if (homebuilding.completiontrack != null)
                    {

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 8)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrack"] = Completionstring;
                        homebuilding.completiontrack = Completionstring;
                    }

                }
                else
                {
                    Session["completionTrack"] = "0-0-0-1-0"; ;
                    homebuilding.completiontrack = Session["completionTrack"].ToString();
                }
                if (homebuilding.completiontrack == "1-1-1-1-1")
                {

                }
                else
                {
                    homebuilding.completiontrack = Completionstring;
                    return View(homebuilding);
                }
                var response = hclient.BaseAddress = new Uri("https://api.insurethat.com.au/");
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/RiskDetails?ApiKey=" + loginKey + "&ObjectName=Home" + "&UnId=" + 1 + "&UnitNumber=" + 1 + "&AddressLine1=" + "Tomworth" + "&AddressLine2=" + null + "&Suburb=" + "&Winzone" + "&State=" + "&Postcode=" + "&List=" + Qlist);
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                }

            }
            return RedirectToAction("HomeContent", "HomeContentValuable", new { cid = homebuilding.CustomerId });

        }
        #region Policy History
        public async System.Threading.Tasks.Task<ActionResult> PolicyHistory(string PcId)
        {
            PolicyHistory model = new PolicyHistory();
            HttpClient hclient = new HttpClient();
            var response = hclient.BaseAddress = new Uri("https://api.insurethat.com.au/");
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int cid = 1;
            ViewBag.cid = cid;
            string ApiKey = string.Empty;
            if (Session["apiKey"] != null)
            {
                Session["apiKey"] = Session["apiKey"];
                ApiKey = Session["apiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }

            HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/api/PolicyHistory?apiKey=" + ApiKey + "&policyNumber=" + PcId);
            if (Res.IsSuccessStatusCode)
            {
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<PolicyHistory>(EmpResponse);
            }
            return View(model);
        }
        #endregion
    }
}