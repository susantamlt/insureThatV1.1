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
    public class TravelController : Controller
    {
        // GET: Travel
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> TravelCover(int? cid, int? PcId)
        {
            string apikey = null;
            if (Session["ApiKey"] != null)
            {
                apikey = Session["ApiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
            TravelCover TravelCover = new TravelCover();
            if (Session["Policyinclustions"] != null)
            {
                List<SessionModel> PolicyInclustions = new List<SessionModel>();
                TravelCover.PolicyInclusions = new List<SessionModel>();
                TravelCover.PolicyInclusions = Policyincllist;
                if (Policyincllist != null)
                {

                    if (Policyincllist.Exists(p => p.name == "Travel"))
                    {
                        if (Session["unId"] == null && Session["profileId"] == null)
                        {
                            Session["unId"] = Policyincllist.Where(p => p.name == "Travel").Select(p => p.UnitId).First();
                            Session["profileId"] = Policyincllist.Where(p => p.name == "Travel").Select(p => p.ProfileId).First();
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
                RedirectToAction("PolicyInclustions", "Customer", new { cid = cid });
            }
            NewPolicyDetailsClass Tmodel = new NewPolicyDetailsClass();

            if (cid != null && cid.HasValue && cid > 0)
            {
                ViewBag.cid = cid;
                TravelCover.CustomerId = cid.Value;

            }
            List<SelectListItem> ExcTcList = new List<SelectListItem>();
            ExcTcList = Tmodel.excessRate();
            var db = new MasterDataEntities();
            TravelCover.PolicyInclusion = new List<usp_GetUnit_Result>();

            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
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

            string policyid = null;
            bool policyinclusion = policyinclusions.Exists(p => p.Name == "Travel");
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
                TravelCover.ExistingPolicyInclustions = policyinclusions;
                //int sectionId = policyinclusions.Where(p => p.Name == "Home Contents" && p.UnitNumber == unid).Select(p => p.UnId).FirstOrDefault();
                //int? profileunid = policyinclusions.Where(p => p.Name == "Home Contents" && p.ProfileUnId == profileid).Select(p => p.ProfileUnId).FirstOrDefault();
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
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Travel&SectionUnId=&ProfileUnId=");
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
                            if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Travel"))
                            {
                                var policyhomelist = Policyincllist.FindAll(p => p.name == "Travel").ToList();
                                if (policyhomelist != null && policyhomelist.Count() > 0)
                                {
                                    if (Policyincllist.FindAll(p => p.name == "Travel").Exists(p => p.UnitId == null))
                                    {
                                        Policyincllist.FindAll(p => p.name == "Travel").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;
                                    }
                                    if (Policyincllist.FindAll(p => p.name == "Travel").Exists(p => p.ProfileId == null))
                                    {
                                        Policyincllist.FindAll(p => p.name == "Travel").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                    }
                                }
                                else
                                {
                                    Policyincllist.FindAll(p => p.name == "Travel").First().UnitId = unitdetails.SectionData.UnId;
                                    Policyincllist.FindAll(p => p.name == "Travel").First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                                TravelCover.PolicyInclusions = Policyincllist;
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
                if (unitdetails.ProfileData != null)
                {
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == TravelCover.DataofbirthObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == TravelCover.DataofbirthObj.EiId).Select(p => p.Value).FirstOrDefault();
                        TravelCover.DataofbirthObj.Dataofbirth = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == TravelCover.ExcessObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == TravelCover.ExcessObj.EiId).Select(p => p.Value).FirstOrDefault();
                        TravelCover.ExcessObj.Excess = val;
                    }
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == TravelCover.ImposedObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == TravelCover.ImposedObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    if (val == "1")
                    //    {
                    //        TravelCover.ImposedObj.Imposed = true;
                    //    }
                    //    else
                    //    {
                    //        TravelCover.ImposedObj.Imposed = false;
                    //    }
                    //}
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == TravelCover.LocationObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == TravelCover.LocationObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    TravelCover.LocationObj.Location = val;
                    //}
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == TravelCover.NumbertravelersObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == TravelCover.NumbertravelersObj.EiId).Select(p => p.Value).FirstOrDefault();
                        TravelCover.NumbertravelersObj.Numbertravelers = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == TravelCover.TravellerscoveredObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == TravelCover.TravellerscoveredObj.EiId).Select(p => p.Value).FirstOrDefault();
                        TravelCover.TravellerscoveredObj.Travellerscovered = val;
                    }
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == TravelCover.UnspecificvaluablesObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == TravelCover.UnspecificvaluablesObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    TravelCover.UnspecificvaluablesObj.Unspecificvaluables = val;
                    //}
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == TravelCover.WintersportObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == TravelCover.WintersportObj.EiId).Select(p => p.Value).FirstOrDefault();
                        TravelCover.WintersportObj.Wintersport = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == TravelCover.YourtripObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == TravelCover.YourtripObj.EiId).Select(p => p.Value).FirstOrDefault();
                        TravelCover.YourtripObj.Yourtrip = val;
                    }
                }
            }
            //var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Travels), Convert.ToInt32(PolicyType.RLS),policyid).ToList();
            //if (details != null && details.Any())
            //{
            //    if (details.Exists(q => q.QuestionId == TravelCover.TravellerscoveredObj.EiId))
            //    {
            //        TravelCover.TravellerscoveredObj.Travellerscovered = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.TravellerscoveredObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == TravelCover.DataofbirthObj.EiId))
            //    {
            //        TravelCover.DataofbirthObj.Dataofbirth = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.DataofbirthObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == TravelCover.NumbertravelersObj.EiId))
            //    {
            //        TravelCover.NumbertravelersObj.Numbertravelers = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.NumbertravelersObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == TravelCover.YourtripObj.EiId))
            //    {
            //        TravelCover.YourtripObj.Yourtrip = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.YourtripObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == TravelCover.WintersportObj.EiId))
            //    {
            //        TravelCover.WintersportObj.Wintersport = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.WintersportObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == TravelCover.ExcessObj.EiId))
            //    {
            //        TravelCover.ExcessObj.Excess = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.ExcessObj.EiId).FirstOrDefault().Answer);
            //    }
            //}
            if (unitdetails.ReferralList != null)
            {
                TravelCover.ReferralList = unitdetails.ReferralList;
                TravelCover.ReferralList.Replace("&nbsp;&nbsp;&nbsp;&nbsp", "");
                TravelCover.Referels = new List<string>();
                string[] delim = { "<br/>" };

                string[] spltd = TravelCover.ReferralList.Split(delim, StringSplitOptions.None);
                if (spltd != null && spltd.Count() > 0)
                {
                    for (int i = 0; i < spltd.Count(); i++)
                    {
                        TravelCover.Referels.Add(spltd[i].Replace("&nbsp;&nbsp;&nbsp;&nbsp", " "));
                    }
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
                //if (TravelCover.TravellerscoveredObj != null && TravelCover.TravellerscoveredObj.EiId > 0 && TravelCover.TravellerscoveredObj.Travellerscovered != null)
                //{
                //    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, Convert.ToInt32(RLSSection.Travels), TravelCover.TravellerscoveredObj.EiId, TravelCover.TravellerscoveredObj.Travellerscovered.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (TravelCover.DataofbirthObj != null && TravelCover.DataofbirthObj.EiId > 0 && TravelCover.DataofbirthObj.Dataofbirth != null)
                //{
                //    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, Convert.ToInt32(RLSSection.Travels), TravelCover.DataofbirthObj.EiId, TravelCover.DataofbirthObj.Dataofbirth.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (TravelCover.NumbertravelersObj != null && TravelCover.NumbertravelersObj.EiId > 0 && TravelCover.NumbertravelersObj.Numbertravelers != null)
                //{
                //    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, Convert.ToInt32(RLSSection.Travels), TravelCover.NumbertravelersObj.EiId, TravelCover.NumbertravelersObj.Numbertravelers.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (TravelCover.YourtripObj != null && TravelCover.YourtripObj.EiId > 0 && TravelCover.YourtripObj.Yourtrip != null)
                //{
                //    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, Convert.ToInt32(RLSSection.Travels), TravelCover.YourtripObj.EiId, TravelCover.YourtripObj.Yourtrip.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (TravelCover.WintersportObj != null && TravelCover.WintersportObj.EiId > 0 && TravelCover.WintersportObj.Wintersport != null)
                //{
                //    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, Convert.ToInt32(RLSSection.Travels), TravelCover.WintersportObj.EiId, TravelCover.WintersportObj.Wintersport.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //}
                //if (TravelCover.ExcessObj != null && TravelCover.ExcessObj.EiId > 0 && TravelCover.ExcessObj.Excess != null)
                //{
                //    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, Convert.ToInt32(RLSSection.Travels), TravelCover.ExcessObj.EiId, TravelCover.ExcessObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                //  }
                Session["unId"] = null;
                Session["profileId"] = null;
            }
            return RedirectToAction("DisclosureDetails", "Disclosure", new { cid = TravelCover.CustomerId });
            //return View(TravelCover);
        }
    }
}