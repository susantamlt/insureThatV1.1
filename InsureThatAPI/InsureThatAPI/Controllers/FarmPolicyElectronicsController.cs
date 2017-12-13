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
    
    public class FarmPolicyElectronicsController : Controller
    {
        // GET: FarmPolicyElectronics
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Electronics(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> ElectronicItemTypeofUnit = new List<SelectListItem>();
            ElectronicItemTypeofUnit = commonModel.ElectronicTypeOfUnit();
            List<SelectListItem> excessToPayElectronics = new List<SelectListItem>();
            excessToPayElectronics = commonModel.excessRate();
            var db = new MasterDataEntities();
            string policyid = null;
            FPElectronics FPElectronics = new FPElectronics();
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    
                        if (Policyincllist.Contains("Electronics"))
                        {
                            return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid });
                        }
                        else{
                        if (Policyincllist.Contains("Money"))
                        {
                            return RedirectToAction("Money", "FarmPolicyMoney", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("Transit"))
                        {
                            return RedirectToAction("Transit", "FarmPolicyTransit", new { cid = cid });
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
                var policyinclusionslist = db.IT_GetPolicyInclusions(cid, policyid, Convert.ToInt32(PolicyType.FarmPolicy)).FirstOrDefault();
                if (policyinclusionslist.PolicyInclusions != null)
                {
                    if (policyinclusionslist.PolicyInclusions.Length > 1)
                    {
                        var policyinclusions = policyinclusionslist.PolicyInclusions.Split('-');
                        if (policyinclusions != null && policyinclusions.Length > 0)
                        {
                            for (int i = 0; i < policyinclusions.Length; i++)
                            {
                                if (i == 5 && policyinclusions[i] == "1")
                                {

                                }
                                else
                                {
                                    if (i == 6 && policyinclusions[i] == "1")
                                    {
                                        return RedirectToAction("Money", "FarmPolicyMoney", new { cid = cid });
                                    }
                                    else if (i == 8 && policyinclusions[i] == "1")
                                    {
                                        return RedirectToAction("Transit", "FarmPolicyTransit", new { cid = cid });
                                    }
                                    else if (i == 9 && policyinclusions[i] == "1")
                                    {
                                        return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid });
                                    }
                                    else if (i == 10 && policyinclusions[i] == "1")
                                    {
                                        return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = cid });
                                    }
                                    else if (i == 11 && policyinclusions[i] == "1")
                                    {
                                        return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid });
                                    }
                                    else if (i == 12 && policyinclusions[i] == "1")
                                    {
                                        return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = cid });
                                    }
                                    else if (i == 13 && policyinclusions[i] == "1")
                                    {
                                        return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid });
                                    }
                                    else if (i == 14 && policyinclusions[i] == "1")
                                    {
                                        //  return RedirectToAction("", "", new { cid = cid });
                                    }
                                    else if (i == 15 && policyinclusions[i] == "1")
                                    {
                                        // return RedirectToAction("", "", new { cid = cid });
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                   return RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = Convert.ToInt32(PolicyType.FarmPolicy) });
                }
            }
            ViewBag.cid = cid;
            if (cid != null)
            {
                FPElectronics.CustomerId = cid.Value;
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

      
            var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(FarmPolicySection.Electronics),Convert.ToInt32(PolicyType.FarmPolicy),policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == FPElectronics.ElectronicsLocationsCoveredFPObj.EiId))
                {
                    FPElectronics.ElectronicsLocationsCoveredFPObj.LocationsCovered = Convert.ToString(details.Where(q => q.QuestionId == FPElectronics.ElectronicsLocationsCoveredFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPElectronics.TypeOfUnitFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPElectronics.TypeOfUnitFPObj.EiId).FirstOrDefault();
                    FPElectronics.TypeOfUnitFPObj.TypeofUnit = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPElectronics.MakeAndModelFPObj.EiId))
                {
                    FPElectronics.MakeAndModelFPObj.MakeandModel = Convert.ToString(details.Where(q => q.QuestionId == FPElectronics.MakeAndModelFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPElectronics.SerialNumberFPObj.EiId))
                {
                    FPElectronics.SerialNumberFPObj.SerialNumber = Convert.ToString(details.Where(q => q.QuestionId == FPElectronics.SerialNumberFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPElectronics.NoOfUnitsFPObj.EiId))
                {
                    FPElectronics.NoOfUnitsFPObj.NoOfUnits = Convert.ToString(details.Where(q => q.QuestionId == FPElectronics.NoOfUnitsFPObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FPElectronics.OptPortableItemsFPObj.EiId))
                {
                    FPElectronics.OptPortableItemsFPObj.PortableItemsOption = Convert.ToString(details.Where(q => q.QuestionId == FPElectronics.OptPortableItemsFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPElectronics.SumInsuredPerUnitFPObj.EiId))
                {
                    FPElectronics.SumInsuredPerUnitFPObj.SumInsuredPerUnit = Convert.ToString(details.Where(q => q.QuestionId == FPElectronics.SumInsuredPerUnitFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPElectronics.TotalSumInsuredFPObj.EiId))
                {
                    FPElectronics.TotalSumInsuredFPObj.TotalSumInsured = Convert.ToString(details.Where(q => q.QuestionId == FPElectronics.TotalSumInsuredFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPElectronics.ExcessElectronicsFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPElectronics.ExcessElectronicsFPObj.EiId).FirstOrDefault();
                    FPElectronics.ExcessElectronicsFPObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPElectronics.CoverLossOfDataFPObj.EiId))
                {
                    FPElectronics.CoverLossOfDataFPObj.CoverLossofData = Convert.ToString(details.Where(q => q.QuestionId == FPElectronics.CoverLossOfDataFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPElectronics.ExcessCoverLossOfDataFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPElectronics.ExcessCoverLossOfDataFPObj.EiId).FirstOrDefault();
                    FPElectronics.ExcessCoverLossOfDataFPObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
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
            if (cid.HasValue && cid > 0)
            {
                if (FPElectronics.ElectronicsLocationsCoveredFPObj!=null && FPElectronics.ElectronicsLocationsCoveredFPObj.EiId > 0 && FPElectronics.ElectronicsLocationsCoveredFPObj.LocationsCovered != null)
                {
                    db.IT_InsertCustomerQnsData(FPElectronics.CustomerId,Convert.ToInt32(FarmPolicySection.Electronics), FPElectronics.ElectronicsLocationsCoveredFPObj.EiId, FPElectronics.ElectronicsLocationsCoveredFPObj.LocationsCovered.ToString(),Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPElectronics.TypeOfUnitFPObj != null && FPElectronics.TypeOfUnitFPObj.EiId > 0 && FPElectronics.TypeOfUnitFPObj.TypeofUnit != null)
                {
                    db.IT_InsertCustomerQnsData(FPElectronics.CustomerId, Convert.ToInt32(FarmPolicySection.Electronics), FPElectronics.TypeOfUnitFPObj.EiId, FPElectronics.TypeOfUnitFPObj.TypeofUnit.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPElectronics.MakeAndModelFPObj != null && FPElectronics.MakeAndModelFPObj.EiId > 0 && FPElectronics.MakeAndModelFPObj.MakeandModel != null)
                {
                    db.IT_InsertCustomerQnsData(FPElectronics.CustomerId, Convert.ToInt32(FarmPolicySection.Electronics), FPElectronics.MakeAndModelFPObj.EiId, FPElectronics.MakeAndModelFPObj.MakeandModel.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPElectronics.SerialNumberFPObj != null && FPElectronics.SerialNumberFPObj.EiId > 0 && FPElectronics.SerialNumberFPObj.SerialNumber != null)
                {
                    db.IT_InsertCustomerQnsData(FPElectronics.CustomerId, Convert.ToInt32(FarmPolicySection.Electronics), FPElectronics.SerialNumberFPObj.EiId, FPElectronics.SerialNumberFPObj.SerialNumber.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPElectronics.NoOfUnitsFPObj != null && FPElectronics.NoOfUnitsFPObj.EiId > 0 && FPElectronics.NoOfUnitsFPObj.NoOfUnits != null)
                {
                    db.IT_InsertCustomerQnsData(FPElectronics.CustomerId, Convert.ToInt32(FarmPolicySection.Electronics), FPElectronics.NoOfUnitsFPObj.EiId, FPElectronics.NoOfUnitsFPObj.NoOfUnits.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPElectronics.OptPortableItemsFPObj != null && FPElectronics.OptPortableItemsFPObj.EiId > 0 && FPElectronics.OptPortableItemsFPObj.PortableItemsOption != null)
                {
                    db.IT_InsertCustomerQnsData(FPElectronics.CustomerId, Convert.ToInt32(FarmPolicySection.Electronics), FPElectronics.OptPortableItemsFPObj.EiId, FPElectronics.OptPortableItemsFPObj.PortableItemsOption.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPElectronics.SumInsuredPerUnitFPObj != null && FPElectronics.SumInsuredPerUnitFPObj.EiId > 0 && FPElectronics.SumInsuredPerUnitFPObj.SumInsuredPerUnit != null)
                {
                    db.IT_InsertCustomerQnsData(FPElectronics.CustomerId, Convert.ToInt32(FarmPolicySection.Electronics), FPElectronics.SumInsuredPerUnitFPObj.EiId, FPElectronics.SumInsuredPerUnitFPObj.SumInsuredPerUnit.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPElectronics.TotalSumInsuredFPObj != null && FPElectronics.TotalSumInsuredFPObj.EiId > 0 && FPElectronics.TotalSumInsuredFPObj.TotalSumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPElectronics.CustomerId, Convert.ToInt32(FarmPolicySection.Electronics), FPElectronics.TotalSumInsuredFPObj.EiId, FPElectronics.TotalSumInsuredFPObj.TotalSumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPElectronics.ExcessElectronicsFPObj != null && FPElectronics.ExcessElectronicsFPObj.EiId > 0 && FPElectronics.ExcessElectronicsFPObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FPElectronics.CustomerId, Convert.ToInt32(FarmPolicySection.Electronics), FPElectronics.ExcessElectronicsFPObj.EiId, FPElectronics.ExcessElectronicsFPObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPElectronics.CoverLossOfDataFPObj != null && FPElectronics.CoverLossOfDataFPObj.EiId > 0 && FPElectronics.CoverLossOfDataFPObj.CoverLossofData != null)
                {
                    db.IT_InsertCustomerQnsData(FPElectronics.CustomerId, Convert.ToInt32(FarmPolicySection.Electronics), FPElectronics.CoverLossOfDataFPObj.EiId, FPElectronics.CoverLossOfDataFPObj.CoverLossofData.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPElectronics.ExcessCoverLossOfDataFPObj != null && FPElectronics.ExcessCoverLossOfDataFPObj.EiId > 0 && FPElectronics.ExcessCoverLossOfDataFPObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FPElectronics.CustomerId, Convert.ToInt32(FarmPolicySection.Electronics), FPElectronics.ExcessCoverLossOfDataFPObj.EiId, FPElectronics.ExcessCoverLossOfDataFPObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

            }
            return View(FPElectronics);
        }
    }
}