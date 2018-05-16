using InsureThatAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;

namespace InsureThatAPI.Controllers
{
    public class PolicyDetailsController : Controller
    {
        [HttpGet]
        public ActionResult ActionName(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
                model.PcId = PcId.ToString();
            }
            var db = new MasterDataEntities();
            if (Session["ApiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
                if (Session["cid"] != null)
                {
                    cid = Convert.ToInt32(Session["cid"]);
                }
                if (policyinclu != null && policyinclu.Count() > 0)
                {
                    if (Session["UnitId"] != null)
                    {
                        int? SessionunitId = Convert.ToInt32(Session["UnitId"]);
                        int unitid = 0;
                        string unitname = null;
                        int matched = 0;                        
                        for (int i = 0; i < policyinclu.Count(); i++)
                        {
                            if (unitid != null && unitid > 0 && unitname != null)
                            {
                                unitname = policyinclu[i].Name;
                                unitid = policyinclu[i].UnitId;
                                matched = 2;
                            }
                            if (policyinclu[i].UnitId == SessionunitId)
                            {
                                unitid = policyinclu[i].UnitId;
                                unitname = policyinclu[i].Name;
                                matched = 1;
                            }
                            if (unitid != null && unitid > 0 && unitname != null && matched == 2)
                            {
                                Session["UnitId"] = unitid;
                                Session["controller"] = "PolicyDetails";
                                if (unitname == "Home")
                                {
                                    Session["UnitId"] = unitid;
                                    return RedirectToAction("HomeBilding", "PolicyDetails", new { cid = cid, PcId = PcId });
                                    // return RedirectToAction("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = pcid });
                                }
                                if (unitname == "Home Buildings")
                                {
                                    Session["UnitId"] = unitid;
                                    return RedirectToAction("HomeBilding", "PolicyDetails", new { cid = cid, PcId = PcId });
                                    // return RedirectToAction("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = pcid });
                                }
                                else if (unitname == "Home Content" || unitname == "Home Contents")
                                {
                                    return RedirectToAction("HomeContents", "PolicyDetails", new { cid = cid, PcId = PcId });
                                    return RedirectToAction("HomeContent", "HomeContentValuable", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Valuables")
                                {
                                    return RedirectToAction("Valuables", "PolicyDetails", new { cid = cid, PcId = PcId });
                                    return RedirectToAction("Valuables", "HomeContentValuable", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Farm Property")
                                {
                                    return RedirectToAction("FarmProperty", "PolicyDetails", new { cid = cid, PcId = PcId });
                                    return RedirectToAction("FarmContents", "Farm", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Liability")
                                {
                                    return RedirectToAction("Liability", "PolicyDetails", new { cid = cid, PcId = PcId });
                                    return RedirectToAction("LiabilityCover", "Liabilities", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Pets")
                                {
                                    return RedirectToAction("Pets", "PolicyDetails", new { cid = cid, PcId = PcId });
                                    return RedirectToAction("PetsCover", "Pets", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Motor")
                                {
                                    return RedirectToAction("Motors", "PolicyDetails", new { cid = cid, PcId = PcId });
                                    return RedirectToAction("VehicleDescription", "MotorCover", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Boat")
                                {
                                    return RedirectToAction("Boat", "PolicyDetails", new { cid = cid, PcId = PcId });
                                    return RedirectToAction("BoatDetails", "Boat", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Travel")
                                {
                                    return RedirectToAction("Travels", "PolicyDetails", new { cid = cid, PcId = PcId });
                                    return RedirectToAction("TravelCover", "Travel", new { cid = cid, PcId = PcId });
                                }
                            }

                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
         
            return RedirectToAction("PremiumDetails", "Customer", new { cid = cid, PcId = PcId });
        }

        [HttpGet]
        public ActionResult ActionNameFarms(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
                model.PcId = PcId.ToString();
            }
            var db = new MasterDataEntities();
            if (Session["ApiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
                if (Session["cid"] != null)
                {
                    cid = Convert.ToInt32(Session["cid"]);
                }
                if (policyinclu != null && policyinclu.Count() > 0)
                {
                    if (Session["UnitId"] != null)
                    {
                        int? SessionunitId = Convert.ToInt32(Session["UnitId"]);
                        int unitid = 0;
                        string unitname = null;
                        int matched = 0;

                        for (int i = 0; i < policyinclu.Count(); i++)
                        {
                            if (unitid != null && unitid > 0 && unitname != null)
                            {
                                unitname = policyinclu[i].Name;
                                unitid = policyinclu[i].UnitId;
                                matched = 2;
                            }
                            if (policyinclu[i].UnitId == SessionunitId)
                            {
                                unitid = policyinclu[i].UnitId;
                                unitname = policyinclu[i].Name;
                                matched = 1;
                            }
                            if (unitid != null && unitid > 0 && unitname != null && matched == 2)
                            {
                                Session["UnitId"] = unitid;
                                Session["controller"] = "PolicyDetails";
                                if (unitname == "Fixed Farm Property")
                                {
                                    return RedirectToAction("FixedFarmProperty", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Mobile Farm Property")
                                {
                                    return RedirectToAction("MobileFarmProperty", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Farm Interruption")
                                {
                                    return RedirectToAction("FarmInterruption", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Farm Liability")
                                {
                                    return RedirectToAction("FarmLiability", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Burglary")
                                {
                                    return RedirectToAction("Burglary", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Electronics")
                                {
                                    return RedirectToAction("FarmElectronics", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Money")
                                {
                                    return RedirectToAction("FarmMoney", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Transit")
                                {
                                    return RedirectToAction("FarmTransit", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Valuables")
                                {
                                    return RedirectToAction("FarmValuables", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Livestock")
                                {
                                    return RedirectToAction("FarmLivestock", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Personal Liability")
                                {
                                    return RedirectToAction("PersonalLiabilit", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Home Buildings")
                                {
                                    return RedirectToAction("FarmHomeBilding", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Home Contents")
                                {
                                    return RedirectToAction("FarmHomeContents", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Machinery")
                                {
                                    return RedirectToAction("FarmMachinery", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                else if (unitname == "Motor")
                                {
                                    return RedirectToAction("FarmMotors", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                else return RedirectToAction("PremiumDetails", "Customer", new { cid = cid, PcId = PcId });
                            }
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            return RedirectToAction("PremiumDetails", "Customer", new { cid = cid, PcId = PcId });
        }
            
   
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> HomeBilding(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                Session["Actname"] = "HomeBilding";
                Session["controller"] = "PolicyDetails";
                if (cid.HasValue)
                {
                    Session["cid"] = cid.Value;
                }
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
                var units = policyinclu.Where(o => o.Name == "Home Buildings" && o.ProfileUnId == 1).FirstOrDefault();
                if (Session["UnitId"] != null)
                {
                    int sessionunitId = Convert.ToInt32(Session["UnitId"]);
                    units = policyinclu.Where(o => o.UnitId == sessionunitId).SingleOrDefault();
                    Session["profileId"] = units.ProfileUnId;
                }
                else
                {
                    units = policyinclu.Where(o => o.Name == "Home Buildings" && o.ProfileUnId == 1).FirstOrDefault();
                    Session["UnitId"] = units.UnitId;
                    Session["profileId"] = units.ProfileUnId;
                }
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    model.PolicyInclusion = policyinclu;
                    if(model.PolicyInclusion!=null && model.PolicyInclusion.Count()>0)
                    {
                        for(int j=0;j<model.PolicyInclusion.Count();j++)
                        {
                            //model.PolicyInclusion[j].Name=model.PolicyInclusion[j].Name+" ("+model.PolicyInclusion[j].Component[j].
                        }
                    }
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                        model.PolicyData.CoverPeriod = policydetails.CoverPeriod.Value;
                        model.PolicyData.CoverPeriodUnit = policydetails.CoverPeriodUnit;
                        model.PolicyData.TrId = policydetails.TrId;
                    }
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null && model.AddressData.Count()>0)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);

                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("Agentlogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
                model.PcId = PcId.ToString();
            }
            Session["PolicyNo"] = model.PolicyData.PolicyNumber;
            return View(model);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> HomeContents(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                Session["Actname"] = "HomeContents";
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).FirstOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
                if (policydetails != null && policyinclu.Exists(o => o.Name == "Home Contents"))
                {

                }
                else
                {
                    return RedirectToAction("Valuables", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
                var units = policyinclu.Where(o => o.Name == "Home Contents").FirstOrDefault();
                if (Session["UnitId"] != null)
                {
                    int sessionunitId = Convert.ToInt32(Session["UnitId"]);
                    units = policyinclu.Where(o => o.UnitId == sessionunitId).FirstOrDefault();
                }
                else
                {
                    units = policyinclu.Where(o => o.Name == "Home Contents").FirstOrDefault();
                    Session["UnitId"] = units.UnitId;
                }
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    model.PolicyInclusion = policyinclu;
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                        model.PolicyData.CoverPeriod = policydetails.CoverPeriod.Value;
                        model.PolicyData.CoverPeriodUnit = policydetails.CoverPeriodUnit;
                    }
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }

                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Valuables", "PolicyDetails", new { cid = cid, PcId = PcId });
                    }
                }
                else
                {
                    return RedirectToAction("Valuables", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                model.PcId = PcId.ToString();
                ViewBag.PcId = PcId;
            }
            return View(model);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Valuables(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                Session["Actname"] = "Valuables";
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).FirstOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
                if (policydetails != null && policyinclu.Exists(o => o.Name == "Valuables"))
                {

                }
                else
                {
                    return RedirectToAction("FarmProperty", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
                var units = policyinclu.Where(o => o.Name == "Valuables").FirstOrDefault();
                if (Session["UnitId"] != null)
                {
                    int sessionunitId = Convert.ToInt32(Session["UnitId"]);
                    units = policyinclu.Where(o => o.UnitId == sessionunitId).FirstOrDefault();
                }
                else
                {
                    units = policyinclu.Where(o => o.Name == "Valuables").FirstOrDefault();
                    Session["UnitId"] = units.UnitId;
                }
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if(model.Status== "Failure")
                    {
                        if (cid != null)
                        {
                            ViewBag.cid = cid;
                            model.CustomerId = cid.Value;
                        }
                        if (PcId != null)
                        {
                            model.PcId = PcId.ToString();
                            ViewBag.PcId = PcId;
                            model.PolicyInclusion = policyinclu;
                            model.PolicyInclusion = policyinclu;
                            if (policydetails != null)
                            {
                                model.PolicyData = new PolicyDetails();
                                model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                                model.PolicyData.InsuredName = policydetails.InsuredName;
                                model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                                model.PolicyData.PrId = policydetails.PrId;
                                model.PolicyData.PcId = policydetails.PcId;
                                model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                                model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                            }
                        }
                        return View(model);
                    }
                    model.PolicyInclusion = policyinclu;
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    ////else
                    ////{
                    ////    return RedirectToAction("FarmProperty", "PolicyDetails", new { cid = cid, PcId = PcId });
                    ////}
                }
                ////else
                ////{
                ////    return RedirectToAction("FarmProperty", "PolicyDetails", new { cid = cid, PcId = PcId });
                ////}
            }
            else
            {
                return RedirectToAction("Agentlogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                model.PcId = PcId.ToString();
                ViewBag.PcId = PcId;
            }
            return View(model);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> FarmProperty(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();

            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                Session["Actname"] = "FarmProperty";
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).FirstOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
                if (policydetails != null && policyinclu.Exists(o => o.Name == "Farm Property"))
                {

                }
                else
                {
                    return RedirectToAction("Travels", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
                var units = policyinclu.Where(o => o.Name == "Farm Property").FirstOrDefault();
                if (Session["UnitId"] != null)
                {
                    int sessionunitId = Convert.ToInt32(Session["UnitId"]);
                    units = policyinclu.Where(o => o.UnitId == sessionunitId).FirstOrDefault();
                }
                else
                {
                    units = policyinclu.Where(o => o.Name == "Farm Property").FirstOrDefault();
                    Session["UnitId"] = units.UnitId;
                }
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    model.PolicyInclusion = policyinclu;
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}

                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    ////else
                    ////{
                    ////    return RedirectToAction("Travels", "PolicyDetails", new { cid = cid, PcId = PcId });
                    ////}
                }
                ////else
                ////{
                ////    return RedirectToAction("Travels", "PolicyDetails", new { cid = cid, PcId = PcId });
                ////}
            }
            else
            {
                return RedirectToAction("Agentlogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                model.PcId = PcId.ToString();
                ViewBag.PcId = PcId;
            }
            return View(model);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Travels(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                Session["Actname"] = "Travels";
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).FirstOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
                model.PolicyInclusion = policyinclu;
                if (policydetails != null && policyinclu.Exists(o => o.Name == "Travel"))
                {

                }
                else
                {
                    return RedirectToAction("Liability", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
                var units = policyinclu.Where(o => o.Name == "Travel").FirstOrDefault();
                if (Session["UnitId"] != null)
                {
                    int sessionunitId = Convert.ToInt32(Session["UnitId"]);
                    units = policyinclu.Where(o => o.UnitId == sessionunitId).FirstOrDefault();
                }
                else
                {
                    units = policyinclu.Where(o => o.Name == "Travel").FirstOrDefault();
                    Session["UnitId"] = units.UnitId;
                }
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}

                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }

                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    ////else
                    ////{
                    ////    return RedirectToAction("Liability", "PolicyDetails", new { cid = cid, PcId = PcId });
                    ////}
                }
                ////else
                ////{
                ////    return RedirectToAction("Liability", "PolicyDetails", new { cid = cid, PcId = PcId });
                ////}
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                model.PcId = PcId.ToString();
                ViewBag.PcId = PcId;
            }
            return View(model);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Liability(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                Session["Actname"] = "Liability";
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).FirstOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
                var units = policyinclu.Where(o => o.Name == "Liability").FirstOrDefault();
                if (Session["UnitId"] != null)
                {
                    int sessionunitId = Convert.ToInt32(Session["UnitId"]);
                    units = policyinclu.Where(o => o.UnitId == sessionunitId).FirstOrDefault();
                }
                else
                {
                    units = policyinclu.Where(o => o.Name == "Liability" && o.ProfileUnId == 1).FirstOrDefault();
                    Session["UnitId"] = units.UnitId;
                }
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + units.UnId + "&ProfileUnId=0" );
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    model.PolicyInclusion = policyinclu;
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    ////else
                    ////{
                    ////    return RedirectToAction("Boat", "PolicyDetails", new { cid = cid, PcId = PcId });
                    ////}
                }
                ////else
                ////{
                ////    return RedirectToAction("Boat", "PolicyDetails", new { cid = cid, PcId = PcId });
                ////}
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                model.PcId = PcId.ToString();
                ViewBag.PcId = PcId;
            }
            return View(model);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Boat(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                Session["Actname"] = "Boat";
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).FirstOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
                if (policydetails != null && policyinclu.Exists(o => o.Name == "Boat"))
                {

                }
                else
                {
                    return RedirectToAction("Motors", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
                var units = policyinclu.Where(o => o.Name == "Boat").FirstOrDefault();
                if (Session["UnitId"] != null)
                {
                    int sessionunitId = Convert.ToInt32(Session["UnitId"]);
                    units = policyinclu.Where(o => o.UnitId == sessionunitId).FirstOrDefault();
                }
                else
                {
                    units = policyinclu.Where(o => o.Name == "Boat").FirstOrDefault();
                    Session["UnitId"] = units.UnitId;
                }
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    model.PolicyInclusion = policyinclu;
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Motors", "PolicyDetails", new { cid = cid, PcId = PcId });
                    }
                }
                else
                {
                    return RedirectToAction("Motors", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                model.PcId = PcId.ToString();
                ViewBag.PcId = PcId;
            }
            return View(model);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Motors(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                Session["Actname"] = "Motors";
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).FirstOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
                if (policydetails != null && policyinclu.Exists(o => o.Name == "Motor"))
                {

                }
                else
                {
                    return RedirectToAction("Pets", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
                var units = policyinclu.Where(o => o.Name == "Motor").FirstOrDefault();
                if (Session["UnitId"] != null)
                {
                    int sessionunitId = Convert.ToInt32(Session["UnitId"]);
                    units = policyinclu.Where(o => o.UnitId == sessionunitId).FirstOrDefault();
                }
                else
                {
                    units = policyinclu.Where(o => o.Name == "Motor").FirstOrDefault();
                    Session["UnitId"] = units.UnitId;
                }
                HttpClient hclient = new HttpClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                hclient.BaseAddress = new Uri(url);
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (units != null)
                {                  
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    model.PolicyInclusion = policyinclu;
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        HttpResponseMessage getunit = await hclient.GetAsync("DriverDetails?ApiKey=" + ApiKey);
                        var EmpResponses = getunit.Content.ReadAsStringAsync().Result;
                        if (EmpResponses != null)
                        {
                            model.DriverList = new DriverList();
                            model.DriverList = JsonConvert.DeserializeObject<DriverList>(EmpResponses);
                        }
                        string unit_Type = null;

                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    ////else
                    ////{
                    ////    return RedirectToAction("Pets", "PolicyDetails", new { cid = cid, PcId = PcId });
                    ////}
                }
                ////else
                ////{
                ////    return RedirectToAction("Pets", "PolicyDetails", new { cid = cid, PcId = PcId });
                ////}
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                model.PcId = PcId.ToString();
                ViewBag.PcId = PcId;
            }
            return View(model);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Pets(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                Session["Actname"] = "Pets";
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).FirstOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
                if (policydetails != null && policyinclu.Exists(o => o.Name == "Pets"))
                {

                }
                else
                {
                    return RedirectToAction("PremiumDetails", "Customer", new { cid = cid, PcId = PcId });
                }
                var units = policyinclu.Where(o => o.Name == "Pets").FirstOrDefault();
                if (Session["UnitId"] != null)
                {
                    int sessionunitId = Convert.ToInt32(Session["UnitId"]);
                    units = policyinclu.Where(o => o.UnitId == sessionunitId).FirstOrDefault();
                }
                else
                {
                    units = policyinclu.Where(o => o.Name == "Pets").FirstOrDefault();
                    Session["UnitId"] = units.UnitId;
                }
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    model.PolicyInclusion = policyinclu;
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    if (cid != null)
                    {
                        ViewBag.cid = cid;
                        model.CustomerId = cid.Value;
                    }
                    if (PcId != null)
                    {
                        model.PcId = PcId.ToString();
                        ViewBag.PcId = PcId;
                    }
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    else if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("Object cannot be cast from DBNull to other types."))
                    {
                        ViewBag.error = "We are unable to fetch details at this time.";
                        return View(model);
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    //else
                    //{
                    //    return RedirectToAction("PremiumDetails", "Customer", new { cid = cid ,PcId=PcId});
                    //}
                }
                ////else
                ////{
                ////    return RedirectToAction("PremiumDetails", "Customer", new { cid = cid, PcId = PcId });
                ////}
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> FarmHomeBilding(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            if (cid != null)
            {
                ViewBag.cid = cid;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
            }
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();               
                var units = policyinclu.Where(o => o.Name == "Home Buildings").FirstOrDefault();
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                        //model.ReferralList = policydetails.ReferralList;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    return RedirectToAction("FarmHomeContents", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> FarmHomeContents(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            if (cid != null)
            {
                ViewBag.cid = cid;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
            }
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();              
                var units = policyinclu.Where(o => o.Name == "Home Contents").SingleOrDefault();
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    return RedirectToAction("FarmValuables", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> FarmValuables(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            if (cid != null)
            {
                ViewBag.cid = cid;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
            }
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();              
                var units = policyinclu.Where(o => o.Name == "Valuables").SingleOrDefault();
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Valuables&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    return RedirectToAction("FixedFarmProperty", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> FixedFarmProperty(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();       
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();             
                var units = policyinclu.Where(o => o.Name == "Fixed Farm Property").FirstOrDefault();
                if (Session["UnitId"] != null)
                {
                    int sessionunitId = Convert.ToInt32(Session["UnitId"]);
                    units = policyinclu.Where(o => o.UnitId == sessionunitId).SingleOrDefault();
                    Session["profileId"] = units.ProfileUnId;
                }
                else
                {
                    units = policyinclu.Where(o => o.Name == "Fixed Farm Property").FirstOrDefault();
                    Session["UnitId"] = units.UnitId;
                    Session["profileId"] = units.ProfileUnId;
                }
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Fixed Farm Property&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                        model.PolicyInclusion = policyinclu;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    return RedirectToAction("PersonalLiabilit", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
                model.PcId = PcId.ToString();
            }
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> PersonalLiabilit(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();         
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();           
                var units = policyinclu.Where(o => o.Name == "Personal Liability").SingleOrDefault();
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Personal Liability&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    return RedirectToAction("FarmLivestock", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
                model.PcId = PcId.ToString();
            }
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> FarmLivestock(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
                var units = policyinclu.Where(o => o.Name == "Livestock").SingleOrDefault();
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Livestock&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    return RedirectToAction("FarmMotors", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
                model.PcId = PcId.ToString();
            }
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> FarmMotors(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
                var units = policyinclu.Where(o => o.Name == "Motor").SingleOrDefault();
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Motor&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    return RedirectToAction("MobileFarmProperty", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
                model.PcId = PcId.ToString();
            }
            return View(model);
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> MobileFarmProperty(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {

                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
                var units = policyinclu.Where(o => o.Name == "Mobile Farm Property").FirstOrDefault();
                if (Session["UnitId"] != null)
                {
                    int sessionunitId = Convert.ToInt32(Session["UnitId"]);
                    units = policyinclu.Where(o => o.UnitId == sessionunitId).SingleOrDefault();
                    Session["profileId"] = units.ProfileUnId;
                }
                else
                {
                    units = policyinclu.Where(o => o.Name == "Mobile Farm Property").FirstOrDefault();
                    Session["UnitId"] = units.UnitId;
                    Session["profileId"] = units.ProfileUnId;
                }               
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Mobile Farm Property&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    return RedirectToAction("FarmLiability", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
                model.PolicyData.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                model.PcId = PcId.ToString();
                ViewBag.PcId = PcId;
            }
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> FarmLiability(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();              
                var units = policyinclu.Where(o => o.Name == "Farm Liability").SingleOrDefault();
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Farm Liability&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    return RedirectToAction("Burglary", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
                model.PcId = PcId.ToString();
            }
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> Burglary(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();              
                var units = policyinclu.Where(o => o.Name == "Burglary").SingleOrDefault();
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Burglary&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    return RedirectToAction("FarmElectronics", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
                model.PcId = PcId.ToString();
            }
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> FarmElectronics(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
             
                var units = policyinclu.Where(o => o.Name == "Farm Electronics").SingleOrDefault();
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Farm Electronics&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    return RedirectToAction("FarmInterruption", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
                model.PcId = PcId.ToString();
            }
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> FarmInterruption(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();             
                var units = policyinclu.Where(o => o.Name == "Farm Interruption").SingleOrDefault();
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Farm Interruption&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    return RedirectToAction("FarmMachinery", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
                model.PcId = PcId.ToString();
            }
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> FarmMachinery(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
            
                var units = policyinclu.Where(o => o.Name == "Machinery").SingleOrDefault();
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Machinery&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);

                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    return RedirectToAction("FarmMoney", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
                model.PcId = PcId.ToString();
            }
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> FarmMoney(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();             
                var units = policyinclu.Where(o => o.Name == "Farm Money").SingleOrDefault();
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Farm Money&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    return RedirectToAction("FarmTransit", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
                model.PcId = PcId.ToString();
            }
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> FarmTransit(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            model.PolicyInclusion = new List<usp_GetUnit_Result>();
            if (cid != null)
            {
                ViewBag.cid = cid;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
            }
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                var policyinclu = db.usp_GetUnit(null, PcId, null).ToList();
            
                var units = policyinclu.Where(o => o.Name == "Transit").SingleOrDefault();
                if (units != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + ApiKey + "&Action=Existing&SectionName=Transit&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (policydetails != null)
                    {
                        model.PolicyData = new PolicyDetails();
                        model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                        model.PolicyData.InsuredName = policydetails.InsuredName;
                        model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                        model.PolicyData.PrId = policydetails.PrId;
                        model.PolicyData.PcId = policydetails.PcId;
                        model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                        model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                    }
                    model.PolicyInclusion = policyinclu;
                    if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                    if (model.SectionData != null)
                    {
                        string unit_Type = null;
                        if (units.Component == "Profile")
                        {
                            unit_Type = "P";
                        }
                        else if (units.Component == "Section")
                        {
                            unit_Type = "S";
                        }
                        if (model.SectionData.RowsourceData != null && model.SectionData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.SectionData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.SectionData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.SectionData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.SectionData.ValueData != null && model.SectionData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.SectionData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.SectionData.StateData != null && model.SectionData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.SectionData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else if (model.ProfileData != null)
                    {
                        string unit_Type = null;

                        if (units.Component == "Home")
                        {
                            unit_Type = "H";
                        }
                        else if (units.Component == "Farm")
                        {
                            unit_Type = "F";
                        }
                        if (model.ProfileData.RowsourceData != null && model.ProfileData.RowsourceData.Count > 0)
                        {
                            for (int row = 0; row < model.ProfileData.RowsourceData.Count; row++)
                            {
                                var jsonRowData = JsonConvert.SerializeObject(model.ProfileData.RowsourceData[row].Options);
                                var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, model.ProfileData.RowsourceData[row].Element.ElId, jsonRowData, null, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            }
                        }
                        if (model.ProfileData.ValueData != null && model.ProfileData.ValueData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonValueData = JsonConvert.SerializeObject(model.ProfileData.ValueData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, jsonValueData, null, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.ProfileData.StateData != null && model.ProfileData.StateData.Count > 0)
                        {
                            ////for (int row = 0; row < model.SectionData.ValueData.Count; row++)
                            ////{
                            var jsonStateData = JsonConvert.SerializeObject(model.ProfileData.StateData);
                            // var jsonValueData = jsonSerialiser.Serialize(model.SectionData.ValueData);
                            var inser_Unit_Details = db.IT_dt_Insert_UnitDetails(units.UnitId, units.Component, unit_Type, units.UnitId, null, null, null, jsonStateData, model.ReferralList, model.Status, model.AddressID).SingleOrDefault();
                            ////}
                        }
                        if (model.AddressData != null)
                        {
                            for (int add = 0; add < model.AddressData.Count; add++)
                            {
                                var addressdata = db.IT_CC_InsertAddressDetails(model.AddressData[add].AddressID, model.AddressData[add].AddressLine1, model.AddressData[add].AddressLine1, model.AddressData[add].Suburb, model.AddressData[add].State, model.AddressData[add].Postcode.ToString());
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    return RedirectToAction("FarmHomeBilding", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (cid != null)
            {
                ViewBag.cid = cid;
                model.CustomerId = cid.Value;
            }
            if (PcId != null)
            {
                ViewBag.PcId = PcId;
                model.PcId = PcId.ToString();
            }
            return View(model);
        }
        // GET: PolicyDetails
    }
}