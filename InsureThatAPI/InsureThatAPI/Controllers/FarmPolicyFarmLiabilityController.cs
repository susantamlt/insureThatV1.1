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
    public class FarmPolicyFarmLiabilityController : Controller
    {
        // GET: FarmPolicyFarmLiability
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> FarmLiability(int? cid, int? PcId)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> GLLimitOfIndemnity = new List<SelectListItem>();
            GLLimitOfIndemnity = commonModel.LimitOfIndemnity();
            List<SelectListItem> excessToPayFarmLiability = new List<SelectListItem>();
            excessToPayFarmLiability = commonModel.excessRate();
            List<SelectListItem> GLTypeOfAccommodation = new List<SelectListItem>();
            GLTypeOfAccommodation = commonModel.TypeOfAccommodation();

            FPFarmliability FPFarmliability = new FPFarmliability();
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPFarmliability.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPFarmliability.CustomerId;
            }
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            NewPolicyDetailsClass commonmethods = new NewPolicyDetailsClass();
            var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
            string apikey = null;
            bool policyinclusion = policyinclusions.Exists(p => p.Name == "Transit");
            if (Session["apiKey"] != null)
            {
                apikey = Session["apiKey"].ToString();
                FPFarmliability.ApiKey = Session["apiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            string policyid = null;
            List<SessionModel> PolicyInclustions = new List<SessionModel>();
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                policyid = PcId.ToString();
                FPFarmliability.PolicyId = policyid;
            }
            else if (Session["Policyinclustions"] != null)
            {
                #region Policy Selected or not testing              
                FPFarmliability.PolicyInclusions = new List<SessionModel>();
                var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                FPFarmliability.PolicyInclusions = Policyincllist;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Exists(p => p.name == "Farm Liability"))
                    {

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
                    else if (Policyincllist.Exists(p => p.name == "LiveStock"))
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
                    if (Policyincllist.Exists(p => p.name == "Farm Liability"))
                    {
                        if (Session["unId"] == null && Session["profileId"] == null)
                        {
                            Session["unId"] = Policyincllist.Where(p => p.name == "Farm Liability").Select(p => p.UnitId).First();
                            Session["profileId"] = Policyincllist.Where(p => p.name == "Farm Liability").Select(p => p.ProfileId).First();
                        }
                    }
                    else
                    {
                        return RedirectToAction("DisclosureDetails", "Disclosure", new { cid = cid, PcId = PcId });
                    }

                }
                #endregion
            }
            FPFarmliability.GenLiabilityLimitOfIndemnityFPObj = new GenLiabilityLimitOfIndemnityFP();
            FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.GLLimitOfIndemnityFPList = GLLimitOfIndemnity;
            FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.EiId = 62871;

            FPFarmliability.ProdLiabilityLimitOfIndemnityFPObj = new ProdLiabilityLimitOfIndemnityFP();
            FPFarmliability.ProdLiabilityLimitOfIndemnityFPObj.EiId = 62875;

            FPFarmliability.ListOfProductsSoldFPObj = new ListOfProductsSoldFP();
            FPFarmliability.ListOfProductsSoldFPObj.EiId = 62877;

            FPFarmliability.OptPayingGuestFP = new OptPayingGuestFP();
            FPFarmliability.OptPayingGuestFP.EiId = 62883;

            FPFarmliability.TypeOfAccomGuestsStayingInFPObj = new TypeOfAccomGuestsStayingInFP();
            FPFarmliability.TypeOfAccomGuestsStayingInFPObj.TypeOfAccommodationGuestsStayingInList = GLTypeOfAccommodation;
            FPFarmliability.TypeOfAccomGuestsStayingInFPObj.EiId = 62887;

            FPFarmliability.DescriptionOfAccommodationFPObj = new DescriptionOfAccommodationFP();
            FPFarmliability.DescriptionOfAccommodationFPObj.EiId = 62889;

            FPFarmliability.OptAccomComplyRegulationFPObj = new OptAccomComplyRegulationFP();
            FPFarmliability.OptAccomComplyRegulationFPObj.EiId = 62899;

            FPFarmliability.OptHolidayFarmsFPObj = new OptHolidayFarmsFP();
            FPFarmliability.OptHolidayFarmsFPObj.EiId = 62901;


            FPFarmliability.ExcessFPFarmLiabilityObj = new ExcessFPFarmLiability();
            FPFarmliability.ExcessFPFarmLiabilityObj.ExcessList = excessToPayFarmLiability;
            FPFarmliability.ExcessFPFarmLiabilityObj.EiId = 62907;
            FPFarmliability.AddressObj = new AddressFLAddress();
            FPFarmliability.AddressObj.EiId = 0;

            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = Convert.ToInt32(Session["unId"]);
            int profileid = Convert.ToInt32(Session["profileId"]);
            if (policyinclusion == true && PcId != null && PcId.HasValue)
            {
                FPFarmliability.ExistingPolicyInclustions = policyinclusions;

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
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Farm Liability&SectionUnId=&ProfileUnId=0");
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);

                        if (unitdetails != null && unitdetails.SectionData != null)
                        {
                            if (unitdetails != null && unitdetails.ErrorMessage != null && unitdetails.ErrorMessage.Count() > 0 && unitdetails.Status == "Failure")
                            {
                                bool exists = FPFarmliability.PolicyInclusions.Exists(p => p.name == "Farm Liability");
                                if (exists == true)
                                {
                                    List<SessionModel> values = new List<SessionModel>();
                                    values = (List<SessionModel>)Session["Policyinclustions"];
                                    for (int k = 0; k < values.Count(); k++)
                                    {
                                        if (values[k].name == "Farm Liability" && values[k].UnitId == null && values[k].ProfileId == null)
                                        {
                                            values.RemoveAt(k);
                                        }
                                    }
                                    Session["Policyinclustions"] = values;
                                }
                            }
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
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.LimitOfIndemnity = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPFarmliability.ProdLiabilityLimitOfIndemnityFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPFarmliability.ProdLiabilityLimitOfIndemnityFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmliability.ProdLiabilityLimitOfIndemnityFPObj.LimitOfIndemnity = val;
                    }
                    //if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPFarmliability.ListOfProductsSoldFPObj.EiId))
                    //{
                    //    string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPFarmliability.ListOfProductsSoldFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    FPFarmliability.ListOfProductsSoldFPObj.ListOfProductsSold = val;
                    //}
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPFarmliability.OptPayingGuestFP.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPFarmliability.OptPayingGuestFP.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmliability.OptPayingGuestFP.PayingGuestOption = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPFarmliability.TypeOfAccomGuestsStayingInFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPFarmliability.TypeOfAccomGuestsStayingInFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmliability.TypeOfAccomGuestsStayingInFPObj.TypeOfAccommodation = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPFarmliability.DescriptionOfAccommodationFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPFarmliability.DescriptionOfAccommodationFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmliability.DescriptionOfAccommodationFPObj.DescOfAccommodation = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPFarmliability.OptAccomComplyRegulationFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPFarmliability.OptAccomComplyRegulationFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmliability.OptAccomComplyRegulationFPObj.AccomComplyRegulations = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPFarmliability.OptHolidayFarmsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPFarmliability.OptHolidayFarmsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmliability.OptHolidayFarmsFPObj.HolidayFarms = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPFarmliability.ExcessFPFarmLiabilityObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPFarmliability.ExcessFPFarmLiabilityObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmliability.ExcessFPFarmLiabilityObj.Excess = val;
                    }
                }
                if (unitdetails.SectionData != null && unitdetails.SectionData.AddressData != null)
                {
                    if (unitdetails.SectionData.AddressData != null)
                    {
                        FPFarmliability.AddressObj.Address = unitdetails.SectionData.AddressData.AddressLine1 + ", " + unitdetails.SectionData.AddressData.Suburb + " ," + unitdetails.SectionData.AddressData.State + ", " + unitdetails.SectionData.AddressData.Postcode;
                    }
                }
            }
            return View(FPFarmliability);
        }

        [HttpPost]
        public ActionResult FarmLiability(int? cid, FPFarmliability FPFarmliability)
        {

            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> GLLimitOfIndemnity = new List<SelectListItem>();
            GLLimitOfIndemnity = commonModel.LimitOfIndemnity();
            FPFarmliability.GenLiabilityLimitOfIndemnityFPObj.GLLimitOfIndemnityFPList = GLLimitOfIndemnity;
            List<SelectListItem> excessToPayFarmLiability = new List<SelectListItem>();
            excessToPayFarmLiability = commonModel.excessRate();
            FPFarmliability.ExcessFPFarmLiabilityObj.ExcessList = excessToPayFarmLiability;
            List<SelectListItem> GLTypeOfAccommodation = new List<SelectListItem>();
            GLTypeOfAccommodation = commonModel.TypeOfAccommodation();
            FPFarmliability.TypeOfAccomGuestsStayingInFPObj.TypeOfAccommodationGuestsStayingInList = GLTypeOfAccommodation;
            var db = new MasterDataEntities();
            string policyid = null;

            Session["unId"] = null;
            Session["profileId"] = null;
            return RedirectToAction("Machinery", "FarmPolicyMachinery", new { cid = FPFarmliability.CustomerId, PcId = FPFarmliability.PcId });
        }

    }
}