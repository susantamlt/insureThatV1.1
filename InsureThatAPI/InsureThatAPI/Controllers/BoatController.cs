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
            try
            {
                NewPolicyDetailsClass BoatDetailsmodel = new NewPolicyDetailsClass();
                List<SelectListItem> TypeBoatList = new List<SelectListItem>();
                TypeBoatList = BoatDetailsmodel.TypeOfBoatList();
                List<SelectListItem> HullMeterialsList = new List<SelectListItem>();
                List<SelectListItem> Addresslist = new List<SelectListItem>();
                Addresslist.Add(new SelectListItem { Text = "ABC", Value = "1" });
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
                var policyinclusions = new List<usp_GetUnit_Result>();
                if (PcId != null && PcId.HasValue && PcId > 0)
                {
                    policyid = PcId.ToString();
                    BoatDetails.PcId = PcId.Value;
                    policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
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
                                return RedirectToAction("PetsCover", "Pets", new { cid = cid, PcId = PcId });
                            }
                            else if (Policyincllist.Exists(p => p.name == "Travel"))
                            {
                                return RedirectToAction("TravelCover", "Travel", new { cid = cid, PcId = PcId });
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
                }
                if (Session["profileId"] != null)
                {
                    profileid = Convert.ToInt32(Session["profileId"]);
                }
                HttpClient hclient = new HttpClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                hclient.BaseAddress = new Uri(url);
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (PcId != null && PcId.HasValue)
                {
                    if (Session["unId"] != null && Session["profileId"] != null)
                    {
                        unid = Convert.ToInt32(Session["unId"]);
                        profileid = Convert.ToInt32(Session["profileId"]);
                    }
                    else
                    {
                        if (policyinclusions.Exists(p => p.Name == "Boat"))
                        {
                            unid = policyinclusions.Where(p => p.Name == "Boat").Select(p => p.UnId).FirstOrDefault();
                            profileid = policyinclusions.Where(p => p.Name == "Boat").Select(p => p.UnId).FirstOrDefault();
                        }
                        else
                        {
                            return RedirectToAction("PetsCover", "Pets", new { cid = cid, PcId = PcId });
                        }
                    }
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
                        BoatDetails.MotorDetailsObjList = new List<ValueDatas>();
                        if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.YearmanufactureMDObj.EiId))
                        {
                            string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.YearmanufactureMDObj.EiId).Select(p => p.Value).FirstOrDefault();
                            if (val != null && !string.IsNullOrEmpty(val))
                            {
                                BoatDetails.YearmanufactureMDObj.Year = val;
                            }
                            if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == BoatDetails.YearmanufactureMDObj.EiId).Count() > 1)
                            {
                                List<ValueDatas> elmnts = new List<ValueDatas>();
                                var NameIpList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.YearmanufactureMDObj.EiId).Select(p => p.Element.ItId).ToList();
                                for (int i = 0; i < NameIpList.Count(); i++)
                                {
                                    ValueDatas vds = new ValueDatas();
                                    vds.Element = new Elements();
                                    vds.Element.ElId = 61151;
                                    vds.Element.ItId = NameIpList[i];
                                    vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.YearmanufactureMDObj.EiId && p.Element.ItId == NameIpList[i]).Select(p => p.Value).FirstOrDefault();
                                    elmnts.Add(vds);
                                }
                                BoatDetails.MotorDetailsObjList = elmnts;
                            }
                        }
                        BoatDetails.SerialNumberObjList = new List<ValueDatas>();
                        if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.SerialnumberObj.EiId))
                        {
                            string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.SerialnumberObj.EiId).Select(p => p.Value).FirstOrDefault();
                            if (val != null && !string.IsNullOrEmpty(val))
                            {
                                BoatDetails.SerialnumberObj.Serialnumber = val;
                            }
                            if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == BoatDetails.SerialnumberObj.EiId).Count() > 1)
                            {
                                List<ValueDatas> elmnts = new List<ValueDatas>();
                                var seriallist = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.SerialnumberObj.EiId).Select(p => p.Element.ItId).ToList();
                                for (int i = 0; i < seriallist.Count(); i++)
                                {
                                    ValueDatas vds = new ValueDatas();
                                    vds.Element = new Elements();
                                    vds.Element.ElId = 61153;
                                    vds.Element.ItId = seriallist[i];
                                    vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.SerialnumberObj.EiId && p.Element.ItId == seriallist[i]).Select(p => p.Value).FirstOrDefault();
                                    elmnts.Add(vds);
                                }
                                BoatDetails.SerialNumberObjList = elmnts;
                            }
                        }
                        BoatDetails.MakemodelObjList = new List<ValueDatas>();
                        if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.MakemodelObj.EiId))
                        {
                            string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.MakemodelObj.EiId).Select(p => p.Value).FirstOrDefault();
                            if (val != null && !string.IsNullOrEmpty(val))
                            {
                                BoatDetails.MakemodelObj.Makemodel = val;
                            }
                            if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == BoatDetails.MakemodelObj.EiId).Count() > 1)
                            {
                                List<ValueDatas> elmnts = new List<ValueDatas>();
                                var makemodellist = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.MakemodelObj.EiId).Select(p => p.Element.ItId).ToList();
                                for (int i = 0; i < makemodellist.Count(); i++)
                                {
                                    ValueDatas vds = new ValueDatas();
                                    vds.Element = new Elements();
                                    vds.Element.ElId = 60967;
                                    vds.Element.ItId = makemodellist[i];
                                    vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.MakemodelObj.EiId && p.Element.ItId == makemodellist[i]).Select(p => p.Value).FirstOrDefault();
                                    elmnts.Add(vds);
                                }
                                BoatDetails.MakemodelObjList = elmnts;
                            }
                        }
                        BoatDetails.FuelObjList = new List<ValueDatas>();
                        if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.FueltypeObj.EiId))
                        {
                            string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.FueltypeObj.EiId).Select(p => p.Value).FirstOrDefault();
                            if (val != null && !string.IsNullOrEmpty(val))
                            {
                                BoatDetails.FueltypeObj.Type = val;
                            }
                            if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == BoatDetails.FueltypeObj.EiId).Count() > 1)
                            {
                                List<ValueDatas> elmnts = new List<ValueDatas>();
                                var fueltype = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.FueltypeObj.EiId).Select(p => p.Element.ItId).ToList();
                                for (int i = 0; i < fueltype.Count(); i++)
                                {
                                    ValueDatas vds = new ValueDatas();
                                    vds.Element = new Elements();
                                    vds.Element.ElId = 61155;
                                    vds.Element.ItId = fueltype[i];
                                    vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.FueltypeObj.EiId && p.Element.ItId == fueltype[i]).Select(p => p.Value).FirstOrDefault();
                                    elmnts.Add(vds);
                                }
                                BoatDetails.FuelObjList = elmnts;
                            }
                        }
                        BoatDetails.MotorPositionObjList = new List<ValueDatas>();
                        if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.MotorpositionObj.EiId))
                        {
                            string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.MotorpositionObj.EiId).Select(p => p.Value).FirstOrDefault();
                            if (val != null && !string.IsNullOrEmpty(val))
                            {
                                BoatDetails.MotorpositionObj.Position = val;
                            }
                            if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == BoatDetails.MotorpositionObj.EiId).Count() > 1)
                            {
                                List<ValueDatas> elmnts = new List<ValueDatas>();
                                var motorpostion = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.MotorpositionObj.EiId).Select(p => p.Element.ItId).ToList();
                                for (int i = 0; i < motorpostion.Count(); i++)
                                {
                                    ValueDatas vds = new ValueDatas();
                                    vds.Element = new Elements();
                                    vds.Element.ElId = 61157;
                                    vds.Element.ItId = motorpostion[i];
                                    vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.MotorpositionObj.EiId && p.Element.ItId == motorpostion[i]).Select(p => p.Value).FirstOrDefault();
                                    elmnts.Add(vds);
                                }
                                BoatDetails.MotorPositionObjList = elmnts;
                            }
                        }
                        BoatDetails.DriverTypeObjList = new List<ValueDatas>();
                        if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.DrivetypeObj.EiId))
                        {
                            string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.DrivetypeObj.EiId).Select(p => p.Value).FirstOrDefault();
                            if (val != null && !string.IsNullOrEmpty(val))
                            {
                                BoatDetails.DrivetypeObj.Drivetype = val;
                            }
                            if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == BoatDetails.DrivetypeObj.EiId).Count() > 1)
                            {
                                List<ValueDatas> elmnts = new List<ValueDatas>();
                                var drivertype = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.DrivetypeObj.EiId).Select(p => p.Element.ItId).ToList();
                                for (int i = 0; i < drivertype.Count(); i++)
                                {
                                    ValueDatas vds = new ValueDatas();
                                    vds.Element = new Elements();
                                    vds.Element.ElId = 61159;
                                    vds.Element.ItId = drivertype[i];
                                    vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.DrivetypeObj.EiId && p.Element.ItId == drivertype[i]).Select(p => p.Value).FirstOrDefault();
                                    elmnts.Add(vds);
                                }
                                BoatDetails.DriverTypeObjList = elmnts;
                            }
                        }
                        BoatDetails.PowerObjList = new List<ValueDatas>();
                        if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.PowerObj.EiId))
                        {
                            string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.PowerObj.EiId).Select(p => p.Value).FirstOrDefault();
                            if (val != null && !string.IsNullOrEmpty(val))
                            {
                                BoatDetails.PowerObj.Power = val;
                            }
                            if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == BoatDetails.PowerObj.EiId).Count() > 1)
                            {
                                List<ValueDatas> elmnts = new List<ValueDatas>();
                                var power = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.PowerObj.EiId).Select(p => p.Element.ItId).ToList();
                                for (int i = 0; i < power.Count(); i++)
                                {
                                    ValueDatas vds = new ValueDatas();
                                    vds.Element = new Elements();
                                    vds.Element.ElId = 61161;
                                    vds.Element.ItId = power[i];
                                    vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.PowerObj.EiId && p.Element.ItId == power[i]).Select(p => p.Value).FirstOrDefault();
                                    elmnts.Add(vds);
                                }
                                BoatDetails.PowerObjList = elmnts;
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
                        if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.YearmanufactureMDObj.EiId))
                        {
                            string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.YearmanufactureMDObj.EiId).Select(p => p.Value).FirstOrDefault();
                            BoatDetails.YearmanufactureMDObj.Year = val;
                        }
                        if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.SerialnumberObj.EiId))
                        {
                            string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.SerialnumberObj.EiId).Select(p => p.Value).FirstOrDefault();
                            BoatDetails.SerialnumberObj.Serialnumber = val;
                        }
                        if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.MotorpositionObj.EiId))
                        {
                            string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.MotorpositionObj.EiId).Select(p => p.Value).FirstOrDefault();
                            BoatDetails.MotorpositionObj.Position = val;
                        }

                        if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.FueltypeObj.EiId))
                        {
                            string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.FueltypeObj.EiId).Select(p => p.Value).FirstOrDefault();
                            BoatDetails.FueltypeObj.Type = val;
                        }
                        if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == BoatDetails.MakemodelObj.EiId))
                        {
                            string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == BoatDetails.MakemodelObj.EiId).Select(p => p.Value).FirstOrDefault();
                            BoatDetails.MakemodelObj.Makemodel = val;
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
                if (unitdetails != null && unitdetails.ReferralList != null)
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
            catch(Exception ex)
            {

            }
            return View();
        }
        [HttpPost]
        public ActionResult BoatDetails(int? cid, BoatDetails BoatDetails)
        {
            try
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
                Session["RLSboat"] = 1;
                return RedirectToAction("PetsCover", "Pets", new { cid = BoatDetails.CustomerId, PcId = BoatDetails.PcId });
            }
            catch(Exception ex)
            {

            }
            return View();
        }
        [HttpPost]
        public ActionResult BoatAjaxcontent(int id, string content)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            if (Request.IsAjaxRequest())
            {
                int cid = 1;
                ViewBag.cid = cid;
                if (content == "motorDetails")
                {
                    List<SelectListItem> listMotorposition = new List<SelectListItem>();
                    listMotorposition.Add(new SelectListItem { Text = "--Select--", Value = "" });
                    listMotorposition.Add(new SelectListItem { Text = "Inboard ", Value = "1" });
                    listMotorposition.Add(new SelectListItem { Text = "Outboard", Value = "2" });

                    List<SelectListItem> listDrivetype = new List<SelectListItem>();
                    listDrivetype.Add(new SelectListItem { Text = "--Select--", Value = "" });
                    listDrivetype.Add(new SelectListItem { Text = "Shaft", Value = "1" });
                    listDrivetype.Add(new SelectListItem { Text = "Stern", Value = "2" });

                    List<SelectListItem> listoffualtype = new List<SelectListItem>();
                    listoffualtype = commonModel.FuelType();
                    return Json(new { status = true, des = listMotorposition, con = listDrivetype, mon = listoffualtype });
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