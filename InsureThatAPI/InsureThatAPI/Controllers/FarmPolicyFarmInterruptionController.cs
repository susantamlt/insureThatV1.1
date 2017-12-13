using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;//append model
using InsureThatAPI.CommonMethods;//append Common Methods
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;

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
        public ActionResult FarmInterruption(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> ExcessToPay = new List<SelectListItem>();
            ExcessToPay = commonModel.excessRate();

            FPFarmInterruption FPFarmInterruption = new FPFarmInterruption();

            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Contains("FarmInteruption"))
                    {

                    }
                    else
                    {
                        if (Policyincllist.Contains("MobileFarmProperty"))
                        {
                            return RedirectToAction("FarmContents", "MobileFarm", new { cid = cid });
                        }

                        else if (Policyincllist.Contains("FarmLiability"))
                        {
                            return RedirectToAction("FarmLiability", "FarmPolicyFarmLiability", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Burglary"))
                        {
                            return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Electronics"))
                        {
                            return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Money"))
                        {
                            return RedirectToAction("Money", "FarmPolicyMoney", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Transit"))
                        {
                            return RedirectToAction("FarmPolicyTransit", "Transit", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("ValuablesFarm"))
                        {
                              return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("LiveStockFarm"))
                        {
                            return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("PersonalLiabilitiesFarm"))
                        {
                            return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeBuildingFarm"))
                        {
                            return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("HomeContent"))
                        {
                            return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Machinery"))
                        {
                            //  return RedirectToAction("", "", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("MotorFarm"))
                        {
                            // return RedirectToAction("", "", new { cid = cid });
                        }
                    }
                }
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 2 });
            }
            ViewBag.cid = cid;
            if (cid != null)
            {
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
            var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(FarmPolicySection.FarmInteruption), Convert.ToInt32(PolicyType.FarmPolicy),policyid).ToList();
            if (details != null && details.Any())
            {

                if (details.Exists(q => q.QuestionId == FPFarmInterruption.ExpFarmIncomeNextYearFPObj.EiId))
                {
                    FPFarmInterruption.ExpFarmIncomeNextYearFPObj.FarmIncomeNextYear = Convert.ToString(details.Where(q => q.QuestionId == FPFarmInterruption.ExpFarmIncomeNextYearFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPFarmInterruption.SumInsuredFarmIncomeFPObj.EiId))
                {
                    FPFarmInterruption.SumInsuredFarmIncomeFPObj.SumInsured = Convert.ToString(details.Where(q => q.QuestionId == FPFarmInterruption.SumInsuredFarmIncomeFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPFarmInterruption.OptFarmIncomeIndemnityPerFPObj.EiId))
                {
                    FPFarmInterruption.OptFarmIncomeIndemnityPerFPObj.OptIndemnityPeriod = Convert.ToString(details.Where(q => q.QuestionId == FPFarmInterruption.OptFarmIncomeIndemnityPerFPObj.EiId).FirstOrDefault().Answer);
                }


                if (details.Exists(q => q.QuestionId == FPFarmInterruption.ExcessFarmIncomeFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPFarmInterruption.ExcessFarmIncomeFPObj.EiId).FirstOrDefault();
                    FPFarmInterruption.ExcessFarmIncomeFPObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPFarmInterruption.ExpAgistIncomeNextYearFPObj.EiId))
                {
                    FPFarmInterruption.ExpAgistIncomeNextYearFPObj.AgistIncomeNextYear = Convert.ToString(details.Where(q => q.QuestionId == FPFarmInterruption.ExpAgistIncomeNextYearFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPFarmInterruption.SumInsuredAgistIncomeFPObj.EiId))
                {
                    FPFarmInterruption.SumInsuredAgistIncomeFPObj.SumInsured = Convert.ToString(details.Where(q => q.QuestionId == FPFarmInterruption.SumInsuredAgistIncomeFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPFarmInterruption.OptAgistIncomeIndemnityPerFPObj.EiId))
                {
                    FPFarmInterruption.OptAgistIncomeIndemnityPerFPObj.OptIndemnityPeriod = Convert.ToString(details.Where(q => q.QuestionId == FPFarmInterruption.OptAgistIncomeIndemnityPerFPObj.EiId).FirstOrDefault().Answer);
                }


                if (details.Exists(q => q.QuestionId == FPFarmInterruption.ExcessAgistIncomeFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPFarmInterruption.ExcessAgistIncomeFPObj.EiId).FirstOrDefault();
                    FPFarmInterruption.ExcessAgistIncomeFPObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == FPFarmInterruption.SumInsuredExtraCostFPObj.EiId))
                {
                    FPFarmInterruption.SumInsuredExtraCostFPObj.SumInsured = Convert.ToString(details.Where(q => q.QuestionId == FPFarmInterruption.SumInsuredExtraCostFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPFarmInterruption.OptExtraCostIndemnityPerFPObj.EiId))
                {
                    FPFarmInterruption.OptExtraCostIndemnityPerFPObj.OptIndemnityPeriod = Convert.ToString(details.Where(q => q.QuestionId == FPFarmInterruption.OptExtraCostIndemnityPerFPObj.EiId).FirstOrDefault().Answer);
                }


                if (details.Exists(q => q.QuestionId == FPFarmInterruption.ExcessExtraCostFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPFarmInterruption.ExcessExtraCostFPObj.EiId).FirstOrDefault();
                    FPFarmInterruption.ExcessExtraCostFPObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPFarmInterruption.SumInsuredShearingDelayFPObj.EiId))
                {
                    FPFarmInterruption.SumInsuredShearingDelayFPObj.SumInsured = Convert.ToString(details.Where(q => q.QuestionId == FPFarmInterruption.SumInsuredShearingDelayFPObj.EiId).FirstOrDefault().Answer);
                }


                if (details.Exists(q => q.QuestionId == FPFarmInterruption.ExcessShearingDelayFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPFarmInterruption.ExcessShearingDelayFPObj.EiId).FirstOrDefault();
                    FPFarmInterruption.ExcessShearingDelayFPObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
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
            if (cid.HasValue && cid > 0)
            {

                if (FPFarmInterruption.ExpFarmIncomeNextYearFPObj != null && FPFarmInterruption.ExpFarmIncomeNextYearFPObj.EiId > 0 && FPFarmInterruption.ExpFarmIncomeNextYearFPObj.FarmIncomeNextYear != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmInterruption.CustomerId, Convert.ToInt32(FarmPolicySection.FarmInteruption), FPFarmInterruption.ExpFarmIncomeNextYearFPObj.EiId, FPFarmInterruption.ExpFarmIncomeNextYearFPObj.FarmIncomeNextYear.ToString(), Convert.ToInt32(PolicyType.FarmPolicy),policyid);
                }

                if (FPFarmInterruption.OptFarmIncomeIndemnityPerFPObj != null && FPFarmInterruption.OptFarmIncomeIndemnityPerFPObj.EiId > 0 && FPFarmInterruption.OptFarmIncomeIndemnityPerFPObj.OptIndemnityPeriod != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmInterruption.CustomerId, Convert.ToInt32(FarmPolicySection.FarmInteruption), FPFarmInterruption.OptFarmIncomeIndemnityPerFPObj.EiId, FPFarmInterruption.OptFarmIncomeIndemnityPerFPObj.OptIndemnityPeriod.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmInterruption.SumInsuredFarmIncomeFPObj != null && FPFarmInterruption.SumInsuredFarmIncomeFPObj.EiId > 0 && FPFarmInterruption.SumInsuredFarmIncomeFPObj.SumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmInterruption.CustomerId, Convert.ToInt32(FarmPolicySection.FarmInteruption), FPFarmInterruption.SumInsuredFarmIncomeFPObj.EiId, FPFarmInterruption.SumInsuredFarmIncomeFPObj.SumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmInterruption.ExcessFarmIncomeFPObj != null && FPFarmInterruption.ExcessFarmIncomeFPObj.EiId > 0 && FPFarmInterruption.ExcessFarmIncomeFPObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmInterruption.CustomerId, Convert.ToInt32(FarmPolicySection.FarmInteruption), FPFarmInterruption.ExcessFarmIncomeFPObj.EiId, FPFarmInterruption.ExcessFarmIncomeFPObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmInterruption.ExpAgistIncomeNextYearFPObj != null && FPFarmInterruption.ExpAgistIncomeNextYearFPObj.EiId > 0 && FPFarmInterruption.ExpAgistIncomeNextYearFPObj.AgistIncomeNextYear != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmInterruption.CustomerId, Convert.ToInt32(FarmPolicySection.FarmInteruption), FPFarmInterruption.ExpAgistIncomeNextYearFPObj.EiId, FPFarmInterruption.ExpAgistIncomeNextYearFPObj.AgistIncomeNextYear.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmInterruption.OptAgistIncomeIndemnityPerFPObj != null && FPFarmInterruption.OptAgistIncomeIndemnityPerFPObj.EiId > 0 && FPFarmInterruption.OptAgistIncomeIndemnityPerFPObj.OptIndemnityPeriod != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmInterruption.CustomerId, Convert.ToInt32(FarmPolicySection.FarmInteruption), FPFarmInterruption.OptAgistIncomeIndemnityPerFPObj.EiId, FPFarmInterruption.OptAgistIncomeIndemnityPerFPObj.OptIndemnityPeriod.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmInterruption.SumInsuredAgistIncomeFPObj != null && FPFarmInterruption.SumInsuredAgistIncomeFPObj.EiId > 0 && FPFarmInterruption.SumInsuredAgistIncomeFPObj.SumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmInterruption.CustomerId, Convert.ToInt32(FarmPolicySection.FarmInteruption), FPFarmInterruption.SumInsuredAgistIncomeFPObj.EiId, FPFarmInterruption.SumInsuredAgistIncomeFPObj.SumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmInterruption.ExcessAgistIncomeFPObj != null && FPFarmInterruption.ExcessAgistIncomeFPObj.EiId > 0 && FPFarmInterruption.ExcessAgistIncomeFPObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmInterruption.CustomerId, Convert.ToInt32(FarmPolicySection.FarmInteruption), FPFarmInterruption.ExcessAgistIncomeFPObj.EiId, FPFarmInterruption.ExcessAgistIncomeFPObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (FPFarmInterruption.OptExtraCostIndemnityPerFPObj != null && FPFarmInterruption.OptExtraCostIndemnityPerFPObj.EiId > 0 && FPFarmInterruption.OptExtraCostIndemnityPerFPObj.OptIndemnityPeriod != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmInterruption.CustomerId, Convert.ToInt32(FarmPolicySection.FarmInteruption), FPFarmInterruption.OptExtraCostIndemnityPerFPObj.EiId, FPFarmInterruption.OptExtraCostIndemnityPerFPObj.OptIndemnityPeriod.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmInterruption.SumInsuredExtraCostFPObj != null && FPFarmInterruption.SumInsuredExtraCostFPObj.EiId > 0 && FPFarmInterruption.SumInsuredExtraCostFPObj.SumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmInterruption.CustomerId, Convert.ToInt32(FarmPolicySection.FarmInteruption), FPFarmInterruption.SumInsuredExtraCostFPObj.EiId, FPFarmInterruption.SumInsuredExtraCostFPObj.SumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmInterruption.ExcessExtraCostFPObj != null && FPFarmInterruption.ExcessExtraCostFPObj.EiId > 0 && FPFarmInterruption.ExcessExtraCostFPObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmInterruption.CustomerId, Convert.ToInt32(FarmPolicySection.FarmInteruption), FPFarmInterruption.ExcessExtraCostFPObj.EiId, FPFarmInterruption.ExcessExtraCostFPObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmInterruption.SumInsuredShearingDelayFPObj != null && FPFarmInterruption.SumInsuredShearingDelayFPObj.EiId > 0 && FPFarmInterruption.SumInsuredShearingDelayFPObj.SumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmInterruption.CustomerId, Convert.ToInt32(FarmPolicySection.FarmInteruption), FPFarmInterruption.SumInsuredShearingDelayFPObj.EiId, FPFarmInterruption.SumInsuredShearingDelayFPObj.SumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPFarmInterruption.ExcessShearingDelayFPObj != null && FPFarmInterruption.ExcessShearingDelayFPObj.EiId > 0 && FPFarmInterruption.ExcessShearingDelayFPObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FPFarmInterruption.CustomerId, Convert.ToInt32(FarmPolicySection.FarmInteruption), FPFarmInterruption.ExcessShearingDelayFPObj.EiId, FPFarmInterruption.ExcessShearingDelayFPObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
            }
            return RedirectToAction("FarmLiability", "FarmPolicyFarmLiability", new { cid = cid });
        }
    }
}