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
    public class FarmPolicyFarmInterruptionController : Controller
    {
        // GET: FarmPolicyFarmInterruption
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> FarmInterruption(int? cid, int? PcId)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> ExcessToPay = new List<SelectListItem>();
            ExcessToPay = commonModel.excessRate();

            FPFarmInterruption FPFarmInterruption = new FPFarmInterruption();
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
                        if (Policyincllist.Exists(p => p.name == "Farm Interuption"))
                        {

                        }
                        else if (Policyincllist.Exists(p => p.name == "Farm Liability"))
                        {
                            return RedirectToAction("FarmLiability", "FarmPolicyFarmLiability", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Burglary"))
                        {
                            return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Electronics"))
                        {
                            return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Money"))
                        {
                            return RedirectToAction("Money", "FarmPolicyMoney", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Transit"))
                        {
                            return RedirectToAction("Transit", "FarmPolicyTransit", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Valuables"))
                        {
                            return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "LiveStock"))
                        {
                            return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = cid, PcId = PcId });

                        }
                        else if (Policyincllist.Exists(p => p.name == "Personal Liabilities Farm"))
                        {
                            return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Home Building"))
                        {
                            return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Home Content"))
                        {
                            return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Machinery"))
                        {
                            return RedirectToAction("Machinery", "FarmPolicyMachinery", new { cid = cid, PcId = PcId });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Motor"))
                        {
                            return RedirectToAction("VehicleDescription", "FarmPolicyMotor", new { cid = cid, PcId = PcId });
                        }
                        if (Policyincllist.Exists(p => p.name == "Farm Interuption"))
                        {
                            if (Session["unId"] == null && Session["profileId"] == null)
                            {
                                Session["unId"] = Policyincllist.Where(p => p.name == "Farm Interuption").Select(p => p.UnitId).First();
                                Session["profileId"] = Policyincllist.Where(p => p.name == "Farm Interuption").Select(p => p.ProfileId).First();
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
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            NewPolicyDetailsClass FarmDetailsmodel = new NewPolicyDetailsClass();


            if (cid != null)
            {
                ViewBag.cid = cid;
                FPFarmInterruption.CustomerId = cid.Value;
            }
            FPFarmInterruption.ExpFarmIncomeNextYearFPObj = new ExpFarmIncomeNextYearFP();
            FPFarmInterruption.ExpFarmIncomeNextYearFPObj.EiId = 62679;

            FPFarmInterruption.OptFarmIncomeIndemnityPerFPObj = new OptFarmIncomeIndemnityPerFP();
            FPFarmInterruption.OptFarmIncomeIndemnityPerFPObj.EiId = 62681;

            FPFarmInterruption.SumInsuredFarmIncomeFPObj = new SumInsuredFarmIncomeFP();
            FPFarmInterruption.SumInsuredFarmIncomeFPObj.EiId = 62683;

            FPFarmInterruption.ExcessFarmIncomeFPObj = new ExcessFarmIncomeFP();
            FPFarmInterruption.ExcessFarmIncomeFPObj.ExcessList = ExcessToPay;
            FPFarmInterruption.ExcessFarmIncomeFPObj.EiId = 62685;

            FPFarmInterruption.ExpAgistIncomeNextYearFPObj = new ExpAgistIncomeNextYearFP();
            FPFarmInterruption.ExpAgistIncomeNextYearFPObj.EiId = 62691;

            FPFarmInterruption.OptAgistIncomeIndemnityPerFPObj = new OptAgistIncomeIndemnityPerFP();
            FPFarmInterruption.OptAgistIncomeIndemnityPerFPObj.EiId = 62693;

            FPFarmInterruption.SumInsuredAgistIncomeFPObj = new SumInsuredAgistIncomeFP();
            FPFarmInterruption.SumInsuredAgistIncomeFPObj.EiId = 62695;

            FPFarmInterruption.ExcessAgistIncomeFPObj = new ExcessAgistIncomeFP();
            FPFarmInterruption.ExcessAgistIncomeFPObj.ExcessList = ExcessToPay;
            FPFarmInterruption.ExcessAgistIncomeFPObj.EiId = 62697;

            FPFarmInterruption.OptExtraCostIndemnityPerFPObj = new OptExtraCostIndemnityPerFP();
            FPFarmInterruption.OptExtraCostIndemnityPerFPObj.EiId = 62711;

            FPFarmInterruption.SumInsuredExtraCostFPObj = new SumInsuredExtraCostFP();
            FPFarmInterruption.SumInsuredExtraCostFPObj.EiId = 62713;

            FPFarmInterruption.ExcessExtraCostFPObj = new ExcessExtraCostFP();
            FPFarmInterruption.ExcessExtraCostFPObj.ExcessList = ExcessToPay;
            FPFarmInterruption.ExcessExtraCostFPObj.EiId = 62715;

            FPFarmInterruption.SumInsuredShearingDelayFPObj = new SumInsuredShearingDelayFP();
            FPFarmInterruption.SumInsuredShearingDelayFPObj.EiId = 62721;

            FPFarmInterruption.ExcessShearingDelayFPObj = new ExcessShearingDelayFP();
            FPFarmInterruption.ExcessShearingDelayFPObj.ExcessList = ExcessToPay;
            FPFarmInterruption.ExcessShearingDelayFPObj.EiId = 62723;

            var db = new MasterDataEntities();
            string policyid = null;
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPFarmInterruption.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPFarmInterruption.CustomerId;
            }
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
                FPFarmInterruption.PolicyInclusions = Policyincllist;
            }
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
                FPFarmInterruption.PolicyInclusion = new List<usp_GetUnit_Result>();
                if (PcId != null && PcId.HasValue && PcId > 0)
                {
                    FPFarmInterruption.PolicyInclusion = policyinclusions;
                }
                FPFarmInterruption.PolicyInclusions = new List<SessionModel>();
                if (PcId != null && PcId > 0)
                {
                    policyid = PcId.ToString();
                    FPFarmInterruption.PolicyId = policyid;
                }
                bool policyinclusion = policyinclusions.Exists(p => p.Name == "Farm Interuption");
                if (policyinclusion == true && PcId != null && PcId.HasValue)
                {

                    int sectionId = policyinclusions.Where(p => p.Name == "Farm Interuption" && p.UnitNumber == unid).Select(p => p.UnId).FirstOrDefault();
                    int? profileunid = policyinclusions.Where(p => p.Name == "Farm Interuption" && p.ProfileUnId == profileid).Select(p => p.ProfileUnId).FirstOrDefault();
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
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Farm Interuption&SectionUnId=&ProfileUnId=");
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails != null && unitdetails.SectionData != null)
                        {
                            Session["unId"] = unitdetails.SectionData.UnId;
                            Session["FprofileId"] = unitdetails.SectionData.ProfileUnId;
                            Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                            if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Farm Interuption"))
                            {
                                var policyhomelist = Policyincllist.FindAll(p => p.name == "Farm Interuption").ToList();
                                if (policyhomelist != null && policyhomelist.Count() > 0)
                                {
                                    Policyincllist.FindAll(p => p.name == "Farm Interuption").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;

                                    Policyincllist.FindAll(p => p.name == "Farm Interuption").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                                else
                                {
                                    Policyincllist.FindAll(p => p.name == "Farm Interuption").First().UnitId = unitdetails.SectionData.UnId;

                                    Policyincllist.FindAll(p => p.name == "Farm Interuption").First().ProfileId = unitdetails.SectionData.ProfileUnId;
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
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPFarmInterruption.ExcessAgistIncomeFPObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPFarmInterruption.ExcessAgistIncomeFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmInterruption.ExcessAgistIncomeFPObj.Excess = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPFarmInterruption.ExcessExtraCostFPObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPFarmInterruption.ExcessExtraCostFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmInterruption.ExcessExtraCostFPObj.Excess = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPFarmInterruption.ExcessFarmIncomeFPObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPFarmInterruption.ExcessFarmIncomeFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmInterruption.ExcessFarmIncomeFPObj.Excess = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPFarmInterruption.ExcessShearingDelayFPObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPFarmInterruption.ExcessShearingDelayFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmInterruption.ExcessShearingDelayFPObj.Excess = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPFarmInterruption.ExpAgistIncomeNextYearFPObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPFarmInterruption.ExpAgistIncomeNextYearFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmInterruption.ExpAgistIncomeNextYearFPObj.AgistIncomeNextYear = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPFarmInterruption.ExpFarmIncomeNextYearFPObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPFarmInterruption.ExpFarmIncomeNextYearFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmInterruption.ExpFarmIncomeNextYearFPObj.FarmIncomeNextYear = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPFarmInterruption.OptAgistIncomeIndemnityPerFPObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPFarmInterruption.OptAgistIncomeIndemnityPerFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmInterruption.OptAgistIncomeIndemnityPerFPObj.OptIndemnityPeriod = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPFarmInterruption.OptExtraCostIndemnityPerFPObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPFarmInterruption.OptExtraCostIndemnityPerFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmInterruption.OptExtraCostIndemnityPerFPObj.OptIndemnityPeriod = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPFarmInterruption.OptFarmIncomeIndemnityPerFPObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPFarmInterruption.OptFarmIncomeIndemnityPerFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmInterruption.OptFarmIncomeIndemnityPerFPObj.OptIndemnityPeriod = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPFarmInterruption.SumInsuredAgistIncomeFPObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPFarmInterruption.SumInsuredAgistIncomeFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmInterruption.SumInsuredAgistIncomeFPObj.SumInsured = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPFarmInterruption.SumInsuredExtraCostFPObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPFarmInterruption.SumInsuredExtraCostFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmInterruption.SumInsuredExtraCostFPObj.SumInsured = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPFarmInterruption.SumInsuredFarmIncomeFPObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPFarmInterruption.SumInsuredFarmIncomeFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmInterruption.SumInsuredFarmIncomeFPObj.SumInsured = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == FPFarmInterruption.SumInsuredShearingDelayFPObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == FPFarmInterruption.SumInsuredShearingDelayFPObj.EiId).Select(p => p.Value).FirstOrDefault();
                        FPFarmInterruption.SumInsuredShearingDelayFPObj.SumInsured = val;
                    }


                }
            }

            return View(FPFarmInterruption);
        }

        [HttpPost]
        public ActionResult FarmInterruption(int? cid, FPFarmInterruption FPFarmInterruption)
        {

            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> ExcessToPay = new List<SelectListItem>();
            ExcessToPay = commonModel.excessRate();

            FPFarmInterruption.ExcessFarmIncomeFPObj.ExcessList = ExcessToPay;
            FPFarmInterruption.ExcessAgistIncomeFPObj.ExcessList = ExcessToPay;
            FPFarmInterruption.ExcessExtraCostFPObj.ExcessList = ExcessToPay;
            FPFarmInterruption.ExcessShearingDelayFPObj.ExcessList = ExcessToPay;
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPFarmInterruption.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPFarmInterruption.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            Session["unId"] = null;
            Session["profileId"] = null;
            return RedirectToAction("FarmLiability", "FarmPolicyFarmLiability", new { cid = cid });
        }
    }
}