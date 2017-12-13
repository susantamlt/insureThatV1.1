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
    public class PetsController : Controller
    {
        // GET: Pets
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PetsCover(int? cid)
        {
            if (Session["Policyinclustions"] != null)
            {
                List<string> PolicyInclustions = new List<string>();
                var Policyincllist = Session["Policyinclustions"] as List<string>;
                if (Policyincllist != null)
                {
                   if (Policyincllist.Contains("Pet"))
                        {
                          
                        }
                }
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid });
            }
            NewPolicyDetailsClass petsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> petsBreedslist = new List<SelectListItem>();
            petsBreedslist = petsmodel.breedListDog();
            Pets Pets = new Pets();
            Pets.SpeciesObj = new Speciesed();
            Pets.SpeciesObj.EiId = 61331;
            Pets.BreedObj = new Breeds();
            Pets.BreedObj.BreedList = petsBreedslist;
            Pets.BreedObj.EiId = 61333;
            Pets.OtherbreedObj = new OtherBreeds();
            Pets.OtherbreedObj.EiId = 61337;
            Pets.NameObj = new Names();
            Pets.NameObj.EiId = 61339;
            Pets.DatebirthObj = new DateOfBirth();
            Pets.DatebirthObj.EiId = 61341;
            Pets.PreexistingObj = new PreExistings();
            Pets.PreexistingObj.EiId = 61343;
            Pets.DescriptionillnessObj = new DescriptionOfIllness();
            Pets.DescriptionillnessObj.EiId = 61345;
            Pets.AnnualcoverlimitObj = new AnnualCoverLimits();
            Pets.AnnualcoverlimitObj.EiId = 61347;
            Pets.ExcessPetObj = new ExcessPets();
            Pets.ExcessPetObj.EiId = 61349;
            Pets.BoardingfeeObj = new BoardingFees();
            Pets.BoardingfeeObj.EiId = 61365;
            Pets.AnnuallimitbfObj = new AnnualLimitsBF();
            Pets.AnnuallimitbfObj.EiId = 61367;
            Pets.DeathillnessObj = new DeathFromIllness();
            Pets.DeathillnessObj.EiId = 61369;
            Pets.AnnuallimitdtObj = new AnnualLimitsDT();
            Pets.AnnuallimitdtObj.EiId = 61371;
            Pets.DeathinjuryObj = new DeathFromInjury();
            Pets.DeathinjuryObj.EiId = 61373;
            Pets.AnnuallimitijObj = new AnnualLimitsIJ();
            Pets.AnnuallimitijObj.EiId = 61375;
            List<SelectListItem> sepclist = new List<SelectListItem>();
            sepclist.Add(new SelectListItem { Text = "Dog", Value = "1" });
            sepclist.Add(new SelectListItem { Text = "Cat", Value = "2" });
            Pets.SpeciesObj.SpeciesList = sepclist;
            List<SelectListItem> ExcList = new List<SelectListItem>();
            ExcList.Add(new SelectListItem { Text = "$250", Value = "1" });
            Pets.ExcessPetObj.ExcessList = ExcList;
            Pets.CustomerId = cid ?? 0;
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(RLSSection.Pet),Convert.ToInt32(PolicyType.RLS), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == Pets.SpeciesObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == Pets.SpeciesObj.EiId).FirstOrDefault();
                    Pets.SpeciesObj.Species = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == Pets.BreedObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == Pets.BreedObj.EiId).FirstOrDefault();
                    Pets.BreedObj.Breed = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == Pets.OtherbreedObj.EiId))
                {
                    Pets.OtherbreedObj.Otherbreed = Convert.ToString(details.Where(q => q.QuestionId == Pets.OtherbreedObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Pets.NameObj.EiId))
                {
                    Pets.NameObj.Name = Convert.ToString(details.Where(q => q.QuestionId == Pets.NameObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Pets.DatebirthObj.EiId))
                {
                    Pets.DatebirthObj.Datebirth = Convert.ToString(details.Where(q => q.QuestionId == Pets.DatebirthObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Pets.PreexistingObj.EiId))
                {
                    Pets.PreexistingObj.Preexisting = Convert.ToString(details.Where(q => q.QuestionId == Pets.PreexistingObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Pets.SpeciesObj.EiId))
                {
                    Pets.DescriptionillnessObj.Descriptionillness = Convert.ToString(details.Where(q => q.QuestionId == Pets.DescriptionillnessObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Pets.AnnualcoverlimitObj.EiId))
                {
                    Pets.AnnualcoverlimitObj.Annualcoverlimit = Convert.ToString(details.Where(q => q.QuestionId == Pets.AnnualcoverlimitObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Pets.ExcessPetObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == Pets.ExcessPetObj.EiId).FirstOrDefault();
                    Pets.ExcessPetObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == Pets.BoardingfeeObj.EiId))
                {
                    Pets.BoardingfeeObj.Boardingfee = Convert.ToString(details.Where(q => q.QuestionId == Pets.BoardingfeeObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Pets.AnnuallimitbfObj.EiId))
                {
                    Pets.AnnuallimitbfObj.Annuallimitbf = Convert.ToString(details.Where(q => q.QuestionId == Pets.AnnuallimitbfObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Pets.DeathillnessObj.EiId))
                {
                    Pets.DeathillnessObj.Deathillness = Convert.ToString(details.Where(q => q.QuestionId == Pets.DeathillnessObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Pets.AnnuallimitdtObj.EiId))
                {
                    Pets.AnnuallimitdtObj.Annuallimit = Convert.ToString(details.Where(q => q.QuestionId == Pets.AnnuallimitdtObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Pets.DeathinjuryObj.EiId))
                {
                    Pets.DeathinjuryObj.Deathinjury = Convert.ToString(details.Where(q => q.QuestionId == Pets.DeathinjuryObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Pets.AnnuallimitijObj.EiId))
                {
                    Pets.AnnuallimitijObj.Annuallimitij = Convert.ToString(details.Where(q => q.QuestionId == Pets.AnnuallimitijObj.EiId).FirstOrDefault().Answer);
                }
            }

            return View(Pets);
        }
        [HttpPost]
        public ActionResult PetsCover(Pets Pets, int? cid)
        {
            NewPolicyDetailsClass petsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> petsBreedslist = new List<SelectListItem>();
            if (Pets.SpeciesObj.EiId > 0 && Pets.SpeciesObj.Species != null && Pets.SpeciesObj.Species=="2")
            {
                petsBreedslist = petsmodel.breedListDog();
            }
            else
            {
                petsBreedslist = petsmodel.breedListDog();
            }
            Pets.BreedObj.BreedList = petsBreedslist;
            List<SelectListItem> sepclist = new List<SelectListItem>();
            sepclist.Add(new SelectListItem { Text = "Dog", Value = "1" });
            sepclist.Add(new SelectListItem { Text = "Cat", Value = "2" });
            Pets.SpeciesObj.SpeciesList = sepclist;
            List<SelectListItem> ExcList = new List<SelectListItem>();
            ExcList.Add(new SelectListItem { Text = "$250", Value = "1" });
            Pets.ExcessPetObj.ExcessList = ExcList;

            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (Pets.SpeciesObj!= null  && Pets.SpeciesObj.EiId > 0 && Pets.SpeciesObj.Species != null) {
                    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.SpeciesObj.EiId, Pets.SpeciesObj.Species.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Pets.BreedObj != null && Pets.BreedObj.EiId > 0  && Pets.BreedObj.Breed != null)
                {
                    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.BreedObj.EiId, Pets.BreedObj.Breed, Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Pets.OtherbreedObj != null && Pets.OtherbreedObj.EiId > 0 && Pets.OtherbreedObj.Otherbreed != null)
                {
                    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.OtherbreedObj.EiId, Pets.OtherbreedObj.Otherbreed.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Pets.NameObj != null && Pets.NameObj.EiId > 0 && Pets.NameObj.Name != null)
                {
                    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.NameObj.EiId, Pets.NameObj.Name.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Pets.DatebirthObj != null && Pets.DatebirthObj.EiId > 0 && Pets.DatebirthObj.Datebirth != null)
                {
                    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.DatebirthObj.EiId, Pets.DatebirthObj.Datebirth.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Pets.PreexistingObj != null && Pets.PreexistingObj.EiId > 0 && Pets.SpeciesObj != null)
                {
                    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.PreexistingObj.EiId, Pets.PreexistingObj.Preexisting.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Pets.DescriptionillnessObj != null && Pets.DescriptionillnessObj.EiId > 0 && Pets.DescriptionillnessObj.Descriptionillness != null)
                {
                    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.DescriptionillnessObj.EiId, Pets.DescriptionillnessObj.Descriptionillness.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Pets.AnnualcoverlimitObj != null && Pets.AnnualcoverlimitObj.EiId > 0 && Pets.AnnualcoverlimitObj.Annualcoverlimit != null)
                {
                    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.AnnualcoverlimitObj.EiId, Pets.AnnualcoverlimitObj.Annualcoverlimit.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Pets.ExcessPetObj != null && Pets.ExcessPetObj.EiId > 0 && Pets.ExcessPetObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.ExcessPetObj.EiId, Pets.ExcessPetObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Pets.BoardingfeeObj != null && Pets.BoardingfeeObj.EiId > 0 && Pets.BoardingfeeObj.Boardingfee != null)
                {
                    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.BoardingfeeObj.EiId, Pets.BoardingfeeObj.Boardingfee.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Pets.AnnuallimitbfObj != null && Pets.AnnuallimitbfObj.EiId > 0 && Pets.AnnuallimitbfObj.Annuallimitbf != null)
                {
                    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.AnnuallimitbfObj.EiId, Pets.AnnuallimitbfObj.Annuallimitbf.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Pets.DeathillnessObj != null && Pets.DeathillnessObj.EiId > 0 && Pets.DeathillnessObj.Deathillness != null)
                {
                    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.DeathillnessObj.EiId, Pets.DeathillnessObj.Deathillness.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Pets.AnnuallimitdtObj != null && Pets.AnnuallimitdtObj.EiId > 0 && Pets.AnnuallimitdtObj.Annuallimit != null)
                {
                    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.AnnuallimitdtObj.EiId, Pets.AnnuallimitdtObj.Annuallimit.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Pets.DeathinjuryObj != null && Pets.DeathinjuryObj.EiId > 0 && Pets.DeathinjuryObj.Deathinjury != null)
                {
                    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.DeathinjuryObj.EiId, Pets.DeathinjuryObj.Deathinjury.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Pets.AnnuallimitijObj != null && Pets.AnnuallimitijObj.EiId > 0 && Pets.AnnuallimitijObj.Annuallimitij != null)
                {
                    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.AnnuallimitijObj.EiId, Pets.AnnuallimitijObj.Annuallimitij.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                Pets.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = Pets.CustomerId;
            }
            return RedirectToAction("BoatDetails","Boat" , new { cid = Pets.CustomerId });
        }
        [HttpPost]
        public ActionResult PetsCoverAjax(string Species, int SpeciesV)
        {
            List<SelectListItem> breadList = new List<SelectListItem>();
            if (Request.IsAjaxRequest())
            {
                NewPolicyDetailsClass model = new NewPolicyDetailsClass();
                if (SpeciesV == 1) //dog
                {
                    breadList = model.breedListDog();
                }
                else if (SpeciesV == 2) //cat
                {
                    breadList = model.breedListCat();
                }
                return Json(new { status = true, result = breadList });
            }
            return Json(new { status = false, result = breadList });
        }
    }
}