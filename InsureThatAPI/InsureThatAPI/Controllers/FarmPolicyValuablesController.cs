using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;//append model
using InsureThatAPI.CommonMethods;//append Common Methods
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace InsureThatAPI.Controllers
{
    public class FarmPolicyValuablesController : Controller
    {
        // GET: FarmPolicyValuables
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Valuables(int? cid, int? PcId)
        {
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            string ApiKey = null;
            if (Session["ApiKey"] != null)
            {
                ApiKey = Session["ApiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            var Policyincllist = new List<SessionModel>();
            FPValuables FPValuables = new FPValuables();
            if (Session["Policyinclustions"] != null)
            {
                Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                if (Policyincllist != null)
                {
                    if (Policyincllist != null)
                    {
                        if (Policyincllist.Exists(p => p.name == "Valuables"))
                        {

                        }
                        else if (Policyincllist.Exists(p => p.name == "Motor"))
                        {
                            return RedirectToAction("VehicleDescription", "FarmMotors", new { cid = cid, PcId = PcId });
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

                    }
                }
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 1021 });
            }
            ViewBag.cid = cid;
            if (cid != null)
            {
                FPValuables.CustomerId = cid.Value;
            }
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> DescriptionSpeciItemsList = new List<SelectListItem>();
            DescriptionSpeciItemsList = commonModel.descriptionListFC();

            List<SelectListItem> ValuablesexcessToPay = new List<SelectListItem>();
            ValuablesexcessToPay = commonModel.excessRate();

            FPValuables.CoverUnspecifiedValuablesObj = new CoverForUnspecifiedValuables();
            FPValuables.CoverUnspecifiedValuablesObj.EiId = 63725;

            FPValuables.SpecifiedItemDescriptionObj = new SpecifiedItemDescription();
            FPValuables.SpecifiedItemDescriptionObj.SpecItemDescriptionList = DescriptionSpeciItemsList;
            FPValuables.SpecifiedItemDescriptionObj.EiId = 63733;

            FPValuables.SpecifiedItemSumInsuredObj = new SpecifiedItemSumInsured();
            FPValuables.SpecifiedItemSumInsuredObj.EiId = 63735;

            FPValuables.ExcessValuablesObj = new ExcessValuables();
            FPValuables.ExcessValuablesObj.ExcessList = ValuablesexcessToPay;
            FPValuables.ExcessValuablesObj.EiId = 63741;

            var db = new MasterDataEntities();
            string policyid = null;
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = 0;
            int profileid = 0;
            int Fprofileid = 0;
            if (Session["unId"] != null && Session["ProfileId"] != null)
            {
                unid = Convert.ToInt32(Session["unId"]);
                profileid = Convert.ToInt32(Session["profileId"]);
            }
            if (Session["FProfileId"] != null)
            {
                Fprofileid = Convert.ToInt32(Session["FprofileId"]);
            }
            if (Session["Policyinclustions"] != null)
            {
                FPValuables.PolicyInclusions = Policyincllist;
            }
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
                FPValuables.PolicyInclusion = new List<usp_GetUnit_Result>();
                if (PcId != null && PcId.HasValue && PcId > 0)
                {
                    FPValuables.PolicyInclusion = policyinclusions;
                }
                FPValuables.PolicyInclusions = new List<SessionModel>();
                if (PcId != null && PcId > 0)
                {
                    policyid = PcId.ToString();
                    FPValuables.PolicyId = policyid;
                }
                bool policyinclusion = policyinclusions.Exists(p => p.Name == "Valuables");
                if (policyinclusion == true && PcId != null && PcId.HasValue)
                {

                    int sectionId = policyinclusions.Where(p => p.Name == "Valuables" && p.UnitNumber == unid).Select(p => p.UnId).FirstOrDefault();
                    int? profileunid = policyinclusions.Where(p => p.Name == "Valuables" && p.ProfileUnId == profileid).Select(p => p.ProfileUnId).FirstOrDefault();
                    HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                    var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    }
                }
            }
            else
            {
                if (PcId == null && Session["unId"] == null && Session["profileId"] == null)
                {
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Valuables&SectionUnId=&ProfileUnId=");
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails != null && unitdetails.SectionData != null)
                        {
                            Session["unId"] = unitdetails.SectionData.UnId;
                            Session["FprofileId"] = unitdetails.SectionData.ProfileUnId;
                            Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                            if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Valuables"))
                            {
                                var policyhomelist = Policyincllist.FindAll(p => p.name == "Valuables").ToList();
                                if (policyhomelist != null && policyhomelist.Count() > 0)
                                {
                                    Policyincllist.FindAll(p => p.name == "Valuables").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;

                                    Policyincllist.FindAll(p => p.name == "Valuables").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                                else
                                {
                                    Policyincllist.FindAll(p => p.name == "Valuables").First().UnitId = unitdetails.SectionData.UnId;

                                    Policyincllist.FindAll(p => p.name == "Valuables").First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (PcId == null && Session["unId"] != null && (Session["profileId"] != null || (Fprofileid != null && Fprofileid < 0)))
                    {
                        if (profileid == null || profileid == 0)
                        {
                            profileid = Fprofileid;
                        }
                        HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                        var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                        if (EmpResponse != null)
                        {
                            unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                            if (unitdetails != null && unitdetails.SectionData != null)
                            {
                                Session["unId"] = unitdetails.SectionData.UnId;
                                Session["FprofileId"] = unitdetails.SectionData.ProfileUnId;
                            }
                        }
                    }
                }
            }
            if (unitdetails != null)
            {
                if (unitdetails.ProfileData != null)
                {
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPValuables.CoverUnspecifiedValuablesObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPValuables.CoverUnspecifiedValuablesObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPValuables.CoverUnspecifiedValuablesObj.CoverUnspecifiedValuables = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPValuables.ExcessValuablesObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPValuables.ExcessValuablesObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPValuables.ExcessValuablesObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPValuables.SpecifiedItemDescriptionObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPValuables.SpecifiedItemDescriptionObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPValuables.SpecifiedItemDescriptionObj.ItemDescription = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPValuables.SpecifiedItemDescriptionObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var ItemDescriptionList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPValuables.SpecifiedItemDescriptionObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < ItemDescriptionList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63733;
                                vds.Element.ItId = ItemDescriptionList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPValuables.SpecifiedItemDescriptionObj.EiId && p.Element.ItId == ItemDescriptionList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPValuables.SpecifiedItemDescriptionList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPValuables.SpecifiedItemSumInsuredObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPValuables.SpecifiedItemSumInsuredObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            FPValuables.SpecifiedItemSumInsuredObj.ItemSumInsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == FPValuables.SpecifiedItemSumInsuredObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var ItemSumInsuredList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPValuables.SpecifiedItemSumInsuredObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < ItemSumInsuredList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 63735;
                                vds.Element.ItId = ItemSumInsuredList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPValuables.SpecifiedItemSumInsuredObj.EiId && p.Element.ItId == ItemSumInsuredList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            FPValuables.SpecifiedItemSumInsuredList = elmnts;
                        }
                    }
                }
            }
            return View(FPValuables);
        }

        [HttpPost]
        public ActionResult Valuables(int? cid, FPValuables FPValuables)
        {

            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> DescriptionSpeciItemsList = new List<SelectListItem>();
            DescriptionSpeciItemsList = commonModel.descriptionListFC();
            FPValuables.SpecifiedItemDescriptionObj.SpecItemDescriptionList = DescriptionSpeciItemsList;
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPValuables.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPValuables.CustomerId;
            }
            List<SelectListItem> ValuablesexcessToPay = new List<SelectListItem>();
            ValuablesexcessToPay = commonModel.excessRate();
            FPValuables.ExcessValuablesObj.ExcessList = ValuablesexcessToPay;
            string policyid = null;
            var db = new MasterDataEntities();
            Session["unId"] = null;
            Session["profileId"] = null;
            return RedirectToAction("VehicleDescription", "FarmMotors", new { cid = FPValuables.CustomerId, PcId = FPValuables.PcId });
        }
    }
}