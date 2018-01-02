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
        public async System.Threading.Tasks.Task<ActionResult> HomeBilding(int? cid, int PcId)
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
                var policyinclu = db.usp_GetUnit(null,PcId,null).ToList();
                model.PolicyInclusion = policyinclu;
                var units = policyinclu.Where(o => o.Name == "Home Buildings").SingleOrDefault();            
                if (units != null)
                {

                    HttpClient hclient = new HttpClient();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Home Buildings&SectionUnId=&ProfileUnId=");
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
                    return RedirectToAction("HomeContents", "PolicyDetails", new { cid = cid, PcId= PcId });
                }
            }
            else
            {
                return RedirectToAction("Agenlogin", "Login");
            }

            return View(model);
        }        
        public async System.Threading.Tasks.Task<ActionResult> HomeContents(int? cid, int PcId)
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
                model.PolicyInclusion = policyinclu;
                var units = policyinclu.Where(o => o.Name == "Home Contents").SingleOrDefault();
                if (units != null)
                {

                    HttpClient hclient = new HttpClient();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Home Contents&SectionUnId=&ProfileUnId=");
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
                    return RedirectToAction("Valuables", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("Agenlogin", "Login");
            }

            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> Valuables(int? cid, int PcId)
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
                model.PolicyInclusion = policyinclu;
                var units = policyinclu.Where(o => o.Name == "Valuables").SingleOrDefault();
                if (units != null)
                {

                    HttpClient hclient = new HttpClient();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Valuables&SectionUnId=&ProfileUnId=");
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
                    return RedirectToAction("FarmProperty", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("Agenlogin", "Login");
            }

            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> FarmProperty(int? cid, int PcId)
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
                model.PolicyInclusion = policyinclu;
                var units = policyinclu.Where(o => o.Name == "Farm Property").SingleOrDefault();
                if (units != null)
                {

                    HttpClient hclient = new HttpClient();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Farm Property&SectionUnId=&ProfileUnId=");
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
                    return RedirectToAction("Travels", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("Agenlogin", "Login");
            }

            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> Travels(int? cid, int PcId)
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
                model.PolicyInclusion = policyinclu;
                var units = policyinclu.Where(o => o.Name == "Travels").SingleOrDefault();
                if (units != null)
                {

                    HttpClient hclient = new HttpClient();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Travels&SectionUnId=&ProfileUnId=");
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
                    return RedirectToAction("Liability", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("Agenlogin", "Login");
            }

            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> Liability(int? cid, int PcId)
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
                model.PolicyInclusion = policyinclu;
                var units = policyinclu.Where(o => o.Name == "Liability").SingleOrDefault();
                if (units != null)
                {

                    HttpClient hclient = new HttpClient();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Liability&SectionUnId=&ProfileUnId=");
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
                    return RedirectToAction("Boat", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("Agenlogin", "Login");
            }

            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> Boat(int? cid, int PcId)
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
                model.PolicyInclusion = policyinclu;
                var units = policyinclu.Where(o => o.Name == "Boat").SingleOrDefault();
                if (units != null)
                {

                    HttpClient hclient = new HttpClient();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Boat&SectionUnId=&ProfileUnId=");
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
                    return RedirectToAction("Motors", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("Agenlogin", "Login");
            }

            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> Motors(int? cid, int PcId)
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
                model.PolicyInclusion = policyinclu;
                var units = policyinclu.Where(o => o.Name == "Motors").SingleOrDefault();
                if (units != null)
                {

                    HttpClient hclient = new HttpClient();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Motors&SectionUnId=&ProfileUnId=");
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
                    return RedirectToAction("Pets", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("Agenlogin", "Login");
            }

            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> Pets(int? cid, int PcId)
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
                model.PolicyInclusion = policyinclu;
                var units = policyinclu.Where(o => o.Name == "Pets").SingleOrDefault();
                if (units != null)
                {

                    HttpClient hclient = new HttpClient();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Pets&SectionUnId=&ProfileUnId=");
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
                    return RedirectToAction("HomeBilding", "PolicyDetails", new { cid = cid, PcId = PcId });
                }
            }
            else
            {
                return RedirectToAction("Agenlogin", "Login");
            }

            return View(model);
        }
        // GET: PolicyDetails
    }
}
