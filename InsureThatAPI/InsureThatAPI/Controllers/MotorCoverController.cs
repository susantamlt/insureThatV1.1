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
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Web.Helpers;
using System.Web.Script.Serialization;
namespace InsureThatAPI.Controllers
{
    public class MotorCoverController : Controller
    {
        // GET: MotorCover
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> VehicleDescription(int? cid, int? PcId)
        {
            MCVehicleDescription MCVehicleDescription = new MCVehicleDescription();
            MCVehicleDescription.MCOPTList = new List<SelectListItem>();
            List<SelectListItem> MCOPTList = new List<SelectListItem>();
            var db = new MasterDataEntities();
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCVehicleDescription.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCVehicleDescription.CustomerId;
            }
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            unitdetails.AddressList = new List<AddressData>();
            AddressData ad = new AddressData();
            NewPolicyDetailsClass commonmethods = new NewPolicyDetailsClass();
            var policyinclusions = new List<usp_GetUnit_Result>();
            if (PcId != null && PcId > 0)
            {
                policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
            }
            string apikey = null;
            bool policyinclusion = (policyinclusions != null && policyinclusions.Count() > 0 && policyinclusions.Exists(p => p.Name == "Motor"));
            if (Session["apiKey"] != null)
            {
                apikey = Session["apiKey"].ToString();
                MCVehicleDescription.ApiKey = Session["apiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            NewPolicyDetailsClass MCVehicleDescriptionmodel = new NewPolicyDetailsClass();
            List<SelectListItem> DriversGendarList = new List<SelectListItem>();
            DriversGendarList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            DriversGendarList.Add(new SelectListItem { Value = "1", Text = "Male" });
            DriversGendarList.Add(new SelectListItem { Value = "2", Text = "Female" });
            List<SelectListItem> VehicleMakeList = new List<SelectListItem>();
            VehicleMakeList = MCVehicleDescriptionmodel.VehicleMake();
            List<SelectListItem> FamilyLists = new List<SelectListItem>();
            FamilyLists = MCVehicleDescriptionmodel.MotorCoverFamily();
            AddressData add = new AddressData();
            List<AddressData> AddressList = new List<AddressData>();
            AddressList = MCVehicleDescriptionmodel.MCADAddress();
            List<SelectListItem> VNList = new List<SelectListItem>();
            VNList = MCVehicleDescriptionmodel.MCADVinNumber();
            List<SelectListItem> ENList = new List<SelectListItem>();
            ENList = MCVehicleDescriptionmodel.MCADEngineNumber();
            List<SelectListItem> descriptionList = new List<SelectListItem>();
            descriptionList = MCVehicleDescriptionmodel.MCCDDescription();
            List<SelectListItem> BasicExcessList = new List<SelectListItem>();
            BasicExcessList.Add(new SelectListItem { Value = "60953", Text = "Under 21 Excess" });
            BasicExcessList.Add(new SelectListItem { Value = "60955", Text = "Under 25 Excess" });
            string policyid = null;
            var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
            CommonUseFunctionClass cmn = new CommonUseFunctionClass();
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                policyid = PcId.ToString();
                MCVehicleDescription.PolicyId = policyid;
            }
            else if (Session["Policyinclustions"] != null)
            {
                #region Policy Selected or not testing
                List<SessionModel> PolicyInclustions = new List<SessionModel>();
                MCVehicleDescription.PolicyInclusions = Policyincllist;
                MCVehicleDescription.NewSections = new List<string>();
                MCVehicleDescription.NewSections = cmn.NewSectionHome(MCVehicleDescription.PolicyInclusions);
                if (Policyincllist != null)
                {
                    if (Policyincllist.Exists(p => p.name == "Motor"))
                    {
                    }
                    else
                    {
                        if (Policyincllist.Exists(p => p.name == "Boat"))
                        {
                            return RedirectToAction("BoatDetails", "Boat", new { cid = cid, PcId = PcId });
                        }
                        if (Policyincllist.Exists(p => p.name == "Pet" || p.name == "Pets"))
                        {
                            return RedirectToAction("PetsCover", "Pets", new { cid = cid, PcId = PcId });
                        }
                        if (Policyincllist.Exists(p => p.name == "Travel"))
                        {
                            return RedirectToAction("TravelCover", "Travel", new { cid = cid, PcId = PcId });
                        }
                        if (Policyincllist.Exists(p => p.name == "Motor" || p.name == "Motors"))
                        {
                            if (Session["unId"] == null && Session["profileId"] == null)
                            {
                                Session["unId"] = Policyincllist.Where(p => p.name == "Motor").Select(p => p.UnitId).First();
                                Session["profileId"] = Policyincllist.Where(p => p.name == "Motor").Select(p => p.ProfileId).First();
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
            #region VehicleDescription
            MCVehicleDescription.LscategoryObj = new GLVCategory();
            MCVehicleDescription.LscategoryObj.EiId = 60751;
            MCVehicleDescription.McmakeObj = new GLVMake();
            MCVehicleDescription.McmakeObj.MakeList = VehicleMakeList;
            MCVehicleDescription.McmakeObj.EiId = 60753;
            MCVehicleDescription.McyearObj = new GLVYear();
            MCVehicleDescription.McyearObj.EiId = 60755;
            MCVehicleDescription.MCfamilyObj = new GLVFamily();
            MCVehicleDescription.MCfamilyObj.FamilyList = FamilyLists;
            MCVehicleDescription.MCfamilyObj.EiId = 60757;
            MCVehicleDescription.MCscdObj = new GLVSelectCorDetails();
            MCVehicleDescription.MCscdObj.EiId = 60759;
            MCVehicleDescription.FmmcmakeObj = new FMMCMake();
            MCVehicleDescription.FmmcmakeObj.EiId = 60767;
            MCVehicleDescription.FmmcyearObj = new FMMCYear();
            MCVehicleDescription.FmmcyearObj.EiId = 60769;
            MCVehicleDescription.FmmctypeObj = new FMMCType();
            MCVehicleDescription.FmmctypeObj.FmFamilyList = FamilyLists;
            MCVehicleDescription.FmmctypeObj.FmFamilyLists = new List<SelectListItem>();
            MCVehicleDescription.FmmctypeObj.FmFamilyLists = FamilyLists;
            MCVehicleDescription.FmmctypeObj.EiId = 60779;
            MCVehicleDescription.FmmcscdObj = new FMMCSelectCorDetails();
            MCVehicleDescription.FmmcscdObj.EiId = 60781;
            #endregion
            #region AdditionDetails
            MCVehicleDescription.KeptnightObj = new MCADKeptAtNight();
            MCVehicleDescription.KeptnightObj.EiId = 60793;
            MCVehicleDescription.AdaddressObj = new MCADAddress();
            MCVehicleDescription.AdaddressObj.AddressList = new List<AddressData>();
            MCVehicleDescription.AdaddressObj.AddressList = AddressList;
            MCVehicleDescription.AdaddressList = new List<SelectListItem>();
            MCVehicleDescription.AdaddressObj.EiId = 60795;
            MCVehicleDescription.VregisterObj = new MCADVehicleRegistered();
            MCVehicleDescription.VregisterObj.EiId = 60797;
            MCVehicleDescription.RnumberObj = new MCADRegistrationNumber();
            MCVehicleDescription.RnumberObj.EiId = 60799;
            MCVehicleDescription.VnumberObj = new MCADVinNumber();
            MCVehicleDescription.VnumberObj.VnumberList = VNList;
            MCVehicleDescription.VnumberObj.EiId = 60801;
            MCVehicleDescription.EnumberObj = new MCADEngineNumber();
            MCVehicleDescription.EnumberObj.EnumberList = ENList;
            MCVehicleDescription.EnumberObj.EiId = 60803;
            MCVehicleDescription.VmodifiedObj = new MCADVehicleModified();
            MCVehicleDescription.VmodifiedObj.EiId = 60805;
            MCVehicleDescription.DmodifiedObj = new MCADdescribeModified();
            MCVehicleDescription.DmodifiedObj.EiId = 60807;
            MCVehicleDescription.AlarmObj = new AlarmInstalled();
            MCVehicleDescription.AlarmObj.EiId = 60809;
            MCVehicleDescription.EngineImmobiliserObj = new EngineImmobiliserInstalled();
            MCVehicleDescription.EngineImmobiliserObj.EiId = 60811;
            MCVehicleDescription.PrivateUseObj = new PrivateUse();
            MCVehicleDescription.PrivateUseObj.EiId = 60821;
            MCVehicleDescription.FarmUseObj = new FarmUse();
            MCVehicleDescription.FarmUseObj.EiId = 60823;
            MCVehicleDescription.BusinessUseObj = new BusinessUse();
            MCVehicleDescription.BusinessUseObj.EiId = 60825;
            MCVehicleDescription.VusedObj = new MCADVehicleUsed();
            MCVehicleDescription.VusedObj.EiId = 60821;
            MCVehicleDescription.CcapacityObj = new MCADCarryingCapacity();
            MCVehicleDescription.CcapacityObj.EiId = 60811;
            #endregion
            #region Driver
            DriverList drlist = new DriverList();
            Driver dr = new Driver();
            MCVehicleDescription.DriverageObj = new DriverAge();

            MCVehicleDescription.DrivergenderObj = new DriverGender();
            MCVehicleDescription.DrivergenderObj.GenderList = DriversGendarList;
            MCVehicleDescription.DriverDatas = new DriverList();
            MCVehicleDescription.UsevehicleObj = new UseOfVehicle();
            MCVehicleDescription.DriverDatas.DriverData = new List<Driver>();
            MCVehicleDescription.UsevehicleObj.EiId = 60845;
            #endregion
            #region Cover Details
            MCVehicleDescription.CoveroptionObj = new CoverOptionCD();
            MCVehicleDescription.CoveroptionObj.EiId = 60859;
            MCVehicleDescription.CovertypeObj = new CoverTypeCD();
            MCVehicleDescription.CovertypeObj.EiId = 60861;
            MCVehicleDescription.MaxMarvalObj = new MaximumMarketValue();
            MCVehicleDescription.MaxMarvalObj.EiId = 60865;
            MCVehicleDescription.CaravanannexObj = new CaravanAnnex();
            MCVehicleDescription.CaravanannexObj.EiId = 60871;
            MCVehicleDescription.UnspecifieditemsObj = new UnspecifiedItems();
            MCVehicleDescription.UnspecifieditemsObj.EiId = 60873;
            MCVehicleDescription.AccessoriesObj = new NonStandardAccessories();
            MCVehicleDescription.AccessoriesObj.EiId = 60877;
            MCVehicleDescription.DescriptionObj = new AccessoryDescriptionCD();
            MCVehicleDescription.DescriptionObj.DescriptionList = descriptionList;
            MCVehicleDescription.DescriptionObj.EiId = 60891;
            MCVehicleDescription.SumnsuredObj = new SumInsuredCD();
            MCVehicleDescription.SumnsuredObj.EiId = 60893;
            MCVehicleDescription.LimitindemnityObj = new LimitOfIndemnityDC();
            MCVehicleDescription.LimitindemnityObj.EiId = 60905;
            MCVehicleDescription.RatingObj = new RatingDC();
            MCVehicleDescription.RatingObj.EiId = 60917;
            MCVehicleDescription.NoclaimbonusObj = new NoClaimBonus();
            MCVehicleDescription.NoclaimbonusObj.EiId = 60919;
            #endregion
            #region Optional Extra Excess
            MCVehicleDescription.CaroptionObj = new HireCarOption();
            MCVehicleDescription.CaroptionObj.EiId = 60947;
            MCVehicleDescription.NoClaimBonusOptionObj = new HireCarOption();
            MCVehicleDescription.NoClaimBonusOptionObj.EiId = 60945;
            MCVehicleDescription.WindscreenObj = new HireCarOption();
            MCVehicleDescription.WindscreenObj.EiId = 60943;
            MCVehicleDescription.ExcessObj = new BasicExcess();
            MCVehicleDescription.ExcessObj.EiId = 60951;
            MCVehicleDescription.Excess21UnderObj = new Excess21UnderPEE();
            MCVehicleDescription.Excess21UnderObj.EiId = 60953;
            MCVehicleDescription.Excess25UnderObj = new Excess25UnderPEE();
            MCVehicleDescription.Excess25UnderObj.EiId = 60955;

            #endregion
            #region Interested Parties
            MCVehicleDescription.MCPartynameObj = new MCInterestedPartyName();
            MCVehicleDescription.MCPartynameObj.EiId = 60967;
            MCVehicleDescription.MCPartyLocationObj = new MCInterestedPartyLocation();
            MCVehicleDescription.MCPartyLocationObj.EiId = 60969;
            #endregion
            #region Calling of API for Endorse and New Policy
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = 0;
            int profileid = 0;
            if (Session["unId"] != null)
            {
                unid = Convert.ToInt32(Session["unId"]);
                profileid = Convert.ToInt32(Session["profileId"]);
            }
            if (Session["profileId"] != null)
            {
                profileid = Convert.ToInt32(Session["profileId"]);
            }
            if (policyinclusion == true && PcId != null && PcId.HasValue)
            {
                MCVehicleDescription.ExistingPolicyInclustions = policyinclusions;
                MCVehicleDescription.PolicyInclusion = new List<usp_GetUnit_Result>();
                MCVehicleDescription.PolicyInclusion = policyinclusions;
                HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                if (EmpResponse != null)
                {
                    unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                }
            }
            else
            {
                if (PcId == null && Session["unId"] == null && (Session["profileId"] == null || profileid == 0))
                {
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Motor&SectionUnId=&ProfileUnId=0");
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails != null && unitdetails.ErrorMessage != null && unitdetails.ErrorMessage.Count() > 0)
                        {
                            var errormessage = "First please provide cover for Home Buildings.";
                            if (unitdetails.ErrorMessage.Contains(errormessage))
                            {
                                TempData["Error"] = errormessage;
                                return RedirectToAction("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = PcId });
                            }
                        }
                        if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Motor"))
                        {
                            var policyhomelist = Policyincllist.FindAll(p => p.name == "Motor").ToList();
                            if (policyhomelist != null && policyhomelist.Count() > 0)
                            {
                                if (Policyincllist.FindAll(p => p.name == "Motor").Exists(p => p.UnitId == null))
                                {
                                    Policyincllist.FindAll(p => p.name == "Motor").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;
                                }
                                if (Policyincllist.FindAll(p => p.name == "Motor").Exists(p => p.ProfileId == null))
                                {
                                    Policyincllist.FindAll(p => p.name == "Motor").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                            }
                            else
                            {
                                Policyincllist.FindAll(p => p.name == "Motor").First().UnitId = unitdetails.SectionData.UnId;
                                Policyincllist.FindAll(p => p.name == "Motor").First().ProfileId = unitdetails.SectionData.ProfileUnId;
                            }
                        }
                        Session["MprofileId"] = unitdetails.SectionData.UnId;
                        MCVehicleDescription.PolicyInclusions = Policyincllist;
                        Session["Policyinclustions"] = Policyincllist;
                        if (unitdetails != null && unitdetails.SectionData != null)
                        {
                            Session["unId"] = unitdetails.SectionData.UnId;
                            Session["profileId"] = unitdetails.SectionData.ProfileUnId;
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
                if (unitdetails.SectionData != null && unitdetails.SectionData.RowsourceData != null)
                {
                    if (unitdetails.SectionData.RowsourceData.Exists(p => p.Element.ElId == MCVehicleDescription.AccessoriesObj.EiId))
                    {
                    }
                    if (unitdetails.SectionData.RowsourceData.Exists(p => p.Element.ElId == MCVehicleDescription.FmmctypeObj.EiId))
                    {
                        Option op = new Option();
                        List<Option> oplist = new List<Option>();
                        List<SelectListItem> oplists = new List<SelectListItem>();
                        MCVehicleDescription.FmmctypeObj.FmFamilyLists = new List<SelectListItem>();
                        var opp = unitdetails.SectionData.RowsourceData.Where(p => p.Element.ElId == MCVehicleDescription.FmmctypeObj.EiId).Select(p => p.Options).ToList();
                        for (int i = 0; i < opp.Count(); i++)
                        {
                            var o = opp[i];
                            for (int j = 0; j < o.Count(); j++)
                            {
                                op = new Option();
                                op.DataValue = o[j].DataValue;
                                op.DataText = o[j].DataText;
                                oplist.Add(op);
                                oplists.Add(new SelectListItem { Value = o[j].DataValue, Text = o[j].DataText });
                            }
                        }
                        MCVehicleDescription.FmmctypeObj.FmFamilyLists = oplists;
                    }
                    if (unitdetails.SectionData.RowsourceData.Exists(p => p.Element.ElId == MCVehicleDescription.MCscdObj.EiId))
                    {
                        Option op = new Option();
                        List<Option> oplist = new List<Option>();
                        List<SelectListItem> oplists = new List<SelectListItem>();
                        MCVehicleDescription.MCscdObj.ScdList = new List<SelectListItem>();
                        var opp = unitdetails.SectionData.RowsourceData.Where(p => p.Element.ElId == MCVehicleDescription.MCscdObj.EiId).Select(p => p.Options).ToList();
                        for (int i = 0; i < opp.Count(); i++)
                        {
                            var o = opp[i];
                            for (int j = 0; j < o.Count(); j++)
                            {
                                op = new Option();
                                op.DataValue = o[j].DataValue;
                                op.DataText = o[j].DataText;
                                oplist.Add(op);
                                oplists.Add(new SelectListItem { Value = o[j].DataValue, Text = o[j].DataText });
                            }
                        }
                        MCVehicleDescription.MCscdObj.ScdList = oplists;
                    }                   
                    if (unitdetails.SectionData.RowsourceData.Exists(p => p.Element.ElId == MCVehicleDescription.MCfamilyObj.EiId))
                    {
                        Option op1 = new Option();
                        List<Option> oplist1 = new List<Option>();
                        List<SelectListItem> oplists1 = new List<SelectListItem>();
                        MCVehicleDescription.MCfamilyObj.FamilyList = new List<SelectListItem>();
                        var opp = unitdetails.SectionData.RowsourceData.Where(p => p.Element.ElId == MCVehicleDescription.MCfamilyObj.EiId).Select(p => p.Options).ToList();
                        for (int i = 0; i < opp.Count(); i++)
                        {
                            var o = opp[i];
                            for (int j = 0; j < o.Count(); j++)
                            {
                                op1 = new Option();
                                op1.DataValue = o[j].DataValue;
                                op1.DataText = o[j].DataText;
                                oplist1.Add(op1);
                                oplists1.Add(new SelectListItem { Value = o[j].DataValue, Text = o[j].DataText });
                            }
                        }
                        MCVehicleDescription.MCfamilyObj.FamilyList = oplists1;
                    }
                    if (unitdetails.SectionData.RowsourceData.Exists(p => p.Element.ElId == MCVehicleDescription.McyearObj.EiId))
                    {
                        Option op = new Option();
                        List<Option> oplist = new List<Option>();
                        List<SelectListItem> oplists = new List<SelectListItem>();
                        MCVehicleDescription.McyearObj.YearList = new List<SelectListItem>();
                        var opp = unitdetails.SectionData.RowsourceData.Where(p => p.Element.ElId == MCVehicleDescription.McyearObj.EiId).Select(p => p.Options).ToList();
                        for (int i = 0; i < opp.Count(); i++)
                        {
                            var o = opp[i];
                            for (int j = 0; j < o.Count(); j++)
                            {
                                op = new Option();
                                op.DataValue = o[j].DataValue;
                                op.DataText = o[j].DataText;
                                oplist.Add(op);
                                if (op.DataValue == "0")
                                {

                                }
                                else
                                {
                                    oplists.Add(new SelectListItem { Value = o[j].DataValue, Text = o[j].DataText });
                                }
                            }
                        }
                        MCVehicleDescription.McyearObj.YearList = oplists;
                    }
                }               
                if (unitdetails.SectionData != null && unitdetails.SectionData.ValueData != null)
                {
                    if (unitdetails.AddressList != null && unitdetails.AddressList.Count > 0)
                    {
                        MCVehicleDescription.AdaddressObj.AddressList = unitdetails.AddressList;
                        if (MCVehicleDescription.AdaddressObj.AddressList != null && MCVehicleDescription.AdaddressObj.AddressList.Count() > 0)
                        {
                            for (int i = 0; i < MCVehicleDescription.AdaddressObj.AddressList.Count(); i++)
                            {
                                MCVehicleDescription.AdaddressList.Add(new SelectListItem { Value = MCVehicleDescription.AdaddressObj.AddressList[i].AddressID.ToString(), Text = MCVehicleDescription.AdaddressObj.AddressList[i].AddressLine1 + ", " + MCVehicleDescription.AdaddressObj.AddressList[i].Suburb + ", " + MCVehicleDescription.AdaddressObj.AddressList[i].State + ", " + MCVehicleDescription.AdaddressObj.AddressList[i].Postcode });
                            }
                        }
                    }
                    if (unitdetails.SectionData != null && unitdetails.SectionData.AddressData != null)
                    {
                        MCVehicleDescription.AdaddressObj.Address = unitdetails.SectionData.AddressData.AddressLine1 + ", " + unitdetails.SectionData.AddressData.Suburb + ", " + unitdetails.SectionData.AddressData.State + ", " + unitdetails.SectionData.AddressData.Postcode;
                    }
                    else if (unitdetails.AddressData != null && unitdetails.AddressData.Count() > 0)
                    {
                       MCVehicleDescription.AdaddressObj.Address = unitdetails.AddressData[0].AddressLine1 + ", " + unitdetails.AddressData[0].Suburb + ", " + unitdetails.AddressData[0].State + ", " + unitdetails.AddressData[0].Postcode;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.AccessoriesObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.AccessoriesObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.AccessoriesObj.Accessories = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.AdaddressObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.AdaddressObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.AdaddressObj.Address = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.CaravanannexObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.CaravanannexObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.CaravanannexObj.Annex = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.CaroptionObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.CaroptionObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val == "1")
                        {
                            MCVehicleDescription.CaroptionObj.Caroption = true;
                        }
                        else if (val == "0")
                        {
                            MCVehicleDescription.CaroptionObj.Caroption = false;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.WindscreenObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.WindscreenObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val == "1")
                        {
                            MCVehicleDescription.WindscreenObj.Caroption = true;
                        }
                        else if (val == "0")
                        {
                            MCVehicleDescription.WindscreenObj.Caroption = false;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.NoClaimBonusOptionObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.NoClaimBonusOptionObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val == "1")
                        {
                            MCVehicleDescription.NoClaimBonusOptionObj.Caroption = true;
                        }
                        else if (val == "0")
                        {
                            MCVehicleDescription.NoClaimBonusOptionObj.Caroption = false;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.CcapacityObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.CcapacityObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.CcapacityObj.Ccapacity = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.CoveroptionObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.CoveroptionObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.CoveroptionObj.Coveroption = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.CovertypeObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.CovertypeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.CovertypeObj.Covertype = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.MCPartynameObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.MCPartynameObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            MCVehicleDescription.MCPartynameObj.Name = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == MCVehicleDescription.MCPartynameObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var NameIpList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.MCPartynameObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < NameIpList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60967;
                                vds.Element.ItId = NameIpList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.MCPartynameObj.EiId && p.Element.ItId == NameIpList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            MCVehicleDescription.MCPartynameObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.MCPartyLocationObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.MCPartyLocationObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            MCVehicleDescription.MCPartyLocationObj.Location = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == MCVehicleDescription.MCPartyLocationObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var LocationIpList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.MCPartyLocationObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < LocationIpList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60969;
                                vds.Element.ItId = LocationIpList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.MCPartyLocationObj.EiId && p.Element.ItId == LocationIpList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            MCVehicleDescription.MCPartyLocationObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.DescriptionObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.DescriptionObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            MCVehicleDescription.DescriptionObj.Description = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == MCVehicleDescription.DescriptionObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var DescriptionList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.DescriptionObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < DescriptionList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60891;
                                vds.Element.ItId = DescriptionList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.DescriptionObj.EiId && p.Element.ItId == DescriptionList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            MCVehicleDescription.DescriptionObjList = elmnts;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.SumnsuredObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.SumnsuredObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val != null && !string.IsNullOrEmpty(val))
                        {
                            MCVehicleDescription.SumnsuredObj.Suminsured = val;
                        }
                        if (unitdetails.SectionData.ValueData.Select(p => p.Element.ElId == MCVehicleDescription.SumnsuredObj.EiId).Count() > 1)
                        {
                            List<ValueDatas> elmnts = new List<ValueDatas>();
                            var SuminsuredList = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.SumnsuredObj.EiId).Select(p => p.Element.ItId).ToList();
                            for (int i = 0; i < SuminsuredList.Count(); i++)
                            {
                                ValueDatas vds = new ValueDatas();
                                vds.Element = new Elements();
                                vds.Element.ElId = 60893;
                                vds.Element.ItId = SuminsuredList[i];
                                vds.Value = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.SumnsuredObj.EiId && p.Element.ItId == SuminsuredList[i]).Select(p => p.Value).FirstOrDefault();
                                elmnts.Add(vds);
                            }
                            MCVehicleDescription.SumnsuredObjList = elmnts;
                        }
                    }
                    //if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.DmodifiedObj.EiId))
                    //{
                    //    string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.DmodifiedObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    MCVehicleDescription.DmodifiedObj.Dmodified = val;
                    //}
                    //if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.DriverageObj.EiId))
                    //{
                    //    string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.DriverageObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    MCVehicleDescription.DriverageObj.Age = val;
                    //}
                    //if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.DrivernameObj.EiId))
                    //{
                    //    string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.DrivernameObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    MCVehicleDescription.DrivernameObj.Name = val;
                    //}
                    if (unitdetails.SectionData.ValueData != null)
                    {
                        HttpResponseMessage getunit = await hclient.GetAsync("DriverDetails?ApiKey=" + apikey);
                        var EmpResponses = getunit.Content.ReadAsStringAsync().Result;
                        if (EmpResponses != null)
                        {
                            MCVehicleDescription.DriverDatas = JsonConvert.DeserializeObject<DriverList>(EmpResponses);
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.EnumberObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.EnumberObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.EnumberObj.Enumber = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.ExcessObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.ExcessObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.ExcessObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.FmmcmakeObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.FmmcmakeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.FmmcmakeObj.FmMake = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.FmmcscdObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.FmmcscdObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.FmmcscdObj.FmScd = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.FmmctypeObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.FmmctypeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.FmmctypeObj.FmFamily = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.FmmcyearObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.FmmcyearObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.FmmcyearObj.FmYear = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.FmmcyearObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.FmmcyearObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.FmmcyearObj.FmYear = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.KeptnightObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.KeptnightObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.KeptnightObj.Keptnight = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.LimitindemnityObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.LimitindemnityObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.LimitindemnityObj.Indemnity = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.LscategoryObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.LscategoryObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.LscategoryObj.Category = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.MaxMarvalObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.MaxMarvalObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.MaxMarvalObj.Marketvalue = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.MCfamilyObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.MCfamilyObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.MCfamilyObj.Family = val;
                    }
                    //if(unitdetails.SectionData.RowsourceData.Exists(p=>p.Element.ElId==MCVehicleDescription.MCfamilyObj.EiId))
                    //{
                    //    List<Options> lstopt = new List<Options>();
                    //   // lstopt = unitdetails.SectionData.RowsourceData.Where(p => p.Element.ElId == MCVehicleDescription.MCfamilyObj.EiId).Select(p=>p.Options).ToList();
                    //    MCVehicleDescription.MCfamilyObj.FamilyList = FamilyLists;

                    //}
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.McmakeObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.McmakeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.McmakeObj.Make = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.MCscdObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.MCscdObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.MCscdObj.Scd = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.McyearObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.McyearObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.McyearObj.Year = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.NoclaimbonusObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.NoclaimbonusObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.NoclaimbonusObj.Bonus = val;
                    }
                    //if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.OwnersmanualObj.EiId))
                    //{
                    //    string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.OwnersmanualObj.EiId).Select(p => p.Value).FirstOrDefault();
                    //    MCVehicleDescription.OwnersmanualObj.Ownersmanual = val;
                    //}
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.RatingObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.RatingObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.RatingObj.Rating = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.RnumberObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.RnumberObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.RnumberObj.Rnumber = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.UnspecifieditemsObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.UnspecifieditemsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.UnspecifieditemsObj.Item = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.UsevehicleObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.UsevehicleObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.UsevehicleObj.Usevehicle = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.VmodifiedObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.VmodifiedObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.VmodifiedObj.Vmodified = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.VnumberObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.VnumberObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.VnumberObj.Vnumber = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.VregisterObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.VregisterObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.VregisterObj.Register = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.Excess21UnderObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.Excess21UnderObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.Excess21UnderObj.Excess21Under = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.ExcessObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.ExcessObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.ExcessObj.Excess = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.Excess25UnderObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.Excess25UnderObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.Excess25UnderObj.Excess25Under = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.AlarmObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.AlarmObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val == "1")
                        {
                            MCVehicleDescription.AlarmObj.Alarm = true;
                        }
                        else if (val == "0")
                        {
                            MCVehicleDescription.AlarmObj.Alarm = false;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.EngineImmobiliserObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.EngineImmobiliserObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val == "1")
                        {
                            MCVehicleDescription.EngineImmobiliserObj.Engineimmobiliser = true;
                        }
                        else if (val == "0")
                        {
                            MCVehicleDescription.EngineImmobiliserObj.Engineimmobiliser = false;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.PrivateUseObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.PrivateUseObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val == "1")
                        {
                            MCVehicleDescription.PrivateUseObj.Private = true;
                        }
                        else if (val == "0")
                        {
                            MCVehicleDescription.PrivateUseObj.Private = false;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.FarmUseObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.FarmUseObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val == "1")
                        {
                            MCVehicleDescription.FarmUseObj.Farm = true;
                        }
                        else if (val == "0")
                        {
                            MCVehicleDescription.FarmUseObj.Farm = false;
                        }
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.BusinessUseObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.BusinessUseObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val == "1")
                        {
                            MCVehicleDescription.BusinessUseObj.Business = true;
                        }
                        else if (val == "0")
                        {
                            MCVehicleDescription.BusinessUseObj.Business = false;
                        }
                    }
                }
            }
            #endregion
            if (unitdetails != null && unitdetails.ReferralList != null)
            {
                MCVehicleDescription.ReferralList = unitdetails.ReferralList;
                MCVehicleDescription.ReferralList.Replace("&nbsp;&nbsp;&nbsp;&nbsp", "");
                MCVehicleDescription.Referels = new List<string>();
                string[] delim = { "<br/>" };
                string[] spltd = MCVehicleDescription.ReferralList.Split(delim, StringSplitOptions.None);
                if (spltd != null && spltd.Count() > 0)
                {
                    for (int i = 0; i < spltd.Count(); i++)
                    {
                        MCVehicleDescription.Referels.Add(spltd[i].Replace("&nbsp;&nbsp;&nbsp;&nbsp", ""));
                    }
                }
            }
            if (cid != null && cid.HasValue)
            {
                MCVehicleDescription.CustomerId = cid.Value;
            }
            if (PcId != null && PcId.HasValue)
            {
                MCVehicleDescription.PcId = PcId;
            }
            if (MCVehicleDescription.DriverDatas.DriverData != null && MCVehicleDescription.DriverDatas.DriverData.Count() == 0)
            {
                dr.Name = "";
                dr.AccidentsAndConvictions = "";
                dr.Dob = "";
                dr.DriverNumber = "";
                dr.Gender = "1";
                dr.YearLicensed = 0;
                MCVehicleDescription.DriverDatas.DriverData.Add(dr);
            }
            Session["Controller"] = "MotorCover";
            Session["ActionName"] = "VehicleDescription";
            return View(MCVehicleDescription);
        }
        [HttpPost]
        public ActionResult VehicleDescription(int? cid, MCVehicleDescription MCVehicleDescription)
        {
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCVehicleDescription.CustomerId = cid.Value;
            }
            else
            {
                cid = MCVehicleDescription.CustomerId;
                ViewBag.cid = MCVehicleDescription.CustomerId;
            }
            NewPolicyDetailsClass MCVehicleDescriptionmodel = new NewPolicyDetailsClass();
            List<SelectListItem> VehicleMakeList = new List<SelectListItem>();
            VehicleMakeList = MCVehicleDescriptionmodel.VehicleMake();
            List<SelectListItem> FamilyLists = new List<SelectListItem>();
            FamilyLists = MCVehicleDescriptionmodel.MotorCoverFamily();
            MCVehicleDescription.MCfamilyObj.FamilyList = FamilyLists;
            MCVehicleDescription.McmakeObj.MakeList = VehicleMakeList;
            MCVehicleDescription.FmmctypeObj.FmFamilyList = FamilyLists;
            string policyid = null;
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
            //    return RedirectToAction(actionname, controllername, new { cid = MCVehicleDescription.CustomerId, PcId = MCVehicleDescription.PcId });
            //}
            return RedirectToAction("BoatDetails", "Boat", new { cid = MCVehicleDescription.CustomerId, PcId = MCVehicleDescription.PcId });
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Drivers(string dataToSend)
        {
            DriverList drs = new DriverList();
            List<Driver> newdriver = new List<Driver>();
            drs.DriverData = new List<Driver>();
            var users = new JavaScriptSerializer().Deserialize<List<Driver>>(dataToSend);
            if (users != null && users.Count() > 0)
            {
                for (int i = 0; i < users.Count(); i++)
                {
                    Driver dr = new Driver();
                    dr.AccidentsAndConvictions = users[i].AccidentsAndConvictions;
                    dr.Name = users[i].Name;
                    dr.Dob = users[i].Dob;
                    if (users[i].Gender == "1")
                    {
                        dr.Gender = "M";
                    }
                    else
                    {
                        dr.Gender = "F";
                    }
                    dr.DriverNumber = "0";
                    dr.YearLicensed = users[i].YearLicensed;
                    newdriver.Add(dr);
                }
            }
            drs.DriverData = newdriver;
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (Request.IsAjaxRequest())
            {
                if (Session["ApiKey"] != null)
                {
                    string ApiKey = Session["apiKey"].ToString();
                    drs.ApiKey = ApiKey;
                }
                else
                {
                    return Json(new { Status = false, data = "login" });
                    return RedirectToAction("AgentLogin", "Login");
                }
                StringContent content = new StringContent(JsonConvert.SerializeObject(drs), Encoding.UTF8, "application/json");
                var responses = await hclient.PostAsync("DriverDetails?", content);
                var result = await responses.Content.ReadAsStringAsync();
                if (result != null)
                {
                }
                }
            return Json(new { Status = true });
        }

        [HttpPost]
        public ActionResult MotorAjaxcontent(int id, string content)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            if (Request.IsAjaxRequest())
            {
                int cid = 1;
                ViewBag.cid = cid;
                if (content == "coverDetails")
                {
                    List<SelectListItem> descriptionList = new List<SelectListItem>();
                    descriptionList = commonModel.MCCDDescription();
                    return Json(new { status = true, des = descriptionList });
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