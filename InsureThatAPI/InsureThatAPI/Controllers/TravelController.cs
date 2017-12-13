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
    public class TravelController : Controller
    {
        // GET: Travel
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult TravelCover(int? cid)
        {
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {

                    if (Policyincllist.Contains("Travel"))
                        {
                          
                        }
                        else {if (Policyincllist.Contains("Boat"))
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
                RedirectToAction("PolicyInclustions", "Customer", new { cid = cid });
            }
            NewPolicyDetailsClass Tmodel = new NewPolicyDetailsClass();
            List<SelectListItem> ExcTcList = new List<SelectListItem>();
            ExcTcList = Tmodel.excessRate();
            TravelCover TravelCover = new TravelCover();
            TravelCover.TravellerscoveredObj = new TravellersToBeCovered();
            TravelCover.TravellerscoveredObj.EiId = 61429;
            TravelCover.DataofbirthObj = new DataOfBirthsTC();
            TravelCover.DataofbirthObj.EiId = 61431;
            TravelCover.NumbertravelersObj = new NumberOfTravelers();
            TravelCover.NumbertravelersObj.EiId = 61433;
            TravelCover.YourtripObj = new YourTrips();
            TravelCover.YourtripObj.EiId = 61437;
            TravelCover.WintersportObj = new WinterSports();
            TravelCover.WintersportObj.EiId = 61441;
            TravelCover.ExcessObj = new ExcessesTC();
            TravelCover.ExcessObj.EiId = 61443;
            TravelCover.ExcessObj.ExcessList = ExcTcList;
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Travels), Convert.ToInt32(PolicyType.RLS),policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == TravelCover.TravellerscoveredObj.EiId))
                {
                    TravelCover.TravellerscoveredObj.Travellerscovered = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.TravellerscoveredObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == TravelCover.DataofbirthObj.EiId))
                {
                    TravelCover.DataofbirthObj.Dataofbirth = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.DataofbirthObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == TravelCover.NumbertravelersObj.EiId))
                {
                    TravelCover.NumbertravelersObj.Numbertravelers = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.NumbertravelersObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == TravelCover.YourtripObj.EiId))
                {
                    TravelCover.YourtripObj.Yourtrip = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.YourtripObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == TravelCover.WintersportObj.EiId))
                {
                    TravelCover.WintersportObj.Wintersport = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.WintersportObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == TravelCover.ExcessObj.EiId))
                {
                    TravelCover.ExcessObj.Excess = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.ExcessObj.EiId).FirstOrDefault().Answer);
                }
            }
                return View(TravelCover);
        }
        [HttpPost]
        public ActionResult TravelCover(TravelCover TravelCover, int? cid)
        {
            NewPolicyDetailsClass Tmodel = new NewPolicyDetailsClass();
            List<SelectListItem> ExcTcList = new List<SelectListItem>();
            ExcTcList = Tmodel.excessRate();
            TravelCover.ExcessObj.ExcessList = ExcTcList;
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (TravelCover.TravellerscoveredObj != null && TravelCover.TravellerscoveredObj.EiId > 0 && TravelCover.TravellerscoveredObj.Travellerscovered != null)
                {
                    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, Convert.ToInt32(RLSSection.Travels), TravelCover.TravellerscoveredObj.EiId, TravelCover.TravellerscoveredObj.Travellerscovered.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (TravelCover.DataofbirthObj != null && TravelCover.DataofbirthObj.EiId > 0 && TravelCover.DataofbirthObj.Dataofbirth != null)
                {
                    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, Convert.ToInt32(RLSSection.Travels), TravelCover.DataofbirthObj.EiId, TravelCover.DataofbirthObj.Dataofbirth.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (TravelCover.NumbertravelersObj != null && TravelCover.NumbertravelersObj.EiId > 0 && TravelCover.NumbertravelersObj.Numbertravelers != null)
                {
                    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, Convert.ToInt32(RLSSection.Travels), TravelCover.NumbertravelersObj.EiId, TravelCover.NumbertravelersObj.Numbertravelers.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (TravelCover.YourtripObj != null && TravelCover.YourtripObj.EiId > 0 && TravelCover.YourtripObj.Yourtrip != null)
                {
                    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, Convert.ToInt32(RLSSection.Travels), TravelCover.YourtripObj.EiId, TravelCover.YourtripObj.Yourtrip.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (TravelCover.WintersportObj != null && TravelCover.WintersportObj.EiId > 0 && TravelCover.WintersportObj.Wintersport != null)
                {
                    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, Convert.ToInt32(RLSSection.Travels), TravelCover.WintersportObj.EiId, TravelCover.WintersportObj.Wintersport.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (TravelCover.ExcessObj != null && TravelCover.ExcessObj.EiId > 0 && TravelCover.ExcessObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, Convert.ToInt32(RLSSection.Travels), TravelCover.ExcessObj.EiId, TravelCover.ExcessObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
            }
            return View(TravelCover);
        }
    }
}