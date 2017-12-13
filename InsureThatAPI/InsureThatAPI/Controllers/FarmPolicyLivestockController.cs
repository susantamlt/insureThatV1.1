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
    public class FarmPolicyLivestockController : Controller
    {
        // GET: FarmPolicyLivestock
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Livestock(int? cid)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> ClassofAnimalLivestock = new List<SelectListItem>();
            ClassofAnimalLivestock = commonModel.ClassofAnimalLivestock();

            List<SelectListItem> TypeofAnimalLivestock = new List<SelectListItem>();
            TypeofAnimalLivestock = commonModel.TypeofAnimalLivestock();

            List<SelectListItem> AgeofAnimalLivestock = new List<SelectListItem>();
            AgeofAnimalLivestock = commonModel.AgeofAnimalLivestock();

            List<SelectListItem> UseofAnimalLivestock = new List<SelectListItem>();
            UseofAnimalLivestock = commonModel.UseofAnimalLivestock();            

            List<SelectListItem> excessToPayLiveStock = new List<SelectListItem>();
            excessToPayLiveStock = commonModel.excessRate();

            FPLivestock FPLivestock = new FPLivestock();
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {

                   
                        if (Policyincllist.Contains("LiveStockFarm"))
                        {
                           
                        }
                        else{ if (Policyincllist.Contains("PersonalLiabilitiesFarm"))
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
                FPLivestock.CustomerId = cid.Value;
            }
            FPLivestock.ClassOfAnimalFPObj = new ClassOfAnimalFP();
            FPLivestock.ClassOfAnimalFPObj.ClassOfAnimalList = ClassofAnimalLivestock;
            FPLivestock.ClassOfAnimalFPObj.EiId = 63321;

            FPLivestock.TypeOfAnimalFPObj = new TypeOfAnimalFP();
            FPLivestock.TypeOfAnimalFPObj.TypeOfAnimalList = TypeofAnimalLivestock;
            FPLivestock.TypeOfAnimalFPObj.EiId = 63323;

            FPLivestock.BreedOfAnimalFPObj = new BreedOfAnimalFP();
            FPLivestock.BreedOfAnimalFPObj.EiId = 63325;

            FPLivestock.AgeOfAnimalFPObj = new AgeOfAnimalFP();
            FPLivestock.AgeOfAnimalFPObj.AgeOfAnimalList  = AgeofAnimalLivestock;
            FPLivestock.AgeOfAnimalFPObj.EiId = 63327;

            FPLivestock.ColourOfAnimalFPObj = new ColourOfAnimalFP();
            FPLivestock.ColourOfAnimalFPObj.EiId = 63329;

            FPLivestock.UseOfAnimalFPObj = new UseOfAnimalFP();
            FPLivestock.UseOfAnimalFPObj.UseOfAnimalList = UseofAnimalLivestock;
            FPLivestock.UseOfAnimalFPObj.EiId = 63331;

            FPLivestock.DescBrandOfAnimalFPObj = new DescBrandOfAnimalFP();
            FPLivestock.DescBrandOfAnimalFPObj.EiId = 63333;

            FPLivestock.DescMarksOfAnimalFPObj = new DescMarksOfAnimalFP();
            FPLivestock.DescMarksOfAnimalFPObj.EiId = 63335;

            FPLivestock.OptSoundHealthofAnimalFPObj = new OptSoundHealthofAnimalFP();
            FPLivestock.OptSoundHealthofAnimalFPObj.EiId = 63345;

            FPLivestock.DescSoundHealthofAnimalFPObj = new DescSoundHealthofAnimalFP();
            FPLivestock.DescSoundHealthofAnimalFPObj.EiId = 63347;       

            FPLivestock.OptDiseaseOfAnimalFPObj = new OptDiseaseOfAnimalFP();
            FPLivestock.OptDiseaseOfAnimalFPObj.EiId = 63349;

            FPLivestock.DescDiseaseOfAnimalFPObj = new DescDiseaseOfAnimalFP();
            FPLivestock.DescDiseaseOfAnimalFPObj.EiId = 63351;

            FPLivestock.OptAnimalSyndicatedFPObj = new OptAnimalSyndicatedFP();
            FPLivestock.OptAnimalSyndicatedFPObj.EiId = 63353;

            FPLivestock.DescAnimalSyndicatedFPObj = new DescAnimalSyndicatedFP();
            FPLivestock.DescAnimalSyndicatedFPObj.EiId = 11111;

            FPLivestock.SumInsuredLivestockFPObj = new SumInsuredLivestockFP();
            FPLivestock.SumInsuredLivestockFPObj.EiId = 63355;

            FPLivestock.OptInfertilityFPObj = new OptInfertilityFP();
            FPLivestock.OptInfertilityFPObj.EiId = 63359;

            FPLivestock.OptLossofUseLivestockFPObj = new OptLossofUseLivestockFP();
            FPLivestock.OptLossofUseLivestockFPObj.EiId = 63361;

            FPLivestock.OptTheftLivestockFPObj = new OptTheftLivestockFP();
            FPLivestock.OptTheftLivestockFPObj.EiId = 63363;

            FPLivestock.OptUnbornFoalFPObj = new OptUnbornFoalFP();
            FPLivestock.OptUnbornFoalFPObj.EiId = 63365;

            FPLivestock.ExcessLivestockFPObj= new ExcessLivestockFP();
            FPLivestock.ExcessLivestockFPObj.ExcessList = excessToPayLiveStock;
            FPLivestock.ExcessLivestockFPObj.EiId = 63375;


            FPLivestock.NoOfContainersFPObj = new NoOfContainersFP();
            FPLivestock.NoOfContainersFPObj.EiId = 63383;

            FPLivestock.MaxStrawsandAmpoulesFPObj = new MaxStrawsandAmpoulesFP();
            FPLivestock.MaxStrawsandAmpoulesFPObj.EiId = 63385;

            FPLivestock.MaxValOneContainerFPObj = new MaxValOneContainerFP();
            FPLivestock.MaxValOneContainerFPObj.EiId = 63387;

            FPLivestock.AnnualStrawsandAmpoulesFPObj = new AnnualStrawsandAmpoulesFP();
            FPLivestock.AnnualStrawsandAmpoulesFPObj.EiId = 63389;


            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(FarmPolicySection.LiveStockFarm) , Convert.ToInt32(PolicyType.FarmPolicy), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == FPLivestock.ClassOfAnimalFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPLivestock.ClassOfAnimalFPObj.EiId).FirstOrDefault();
                    FPLivestock.ClassOfAnimalFPObj.ClassofAnimal = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == FPLivestock.TypeOfAnimalFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPLivestock.TypeOfAnimalFPObj.EiId).FirstOrDefault();
                    FPLivestock.TypeOfAnimalFPObj.TypeofAnimal = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPLivestock.BreedOfAnimalFPObj.EiId))
                {
                    FPLivestock.BreedOfAnimalFPObj.BreedofAnimal = Convert.ToString(details.Where(q => q.QuestionId == FPLivestock.BreedOfAnimalFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPLivestock.AgeOfAnimalFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPLivestock.AgeOfAnimalFPObj.EiId).FirstOrDefault();
                    FPLivestock.AgeOfAnimalFPObj.AgeofAnimal = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPLivestock.ColourOfAnimalFPObj.EiId))
                {
                    FPLivestock.ColourOfAnimalFPObj.ColourofAnimal = Convert.ToString(details.Where(q => q.QuestionId == FPLivestock.ColourOfAnimalFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPLivestock.UseOfAnimalFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPLivestock.UseOfAnimalFPObj.EiId).FirstOrDefault();
                    FPLivestock.UseOfAnimalFPObj.UseofAnimal = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == FPLivestock.DescBrandOfAnimalFPObj.EiId))
                {
                    FPLivestock.DescBrandOfAnimalFPObj.BrandofAnimal = Convert.ToString(details.Where(q => q.QuestionId == FPLivestock.DescBrandOfAnimalFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPLivestock.DescMarksOfAnimalFPObj.EiId))
                {
                    FPLivestock.DescMarksOfAnimalFPObj.MarkingsofAnimal = Convert.ToString(details.Where(q => q.QuestionId == FPLivestock.DescMarksOfAnimalFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPLivestock.OptSoundHealthofAnimalFPObj.EiId))
                {
                    FPLivestock.OptSoundHealthofAnimalFPObj.HealthofAnimal = Convert.ToString(details.Where(q => q.QuestionId == FPLivestock.OptSoundHealthofAnimalFPObj.EiId).FirstOrDefault().Answer);
                }

                
                if (details.Exists(q => q.QuestionId == FPLivestock.DescSoundHealthofAnimalFPObj.EiId))
                {
                    FPLivestock.DescSoundHealthofAnimalFPObj.Description = Convert.ToString(details.Where(q => q.QuestionId == FPLivestock.DescSoundHealthofAnimalFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPLivestock.OptDiseaseOfAnimalFPObj.EiId))
                {
                    FPLivestock.OptDiseaseOfAnimalFPObj.DiseaseofAnimal = Convert.ToString(details.Where(q => q.QuestionId == FPLivestock.OptDiseaseOfAnimalFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPLivestock.DescDiseaseOfAnimalFPObj.EiId))
                {
                    FPLivestock.DescDiseaseOfAnimalFPObj.Description = Convert.ToString(details.Where(q => q.QuestionId == FPLivestock.DescDiseaseOfAnimalFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPLivestock.OptAnimalSyndicatedFPObj.EiId))
                {
                    FPLivestock.OptAnimalSyndicatedFPObj.AnimalSyndicated = Convert.ToString(details.Where(q => q.QuestionId == FPLivestock.OptAnimalSyndicatedFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPLivestock.SumInsuredLivestockFPObj.EiId))
                {
                    FPLivestock.SumInsuredLivestockFPObj.SumInsured = Convert.ToString(details.Where(q => q.QuestionId == FPLivestock.SumInsuredLivestockFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPLivestock.ExcessLivestockFPObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FPLivestock.ExcessLivestockFPObj.EiId).FirstOrDefault();
                    FPLivestock.ExcessLivestockFPObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

                if (details.Exists(q => q.QuestionId == FPLivestock.NoOfContainersFPObj.EiId))
                {
                    FPLivestock.NoOfContainersFPObj.NumberOfContainers = Convert.ToString(details.Where(q => q.QuestionId == FPLivestock.NoOfContainersFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPLivestock.MaxStrawsandAmpoulesFPObj.EiId))
                {
                    FPLivestock.MaxStrawsandAmpoulesFPObj.StrawAndAmpoules = Convert.ToString(details.Where(q => q.QuestionId == FPLivestock.MaxStrawsandAmpoulesFPObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == FPLivestock.MaxValOneContainerFPObj.EiId))
                {
                    FPLivestock.MaxValOneContainerFPObj.MaxValoneContainer = Convert.ToString(details.Where(q => q.QuestionId == FPLivestock.MaxValOneContainerFPObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FPLivestock.AnnualStrawsandAmpoulesFPObj.EiId))
                {
                    FPLivestock.AnnualStrawsandAmpoulesFPObj.AnnualStrawandAmpoules = Convert.ToString(details.Where(q => q.QuestionId == FPLivestock.AnnualStrawsandAmpoulesFPObj.EiId).FirstOrDefault().Answer);
                }

            }

            return View(FPLivestock);
        }

        [HttpPost]
        public ActionResult Livestock(int? cid, FPLivestock FPLivestock)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            List<SelectListItem> ClassofAnimalLivestock = new List<SelectListItem>();
            ClassofAnimalLivestock = commonModel.ClassofAnimalLivestock();
            FPLivestock.ClassOfAnimalFPObj.ClassOfAnimalList = ClassofAnimalLivestock;

            List<SelectListItem> TypeofAnimalLivestock = new List<SelectListItem>();
            TypeofAnimalLivestock = commonModel.TypeofAnimalLivestock();
            FPLivestock.TypeOfAnimalFPObj.TypeOfAnimalList = TypeofAnimalLivestock;


            List<SelectListItem> AgeofAnimalLivestock = new List<SelectListItem>();
            AgeofAnimalLivestock = commonModel.AgeofAnimalLivestock();
            FPLivestock.AgeOfAnimalFPObj.AgeOfAnimalList = AgeofAnimalLivestock;

            List<SelectListItem> UseofAnimalLivestock = new List<SelectListItem>();
            UseofAnimalLivestock = commonModel.UseofAnimalLivestock();
            FPLivestock.UseOfAnimalFPObj.UseOfAnimalList = UseofAnimalLivestock;

            List<SelectListItem> excessToPayLiveStock = new List<SelectListItem>();
            excessToPayLiveStock = commonModel.excessRate();
            FPLivestock.ExcessLivestockFPObj.ExcessList = excessToPayLiveStock;
            if (cid != null)
            {
                ViewBag.cid = cid;
                FPLivestock.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = FPLivestock.CustomerId;
            }
            string policyid = null;
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (FPLivestock.ClassOfAnimalFPObj != null && FPLivestock.ClassOfAnimalFPObj.EiId > 0 && FPLivestock.ClassOfAnimalFPObj.ClassofAnimal != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.ClassOfAnimalFPObj.EiId, FPLivestock.ClassOfAnimalFPObj.ClassofAnimal.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.TypeOfAnimalFPObj != null && FPLivestock.TypeOfAnimalFPObj.EiId > 0 && FPLivestock.TypeOfAnimalFPObj.TypeofAnimal != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.TypeOfAnimalFPObj.EiId, FPLivestock.TypeOfAnimalFPObj.TypeofAnimal.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.BreedOfAnimalFPObj != null && FPLivestock.BreedOfAnimalFPObj.EiId > 0 && FPLivestock.BreedOfAnimalFPObj.BreedofAnimal != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.BreedOfAnimalFPObj.EiId, FPLivestock.BreedOfAnimalFPObj.BreedofAnimal.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.AgeOfAnimalFPObj != null && FPLivestock.AgeOfAnimalFPObj.EiId > 0 && FPLivestock.AgeOfAnimalFPObj.AgeofAnimal != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.AgeOfAnimalFPObj.EiId, FPLivestock.AgeOfAnimalFPObj.AgeofAnimal.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.ColourOfAnimalFPObj != null && FPLivestock.ColourOfAnimalFPObj.EiId > 0 && FPLivestock.ColourOfAnimalFPObj.ColourofAnimal != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.ColourOfAnimalFPObj.EiId, FPLivestock.ColourOfAnimalFPObj.ColourofAnimal.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.UseOfAnimalFPObj != null && FPLivestock.UseOfAnimalFPObj.EiId > 0 && FPLivestock.UseOfAnimalFPObj.UseofAnimal != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.UseOfAnimalFPObj.EiId, FPLivestock.UseOfAnimalFPObj.UseofAnimal.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.DescBrandOfAnimalFPObj != null && FPLivestock.DescBrandOfAnimalFPObj.EiId > 0 && FPLivestock.DescBrandOfAnimalFPObj.EiId > 0 && FPLivestock.DescBrandOfAnimalFPObj.BrandofAnimal != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.DescBrandOfAnimalFPObj.EiId, FPLivestock.DescBrandOfAnimalFPObj.BrandofAnimal.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.DescMarksOfAnimalFPObj != null && FPLivestock.DescMarksOfAnimalFPObj.EiId > 0 && FPLivestock.DescMarksOfAnimalFPObj.MarkingsofAnimal != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.DescMarksOfAnimalFPObj.EiId, FPLivestock.DescMarksOfAnimalFPObj.MarkingsofAnimal.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.OptSoundHealthofAnimalFPObj != null && FPLivestock.OptSoundHealthofAnimalFPObj.EiId > 0 && FPLivestock.OptSoundHealthofAnimalFPObj.HealthofAnimal != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.OptSoundHealthofAnimalFPObj.EiId, FPLivestock.OptSoundHealthofAnimalFPObj.HealthofAnimal.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.DescSoundHealthofAnimalFPObj != null && FPLivestock.DescSoundHealthofAnimalFPObj.EiId > 0 && FPLivestock.DescSoundHealthofAnimalFPObj.Description != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.DescSoundHealthofAnimalFPObj.EiId, FPLivestock.DescSoundHealthofAnimalFPObj.Description.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.OptDiseaseOfAnimalFPObj != null && FPLivestock.OptDiseaseOfAnimalFPObj.EiId > 0 && FPLivestock.OptDiseaseOfAnimalFPObj.DiseaseofAnimal != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.OptDiseaseOfAnimalFPObj.EiId, FPLivestock.OptDiseaseOfAnimalFPObj.DiseaseofAnimal.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.DescDiseaseOfAnimalFPObj != null && FPLivestock.DescDiseaseOfAnimalFPObj.EiId > 0 && FPLivestock.DescDiseaseOfAnimalFPObj.Description != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.DescDiseaseOfAnimalFPObj.EiId, FPLivestock.DescDiseaseOfAnimalFPObj.Description.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.OptAnimalSyndicatedFPObj != null && FPLivestock.OptAnimalSyndicatedFPObj.EiId > 0 && FPLivestock.OptAnimalSyndicatedFPObj.AnimalSyndicated != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.OptAnimalSyndicatedFPObj.EiId, FPLivestock.OptAnimalSyndicatedFPObj.AnimalSyndicated.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.DescAnimalSyndicatedFPObj != null && FPLivestock.DescAnimalSyndicatedFPObj.EiId > 0 && FPLivestock.DescAnimalSyndicatedFPObj.Description != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.DescAnimalSyndicatedFPObj.EiId, FPLivestock.DescAnimalSyndicatedFPObj.Description.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.SumInsuredLivestockFPObj != null && FPLivestock.SumInsuredLivestockFPObj.EiId > 0 && FPLivestock.SumInsuredLivestockFPObj.SumInsured != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.SumInsuredLivestockFPObj.EiId, FPLivestock.SumInsuredLivestockFPObj.SumInsured.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (FPLivestock.OptInfertilityFPObj != null && FPLivestock.OptInfertilityFPObj.EiId > 0 && FPLivestock.OptInfertilityFPObj.Infertility != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.OptInfertilityFPObj.EiId, FPLivestock.OptInfertilityFPObj.Infertility.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.OptLossofUseLivestockFPObj != null && FPLivestock.OptLossofUseLivestockFPObj.EiId > 0 && FPLivestock.OptLossofUseLivestockFPObj.LossofUse != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.OptLossofUseLivestockFPObj.EiId, FPLivestock.OptLossofUseLivestockFPObj.LossofUse.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.OptTheftLivestockFPObj != null && FPLivestock.OptTheftLivestockFPObj.EiId > 0 && FPLivestock.OptTheftLivestockFPObj.TheftOption != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.OptTheftLivestockFPObj.EiId, FPLivestock.OptTheftLivestockFPObj.TheftOption.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.OptUnbornFoalFPObj != null && FPLivestock.OptUnbornFoalFPObj.EiId > 0 && FPLivestock.OptUnbornFoalFPObj.UnbornFoal != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.OptUnbornFoalFPObj.EiId, FPLivestock.OptUnbornFoalFPObj.UnbornFoal.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.ExcessLivestockFPObj != null && FPLivestock.ExcessLivestockFPObj.EiId > 0 && FPLivestock.ExcessLivestockFPObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.ExcessLivestockFPObj.EiId, FPLivestock.ExcessLivestockFPObj.Excess.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
                if (FPLivestock.NoOfContainersFPObj != null && FPLivestock.NoOfContainersFPObj.EiId > 0 && FPLivestock.NoOfContainersFPObj.NumberOfContainers != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.NoOfContainersFPObj.EiId, FPLivestock.NoOfContainersFPObj.NumberOfContainers.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.MaxStrawsandAmpoulesFPObj != null && FPLivestock.MaxStrawsandAmpoulesFPObj.EiId > 0 && FPLivestock.MaxStrawsandAmpoulesFPObj.StrawAndAmpoules != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.MaxStrawsandAmpoulesFPObj.EiId, FPLivestock.MaxStrawsandAmpoulesFPObj.StrawAndAmpoules.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.MaxValOneContainerFPObj != null && FPLivestock.MaxValOneContainerFPObj.EiId > 0 && FPLivestock.MaxValOneContainerFPObj.MaxValoneContainer != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.MaxValOneContainerFPObj.EiId, FPLivestock.MaxValOneContainerFPObj.MaxValoneContainer.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }

                if (FPLivestock.AnnualStrawsandAmpoulesFPObj != null && FPLivestock.AnnualStrawsandAmpoulesFPObj.EiId > 0 && FPLivestock.AnnualStrawsandAmpoulesFPObj.AnnualStrawandAmpoules != null)
                {
                    db.IT_InsertCustomerQnsData(FPLivestock.CustomerId, Convert.ToInt32(FarmPolicySection.LiveStockFarm), FPLivestock.AnnualStrawsandAmpoulesFPObj.EiId, FPLivestock.AnnualStrawsandAmpoulesFPObj.AnnualStrawandAmpoules.ToString(), Convert.ToInt32(PolicyType.FarmPolicy), policyid);
                }
            }
            return RedirectToAction("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid });
        }
    }
}