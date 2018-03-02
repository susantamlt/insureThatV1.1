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
                    if (Policyincllist.Exists(p => p.name == "Pet" || p.name == "Pets"))
                    {
                        if (Session["unId"] == null && Session["profileId"] == null)
                        {
                            Session["unId"] = Policyincllist.Where(p => p.name == "Pet" || p.name == "Pets" ).Select(p => p.UnitId).First();
                            Session["profileId"] = Policyincllist.Where(p => p.name == "Pet" || p.name == "Pets").Select(p => p.ProfileId).First();
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
            if ( PcId != null && PcId.HasValue)
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
                if (unitdetails.SectionData != null && unitdetails.SectionData.ValueData!=null)
                {
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.AnnualcoverlimitObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.AnnualcoverlimitObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.AnnualcoverlimitObj.Annualcoverlimit = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.AnnuallimitbfObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.AnnuallimitbfObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.AnnuallimitbfObj.Annuallimitbf = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.AnnuallimitdtObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.AnnuallimitdtObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.AnnuallimitdtObj.Annuallimit = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.AnnuallimitijObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.AnnuallimitijObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.AnnuallimitijObj.Annuallimitij = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.BoardingfeeObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.BoardingfeeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.BoardingfeeObj.Boardingfee = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.BreedObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.BreedObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.BreedObj.Breed = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.DatebirthObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.DatebirthObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.DatebirthObj.Datebirth = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.DeathillnessObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.DeathillnessObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.DeathillnessObj.Deathillness = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.DeathinjuryObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.DeathinjuryObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.DeathinjuryObj.Deathinjury = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.DescriptionillnessObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.DescriptionillnessObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.DescriptionillnessObj.Descriptionillness = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.ExcessPetObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.ExcessPetObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.ExcessPetObj.Excess = val;
                    }
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == Pets.ImposedpetsObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == Pets.ImposedpetsObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    Pets.ImposedpetsObj.Imposed = val;
                    //}
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.NameObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.NameObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.NameObj.Name = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.OtherbreedObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.OtherbreedObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.OtherbreedObj.Otherbreed = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.PreexistingObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.PreexistingObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.PreexistingObj.Preexisting = val;
                    }
                    //if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.PremiumptsObj.EiId))
                    //{
                    //    string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.PremiumptsObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    Pets.PremiumptsObj.Premium = val;
                    //}
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == Pets.SpeciesObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == Pets.SpeciesObj.EiId).Select(p => p.Value).FirstOrDefault();
                        Pets.SpeciesObj.Species = val;
                    }
                }
            }
          
            if (unitdetails!=null && unitdetails.ReferralList != null)
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
            if(cid!=null && cid>0)
            {
                Pets.CustomerId = cid.Value;
            }
            if(PcId!=null && PcId>0)
            {
                Pets.PcId = PcId;
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
         
            if (cid != null)
            {
                ViewBag.cid = cid;
                Pets.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = Pets.CustomerId;
            }
            string actionname = null;
            string controllername = null;
            if (Session["Actname"] != null)
            {
                actionname = Session["Actname"].ToString();
            }
            if (Session["controller"] != null)
            {
                controllername = Session["controller"].ToString();
            }
            //if (actionname != null && controllername != null)
            //{
            //    return RedirectToAction(actionname, controllername, new { cid = Pets.CustomerId, PcId = Pets.PcId });
            //}
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