using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using InsureThatAPI.CommonMethods;
using Newtonsoft.Json;
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;

namespace InsureThatAPI.Controllers
{
    public class BoatController : Controller
    {
        // GET: Boat
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> BoatDetails(int? cid, int? PcId)
        {
            NewPolicyDetailsClass BoatDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> TypeBoatList = new List<SelectListItem>();
            TypeBoatList = BoatDetailsmodel.TypeOfBoatList();
            List<SelectListItem> HullMeterialsList = new List<SelectListItem>();
            List<SelectListItem> Addresslist = new List<SelectListItem>();
            Addresslist.Add(new SelectListItem { Text = "ABC", Value = "1" });
            Addresslist.Add(new SelectListItem { Text = "XYZ", Value = "2" });
            HullMeterialsList = BoatDetailsmodel.HullMeterialList();
            List<SelectListItem> FuelTypeList = new List<SelectListItem>();
            FuelTypeList = BoatDetailsmodel.FuelType();
            List<SelectListItem> MotorPositionList = new List<SelectListItem>();
            MotorPositionList = BoatDetailsmodel.MotorPosition();
            List<SelectListItem> DriveTypeList = new List<SelectListItem>();
            DriveTypeList = BoatDetailsmodel.DriveType();
            List<SelectListItem> NameBOLists = new List<SelectListItem>();
            NameBOLists = BoatDetailsmodel.BoatOperatorLists();
            List<SelectListItem> ExcessList = new List<SelectListItem>();
            ExcessList = BoatDetailsmodel.excessRate();
            BoatDetails BoatDetails = new BoatDetails();
            AddressData add = new AddressData();
            NewPolicyDetailsClass MCVehicleDescriptionmodel = new NewPolicyDetailsClass();
            List<AddressData> AddressList = new List<AddressData>();
            AddressList = MCVehicleDescriptionmodel.MCADAddress();
            BoatDetails.AdaddressObj = new MCADAddress();
            BoatDetails.AdaddressObj.AddressList = new List<AddressData>();
            BoatDetails.AdaddressObj.AddressList = AddressList;
            BoatDetails.AdaddressList = new List<SelectListItem>();
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                BoatDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = BoatDetails.CustomerId;
            }
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            NewPolicyDetailsClass commonmethods = new NewPolicyDetailsClass();

