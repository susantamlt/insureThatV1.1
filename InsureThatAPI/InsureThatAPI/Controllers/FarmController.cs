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
    public class FarmController : Controller
    {
        // GET: Farm
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FarmContents(int? cid)
        {
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {                
                        
                        if (Policyincllist.Contains("FarmProperty"))
                        {
                        }
                        else {if (Policyincllist.Contains("Liability"))
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
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> DescriptionListFContent = new List<SelectListItem>();
            DescriptionListFContent = commonModel.descriptionListFC();
            List<SelectListItem> constructionTypeFC = new List<SelectListItem>();
            constructionTypeFC = commonModel.constructionType();
            List<SelectListItem> excessToPay = new List<SelectListItem>();
            excessToPay = commonModel.excessRate();
            FarmContents FarmContents = new FarmContents();
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
            if (Session["CompletionTrackFPC"] != null)
            {
                Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                FarmContents.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }
            else
            {
                Session["CompletionTrackFPC"] = "0-0-0-0"; ;
                FarmContents.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.FarmProperty), Convert.ToInt32(PolicyType.RLS),policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == FarmContents.DescriptionFCObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FarmContents.DescriptionFCObj.EiId).FirstOrDefault();
                    FarmContents.DescriptionFCObj.Description = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == FarmContents.YearObj.EiId))
                {
                    FarmContents.YearObj.Year = Convert.ToString(details.Where(q => q.QuestionId == FarmContents.YearObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmContents.MaterialsObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FarmContents.MaterialsObj.EiId).FirstOrDefault();
                    FarmContents.MaterialsObj.Materials = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == FarmContents.CoolroomFcObj.EiId))
                {
                    FarmContents.CoolroomFcObj.Coolroom = Convert.ToString(details.Where(q => q.QuestionId == FarmContents.CoolroomFcObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmContents.SuminsuredObj.EiId))
                {
                    FarmContents.SuminsuredObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == FarmContents.SuminsuredObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmContents.confirmfsObj.EiId))
                {
                    FarmContents.confirmfsObj.Confirm = Convert.ToString(details.Where(q => q.QuestionId == FarmContents.confirmfsObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmContents.FarmfencingObj.EiId))
                {
                    FarmContents.FarmfencingObj.Farmfencing = Convert.ToString(details.Where(q => q.QuestionId == FarmContents.FarmfencingObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmContents.FarmcencingTcObj.EiId))
                {
                    FarmContents.FarmcencingTcObj.Totalcover = Convert.ToString(details.Where(q => q.QuestionId == FarmContents.FarmcencingTcObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmContents.FarmstructuresObj.EiId))
                {
                    FarmContents.FarmstructuresObj.Farmstructures = Convert.ToString(details.Where(q => q.QuestionId == FarmContents.FarmstructuresObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmContents.FarmContentFcObj.EiId))
                {
                    FarmContents.FarmContentFcObj.Farmcontents = Convert.ToString(details.Where(q => q.QuestionId == FarmContents.FarmContentFcObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmContents.ExcessesFpcObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FarmContents.ExcessesFpcObj.EiId).FirstOrDefault();
                    FarmContents.ExcessesFpcObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                FarmContents.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FarmContents.CustomerId;
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
                if (FarmContents.DescriptionFCObj!=null && FarmContents.DescriptionFCObj.EiId > 0 && FarmContents.DescriptionFCObj.Description != null)
                {
                    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.DescriptionFCObj.EiId, FarmContents.DescriptionFCObj.Description.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmContents.YearObj!=null && FarmContents.YearObj.EiId > 0 && FarmContents.YearObj.Year != null)
                {
                    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.YearObj.EiId, FarmContents.YearObj.Year.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmContents.MaterialsObj!=null && FarmContents.MaterialsObj.EiId > 0 && FarmContents.MaterialsObj.Materials != null)
                {
                    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.MaterialsObj.EiId, FarmContents.MaterialsObj.Materials.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmContents.CoolroomFcObj!=null && FarmContents.CoolroomFcObj.EiId > 0 && FarmContents.CoolroomFcObj.Coolroom != null)
                {
                    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.CoolroomFcObj.EiId, FarmContents.CoolroomFcObj.Coolroom.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmContents.SuminsuredObj!=null && FarmContents.SuminsuredObj.EiId > 0 && FarmContents.SuminsuredObj.Suminsured != null)
                {
                    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.SuminsuredObj.EiId, FarmContents.SuminsuredObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmContents.confirmfsObj!=null && FarmContents.confirmfsObj.EiId > 0 && FarmContents.confirmfsObj.Confirm != null)
                {
                    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.confirmfsObj.EiId, FarmContents.confirmfsObj.Confirm.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmContents.FarmfencingObj!=null && FarmContents.FarmfencingObj.EiId > 0 && FarmContents.FarmfencingObj.Farmfencing != null)
                {
                    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.FarmfencingObj.EiId, FarmContents.FarmfencingObj.Farmfencing.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmContents.FarmcencingTcObj!=null && FarmContents.FarmcencingTcObj.EiId > 0 && FarmContents.FarmcencingTcObj.Totalcover != null)
                {
                    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.FarmcencingTcObj.EiId, FarmContents.FarmcencingTcObj.Totalcover.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmContents.FarmstructuresObj!=null && FarmContents.FarmstructuresObj.EiId > 0 && FarmContents.FarmstructuresObj.Farmstructures != null)
                {
                    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.FarmstructuresObj.EiId, FarmContents.FarmstructuresObj.Farmstructures.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmContents.FarmContentFcObj!=null && FarmContents.FarmContentFcObj.EiId > 0 && FarmContents.FarmContentFcObj.Farmcontents != null)
                {
                    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.FarmContentFcObj.EiId, FarmContents.FarmContentFcObj.Farmcontents.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmContents.ExcessesFpcObj!=null && FarmContents.ExcessesFpcObj.EiId > 0 && FarmContents.ExcessesFpcObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FarmContents.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmContents.ExcessesFpcObj.EiId, FarmContents.ExcessesFpcObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Session["CompletionTrackFPC"] != null)
                {
                    Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                    FarmContents.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                    if (FarmContents.CompletionTrackFPC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = FarmContents.CompletionTrackFPC.ToCharArray();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 0)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["CompletionTrackFPC"] = Completionstring;
                        FarmContents.CompletionTrackFPC = Completionstring;
                    }
                }
                else
                {
                    Session["CompletionTrackFPC"] = "1-0-0-0"; ;
                    FarmContents.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                }
                return RedirectToAction("FarmMachinery", new { cid = FarmContents.CustomerId });
            }
          
            return View(FarmContents);
        }
        [HttpGet]
        public ActionResult FarmMachinery(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
          
            List<SelectListItem> excessforUMPay = new List<SelectListItem>();
            excessforUMPay = commonModel.excessRate();
            FarmMachinery FarmMachinery = new FarmMachinery();
            if (cid != null)
            {
                ViewBag.cid = cid;
                FarmMachinery.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FarmMachinery.CustomerId;
            }
            FarmMachinery.FarmContentFcObj = new FarmContentsFC();
            FarmMachinery.FarmContentFcObj.EiId = 60523;
            FarmMachinery.ExcessUMObj = new ExcessforUM();
            FarmMachinery.ExcessUMObj.ExcessumList = excessforUMPay;
            FarmMachinery.ExcessUMObj.EiId = 60525;
            FarmMachinery.DescriptionFmObj = new DescriptionsFM();
            FarmMachinery.DescriptionFmObj.EiId = 60529;
            FarmMachinery.YearObj = new YearFPC();
            FarmMachinery.YearObj.EiId = 60531;
            FarmMachinery.SerialnumberObj = new SerialNumbers();
            FarmMachinery.SerialnumberObj.EiId = 60533;
            FarmMachinery.ExcessesFpcObj = new ExcessesFPC();
            FarmMachinery.ExcessesFpcObj.ExcessList = excessforUMPay;
            FarmMachinery.ExcessesFpcObj.EiId = 60535;
            FarmMachinery.SuminsuredObj = new SumOfInsured();
            FarmMachinery.SuminsuredObj.EiId = 60537;
            if (Session["CompletionTrackFPC"] != null)
            {
                Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                FarmMachinery.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }
            else
            {
                Session["CompletionTrackFPC"] = "0-0-0-0"; ;
                FarmMachinery.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }
            string policyid = null;
            var db = new MasterDataEntities();
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.FarmProperty), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == FarmMachinery.FarmContentFcObj.EiId))
                {
                    FarmMachinery.FarmContentFcObj.Farmcontents = Convert.ToString(details.Where(q => q.QuestionId == FarmMachinery.FarmContentFcObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmMachinery.ExcessUMObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FarmMachinery.ExcessUMObj.EiId).FirstOrDefault();
                    FarmMachinery.ExcessUMObj.Excessum = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == FarmMachinery.DescriptionFmObj.EiId))
                {
                    FarmMachinery.DescriptionFmObj.Description = Convert.ToString(details.Where(q => q.QuestionId == FarmMachinery.YearObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmMachinery.YearObj.EiId))
                {
                    FarmMachinery.YearObj.Year = Convert.ToString(details.Where(q => q.QuestionId == FarmMachinery.YearObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmMachinery.SerialnumberObj.EiId))
                {
                    FarmMachinery.SerialnumberObj.Serialnumber = Convert.ToString(details.Where(q => q.QuestionId == FarmMachinery.SerialnumberObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmMachinery.ExcessesFpcObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FarmMachinery.ExcessesFpcObj.EiId).FirstOrDefault();
                    FarmMachinery.ExcessesFpcObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == FarmMachinery.SuminsuredObj.EiId))
                {
                    FarmMachinery.SuminsuredObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == FarmMachinery.SuminsuredObj.EiId).FirstOrDefault().Answer);
                }
            }
            return View(FarmMachinery);
        }
        [HttpPost]
        public ActionResult FarmMachinery(FarmMachinery FarmMachinery, int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> excessforUMPay = new List<SelectListItem>();
            excessforUMPay = commonModel.excessRate();
            if (cid != null)
            {
                ViewBag.cid = cid;
                FarmMachinery.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FarmMachinery.CustomerId;
            }
            FarmMachinery.ExcessUMObj.ExcessumList = excessforUMPay;
            FarmMachinery.ExcessesFpcObj.ExcessList = excessforUMPay;
            string policyid = null;
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (FarmMachinery.FarmContentFcObj!=null && FarmMachinery.FarmContentFcObj.EiId > 0 && FarmMachinery.FarmContentFcObj.Farmcontents != null)
                {
                    db.IT_InsertCustomerQnsData(FarmMachinery.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmMachinery.FarmContentFcObj.EiId, FarmMachinery.FarmContentFcObj.Farmcontents.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmMachinery.ExcessUMObj!=null && FarmMachinery.ExcessUMObj.EiId > 0 && FarmMachinery.ExcessUMObj.Excessum != null)
                {
                    db.IT_InsertCustomerQnsData(FarmMachinery.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmMachinery.ExcessUMObj.EiId, FarmMachinery.ExcessUMObj.Excessum.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmMachinery.DescriptionFmObj!=null && FarmMachinery.DescriptionFmObj.EiId > 0 && FarmMachinery.DescriptionFmObj.Description != null)
                {
                    db.IT_InsertCustomerQnsData(FarmMachinery.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmMachinery.DescriptionFmObj.EiId, FarmMachinery.DescriptionFmObj.Description.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmMachinery.YearObj!=null && FarmMachinery.YearObj.EiId > 0 && FarmMachinery.YearObj.Year != null)
                {
                    db.IT_InsertCustomerQnsData(FarmMachinery.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmMachinery.YearObj.EiId, FarmMachinery.YearObj.Year.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmMachinery.SerialnumberObj!=null && FarmMachinery.SerialnumberObj.EiId > 0 && FarmMachinery.SerialnumberObj.Serialnumber != null)
                {
                    db.IT_InsertCustomerQnsData(FarmMachinery.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmMachinery.SerialnumberObj.EiId, FarmMachinery.SerialnumberObj.Serialnumber.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmMachinery.ExcessesFpcObj!=null && FarmMachinery.ExcessesFpcObj.EiId > 0 && FarmMachinery.ExcessesFpcObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FarmMachinery.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmMachinery.ExcessesFpcObj.EiId, FarmMachinery.ExcessesFpcObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (FarmMachinery.SuminsuredObj!=null && FarmMachinery.SuminsuredObj.EiId > 0 && FarmMachinery.SuminsuredObj.Suminsured != null)
                {
                    db.IT_InsertCustomerQnsData(FarmMachinery.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), FarmMachinery.SuminsuredObj.EiId, FarmMachinery.SuminsuredObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Session["CompletionTrackFPC"] != null)
                {
                    Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                    FarmMachinery.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                    if (FarmMachinery.CompletionTrackFPC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = FarmMachinery.CompletionTrackFPC.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 2)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["CompletionTrackFPC"] = Completionstring;
                        FarmMachinery.CompletionTrackFPC = Completionstring;
                    }
                }
                else
                {
                    Session["CompletionTrackFPC"] = "0-1-0-0"; ;
                    FarmMachinery.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                }
                return RedirectToAction("Livestock", new { cid = FarmMachinery.CustomerId });
            }
            return View(FarmMachinery);
        }
        [HttpGet]
        public ActionResult Livestock(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> desList = new List<SelectListItem>();
            desList = commonModel.descriptionLS();
           
            List<SelectListItem> excessforUMPay = new List<SelectListItem>();
            excessforUMPay = commonModel.excessRate();
            Livestock Livestock = new Livestock();
            if (cid != null)
            {
                ViewBag.cid = cid;
                Livestock.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = Livestock.CustomerId;
            }
            Livestock.DescriptionFCObj = new DescriptionsFC();
            Livestock.DescriptionFCObj.DescriptionList = desList;
            Livestock.DescriptionFCObj.EiId = 60565;
            Livestock.NumberanimalObj = new NumberOfAnimals();
            Livestock.NumberanimalObj.EiId = 60567;
            Livestock.SuminsuredperObj = new SumInsuredPerAnimals();
            Livestock.SuminsuredperObj.EiId = 60569;
            Livestock.SuminsuredObj = new SumOfInsured();
            Livestock.SuminsuredObj.EiId = 60571;
            Livestock.DogattackObj = new DogAttackOption();
            Livestock.DogattackObj.EiId = 60575;
            Livestock.ExcessesFpcObj = new ExcessesFPC();
            Livestock.ExcessesFpcObj.ExcessList = excessforUMPay;
            Livestock.ExcessesFpcObj.EiId = 60577;
            if (Session["CompletionTrackFPC"] != null)
            {
                Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                Livestock.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }
            else
            {
                Session["CompletionTrackFPC"] = "0-0-0-0"; ;
                Livestock.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(RLSSection.FarmProperty), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == Livestock.DescriptionFCObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == Livestock.DescriptionFCObj.EiId).FirstOrDefault();
                    Livestock.DescriptionFCObj.Description = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == Livestock.NumberanimalObj.EiId))
                {
                    Livestock.NumberanimalObj.Numberanimal = Convert.ToString(details.Where(q => q.QuestionId == Livestock.NumberanimalObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Livestock.SuminsuredperObj.EiId))
                {
                    Livestock.SuminsuredperObj.Suminsuredper = Convert.ToString(details.Where(q => q.QuestionId == Livestock.SuminsuredperObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Livestock.SuminsuredObj.EiId))
                {
                    Livestock.SuminsuredObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == Livestock.SuminsuredObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Livestock.DogattackObj.EiId))
                {
                    Livestock.DogattackObj.Dogattack = Convert.ToString(details.Where(q => q.QuestionId == Livestock.DogattackObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Livestock.ExcessesFpcObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == Livestock.ExcessesFpcObj.EiId).FirstOrDefault();
                    Livestock.ExcessesFpcObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
            }
            return View(Livestock);
        }
        [HttpPost]
        public ActionResult Livestock(Livestock Livestock, int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> desList = new List<SelectListItem>();
            desList = commonModel.descriptionLS();
            List<SelectListItem> excessforUMPay = new List<SelectListItem>();
            excessforUMPay = commonModel.excessRate();
            Livestock.DescriptionFCObj.DescriptionList = desList;
            Livestock.ExcessesFpcObj.ExcessList = excessforUMPay;
            if (cid != null)
            {
                ViewBag.cid = cid;
                Livestock.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = Livestock.CustomerId;
            }
            string policyid = null;
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (Livestock.DescriptionFCObj!=null && Livestock.DescriptionFCObj.EiId > 0 && Livestock.DescriptionFCObj.Description != null)
                {
                    db.IT_InsertCustomerQnsData(Livestock.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), Livestock.DescriptionFCObj.EiId, Livestock.DescriptionFCObj.Description.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Livestock.NumberanimalObj!=null && Livestock.NumberanimalObj.EiId > 0 && Livestock.NumberanimalObj.Numberanimal != null)
                {
                    db.IT_InsertCustomerQnsData(Livestock.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), Livestock.NumberanimalObj.EiId, Livestock.NumberanimalObj.Numberanimal.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Livestock.SuminsuredperObj!=null && Livestock.SuminsuredperObj.EiId > 0 && Livestock.SuminsuredperObj.Suminsuredper != null)
                {
                    db.IT_InsertCustomerQnsData(Livestock.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), Livestock.SuminsuredperObj.EiId, Livestock.SuminsuredperObj.Suminsuredper.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Livestock.SuminsuredObj!=null && Livestock.SuminsuredObj.EiId > 0 && Livestock.SuminsuredObj.Suminsured != null)
                {
                    db.IT_InsertCustomerQnsData(Livestock.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), Livestock.SuminsuredObj.EiId, Livestock.SuminsuredObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Livestock.DogattackObj!=null && Livestock.DogattackObj.EiId > 0 && Livestock.DogattackObj.Dogattack != null)
                {
                    db.IT_InsertCustomerQnsData(Livestock.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), Livestock.DogattackObj.EiId, Livestock.DogattackObj.Dogattack.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Livestock.ExcessesFpcObj != null && Livestock.ExcessesFpcObj.EiId > 0 && Livestock.ExcessesFpcObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(Livestock.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), Livestock.ExcessesFpcObj.EiId, Livestock.ExcessesFpcObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Session["CompletionTrackFPC"] != null)
                {
                    Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                    Livestock.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                    if (Livestock.CompletionTrackFPC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = Livestock.CompletionTrackFPC.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 4)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["CompletionTrackFPC"] = Completionstring;
                        Livestock.CompletionTrackFPC = Completionstring;
                    }
                }
                else
                {
                    Session["CompletionTrackFPC"] = "0-0-1-0"; ;
                    Livestock.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                }
                return RedirectToAction("HarvestedCropsBeehives", new { cid = Livestock.CustomerId });
            }
            return View(Livestock);
        }
        [HttpGet]
        public ActionResult HarvestedCropsBeehives(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> excessforUMPay = new List<SelectListItem>();
            excessforUMPay = commonModel.excessRate();
            HarvestedCropsBeehives HarvestedCropsBeehives = new HarvestedCropsBeehives();
            HarvestedCropsBeehives.SuminsuredObj = new SumOfInsured();
            HarvestedCropsBeehives.SuminsuredObj.EiId = 60593;
            HarvestedCropsBeehives.ExcessesFpcObj = new ExcessesFPC();
            HarvestedCropsBeehives.ExcessesFpcObj.ExcessList = excessforUMPay;
            HarvestedCropsBeehives.ExcessesFpcObj.EiId = 60595;
            HarvestedCropsBeehives.SuminsuredHbcObj = new SumOfInsuredHCB();
            HarvestedCropsBeehives.SuminsuredHbcObj.EiId = 60601;
            HarvestedCropsBeehives.NumberhiveObj = new NumberOfHives();
            HarvestedCropsBeehives.NumberhiveObj.EiId = 60603;
            HarvestedCropsBeehives.ExcessBObj = new ExcessesBeehives();
            HarvestedCropsBeehives.ExcessBObj.ExcessBList = excessforUMPay;
            HarvestedCropsBeehives.ExcessBObj.EiId = 60595;
            if (cid != null)
            {
                ViewBag.cid = cid;
                HarvestedCropsBeehives.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = HarvestedCropsBeehives.CustomerId;
            }
            if (Session["CompletionTrackFPC"] != null)
            {
                Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                HarvestedCropsBeehives.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }
            else
            {
                Session["CompletionTrackFPC"] = "0-0-0-0"; ;
                HarvestedCropsBeehives.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(RLSSection.FarmProperty),Convert.ToInt32(PolicyType.RLS) ,policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == HarvestedCropsBeehives.SuminsuredObj.EiId))
                {
                    HarvestedCropsBeehives.SuminsuredObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == HarvestedCropsBeehives.SuminsuredObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == HarvestedCropsBeehives.ExcessesFpcObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == HarvestedCropsBeehives.ExcessesFpcObj.EiId).FirstOrDefault();
                    HarvestedCropsBeehives.ExcessesFpcObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == HarvestedCropsBeehives.SuminsuredHbcObj.EiId))
                {
                    HarvestedCropsBeehives.SuminsuredHbcObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == HarvestedCropsBeehives.SuminsuredHbcObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == HarvestedCropsBeehives.NumberhiveObj.EiId))
                {
                    HarvestedCropsBeehives.NumberhiveObj.Numberhive = Convert.ToString(details.Where(q => q.QuestionId == HarvestedCropsBeehives.NumberhiveObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == HarvestedCropsBeehives.ExcessBObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == HarvestedCropsBeehives.ExcessBObj.EiId).FirstOrDefault();
                    HarvestedCropsBeehives.ExcessBObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
            }
            return View(HarvestedCropsBeehives);
        }
        [HttpPost]
        public ActionResult HarvestedCropsBeehives(HarvestedCropsBeehives HarvestedCropsBeehives, int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> excessforUMPay = new List<SelectListItem>();
            excessforUMPay = commonModel.excessRate();
            HarvestedCropsBeehives.ExcessesFpcObj.ExcessList = excessforUMPay;
            HarvestedCropsBeehives.ExcessBObj.ExcessBList = excessforUMPay;
            if (cid != null)
            {
                ViewBag.cid = cid;
                HarvestedCropsBeehives.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = HarvestedCropsBeehives.CustomerId;
            }
            string policyid = null;
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (HarvestedCropsBeehives.SuminsuredObj!=null && HarvestedCropsBeehives.SuminsuredObj.EiId > 0 && HarvestedCropsBeehives.SuminsuredObj.Suminsured != null)
                {
                    db.IT_InsertCustomerQnsData(HarvestedCropsBeehives.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), HarvestedCropsBeehives.SuminsuredObj.EiId, HarvestedCropsBeehives.SuminsuredObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS),policyid);
                }
                if (HarvestedCropsBeehives.ExcessesFpcObj!=null && HarvestedCropsBeehives.ExcessesFpcObj.EiId > 0 && HarvestedCropsBeehives.ExcessesFpcObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(HarvestedCropsBeehives.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), HarvestedCropsBeehives.ExcessesFpcObj.EiId, HarvestedCropsBeehives.ExcessesFpcObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (HarvestedCropsBeehives.SuminsuredHbcObj != null && HarvestedCropsBeehives.SuminsuredHbcObj.EiId > 0 && HarvestedCropsBeehives.SuminsuredHbcObj.Suminsured != null)
                {
                    db.IT_InsertCustomerQnsData(HarvestedCropsBeehives.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), HarvestedCropsBeehives.SuminsuredHbcObj.EiId, HarvestedCropsBeehives.SuminsuredHbcObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (HarvestedCropsBeehives.NumberhiveObj != null && HarvestedCropsBeehives.NumberhiveObj.EiId > 0 && HarvestedCropsBeehives.NumberhiveObj.Numberhive != null)
                {
                    db.IT_InsertCustomerQnsData(HarvestedCropsBeehives.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), HarvestedCropsBeehives.NumberhiveObj.EiId, HarvestedCropsBeehives.NumberhiveObj.Numberhive.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (HarvestedCropsBeehives.ExcessBObj!=null && HarvestedCropsBeehives.ExcessBObj.EiId > 0 && HarvestedCropsBeehives.ExcessBObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(HarvestedCropsBeehives.CustomerId, Convert.ToInt32(RLSSection.FarmProperty), HarvestedCropsBeehives.ExcessBObj.EiId, HarvestedCropsBeehives.ExcessBObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Session["CompletionTrackFPC"] != null)
                {
                    Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                    HarvestedCropsBeehives.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                    if (HarvestedCropsBeehives.CompletionTrackFPC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = HarvestedCropsBeehives.CompletionTrackFPC.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 6)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["CompletionTrackFPC"] = Completionstring;
                        HarvestedCropsBeehives.CompletionTrackFPC = Completionstring;
                    }
                }
                else
                {
                    Session["CompletionTrackFPC"] = "0-0-0-1"; ;
                    HarvestedCropsBeehives.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                }
                return RedirectToAction("PetsCover","Pets", new { cid = HarvestedCropsBeehives.CustomerId });
            }
            return View(HarvestedCropsBeehives);
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