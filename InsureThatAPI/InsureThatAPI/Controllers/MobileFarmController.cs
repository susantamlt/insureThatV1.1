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
    public class MobileFarmController : Controller
    {
        // GET: MobileFarm
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FarmContents(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();

            List<SelectListItem> excessToPay = new List<SelectListItem>();
            excessToPay = commonModel.excessRate();

            MobileFarmContents MobileFarmContents = new MobileFarmContents();
            var db = new MasterDataEntities();
            string policyid = null;
            var policyinclusion = db.IT_GetPolicyInclusions(cid, policyid, Convert.ToInt32(PolicyType.FarmPolicy)).FirstOrDefault();
            if (policyinclusion != null && policyinclusion.PolicyInclusions != null)
            {
                MobileFarmContents.PolicyInclusions = policyinclusion.PolicyInclusions;
            }
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Contains("MobileFarmProperty"))
                    {
                    }

                    else
                    {

                        if (Policyincllist.Contains("FixedFarmProperty"))
                        {
                            return RedirectToAction("FarmLocationDetails", "FarmPolicyProperty", new { cid = cid });
                        }
                        else if (Policyincllist.Contains("FarmInteruption"))
                        {
                            return RedirectToAction("FarmInterruption", "FarmPolicyFarmInterruption", new { cid = cid });
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
                            return RedirectToAction("Transit", "FarmPolicyTransit",  new { cid = cid });
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

                if (policyinclusion != null && policyinclusion.PolicyInclusions != null)
                {
                    if (policyinclusion.PolicyInclusions.Length > 1)
                    {
                        var policyinclusions = policyinclusion.PolicyInclusions.Split('-');
                        if (policyinclusions != null && policyinclusions.Length > 0)
                        {

                            if (policyinclusions[0] == "1")
                            {

                            }
                            else
                            {
                                if (policyinclusions[1] == "1")
                                {
                                    return RedirectToAction("FarmLocationDetails", "FarmPolicyProperty", new { cid = cid });
                                }
                                if (policyinclusions[2] == "1")
                                {
                                    return RedirectToAction("FarmInterruption", "FarmPolicyFarmInterruption", new { cid = cid });
                                }
                                if (policyinclusions[3] == "1")
                                {
                                    return RedirectToAction("FarmLiability", "FarmPolicyFarmLiability", new { cid = cid });
                                }
                                if (policyinclusions[4] == "1")
                                {
                                    return RedirectToAction("Burglary", "FarmPolicyBurglary", new { cid = cid });
                                }
                                if (policyinclusions[5] == "1")
                                {
                                    return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid });
                                }
                                if (policyinclusions[6] == "1")
                                {
                                    return RedirectToAction("Money", "FarmPolicyMoney", new { cid = cid });
                                }
                               
                                if (policyinclusions[7] == "1")
                                {
                                    return RedirectToAction("Transit", "FarmPolicyTransit", new { cid = cid });
                                }
                                else if ( policyinclusions[8] == "1")
                                {
                                    return RedirectToAction("Valuables", "FarmPolicyValuables", new { cid = cid });
                                }
                                else if ( policyinclusions[9] == "1")
                                {
                                    return RedirectToAction("Livestock", "FarmPolicyLivestock", new { cid = cid });
                                }
                                else if ( policyinclusions[10] == "1")
                                {
                                    return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid });
                                }
                                else if ( policyinclusions[11] == "1")
                                {
                                    return RedirectToAction("MainDetails", "FarmPolicyHome", new { cid = cid });
                                }
                                else if (policyinclusions[12] == "1")
                                {
                                    return RedirectToAction("HomeContents", "FarmPolicyHomeContent", new { cid = cid });
                                }
                                else if ( policyinclusions[13] == "1")
                                {
                                    //  return RedirectToAction("", "", new { cid = cid });
                                }
                                else if ( policyinclusions[14] == "1")
                                {
                                    // return RedirectToAction("", "", new { cid = cid });
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
                MobileFarmContents.CustomerId = cid.Value;
            }

            MobileFarmContents.FarmContentsSumInsuredFPObj = new FarmContentsSumInsuredFP();
            MobileFarmContents.FarmContentsSumInsuredFPObj.EiId = 62403;

            MobileFarmContents.OptPortableItemsFarmContentFPObj = new OptPortableItemsFarmContentFP();
            MobileFarmContents.OptPortableItemsFarmContentFPObj.EiId = 60405;

            MobileFarmContents.ExcessFarmContentFPObj = new ExcessFarmContentFP();
            MobileFarmContents.ExcessFarmContentFPObj.ExcessList = excessToPay;
            MobileFarmContents.ExcessFarmContentFPObj.EiId = 62407;

            if (Session["CompletionTrackFPC"] != null)
            {
                Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                MobileFarmContents.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }
            else
            {
                Session["CompletionTrackFPC"] = "0-0-0-0"; ;
                MobileFarmContents.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }

            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == MobileFarmContents.FarmContentsSumInsuredFPObj.EiId))
                {
                    MobileFarmContents.FarmContentsSumInsuredFPObj.SumInsuredFC = Convert.ToString(details.Where(q => q.QuestionId == MobileFarmContents.FarmContentsSumInsuredFPObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MobileFarmContents.OptPortableItemsFarmContentFPObj.EiId))
                {
                    MobileFarmContents.OptPortableItemsFarmContentFPObj.OptPortalableItems = Convert.ToString(details.Where(q => q.QuestionId == MobileFarmContents.OptPortableItemsFarmContentFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == MobileFarmContents.ExcessFarmContentFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MobileFarmContents.ExcessFarmContentFPObj.EiId).FirstOrDefault();
                    MobileFarmContents.ExcessFarmContentFPObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                MobileFarmContents.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MobileFarmContents.CustomerId;
            }
            return View(MobileFarmContents);
        }

        [HttpPost]
        public ActionResult FarmContents(MobileFarmContents MobileFarmContents, int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();

            List<SelectListItem> excessToPay = new List<SelectListItem>();
            excessToPay = commonModel.excessRate();
            MobileFarmContents.ExcessFarmContentFPObj.ExcessList = excessToPay;

            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                MobileFarmContents.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MobileFarmContents.CustomerId;
            }
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {

                if (MobileFarmContents.FarmContentsSumInsuredFPObj != null && MobileFarmContents.FarmContentsSumInsuredFPObj.EiId > 0 && MobileFarmContents.FarmContentsSumInsuredFPObj.SumInsuredFC != null)
                {
                    db.IT_InsertCustomerQnsData(MobileFarmContents.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileFarmContents.FarmContentsSumInsuredFPObj.EiId, MobileFarmContents.FarmContentsSumInsuredFPObj.SumInsuredFC.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileFarmContents.OptPortableItemsFarmContentFPObj != null && MobileFarmContents.OptPortableItemsFarmContentFPObj.EiId > 0 && MobileFarmContents.OptPortableItemsFarmContentFPObj.OptPortalableItems != null)
                {
                    db.IT_InsertCustomerQnsData(MobileFarmContents.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileFarmContents.OptPortableItemsFarmContentFPObj.EiId, MobileFarmContents.OptPortableItemsFarmContentFPObj.OptPortalableItems.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (MobileFarmContents.ExcessFarmContentFPObj != null && MobileFarmContents.ExcessFarmContentFPObj.EiId > 0 && MobileFarmContents.ExcessFarmContentFPObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(MobileFarmContents.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileFarmContents.ExcessFarmContentFPObj.EiId, MobileFarmContents.ExcessFarmContentFPObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (Session["CompletionTrackFPC"] != null)
                {
                    Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                    MobileFarmContents.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                    if (MobileFarmContents.CompletionTrackFPC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = MobileFarmContents.CompletionTrackFPC.ToCharArray();
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
                        MobileFarmContents.CompletionTrackFPC = Completionstring;
                    }
                }
                else
                {
                    Session["CompletionTrackFPC"] = "1-0-0-0"; ;
                    MobileFarmContents.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                }
                return RedirectToAction("FarmMachinery", new { cid = MobileFarmContents.CustomerId });
            }

            return View(MobileFarmContents);
        }

        [HttpGet]
        public ActionResult FarmMachinery(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();

            List<SelectListItem> excessforUMPay = new List<SelectListItem>();
            excessforUMPay = commonModel.excessRate();

            List<SelectListItem> excessToPay = new List<SelectListItem>();
            excessToPay = commonModel.excessRate();

            MobileFarmMachinery MobileFarmMachinery = new MobileFarmMachinery();
            if (cid != null)
            {

                ViewBag.cid = cid;
                MobileFarmMachinery.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MobileFarmMachinery.CustomerId;
            }
            MobileFarmMachinery.FPUnspecifiedMachineryFMObj = new FPUnspecifiedMachineryFM();
            MobileFarmMachinery.FPUnspecifiedMachineryFMObj.EiId = 62423;

            MobileFarmMachinery.FPExcessforUMFMObj = new FPExcessforUMFM();
            MobileFarmMachinery.FPExcessforUMFMObj.ExcessUMList = excessforUMPay;
            MobileFarmMachinery.FPExcessforUMFMObj.EiId = 62425;

            MobileFarmMachinery.FPDescriptionsFMObj = new FPDescriptionsFM();
            MobileFarmMachinery.FPDescriptionsFMObj.EiId = 62431;

            MobileFarmMachinery.FPYearFMObj = new FPYearFM();
            MobileFarmMachinery.FPYearFMObj.EiId = 62433;

            MobileFarmMachinery.FPSerialNumberFMObj = new FPSerialNumberFM();
            MobileFarmMachinery.FPSerialNumberFMObj.EiId = 62435;

            MobileFarmMachinery.FPExcessFMObj = new FPExcessFM();
            MobileFarmMachinery.FPExcessFMObj.ExcessList = excessToPay;
            MobileFarmMachinery.FPExcessFMObj.EiId = 62437;

            MobileFarmMachinery.FPSumOfInsuredFMObj = new FPSumOfInsuredFM();
            MobileFarmMachinery.FPSumOfInsuredFMObj.EiId = 62439;
            var db = new MasterDataEntities();
            string policyid = null;
            var policyinclusion = db.IT_GetPolicyInclusions(cid, policyid, Convert.ToInt32(PolicyType.FarmPolicy)).FirstOrDefault();
            if (policyinclusion != null && policyinclusion.PolicyInclusions != null)
            {
                MobileFarmMachinery.PolicyInclusions = policyinclusion.PolicyInclusions;
            }
            if (Session["CompletionTrackFPC"] != null)
            {
                Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                MobileFarmMachinery.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }
            else
            {
                Session["CompletionTrackFPC"] = "0-0-0-0"; ;
                MobileFarmMachinery.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }
        
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == MobileFarmMachinery.FPUnspecifiedMachineryFMObj.EiId))
                {
                    MobileFarmMachinery.FPUnspecifiedMachineryFMObj.UnspecifiedMachinery = Convert.ToString(details.Where(q => q.QuestionId == MobileFarmMachinery.FPUnspecifiedMachineryFMObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MobileFarmMachinery.FPExcessforUMFMObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MobileFarmMachinery.FPExcessforUMFMObj.EiId).FirstOrDefault();
                    MobileFarmMachinery.FPExcessforUMFMObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == MobileFarmMachinery.FPDescriptionsFMObj.EiId))
                {
                    MobileFarmMachinery.FPDescriptionsFMObj.DescriptionFM = Convert.ToString(details.Where(q => q.QuestionId == MobileFarmMachinery.FPDescriptionsFMObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MobileFarmMachinery.FPYearFMObj.EiId))
                {
                    MobileFarmMachinery.FPYearFMObj.YearFM = Convert.ToString(details.Where(q => q.QuestionId == MobileFarmMachinery.FPYearFMObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MobileFarmMachinery.FPSerialNumberFMObj.EiId))
                {
                    MobileFarmMachinery.FPSerialNumberFMObj.SerialNumberFM = Convert.ToString(details.Where(q => q.QuestionId == MobileFarmMachinery.FPSerialNumberFMObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MobileFarmMachinery.FPExcessFMObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MobileFarmMachinery.FPExcessFMObj.EiId).FirstOrDefault();
                    MobileFarmMachinery.FPExcessFMObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == MobileFarmMachinery.FPSumOfInsuredFMObj.EiId))
                {
                    MobileFarmMachinery.FPSumOfInsuredFMObj.SuminsuredFM = Convert.ToString(details.Where(q => q.QuestionId == MobileFarmMachinery.FPSumOfInsuredFMObj.EiId).FirstOrDefault().Answer);
                }
            }
            return View(MobileFarmMachinery);
        }

        [HttpPost]
        public ActionResult FarmMachinery(MobileFarmMachinery MobileFarmMachinery, int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();

            List<SelectListItem> excessforUMPay = new List<SelectListItem>();
            excessforUMPay = commonModel.excessRate();

            List<SelectListItem> excessToPay = new List<SelectListItem>();
            excessToPay = commonModel.excessRate();

            if (cid != null)
            {
                ViewBag.cid = cid;
                MobileFarmMachinery.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MobileFarmMachinery.CustomerId;
            }
            MobileFarmMachinery.FPExcessforUMFMObj.ExcessUMList = excessforUMPay;
            MobileFarmMachinery.FPExcessFMObj.ExcessList = excessToPay;
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (MobileFarmMachinery.FPUnspecifiedMachineryFMObj != null && MobileFarmMachinery.FPUnspecifiedMachineryFMObj.EiId > 0 && MobileFarmMachinery.FPUnspecifiedMachineryFMObj.UnspecifiedMachinery != null)
                {
                    db.IT_InsertCustomerQnsData(MobileFarmMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileFarmMachinery.FPUnspecifiedMachineryFMObj.EiId, MobileFarmMachinery.FPUnspecifiedMachineryFMObj.UnspecifiedMachinery.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileFarmMachinery.FPExcessforUMFMObj != null && MobileFarmMachinery.FPExcessforUMFMObj.EiId > 0 && MobileFarmMachinery.FPExcessforUMFMObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(MobileFarmMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileFarmMachinery.FPExcessforUMFMObj.EiId, MobileFarmMachinery.FPExcessforUMFMObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileFarmMachinery.FPDescriptionsFMObj != null && MobileFarmMachinery.FPDescriptionsFMObj.EiId > 0 && MobileFarmMachinery.FPDescriptionsFMObj.DescriptionFM != null)
                {
                    db.IT_InsertCustomerQnsData(MobileFarmMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileFarmMachinery.FPDescriptionsFMObj.EiId, MobileFarmMachinery.FPDescriptionsFMObj.DescriptionFM.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileFarmMachinery.FPYearFMObj != null && MobileFarmMachinery.FPYearFMObj.EiId > 0 && MobileFarmMachinery.FPYearFMObj.YearFM != null)
                {
                    db.IT_InsertCustomerQnsData(MobileFarmMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileFarmMachinery.FPYearFMObj.EiId, MobileFarmMachinery.FPYearFMObj.YearFM.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileFarmMachinery.FPSerialNumberFMObj != null && MobileFarmMachinery.FPSerialNumberFMObj.EiId > 0 && MobileFarmMachinery.FPSerialNumberFMObj.SerialNumberFM != null)
                {
                    db.IT_InsertCustomerQnsData(MobileFarmMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileFarmMachinery.FPSerialNumberFMObj.EiId, MobileFarmMachinery.FPSerialNumberFMObj.SerialNumberFM.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileFarmMachinery.FPExcessFMObj != null && MobileFarmMachinery.FPExcessFMObj.EiId > 0 && MobileFarmMachinery.FPExcessFMObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(MobileFarmMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileFarmMachinery.FPExcessFMObj.EiId, MobileFarmMachinery.FPExcessFMObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileFarmMachinery.FPSumOfInsuredFMObj != null && MobileFarmMachinery.FPSumOfInsuredFMObj.EiId > 0 && MobileFarmMachinery.FPSumOfInsuredFMObj.SuminsuredFM != null)
                {
                    db.IT_InsertCustomerQnsData(MobileFarmMachinery.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileFarmMachinery.FPSumOfInsuredFMObj.EiId, MobileFarmMachinery.FPSumOfInsuredFMObj.SuminsuredFM.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (Session["CompletionTrackFPC"] != null)
                {
                    Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                    MobileFarmMachinery.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                    if (MobileFarmMachinery.CompletionTrackFPC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = MobileFarmMachinery.CompletionTrackFPC.ToCharArray();

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
                        MobileFarmMachinery.CompletionTrackFPC = Completionstring;
                    }
                }
                else
                {
                    Session["CompletionTrackFPC"] = "0-1-0-0"; ;
                    MobileFarmMachinery.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                }
                return RedirectToAction("Livestock", new { cid = MobileFarmMachinery.CustomerId });
            }
            return View(MobileFarmMachinery);
        }

        [HttpGet]
        public ActionResult Livestock(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> desList = new List<SelectListItem>();
            desList = commonModel.descriptionLS();

            List<SelectListItem> excessforUMPay = new List<SelectListItem>();
            excessforUMPay = commonModel.excessRate();

            MobileLiveStock MobileLiveStock = new MobileLiveStock();

            if (cid != null)
            {
                ViewBag.cid = cid;
                MobileLiveStock.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MobileLiveStock.CustomerId;
            }
            MobileLiveStock.FPDescriptionLivestockObj = new FPDescriptionLivestock();
            MobileLiveStock.FPDescriptionLivestockObj.DescriptionList = desList;
            MobileLiveStock.FPDescriptionLivestockObj.EiId = 62467;

            MobileLiveStock.FPNumberOfAnimalsLivestockObj = new FPNumberOfAnimalsLivestock();
            MobileLiveStock.FPNumberOfAnimalsLivestockObj.EiId = 62469;

            MobileLiveStock.FPSumInsuredPerAnimalsLivestockObj = new FPSumInsuredPerAnimalsLivestock();
            MobileLiveStock.FPSumInsuredPerAnimalsLivestockObj.EiId = 62471;

            MobileLiveStock.FPTotalSumOfInsuredLivestockObj = new FPTotalSumOfInsuredLivestock();
            MobileLiveStock.FPTotalSumOfInsuredLivestockObj.EiId = 62473;

            MobileLiveStock.OptDogAttackLivestockObj = new OptDogAttackLivestock();
            MobileLiveStock.OptDogAttackLivestockObj.EiId = 62475;

            MobileLiveStock.FPExcessLivestockObj = new FPExcessLivestock();
            MobileLiveStock.FPExcessLivestockObj.ExcessList = excessforUMPay;
            MobileLiveStock.FPExcessLivestockObj.EiId = 62477;

            if (Session["CompletionTrackFPC"] != null)
            {
                Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                MobileLiveStock.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }
            else
            {
                Session["CompletionTrackFPC"] = "0-0-0-0"; ;
                MobileLiveStock.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == MobileLiveStock.FPDescriptionLivestockObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MobileLiveStock.FPDescriptionLivestockObj.EiId).FirstOrDefault();
                    MobileLiveStock.FPDescriptionLivestockObj.Description = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == MobileLiveStock.FPNumberOfAnimalsLivestockObj.EiId))
                {
                    MobileLiveStock.FPNumberOfAnimalsLivestockObj.NumberOfanimals = Convert.ToString(details.Where(q => q.QuestionId == MobileLiveStock.FPNumberOfAnimalsLivestockObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MobileLiveStock.FPSumInsuredPerAnimalsLivestockObj.EiId))
                {
                    MobileLiveStock.FPSumInsuredPerAnimalsLivestockObj.SumInsuredPerAnimal = Convert.ToString(details.Where(q => q.QuestionId == MobileLiveStock.FPSumInsuredPerAnimalsLivestockObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MobileLiveStock.FPTotalSumOfInsuredLivestockObj.EiId))
                {
                    MobileLiveStock.FPTotalSumOfInsuredLivestockObj.TotalSumOfInsured = Convert.ToString(details.Where(q => q.QuestionId == MobileLiveStock.FPTotalSumOfInsuredLivestockObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MobileLiveStock.OptDogAttackLivestockObj.EiId))
                {
                    MobileLiveStock.OptDogAttackLivestockObj.OptDogAttack = Convert.ToString(details.Where(q => q.QuestionId == MobileLiveStock.OptDogAttackLivestockObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MobileLiveStock.FPExcessLivestockObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MobileLiveStock.FPExcessLivestockObj.EiId).FirstOrDefault();
                    MobileLiveStock.FPExcessLivestockObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
            }
            return View(MobileLiveStock);
        }

        [HttpPost]
        public ActionResult Livestock(MobileLiveStock MobileLiveStock, int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> desList = new List<SelectListItem>();
            desList = commonModel.descriptionLS();
            List<SelectListItem> excessforUMPay = new List<SelectListItem>();
            excessforUMPay = commonModel.excessRate();
            MobileLiveStock.FPDescriptionLivestockObj.DescriptionList = desList;
            MobileLiveStock.FPExcessLivestockObj.ExcessList = excessforUMPay;
            if (cid != null)
            {
                ViewBag.cid = cid;
                MobileLiveStock.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MobileLiveStock.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (MobileLiveStock.FPDescriptionLivestockObj != null && MobileLiveStock.FPDescriptionLivestockObj.EiId > 0 && MobileLiveStock.FPDescriptionLivestockObj.Description != null)
                {
                    db.IT_InsertCustomerQnsData(MobileLiveStock.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileLiveStock.FPDescriptionLivestockObj.EiId, MobileLiveStock.FPDescriptionLivestockObj.Description.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileLiveStock.FPNumberOfAnimalsLivestockObj != null && MobileLiveStock.FPNumberOfAnimalsLivestockObj.EiId > 0 && MobileLiveStock.FPNumberOfAnimalsLivestockObj.NumberOfanimals != null)
                {
                    db.IT_InsertCustomerQnsData(MobileLiveStock.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileLiveStock.FPNumberOfAnimalsLivestockObj.EiId, MobileLiveStock.FPNumberOfAnimalsLivestockObj.NumberOfanimals.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileLiveStock.FPSumInsuredPerAnimalsLivestockObj != null && MobileLiveStock.FPSumInsuredPerAnimalsLivestockObj.EiId > 0 && MobileLiveStock.FPSumInsuredPerAnimalsLivestockObj.SumInsuredPerAnimal != null)
                {
                    db.IT_InsertCustomerQnsData(MobileLiveStock.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileLiveStock.FPSumInsuredPerAnimalsLivestockObj.EiId, MobileLiveStock.FPSumInsuredPerAnimalsLivestockObj.SumInsuredPerAnimal.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileLiveStock.FPTotalSumOfInsuredLivestockObj != null && MobileLiveStock.FPTotalSumOfInsuredLivestockObj.EiId > 0 && MobileLiveStock.FPTotalSumOfInsuredLivestockObj.TotalSumOfInsured != null)
                {
                    db.IT_InsertCustomerQnsData(MobileLiveStock.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileLiveStock.FPTotalSumOfInsuredLivestockObj.EiId, MobileLiveStock.FPTotalSumOfInsuredLivestockObj.TotalSumOfInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileLiveStock.OptDogAttackLivestockObj != null && MobileLiveStock.OptDogAttackLivestockObj.EiId > 0 && MobileLiveStock.OptDogAttackLivestockObj.OptDogAttack != null)
                {
                    db.IT_InsertCustomerQnsData(MobileLiveStock.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileLiveStock.OptDogAttackLivestockObj.EiId, MobileLiveStock.OptDogAttackLivestockObj.OptDogAttack.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileLiveStock.FPExcessLivestockObj != null && MobileLiveStock.FPExcessLivestockObj.EiId > 0 && MobileLiveStock.FPExcessLivestockObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(MobileLiveStock.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileLiveStock.FPExcessLivestockObj.EiId, MobileLiveStock.FPExcessLivestockObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (Session["CompletionTrackFPC"] != null)
                {
                    Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                    MobileLiveStock.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                    if (MobileLiveStock.CompletionTrackFPC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = MobileLiveStock.CompletionTrackFPC.ToCharArray();

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
                        MobileLiveStock.CompletionTrackFPC = Completionstring;
                    }
                }
                else
                {
                    Session["CompletionTrackFPC"] = "0-0-1-0"; ;
                    MobileLiveStock.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                }
                return RedirectToAction("WorkingDogsBeehives", new { cid = MobileLiveStock.CustomerId });
            }
            return View(MobileLiveStock);
        }

        [HttpGet]
        public ActionResult WorkingDogsBeehives(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> excessforUMPay = new List<SelectListItem>();
            excessforUMPay = commonModel.excessRate();

            MobileWorkingDogsBeehives MobileWorkingDogsBeehives = new MobileWorkingDogsBeehives();

            MobileWorkingDogsBeehives.FPSumOfInsuredPerDogObj = new FPSumOfInsuredPerDog();
            MobileWorkingDogsBeehives.FPSumOfInsuredPerDogObj.EiId = 62491;

            MobileWorkingDogsBeehives.FPNoOfWorkingDogsObj = new FPNoOfWorkingDogs();
            MobileWorkingDogsBeehives.FPNoOfWorkingDogsObj.EiId = 62493;

            MobileWorkingDogsBeehives.FPTotalSumInsuredWDBObj = new FPTotalSumInsuredWDB();
            MobileWorkingDogsBeehives.FPTotalSumInsuredWDBObj.EiId = 62495;

            MobileWorkingDogsBeehives.FPExcessWorkingDogsObj = new FPExcessWorkingDogs();
            MobileWorkingDogsBeehives.FPExcessWorkingDogsObj.ExcessList = excessforUMPay;
            MobileWorkingDogsBeehives.FPExcessWorkingDogsObj.EiId = 62497;

            MobileWorkingDogsBeehives.FPBeehivesSumInsuredObj = new FPBeehivesSumInsured();
            MobileWorkingDogsBeehives.FPBeehivesSumInsuredObj.EiId = 62503;

            MobileWorkingDogsBeehives.FPNumberOfHivesObj = new FPNumberOfHives();
            MobileWorkingDogsBeehives.FPNumberOfHivesObj.EiId = 62505;

            MobileWorkingDogsBeehives.FPExcessBeehivesObj = new FPExcessBeehives();
            MobileWorkingDogsBeehives.FPExcessBeehivesObj.ExcessList = excessforUMPay;
            MobileWorkingDogsBeehives.FPExcessBeehivesObj.EiId = 62507;

            if (cid != null)
            {
                ViewBag.cid = cid;
                MobileWorkingDogsBeehives.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MobileWorkingDogsBeehives.CustomerId;
            }
            if (Session["CompletionTrackFPC"] != null)
            {
                Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                MobileWorkingDogsBeehives.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }
            else
            {
                Session["CompletionTrackFPC"] = "0-0-0-0"; ;
                MobileWorkingDogsBeehives.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == MobileWorkingDogsBeehives.FPSumOfInsuredPerDogObj.EiId))
                {
                    MobileWorkingDogsBeehives.FPSumOfInsuredPerDogObj.SumInsuredPerDog = Convert.ToString(details.Where(q => q.QuestionId == MobileWorkingDogsBeehives.FPSumOfInsuredPerDogObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MobileWorkingDogsBeehives.FPNoOfWorkingDogsObj.EiId))
                {
                    MobileWorkingDogsBeehives.FPNoOfWorkingDogsObj.NoOfWorkingDogs = Convert.ToString(details.Where(q => q.QuestionId == MobileWorkingDogsBeehives.FPNoOfWorkingDogsObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MobileWorkingDogsBeehives.FPTotalSumInsuredWDBObj.EiId))
                {
                    MobileWorkingDogsBeehives.FPTotalSumInsuredWDBObj.TotalSumInsured = Convert.ToString(details.Where(q => q.QuestionId == MobileWorkingDogsBeehives.FPTotalSumInsuredWDBObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MobileWorkingDogsBeehives.FPExcessWorkingDogsObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MobileWorkingDogsBeehives.FPExcessWorkingDogsObj.EiId).FirstOrDefault();
                    MobileWorkingDogsBeehives.FPExcessWorkingDogsObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == MobileWorkingDogsBeehives.FPBeehivesSumInsuredObj.EiId))
                {
                    MobileWorkingDogsBeehives.FPBeehivesSumInsuredObj.BeehivesSumInsured = Convert.ToString(details.Where(q => q.QuestionId == MobileWorkingDogsBeehives.FPBeehivesSumInsuredObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MobileWorkingDogsBeehives.FPNumberOfHivesObj.EiId))
                {
                    MobileWorkingDogsBeehives.FPNumberOfHivesObj.NumberOfHives = Convert.ToString(details.Where(q => q.QuestionId == MobileWorkingDogsBeehives.FPNumberOfHivesObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MobileWorkingDogsBeehives.FPExcessBeehivesObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MobileWorkingDogsBeehives.FPExcessBeehivesObj.EiId).FirstOrDefault();
                    MobileWorkingDogsBeehives.FPExcessBeehivesObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
            }
            return View(MobileWorkingDogsBeehives);
        }

        [HttpPost]
        public ActionResult WorkingDogsBeehives(MobileWorkingDogsBeehives MobileWorkingDogsBeehives, int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> excessforUMPay = new List<SelectListItem>();
            excessforUMPay = commonModel.excessRate();

            MobileWorkingDogsBeehives.FPExcessBeehivesObj.ExcessList = excessforUMPay;
            MobileWorkingDogsBeehives.FPExcessWorkingDogsObj.ExcessList = excessforUMPay;
            if (cid != null)
            {
                ViewBag.cid = cid;
                MobileWorkingDogsBeehives.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MobileWorkingDogsBeehives.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (MobileWorkingDogsBeehives.FPSumOfInsuredPerDogObj != null && MobileWorkingDogsBeehives.FPSumOfInsuredPerDogObj.EiId > 0 && MobileWorkingDogsBeehives.FPSumOfInsuredPerDogObj.SumInsuredPerDog != null)
                {
                    db.IT_InsertCustomerQnsData(MobileWorkingDogsBeehives.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileWorkingDogsBeehives.FPSumOfInsuredPerDogObj.EiId, MobileWorkingDogsBeehives.FPSumOfInsuredPerDogObj.SumInsuredPerDog.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileWorkingDogsBeehives.FPNoOfWorkingDogsObj != null && MobileWorkingDogsBeehives.FPNoOfWorkingDogsObj.EiId > 0 && MobileWorkingDogsBeehives.FPNoOfWorkingDogsObj.NoOfWorkingDogs != null)
                {
                    db.IT_InsertCustomerQnsData(MobileWorkingDogsBeehives.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileWorkingDogsBeehives.FPNoOfWorkingDogsObj.EiId, MobileWorkingDogsBeehives.FPNoOfWorkingDogsObj.NoOfWorkingDogs.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileWorkingDogsBeehives.FPTotalSumInsuredWDBObj != null && MobileWorkingDogsBeehives.FPTotalSumInsuredWDBObj.EiId > 0 && MobileWorkingDogsBeehives.FPTotalSumInsuredWDBObj.TotalSumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(MobileWorkingDogsBeehives.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileWorkingDogsBeehives.FPTotalSumInsuredWDBObj.EiId, MobileWorkingDogsBeehives.FPTotalSumInsuredWDBObj.TotalSumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileWorkingDogsBeehives.FPExcessWorkingDogsObj != null && MobileWorkingDogsBeehives.FPExcessWorkingDogsObj.EiId > 0 && MobileWorkingDogsBeehives.FPExcessWorkingDogsObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(MobileWorkingDogsBeehives.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileWorkingDogsBeehives.FPExcessWorkingDogsObj.EiId, MobileWorkingDogsBeehives.FPExcessWorkingDogsObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileWorkingDogsBeehives.FPBeehivesSumInsuredObj != null && MobileWorkingDogsBeehives.FPBeehivesSumInsuredObj.EiId > 0 && MobileWorkingDogsBeehives.FPBeehivesSumInsuredObj.BeehivesSumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(MobileWorkingDogsBeehives.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileWorkingDogsBeehives.FPBeehivesSumInsuredObj.EiId, MobileWorkingDogsBeehives.FPBeehivesSumInsuredObj.BeehivesSumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileWorkingDogsBeehives.FPNumberOfHivesObj != null && MobileWorkingDogsBeehives.FPNumberOfHivesObj.EiId > 0 && MobileWorkingDogsBeehives.FPNumberOfHivesObj.NumberOfHives != null)
                {
                    db.IT_InsertCustomerQnsData(MobileWorkingDogsBeehives.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileWorkingDogsBeehives.FPNumberOfHivesObj.EiId, MobileWorkingDogsBeehives.FPNumberOfHivesObj.NumberOfHives.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (MobileWorkingDogsBeehives.FPExcessBeehivesObj != null && MobileWorkingDogsBeehives.FPExcessBeehivesObj.EiId > 0 && MobileWorkingDogsBeehives.FPExcessBeehivesObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(MobileWorkingDogsBeehives.CustomerId, Convert.ToInt32(FarmPolicySection.MobileFarmProperty), MobileWorkingDogsBeehives.FPExcessBeehivesObj.EiId, MobileWorkingDogsBeehives.FPExcessBeehivesObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (Session["CompletionTrackFPC"] != null)
                {
                    Session["CompletionTrackFPC"] = Session["CompletionTrackFPC"];
                    MobileWorkingDogsBeehives.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                    if (MobileWorkingDogsBeehives.CompletionTrackFPC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = MobileWorkingDogsBeehives.CompletionTrackFPC.ToCharArray();

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
                        MobileWorkingDogsBeehives.CompletionTrackFPC = Completionstring;
                    }
                }
                else
                {
                    Session["CompletionTrackFPC"] = "0-0-0-1"; ;
                    MobileWorkingDogsBeehives.CompletionTrackFPC = Session["CompletionTrackFPC"].ToString();
                }
                return RedirectToAction("FarmLocationDetails", "FarmPolicyProperty", new { cid = MobileWorkingDogsBeehives.CustomerId });
            }
            return View(MobileWorkingDogsBeehives);
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