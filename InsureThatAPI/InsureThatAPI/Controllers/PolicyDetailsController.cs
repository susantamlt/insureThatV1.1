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
using Newtonsoft.Json;
namespace InsureThatAPI.Controllers
{
    public class PolicyDetailsController : Controller
    {
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> HomeBilding(int? cid, int PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                var policydetails = db.usp_dt_GetPolicyDetails(null, PcId).SingleOrDefault();
                if(policydetails!=null)
                {
                    model.PolicyData.PolicyStatus = policydetails.PolicyStatus;
                    model.PolicyData.InsuredName = policydetails.InsuredName;
                    model.PolicyData.PolicyNumber = policydetails.PolicyNumber;
                    model.PolicyData.PrId = policydetails.PrId;
                    model.PolicyData.InceptionDate = policydetails.InceptionDate.Value;
                    model.PolicyData.ExpiryDate = policydetails.ExpiryDate.Value;
                }
                var units = db.usp_GetUnit(null, policydetails.PolicyId, "Home Buildings").FirstOrDefault();
                if (units != null)
                {

                    HttpClient hclient = new HttpClient();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Home Buildings&SectionUnId=" + units.UnId + "&ProfileUnId=" + units.ProfileUnId);/*insureddetails.InsuredID */
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword

                    model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
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
            }
            else
            {
                return RedirectToAction("", "");
            }

            return View(model);
        }


        // GET: PolicyDetails
    
    }
}
