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
    public class HomeContentValuableController : Controller
    {
        // GET: HomeContentValuable
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> HomeContent(int cid, int? PcId)
        {
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            MasterDataEntities db = new MasterDataEntities();
            NewPolicyDetailsClass HCmodel = new NewPolicyDetailsClass();
            List<SelectListItem> HCList = new List<SelectListItem>();
            HomeContent HomeContent = new HomeContent();
            HCList = HCmodel.excessRate();
            string apikey = null;
            //var suburblist = db.IT_Master_GetSuburbList().ToList();
            var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
            if (Session["ApiKey"] != null)
            {
                apikey = Session["ApiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");

            }
            //HomeContent.SubUrb = suburblist.Where(s => !string.IsNullOrEmpty(s)).Select(s => new SelectListItem() { Text = s, Value = s }).ToList();
            HomeContent.AddressObj = new Addresses();
            HomeContent.LocationObj = new LocationNew();
            HomeContent.CosttoreplaceObj = new CostToReplaces();
            HomeContent.CosttoreplaceObj.EiId = 60273;
            HomeContent.DescriptionObj = new Descriptions();
            HomeContent.DescriptionObj.EiId = 60285;
            HomeContent.SuminsuredObj = new SumInsures();
            HomeContent.SuminsuredObj.EiId = 60287;
            HomeContent.TotalcoverObj = new TotalCovers();
            HomeContent.TotalcoverObj.EiId = 60289;
            HomeContent.YearclaimObj = new YearClaims();
            HomeContent.YearclaimObj.EiId = 60301;
            HomeContent.ExcesspayObj = new ExcessesPay();
            HomeContent.ExcesspayObj.ExcessList = HCList;
            HomeContent.ExcesspayObj.EiId = 60303;
            HomeContent.ImposedObj = new Imposednew();
            string policyid = null;
            List<SessionModel> PolicyInclustions = new List<SessionModel>();
            var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
            if (Session["Policyinclustions"] != null)
            {
                #region Policy Selected or not testing              

                // var Suburb = new List<KeyValuePair<string, string>>();
                // List<SelectListItem> listItems = new List<SelectListItem>();
                HomeContent.PolicyInclusions = new List<SessionModel>();
                HomeContent.PolicyInclusions = Policyincllist;

                if (Policyincllist != null)
                {
                    if (Policyincllist.Exists(p => p.name == "Home Content" || p.name == "Home Contents"))
                    {

                    }
                    else if (Policyincllist.Exists(p => p.name == "Valuables"))
                    {
                        return RedirectToAction("Valuables", "HomeContentValuable", new { cid = cid });
                    }
                    else if (Policyincllist.Exists(p => p.name == "Farm Property"))
                    {
                        return RedirectToAction("FarmContents", "Farm", new { cid = cid });
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
                 
                    if (Policyincllist.Exists(p => p.name == "Home Content" || p.name == "Home Contents"))
                    {
                        if (Session["unId"] == null && Session["profileId"] == null)
                        {
                            Session["unId"] = Policyincllist.Where(p => p.name == "Home Content" || p.name == "Home Contents").Select(p => p.UnitId).First();
                            Session["profileId"] = Policyincllist.Where(p => p.name == "Home Content" || p.name == "Home Contents").Select(p => p.ProfileId).First();
                        }
                     
                    }
                    else
                    {
                        return RedirectToAction("DisclosureDetails", "Disclosure", new { cid = cid, PcId = PcId });
                    }
                    //else
                    //{
                    //    return RedirectToAction("PremiumDetails", "Customer", new { cid = cid });
                    //}
                }
                #endregion
            }
            int unid = 0;
            int profileid = 0;
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                unid = Convert.ToInt32(Session["unId"]);
                profileid = Convert.ToInt32(Session["profileId"]);
                HomeContent.PolicyInclusion = policyinclusions;
                if (unid == null || unid == 0)
                {
                    unid = unitdetails.SectionData.UnId;
                    profileid = unitdetails.SectionData.ProfileUnId;
                }
            }
            bool policyinclusion = policyinclusions.Exists(p => p.Name == "Home Contents");
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (policyinclusion == true && PcId != null && PcId.HasValue)
            {
                HomeContent.ExistingPolicyInclustions = policyinclusions;

                //int sectionId = policyinclusions.Where(p => p.Name == "Home Contents" && p.UnitNumber == unid).Select(p => p.UnId).FirstOrDefault();
                //int? profileunid = policyinclusions.Where(p => p.Name == "Home Contents" && p.ProfileUnId == profileid).Select(p => p.ProfileUnId).FirstOrDefault();
                HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                if (EmpResponse != null)
                {
                    unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                }
            }
            else if (PcId == null && Session["unId"] != null && (Session["profileId"] != null))
            {
                unid = Convert.ToInt32(Session["unId"]);
                profileid = Convert.ToInt32(Session["profileId"]);
                HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                if (EmpResponse != null)
                {
                    unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                }
            }
            else
            {
                int HprofileId = -1;
                if (Session["HprofileId"] != null)
                {
                    HprofileId = Convert.ToInt32(Session["HprofileId"]);
                }
                HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Home Contents&SectionUnId=&ProfileUnId=" + HprofileId);
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
                    if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Home Contents"))
                    {
                        var policyhomelist = Policyincllist.FindAll(p => p.name == "Home Contents").ToList();
                        if (policyhomelist != null && policyhomelist.Count() > 0)
                        {
                            if (Policyincllist.FindAll(p => p.name == "Home Contents").Exists(p => p.UnitId == null))
                            {
                                Policyincllist.FindAll(p => p.name == "Home Contents").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;
                            }
                            if (Policyincllist.FindAll(p => p.name == "Home Contents").Exists(p => p.ProfileId == null))
                            {
                                Policyincllist.FindAll(p => p.name == "Home Contents").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                            }
                        }
                        else
                        {
                            Policyincllist.FindAll(p => p.name == "Home Contents").First().UnitId = unitdetails.SectionData.UnId;
                            Policyincllist.FindAll(p => p.name == "Home Contents").First().ProfileId = unitdetails.SectionData.ProfileUnId;
                        }
                        HomeContent.PolicyInclusions = Policyincllist;
                        Session["Policyinclustions"] = Policyincllist;
                    }
                    if (unitdetails != null && unitdetails.SectionData != null)
                    {
                        Session["unId"] = unitdetails.SectionData.UnId;
                        Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                    }
                }
            }
            if (unitdetails != null)
            {
                if (unitdetails.SectionData != null)
                {
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == HomeContent.LocationObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == HomeContent.LocationObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    HomeContent.LocationObj.Location = val;
                    //}
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == HomeContent.CosttoreplaceObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == HomeContent.CosttoreplaceObj.EiId).Select(p => p.Value).FirstOrDefault();
                        HomeContent.CosttoreplaceObj.Costtoreplaces = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == HomeContent.SuminsuredObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == HomeContent.SuminsuredObj.EiId).Select(p => p.Value).FirstOrDefault();
                        HomeContent.SuminsuredObj.Suminsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == HomeContent.DescriptionObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == HomeContent.DescriptionObj.EiId).Select(p => p.Value).FirstOrDefault();
                        HomeContent.DescriptionObj.Description = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == HomeContent.YearclaimObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == HomeContent.YearclaimObj.EiId).Select(p => p.Value).FirstOrDefault();
                        HomeContent.YearclaimObj.Yearclaim = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == HomeContent.ExcesspayObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == HomeContent.ExcesspayObj.EiId).Select(p => p.Value).FirstOrDefault();
                        HomeContent.ExcesspayObj.Excess = val;
                    }
                }
            }
            if (unitdetails != null && unitdetails.SectionData.AddressData != null)
            {
                if (unitdetails.SectionData.AddressData != null )
                {
                    HomeContent.AddressObj.Address = unitdetails.SectionData.AddressData.AddressLine1+", "+ unitdetails.SectionData.AddressData.Suburb + " ,"+unitdetails.SectionData.AddressData.State +", "+ unitdetails.SectionData.AddressData.Postcode;
                    HomeContent.Pincode = unitdetails.SectionData.AddressData.Postcode;
                    HomeContent.Sub = unitdetails.SectionData.AddressData.Suburb;
                    HomeContent.state = unitdetails.SectionData.AddressData.State;
                }
            }
            if (unitdetails.ReferralList != null)
            {
                HomeContent.ReferralList = unitdetails.ReferralList;
                HomeContent.ReferralList.Replace("&nbsp;&nbsp;&nbsp;&nbsp", "");
                HomeContent.Referels = new List<string>();
                string[] delim = { "<br/>" };

                string[] spltd = HomeContent.ReferralList.Split(delim, StringSplitOptions.None);
                if (spltd != null && spltd.Count() > 0)
                {
                    for (int i = 0; i < spltd.Count(); i++)
                    {
                        HomeContent.Referels.Add(spltd[i].Replace("&nbsp;&nbsp;&nbsp;&nbsp", " "));
                    }
                }

            }

            //var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.HomeContents),Convert.ToInt32(PolicyType.RLS),policyid).ToList();
            //if (details != null && details.Any())
            //{
            //    if (details.Exists(q => q.QuestionId == HomeContent.CosttoreplaceObj.EiId))
            //    {
            //        HomeContent.CosttoreplaceObj.Costtoreplaces = Convert.ToString(details.Where(q => q.QuestionId == HomeContent.CosttoreplaceObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == HomeContent.TotalcoverObj.EiId))
            //    {
            //        HomeContent.TotalcoverObj.Totalcover = Convert.ToString(details.Where(q => q.QuestionId == HomeContent.TotalcoverObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == HomeContent.YearclaimObj.EiId))
            //    {
            //        HomeContent.YearclaimObj.Yearclaim = Convert.ToString(details.Where(q => q.QuestionId == HomeContent.YearclaimObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == HomeContent.ExcesspayObj.EiId))
            //    {
            //        var loc = details.Where(q => q.QuestionId == HomeContent.ExcesspayObj.EiId).FirstOrDefault();
            //        HomeContent.ExcesspayObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
            //    }
            //    if (details.Exists(q => q.QuestionId == HomeContent.DescriptionObj.EiId))
            //    {
            //        HomeContent.DescriptionObj.Description = Convert.ToString(details.Where(q => q.QuestionId == HomeContent.DescriptionObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == HomeContent.SuminsuredObj.EiId))
            //    {
            //        HomeContent.SuminsuredObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == HomeContent.SuminsuredObj.EiId).FirstOrDefault().Answer);
            //    }
            //}
            ViewBag.cid = cid;
            return View(HomeContent);
        }
        [HttpPost]
        public ActionResult HomeContent(HomeContent HomeContent, int? cid)
        {
            NewPolicyDetailsClass HCmodel = new NewPolicyDetailsClass();
            List<SelectListItem> HCList = new List<SelectListItem>();

            HCList = HCmodel.excessRate();
            if (cid != null)
            {
                ViewBag.cid = cid;
                HomeContent.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = HomeContent.CustomerId;
            }
            HomeContent.ExcesspayObj.ExcessList = HCList;
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                //if (HomeContent.LocationObj != null && HomeContent.LocationObj.EiId > 0 && HomeContent.LocationObj.Location != null)
                //{
                //    db.IT_InsertCustomerQnsData(HomeContent.CustomerId, Convert.ToInt32(RLSSection.HomeContents), HomeContent.LocationObj.EiId, HomeContent.LocationObj.Location.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (HomeContent.CosttoreplaceObj != null && HomeContent.CosttoreplaceObj.EiId > 0 && HomeContent.CosttoreplaceObj.Costtoreplaces != null)
                //{
                //    db.IT_InsertCustomerQnsData(HomeContent.CustomerId, Convert.ToInt32(RLSSection.HomeContents), HomeContent.CosttoreplaceObj.EiId, HomeContent.CosttoreplaceObj.Costtoreplaces.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (HomeContent.DescriptionObj != null && HomeContent.DescriptionObj.EiId > 0 && HomeContent.DescriptionObj.Description != null)
                //{
                //    db.IT_InsertCustomerQnsData(HomeContent.CustomerId, Convert.ToInt32(RLSSection.HomeContents), HomeContent.DescriptionObj.EiId, HomeContent.DescriptionObj.Description.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (HomeContent.SuminsuredObj != null && HomeContent.SuminsuredObj.EiId > 0 && HomeContent.SuminsuredObj.Suminsured != null)
                //{
                //    db.IT_InsertCustomerQnsData(HomeContent.CustomerId, Convert.ToInt32(RLSSection.HomeContents), HomeContent.SuminsuredObj.EiId, HomeContent.SuminsuredObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (HomeContent.YearclaimObj != null && HomeContent.YearclaimObj.EiId > 0 && HomeContent.YearclaimObj.Yearclaim != null)
                //{
                //    db.IT_InsertCustomerQnsData(HomeContent.CustomerId, Convert.ToInt32(RLSSection.HomeContents), HomeContent.YearclaimObj.EiId, HomeContent.YearclaimObj.Yearclaim.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (HomeContent.ExcesspayObj != null && HomeContent.ExcesspayObj.EiId > 0 && HomeContent.ExcesspayObj.Excess != null)
                //{
                //    db.IT_InsertCustomerQnsData(HomeContent.CustomerId, Convert.ToInt32(RLSSection.HomeContents), HomeContent.ExcesspayObj.EiId, HomeContent.ExcesspayObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
            }
            Session["profileId"] = null;
            Session["UnId"] = null;
            return RedirectToAction("Valuables", new { cid = HomeContent.CustomerId });
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Valuables(int? cid, int? PcId)
        {
            string apikey = null;
            ValuablesHC ValuablesHC = new ValuablesHC();
            if (Session["ApiKey"] != null)
            {
                apikey = Session["ApiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
            if (Session["Policyinclustions"] != null)
            {

                ValuablesHC.PolicyInclusions = new List<SessionModel>();
                ValuablesHC.PolicyInclusions = Policyincllist;
                if (Policyincllist != null)
                {
                    //var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                    // var Suburb = new List<KeyValuePair<string, string>>();
                    // List<SelectListItem> listItems = new List<SelectListItem>();               

                    if (Policyincllist != null)
                    {

                        if (Policyincllist.Exists(p => p.name == "Valuables"))
                        {

                        }
                        else if (Policyincllist.Exists(p => p.name == "Farm Property"))
                        {
                            return RedirectToAction("FarmContents", "Farm", new { cid = cid });
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

                        if (Policyincllist.Exists(p => p.name == "Valuables"))
                        {
                            if (Session["unId"] == null && Session["profileId"] == null)
                            {
                                Session["unId"] = Policyincllist.Where(p => p.name == "Valuables").Select(p => p.UnitId).First();
                                Session["profileId"] = Policyincllist.Where(p => p.name == "Valuables").Select(p => p.ProfileId).First();
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
                }
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 1029 });
            }
            MasterDataEntities db = new MasterDataEntities();
            NewPolicyDetailsClass HCmodel = new NewPolicyDetailsClass();
            List<SelectListItem> HCList = new List<SelectListItem>();
            HCList = HCmodel.excessRate();
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
            ValuablesHC.PolicyInclusion = new List<usp_GetUnit_Result>();
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                ValuablesHC.PolicyInclusion = policyinclusions;
            }
            //var suburblist = db.IT_Master_GetSuburbList().ToList();
            //ValuablesHC.SubUrb = suburblist.Where(s => !string.IsNullOrEmpty(s)).Select(s => new SelectListItem() { Text = s, Value = s }).ToList();
            if (cid != null)
            {
                ViewBag.cid = cid;
                ValuablesHC.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = ValuablesHC.CustomerId;
            }
            ValuablesHC.AddressObj = new Addresses();
            ValuablesHC.LocationObj = new LocationNew();
            ValuablesHC.UnspecificObj = new Unspecifics();
            ValuablesHC.UnspecificObj.EiId = 60383;
            ValuablesHC.DescriptionObj = new Descriptions();
            ValuablesHC.DescriptionObj.EiId = 60391;
            ValuablesHC.SuminsuredObj = new SumInsures();
            ValuablesHC.SuminsuredObj.EiId = 60393;
            ValuablesHC.TotalcoverObj = new TotalCovers();
            ValuablesHC.TotalcoverObj.EiId = 0;
            ValuablesHC.ExcesspayObj = new ExcessesPay();
            ValuablesHC.ExcesspayObj.ExcessList = HCList;
            ValuablesHC.ExcesspayObj.EiId = 60399;
            string policyid = null;
            bool policyinclusion = policyinclusions.Exists(p => p.Name == "Valuables");
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (policyinclusion == true && PcId != null && PcId.HasValue)
            {
                ValuablesHC.ExistingPolicyInclustions = policyinclusions;
                int unid = Convert.ToInt32(Session["unId"]);
                int profileid = Convert.ToInt32(Session["profileId"]);
                //int sectionId = policyinclusions.Where(p => p.Name == "Home Contents" && p.UnitNumber == unid).Select(p => p.UnId).FirstOrDefault();
                //int? profileunid = policyinclusions.Where(p => p.Name == "Home Contents" && p.ProfileUnId == profileid).Select(p => p.ProfileUnId).FirstOrDefault();
                HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                if (EmpResponse != null)
                {
                    unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                }
            }
            else if (PcId == null && Session["unId"] != null && (Session["profileId"] != null))
            {
                int unid = Convert.ToInt32(Session["unId"]);
                int profileid = Convert.ToInt32(Session["profileId"]);
                HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                if (EmpResponse != null)
                {
                    unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                }
            }
            else
            {
                int HprofileId = -1;
                if (Session["HprofileId"] != null)
                {
                    HprofileId = Convert.ToInt32(Session["HprofileId"]);
                }

                HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Valuables&SectionUnId=&ProfileUnId=" + HprofileId);
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
                    if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Valuables"))
                    {
                        var policyhomelist = Policyincllist.FindAll(p => p.name == "Valuables").ToList();
                        if (policyhomelist != null && policyhomelist.Count() > 0)
                        {
                            if (Policyincllist.FindAll(p => p.name == "Valuables").Exists(p => p.UnitId == null))
                            {
                                Policyincllist.FindAll(p => p.name == "Valuables").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;
                            }
                            if (Policyincllist.FindAll(p => p.name == "Valuables").Exists(p => p.ProfileId == null))
                            {
                                Policyincllist.FindAll(p => p.name == "Valuables").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                            }
                        }
                        else
                        {
                            Policyincllist.FindAll(p => p.name == "Valuables").First().UnitId = unitdetails.SectionData.UnId;
                            Policyincllist.FindAll(p => p.name == "Valuables").First().ProfileId = unitdetails.SectionData.ProfileUnId;
                        }
                        ValuablesHC.PolicyInclusions = Policyincllist;
                        Session["Policyinclustions"] = Policyincllist;
                    }
                    if (unitdetails != null && unitdetails.SectionData != null)
                    {
                        Session["unId"] = unitdetails.SectionData.UnId;
                        Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                       
                    }
                }
            }
            if (unitdetails != null)
            {
                if (unitdetails != null && unitdetails.SectionData.AddressData != null)
                {
                    if (unitdetails.SectionData.AddressData != null)
                    {
                        ValuablesHC.AddressObj.Address = unitdetails.SectionData.AddressData.AddressLine1 + ", " + unitdetails.SectionData.AddressData.Suburb + " ," + unitdetails.SectionData.AddressData.State + ", " + unitdetails.SectionData.AddressData.Postcode;
                       
                    }
                }
                if (unitdetails.SectionData != null)
                {
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == ValuablesHC.LocationObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == ValuablesHC.LocationObj.EiId).Select(p => p.Value).FirstOrDefault();
                        ValuablesHC.LocationObj.Location = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == ValuablesHC.DescriptionObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == ValuablesHC.DescriptionObj.EiId).Select(p => p.Value).FirstOrDefault();
                        ValuablesHC.DescriptionObj.Description = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == ValuablesHC.ExcesspayObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == ValuablesHC.ExcesspayObj.EiId).Select(p => p.Value).FirstOrDefault();
                        ValuablesHC.ExcesspayObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == ValuablesHC.SuminsuredObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == ValuablesHC.SuminsuredObj.EiId).Select(p => p.Value).FirstOrDefault();
                        ValuablesHC.SuminsuredObj.Suminsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == ValuablesHC.TotalcoverObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == ValuablesHC.TotalcoverObj.EiId).Select(p => p.Value).FirstOrDefault();
                        ValuablesHC.TotalcoverObj.Totalcover = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == ValuablesHC.UnspecificObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == ValuablesHC.UnspecificObj.EiId).Select(p => p.Value).FirstOrDefault();
                        ValuablesHC.UnspecificObj.Unspecific = val;
                    }
                }
            }
            //var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Valuables), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
            //if (details != null && details.Any())
            //{
            //    if (details.Exists(q => q.QuestionId == ValuablesHC.UnspecificObj.EiId))
            //    {
            //        ValuablesHC.UnspecificObj.Unspecific = Convert.ToString(details.Where(q => q.QuestionId == ValuablesHC.UnspecificObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == ValuablesHC.TotalcoverObj.EiId))
            //    {
            //        ValuablesHC.TotalcoverObj.Totalcover = Convert.ToString(details.Where(q => q.QuestionId == ValuablesHC.TotalcoverObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == ValuablesHC.ExcesspayObj.EiId))
            //    {
            //        var loc = details.Where(q => q.QuestionId == ValuablesHC.ExcesspayObj.EiId).FirstOrDefault();
            //        ValuablesHC.ExcesspayObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
            //    }
            //    if (details.Exists(q => q.QuestionId == ValuablesHC.DescriptionObj.EiId))
            //    {
            //        ValuablesHC.DescriptionObj.Description = Convert.ToString(details.Where(q => q.QuestionId == ValuablesHC.DescriptionObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == ValuablesHC.SuminsuredObj.EiId))
            //    {
            //        ValuablesHC.SuminsuredObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == ValuablesHC.SuminsuredObj.EiId).FirstOrDefault().Answer);
            //    }
            //}
            if (unitdetails.ReferralList != null)
            {
                ValuablesHC.ReferralList = unitdetails.ReferralList;
                ValuablesHC.ReferralList.Replace("&nbsp;&nbsp;&nbsp;&nbsp", "");
                ValuablesHC.Referels = new List<string>();
                string[] delim = { "<br/>" };

                string[] spltd = ValuablesHC.ReferralList.Split(delim, StringSplitOptions.None);
                if (spltd != null && spltd.Count() > 0)
                {
                    for (int i = 0; i < spltd.Count(); i++)
                    {
                        ValuablesHC.Referels.Add(spltd[i].Replace("&nbsp;&nbsp;&nbsp;&nbsp", " "));
                    }
                }

            }
            return View(ValuablesHC);
        }
        [HttpPost]
        public ActionResult Valuables(ValuablesHC ValuablesHC, int? cid)
        {
            NewPolicyDetailsClass HCmodel = new NewPolicyDetailsClass();
            List<SelectListItem> HCList = new List<SelectListItem>();
            HCList = HCmodel.excessRate();
            if (cid != null)
            {
                ViewBag.cid = cid;
                ValuablesHC.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = ValuablesHC.CustomerId;
            }
            ValuablesHC.ExcesspayObj.ExcessList = HCList;
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                //if (ValuablesHC.LocationObj != null && ValuablesHC.LocationObj.EiId > 0 && ValuablesHC.LocationObj.Location != null)
                //{
                //    db.IT_InsertCustomerQnsData(ValuablesHC.CustomerId, Convert.ToInt32(RLSSection.Valuables), ValuablesHC.LocationObj.EiId, ValuablesHC.LocationObj.Location.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (ValuablesHC.UnspecificObj != null && ValuablesHC.UnspecificObj.EiId > 0 && ValuablesHC.UnspecificObj.Unspecific != null)
                //{
                //    db.IT_InsertCustomerQnsData(ValuablesHC.CustomerId, Convert.ToInt32(RLSSection.Valuables), ValuablesHC.UnspecificObj.EiId, ValuablesHC.UnspecificObj.Unspecific.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (ValuablesHC.DescriptionObj != null && ValuablesHC.DescriptionObj.EiId > 0 && ValuablesHC.DescriptionObj.Description != null)
                //{
                //    db.IT_InsertCustomerQnsData(ValuablesHC.CustomerId, Convert.ToInt32(RLSSection.Valuables), ValuablesHC.DescriptionObj.EiId, ValuablesHC.DescriptionObj.Description.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (ValuablesHC.SuminsuredObj != null && ValuablesHC.SuminsuredObj.EiId > 0 && ValuablesHC.SuminsuredObj.Suminsured != null)
                //{
                //    db.IT_InsertCustomerQnsData(ValuablesHC.CustomerId, Convert.ToInt32(RLSSection.Valuables), ValuablesHC.SuminsuredObj.EiId, ValuablesHC.SuminsuredObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (ValuablesHC.ExcesspayObj != null && ValuablesHC.ExcesspayObj.EiId > 0 && ValuablesHC.ExcesspayObj.Excess != null)
                //{
                //    db.IT_InsertCustomerQnsData(ValuablesHC.CustomerId, Convert.ToInt32(RLSSection.Valuables), ValuablesHC.ExcesspayObj.EiId, ValuablesHC.ExcesspayObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
            }
            Session["profileId"] = null;
            Session["UnId"] = null;
            return RedirectToAction("FarmContents", "Farm", new { cid = ValuablesHC.CustomerId });
        }
    }
}