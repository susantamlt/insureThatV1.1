using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;//append model
using InsureThatAPI.CommonMethods;//append Common Methods
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace InsureThatAPI.Controllers
{
    
    public class FarmPolicyElectronicsController : Controller
    {
        // GET: FarmPolicyElectronics
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Electronics(int? cid,int? PcId)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> ElectronicItemTypeofUnit = new List<SelectListItem>();
            ElectronicItemTypeofUnit = commonModel.ElectronicTypeOfUnit();
            List<SelectListItem> excessToPayElectronics = new List<SelectListItem>();
            excessToPayElectronics = commonModel.excessRate();
            FPElectronics FPElectronics = new FPElectronics();
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPElectronics.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPElectronics.CustomerId;
            }
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            NewPolicyDetailsClass commonmethods = new NewPolicyDetailsClass();
            var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
            string apikey = null;
            bool policyinclusion = policyinclusions.Exists(p => p.Name == "Electronics");
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
            if (Session["Policyinclustions"] != null)
            {
                Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                if (Policyincllist != null)
                {
                    if (Policyincllist != null)
                    {
                      if (Policyincllist.Exists(p => p.name == "Electronics"))
                        {
                       
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
                        if (Policyincllist.Exists(p => p.name == "Electronics"))
                        {
                            if (Session["unId"] == null && Session["profileId"] == null)
                            {
                                Session["unId"] = Policyincllist.Where(p => p.name == "Electronics").Select(p => p.UnitId).First();
                                Session["profileId"] = Policyincllist.Where(p => p.name == "Electronics").Select(p => p.ProfileId).First();
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
            FPElectronics.TypeOfUnitFPObj = new TypeOfUnitFP();
            FPElectronics.TypeOfUnitFPObj.ElectronicsTypeofUnitList = ElectronicItemTypeofUnit;
            FPElectronics.TypeOfUnitFPObj.EiId = 63161;
            FPElectronics.MakeAndModelFPObj = new MakeAndModelFP();
            FPElectronics.MakeAndModelFPObj.EiId = 63163;
            FPElectronics.SerialNumberFPObj = new SerialNumberFP();
            FPElectronics.SerialNumberFPObj.EiId = 63165;
            FPElectronics.NoOfUnitsFPObj = new NoOfUnitsFP();
            FPElectronics.NoOfUnitsFPObj.EiId = 63167;
            FPElectronics.OptPortableItemsFPObj = new OptPortableItemsFP();
            FPElectronics.OptPortableItemsFPObj.EiId = 63169;
            FPElectronics.SumInsuredPerUnitFPObj = new SumInsuredPerUnitFP();
            FPElectronics.SumInsuredPerUnitFPObj.EiId = 63171;
            FPElectronics.TotalSumInsuredFPObj = new TotalSumInsuredFP();
            FPElectronics.TotalSumInsuredFPObj.EiId = 63173;
            FPElectronics.ExcessElectronicsFPObj = new ExcessElectronicsFP();
            FPElectronics.ExcessElectronicsFPObj.ExcessList = excessToPayElectronics;
            FPElectronics.ExcessElectronicsFPObj.EiId = 63177;
            FPElectronics.CoverLossOfDataFPObj = new CoverLossOfDataFP();
            FPElectronics.CoverLossOfDataFPObj.EiId = 63189;
            FPElectronics.ExcessCoverLossOfDataFPObj = new ExcessCoverLossOfDataFP();
            FPElectronics.ExcessCoverLossOfDataFPObj.ExcessList = excessToPayElectronics;
            FPElectronics.ExcessCoverLossOfDataFPObj.EiId = 63191;
            FPElectronics.AddressObj = new AddressEAddress();
            FPElectronics.AddressObj.EiId = 0;

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
                FPElectronics.PolicyInclusions = Policyincllist;
            }
            if (policyinclusion == true && PcId != null && PcId.HasValue)
            {
                FPElectronics.ExistingPolicyInclustions = policyinclusions;

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
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Electronics&SectionUnId=&ProfileUnId=");
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
                if (unitdetails.SectionData != null && unitdetails.SectionData.ValueData != null)
                {
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPElectronics.TypeOfUnitFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPElectronics.TypeOfUnitFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPElectronics.TypeOfUnitFPObj.TypeofUnit = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPElectronics.MakeAndModelFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPElectronics.MakeAndModelFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPElectronics.MakeAndModelFPObj.MakeandModel = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPElectronics.SerialNumberFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPElectronics.SerialNumberFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPElectronics.SerialNumberFPObj.SerialNumber = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPElectronics.NoOfUnitsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPElectronics.NoOfUnitsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPElectronics.NoOfUnitsFPObj.NoOfUnits = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPElectronics.OptPortableItemsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPElectronics.OptPortableItemsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                       // FPElectronics.OptPortableItemsFPObj.PortableItemsOption = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPElectronics.SumInsuredPerUnitFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPElectronics.SumInsuredPerUnitFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPElectronics.SumInsuredPerUnitFPObj.SumInsuredPerUnit = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPElectronics.TotalSumInsuredFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPElectronics.TotalSumInsuredFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPElectronics.TotalSumInsuredFPObj.TotalSumInsured = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPElectronics.ExcessElectronicsFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPElectronics.ExcessElectronicsFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPElectronics.ExcessElectronicsFPObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPElectronics.CoverLossOfDataFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPElectronics.CoverLossOfDataFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPElectronics.CoverLossOfDataFPObj.CoverLossofData = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == FPElectronics.ExcessCoverLossOfDataFPObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == FPElectronics.ExcessCoverLossOfDataFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPElectronics.ExcessCoverLossOfDataFPObj.Excess = val;
                    }
                }
                if (unitdetails.SectionData != null && unitdetails.SectionData.AddressData != null)
                {
                    if (unitdetails.SectionData.AddressData != null)
                    {
                        FPElectronics.AddressObj.Address = unitdetails.SectionData.AddressData.AddressLine1 + ", " + unitdetails.SectionData.AddressData.Suburb + " ," + unitdetails.SectionData.AddressData.State + ", " + unitdetails.SectionData.AddressData.Postcode;
                    }
                }

            }
            return View(FPElectronics);
        }
        [HttpPost]
        public ActionResult Electronics(int? cid, FPElectronics FPElectronics)
        {

            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> ElectronicsExcessToPay = new List<SelectListItem>();
            ElectronicsExcessToPay = commonModel.excessRate();
            FPElectronics.ExcessElectronicsFPObj.ExcessList = ElectronicsExcessToPay;
            FPElectronics.ExcessCoverLossOfDataFPObj.ExcessList = ElectronicsExcessToPay;
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPElectronics.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPElectronics.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            Session["unId"] = null;
            Session["profileId"] = null;
            return RedirectToAction("Transit", "FarmPolicyTransit", new { cid = FPElectronics.CustomerId, PcId = FPElectronics.PcId });
        }
    }
}