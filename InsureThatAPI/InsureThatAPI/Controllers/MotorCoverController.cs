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
            List<SelectListItem> AddressList = new List<SelectListItem>();
            AddressList = MCVehicleDescriptionmodel.MCADAddress();
            List<SelectListItem> VNList = new List<SelectListItem>();
            VNList = MCVehicleDescriptionmodel.MCADVinNumber();
            List<SelectListItem> ENList = new List<SelectListItem>();
            ENList = MCVehicleDescriptionmodel.MCADEngineNumber();

            List<SelectListItem> descriptionList = new List<SelectListItem>();
            descriptionList = MCVehicleDescriptionmodel.MCCDDescription();

            List<SelectListItem> BasicExcessList = new List<SelectListItem>();
            BasicExcessList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            BasicExcessList.Add(new SelectListItem { Value = "60953", Text = "Under 21 Excess" });
            BasicExcessList.Add(new SelectListItem { Value = "60955", Text = "Under 25 Excess" });
            string policyid = null;
            var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
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
                if (Policyincllist != null)
                {
                    if (Policyincllist.Exists(p => p.name == "Motor"))
                    {
                    }
                    else
                    {
                        if (Policyincllist.Exists(p => p.name == "Pet" || p.name=="Pets"))
                        {
                            return RedirectToAction("PetsCover", "Pets", new { cid = cid, PcId = PcId });
                        }
                        if (Policyincllist.Exists(p => p.name == "Motor" || p.name=="Motors"))
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
            MCVehicleDescription.FmmctypeObj.EiId = 60779;
            MCVehicleDescription.FmmcscdObj = new FMMCSelectCorDetails();
            MCVehicleDescription.FmmcscdObj.EiId = 60781;
            #endregion
            #region AdditionDetails
            MCVehicleDescription.KeptnightObj = new MCADKeptAtNight();
            MCVehicleDescription.KeptnightObj.EiId = 60793;
            MCVehicleDescription.AdaddressObj = new MCADAddress();
            MCVehicleDescription.AdaddressObj.AddressList = AddressList;
            MCVehicleDescription.AdaddressObj.EiId = 0;
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
            MCVehicleDescription.SFinstalledObj = new MCADSecurityFeaturesInstalled();
            MCVehicleDescription.SFinstalledObj.EiId = 60809;
            MCVehicleDescription.VusedObj = new MCADVehicleUsed();
            MCVehicleDescription.VusedObj.EiId = 60821;
            MCVehicleDescription.CcapacityObj = new MCADCarryingCapacity();
            MCVehicleDescription.CcapacityObj.EiId = 60811;
            #endregion
            #region Driver
            MCVehicleDescription.DrivernameObj = new DriverName();
            MCVehicleDescription.DrivernameObj.EiId = 60843;
            MCVehicleDescription.DriverageObj = new DriverAge();
            MCVehicleDescription.DriverageObj.EiId = 60843;
            MCVehicleDescription.DrivergenderObj = new DriverGender();
            MCVehicleDescription.DrivergenderObj.GenderList = DriversGendarList;
            MCVehicleDescription.DrivergenderObj.EiId = 60843;
            MCVehicleDescription.DriveramicObj = new DriverAmic();
            MCVehicleDescription.DriveramicObj.EiId = 60843;
            MCVehicleDescription.UsevehicleObj = new UseOfVehicle();
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
            if (Session["unId"] != null && Session["profileId"] != null)
            {
                unid = Convert.ToInt32(Session["unId"]);
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
                if (PcId == null && Session["unId"] == null && Session["profileId"] == null)
                {
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=Motor&SectionUnId=&ProfileUnId=");
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
                            MCVehicleDescription.PolicyInclusions = Policyincllist;
                            Session["Policyinclustions"] = Policyincllist;

                        }
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
                }

                if (unitdetails.SectionData != null && unitdetails.SectionData.ValueData != null)
                {
                    if (unitdetails.SectionData != null && unitdetails.SectionData.AddressData != null)
                    {
                        MCVehicleDescription.AdaddressObj.Address = unitdetails.SectionData.AddressData.AddressLine1 + ", " + unitdetails.SectionData.AddressData.Suburb + ", " + unitdetails.SectionData.AddressData.State + ", " + unitdetails.SectionData.AddressData.Postcode;
                    }
                    else if(unitdetails.AddressData!=null && unitdetails.AddressData.Count()>0)
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
                        MCVehicleDescription.CaroptionObj.Caroption = val;
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
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.DmodifiedObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.DmodifiedObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.DmodifiedObj.Dmodified = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.DriverageObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.DriverageObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.DriverageObj.Age = val;
                    }
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.DrivernameObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.DrivernameObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.DrivernameObj.Name = val;
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
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.SFinstalledObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.SFinstalledObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val == "1")
                        {
                            MCVehicleDescription.SFinstalledObj.Installed = true;
                        }
                        else if (val == "0")
                        {
                            MCVehicleDescription.SFinstalledObj.Installed = false;

                        }
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
                    if (unitdetails.SectionData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.VusedObj.EiId))
                    {
                        string val = unitdetails.SectionData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.VusedObj.EiId).Select(p => p.Value).FirstOrDefault();
                        if (val == "1")
                        {
                            MCVehicleDescription.VusedObj.Vused = true;
                        }
                        else if (val == "0")
                        {
                            MCVehicleDescription.VusedObj.Vused = false;
                        }
                    }
                }
            }
            #endregion
            if (unitdetails!=null && unitdetails.ReferralList != null)
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
                        MCVehicleDescription.Referels.Add(spltd[i].Replace("&nbsp;&nbsp;&nbsp;&nbsp", " "));
                    }
                }

            }
            if(cid!=null && cid.HasValue)
            {
                MCVehicleDescription.CustomerId = cid.Value;

            }
            if(PcId!=null && PcId.HasValue)
            {
                MCVehicleDescription.PcId = PcId;
            }
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
                return RedirectToAction("BoatDetails", "Boat", new { cid = MCVehicleDescription.CustomerId });
            
            return RedirectToAction("BoatDetails", "Boat", new { cid = MCVehicleDescription.CustomerId });
        }
        //[HttpGet]
        //public ActionResult AdditionalDetails(int? cid)
        //{
        //    NewPolicyDetailsClass MCAdditionalDetailsmodel = new NewPolicyDetailsClass();
        //    List<SelectListItem> AddressList = new List<SelectListItem>();
        //    AddressList = MCAdditionalDetailsmodel.MCADAddress();
        //    List<SelectListItem> VNList = new List<SelectListItem>();
        //    VNList = MCAdditionalDetailsmodel.MCADVinNumber();
        //    List<SelectListItem> ENList = new List<SelectListItem>();
        //    ENList = MCAdditionalDetailsmodel.MCADEngineNumber();

        //    MCAdditionalDetails MCAdditionalDetails = new MCAdditionalDetails();
        //    MCAdditionalDetails.KeptnightObj = new MCADKeptAtNight();
        //    MCAdditionalDetails.KeptnightObj.EiId = 60793;
        //    MCAdditionalDetails.AdaddressObj = new MCADAddress();
        //    MCAdditionalDetails.AdaddressObj.AddressList = AddressList;
        //    MCAdditionalDetails.AdaddressObj.EiId = 0;
        //    MCAdditionalDetails.VregisterObj = new MCADVehicleRegistered();
        //    MCAdditionalDetails.VregisterObj.EiId = 60797;
        //    MCAdditionalDetails.RnumberObj = new MCADRegistrationNumber();
        //    MCAdditionalDetails.RnumberObj.EiId = 60799;
        //    MCAdditionalDetails.VnumberObj = new MCADVinNumber();
        //    MCAdditionalDetails.VnumberObj.VnumberList = VNList;
        //    MCAdditionalDetails.VnumberObj.EiId = 60801;
        //    MCAdditionalDetails.EnumberObj = new MCADEngineNumber();
        //    MCAdditionalDetails.EnumberObj.EnumberList = ENList;
        //    MCAdditionalDetails.EnumberObj.EiId = 60803;
        //    MCAdditionalDetails.VmodifiedObj = new MCADVehicleModified();
        //    MCAdditionalDetails.VmodifiedObj.EiId = 60805;
        //    MCAdditionalDetails.DmodifiedObj = new MCADdescribeModified();
        //    MCAdditionalDetails.DmodifiedObj.EiId = 60807;
        //    MCAdditionalDetails.SFinstalledObj = new MCADSecurityFeaturesInstalled();
        //    MCAdditionalDetails.SFinstalledObj.EiId = 60809;
        //    MCAdditionalDetails.VusedObj = new MCADVehicleUsed();
        //    MCAdditionalDetails.VusedObj.EiId = 60821;
        //    MCAdditionalDetails.CcapacityObj = new MCADCarryingCapacity();
        //    MCAdditionalDetails.CcapacityObj.EiId = 60811;
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        MCAdditionalDetails.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = MCAdditionalDetails.CustomerId;
        //    }
        //    if (Session["completionTrackMC"] != null)
        //    {
        //        Session["completionTrackMC"] = Session["completionTrackMC"];
        //        MCAdditionalDetails.completionTrackMC = Session["completionTrackMC"].ToString();
        //    }
        //    else
        //    {
        //        Session["completionTrackMC"] = "0-0-0-0-0-0"; ;
        //        MCAdditionalDetails.completionTrackMC = Session["completionTrackMC"].ToString();
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Motor), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
        //    if (details != null && details.Any())
        //    {
        //        if (details.Exists(q => q.QuestionId == MCAdditionalDetails.KeptnightObj.EiId))
        //        {
        //            MCAdditionalDetails.KeptnightObj.Keptnight = Convert.ToString(details.Where(q => q.QuestionId == MCAdditionalDetails.KeptnightObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == MCAdditionalDetails.KeptnightObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == MCAdditionalDetails.KeptnightObj.EiId).FirstOrDefault();
        //            MCAdditionalDetails.KeptnightObj.Keptnight = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //        if (details.Exists(q => q.QuestionId == MCAdditionalDetails.VregisterObj.EiId))
        //        {
        //            MCAdditionalDetails.VregisterObj.Register = Convert.ToString(details.Where(q => q.QuestionId == MCAdditionalDetails.VregisterObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == MCAdditionalDetails.RnumberObj.EiId))
        //        {
        //            MCAdditionalDetails.RnumberObj.Rnumber = Convert.ToString(details.Where(q => q.QuestionId == MCAdditionalDetails.RnumberObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == MCAdditionalDetails.KeptnightObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == MCAdditionalDetails.VnumberObj.EiId).FirstOrDefault();
        //            MCAdditionalDetails.VnumberObj.Vnumber = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //        if (details.Exists(q => q.QuestionId == MCAdditionalDetails.EnumberObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == MCAdditionalDetails.EnumberObj.EiId).FirstOrDefault();
        //            MCAdditionalDetails.EnumberObj.Enumber = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //        if (details.Exists(q => q.QuestionId == MCAdditionalDetails.VmodifiedObj.EiId))
        //        {
        //            MCAdditionalDetails.VmodifiedObj.Vmodified = Convert.ToString(details.Where(q => q.QuestionId == MCAdditionalDetails.VmodifiedObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == MCAdditionalDetails.DmodifiedObj.EiId))
        //        {
        //            MCAdditionalDetails.DmodifiedObj.Dmodified = Convert.ToString(details.Where(q => q.QuestionId == MCAdditionalDetails.DmodifiedObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == MCAdditionalDetails.SFinstalledObj.EiId))
        //        {
        //            MCAdditionalDetails.SFinstalledObj.Installed = Convert.ToBoolean(details.Where(q => q.QuestionId == MCAdditionalDetails.SFinstalledObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == MCAdditionalDetails.VusedObj.EiId))
        //        {
        //            MCAdditionalDetails.VusedObj.Vused = Convert.ToBoolean(details.Where(q => q.QuestionId == MCAdditionalDetails.VusedObj.EiId).FirstOrDefault().Answer);
        //        }
        //        if (details.Exists(q => q.QuestionId == MCAdditionalDetails.CcapacityObj.EiId))
        //        {
        //            MCAdditionalDetails.CcapacityObj.Ccapacity = Convert.ToString(details.Where(q => q.QuestionId == MCAdditionalDetails.CcapacityObj.EiId).FirstOrDefault().Answer);
        //        }
        //    }
        //    return View(MCAdditionalDetails);
        //}
        //[HttpPost]
        //public ActionResult AdditionalDetails(int? cid, MCAdditionalDetails MCAdditionalDetails)
        //{
        //    NewPolicyDetailsClass MCAdditionalDetailsmodel = new NewPolicyDetailsClass();
        //    List<SelectListItem> AddressList = new List<SelectListItem>();
        //    AddressList = MCAdditionalDetailsmodel.MCADAddress();
        //    List<SelectListItem> VNList = new List<SelectListItem>();
        //    VNList = MCAdditionalDetailsmodel.MCADVinNumber();
        //    List<SelectListItem> ENList = new List<SelectListItem>();
        //    ENList = MCAdditionalDetailsmodel.MCADEngineNumber();

        //    MCAdditionalDetails.AdaddressObj.AddressList = AddressList;
        //    MCAdditionalDetails.VnumberObj.VnumberList = VNList;
        //    MCAdditionalDetails.EnumberObj.EnumberList = ENList;

        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        MCAdditionalDetails.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = MCAdditionalDetails.CustomerId;
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    if (cid.HasValue && cid > 0)
        //    {
        //        if (MCAdditionalDetails.KeptnightObj != null && MCAdditionalDetails.KeptnightObj.EiId > 0 && MCAdditionalDetails.KeptnightObj.Keptnight != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCAdditionalDetails.KeptnightObj.EiId, MCAdditionalDetails.KeptnightObj.Keptnight.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCAdditionalDetails.AdaddressObj != null && MCAdditionalDetails.AdaddressObj.EiId > 0 && MCAdditionalDetails.AdaddressObj.Address != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCAdditionalDetails.AdaddressObj.EiId, MCAdditionalDetails.AdaddressObj.Address.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCAdditionalDetails.VregisterObj != null && MCAdditionalDetails.VregisterObj.EiId > 0 && MCAdditionalDetails.VregisterObj.Register != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCAdditionalDetails.VregisterObj.EiId, MCAdditionalDetails.VregisterObj.Register.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCAdditionalDetails.RnumberObj != null && MCAdditionalDetails.RnumberObj.EiId > 0 && MCAdditionalDetails.RnumberObj.Rnumber != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCAdditionalDetails.RnumberObj.EiId, MCAdditionalDetails.RnumberObj.Rnumber.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCAdditionalDetails.VnumberObj != null && MCAdditionalDetails.VnumberObj.EiId > 0 && MCAdditionalDetails.VnumberObj.Vnumber != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCAdditionalDetails.VnumberObj.EiId, MCAdditionalDetails.VnumberObj.Vnumber.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCAdditionalDetails.EnumberObj != null && MCAdditionalDetails.EnumberObj.EiId > 0 && MCAdditionalDetails.EnumberObj.Enumber != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCAdditionalDetails.EnumberObj.EiId, MCAdditionalDetails.EnumberObj.Enumber.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCAdditionalDetails.VmodifiedObj != null && MCAdditionalDetails.VmodifiedObj.EiId > 0 && MCAdditionalDetails.VmodifiedObj.Vmodified != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCAdditionalDetails.VmodifiedObj.EiId, MCAdditionalDetails.VmodifiedObj.Vmodified.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCAdditionalDetails.DmodifiedObj != null && MCAdditionalDetails.DmodifiedObj.EiId > 0 && MCAdditionalDetails.DmodifiedObj.Dmodified != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCAdditionalDetails.DmodifiedObj.EiId, MCAdditionalDetails.DmodifiedObj.Dmodified.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCAdditionalDetails.SFinstalledObj != null && MCAdditionalDetails.SFinstalledObj.EiId > 0 && MCAdditionalDetails.SFinstalledObj.Installed != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCAdditionalDetails.SFinstalledObj.EiId, MCAdditionalDetails.SFinstalledObj.Installed.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCAdditionalDetails.VusedObj != null && MCAdditionalDetails.VusedObj.EiId > 0 && MCAdditionalDetails.VusedObj.Vused != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCAdditionalDetails.VusedObj.EiId, MCAdditionalDetails.VusedObj.Vused.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCAdditionalDetails.CcapacityObj != null && MCAdditionalDetails.CcapacityObj.EiId > 0 && MCAdditionalDetails.CcapacityObj.Ccapacity != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCAdditionalDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCAdditionalDetails.CcapacityObj.EiId, MCAdditionalDetails.CcapacityObj.Ccapacity.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Session["completionTrackMC"] != null)
        //        {
        //            Session["completionTrackMC"] = Session["completionTrackMC"];
        //            MCAdditionalDetails.completionTrackMC = Session["completionTrackMC"].ToString();
        //            if (MCAdditionalDetails.completionTrackMC != null)
        //            {
        //                string Completionstring = string.Empty;
        //                char[] arr = MCAdditionalDetails.completionTrackMC.ToCharArray();

        //                for (int i = 0; i < arr.Length; i++)
        //                {
        //                    char a = arr[i];
        //                    if (i == 2)
        //                    {
        //                        a = '1';
        //                    }
        //                    Completionstring = Completionstring + a;
        //                }
        //                Session["completionTrackMC"] = Completionstring;
        //                MCAdditionalDetails.completionTrackMC = Completionstring;
        //            }
        //        }
        //        else
        //        {
        //            Session["completionTrackMC"] = "0-1-0-0-0-0"; ;
        //            MCAdditionalDetails.completionTrackMC = Session["completionTrackMC"].ToString();
        //        }
        //        return RedirectToAction("Drivers", new { cid = MCAdditionalDetails.CustomerId });
        //    }
        //    return View(MCAdditionalDetails);
        //}
        //[HttpGet]
        //public ActionResult Drivers(int? cid)
        //{
        //    List<SelectListItem> DriversGendarList = new List<SelectListItem>();
        //    DriversGendarList.Add(new SelectListItem { Value = "", Text = "--Select--" });
        //    DriversGendarList.Add(new SelectListItem { Value = "1", Text = "Male" });
        //    DriversGendarList.Add(new SelectListItem { Value = "2", Text = "Female" });
        //    MCDrivers MCDrivers = new MCDrivers();
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        MCDrivers.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = MCDrivers.CustomerId;
        //    }
        //    MCDrivers.DrivernameObj = new DriverName();
        //    MCDrivers.DrivernameObj.EiId = 60843;
        //    MCDrivers.DriverageObj = new DriverAge();
        //    MCDrivers.DriverageObj.EiId = 60843;
        //    MCDrivers.DrivergenderObj = new DriverGender();
        //    MCDrivers.DrivergenderObj.GenderList = DriversGendarList;
        //    MCDrivers.DrivergenderObj.EiId = 60843;
        //    MCDrivers.DriveramicObj = new DriverAmic();
        //    MCDrivers.DriveramicObj.EiId = 60843;
        //    MCDrivers.UsevehicleObj = new UseOfVehicle();
        //    MCDrivers.UsevehicleObj.EiId = 60845;
        //    if (Session["completionTrackMC"] != null)
        //    {
        //        Session["completionTrackMC"] = Session["completionTrackMC"];
        //        MCDrivers.completionTrackMC = Session["completionTrackMC"].ToString();
        //    }
        //    else
        //    {
        //        Session["completionTrackMC"] = "0-0-0-0-0-0"; ;
        //        MCDrivers.completionTrackMC = Session["completionTrackMC"].ToString();
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Motor), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
        //    if (details != null && details.Any())
        //    {
        //        if (details.Exists(q => q.QuestionId == MCDrivers.DrivernameObj.EiId))
        //        {
        //            MCDrivers.DrivernameObj.Name = details.Where(q => q.QuestionId == MCDrivers.DrivernameObj.EiId).FirstOrDefault().Answer;
        //        }
        //        if (details.Exists(q => q.QuestionId == MCDrivers.DriverageObj.EiId))
        //        {
        //            MCDrivers.DriverageObj.Age = details.Where(q => q.QuestionId == MCDrivers.DriverageObj.EiId).FirstOrDefault().Answer;
        //        }
        //        if (details.Exists(q => q.QuestionId == MCDrivers.DrivergenderObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == MCDrivers.DrivergenderObj.EiId).FirstOrDefault();
        //            MCDrivers.DrivergenderObj.Gender = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //        if (details.Exists(q => q.QuestionId == MCDrivers.DriveramicObj.EiId))
        //        {
        //            MCDrivers.DriveramicObj.Amic = details.Where(q => q.QuestionId == MCDrivers.DriveramicObj.EiId).FirstOrDefault().Answer;
        //        }
        //        if (details.Exists(q => q.QuestionId == MCDrivers.UsevehicleObj.EiId))
        //        {
        //            MCDrivers.UsevehicleObj.Usevehicle = details.Where(q => q.QuestionId == MCDrivers.UsevehicleObj.EiId).FirstOrDefault().Answer;
        //        }
        //    }
        //    return View(MCDrivers);
        //}
        //[HttpPost]
        //public ActionResult Drivers(int? cid, MCDrivers MCDrivers)
        //{
        //    List<SelectListItem> DriversGendarList = new List<SelectListItem>();
        //    DriversGendarList.Add(new SelectListItem { Value = "", Text = "--Select--" });
        //    DriversGendarList.Add(new SelectListItem { Value = "1", Text = "Male" });
        //    DriversGendarList.Add(new SelectListItem { Value = "2", Text = "Female" });
        //    MCDrivers.DrivergenderObj.GenderList = DriversGendarList;
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        MCDrivers.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = MCDrivers.CustomerId;
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    if (cid.HasValue && cid > 0)
        //    {
        //        if (MCDrivers.DrivernameObj != null && MCDrivers.DrivernameObj.EiId > 0 && MCDrivers.DrivernameObj.Name != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCDrivers.CustomerId, Convert.ToInt32(RLSSection.Motor), MCDrivers.DrivernameObj.EiId, MCDrivers.DrivernameObj.Name.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCDrivers.DriverageObj != null && MCDrivers.DriverageObj.EiId > 0 && MCDrivers.DriverageObj.Age != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCDrivers.CustomerId, Convert.ToInt32(RLSSection.Motor), MCDrivers.DriverageObj.EiId, MCDrivers.DriverageObj.Age.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCDrivers.DrivergenderObj != null && MCDrivers.DrivergenderObj.EiId > 0 && MCDrivers.DrivergenderObj.Gender != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCDrivers.CustomerId, Convert.ToInt32(RLSSection.Motor), MCDrivers.DrivergenderObj.EiId, MCDrivers.DrivergenderObj.Gender.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCDrivers.DriveramicObj != null && MCDrivers.DriveramicObj.EiId > 0 && MCDrivers.DriveramicObj.Amic != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCDrivers.CustomerId, Convert.ToInt32(RLSSection.Motor), MCDrivers.DriveramicObj.EiId, MCDrivers.DriveramicObj.Amic.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCDrivers.UsevehicleObj != null && MCDrivers.UsevehicleObj.EiId > 0 && MCDrivers.UsevehicleObj.Usevehicle != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCDrivers.CustomerId, Convert.ToInt32(RLSSection.Motor), MCDrivers.UsevehicleObj.EiId, MCDrivers.UsevehicleObj.Usevehicle.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Session["completionTrackMC"] != null)
        //        {
        //            Session["completionTrackMC"] = Session["completionTrackMC"];
        //            MCDrivers.completionTrackMC = Session["completionTrackMC"].ToString();
        //            if (MCDrivers.completionTrackMC != null)
        //            {
        //                string Completionstring = string.Empty;
        //                char[] arr = MCDrivers.completionTrackMC.ToCharArray();

        //                for (int i = 0; i < arr.Length; i++)
        //                {
        //                    char a = arr[i];
        //                    if (i == 4)
        //                    {
        //                        a = '1';
        //                    }
        //                    Completionstring = Completionstring + a;
        //                }
        //                Session["completionTrackMC"] = Completionstring;
        //                MCDrivers.completionTrackMC = Completionstring;
        //            }
        //        }
        //        else
        //        {
        //            Session["completionTrackMC"] = "0-0-1-0-0-0"; ;
        //            MCDrivers.completionTrackMC = Session["completionTrackMC"].ToString();
        //        }
        //        return RedirectToAction("CoverDetails", new { cid = MCDrivers.CustomerId });
        //    }
        //    return View();
        //}
        //[HttpGet]
        //public ActionResult CoverDetails(int? cid)
        //{
        //    NewPolicyDetailsClass CoverDetailsmodel = new NewPolicyDetailsClass();
        //    List<SelectListItem> descriptionList = new List<SelectListItem>();
        //    descriptionList = CoverDetailsmodel.MCCDDescription();
        //    MCCoverDetails MCCoverDetails = new MCCoverDetails();
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        MCCoverDetails.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = MCCoverDetails.CustomerId;
        //    }
        //    MCCoverDetails.CoveroptionObj = new CoverOptionCD();
        //    MCCoverDetails.CoveroptionObj.EiId = 60859;
        //    MCCoverDetails.CovertypeObj = new CoverTypeCD();
        //    MCCoverDetails.CovertypeObj.EiId = 60861;
        //    MCCoverDetails.MaxMarvalObj = new MaximumMarketValue();
        //    MCCoverDetails.MaxMarvalObj.EiId = 60865;
        //    MCCoverDetails.CaravanannexObj = new CaravanAnnex();
        //    MCCoverDetails.CaravanannexObj.EiId = 60871;
        //    MCCoverDetails.UnspecifieditemsObj = new UnspecifiedItems();
        //    MCCoverDetails.UnspecifieditemsObj.EiId = 60873;
        //    MCCoverDetails.AccessoriesObj = new NonStandardAccessories();
        //    MCCoverDetails.AccessoriesObj.EiId = 60877;
        //    MCCoverDetails.DescriptionObj = new AccessoryDescriptionCD();
        //    MCCoverDetails.DescriptionObj.DescriptionList = descriptionList;
        //    MCCoverDetails.DescriptionObj.EiId = 60891;
        //    MCCoverDetails.SumnsuredObj = new SumInsuredCD();
        //    MCCoverDetails.SumnsuredObj.EiId = 60893;
        //    MCCoverDetails.LimitindemnityObj = new LimitOfIndemnityDC();
        //    MCCoverDetails.LimitindemnityObj.EiId = 60905;
        //    MCCoverDetails.RatingObj = new RatingDC();
        //    MCCoverDetails.RatingObj.EiId = 60917;
        //    MCCoverDetails.NoclaimbonusObj = new NoClaimBonus();
        //    MCCoverDetails.NoclaimbonusObj.EiId = 60919;
        //    if (Session["completionTrackMC"] != null)
        //    {
        //        Session["completionTrackMC"] = Session["completionTrackMC"];
        //        MCCoverDetails.completionTrackMC = Session["completionTrackMC"].ToString();
        //    }
        //    else
        //    {
        //        Session["completionTrackMC"] = "0-0-0-0-0-0"; ;
        //        MCCoverDetails.completionTrackMC = Session["completionTrackMC"].ToString();
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Motor), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
        //    if (details != null && details.Any())
        //    {
        //        if (details.Exists(q => q.QuestionId == MCCoverDetails.CoveroptionObj.EiId))
        //        {
        //            MCCoverDetails.CoveroptionObj.Coveroption = details.Where(q => q.QuestionId == MCCoverDetails.CoveroptionObj.EiId).FirstOrDefault().Answer;
        //        }
        //        if (details.Exists(q => q.QuestionId == MCCoverDetails.CovertypeObj.EiId))
        //        {
        //            MCCoverDetails.CovertypeObj.Covertype = details.Where(q => q.QuestionId == MCCoverDetails.CovertypeObj.EiId).FirstOrDefault().Answer;
        //        }
        //        if (details.Exists(q => q.QuestionId == MCCoverDetails.MaxMarvalObj.EiId))
        //        {
        //            MCCoverDetails.MaxMarvalObj.Marketvalue = details.Where(q => q.QuestionId == MCCoverDetails.MaxMarvalObj.EiId).FirstOrDefault().Answer;
        //        }
        //        if (details.Exists(q => q.QuestionId == MCCoverDetails.CaravanannexObj.EiId))
        //        {
        //            MCCoverDetails.CaravanannexObj.Annex = details.Where(q => q.QuestionId == MCCoverDetails.CaravanannexObj.EiId).FirstOrDefault().Answer;
        //        }
        //        if (details.Exists(q => q.QuestionId == MCCoverDetails.DescriptionObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == MCCoverDetails.DescriptionObj.EiId).FirstOrDefault();
        //            MCCoverDetails.DescriptionObj.Description = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }

        //    }
        //    return View(MCCoverDetails);
        //}
        //[HttpPost]
        //public ActionResult CoverDetails(int? cid, MCCoverDetails MCCoverDetails)
        //{
        //    NewPolicyDetailsClass CoverDetailsmodel = new NewPolicyDetailsClass();
        //    List<SelectListItem> descriptionList = new List<SelectListItem>();
        //    descriptionList = CoverDetailsmodel.MCCDDescription();
        //    MCCoverDetails.DescriptionObj.DescriptionList = descriptionList;
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        MCCoverDetails.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = MCCoverDetails.CustomerId;
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    if (cid.HasValue && cid > 0)
        //    {
        //        if (MCCoverDetails.CoveroptionObj != null && MCCoverDetails.CoveroptionObj.EiId > 0 && MCCoverDetails.CoveroptionObj.Coveroption != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.CoveroptionObj.EiId, MCCoverDetails.CoveroptionObj.Coveroption.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCCoverDetails.CovertypeObj != null && MCCoverDetails.CovertypeObj.EiId > 0 && MCCoverDetails.CovertypeObj.Covertype != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.CovertypeObj.EiId, MCCoverDetails.CovertypeObj.Covertype.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCCoverDetails.MaxMarvalObj != null && MCCoverDetails.MaxMarvalObj.EiId > 0 && MCCoverDetails.MaxMarvalObj.Marketvalue != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.MaxMarvalObj.EiId, MCCoverDetails.MaxMarvalObj.Marketvalue.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCCoverDetails.CaravanannexObj != null && MCCoverDetails.CaravanannexObj.EiId > 0 && MCCoverDetails.CaravanannexObj.Annex != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.CaravanannexObj.EiId, MCCoverDetails.CaravanannexObj.Annex.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCCoverDetails.UnspecifieditemsObj != null && MCCoverDetails.UnspecifieditemsObj.EiId > 0 && MCCoverDetails.UnspecifieditemsObj.Item != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.UnspecifieditemsObj.EiId, MCCoverDetails.UnspecifieditemsObj.Item.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCCoverDetails.AccessoriesObj != null && MCCoverDetails.AccessoriesObj.EiId > 0 && MCCoverDetails.AccessoriesObj.Accessories != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.AccessoriesObj.EiId, MCCoverDetails.AccessoriesObj.Accessories.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCCoverDetails.DescriptionObj != null && MCCoverDetails.DescriptionObj.EiId > 0 && MCCoverDetails.DescriptionObj.Description != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.DescriptionObj.EiId, MCCoverDetails.DescriptionObj.Description.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCCoverDetails.SumnsuredObj != null && MCCoverDetails.SumnsuredObj.EiId > 0 && MCCoverDetails.SumnsuredObj.Suminsured != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.SumnsuredObj.EiId, MCCoverDetails.SumnsuredObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCCoverDetails.LimitindemnityObj != null && MCCoverDetails.LimitindemnityObj.EiId > 0 && MCCoverDetails.LimitindemnityObj.Indemnity != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.LimitindemnityObj.EiId, MCCoverDetails.LimitindemnityObj.Indemnity.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCCoverDetails.RatingObj != null && MCCoverDetails.RatingObj.EiId > 0 && MCCoverDetails.RatingObj.Rating != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.RatingObj.EiId, MCCoverDetails.RatingObj.Rating.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCCoverDetails.NoclaimbonusObj != null && MCCoverDetails.NoclaimbonusObj.EiId > 0 && MCCoverDetails.NoclaimbonusObj.Bonus != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.NoclaimbonusObj.EiId, MCCoverDetails.NoclaimbonusObj.Bonus.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Session["completionTrackMC"] != null)
        //        {
        //            Session["completionTrackMC"] = Session["completionTrackMC"];
        //            MCCoverDetails.completionTrackMC = Session["completionTrackMC"].ToString();
        //            if (MCCoverDetails.completionTrackMC != null)
        //            {
        //                string Completionstring = string.Empty;
        //                char[] arr = MCCoverDetails.completionTrackMC.ToCharArray();

        //                for (int i = 0; i < arr.Length; i++)
        //                {
        //                    char a = arr[i];
        //                    if (i == 6)
        //                    {
        //                        a = '1';
        //                    }
        //                    Completionstring = Completionstring + a;
        //                }
        //                Session["completionTrackMC"] = Completionstring;
        //                MCCoverDetails.completionTrackMC = Completionstring;
        //            }
        //        }
        //        else
        //        {
        //            Session["completionTrackMC"] = "0-0-0-1-0-0"; ;
        //            MCCoverDetails.completionTrackMC = Session["completionTrackMC"].ToString();
        //        }
        //        return RedirectToAction("OptionalExtrasExcesses", new { cid = MCCoverDetails.CustomerId });
        //    }
        //    return View(MCCoverDetails);
        //}
        //[HttpGet]
        //public ActionResult OptionalExtrasExcesses(int? cid)
        //{
        //    List<SelectListItem> BasicExcessList = new List<SelectListItem>();
        //    BasicExcessList.Add(new SelectListItem { Value = "", Text = "--Select--" });
        //    BasicExcessList.Add(new SelectListItem { Value = "60953", Text = "Under 21 Excess" });
        //    BasicExcessList.Add(new SelectListItem { Value = "60955", Text = "Under 25 Excess" });
        //    MCOptionalExtrasExcesses MCOptionalExtrasExcesses = new MCOptionalExtrasExcesses();
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        MCOptionalExtrasExcesses.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = MCOptionalExtrasExcesses.CustomerId;
        //    }
        //    MCOptionalExtrasExcesses.CaroptionObj = new HireCarOption();
        //    MCOptionalExtrasExcesses.CaroptionObj.EiId = 60947;
        //    MCOptionalExtrasExcesses.ExcessObj = new BasicExcess();
        //    MCOptionalExtrasExcesses.ExcessObj.ExcessList = BasicExcessList;
        //    MCOptionalExtrasExcesses.ExcessObj.EiId = 60951;
        //    if (Session["completionTrackMC"] != null)
        //    {
        //        Session["completionTrackMC"] = Session["completionTrackMC"];
        //        MCOptionalExtrasExcesses.completionTrackMC = Session["completionTrackMC"].ToString();
        //    }
        //    else
        //    {
        //        Session["completionTrackMC"] = "0-0-0-0-0-0"; ;
        //        MCOptionalExtrasExcesses.completionTrackMC = Session["completionTrackMC"].ToString();
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Motor), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
        //    if (details != null && details.Any())
        //    {
        //        if (details.Exists(q => q.QuestionId == MCOptionalExtrasExcesses.CaroptionObj.EiId))
        //        {
        //            MCOptionalExtrasExcesses.CaroptionObj.Caroption = details.Where(q => q.QuestionId == MCOptionalExtrasExcesses.CaroptionObj.EiId).FirstOrDefault().Answer;
        //        }
        //        if (details.Exists(q => q.QuestionId == MCOptionalExtrasExcesses.ExcessObj.EiId))
        //        {
        //            var loc = details.Where(q => q.QuestionId == MCOptionalExtrasExcesses.ExcessObj.EiId).FirstOrDefault();
        //            MCOptionalExtrasExcesses.ExcessObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
        //        }
        //    }
        //    return View(MCOptionalExtrasExcesses);
        //}
        //[HttpPost]
        //public ActionResult OptionalExtrasExcesses(int? cid, MCOptionalExtrasExcesses MCOptionalExtrasExcesses)
        //{
        //    List<SelectListItem> BasicExcessList = new List<SelectListItem>();
        //    BasicExcessList.Add(new SelectListItem { Value = "", Text = "--Select--" });
        //    BasicExcessList.Add(new SelectListItem { Value = "60953", Text = "Under 21 Excess" });
        //    BasicExcessList.Add(new SelectListItem { Value = "60955", Text = "Under 25 Excess" });
        //    MCOptionalExtrasExcesses.ExcessObj.ExcessList = BasicExcessList;
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        MCOptionalExtrasExcesses.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = MCOptionalExtrasExcesses.CustomerId;
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    if (cid.HasValue && cid > 0)
        //    {
        //        if (MCOptionalExtrasExcesses.CaroptionObj != null && MCOptionalExtrasExcesses.CaroptionObj.EiId > 0 && MCOptionalExtrasExcesses.CaroptionObj.Caroption != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCOptionalExtrasExcesses.CustomerId, Convert.ToInt32(RLSSection.Motor), MCOptionalExtrasExcesses.CaroptionObj.EiId, MCOptionalExtrasExcesses.CaroptionObj.Caroption.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCOptionalExtrasExcesses.ExcessObj != null && MCOptionalExtrasExcesses.ExcessObj.EiId > 0 && MCOptionalExtrasExcesses.ExcessObj.Excess != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCOptionalExtrasExcesses.CustomerId, Convert.ToInt32(RLSSection.Motor), MCOptionalExtrasExcesses.ExcessObj.EiId, MCOptionalExtrasExcesses.ExcessObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Session["completionTrackMC"] != null)
        //        {
        //            Session["completionTrackMC"] = Session["completionTrackMC"];
        //            MCOptionalExtrasExcesses.completionTrackMC = Session["completionTrackMC"].ToString();
        //            if (MCOptionalExtrasExcesses.completionTrackMC != null)
        //            {
        //                string Completionstring = string.Empty;
        //                char[] arr = MCOptionalExtrasExcesses.completionTrackMC.ToCharArray();

        //                for (int i = 0; i < arr.Length; i++)
        //                {
        //                    char a = arr[i];
        //                    if (i == 8)
        //                    {
        //                        a = '1';
        //                    }
        //                    Completionstring = Completionstring + a;
        //                }
        //                Session["completionTrackMC"] = Completionstring;
        //                MCOptionalExtrasExcesses.completionTrackMC = Completionstring;
        //            }
        //        }
        //        else
        //        {
        //            Session["completionTrackMC"] = "0-0-0-0-1-0"; ;
        //            MCOptionalExtrasExcesses.completionTrackMC = Session["completionTrackMC"].ToString();
        //        }
        //        return RedirectToAction("InterestedParties", new { cid = MCOptionalExtrasExcesses.CustomerId });
        //    }
        //    return View(MCOptionalExtrasExcesses);
        //}
        //[HttpGet]
        //public ActionResult InterestedParties(int? cid)
        //{
        //    MCInterestedParties MCInterestedParties = new MCInterestedParties();
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        MCInterestedParties.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = MCInterestedParties.CustomerId;
        //    }
        //    MCInterestedParties.MCPartynameObj = new MCInterestedPartyName();
        //    MCInterestedParties.MCPartynameObj.EiId = 60967;
        //    MCInterestedParties.MCPartyLocationObj = new MCInterestedPartyLocation();
        //    MCInterestedParties.MCPartyLocationObj.EiId = 60969;
        //    if (Session["completionTrackMC"] != null)
        //    {
        //        Session["completionTrackMC"] = Session["completionTrackMC"];
        //        MCInterestedParties.completionTrackMC = Session["completionTrackMC"].ToString();
        //    }
        //    else
        //    {
        //        Session["completionTrackMC"] = "0-0-0-0-0-0"; ;
        //        MCInterestedParties.completionTrackMC = Session["completionTrackMC"].ToString();
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Motor), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
        //    if (details != null && details.Any())
        //    {
        //        if (details.Exists(q => q.QuestionId == MCInterestedParties.MCPartynameObj.EiId))
        //        {
        //            MCInterestedParties.MCPartynameObj.Name = details.Where(q => q.QuestionId == MCInterestedParties.MCPartyLocationObj.EiId).FirstOrDefault().Answer;
        //        }
        //        if (details.Exists(q => q.QuestionId == MCInterestedParties.MCPartyLocationObj.EiId))
        //        {
        //            MCInterestedParties.MCPartyLocationObj.Location = details.Where(q => q.QuestionId == MCInterestedParties.MCPartyLocationObj.EiId).FirstOrDefault().Answer;
        //        }
        //    }
        //    return View(MCInterestedParties);
        //}
        //[HttpPost]
        //public ActionResult InterestedParties(int? cid, MCInterestedParties MCInterestedParties)
        //{
        //    if (cid != null)
        //    {
        //        ViewBag.cid = cid;
        //        MCInterestedParties.CustomerId = cid.Value;
        //    }
        //    else
        //    {
        //        ViewBag.cid = MCInterestedParties.CustomerId;
        //    }
        //    var db = new MasterDataEntities();
        //    string policyid = null;
        //    if (cid.HasValue && cid > 0)
        //    {
        //        if (MCInterestedParties.MCPartynameObj != null && MCInterestedParties.MCPartynameObj.EiId > 0 && MCInterestedParties.MCPartynameObj.Name != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCInterestedParties.CustomerId, Convert.ToInt32(RLSSection.Motor), MCInterestedParties.MCPartynameObj.EiId, MCInterestedParties.MCPartynameObj.Name.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (MCInterestedParties.MCPartyLocationObj != null && MCInterestedParties.MCPartyLocationObj.EiId > 0 && MCInterestedParties.MCPartyLocationObj.Location != null)
        //        {
        //            db.IT_InsertCustomerQnsData(MCInterestedParties.CustomerId, Convert.ToInt32(RLSSection.Motor), MCInterestedParties.MCPartyLocationObj.EiId, MCInterestedParties.MCPartyLocationObj.Location.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
        //        }
        //        if (Session["completionTrackMC"] != null)
        //        {
        //            Session["completionTrackMC"] = Session["completionTrackMC"];
        //            MCInterestedParties.completionTrackMC = Session["completionTrackMC"].ToString();
        //            if (MCInterestedParties.completionTrackMC != null)
        //            {
        //                string Completionstring = string.Empty;
        //                char[] arr = MCInterestedParties.completionTrackMC.ToCharArray();

        //                for (int i = 0; i < arr.Length; i++)
        //                {
        //                    char a = arr[i];
        //                    if (i == 10)
        //                    {
        //                        a = '1';
        //                    }
        //                    Completionstring = Completionstring + a;
        //                }
        //                Session["completionTrackMC"] = Completionstring;
        //                MCInterestedParties.completionTrackMC = Completionstring;
        //            }
        //        }
        //        else
        //        {
        //            Session["completionTrackMC"] = "0-0-0-0-0-1"; ;
        //            MCInterestedParties.completionTrackMC = Session["completionTrackMC"].ToString();
        //        }
        //        return RedirectToAction("BindCover", "Customer", new { cid = cid });
        //        // return RedirectToAction("VehicleDescription", new { cid = MCInterestedParties.CustomerId });
        //    }
        //    return View(MCInterestedParties);
        //}
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