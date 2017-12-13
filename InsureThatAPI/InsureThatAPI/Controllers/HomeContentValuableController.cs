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
    public class HomeContentValuableController : Controller
    {
        // GET: HomeContentValuable
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult HomeContent(int? cid)
        {
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Contains("HomeContents"))
                    {

                    }
                    else
                    {
                        if (Policyincllist.Contains("Valuables"))
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
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid,type=1 });
            }
            MasterDataEntities db = new MasterDataEntities();
            NewPolicyDetailsClass HCmodel = new NewPolicyDetailsClass();
            List<SelectListItem> HCList = new List<SelectListItem>();
            HCList = HCmodel.excessRate();
            var suburblist = db.IT_Master_GetSuburbList().ToList();
            // var Suburb = new List<KeyValuePair<string, string>>();
            // List<SelectListItem> listItems = new List<SelectListItem>();
            HomeContent HomeContent = new HomeContent();
            HomeContent.SubUrb = suburblist.Where(s => !string.IsNullOrEmpty(s)).Select(s => new SelectListItem() { Text = s, Value = s }).ToList();

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
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.HomeContents),Convert.ToInt32(PolicyType.RLS),policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == HomeContent.CosttoreplaceObj.EiId))
                {
                    HomeContent.CosttoreplaceObj.Costtoreplaces = Convert.ToString(details.Where(q => q.QuestionId == HomeContent.CosttoreplaceObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == HomeContent.TotalcoverObj.EiId))
                {
                    HomeContent.TotalcoverObj.Totalcover = Convert.ToString(details.Where(q => q.QuestionId == HomeContent.TotalcoverObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == HomeContent.YearclaimObj.EiId))
                {
                    HomeContent.YearclaimObj.Yearclaim = Convert.ToString(details.Where(q => q.QuestionId == HomeContent.YearclaimObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == HomeContent.ExcesspayObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == HomeContent.ExcesspayObj.EiId).FirstOrDefault();
                    HomeContent.ExcesspayObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == HomeContent.DescriptionObj.EiId))
                {
                    HomeContent.DescriptionObj.Description = Convert.ToString(details.Where(q => q.QuestionId == HomeContent.DescriptionObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == HomeContent.SuminsuredObj.EiId))
                {
                    HomeContent.SuminsuredObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == HomeContent.SuminsuredObj.EiId).FirstOrDefault().Answer);
                }
            }
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
                if (HomeContent.LocationObj != null && HomeContent.LocationObj.EiId > 0 && HomeContent.LocationObj.Location != null)
                {
                    db.IT_InsertCustomerQnsData(HomeContent.CustomerId, Convert.ToInt32(RLSSection.HomeContents), HomeContent.LocationObj.EiId, HomeContent.LocationObj.Location.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (HomeContent.CosttoreplaceObj != null && HomeContent.CosttoreplaceObj.EiId > 0 && HomeContent.CosttoreplaceObj.Costtoreplaces != null)
                {
                    db.IT_InsertCustomerQnsData(HomeContent.CustomerId, Convert.ToInt32(RLSSection.HomeContents), HomeContent.CosttoreplaceObj.EiId, HomeContent.CosttoreplaceObj.Costtoreplaces.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (HomeContent.DescriptionObj != null && HomeContent.DescriptionObj.EiId > 0 && HomeContent.DescriptionObj.Description != null)
                {
                    db.IT_InsertCustomerQnsData(HomeContent.CustomerId, Convert.ToInt32(RLSSection.HomeContents), HomeContent.DescriptionObj.EiId, HomeContent.DescriptionObj.Description.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (HomeContent.SuminsuredObj != null && HomeContent.SuminsuredObj.EiId > 0 && HomeContent.SuminsuredObj.Suminsured != null)
                {
                    db.IT_InsertCustomerQnsData(HomeContent.CustomerId, Convert.ToInt32(RLSSection.HomeContents), HomeContent.SuminsuredObj.EiId, HomeContent.SuminsuredObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (HomeContent.YearclaimObj != null && HomeContent.YearclaimObj.EiId > 0 && HomeContent.YearclaimObj.Yearclaim != null)
                {
                    db.IT_InsertCustomerQnsData(HomeContent.CustomerId, Convert.ToInt32(RLSSection.HomeContents), HomeContent.YearclaimObj.EiId, HomeContent.YearclaimObj.Yearclaim.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (HomeContent.ExcesspayObj != null && HomeContent.ExcesspayObj.EiId > 0 && HomeContent.ExcesspayObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(HomeContent.CustomerId, Convert.ToInt32(RLSSection.HomeContents), HomeContent.ExcesspayObj.EiId, HomeContent.ExcesspayObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
            }
            return RedirectToAction("Valuables", new { cid = HomeContent.CustomerId });
        }
        [HttpGet]
        public ActionResult Valuables(int? cid)
        {
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
               
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {

                    if (Policyincllist.Contains("Valuables"))
                    {
                       
                    }
                    else
                    {
                        if (Policyincllist.Contains("FarmProperty"))
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
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid,type=1 });
            }
            MasterDataEntities db = new MasterDataEntities();
            NewPolicyDetailsClass HCmodel = new NewPolicyDetailsClass();
            List<SelectListItem> HCList = new List<SelectListItem>();
            HCList = HCmodel.excessRate();
            ValuablesHC ValuablesHC = new ValuablesHC();
            var suburblist = db.IT_Master_GetSuburbList().ToList();
            ValuablesHC.SubUrb = suburblist.Where(s => !string.IsNullOrEmpty(s)).Select(s => new SelectListItem() { Text = s, Value = s }).ToList();
            if (cid != null)
            {
                ViewBag.cid = cid;
                ValuablesHC.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = ValuablesHC.CustomerId;
            }
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
            var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(RLSSection.Valuables) , Convert.ToInt32(PolicyType.RLS), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == ValuablesHC.UnspecificObj.EiId))
                {
                    ValuablesHC.UnspecificObj.Unspecific = Convert.ToString(details.Where(q => q.QuestionId == ValuablesHC.UnspecificObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == ValuablesHC.TotalcoverObj.EiId))
                {
                    ValuablesHC.TotalcoverObj.Totalcover = Convert.ToString(details.Where(q => q.QuestionId == ValuablesHC.TotalcoverObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == ValuablesHC.ExcesspayObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == ValuablesHC.ExcesspayObj.EiId).FirstOrDefault();
                    ValuablesHC.ExcesspayObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == ValuablesHC.DescriptionObj.EiId))
                {
                    ValuablesHC.DescriptionObj.Description = Convert.ToString(details.Where(q => q.QuestionId == ValuablesHC.DescriptionObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == ValuablesHC.SuminsuredObj.EiId))
                {
                    ValuablesHC.SuminsuredObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == ValuablesHC.SuminsuredObj.EiId).FirstOrDefault().Answer);
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
                if (ValuablesHC.LocationObj != null && ValuablesHC.LocationObj.EiId > 0 && ValuablesHC.LocationObj.Location != null)
                {
                    db.IT_InsertCustomerQnsData(ValuablesHC.CustomerId, Convert.ToInt32(RLSSection.Valuables), ValuablesHC.LocationObj.EiId, ValuablesHC.LocationObj.Location.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (ValuablesHC.UnspecificObj != null && ValuablesHC.UnspecificObj.EiId > 0 && ValuablesHC.UnspecificObj.Unspecific != null)
                {
                    db.IT_InsertCustomerQnsData(ValuablesHC.CustomerId, Convert.ToInt32(RLSSection.Valuables), ValuablesHC.UnspecificObj.EiId, ValuablesHC.UnspecificObj.Unspecific.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (ValuablesHC.DescriptionObj != null && ValuablesHC.DescriptionObj.EiId > 0 && ValuablesHC.DescriptionObj.Description != null)
                {
                    db.IT_InsertCustomerQnsData(ValuablesHC.CustomerId, Convert.ToInt32(RLSSection.Valuables), ValuablesHC.DescriptionObj.EiId, ValuablesHC.DescriptionObj.Description.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (ValuablesHC.SuminsuredObj != null && ValuablesHC.SuminsuredObj.EiId > 0 && ValuablesHC.SuminsuredObj.Suminsured != null)
                {
                    db.IT_InsertCustomerQnsData(ValuablesHC.CustomerId, Convert.ToInt32(RLSSection.Valuables), ValuablesHC.SuminsuredObj.EiId, ValuablesHC.SuminsuredObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (ValuablesHC.ExcesspayObj != null && ValuablesHC.ExcesspayObj.EiId > 0 && ValuablesHC.ExcesspayObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(ValuablesHC.CustomerId, Convert.ToInt32(RLSSection.Valuables), ValuablesHC.ExcesspayObj.EiId, ValuablesHC.ExcesspayObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
            }
            return RedirectToAction("FarmContents", "Farm", new { cid = ValuablesHC.CustomerId });
        }
    }
}