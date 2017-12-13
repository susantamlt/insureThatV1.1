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
    public class FarmPolicyBurglaryController : Controller
    {
        // GET: FarmPolicyBurglary
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Burglary(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> BurglaryExcessToPay = new List<SelectListItem>();
            BurglaryExcessToPay = commonModel.excessRate();


            FPBurglary FPBurglary = new FPBurglary();
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Contains("Burglary"))
                    {
                       
                    }
                    else
                    {
                        if (Policyincllist.Contains("Electronics"))
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
                FPBurglary.CustomerId = cid.Value;
            }
            FPBurglary.CoverYourPropOptionFPObj = new CoverYourPropOptionFP();
            FPBurglary.CoverYourPropOptionFPObj.EiId = 62581;

            FPBurglary.FarmBuildingExCoolRoomsFPObj = new FarmBuildingExcCoolRoomsFP();
            FPBurglary.FarmBuildingExCoolRoomsFPObj.EiId = 62585;

            FPBurglary.CoolRoomsFPObj = new CoolRoomsFP();
            FPBurglary.CoolRoomsFPObj.EiId = 62587;

            FPBurglary.OtherFarmStructuresFPObj = new OtherFarmStructuresFP();
            FPBurglary.OtherFarmStructuresFPObj.EiId = 62591;

            FPBurglary.FarmContentsFPObj = new FarmContentsFP();
            FPBurglary.FarmContentsFPObj.EiId = 62595;

            FPBurglary.HailNettingStoredFPObj = new HailNettingStoredFP();
            FPBurglary.HailNettingStoredFPObj.EiId = 62597;

            FPBurglary.UnspecifiedMachineryFPObj = new UnspecifiedMachineryFP();
            FPBurglary.UnspecifiedMachineryFPObj.EiId = 62609;

            FPBurglary.SpecifiedItemsOver5KFPObj = new SpecifiedItemsOver5KFP();
            FPBurglary.SpecifiedItemsOver5KFPObj.EiId = 62613;

            FPBurglary.ExcessFPBurglaryObj = new ExcessFPBurglary();
            FPBurglary.ExcessFPBurglaryObj.ExcessList = BurglaryExcessToPay;
            FPBurglary.ExcessFPBurglaryObj.EiId = 62615;

            FPBurglary.OptFPCoverTheftFSAndFCObj = new FPCoverTheftFSAndFC();
            FPBurglary.OptFPCoverTheftFSAndFCObj.EiId = 62619;

            FPBurglary.OptFPCoverTheftFarmMachineryObj = new FPCoverTheftFarmMachinery();
            FPBurglary.OptFPCoverTheftFarmMachineryObj.EiId = 62621;

            FPBurglary.OptFPPortalableItemsOptObj = new FPPortalableItemsOpt();
            FPBurglary.OptFPPortalableItemsOptObj.EiId = 62623;

            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(FarmPolicySection.Burglary),Convert.ToInt32(PolicyType.FarmPolicy),policyid).ToList();
            if (details != null && details.Any())
            {


                if (details.Exists(q => q.QuestionId == FPBurglary.CoverYourPropOptionFPObj.EiId))
                {
                    FPBurglary.CoverYourPropOptionFPObj.Cover = Convert.ToString(details.Where(q => q.QuestionId == FPBurglary.CoverYourPropOptionFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPBurglary.FarmBuildingExCoolRoomsFPObj.EiId))
                {
                    FPBurglary.FarmBuildingExCoolRoomsFPObj.FarmBuildingExcCoolRooms = Convert.ToString(details.Where(q => q.QuestionId == FPBurglary.FarmBuildingExCoolRoomsFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPBurglary.CoolRoomsFPObj.EiId))
                {
                    FPBurglary.CoolRoomsFPObj.CoolRooms = Convert.ToString(details.Where(q => q.QuestionId == FPBurglary.CoolRoomsFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPBurglary.FarmFencingFPObj.EiId))
                {
                    FPBurglary.FarmFencingFPObj.FarmFencing = Convert.ToString(details.Where(q => q.QuestionId == FPBurglary.FarmFencingFPObj.EiId).FirstOrDefault().Answer);
                }


                if (details.Exists(q => q.QuestionId == FPBurglary.OtherFarmStructuresFPObj.EiId))
                {
                    FPBurglary.OtherFarmStructuresFPObj.OtherFarmStructures = Convert.ToString(details.Where(q => q.QuestionId == FPBurglary.OtherFarmStructuresFPObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FPBurglary.FarmContentsFPObj.EiId))
                {
                    FPBurglary.FarmContentsFPObj.FarmContents = Convert.ToString(details.Where(q => q.QuestionId == FPBurglary.FarmContentsFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPBurglary.HailNettingStoredFPObj.EiId))
                {
                    FPBurglary.HailNettingStoredFPObj.HailNettingStored = Convert.ToString(details.Where(q => q.QuestionId == FPBurglary.HailNettingStoredFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPBurglary.UnspecifiedMachineryFPObj.EiId))
                {
                    FPBurglary.UnspecifiedMachineryFPObj.UnspecifiedMachinery = Convert.ToString(details.Where(q => q.QuestionId == FPBurglary.UnspecifiedMachineryFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPBurglary.SpecifiedItemsOver5KFPObj.EiId))
                {
                    FPBurglary.SpecifiedItemsOver5KFPObj.SpecifiedItemOver5K = Convert.ToString(details.Where(q => q.QuestionId == FPBurglary.SpecifiedItemsOver5KFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPBurglary.ExcessFPBurglaryObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPBurglary.ExcessFPBurglaryObj.EiId).FirstOrDefault();
                    FPBurglary.ExcessFPBurglaryObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPBurglary.OptFPCoverTheftFSAndFCObj.EiId))
                {
                    FPBurglary.OptFPCoverTheftFSAndFCObj.CoverTheftFSandFC = Convert.ToString(details.Where(q => q.QuestionId == FPBurglary.OptFPCoverTheftFSAndFCObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPBurglary.OptFPCoverTheftFarmMachineryObj.EiId))
                {
                    FPBurglary.OptFPCoverTheftFarmMachineryObj.CoverTheftFarmMachinery = Convert.ToString(details.Where(q => q.QuestionId == FPBurglary.OptFPCoverTheftFarmMachineryObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPBurglary.OptFPPortalableItemsOptObj.EiId))
                {
                    FPBurglary.OptFPPortalableItemsOptObj.PortalbleItemsOpt = Convert.ToString(details.Where(q => q.QuestionId == FPBurglary.OptFPPortalableItemsOptObj.EiId).FirstOrDefault().Answer);
                }

            }

            return View(FPBurglary);
        }

        [HttpPost]
        public ActionResult Burglary(int? cid, FPBurglary FPBurglary)
        {

            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> BurglaryExcessToPay = new List<SelectListItem>();
            BurglaryExcessToPay = commonModel.excessRate();
            FPBurglary.ExcessFPBurglaryObj.ExcessList = BurglaryExcessToPay;
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPBurglary.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPBurglary.CustomerId;
            }
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {

                if (FPBurglary.CoverYourPropOptionFPObj != null && FPBurglary.CoverYourPropOptionFPObj.EiId > 0 && FPBurglary.CoverYourPropOptionFPObj.Cover != null)
                {
                    db.IT_InsertCustomerQnsData(FPBurglary.CustomerId, Convert.ToInt32(FarmPolicySection.Burglary), FPBurglary.CoverYourPropOptionFPObj.EiId, FPBurglary.CoverYourPropOptionFPObj.Cover.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPBurglary.FarmBuildingExCoolRoomsFPObj != null && FPBurglary.FarmBuildingExCoolRoomsFPObj.EiId > 0 && FPBurglary.FarmBuildingExCoolRoomsFPObj.FarmBuildingExcCoolRooms != null)
                {
                    db.IT_InsertCustomerQnsData(FPBurglary.CustomerId, Convert.ToInt32(FarmPolicySection.Burglary), FPBurglary.FarmBuildingExCoolRoomsFPObj.EiId, FPBurglary.FarmBuildingExCoolRoomsFPObj.FarmBuildingExcCoolRooms.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPBurglary.CoolRoomsFPObj != null && FPBurglary.CoolRoomsFPObj.EiId > 0 && FPBurglary.CoolRoomsFPObj.CoolRooms != null)
                {
                    db.IT_InsertCustomerQnsData(FPBurglary.CustomerId, Convert.ToInt32(FarmPolicySection.Burglary), FPBurglary.CoolRoomsFPObj.EiId, FPBurglary.CoolRoomsFPObj.CoolRooms.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPBurglary.FarmFencingFPObj != null && FPBurglary.FarmFencingFPObj.EiId > 0 && FPBurglary.FarmFencingFPObj.FarmFencing != null)
                {
                    db.IT_InsertCustomerQnsData(FPBurglary.CustomerId, Convert.ToInt32(FarmPolicySection.Burglary), FPBurglary.FarmFencingFPObj.EiId, FPBurglary.FarmFencingFPObj.FarmFencing.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPBurglary.OtherFarmStructuresFPObj != null && FPBurglary.OtherFarmStructuresFPObj.EiId > 0 && FPBurglary.OtherFarmStructuresFPObj.OtherFarmStructures != null)
                {
                    db.IT_InsertCustomerQnsData(FPBurglary.CustomerId, Convert.ToInt32(FarmPolicySection.Burglary), FPBurglary.OtherFarmStructuresFPObj.EiId, FPBurglary.OtherFarmStructuresFPObj.OtherFarmStructures.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPBurglary.FarmContentsFPObj != null && FPBurglary.FarmContentsFPObj.EiId > 0 && FPBurglary.FarmContentsFPObj.FarmContents != null)
                {
                    db.IT_InsertCustomerQnsData(FPBurglary.CustomerId, Convert.ToInt32(FarmPolicySection.Burglary), FPBurglary.FarmContentsFPObj.EiId, FPBurglary.FarmContentsFPObj.FarmContents.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPBurglary.HailNettingStoredFPObj != null && FPBurglary.HailNettingStoredFPObj.EiId > 0 && FPBurglary.HailNettingStoredFPObj.HailNettingStored != null)
                {
                    db.IT_InsertCustomerQnsData(FPBurglary.CustomerId, Convert.ToInt32(FarmPolicySection.Burglary), FPBurglary.HailNettingStoredFPObj.EiId, FPBurglary.HailNettingStoredFPObj.HailNettingStored.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPBurglary.UnspecifiedMachineryFPObj != null && FPBurglary.UnspecifiedMachineryFPObj.EiId > 0 && FPBurglary.UnspecifiedMachineryFPObj.UnspecifiedMachinery != null)
                {
                    db.IT_InsertCustomerQnsData(FPBurglary.CustomerId, Convert.ToInt32(FarmPolicySection.Burglary), FPBurglary.UnspecifiedMachineryFPObj.EiId, FPBurglary.UnspecifiedMachineryFPObj.UnspecifiedMachinery.ToString(),Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPBurglary.SpecifiedItemsOver5KFPObj != null && FPBurglary.SpecifiedItemsOver5KFPObj.EiId > 0 && FPBurglary.SpecifiedItemsOver5KFPObj.SpecifiedItemOver5K != null)
                {
                    db.IT_InsertCustomerQnsData(FPBurglary.CustomerId, Convert.ToInt32(FarmPolicySection.Burglary), FPBurglary.SpecifiedItemsOver5KFPObj.EiId, FPBurglary.SpecifiedItemsOver5KFPObj.SpecifiedItemOver5K.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPBurglary.ExcessFPBurglaryObj != null && FPBurglary.ExcessFPBurglaryObj.EiId > 0 && FPBurglary.ExcessFPBurglaryObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FPBurglary.CustomerId, Convert.ToInt32(FarmPolicySection.Burglary), FPBurglary.ExcessFPBurglaryObj.EiId, FPBurglary.ExcessFPBurglaryObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPBurglary.OptFPCoverTheftFSAndFCObj != null && FPBurglary.OptFPCoverTheftFSAndFCObj.EiId > 0 && FPBurglary.OptFPCoverTheftFSAndFCObj.CoverTheftFSandFC != null)
                {
                    db.IT_InsertCustomerQnsData(FPBurglary.CustomerId, Convert.ToInt32(FarmPolicySection.Burglary), FPBurglary.OptFPCoverTheftFSAndFCObj.EiId, FPBurglary.OptFPCoverTheftFSAndFCObj.CoverTheftFSandFC.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPBurglary.OptFPCoverTheftFarmMachineryObj != null && FPBurglary.OptFPCoverTheftFarmMachineryObj.EiId > 0 && FPBurglary.OptFPCoverTheftFarmMachineryObj.CoverTheftFarmMachinery != null)
                {
                    db.IT_InsertCustomerQnsData(FPBurglary.CustomerId, Convert.ToInt32(FarmPolicySection.Burglary), FPBurglary.OptFPCoverTheftFarmMachineryObj.EiId, FPBurglary.OptFPCoverTheftFarmMachineryObj.CoverTheftFarmMachinery.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (FPBurglary.OptFPPortalableItemsOptObj != null && FPBurglary.OptFPPortalableItemsOptObj.EiId > 0 && FPBurglary.OptFPPortalableItemsOptObj.PortalbleItemsOpt != null)
                {
                    db.IT_InsertCustomerQnsData(FPBurglary.CustomerId, Convert.ToInt32(FarmPolicySection.Burglary), FPBurglary.OptFPPortalableItemsOptObj.EiId, FPBurglary.OptFPPortalableItemsOptObj.PortalbleItemsOpt.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
            }
            return RedirectToAction("Electronics", "FarmPolicyElectronics", new { cid = cid });
        }
    }
}