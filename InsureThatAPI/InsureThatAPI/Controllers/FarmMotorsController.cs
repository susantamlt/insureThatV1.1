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
    public class FarmMotorsController : Controller
    {
        // GET: FarmMotors
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> VehicleDescription(int? cid, int? PcId)
        {
            MCVehicleDescription MCVehicleDescription = new MCVehicleDescription();
            string ApiKey = null;
            if(Session["ApiKey"]!=null)
            {
                ApiKey = Session["ApiKey"].ToString();
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCVehicleDescription.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCVehicleDescription.CustomerId;
            }
            var Policyincllist = new List<SessionModel>();
            if (Session["Policyinclustions"] != null)
            {
                Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                if (Policyincllist != null)
                {
                    if (Policyincllist != null)
                    {
                        if (Policyincllist.Exists(p => p.name == "Motor"))
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
            }
            else
            {
                RedirectToAction("PolicyInclustions", "Customer", new { CustomerId = cid, type = 1021 });
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
            #region VehicleDescription
            MCVehicleDescription.LscategoryObj = new GLVCategory();
            MCVehicleDescription.LscategoryObj.EiId = 63807;
            MCVehicleDescription.McmakeObj = new GLVMake();
            MCVehicleDescription.McmakeObj.MakeList = VehicleMakeList;
            MCVehicleDescription.McmakeObj.EiId = 63809;
            MCVehicleDescription.McyearObj = new GLVYear();
            MCVehicleDescription.McyearObj.EiId = 63811;
            MCVehicleDescription.MCfamilyObj = new GLVFamily();
            MCVehicleDescription.MCfamilyObj.FamilyList = FamilyLists;
            MCVehicleDescription.MCfamilyObj.EiId = 63813;
            MCVehicleDescription.MCscdObj = new GLVSelectCorDetails();
            MCVehicleDescription.MCscdObj.EiId = 63815;
            MCVehicleDescription.EstimatedValueObj = new EstimatedRetailValue();
            MCVehicleDescription.EstimatedValueObj.EiId = 63817;
            MCVehicleDescription.FmmcmakeObj = new FMMCMake();
            MCVehicleDescription.FmmcmakeObj.EiId = 63823;
            MCVehicleDescription.FmmcyearObj = new FMMCYear();
            MCVehicleDescription.FmmcyearObj.EiId = 63825;
            MCVehicleDescription.FmmctypeObj = new FMMCType();
            MCVehicleDescription.FmmctypeObj.FmFamilyList = FamilyLists;
            MCVehicleDescription.FmmctypeObj.EiId = 63835;
            MCVehicleDescription.FmmcscdObj = new FMMCSelectCorDetails();
            MCVehicleDescription.FmmcscdObj.EiId = 63837;
            #endregion
            #region AdditionDetails
            MCVehicleDescription.KeptnightObj = new MCADKeptAtNight();
            MCVehicleDescription.KeptnightObj.EiId = 63851;
            MCVehicleDescription.AdaddressObj = new MCADAddress();
            MCVehicleDescription.AdaddressObj.AddressList = AddressList;
            MCVehicleDescription.AdaddressObj.EiId = 0;
            MCVehicleDescription.VregisterObj = new MCADVehicleRegistered();
            MCVehicleDescription.VregisterObj.EiId = 63855;
            MCVehicleDescription.RnumberObj = new MCADRegistrationNumber();
            MCVehicleDescription.RnumberObj.EiId = 63857;
            MCVehicleDescription.VnumberObj = new MCADVinNumber();
            MCVehicleDescription.VnumberObj.VnumberList = VNList;
            MCVehicleDescription.VnumberObj.EiId = 63859;
            MCVehicleDescription.EnumberObj = new MCADEngineNumber();
            MCVehicleDescription.EnumberObj.EnumberList = ENList;
            MCVehicleDescription.EnumberObj.EiId = 63861;
            MCVehicleDescription.VmodifiedObj = new MCADVehicleModified();
            MCVehicleDescription.VmodifiedObj.EiId = 63863;
            MCVehicleDescription.DmodifiedObj = new MCADdescribeModified();
            MCVehicleDescription.DmodifiedObj.EiId = 63865;
            MCVehicleDescription.SFinstalledObj = new MCADSecurityFeaturesInstalled();
            MCVehicleDescription.SFinstalledObj.EiId = 63867;
            MCVehicleDescription.VusedObj = new MCADVehicleUsed();
            MCVehicleDescription.VusedObj.EiId = 63879;
            MCVehicleDescription.CcapacityObj = new MCADCarryingCapacity();
            MCVehicleDescription.CcapacityObj.EiId = 63887;
            #endregion
            #region Driver
            MCVehicleDescription.DrivernameObj = new DriverName();
            MCVehicleDescription.DrivernameObj.EiId = 63901;
            MCVehicleDescription.DriverageObj = new DriverAge();
            MCVehicleDescription.DriverageObj.EiId = 63901;
            MCVehicleDescription.DrivergenderObj = new DriverGender();
            MCVehicleDescription.DrivergenderObj.GenderList = DriversGendarList;
            MCVehicleDescription.DrivergenderObj.EiId = 63901;
            MCVehicleDescription.DriveramicObj = new DriverAmic();
            MCVehicleDescription.DriveramicObj.EiId = 63901;
            MCVehicleDescription.UsevehicleObj = new UseOfVehicle();
            MCVehicleDescription.UsevehicleObj.EiId = 63903;
            #endregion
            #region Cover Details
            MCVehicleDescription.CoveroptionObj = new CoverOptionCD();
            MCVehicleDescription.CoveroptionObj.EiId = 63919;
            MCVehicleDescription.CovertypeObj = new CoverTypeCD();
            MCVehicleDescription.CovertypeObj.EiId = 63921;
            MCVehicleDescription.MaxMarvalObj = new MaximumMarketValue();
            MCVehicleDescription.MaxMarvalObj.EiId = 63925;
            MCVehicleDescription.CaravanannexObj = new CaravanAnnex();
            MCVehicleDescription.CaravanannexObj.EiId = 63931;
            MCVehicleDescription.UnspecifieditemsObj = new UnspecifiedItems();
            MCVehicleDescription.UnspecifieditemsObj.EiId = 63933;
            MCVehicleDescription.AccessoriesObj = new NonStandardAccessories();
            MCVehicleDescription.AccessoriesObj.EiId = 63937;
            MCVehicleDescription.DescriptionObj = new AccessoryDescriptionCD();
            MCVehicleDescription.DescriptionObj.DescriptionList = descriptionList;
            MCVehicleDescription.DescriptionObj.EiId = 63951;
            MCVehicleDescription.SumnsuredObj = new SumInsuredCD();
            MCVehicleDescription.SumnsuredObj.EiId = 63953;
            MCVehicleDescription.LimitindemnityObj = new LimitOfIndemnityDC();
            MCVehicleDescription.LimitindemnityObj.EiId = 63965;
            MCVehicleDescription.RatingObj = new RatingDC();
            MCVehicleDescription.RatingObj.EiId = 63977;
            MCVehicleDescription.NoclaimbonusObj = new NoClaimBonus();
            MCVehicleDescription.NoclaimbonusObj.EiId = 63979;
            #endregion
            #region Optional Extra Excess
            MCVehicleDescription.CaroptionObj = new HireCarOption();
            MCVehicleDescription.CaroptionObj.EiId = 64003;
            MCVehicleDescription.ExcessObj = new BasicExcess();
            MCVehicleDescription.ExcessObj.ExcessList = BasicExcessList;
            MCVehicleDescription.ExcessObj.EiId = 64009;
            #endregion
            #region Interested Parties
            MCVehicleDescription.MCPartynameObj = new MCInterestedPartyName();
            MCVehicleDescription.MCPartynameObj.EiId = 64025;
            MCVehicleDescription.MCPartyLocationObj = new MCInterestedPartyLocation();
            MCVehicleDescription.MCPartyLocationObj.EiId = 64027;
            #endregion

            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            string policyid = null;
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
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int unid = 0;
            int profileid = 0;
            int Fprofileid = 0;
            if (Session["unId"] != null && Session["ProfileId"] != null)
            {
                unid = Convert.ToInt32(Session["unId"]);
                profileid = Convert.ToInt32(Session["profileId"]);
            }
            if (Session["FProfileId"] != null)
            {
                Fprofileid = Convert.ToInt32(Session["FprofileId"]);
            }
            if (Session["Policyinclustions"] != null)
            {
                MCVehicleDescription.PolicyInclusions = Policyincllist;
            }
            if (PcId != null && PcId.HasValue && PcId > 0)
            {
                var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
                MCVehicleDescription.PolicyInclusion = new List<usp_GetUnit_Result>();
                if (PcId != null && PcId.HasValue && PcId > 0)
                {
                    MCVehicleDescription.PolicyInclusion = policyinclusions;
                }
                MCVehicleDescription.PolicyInclusions = new List<SessionModel>();
                if (PcId != null && PcId > 0)
                {
                    policyid = PcId.ToString();
                    MCVehicleDescription.PolicyId = policyid;
                }
                bool policyinclusion = policyinclusions.Exists(p => p.Name == "Motor");
                if (policyinclusion == true && PcId != null && PcId.HasValue)
                {

                    int sectionId = policyinclusions.Where(p => p.Name == "Motor" && p.UnitNumber == unid).Select(p => p.UnId).FirstOrDefault();
                    int? profileunid = policyinclusions.Where(p => p.Name == "Motor" && p.ProfileUnId == profileid).Select(p => p.ProfileUnId).FirstOrDefault();
                    HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                    var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    }
                }
            }
            else
            {
                if (PcId == null && Session["unId"] == null && Session["profileId"] == null)
                {
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Motor&SectionUnId=&ProfileUnId=");
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails != null && unitdetails.SectionData != null)
                        {
                            Session["unId"] = unitdetails.SectionData.UnId;
                          
                            Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                            if (Policyincllist != null && Policyincllist.Exists(p => p.name == "Motor"))
                            {
                                var policyhomelist = Policyincllist.FindAll(p => p.name == "Motor").ToList();
                                if (policyhomelist != null && policyhomelist.Count() > 0)
                                {
                                    Policyincllist.FindAll(p => p.name == "Motor").Where(p => p.UnitId == null).First().UnitId = unitdetails.SectionData.UnId;

                                    Policyincllist.FindAll(p => p.name == "Motor").Where(p => p.ProfileId == null).First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                                else
                                {
                                    Policyincllist.FindAll(p => p.name == "Motor").First().UnitId = unitdetails.SectionData.UnId;

                                    Policyincllist.FindAll(p => p.name == "Motor").First().ProfileId = unitdetails.SectionData.ProfileUnId;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (PcId == null && Session["unId"] != null && (Session["profileId"] != null || (profileid != null && profileid < 0)))
                    {                       
                        HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                        var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                        if (EmpResponse != null)
                        {
                            unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                            if (unitdetails != null && unitdetails.SectionData != null)
                            {
                                Session["unId"] = unitdetails.SectionData.UnId;
                                Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                            }
                        }
                    }
                }
            }
            if (unitdetails != null)
            {
                if (unitdetails.ProfileData != null)
                {
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.AccessoriesObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.AccessoriesObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.AccessoriesObj.Accessories = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.AdaddressObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.AdaddressObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.AdaddressObj.Address = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.CaravanannexObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.CaravanannexObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.CaravanannexObj.Annex = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.CaroptionObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.CaroptionObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.CaroptionObj.Caroption = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.CcapacityObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.CcapacityObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.CcapacityObj.Ccapacity = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.CoveroptionObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.CoveroptionObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.CoveroptionObj.Coveroption = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.DescriptionObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.DescriptionObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.DescriptionObj.Description = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.DmodifiedObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.DmodifiedObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.DmodifiedObj.Dmodified = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.DriverageObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.DriverageObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.DriverageObj.Age = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.DriveramicObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.DriveramicObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.DriveramicObj.Amic = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.DrivergenderObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.DrivergenderObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.DrivergenderObj.Gender = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.DrivernameObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.DrivernameObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.DrivernameObj.Name = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.EnumberObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.EnumberObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.EnumberObj.Enumber = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.EstimatedValueObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.EstimatedValueObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.EstimatedValueObj.EstimatedValue = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.ExcessObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.ExcessObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.ExcessObj.Excess = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.FmmcmakeObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.FmmcmakeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.FmmcmakeObj.FmMake = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.FmmcscdObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.FmmcscdObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.FmmcscdObj.FmScd = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.FmmctypeObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.FmmctypeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.FmmctypeObj.FmFamily = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.FmmcyearObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.FmmcyearObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.FmmcyearObj.FmYear = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.KeptnightObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.KeptnightObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.KeptnightObj.Keptnight = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.LimitindemnityObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.LimitindemnityObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.LimitindemnityObj.Indemnity = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.LscategoryObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.LscategoryObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.LscategoryObj.Category = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.MaxMarvalObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.MaxMarvalObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.MaxMarvalObj.Marketvalue = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.MCfamilyObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.MCfamilyObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.MCfamilyObj.Family = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.McmakeObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.McmakeObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.McmakeObj.Make = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.MCPartyLocationObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.MCPartyLocationObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.MCPartyLocationObj.Location = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.MCPartynameObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.MCPartynameObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.MCPartynameObj.Name = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.MCscdObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.MCscdObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.MCscdObj.Scd = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.McyearObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.McyearObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.McyearObj.Year = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.NoclaimbonusObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.NoclaimbonusObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.NoclaimbonusObj.Bonus = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.OwnersmanualObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.OwnersmanualObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.OwnersmanualObj.Ownersmanual = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.RatingObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.RatingObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.RatingObj.Rating = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.RnumberObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.RnumberObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.RnumberObj.Rnumber = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.SFinstalledObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.SFinstalledObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.SFinstalledObj.Installed = Convert.ToBoolean(val);
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.SumnsuredObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.SumnsuredObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.SumnsuredObj.Suminsured = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.UnspecifieditemsObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.UnspecifieditemsObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.UnspecifieditemsObj.Item = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.UsevehicleObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.UsevehicleObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.UsevehicleObj.Usevehicle = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.VmodifiedObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.VmodifiedObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.VmodifiedObj.Vmodified = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.VnumberObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.VnumberObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.VnumberObj.Vnumber = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.VregisterObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.VregisterObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.VregisterObj.Register = val;
                    }
                    if (unitdetails.ProfileData.ValueData.Exists(p => p.Element.ElId == MCVehicleDescription.VusedObj.EiId))
                    {
                        string val = unitdetails.ProfileData.ValueData.Where(p => p.Element.ElId == MCVehicleDescription.VusedObj.EiId).Select(p => p.Value).FirstOrDefault();
                        MCVehicleDescription.VusedObj.Vused =Convert.ToBoolean(val);
                    }
                }
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
            return RedirectToAction("DisclosureDetails", "Disclosure", new { cid = cid, PcId = MCVehicleDescription.PcId });
        }
        [HttpGet]
        public ActionResult AdditionalDetails(int? cid)
        {
            NewPolicyDetailsClass MCAdditionalDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> AddressList = new List<SelectListItem>();
            AddressList = MCAdditionalDetailsmodel.MCADAddress();
            List<SelectListItem> VNList = new List<SelectListItem>();
            VNList = MCAdditionalDetailsmodel.MCADVinNumber();
            List<SelectListItem> ENList = new List<SelectListItem>();
            ENList = MCAdditionalDetailsmodel.MCADEngineNumber();

            MCAdditionalDetails MCAdditionalDetails = new MCAdditionalDetails();
            MCAdditionalDetails.KeptnightObj = new MCADKeptAtNight();
            MCAdditionalDetails.KeptnightObj.EiId = 63851;
            MCAdditionalDetails.AdaddressObj = new MCADAddress();
            MCAdditionalDetails.AdaddressObj.AddressList = AddressList;
            MCAdditionalDetails.AdaddressObj.EiId = 0;
            MCAdditionalDetails.VregisterObj = new MCADVehicleRegistered();
            MCAdditionalDetails.VregisterObj.EiId = 63855;
            MCAdditionalDetails.RnumberObj = new MCADRegistrationNumber();
            MCAdditionalDetails.RnumberObj.EiId = 63857;
            MCAdditionalDetails.VnumberObj = new MCADVinNumber();
            MCAdditionalDetails.VnumberObj.VnumberList = VNList;
            MCAdditionalDetails.VnumberObj.EiId = 63859;
            MCAdditionalDetails.EnumberObj = new MCADEngineNumber();
            MCAdditionalDetails.EnumberObj.EnumberList = ENList;
            MCAdditionalDetails.EnumberObj.EiId = 63861;
            MCAdditionalDetails.VmodifiedObj = new MCADVehicleModified();
            MCAdditionalDetails.VmodifiedObj.EiId = 63863;
            MCAdditionalDetails.DmodifiedObj = new MCADdescribeModified();
            MCAdditionalDetails.DmodifiedObj.EiId = 63865;
            MCAdditionalDetails.SFinstalledObj = new MCADSecurityFeaturesInstalled();
            MCAdditionalDetails.SFinstalledObj.EiId = 63867;
            MCAdditionalDetails.VusedObj = new MCADVehicleUsed();
            MCAdditionalDetails.VusedObj.EiId = 63879;
            MCAdditionalDetails.CcapacityObj = new MCADCarryingCapacity();
            MCAdditionalDetails.CcapacityObj.EiId = 63887;
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCAdditionalDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCAdditionalDetails.CustomerId;
            }
            if (Session["completionTrackFMC"] != null)
            {
                Session["completionTrackFMC"] = Session["completionTrackFMC"];
                MCAdditionalDetails.completionTrackMC = Session["completionTrackFMC"].ToString();
            }
            else
            {
                Session["completionTrackFMC"] = "0-0-0-0-0-0"; ;
                MCAdditionalDetails.completionTrackMC = Session["completionTrackFMC"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
           
            return View(MCAdditionalDetails);
        }
        [HttpPost]
        public ActionResult AdditionalDetails(int? cid, MCAdditionalDetails MCAdditionalDetails)
        {
            NewPolicyDetailsClass MCAdditionalDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> AddressList = new List<SelectListItem>();
            AddressList = MCAdditionalDetailsmodel.MCADAddress();
            List<SelectListItem> VNList = new List<SelectListItem>();
            VNList = MCAdditionalDetailsmodel.MCADVinNumber();
            List<SelectListItem> ENList = new List<SelectListItem>();
            ENList = MCAdditionalDetailsmodel.MCADEngineNumber();

            MCAdditionalDetails.AdaddressObj.AddressList = AddressList;
            MCAdditionalDetails.VnumberObj.VnumberList = VNList;
            MCAdditionalDetails.EnumberObj.EnumberList = ENList;

            if (cid != null)
            {
                ViewBag.cid = cid;
                MCAdditionalDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCAdditionalDetails.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
          
            return View(MCAdditionalDetails);
        }
        [HttpGet]
        public ActionResult Drivers(int? cid)
        {
            List<SelectListItem> DriversGendarList = new List<SelectListItem>();
            DriversGendarList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            DriversGendarList.Add(new SelectListItem { Value = "1", Text = "Male" });
            DriversGendarList.Add(new SelectListItem { Value = "2", Text = "Female" });
            MCDrivers MCDrivers = new MCDrivers();
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCDrivers.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCDrivers.CustomerId;
            }
            MCDrivers.DrivernameObj = new DriverName();
            MCDrivers.DrivernameObj.EiId = 63901;
            MCDrivers.DriverageObj = new DriverAge();
            MCDrivers.DriverageObj.EiId = 63901;
            MCDrivers.DrivergenderObj = new DriverGender();
            MCDrivers.DrivergenderObj.GenderList = DriversGendarList;
            MCDrivers.DrivergenderObj.EiId = 63901;
            MCDrivers.DriveramicObj = new DriverAmic();
            MCDrivers.DriveramicObj.EiId = 63901;
            MCDrivers.UsevehicleObj = new UseOfVehicle();
            MCDrivers.UsevehicleObj.EiId = 63901;
            if (Session["completionTrackFMC"] != null)
            {
                Session["completionTrackFMC"] = Session["completionTrackFMC"];
                MCDrivers.completionTrackMC = Session["completionTrackFMC"].ToString();
            }
            else
            {
                Session["completionTrackFMC"] = "0-0-0-0-0-0"; ;
                MCDrivers.completionTrackMC = Session["completionTrackFMC"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
           return View(MCDrivers);
        }
        [HttpPost]
        public ActionResult Drivers(int? cid, MCDrivers MCDrivers)
        {
            List<SelectListItem> DriversGendarList = new List<SelectListItem>();
            DriversGendarList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            DriversGendarList.Add(new SelectListItem { Value = "1", Text = "Male" });
            DriversGendarList.Add(new SelectListItem { Value = "2", Text = "Female" });
            MCDrivers.DrivergenderObj.GenderList = DriversGendarList;
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCDrivers.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCDrivers.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
          
            return View();
        }
        [HttpGet]
        public ActionResult CoverDetails(int? cid)
        {
            NewPolicyDetailsClass CoverDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> descriptionList = new List<SelectListItem>();
            descriptionList = CoverDetailsmodel.MCCDDescription();
            MCCoverDetails MCCoverDetails = new MCCoverDetails();
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCCoverDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCCoverDetails.CustomerId;
            }
            MCCoverDetails.CoveroptionObj = new CoverOptionCD();
            MCCoverDetails.CoveroptionObj.EiId = 63919;
            MCCoverDetails.CovertypeObj = new CoverTypeCD();
            MCCoverDetails.CovertypeObj.EiId = 63921;
            MCCoverDetails.MaxMarvalObj = new MaximumMarketValue();
            MCCoverDetails.MaxMarvalObj.EiId = 63925;
            MCCoverDetails.CaravanannexObj = new CaravanAnnex();
            MCCoverDetails.CaravanannexObj.EiId = 63931;
            MCCoverDetails.UnspecifieditemsObj = new UnspecifiedItems();
            MCCoverDetails.UnspecifieditemsObj.EiId = 63933;
            MCCoverDetails.AccessoriesObj = new NonStandardAccessories();
            MCCoverDetails.AccessoriesObj.EiId = 63937;
            MCCoverDetails.DescriptionObj = new AccessoryDescriptionCD();
            MCCoverDetails.DescriptionObj.DescriptionList = descriptionList;
            MCCoverDetails.DescriptionObj.EiId = 63951;
            MCCoverDetails.SumnsuredObj = new SumInsuredCD();
            MCCoverDetails.SumnsuredObj.EiId = 63953;
            MCCoverDetails.LimitindemnityObj = new LimitOfIndemnityDC();
            MCCoverDetails.LimitindemnityObj.EiId = 63965;
            MCCoverDetails.RatingObj = new RatingDC();
            MCCoverDetails.RatingObj.EiId = 63977;
            MCCoverDetails.NoclaimbonusObj = new NoClaimBonus();
            MCCoverDetails.NoclaimbonusObj.EiId = 63979;
            if (Session["completionTrackFMC"] != null)
            {
                Session["completionTrackFMC"] = Session["completionTrackFMC"];
                MCCoverDetails.completionTrackMC = Session["completionTrackFMC"].ToString();
            }
            else
            {
                Session["completionTrackFMC"] = "0-0-0-0-0-0"; ;
                MCCoverDetails.completionTrackMC = Session["completionTrackFMC"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Motor), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == MCCoverDetails.CoveroptionObj.EiId))
                {
                    MCCoverDetails.CoveroptionObj.Coveroption = details.Where(q => q.QuestionId == MCCoverDetails.CoveroptionObj.EiId).FirstOrDefault().Answer;
                }
                if (details.Exists(q => q.QuestionId == MCCoverDetails.CovertypeObj.EiId))
                {
                    MCCoverDetails.CovertypeObj.Covertype = details.Where(q => q.QuestionId == MCCoverDetails.CovertypeObj.EiId).FirstOrDefault().Answer;
                }
                if (details.Exists(q => q.QuestionId == MCCoverDetails.MaxMarvalObj.EiId))
                {
                    MCCoverDetails.MaxMarvalObj.Marketvalue = details.Where(q => q.QuestionId == MCCoverDetails.MaxMarvalObj.EiId).FirstOrDefault().Answer;
                }
                if (details.Exists(q => q.QuestionId == MCCoverDetails.CaravanannexObj.EiId))
                {
                    MCCoverDetails.CaravanannexObj.Annex = details.Where(q => q.QuestionId == MCCoverDetails.CaravanannexObj.EiId).FirstOrDefault().Answer;
                }
                if (details.Exists(q => q.QuestionId == MCCoverDetails.DescriptionObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MCCoverDetails.DescriptionObj.EiId).FirstOrDefault();
                    MCCoverDetails.DescriptionObj.Description = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }

            }
            return View(MCCoverDetails);
        }
        [HttpPost]
        public ActionResult CoverDetails(int? cid, MCCoverDetails MCCoverDetails)
        {
            NewPolicyDetailsClass CoverDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> descriptionList = new List<SelectListItem>();
            descriptionList = CoverDetailsmodel.MCCDDescription();
            MCCoverDetails.DescriptionObj.DescriptionList = descriptionList;
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCCoverDetails.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCCoverDetails.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (MCCoverDetails.CoveroptionObj != null && MCCoverDetails.CoveroptionObj.EiId > 0 && MCCoverDetails.CoveroptionObj.Coveroption != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.CoveroptionObj.EiId, MCCoverDetails.CoveroptionObj.Coveroption.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (MCCoverDetails.CovertypeObj != null && MCCoverDetails.CovertypeObj.EiId > 0 && MCCoverDetails.CovertypeObj.Covertype != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.CovertypeObj.EiId, MCCoverDetails.CovertypeObj.Covertype.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (MCCoverDetails.MaxMarvalObj != null && MCCoverDetails.MaxMarvalObj.EiId > 0 && MCCoverDetails.MaxMarvalObj.Marketvalue != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.MaxMarvalObj.EiId, MCCoverDetails.MaxMarvalObj.Marketvalue.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (MCCoverDetails.CaravanannexObj != null && MCCoverDetails.CaravanannexObj.EiId > 0 && MCCoverDetails.CaravanannexObj.Annex != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.CaravanannexObj.EiId, MCCoverDetails.CaravanannexObj.Annex.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (MCCoverDetails.UnspecifieditemsObj != null && MCCoverDetails.UnspecifieditemsObj.EiId > 0 && MCCoverDetails.UnspecifieditemsObj.Item != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.UnspecifieditemsObj.EiId, MCCoverDetails.UnspecifieditemsObj.Item.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (MCCoverDetails.AccessoriesObj != null && MCCoverDetails.AccessoriesObj.EiId > 0 && MCCoverDetails.AccessoriesObj.Accessories != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.AccessoriesObj.EiId, MCCoverDetails.AccessoriesObj.Accessories.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (MCCoverDetails.DescriptionObj != null && MCCoverDetails.DescriptionObj.EiId > 0 && MCCoverDetails.DescriptionObj.Description != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.DescriptionObj.EiId, MCCoverDetails.DescriptionObj.Description.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (MCCoverDetails.SumnsuredObj != null && MCCoverDetails.SumnsuredObj.EiId > 0 && MCCoverDetails.SumnsuredObj.Suminsured != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.SumnsuredObj.EiId, MCCoverDetails.SumnsuredObj.Suminsured.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (MCCoverDetails.LimitindemnityObj != null && MCCoverDetails.LimitindemnityObj.EiId > 0 && MCCoverDetails.LimitindemnityObj.Indemnity != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.LimitindemnityObj.EiId, MCCoverDetails.LimitindemnityObj.Indemnity.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (MCCoverDetails.RatingObj != null && MCCoverDetails.RatingObj.EiId > 0 && MCCoverDetails.RatingObj.Rating != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.RatingObj.EiId, MCCoverDetails.RatingObj.Rating.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (MCCoverDetails.NoclaimbonusObj != null && MCCoverDetails.NoclaimbonusObj.EiId > 0 && MCCoverDetails.NoclaimbonusObj.Bonus != null)
                {
                    db.IT_InsertCustomerQnsData(MCCoverDetails.CustomerId, Convert.ToInt32(RLSSection.Motor), MCCoverDetails.NoclaimbonusObj.EiId, MCCoverDetails.NoclaimbonusObj.Bonus.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Session["completionTrackFMC"] != null)
                {
                    Session["completionTrackFMC"] = Session["completionTrackFMC"];
                    MCCoverDetails.completionTrackMC = Session["completionTrackFMC"].ToString();
                    if (MCCoverDetails.completionTrackMC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = MCCoverDetails.completionTrackMC.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 6)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackFMC"] = Completionstring;
                        MCCoverDetails.completionTrackMC = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackFMC"] = "0-0-0-1-0-0"; ;
                    MCCoverDetails.completionTrackMC = Session["completionTrackFMC"].ToString();
                }
                return RedirectToAction("OptionalExtrasExcesses", new { cid = MCCoverDetails.CustomerId });
            }
            return View(MCCoverDetails);
        }
        [HttpGet]
        public ActionResult OptionalExtrasExcesses(int? cid)
        {
            List<SelectListItem> BasicExcessList = new List<SelectListItem>();
            BasicExcessList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            BasicExcessList.Add(new SelectListItem { Value = "60953", Text = "Under 21 Excess" });
            BasicExcessList.Add(new SelectListItem { Value = "60955", Text = "Under 25 Excess" });
            MCOptionalExtrasExcesses MCOptionalExtrasExcesses = new MCOptionalExtrasExcesses();
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCOptionalExtrasExcesses.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCOptionalExtrasExcesses.CustomerId;
            }
            MCOptionalExtrasExcesses.CaroptionObj = new HireCarOption();
            MCOptionalExtrasExcesses.CaroptionObj.EiId = 64003;
            MCOptionalExtrasExcesses.ExcessObj = new BasicExcess();
            MCOptionalExtrasExcesses.ExcessObj.ExcessList = BasicExcessList;
            MCOptionalExtrasExcesses.ExcessObj.EiId = 64009;
            if (Session["completionTrackFMC"] != null)
            {
                Session["completionTrackFMC"] = Session["completionTrackFMC"];
                MCOptionalExtrasExcesses.completionTrackMC = Session["completionTrackFMC"].ToString();
            }
            else
            {
                Session["completionTrackFMC"] = "0-0-0-0-0-0"; ;
                MCOptionalExtrasExcesses.completionTrackMC = Session["completionTrackFMC"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Motor), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == MCOptionalExtrasExcesses.CaroptionObj.EiId))
                {
                    MCOptionalExtrasExcesses.CaroptionObj.Caroption = details.Where(q => q.QuestionId == MCOptionalExtrasExcesses.CaroptionObj.EiId).FirstOrDefault().Answer;
                }
                if (details.Exists(q => q.QuestionId == MCOptionalExtrasExcesses.ExcessObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MCOptionalExtrasExcesses.ExcessObj.EiId).FirstOrDefault();
                    MCOptionalExtrasExcesses.ExcessObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
            }
            return View(MCOptionalExtrasExcesses);
        }
        [HttpPost]
        public ActionResult OptionalExtrasExcesses(int? cid, MCOptionalExtrasExcesses MCOptionalExtrasExcesses)
        {
            List<SelectListItem> BasicExcessList = new List<SelectListItem>();
            //if (MCOptionalExtrasExcesses!=null && MCOptionalExtrasExcesses.ExcessObj != null)
            //{
            //    MCOptionalExtrasExcesses.ExcessObj.ExcessList = new List<SelectListItem>();
            //    BasicExcessList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            //    BasicExcessList.Add(new SelectListItem { Value = "60953", Text = "Under 21 Excess" });
            //    BasicExcessList.Add(new SelectListItem { Value = "60955", Text = "Under 25 Excess" });
            //    MCOptionalExtrasExcesses.ExcessObj.ExcessList = BasicExcessList;
            //}
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCOptionalExtrasExcesses.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCOptionalExtrasExcesses.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (MCOptionalExtrasExcesses.CaroptionObj != null && MCOptionalExtrasExcesses.CaroptionObj.EiId > 0 && MCOptionalExtrasExcesses.CaroptionObj.Caroption != null)
                {
                    db.IT_InsertCustomerQnsData(MCOptionalExtrasExcesses.CustomerId, Convert.ToInt32(RLSSection.Motor), MCOptionalExtrasExcesses.CaroptionObj.EiId, MCOptionalExtrasExcesses.CaroptionObj.Caroption.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (MCOptionalExtrasExcesses.ExcessObj != null && MCOptionalExtrasExcesses.ExcessObj.EiId > 0 && MCOptionalExtrasExcesses.ExcessObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(MCOptionalExtrasExcesses.CustomerId, Convert.ToInt32(RLSSection.Motor), MCOptionalExtrasExcesses.ExcessObj.EiId, MCOptionalExtrasExcesses.ExcessObj.Excess.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (Session["completionTrackFMC"] != null)
                {
                    Session["completionTrackFMC"] = Session["completionTrackFMC"];
                    MCOptionalExtrasExcesses.completionTrackMC = Session["completionTrackFMC"].ToString();
                    if (MCOptionalExtrasExcesses.completionTrackMC != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = MCOptionalExtrasExcesses.completionTrackMC.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 8)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackFMC"] = Completionstring;
                        MCOptionalExtrasExcesses.completionTrackMC = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackFMC"] = "0-0-0-0-1-0"; ;
                    MCOptionalExtrasExcesses.completionTrackMC = Session["completionTrackFMC"].ToString();
                }
                return RedirectToAction("InterestedParties", new { cid = MCOptionalExtrasExcesses.CustomerId });
            }
            return View(MCOptionalExtrasExcesses);
        }
        [HttpGet]
        public ActionResult InterestedParties(int? cid)
        {
            MCInterestedParties MCInterestedParties = new MCInterestedParties();
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCInterestedParties.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCInterestedParties.CustomerId;
            }
            MCInterestedParties.MCPartynameObj = new MCInterestedPartyName();
            MCInterestedParties.MCPartynameObj.EiId = 64025;
            MCInterestedParties.MCPartyLocationObj = new MCInterestedPartyLocation();
            MCInterestedParties.MCPartyLocationObj.EiId = 64027;
            if (Session["completionTrackFMC"] != null)
            {
                Session["completionTrackFMC"] = Session["completionTrackFMC"];
                MCInterestedParties.completionTrackMC = Session["completionTrackFMC"].ToString();
            }
            else
            {
                Session["completionTrackFMC"] = "0-0-0-0-0-0"; ;
                MCInterestedParties.completionTrackMC = Session["completionTrackFMC"].ToString();
            }
            var db = new MasterDataEntities();
            string policyid = null;
            var details = db.IT_GetCustomerQnsDetails(cid, Convert.ToInt32(RLSSection.Motor), Convert.ToInt32(PolicyType.RLS), policyid).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == MCInterestedParties.MCPartynameObj.EiId))
                {
                    MCInterestedParties.MCPartynameObj.Name = details.Where(q => q.QuestionId == MCInterestedParties.MCPartyLocationObj.EiId).FirstOrDefault().Answer;
                }
                if (details.Exists(q => q.QuestionId == MCInterestedParties.MCPartyLocationObj.EiId))
                {
                    MCInterestedParties.MCPartyLocationObj.Location = details.Where(q => q.QuestionId == MCInterestedParties.MCPartyLocationObj.EiId).FirstOrDefault().Answer;
                }
            }
            return View(MCInterestedParties);
        }
        [HttpPost]
        public ActionResult InterestedParties(int? cid, MCInterestedParties MCInterestedParties)
        {
            if (cid != null)
            {
                ViewBag.cid = cid;
                MCInterestedParties.CustomerId = cid.Value;
            }
            else
            {
                ViewBag.cid = MCInterestedParties.CustomerId;
            }
            var db = new MasterDataEntities();
            string policyid = null;
            if (cid.HasValue && cid > 0)
            {
                if (MCInterestedParties.MCPartynameObj != null && MCInterestedParties.MCPartynameObj.EiId > 0 && MCInterestedParties.MCPartynameObj.Name != null)
                {
                    db.IT_InsertCustomerQnsData(MCInterestedParties.CustomerId, Convert.ToInt32(RLSSection.Motor), MCInterestedParties.MCPartynameObj.EiId, MCInterestedParties.MCPartynameObj.Name.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                if (MCInterestedParties.MCPartyLocationObj != null && MCInterestedParties.MCPartyLocationObj.EiId > 0 && MCInterestedParties.MCPartyLocationObj.Location != null)
                {
                    db.IT_InsertCustomerQnsData(MCInterestedParties.CustomerId, Convert.ToInt32(RLSSection.Motor), MCInterestedParties.MCPartyLocationObj.EiId, MCInterestedParties.MCPartyLocationObj.Location.ToString(), Convert.ToInt32(PolicyType.RLS), policyid);
                }
                //if (Session["completionTrackFMC"] != null)
                //{
                //    Session["completionTrackFMC"] = Session["completionTrackFMC"];
                //    MCInterestedParties.completionTrackMC = Session["completionTrackFMC"].ToString();
                //    if (MCInterestedParties.completionTrackMC != null)
                //    {
                //        string Completionstring = string.Empty;
                //        char[] arr = MCInterestedParties.completionTrackMC.ToCharArray();

                //        for (int i = 0; i < arr.Length; i++)
                //        {
                //            char a = arr[i];
                //            if (i == 10)
                //            {
                //                a = '1';
                //            }
                //            Completionstring = Completionstring + a;
                //        }
                //        Session["completionTrackFMC"] = Completionstring;
                //        MCInterestedParties.completionTrackMC = Completionstring;
                //    }
                //}
                //else
                //{
                //    Session["completionTrackFMC"] = "0-0-0-0-0-1"; ;
                //    MCInterestedParties.completionTrackMC = Session["completionTrackFMC"].ToString();
                //}
                //   return RedirectToAction("PetsCover", "Pets", new { cid = cid });
                return RedirectToAction("BindCover", "Customer", new { cid = cid });
                // return RedirectToAction("VehicleDescription", new { cid = MCInterestedParties.CustomerId });
            }
            return View(MCInterestedParties);
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