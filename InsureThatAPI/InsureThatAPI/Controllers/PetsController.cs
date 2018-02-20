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
    public class PetsController : Controller
    {
        // GET: Pets
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> PetsCover(int? cid, int? PcId)
        {
            string apikey = null;
            NewPolicyDetailsClass petsmodel = new NewPolicyDetailsClass();
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            Pets Pets = new Pets();
            List<SessionModel> PolicyInclustions = new List<SessionModel>();
            var Policyincllist = new List<SessionModel>();
            if (Session["ApiKey"] != null)
            {
                apikey = Session["ApiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (Session["Policyinclustions"] != null)
            {
                Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                Pets.PolicyInclusions = new List<SessionModel>();
                Pets.PolicyInclusions = Policyincllist;
                if (Policyincllist != null)
                {
                    if (Policyincllist.Exists(p => p.name == "Pet"))
                    {
                        if (Session["unId"] == null && Session["profileId"] == null)
                        {
                            Session["unId"] = Policyincllist.Where(p => p.name == "Pet").Select(p => p.UnitId).First();
                            Session["profileId"] = Policyincllist.Where(p => p.name == "Pet").Select(p => p.ProfileId).First();
                        }
                    }
                    else
                    {
                        if (Policyincllist.Exists(p => p.name == "Travel"))
                        {
                            return RedirectToAction("TravelCover", "Travel", new { cid = cid });
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
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid });
            }
            var db = new MasterDataEntities();

            var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
            List<SelectListItem> petsBreedslist = new List<SelectListItem>();
            petsBreedslist = petsmodel.breedListDog();

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

            string policyid = null;
            bool policyinclusion = policyinclusions.Exists(p => p.Name == "Pet");
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = 0;
            int profileid = 0;
            if (Session["unId"] != null && Session["profileId"] != null)
            {
                unid = Convert.ToInt32(Session["unId"]);
                profileid = Convert.ToInt32(Session["profileId"]);
            }
            if (policyinclusion == true && PcId != null && PcId.HasValue)
            {
                Pets.PolicyInclusion = policyinclusions;
                Pets.ExistingPolicyInclustions = policyinclusions;
                //int sectionId = policyinclusions.Where(p => p.Name == "Home Contents" && p.UnitNumber == unid).Select(p => p.UnId).FirstOrDefault();
                //int? profileunid = policyinclusions.Where(p => p.Name == "Home Contents" && p.ProfileUnId == profileid).Select(p => p.ProfileUnId).FirstOrDefault();
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
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Pets&SectionUnId=&ProfileUnId=");
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails.ErrorMessage != null && unitdetails.ErrorMessage.Count() > 0)
                        {
                            var errormessage = "First please provide cover for Home Buildings.";
                            if (unitdetails.ErrorMessage.Contains(errormessage))
                            {
                                TempData["Error"] = errormessage;
                                return RedirectToAction("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = PcId });
                            }
                        }
                        if (unitdetails != null && unitdetails.SectionData != null)
                        {
                            Session["unId"] = unitdetails.SectionData.UnId;
                            Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                            if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Pet"))
                            {
                                var policyhomelist = Policyincllist.FindAll(p => p.name == "Pet").ToList();
                                if (policyhomelist != null && policyhomelist.Count() > 0)
                                {
                                    if (Policyincllist.FindAll(p => p.name == "Pet").Exists(p => p.UnitId == null))
                                    {
                                        Policyincllist.FindAll(p => p.name == "Pet").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;
                                    }
                                    if (Policyincllist.FindAll(p => p.name == "Pet").Exists(p => p.ProfileId == null))
                                    {
                                        Policyincllist.FindAll(p => p.name == "Pet").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                    }
                                }
                                else
                                {
                                    Policyincllist.FindAll(p => p.name == "Pet").First().UnitId = unitdetails.SectionData.UnId;
                                    Policyincllist.FindAll(p => p.name == "Pet").First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                                Pets.PolicyInclusions = Policyincllist;
                                Session["Policyinclustions"] = Policyincllist;
                            }
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
                    }
                }
            }
            if (unitdetails != null)
            {
                if (unitdetails.ProfileData != null)
                {
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.AnnualcoverlimitObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.AnnualcoverlimitObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.AnnualcoverlimitObj.Annualcoverlimit = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.AnnuallimitbfObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.AnnuallimitbfObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.AnnuallimitbfObj.Annuallimitbf = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.AnnuallimitdtObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.AnnuallimitdtObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.AnnuallimitdtObj.Annuallimit = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.AnnuallimitijObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.AnnuallimitijObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.AnnuallimitijObj.Annuallimitij = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.BoardingfeeObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.BoardingfeeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.BoardingfeeObj.Boardingfee = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.BreedObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.BreedObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.BreedObj.Breed = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.DatebirthObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.DatebirthObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.DatebirthObj.Datebirth = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.DeathillnessObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.DeathillnessObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.DeathillnessObj.Deathillness = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.DeathinjuryObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.DeathinjuryObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.DeathinjuryObj.Deathinjury = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.DescriptionillnessObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.DescriptionillnessObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.DescriptionillnessObj.Descriptionillness = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.ExcessPetObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.ExcessPetObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.ExcessPetObj.Excess = val;
                    }
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.ImposedpetsObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.ImposedpetsObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    Pets.ImposedpetsObj.Imposed = val;
                    //}
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.NameObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.NameObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.NameObj.Name = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.OtherbreedObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.OtherbreedObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.OtherbreedObj.Otherbreed = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.PreexistingObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.PreexistingObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.PreexistingObj.Preexisting = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.PremiumptsObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.PremiumptsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.PremiumptsObj.Premium = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.SpeciesObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.SpeciesObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.SpeciesObj.Species = val;
                    }
                }
            }
            //var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(RLSSection.Pet),Convert.ToInt32(PolicyType.RLS), policyid).ToList();
            //if (details != null && details.Any())
            //{
            //    if (details.Exists(q => q.QuestionId == Pets.SpeciesObj.EiId))
            //    {
            //        var loc = details.Where(q => q.QuestionId == Pets.SpeciesObj.EiId).FirstOrDefault();
            //        Pets.SpeciesObj.Species = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
            //    }
            //    if (details.Exists(q => q.QuestionId == Pets.BreedObj.EiId))
            //    {
            //        var loc = details.Where(q => q.QuestionId == Pets.BreedObj.EiId).FirstOrDefault();
            //        Pets.BreedObj.Breed = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
            //    }
            //    if (details.Exists(q => q.QuestionId == Pets.OtherbreedObj.EiId))
            //    {
            //        Pets.OtherbreedObj.Otherbreed = Convert.ToString(details.Where(q => q.QuestionId == Pets.OtherbreedObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == Pets.NameObj.EiId))
            //    {
            //        Pets.NameObj.Name = Convert.ToString(details.Where(q => q.QuestionId == Pets.NameObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == Pets.DatebirthObj.EiId))
            //    {
            //        Pets.DatebirthObj.Datebirth = Convert.ToString(details.Where(q => q.QuestionId == Pets.DatebirthObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == Pets.PreexistingObj.EiId))
            //    {
            //        Pets.PreexistingObj.Preexisting = Convert.ToString(details.Where(q => q.QuestionId == Pets.PreexistingObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == Pets.SpeciesObj.EiId))
            //    {
            //        Pets.DescriptionillnessObj.Descriptionillness = Convert.ToString(details.Where(q => q.QuestionId == Pets.DescriptionillnessObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == Pets.AnnualcoverlimitObj.EiId))
            //    {
            //        Pets.AnnualcoverlimitObj.Annualcoverlimit = Convert.ToString(details.Where(q => q.QuestionId == Pets.AnnualcoverlimitObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == Pets.ExcessPetObj.EiId))
            //    {
            //        var loc = details.Where(q => q.QuestionId == Pets.ExcessPetObj.EiId).FirstOrDefault();
            //        Pets.ExcessPetObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
            //    }
            //    if (details.Exists(q => q.QuestionId == Pets.BoardingfeeObj.EiId))
            //    {
            //        Pets.BoardingfeeObj.Boardingfee = Convert.ToString(details.Where(q => q.QuestionId == Pets.BoardingfeeObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == Pets.AnnuallimitbfObj.EiId))
            //    {
            //        Pets.AnnuallimitbfObj.Annuallimitbf = Convert.ToString(details.Where(q => q.QuestionId == Pets.AnnuallimitbfObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == Pets.DeathillnessObj.EiId))
            //    {
            //        Pets.DeathillnessObj.Deathillness = Convert.ToString(details.Where(q => q.QuestionId == Pets.DeathillnessObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == Pets.AnnuallimitdtObj.EiId))
            //    {
            //        Pets.AnnuallimitdtObj.Annuallimit = Convert.ToString(details.Where(q => q.QuestionId == Pets.AnnuallimitdtObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == Pets.DeathinjuryObj.EiId))
            //    {
            //        Pets.DeathinjuryObj.Deathinjury = Convert.ToString(details.Where(q => q.QuestionId == Pets.DeathinjuryObj.EiId).FirstOrDefault().Answer);
            //    }
            //    if (details.Exists(q => q.QuestionId == Pets.AnnuallimitijObj.EiId))
            //    {
            //        Pets.AnnuallimitijObj.Annuallimitij = Convert.ToString(details.Where(q => q.QuestionId == Pets.AnnuallimitijObj.EiId).FirstOrDefault().Answer);
            //    }
            //}
            if (unitdetails.ReferralList != null)
            {
                Pets.ReferralList = unitdetails.ReferralList;
                Pets.ReferralList.Replace("&nbsp;&nbsp;&nbsp;&nbsp", "");
                Pets.Referels = new List<string>();
                string[] delim = { "<br/>" };

                string[] spltd = Pets.ReferralList.Split(delim, StringSplitOptions.None);
                if (spltd != null && spltd.Count() > 0)
                {
                    for (int i = 0; i < spltd.Count(); i++)
                    {
                        Pets.Referels.Add(spltd[i].Replace("&nbsp;&nbsp;&nbsp;&nbsp", " "));
                    }
                }

            }
            return View(Pets);
        }
        [HttpPost]
        public ActionResult PetsCover(Pets Pets, int? cid)
        {
            NewPolicyDetailsClass petsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> petsBreedslist = new List<SelectListItem>();
            if (Pets.SpeciesObj.EiId > 0 && Pets.SpeciesObj.Species != null && Pets.SpeciesObj.Species == "2")
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
                ////if (Pets.SpeciesObj != null && Pets.SpeciesObj.EiId > 0 && Pets.SpeciesObj.Species != null)
                ////{
                ////    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.SpeciesObj.EiId, Pets.SpeciesObj.Species.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                ////}
                ////if (Pets.BreedObj != null && Pets.BreedObj.EiId > 0 && Pets.BreedObj.Breed != null)
                ////{
                ////    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.BreedObj.EiId, Pets.BreedObj.Breed, Convert.ToInt32(PolicyType.RLS), policyid);
                ////}
                ////if (Pets.OtherbreedObj != null && Pets.OtherbreedObj.EiId > 0 && Pets.OtherbreedObj.Otherbreed != null)
                ////{
                ////    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.OtherbreedObj.EiId, Pets.OtherbreedObj.Otherbreed.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                ////}
                ////if (Pets.NameObj != null && Pets.NameObj.EiId > 0 && Pets.NameObj.Name != null)
                ////{
                ////    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.NameObj.EiId, Pets.NameObj.Name.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                ////}
                ////if (Pets.DatebirthObj != null && Pets.DatebirthObj.EiId > 0 && Pets.DatebirthObj.Datebirth != null)
                ////{
                ////    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.DatebirthObj.EiId, Pets.DatebirthObj.Datebirth.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                ////}
                ////if (Pets.PreexistingObj != null && Pets.PreexistingObj.EiId > 0 && Pets.SpeciesObj != null)
                ////{
                ////    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.PreexistingObj.EiId, Pets.PreexistingObj.Preexisting.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                ////}
                ////if (Pets.DescriptionillnessObj != null && Pets.DescriptionillnessObj.EiId > 0 && Pets.DescriptionillnessObj.Descriptionillness != null)
                ////{
                ////    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.DescriptionillnessObj.EiId, Pets.DescriptionillnessObj.Descriptionillness.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                ////}
                ////if (Pets.AnnualcoverlimitObj != null && Pets.AnnualcoverlimitObj.EiId > 0 && Pets.AnnualcoverlimitObj.Annualcoverlimit != null)
                ////{
                ////    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.AnnualcoverlimitObj.EiId, Pets.AnnualcoverlimitObj.Annualcoverlimit.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                ////}
                ////if (Pets.ExcessPetObj != null && Pets.ExcessPetObj.EiId > 0 && Pets.ExcessPetObj.Excess != null)
                ////{
                ////    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.ExcessPetObj.EiId, Pets.ExcessPetObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                ////}
                ////if (Pets.BoardingfeeObj != null && Pets.BoardingfeeObj.EiId > 0 && Pets.BoardingfeeObj.Boardingfee != null)
                ////{
                ////    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.BoardingfeeObj.EiId, Pets.BoardingfeeObj.Boardingfee.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                ////}
                ////if (Pets.AnnuallimitbfObj != null && Pets.AnnuallimitbfObj.EiId > 0 && Pets.AnnuallimitbfObj.Annuallimitbf != null)
                ////{
                ////    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.AnnuallimitbfObj.EiId, Pets.AnnuallimitbfObj.Annuallimitbf.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                ////}
                ////if (Pets.DeathillnessObj != null && Pets.DeathillnessObj.EiId > 0 && Pets.DeathillnessObj.Deathillness != null)
                ////{
                ////    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.DeathillnessObj.EiId, Pets.DeathillnessObj.Deathillness.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                ////}
                ////if (Pets.AnnuallimitdtObj != null && Pets.AnnuallimitdtObj.EiId > 0 && Pets.AnnuallimitdtObj.Annuallimit != null)
                ////{
                ////    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.AnnuallimitdtObj.EiId, Pets.AnnuallimitdtObj.Annuallimit.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                ////}
                ////if (Pets.DeathinjuryObj != null && Pets.DeathinjuryObj.EiId > 0 && Pets.DeathinjuryObj.Deathinjury != null)
                ////{
                ////    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.DeathinjuryObj.EiId, Pets.DeathinjuryObj.Deathinjury.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                ////}
                ////if (Pets.AnnuallimitijObj != null && Pets.AnnuallimitijObj.EiId > 0 && Pets.AnnuallimitijObj.Annuallimitij != null)
                ////{
                ////    db.IT_InsertCustomerQnsData(Pets.CustomerId, Convert.ToInt32(RLSSection.Pet), Pets.AnnuallimitijObj.EiId, Pets.AnnuallimitijObj.Annuallimitij.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                ////}
                Session["unId"] = null;
                Session["profileId"] = null;
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
            return RedirectToAction("TravelCover", "Travel", new { cid = Pets.CustomerId });
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