            string apikey = null;
            bool policyinclusion = false;
            if (Session["apiKey"] != null)
            {
                apikey = Session["apiKey"].ToString();
                BoatDetails.ApiKey = Session["apiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            string policyid = null;
            CommonUseFunctionClass cmn = new CommonUseFunctionClass();
            BoatDetails.NewSections = new List<string>();
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                policyid = PcId.ToString();
                BoatDetails.PcId = PcId.Value;
                var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
                BoatDetails.PolicyId = policyid;
                policyinclusion = policyinclusions.Exists(p => p.Name == "Boat");          
                BoatDetails.PolicyInclusion = new List<usp_GetUnit_Result>();
                BoatDetails.PolicyInclusion = policyinclusions;
                BoatDetails.NewSections = cmn.NewSectionP(policyinclusions);
            }
            var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
            if (Session["Policyinclustions"] != null)
            {
                #region Policy Selected or not testing
                List<SessionModel> PolicyInclustions = new List<SessionModel>();
                BoatDetails.PolicyInclusions = new List<SessionModel>();

                BoatDetails.PolicyInclusions = Policyincllist;
                BoatDetails.NewSections = cmn.NewSectionHome(BoatDetails.PolicyInclusions);
                if (Policyincllist != null)
                {
                    if (Policyincllist != null)
                    {
                        if (Policyincllist.Exists(p => p.name == "Boat"))
                        {

                        }
                        else if (Policyincllist.Exists(p => p.name == "Pet" || p.name == "Pets"))
                        {
                            return RedirectToAction("PetsCover", "Pets", new { cid = cid });
                        }
                        else if (Policyincllist.Exists(p => p.name == "Travel"))
                        {
                            return RedirectToAction("TravelCover", "Travel", new { cid = cid });
                        }
                        if (Policyincllist.Exists(p => p.name == "Boat"))
                        {
                            if (Session["unId"] == null && Session["profileId"] == null)
                            {
                                Session["unId"] = Policyincllist.Where(p => p.name == "Boat").Select(p => p.UnitId).First();
                                Session["profileId"] = Policyincllist.Where(p => p.name == "Boat").Select(p => p.ProfileId).First();
                            }
                        }
                        else
                        {
                            return RedirectToAction("DisclosureDetails", "Disclosure", new { cid = cid, PcId = PcId });
                        }
                    }
                }
                #endregion
            }
            #region BoatDetails
            BoatDetails.BoatnameObj = new BoatNames();
            BoatDetails.BoatnameObj.EiId = 61103;
            BoatDetails.RegistrationdetailObj = new RegistrationDetails();
            BoatDetails.RegistrationdetailObj.EiId = 61105;
            BoatDetails.MakeObj = new Makes();
            BoatDetails.MakeObj.EiId = 61107;
            BoatDetails.ModelbObj = new ModelsB();
            BoatDetails.ModelbObj.EiId = 61109;
            BoatDetails.YearmanufactureObj = new YearOfManufacture();
            BoatDetails.YearmanufactureObj.EiId = 61111;
            BoatDetails.LengthmetreObj = new LengthInMetres();
            BoatDetails.LengthmetreObj.EiId = 61113;
            BoatDetails.TypeboatObj = new TypeOfBoat();
            BoatDetails.TypeboatObj.TypeList = TypeBoatList;
            BoatDetails.TypeboatObj.EiId = 61117;
            BoatDetails.HullmeterialObj = new HullMeterials();
            BoatDetails.HullmeterialObj.MeterialList = HullMeterialsList;
            BoatDetails.HullmeterialObj.EiId = 61119;
            BoatDetails.SpeedObj = new Speeds();
            BoatDetails.SpeedObj.EiId = 61129;
            BoatDetails.DetectorObj = new Detectors();
            BoatDetails.DetectorObj.EiId = 61131;
            BoatDetails.MooringstorageObj = new TypeOfMooringStorage();
            BoatDetails.MooringstorageObj.EiId = 61133;
            BoatDetails.otherpleasedetailObj = new OtherPleaseDetails();
            BoatDetails.otherpleasedetailObj.EiId = 0;
            BoatDetails.AddressObj = new AddressesBD();
            BoatDetails.AddressObj.AddressList = Addresslist;
            BoatDetails.AddressObj.EiId = 0;
            #endregion
            #region MotorsDetails
            BoatDetails.YearmanufactureMDObj = new YearOfManufacture();
            BoatDetails.YearmanufactureMDObj.EiId = 61149;
            BoatDetails.MakemodelObj = new MakeAndModel();
            BoatDetails.MakemodelObj.EiId = 61151;
            BoatDetails.SerialnumberObj = new SerialNumbersMD();
            BoatDetails.SerialnumberObj.EiId = 61153;
            BoatDetails.FueltypeObj = new FuelType();
            BoatDetails.FueltypeObj.FualList = FuelTypeList;
            BoatDetails.FueltypeObj.EiId = 61155;
            BoatDetails.MotorpositionObj = new MotorPosition();
            BoatDetails.MotorpositionObj.MotorList = MotorPositionList;
            BoatDetails.MotorpositionObj.EiId = 61157;
            BoatDetails.DetectorMDObj = new Detectors();
            BoatDetails.DetectorMDObj.EiId = 61131;
            BoatDetails.DrivetypeObj = new DriveType();
            BoatDetails.DrivetypeObj.DriveList = DriveTypeList;
            BoatDetails.DrivetypeObj.EiId = 61159;
            BoatDetails.PowerObj = new Powers();
            BoatDetails.PowerObj.EiId = 61161;
            BoatDetails.MarketvalueObj = new MarketValues();
            BoatDetails.MarketvalueObj.EiId = 61163;
            #endregion
            #region BoatOperater
            BoatDetails.NameboObj = new NameBOs();
            BoatDetails.NameboObj.NameBOList = NameBOLists;
            BoatDetails.NameboObj.EiId = 61187;
            BoatDetails.YearsexperienceObj = new YearsExperienced();
            BoatDetails.YearsexperienceObj.EiId = 61189;
            BoatDetails.TypesboatObj = new TypesofBoat();
            BoatDetails.TypesboatObj.EiId = 61191;
            #endregion
            #region CoverDetails
            BoatDetails.MarketvalueCDObj = new MarketValues();
            BoatDetails.MarketvalueCDObj.EiId = 61207;
            BoatDetails.MotorvalueObj = new MotorValues();
            BoatDetails.MotorvalueObj.EiId = 61209;
            BoatDetails.AccessorydescriptionObj = new AccessoryDescription();
            BoatDetails.AccessorydescriptionObj.EiId = 61213;
            BoatDetails.AccessorysuminsureObj = new AccessorySumInsured();
            BoatDetails.AccessorysuminsureObj.EiId = 61215;
            BoatDetails.Totalsumassured = "0";
            BoatDetails.Coverforaccessories = "";
            BoatDetails.Totalcoverboat = "";
            BoatDetails.LiabilityObj = new LiabilityCD();
            BoatDetails.LiabilityObj.EiId = 61223;
            BoatDetails.ExcesscdObj = new ExcessCD();
            BoatDetails.ExcesscdObj.ExcessList = ExcessList;
            BoatDetails.ExcesscdObj.EiId = 61233;
            BoatDetails.FreeperiodObj = new ClaimFreePeriod();
            BoatDetails.FreeperiodObj.EiId = 61235;
            BoatDetails.NodiscountObj = new NoClaimDiscount();
            BoatDetails.NodiscountObj.EiId = 61237;
            #endregion
            #region option
            BoatDetails.WaterwayObj = new Waterways();
            BoatDetails.WaterwayObj.EiId = 61253;
            BoatDetails.LimitseawardObj = new LimitSeawardTravel();
            BoatDetails.LimitseawardObj.EiId = 61255;
            BoatDetails.SailboatObj = new SailBoats();
            BoatDetails.SailboatObj.EiId = 61259;
            #endregion
            #region InterestedPartiesBoat
            BoatDetails.InstitutionsObj = new NameOfInstitutions();
            BoatDetails.InstitutionsObj.EiId = 61277;
            BoatDetails.LocationObj = new LocationsIPB();
            BoatDetails.LocationObj.EiId = 61279;
            #endregion
            int unid = 0;
            int profileid = 0;
            if (Session["unId"] != null && Session["profileId"] != null)
            {
                unid = Convert.ToInt32(Session["unId"]);
                Session["unId"] = unid;
              
            }
            if(Session["profileId"] != null)
            {               
                profileid = Convert.ToInt32(Session["profileId"]);
                Session["profileId"] = profileid;
            }
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (policyinclusion == true && PcId != null && PcId.HasValue)
            {
                HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                if (EmpResponse != null)
                {
                    unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                }
            }
            else
            {
                  if (PcId == null && Session["unId"] != null && Session["profileId"] != null)
                {
                    HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                    var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    }
                }
               else if (PcId == null && Session["unId"] == null && (Session["profileId"] == null || profileid == 0))
                {
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Boat&SectionUnId=&ProfileUnId=0");
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                      
                        if (unitdetails != null && unitdetails.ErrorMessage != null && unitdetails.ErrorMessage.Count() > 0)
                        {
                            bool exists = BoatDetails.PolicyInclusions.Exists(p => p.name == "Boat");
                            if (exists == true)
                            {
                                List<SessionModel> values = new List<SessionModel>();
                                values = (List<SessionModel>)Session["Policyinclustions"];
                                for (int k = 0; k < values.Count(); k++)
                                {
                                    if (values[k].name == "Boat" && values[k].UnitId == null && values[k].ProfileId == null)
                                    {
                                        values.RemoveAt(k);
                                    }
                                }
                                Session["Policyinclustions"] = values;
                            }
                            var errormessage = "First please provide cover for Home Buildings.";
                            if (unitdetails.ErrorMessage.Contains(errormessage))
                            {
                                TempData["Error"] = errormessage;
                                return RedirectToAction("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = PcId });
                            }
                        }
                        if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Boat"))
                        {
                            var policyhomelist = Policyincllist.FindAll(p => p.name == "Boat").ToList();
                            if (policyhomelist != null && policyhomelist.Count() > 0)
                            {
                                if (Policyincllist.FindAll(p => p.name == "Boat").Exists(p => p.UnitId == null))
                                {
                                    Policyincllist.FindAll(p => p.name == "Boat").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;
                                }
                                if (Policyincllist.FindAll(p => p.name == "Boat").Exists(p => p.ProfileId == null))
                                {
                                    Policyincllist.FindAll(p => p.name == "Boat").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                            }
                            else
                            {
                                Policyincllist.FindAll(p => p.name == "Boat").First().UnitId = unitdetails.SectionData.UnId;
                                Policyincllist.FindAll(p => p.name == "Boat").First().ProfileId = unitdetails.SectionData.ProfileUnId;
                            }
                            BoatDetails.PolicyInclusions = Policyincllist;
                            Session["Policyinclustions"] = Policyincllist;

                        }
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
                    if (unitdetails.AddressList != null && unitdetails.AddressList.Count > 0)
                    {
                        BoatDetails.AdaddressObj.AddressList = unitdetails.AddressList;
                        if (BoatDetails.AdaddressObj.AddressList != null && BoatDetails.AdaddressObj.AddressList.Count() > 0)
                        {
                            for (int i = 0; i < BoatDetails.AdaddressObj.AddressList.Count(); i++)
                            {
                                BoatDetails.AdaddressList.Add(new SelectListItem { Value = BoatDetails.AdaddressObj.AddressList[i].AddressID.ToString(), Text = BoatDetails.AdaddressObj.AddressList[i].AddressLine1 + ", " + BoatDetails.AdaddressObj.AddressList[i].Suburb + ", " + BoatDetails.AdaddressObj.AddressList[i].State + ", " + BoatDetails.AdaddressObj.AddressList[i].Postcode });
                            }
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.BoatnameObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.BoatnameObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.BoatnameObj.Name = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.AddressObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.AddressObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.AddressObj.Address = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.DetectorObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.DetectorObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.DetectorObj.Detector = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.DrivetypeObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.DrivetypeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.DrivetypeObj.Drivetype = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.ExcesscdObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.ExcesscdObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.ExcesscdObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.FreeperiodObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.FreeperiodObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.FreeperiodObj.Freeperiod = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.NameboObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.NameboObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            BoatDetails.NameboObj.Name = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == BoatDetails.NameboObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var NameLsList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.NameboObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < NameLsList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 61187;
                                vds.Element.ItId = NameLsList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.NameboObj.EiId && p.Element.ItId == NameLsList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            BoatDetails.NameboObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.YearsexperienceObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.YearsexperienceObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            BoatDetails.YearsexperienceObj.Year = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == BoatDetails.YearsexperienceObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var YearLsList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.YearsexperienceObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < YearLsList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 61189;
                                vds.Element.ItId = YearLsList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.YearsexperienceObj.EiId && p.Element.ItId == YearLsList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            BoatDetails.YearsexperienceObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.TypesboatObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.TypesboatObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            BoatDetails.TypesboatObj.Type = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == BoatDetails.TypesboatObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var TypeLsList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.TypesboatObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < TypeLsList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 61191;
                                vds.Element.ItId = TypeLsList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.TypesboatObj.EiId && p.Element.ItId == TypeLsList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            BoatDetails.TypesboatObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.AccessorydescriptionObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.AccessorydescriptionObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            BoatDetails.AccessorydescriptionObj.Description = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == BoatDetails.AccessorydescriptionObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var DescriptionLsList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.AccessorydescriptionObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < DescriptionLsList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 61213;
                                vds.Element.ItId = DescriptionLsList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.AccessorydescriptionObj.EiId && p.Element.ItId == DescriptionLsList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            BoatDetails.AccessorydescriptionObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.AccessorysuminsureObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.AccessorysuminsureObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            BoatDetails.AccessorysuminsureObj.Suminsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == BoatDetails.AccessorysuminsureObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var SuminsuredList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.AccessorysuminsureObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < SuminsuredList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 61215;
                                vds.Element.ItId = SuminsuredList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.AccessorysuminsureObj.EiId && p.Element.ItId == SuminsuredList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            BoatDetails.AccessorysuminsureObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.InstitutionsObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.InstitutionsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            BoatDetails.InstitutionsObj.Name = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == BoatDetails.InstitutionsObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var NameIPList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.InstitutionsObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < NameIPList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 61277;
                                vds.Element.ItId = NameIPList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.InstitutionsObj.EiId && p.Element.ItId == NameIPList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            BoatDetails.InstitutionsObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.LocationObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.LocationObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            BoatDetails.LocationObj.Location = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == BoatDetails.LocationObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var LocationIPList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.LocationObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < LocationIPList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 61279;
                                vds.Element.ItId = LocationIPList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.LocationObj.EiId && p.Element.ItId == LocationIPList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            BoatDetails.LocationObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.HullmeterialObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.HullmeterialObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.HullmeterialObj.Meterials = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.LengthmetreObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.LengthmetreObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.LengthmetreObj.Metres = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.LiabilityObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.LiabilityObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.LiabilityObj.Liability = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.LimitseawardObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.LimitseawardObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.LimitseawardObj.Seaward = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.MakemodelObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.MakemodelObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.MakemodelObj.Makemodel = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.MakeObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.MakeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.MakeObj.Make = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.MarketvalueCDObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.MarketvalueCDObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.MarketvalueCDObj.Marketvalue = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.MarketvalueObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.MarketvalueObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.MarketvalueObj.Marketvalue = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.ModelbObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.ModelbObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.ModelbObj.Modelb = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.MooringstorageObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.MooringstorageObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.MooringstorageObj.Mooringorstorage = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.MotorpositionObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.MotorpositionObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.MotorpositionObj.Position = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.MotorvalueObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.MotorvalueObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.MotorvalueObj.Motorvalue = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.NodiscountObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.NodiscountObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.NodiscountObj.Nodiscount = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.otherpleasedetailObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.otherpleasedetailObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.otherpleasedetailObj.Other = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.PowerObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.PowerObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.PowerObj.Power = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.RegistrationdetailObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.RegistrationdetailObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.RegistrationdetailObj.Registration = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.SailboatObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.SailboatObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.SailboatObj.Sailboat = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.SerialnumberObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.SerialnumberObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.SerialnumberObj.Serialnumber = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.SpeedObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.SpeedObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.SpeedObj.Speed = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.TypeboatObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.TypeboatObj.EiId).Select(p => p.Value).FirstOrDefault();
                        BoatDetails.TypeboatObj.Type = val;
                    }
                }
            }
            if (unitdetails!=null && unitdetails.ReferralList != null)
            {
                BoatDetails.ReferralList = unitdetails.ReferralList;
                BoatDetails.ReferralList.Replace("&nbsp;&nbsp;&nbsp;&nbsp", "");
                BoatDetails.Referels = new List<string>();
                string[] delim = { "<br/>" };

                string[] spltd = BoatDetails.ReferralList.Split(delim, StringSplitOptions.None);
                if (spltd != null && spltd.Count() > 0)
                {
                    for (int i = 0; i < spltd.Count(); i++)
                    {
                        BoatDetails.Referels.Add(spltd[i].Replace("&nbsp;&nbsp;&nbsp;&nbsp", " "));
                    }
                }

            }
            Session["Controller"] = "Boat";
            Session["ActionName"] = "BoatDetails";
            return View(BoatDetails);
        }
        [HttpPost]
        public ActionResult BoatDetails(int? cid, BoatDetails BoatDetails)
        {
            NewPolicyDetailsClass BoatDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> TypeBoatList = new List<SelectListItem>();
            TypeBoatList = BoatDetailsmodel.TypeOfBoatList();
            List<SelectListItem> HullMeterialsList = new List<SelectListItem>();
            HullMeterialsList = BoatDetailsmodel.HullMeterialList();
            BoatDetails.TypeboatObj.TypeList = TypeBoatList;
            BoatDetails.HullmeterialObj.MeterialList = HullMeterialsList;
            if (cid != null)
            {
                ViewBag.cid = cid;
                BoatDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = BoatDetails.CustomerId;
            }
            string policyid = null;
            List<SelectListItem> Addresslist = new List<SelectListItem>();
            Addresslist.Add(new SelectListItem { Text = "ABC", Value = "1" });
            Addresslist.Add(new SelectListItem { Text = "XYZ", Value = "2" });
            BoatDetails.AddressObj.AddressList = Addresslist;
            var db = new MasterDataEntities();
 
                Session["unId"] = null;
                Session["profileId"] = null;
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
                //    return RedirectToAction(actionname, controllername, new { cid = BoatDetails.CustomerId, PcId = BoatDetails.PcId });
                //}
                return RedirectToAction("PetsCover", "Pets", new { cid = BoatDetails.CustomerId, PcId = BoatDetails.PcId });
               // return RedirectToAction("PetsCover", "Pets", new { cid = BoatDetails.CustomerId });
            
            return RedirectToAction("PetsCover", "Pets", new { cid = BoatDetails.CustomerId });
        }
        //[HttpGet]
        //public ActionResult MotorDetails(int? cid)
        //{
        //    NewPolicyDetailsClass MotorDetailsmodel = new NewPolicyDetailsClass();
        //    List<SelectListItem> FuelTypeList = new List<SelectListItem>();
        //    FuelTypeList = MotorDetailsmodel.FuelType();
        //    List<SelectListItem> MotorPositionList = new List<SelectListItem>();
        //    MotorPositionList = MotorDetailsmodel.MotorPosition();
        //    List<SelectListItem> DriveTypeList = new List<SelectListItem>();
        //    DriveTypeList = MotorDetailsmodel.DriveType();
        //    MotorDetails MotorDetails = new MotorDetails();
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        MotorDetails.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = MotorDetails.CustomerId;
        //    }
        //    MotorDetails.YearmanufactureObj = new YearOfManufacture();
        //    MotorDetails.YearmanufactureObj.EiId = 61149;
        //    MotorDetails.MakemodelObj = new MakeAndModel();
        //    MotorDetails.MakemodelObj.EiId = 61151;
        //    MotorDetails.SerialnumberObj = new SerialNumbersMD();
        //    MotorDetails.SerialnumberObj.EiId = 61153;
        //    MotorDetails.FueltypeObj = new FuelType();
        //    MotorDetails.FueltypeObj.FualList = FuelTypeList;
        //    MotorDetails.FueltypeObj.EiId = 61155;
        //    MotorDetails.MotorpositionObj = new MotorPosition();
        //    MotorDetails.MotorpositionObj.MotorList = MotorPositionList;
        //    MotorDetails.MotorpositionObj.EiId = 61157;
        //    MotorDetails.DetectorObj = new Detectors();
        //    MotorDetails.DetectorObj.EiId = 0;
        //    MotorDetails.DrivetypeObj = new DriveType();
        //    MotorDetails.DrivetypeObj.DriveList = DriveTypeList;
        //    MotorDetails.DrivetypeObj.EiId = 61159;
        //    MotorDetails.PowerObj = new Powers();
        //    MotorDetails.PowerObj.EiId = 61161;
        //    MotorDetails.MarketvalueObj = new MarketValues();
        //    MotorDetails.MarketvalueObj.EiId = 0;
        //    if (Session["completionTrackB"] != null)
        //    {
        //        Session["completionTrackB"] = Session["completionTrackB"];
        //        MotorDetails.CompletionTrackB = Session["completionTrackB"].ToString();
        //    }
        //    else
        //    {
        //        Session["completionTrackB"] = "0-0-0-0-0-0"; ;
        //        MotorDetails.CompletionTrackB = Session["completionTrackB"].ToString();
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(RLSSection.Boat), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
        //    if (details != null && details.Any())
        //    {
        //        if (details.Exists(q => q.QuestionId == MotorDetails.YearmanufactureObj.EiId))
        //        {
        //            MotorDetails.YearmanufactureObj.Year = Convert.ToString(details.Where(q => q.QuestionId == MotorDetails.YearmanufactureObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == MotorDetails.MakemodelObj.EiId))
        //        {
        //            MotorDetails.MakemodelObj.Makemodel = Convert.ToString(details.Where(q => q.QuestionId == MotorDetails.MakemodelObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == MotorDetails.SerialnumberObj.EiId))
        //        {
        //            MotorDetails.SerialnumberObj.Serialnumber = Convert.ToString(details.Where(q => q.QuestionId == MotorDetails.SerialnumberObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == MotorDetails.FueltypeObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == MotorDetails.FueltypeObj.EiId).FirstOrDefault();
        //            MotorDetails.FueltypeObj.Type = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //        if (details.Exists(q => q.QuestionId == MotorDetails.MotorpositionObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == MotorDetails.MotorpositionObj.EiId).FirstOrDefault();
        //            MotorDetails.MotorpositionObj.Position = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //        if (details.Exists(q => q.QuestionId == MotorDetails.DetectorObj.EiId))
        //        {
        //            MotorDetails.DetectorObj.Detector = Convert.ToString(details.Where(q => q.QuestionId == MotorDetails.DetectorObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == MotorDetails.DrivetypeObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == MotorDetails.DrivetypeObj.EiId).FirstOrDefault();
        //            MotorDetails.DrivetypeObj.Drivetype = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //        if (details.Exists(q => q.QuestionId == MotorDetails.PowerObj.EiId))
        //        {
        //            MotorDetails.PowerObj.Power = Convert.ToString(details.Where(q => q.QuestionId == MotorDetails.PowerObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == MotorDetails.MarketvalueObj.EiId))
        //        {
        //            MotorDetails.MarketvalueObj.Marketvalue = Convert.ToString(details.Where(q => q.QuestionId == MotorDetails.MarketvalueObj.EiId).FirstOrDefault().Answer);
        //        }
        //    }
        //    return View(MotorDetails);
        //}
        //[HttpPost]
        //public ActionResult MotorDetails(int? cid, MotorDetails MotorDetails)
        //{
        //    NewPolicyDetailsClass MotorDetailsmodel = new NewPolicyDetailsClass();
        //    List<SelectListItem> FuelTypeList = new List<SelectListItem>();
        //    FuelTypeList = MotorDetailsmodel.FuelType();
        //    List<SelectListItem> MotorPositionList = new List<SelectListItem>();
        //    MotorPositionList = MotorDetailsmodel.MotorPosition();
        //    List<SelectListItem> DriveTypeList = new List<SelectListItem>();
        //    DriveTypeList = MotorDetailsmodel.DriveType();
        //    MotorDetails.FueltypeObj.FualList = FuelTypeList;
        //    MotorDetails.MotorpositionObj.MotorList = MotorPositionList;
        //    MotorDetails.DrivetypeObj.DriveList = DriveTypeList;
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        MotorDetails.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = MotorDetails.CustomerId;
        //    }
        //    string policyid = null;
        //    var db = new MasterDataEntities();
        //    if (cid.HasValue && cid > 0)
        //    {
        //        if (MotorDetails.YearmanufactureObj != null && MotorDetails.YearmanufactureObj.EiId > 0 && MotorDetails.YearmanufactureObj.Year != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), MotorDetails.YearmanufactureObj.EiId, MotorDetails.YearmanufactureObj.Year.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MotorDetails.MakemodelObj != null && MotorDetails.MakemodelObj.EiId > 0 && MotorDetails.MakemodelObj.Makemodel != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), MotorDetails.MakemodelObj.EiId, MotorDetails.MakemodelObj.Makemodel.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MotorDetails.SerialnumberObj != null && MotorDetails.SerialnumberObj.EiId > 0 && MotorDetails.SerialnumberObj.Serialnumber != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), MotorDetails.SerialnumberObj.EiId, MotorDetails.SerialnumberObj.Serialnumber.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MotorDetails.FueltypeObj != null && MotorDetails.FueltypeObj.EiId > 0 && MotorDetails.FueltypeObj.Type != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), MotorDetails.FueltypeObj.EiId, MotorDetails.FueltypeObj.Type.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MotorDetails.MotorpositionObj != null && MotorDetails.MotorpositionObj.EiId > 0 && MotorDetails.MotorpositionObj.Position != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), MotorDetails.MotorpositionObj.EiId, MotorDetails.MotorpositionObj.Position.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MotorDetails.DetectorObj != null && MotorDetails.DetectorObj.EiId > 0 && MotorDetails.DetectorObj.Detector.ToString() != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), MotorDetails.DetectorObj.EiId, MotorDetails.DetectorObj.Detector.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MotorDetails.DrivetypeObj != null && MotorDetails.DrivetypeObj.EiId > 0 && MotorDetails.DrivetypeObj.Drivetype != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), MotorDetails.DrivetypeObj.EiId, MotorDetails.DrivetypeObj.Drivetype.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MotorDetails.PowerObj != null && MotorDetails.PowerObj.EiId > 0 && MotorDetails.PowerObj.Power != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), MotorDetails.PowerObj.EiId, MotorDetails.PowerObj.Power.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MotorDetails.MarketvalueObj != null && MotorDetails.MarketvalueObj.EiId > 0 && MotorDetails.MarketvalueObj.Marketvalue != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), MotorDetails.MarketvalueObj.EiId, MotorDetails.MarketvalueObj.Marketvalue.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Session["completionTrackB"] != null)
        //        {
        //            Session["completionTrackB"] = Session["completionTrackB"];
        //            MotorDetails.CompletionTrackB = Session["completionTrackB"].ToString();
        //            if (MotorDetails.CompletionTrackB != null)
        //            {
        //                string Completionstring = string.Empty;
        //                char[] arr = MotorDetails.CompletionTrackB.ToCharArray();
        //                for (int i = 0; i < arr.Length; i++)
        //                {
        //                    char a = arr[i];
        //                    if (i == 2)
        //                    {
        //                        a = '1';
        //                    }
        //                    Completionstring = Completionstring + a;
        //                }
        //                Session["completionTrackB"] = Completionstring;
        //                MotorDetails.CompletionTrackB = Completionstring;
        //            }
        //        }
        //        else
        //        {
        //            Session["completionTrackB"] = "0-1-0-0-0-0"; ;
        //            MotorDetails.CompletionTrackB = Session["completionTrackB"].ToString();
        //        }
        //        return RedirectToAction("BoatOperator", new { cid = MotorDetails.CustomerId });
        //    }
        //    return View(MotorDetails);
        //}
        //[HttpGet]
        //public ActionResult BoatOperator(int? cid)
        //{
        //    NewPolicyDetailsClass BoatOperatorListmodel = new NewPolicyDetailsClass();
        //    List<SelectListItem> NameBOLists = new List<SelectListItem>();
        //    NameBOLists = BoatOperatorListmodel.BoatOperatorLists();
        //    BoatOperator BoatOperator = new BoatOperator();
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        BoatOperator.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = BoatOperator.CustomerId;
        //    }
        //    BoatOperator.NameboObj = new NameBOs();
        //    BoatOperator.NameboObj.NameBOList = NameBOLists;
        //    BoatOperator.NameboObj.EiId = 61187;
        //    BoatOperator.YearsexperienceObj = new YearsExperienced();
        //    BoatOperator.YearsexperienceObj.EiId = 61189;
        //    BoatOperator.TypesboatObj = new TypesofBoat();
        //    BoatOperator.TypesboatObj.EiId = 61191;
        //    if (Session["completionTrackB"] != null)
        //    {
        //        Session["completionTrackB"] = Session["completionTrackB"];
        //        BoatOperator.CompletionTrackB = Session["completionTrackB"].ToString();
        //    }
        //    else
        //    {
        //        Session["completionTrackB"] = "0-0-0-0-0-0"; ;
        //        BoatOperator.CompletionTrackB = Session["completionTrackB"].ToString();
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Boat), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
        //    if (details != null && details.Any())
        //    {
        //        if (details.Exists(q => q.QuestionId == BoatOperator.NameboObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == BoatOperator.NameboObj.EiId).FirstOrDefault();
        //            BoatOperator.NameboObj.Name = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //        if (details.Exists(q => q.QuestionId == BoatOperator.YearsexperienceObj.EiId))
        //        {
        //            BoatOperator.YearsexperienceObj.Year = Convert.ToString(details.Where(q => q.QuestionId == BoatOperator.YearsexperienceObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == BoatOperator.TypesboatObj.EiId))
        //        {
        //            BoatOperator.TypesboatObj.Type = Convert.ToString(details.Where(q => q.QuestionId == BoatOperator.TypesboatObj.EiId).FirstOrDefault().Answer);
        //        }
        //    }
        //    return View(BoatOperator);
        //}
        //[HttpPost]
        //public ActionResult BoatOperator(int? cid, BoatOperator BoatOperator)
        //{
        //    NewPolicyDetailsClass BoatOperatorListmodel = new NewPolicyDetailsClass();
        //    List<SelectListItem> NameBOLists = new List<SelectListItem>();
        //    NameBOLists = BoatOperatorListmodel.BoatOperatorLists();
        //    BoatOperator.NameboObj.NameBOList = NameBOLists;
        //    var db = new MasterDataEntities();
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        BoatOperator.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = BoatOperator.CustomerId;
        //    }
        //    string policyid = null;
        //    if (cid.HasValue && cid > 0)
        //    {
        //        if (BoatOperator.NameboObj != null && BoatOperator.NameboObj.EiId > 0 && BoatOperator.NameboObj.Name != null)
        //        {
        //            db.IT_InsertCustomerQnsData(BoatOperator.CustomerId, Convert.ToInt32(RLSSection.Boat), BoatOperator.NameboObj.EiId, BoatOperator.NameboObj.Name.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (BoatOperator.YearsexperienceObj != null && BoatOperator.YearsexperienceObj.EiId > 0 && BoatOperator.YearsexperienceObj.Year != null)
        //        {
        //            db.IT_InsertCustomerQnsData(BoatOperator.CustomerId, Convert.ToInt32(RLSSection.Boat), BoatOperator.YearsexperienceObj.EiId, BoatOperator.YearsexperienceObj.Year.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (BoatOperator.YearsexperienceObj != null && BoatOperator.YearsexperienceObj.EiId > 0 && BoatOperator.TypesboatObj.Type != null)
        //        {
        //            db.IT_InsertCustomerQnsData(BoatOperator.CustomerId, Convert.ToInt32(RLSSection.Boat), BoatOperator.TypesboatObj.EiId, BoatOperator.TypesboatObj.Type.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Session["completionTrackB"] != null)
        //        {
        //            Session["completionTrackB"] = Session["completionTrackB"];
        //            BoatOperator.CompletionTrackB = Session["completionTrackB"].ToString();
        //            if (BoatOperator.CompletionTrackB != null)
        //            {
        //                string Completionstring = string.Empty;
        //                char[] arr = BoatOperator.CompletionTrackB.ToCharArray();
        //                for (int i = 0; i < arr.Length; i++)
        //                {
        //                    char a = arr[i];
        //                    if (i == 4)
        //                    {
        //                        a = '1';
        //                    }
        //                    Completionstring = Completionstring + a;
        //                }
        //                Session["completionTrackB"] = Completionstring;
        //                BoatOperator.CompletionTrackB = Completionstring;
        //            }
        //        }
        //        else
        //        {
        //            Session["completionTrackB"] = "0-0-1-0-0-0"; ;
        //            BoatOperator.CompletionTrackB = Session["completionTrackB"].ToString();
        //        }
        //        return RedirectToAction("CoverDetails", new { cid = BoatOperator.CustomerId });
        //    }
        //    return View(BoatOperator);
        //}
        //[HttpGet]
        //public ActionResult CoverDetails(int? cid)
        //{
        //    NewPolicyDetailsClass CoverDetailsmodel = new NewPolicyDetailsClass();
        //    List<SelectListItem> ExcessList = new List<SelectListItem>();

        //    ExcessList = CoverDetailsmodel.excessRate();
        //    CoverDetails CoverDetails = new CoverDetails();
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        CoverDetails.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = CoverDetails.CustomerId;
        //    }
        //    CoverDetails.MarketvalueObj = new MarketValues();
        //    CoverDetails.MarketvalueObj.EiId = 61207;
        //    CoverDetails.MotorvalueObj = new MotorValues();
        //    CoverDetails.MotorvalueObj.EiId = 61209;
        //    CoverDetails.AccessorydescriptionObj = new AccessoryDescription();
        //    CoverDetails.AccessorydescriptionObj.EiId = 61213;
        //    CoverDetails.AccessorysuminsureObj = new AccessorySumInsured();
        //    CoverDetails.AccessorysuminsureObj.EiId = 61215;
        //    CoverDetails.Totalsumassured = "0";
        //    CoverDetails.Coverforaccessories = "";
        //    CoverDetails.Totalcoverboat = "";
        //    CoverDetails.LiabilityObj = new LiabilityCD();
        //    CoverDetails.LiabilityObj.EiId = 61223;
        //    CoverDetails.ExcesscdObj = new ExcessCD();
        //    CoverDetails.ExcesscdObj.ExcessList = ExcessList;
        //    CoverDetails.ExcesscdObj.EiId = 61233;
        //    CoverDetails.FreeperiodObj = new ClaimFreePeriod();
        //    CoverDetails.FreeperiodObj.EiId = 61235;
        //    CoverDetails.NodiscountObj = new NoClaimDiscount();
        //    CoverDetails.NodiscountObj.EiId = 61237;
        //    if (Session["completionTrackB"] != null)
        //    {
        //        Session["completionTrackB"] = Session["completionTrackB"];
        //        CoverDetails.CompletionTrackB = Session["completionTrackB"].ToString();
        //    }
        //    else
        //    {
        //        Session["completionTrackB"] = "0-0-0-0-0-0"; ;
        //        CoverDetails.CompletionTrackB = Session["completionTrackB"].ToString();
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    var details = db.IT_GetCustomerQnsDetails(cid,Convert.ToInt32(RLSSection.Boat),Convert.ToInt32(PolicyType.RLS),policyid).ToList();
        //    if (details != null && details.Any())
        //    {
        //        if (details.Exists(q => q.QuestionId == CoverDetails.MarketvalueObj.EiId))
        //        {
        //            CoverDetails.MarketvalueObj.Marketvalue = Convert.ToString(details.Where(q => q.QuestionId == CoverDetails.MarketvalueObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == CoverDetails.MotorvalueObj.EiId))
        //        {
        //            CoverDetails.MotorvalueObj.Motorvalue = Convert.ToString(details.Where(q => q.QuestionId == CoverDetails.MotorvalueObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == CoverDetails.AccessorydescriptionObj.EiId))
        //        {
        //            CoverDetails.AccessorydescriptionObj.Description = Convert.ToString(details.Where(q => q.QuestionId == CoverDetails.AccessorydescriptionObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == CoverDetails.AccessorysuminsureObj.EiId))
        //        {
        //            CoverDetails.AccessorysuminsureObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == CoverDetails.AccessorysuminsureObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == CoverDetails.LiabilityObj.EiId))
        //        {
        //            CoverDetails.LiabilityObj.Liability = Convert.ToString(details.Where(q => q.QuestionId == CoverDetails.LiabilityObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == CoverDetails.ExcesscdObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == CoverDetails.ExcesscdObj.EiId).FirstOrDefault();
        //            CoverDetails.ExcesscdObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //        if (details.Exists(q => q.QuestionId == CoverDetails.FreeperiodObj.EiId))
        //        {
        //            CoverDetails.FreeperiodObj.Freeperiod = Convert.ToString(details.Where(q => q.QuestionId == CoverDetails.FreeperiodObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == CoverDetails.NodiscountObj.EiId))
        //        {
        //            CoverDetails.NodiscountObj.Nodiscount = Convert.ToString(details.Where(q => q.QuestionId == CoverDetails.NodiscountObj.EiId).FirstOrDefault().Answer);
        //        }
        //    }
        //    return View(CoverDetails);
        //}
        //[HttpPost]
        //public ActionResult CoverDetails(int? cid, CoverDetails CoverDetails)
        //{
        //    NewPolicyDetailsClass CoverDetailsmodel = new NewPolicyDetailsClass();
        //    List<SelectListItem> ExcessList = new List<SelectListItem>();
        //    ExcessList = CoverDetailsmodel.excessRate();
        //    CoverDetails.ExcesscdObj.ExcessList = ExcessList;
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        CoverDetails.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = CoverDetails.CustomerId;
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    if (cid.HasValue && cid > 0)
        //    {
        //        if (CoverDetails.MarketvalueObj != null && CoverDetails.MarketvalueObj.EiId > 0 && CoverDetails.MarketvalueObj.Marketvalue != null)
        //        {
        //            db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), CoverDetails.MarketvalueObj.EiId, CoverDetails.MarketvalueObj.Marketvalue.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (CoverDetails.MotorvalueObj != null && CoverDetails.MotorvalueObj.EiId > 0 && CoverDetails.MotorvalueObj.Motorvalue != null)
        //        {
        //            db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), CoverDetails.MotorvalueObj.EiId, CoverDetails.MotorvalueObj.Motorvalue.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (CoverDetails.AccessorydescriptionObj != null && CoverDetails.AccessorydescriptionObj.EiId > 0 && CoverDetails.AccessorydescriptionObj.Description != null)
        //        {
        //            db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), CoverDetails.AccessorydescriptionObj.EiId, CoverDetails.AccessorydescriptionObj.Description.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (CoverDetails.AccessorysuminsureObj != null && CoverDetails.AccessorysuminsureObj.EiId > 0 && CoverDetails.AccessorysuminsureObj.Suminsured != null)
        //        {
        //            db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), CoverDetails.AccessorysuminsureObj.EiId, CoverDetails.AccessorysuminsureObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (CoverDetails.LiabilityObj != null && CoverDetails.LiabilityObj.EiId > 0 && CoverDetails.LiabilityObj.Liability != null)
        //        {
        //            db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), CoverDetails.LiabilityObj.EiId, CoverDetails.LiabilityObj.Liability.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (CoverDetails.ExcesscdObj != null && CoverDetails.ExcesscdObj.EiId > 0 && CoverDetails.ExcesscdObj.Excess != null)
        //        {
        //            db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), CoverDetails.ExcesscdObj.EiId, CoverDetails.ExcesscdObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (CoverDetails.FreeperiodObj != null && CoverDetails.FreeperiodObj.EiId > 0 && CoverDetails.FreeperiodObj.Freeperiod != null)
        //        {
        //            db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), CoverDetails.FreeperiodObj.EiId, CoverDetails.FreeperiodObj.Freeperiod.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (CoverDetails.NodiscountObj != null && CoverDetails.NodiscountObj.EiId > 0 && CoverDetails.NodiscountObj.Nodiscount != null)
        //        {
        //            db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, Convert.ToInt32(RLSSection.Boat), CoverDetails.NodiscountObj.EiId, CoverDetails.NodiscountObj.Nodiscount.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Session["completionTrackB"] != null)
        //        {
        //            Session["completionTrackB"] = Session["completionTrackB"];
        //            CoverDetails.CompletionTrackB = Session["completionTrackB"].ToString();
        //            if (CoverDetails.CompletionTrackB != null)
        //            {
        //                string Completionstring = string.Empty;
        //                char[] arr = CoverDetails.CompletionTrackB.ToCharArray();
        //                for (int i = 0; i < arr.Length; i++)
        //                {
        //                    char a = arr[i];
        //                    if (i == 6)
        //                    {
        //                        a = '1';
        //                    }
        //                    Completionstring = Completionstring + a;
        //                }
        //                Session["completionTrackB"] = Completionstring;
        //                CoverDetails.CompletionTrackB = Completionstring;
        //            }
        //        }
        //        else
        //        {
        //            Session["completionTrackB"] = "0-0-0-1-0-0"; ;
        //            CoverDetails.CompletionTrackB = Session["completionTrackB"].ToString();
        //        }
        //        return RedirectToAction("Options", new { cid = CoverDetails.CustomerId });
        //    }
        //    return View(CoverDetails);
        //}
        //[HttpGet]
        //public ActionResult Options(int? cid)
        //{
        //    Options Options = new Options();
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        Options.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = Options.CustomerId;
        //    }
        //    Options.WaterwayObj = new Waterways();
        //    Options.WaterwayObj.EiId = 61253;
        //    Options.LimitseawardObj = new LimitSeawardTravel();
        //    Options.LimitseawardObj.EiId = 61255;
        //    Options.SailboatObj = new SailBoats();
        //    Options.SailboatObj.EiId = 61259;
        //    if (Session["completionTrackB"] != null)
        //    {
        //        Session["completionTrackB"] = Session["completionTrackB"];
        //        Options.CompletionTrackB = Session["completionTrackB"].ToString();
        //    }
        //    else
        //    {
        //        Session["completionTrackB"] = "0-0-0-0-0-0"; ;
        //        Options.CompletionTrackB = Session["completionTrackB"].ToString();
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Boat), Convert.ToInt32(PolicyType.RLS),policyid).ToList();
        //    if (details != null && details.Any())
        //    {
        //        if (details.Exists(q => q.QuestionId == Options.WaterwayObj.EiId))
        //        {
        //            Options.WaterwayObj.Waterway = Convert.ToString(details.Where(q => q.QuestionId == Options.WaterwayObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == Options.LimitseawardObj.EiId))
        //        {
        //            Options.LimitseawardObj.Seaward = Convert.ToString(details.Where(q => q.QuestionId == Options.LimitseawardObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == Options.SailboatObj.EiId))
        //        {
        //            Options.SailboatObj.Sailboat = Convert.ToString(details.Where(q => q.QuestionId == Options.SailboatObj.EiId).FirstOrDefault().Answer);
        //        }
        //    }
        //    return View(Options);
        //}
        //[HttpPost]
        //public ActionResult Options(int? cid, Options Options)
        //{
        //    var db = new MasterDataEntities();
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        Options.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = Options.CustomerId;
        //    }
        //    string policyid = null;
        //    if (cid.HasValue && cid > 0)
        //    {
        //        if (Options.WaterwayObj != null && Options.WaterwayObj.EiId > 0 && Options.WaterwayObj.Waterway != null)
        //        {
        //            db.IT_InsertCustomerQnsData(Options.CustomerId, Convert.ToInt32(RLSSection.Boat), Options.WaterwayObj.EiId, Options.WaterwayObj.Waterway.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Options.LimitseawardObj != null && Options.LimitseawardObj.EiId > 0 && Options.LimitseawardObj.Seaward != null)
        //        {
        //            db.IT_InsertCustomerQnsData(Options.CustomerId, Convert.ToInt32(RLSSection.Boat), Options.WaterwayObj.EiId, Options.WaterwayObj.Waterway.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Options.SailboatObj != null && Options.SailboatObj.EiId > 0 && Options.SailboatObj.Sailboat != null)
        //        {
        //            db.IT_InsertCustomerQnsData(Options.CustomerId, Convert.ToInt32(RLSSection.Boat), Options.SailboatObj.EiId, Options.SailboatObj.Sailboat.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Session["completionTrackB"] != null)
        //        {
        //            Session["completionTrackB"] = Session["completionTrackB"];
        //            Options.CompletionTrackB = Session["completionTrackB"].ToString();
        //            if (Options.CompletionTrackB != null)
        //            {
        //                string Completionstring = string.Empty;
        //                char[] arr = Options.CompletionTrackB.ToCharArray();
        //                for (int i = 0; i < arr.Length; i++)
        //                {
        //                    char a = arr[i];
        //                    if (i == 8)
        //                    {
        //                        a = '1';
        //                    }
        //                    Completionstring = Completionstring + a;
        //                }
        //                Session["completionTrackB"] = Completionstring;
        //                Options.CompletionTrackB = Completionstring;
        //            }
        //        }
        //        else
        //        {
        //            Session["completionTrackB"] = "0-0-0-0-1-0"; ;
        //            Options.CompletionTrackB = Session["completionTrackB"].ToString();
        //        }
        //        return RedirectToAction("InterestedPartiesBoat", new { cid = Options.CustomerId });
        //    }
        //    return View(Options);
        //}
        //[HttpGet]
        //public ActionResult InterestedPartiesBoat(int? cid)
        //{
        //    InterestedPartiesBoat InterestedPartiesBoat = new InterestedPartiesBoat();
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        InterestedPartiesBoat.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = InterestedPartiesBoat.CustomerId;
        //    }
        //    InterestedPartiesBoat.InstitutionsObj = new NameOfInstitutions();
        //    InterestedPartiesBoat.InstitutionsObj.EiId = 61277;
        //    InterestedPartiesBoat.LocationObj = new LocationsIPB();
        //    InterestedPartiesBoat.LocationObj.EiId = 61279;
        //    if (Session["completionTrackB"] != null)
        //    {
        //        Session["completionTrackB"] = Session["completionTrackB"];
        //        InterestedPartiesBoat.CompletionTrackB = Session["completionTrackB"].ToString();
        //    }
        //    else
        //    {
        //        Session["completionTrackB"] = "0-0-0-0-0-0";
        //        InterestedPartiesBoat.CompletionTrackB = Session["completionTrackB"].ToString();
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Boat), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
        //    if (details != null && details.Any())
        //    {
        //        if (details.Exists(q => q.QuestionId == InterestedPartiesBoat.InstitutionsObj.EiId))
        //        {
        //            InterestedPartiesBoat.InstitutionsObj.Name = Convert.ToString(details.Where(q => q.QuestionId == InterestedPartiesBoat.InstitutionsObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == InterestedPartiesBoat.LocationObj.EiId))
        //        {
        //            InterestedPartiesBoat.LocationObj.Location = Convert.ToString(details.Where(q => q.QuestionId == InterestedPartiesBoat.LocationObj.EiId).FirstOrDefault().Answer);
        //        }
        //    }
        //    return View(InterestedPartiesBoat);
        //}
        //[HttpPost]
        //public ActionResult InterestedPartiesBoat(int? cid, InterestedPartiesBoat InterestedPartiesBoat)
        //{
        //    var db = new MasterDataEntities();
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        InterestedPartiesBoat.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = InterestedPartiesBoat.CustomerId;
        //    }
        //    string policyid = null;
        //    if (cid.HasValue && cid > 0)
        //    {
        //        if (InterestedPartiesBoat.InstitutionsObj != null && InterestedPartiesBoat.InstitutionsObj.EiId > 0 && InterestedPartiesBoat.InstitutionsObj.Name != null)
        //        {
        //            db.IT_InsertCustomerQnsData(InterestedPartiesBoat.CustomerId, Convert.ToInt32(RLSSection.Boat), InterestedPartiesBoat.InstitutionsObj.EiId, InterestedPartiesBoat.InstitutionsObj.Name.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (InterestedPartiesBoat.LocationObj != null && InterestedPartiesBoat.LocationObj.EiId > 0 && InterestedPartiesBoat.LocationObj.Location != null)
        //        {
        //            db.IT_InsertCustomerQnsData(InterestedPartiesBoat.CustomerId, Convert.ToInt32(RLSSection.Boat), InterestedPartiesBoat.LocationObj.EiId, InterestedPartiesBoat.LocationObj.Location.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Session["completionTrackB"] != null)
        //        {
        //            Session["completionTrackB"] = Session["completionTrackB"];
        //            InterestedPartiesBoat.CompletionTrackB = Session["completionTrackB"].ToString();
        //            if (InterestedPartiesBoat.CompletionTrackB != null)
        //            {
        //                string Completionstring = string.Empty;
        //                char[] arr = InterestedPartiesBoat.CompletionTrackB.ToCharArray();
        //                for (int i = 0; i < arr.Length; i++)
        //                {
        //                    char a = arr[i];
        //                    if (i == 10)
        //                    {
        //                        a = '1';
        //                    }
        //                    Completionstring = Completionstring + a;
        //                }
        //                Session["completionTrackB"] = Completionstring;
        //                InterestedPartiesBoat.CompletionTrackB = Completionstring;
        //            }
        //        }
        //        else
        //        {
        //            Session["completionTrackB"] = "0-0-0-0-0-1"; ;
        //            InterestedPartiesBoat.CompletionTrackB = Session["completionTrackB"].ToString();
        //        }
        //        return RedirectToAction("PetsCover", "Pets", new { cid = cid });
        //    }
        //    return RedirectToAction("PetsCover", "Pets", new { cid = cid });
        //}
    }
}