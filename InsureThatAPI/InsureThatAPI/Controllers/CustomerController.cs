using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using InsureThatAPI.CommonMethods;
using Newtonsoft.Json.Linq;
using System.IO;
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;
using System.Data;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net;

namespace InsureThatAPI.Controllers
{
    public class CustomerController : Controller
    {
        #region PARTIAL VIEW TO STRING
        [NonAction]
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        // GET: Customer
        [HttpGet]
        public ActionResult CustomerSearch()
        {
            Session["EmailId"] = null;
            Session["InsuredId"] = null;
            Session["Actn"] = null;
            Session["UnitId"] = null;
            Session["InsuredName"] = null;
            Session["UnitId"] = null;
            Session["controller"] = null;
            Session["Actname"] = null;
            Session["Home2"] = null;
            Session["hombud"] = null;
            Session["MprofileId"] = null;
            if (Session["apiKey"] != null)
            {

            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            return View();
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CustomerSearch(CustomerSearch customersearch, string term)
        {
            string ApiKey = "";
            var db = new MasterDataEntities();
            if (Session["ApiKey"] != null)
            {
                ApiKey = Session["ApiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }

            HttpClient hclient = new HttpClient();
            if (Session["IyId"] != null && Session["IyId"] != "")
            {
                customersearch.iyId = Session["IyId"].ToString();
            }
            InsuredDetails insuredetails = new InsuredDetails();
            customersearch.iyId = 9262.ToString();//testing should remove
                                                  //  StringContent content = new StringContent(JsonConvert.SerializeObject(customersearch), Encoding.UTF8, "application/json");
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await hclient.GetAsync("InsuredDetails?ApiKey=" + ApiKey + "&iyId=" + customersearch.iyId + "&policyNo=" + customersearch.policyNo + "&insuredName=" + term + "&emailId=" + customersearch.emailId + "&phoneNo=" + customersearch.phoneNo);

            if (Res.IsSuccessStatusCode)
            {

                GetInsuredDetailsRef insureddetails = new GetInsuredDetailsRef();
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                if (EmpResponse != null)
                    insureddetails = JsonConvert.DeserializeObject<GetInsuredDetailsRef>(EmpResponse);
                if (Request.IsAjaxRequest() && insureddetails.Insureds != null && insureddetails.Insureds.Count() > 0)
                {
                    Session["InsuredId"] = insureddetails.Insureds.Select(p => p.InsuredID).FirstOrDefault();
                    Session["EmailId"] = customersearch.emailId;

                    return Json(new { results = insureddetails.Insureds.Select(p => new InsuredListDDL() { id = p.InsuredID, text = p.CompanyBusinessName + p.FirstName + " " + p.MiddleName + " " + p.LastName + " (" + p.Address + ")" }).ToList() });
                }
                else if (insureddetails.Insureds != null && insureddetails.Insureds.Count() > 0)
                {
                    Session["InsuredId"] = insureddetails.Insureds.Select(p => p.InsuredID).FirstOrDefault();
                    Session["EmailId"] = customersearch.emailId;
                    Session["InsuredName"] = insureddetails.Insureds.Select(p => p.FirstName).FirstOrDefault();
                    int? customerid = db.IT_InsertCustomerMaster(customersearch.emailId, customersearch.InsuredId, customersearch.policyNo, null, customersearch.insuredName, null).SingleOrDefault();
                    return RedirectToAction("InsuredPolicys", "Customer", new { InsuredId = insureddetails.Insureds.Select(p => p.InsuredID).FirstOrDefault(), cid = customerid });
                }
                else if (insureddetails.ErrorMessage != null && insureddetails.ErrorMessage.Count() > 0)
                {
                    ViewBag.Status = "Failure";
                    return View(customersearch);
                }
            }
            else
            {
                ViewBag.Status = "Failure";

            }
            if (term != null || customersearch.insuredName != null || customersearch.policyNo != null || customersearch.phoneNo != null)
            {
                TempData["ErrorMessage"] = "No data found.";
                return RedirectToAction("AdvancedCustomerSearch", "Customer", customersearch);
            }
            return View(customersearch);
        }
        [HttpGet]
        public ActionResult AdvancedCustomerSearch(CustomerSearch customersearch, string actions)
        {

            if (Session["Apikey"] != null)
            {
                if (actions != null && !string.IsNullOrEmpty(actions))
                {
                    customersearch.Actntype = "Attach";
                }
                if (actions != null && !string.IsNullOrEmpty(actions) && actions == "attachs")
                {
                    customersearch.Actntype = "Attachs";
                    Session["newattach"] = "Attachs";
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            Session["EmailId"] = null;
            Session["InsuredId"] = null;
            Session["UnitId"] = null;
            Session["Actn"] = null;
            Session["controller"] = null;
            Session["Actname"] = null;
            Session["InsuredName"] = null;
            Session["Home2"] = null;
            Session["hombud"] = null;
            Session["MprofileId"] = null;
            Session["PolicyNo"] = null;
            if (TempData["ExistingError"] != null)
            {
                ViewBag.errormessage = TempData["ExistingError"].ToString();
            }
            return View(customersearch);
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AdvCustomerSearch(CustomerSearch customersearch, string InsuredName, string PolicyNo, string PhoneNo, string EmailId)
        {
            HttpClient hclient = new HttpClient();
            string ApiKey = " ";
            if (Session["ApiKey"] != null)
            {
                ApiKey = Session["ApiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (InsuredName != null && InsuredName == "newattach")
            {
                HttpResponseMessage attach = await hclient.GetAsync("PolicyDetails?apiKey=" + ApiKey + "&action=Attach&pcId=&trId=&prId=1029&InsuredId=&effectiveDate=&reason=");
                if (InsuredName != null && InsuredName == "newattach")
                {
                    return Json(Url.Action("CustomerRegistration", "Customer"));
                }
            }
            if (((customersearch.policyNo == null || string.IsNullOrWhiteSpace(customersearch.policyNo)) && (customersearch.emailId == null || string.IsNullOrWhiteSpace(customersearch.emailId)) && (customersearch.phoneNo == null || string.IsNullOrWhiteSpace(customersearch.phoneNo))) && ((PolicyNo == null || String.IsNullOrWhiteSpace(PolicyNo)) && (InsuredName == null || String.IsNullOrWhiteSpace(InsuredName)) && (EmailId == null || String.IsNullOrWhiteSpace(EmailId)) && (PhoneNo == null || String.IsNullOrWhiteSpace(PhoneNo))))
            {
                TempData["ErrorMsg"] = "Enter any details to search";
                return RedirectToAction("AdvancedCustomerSearch", "Customer");
            }
            customersearch.ErrorMessage = new List<string>();
            //else if ((PolicyNo ==null || String.IsNullOrWhiteSpace(PolicyNo))  && (InsuredName==null || String.IsNullOrWhiteSpace(InsuredName)) && (EmailId==null || String.IsNullOrWhiteSpace(EmailId)) && (PhoneNo==null || String.IsNullOrWhiteSpace(PhoneNo)))
            //{
            //    TempData["ErrorMsg"] = "Enter any details to search";
            //    return RedirectToAction("AdvancedCustomerSearch", "Customer");
            //}

            if (Session["IyId"] != null && Session["IyId"] != "")
            {
                customersearch.iyId = Session["IyId"].ToString();
            }
            InsuredDetails insuredetails = new InsuredDetails();
            MasterDataEntities db = new MasterDataEntities();
            customersearch.iyId = 9262.ToString();//testing should remove//9262 is raci
                                                  //  StringContent content = new StringContent(JsonConvert.SerializeObject(customersearch), Encoding.UTF8, "application/json");
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string[] name = null;
            if (customersearch.insuredName != null)
            {
                string[] stringSeparators = new string[] { " (" };
                name = InsuredName.Split(stringSeparators, StringSplitOptions.None);
            }
            if (customersearch != null && (customersearch.emailId != null || customersearch.phoneNo != null) || customersearch.insuredName != null || (customersearch.policyNo != null))
            {
                //On Submit
                HttpResponseMessage Ress = await hclient.GetAsync("InsuredDetails?ApiKey=" + ApiKey + "&iyId=" + customersearch.iyId + "&policyNo=" + customersearch.policyNo + "&insuredName=" + null + "&emailId=" + customersearch.emailId + "&phoneNo=" + customersearch.phoneNo);
                if (Ress.IsSuccessStatusCode)
                {
                    GetInsuredDetailsRef insureddetails = new GetInsuredDetailsRef();
                    var EmpResponse = Ress.Content.ReadAsStringAsync().Result;
                    insureddetails = JsonConvert.DeserializeObject<GetInsuredDetailsRef>(EmpResponse);
                    if (insureddetails.Insureds != null && insureddetails.Insureds.Count > 0)
                    {
                        ViewBag.InsuredId = insureddetails.Insureds.Select(p => p.InsuredID).FirstOrDefault();
                        Session["InsuredId"] = ViewBag.InsuredId;
                        Session["EmailId"] = customersearch.emailId;
                        Session["InsuredName"] = insureddetails.Insureds.Select(p => p.FirstName).FirstOrDefault();
                        // int cid=db.IT_CC_Insert_InsuredDetails()
                        if (insureddetails != null && insureddetails.Insureds.Count() > 0)
                        {
                            if (insureddetails.Insureds[0].EmailID != null)
                            {
                                customersearch.emailId = insureddetails.Insureds[0].EmailID;
                            }
                            if (insureddetails.Insureds[0].InsuredID != null)
                            {
                                customersearch.InsuredId = insureddetails.Insureds[0].InsuredID;
                            }
                            if (insureddetails.Insureds[0].PolicyNumbers != null && insureddetails.Insureds[0].PolicyNumbers.Count() > 0)
                            {
                                customersearch.policyNo = insureddetails.Insureds[0].PolicyNumbers.First();
                            }
                        }
                        int? customerid = db.IT_InsertCustomerMaster(customersearch.emailId, customersearch.InsuredId, customersearch.policyNo, null, customersearch.insuredName, null).SingleOrDefault();
                        if (Request.IsAjaxRequest())
                        {
                            return Json(new { results = insureddetails.Insureds });
                        }
                        return RedirectToAction("InsuredPolicys", "Customer", new { InsuredId = ViewBag.InsuredId, cid = customerid });
                    }
                    if (insureddetails != null && insureddetails.Status == "Failure" && insureddetails.ErrorMessage != null && insureddetails.ErrorMessage.Count() > 0)
                    {
                        if (Request.IsAjaxRequest())
                        {
                        }
                        else
                        {
                            TempData["ErrorMsg"] = "No Data available";
                            //customersearch.ErrorMessage.Add(ViewBag.errormessage);
                            return RedirectToAction("AdvancedCustomerSearch", "Customer", customersearch);
                        }
                    }
                }
            }
            if (Request.IsAjaxRequest())
            {
                //On Auto Search
                // string[] name = InsuredName.Split(" (");
                HttpResponseMessage Res = await hclient.GetAsync("InsuredDetails?ApiKey=" + ApiKey + "&iyId=" + customersearch.iyId + "&policyNo=" + PolicyNo + "&insuredName=" + name[0] + "&emailId=" + EmailId + "&phoneNo=" + PhoneNo);
                if (Res.IsSuccessStatusCode)
                {
                    GetInsuredDetailsRef insureddetails = new GetInsuredDetailsRef();
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    insureddetails = JsonConvert.DeserializeObject<GetInsuredDetailsRef>(EmpResponse);
                    if (insureddetails.Insureds != null)
                    {
                        ViewBag.InsuredId = insureddetails.Insureds.Select(p => p.InsuredID).FirstOrDefault();
                        db.IT_InsertCustomerMaster(insureddetails.Insureds.Select(p => p.EmailID).FirstOrDefault(), ViewBag.InsuredId, PolicyNo, null, InsuredName, null);
                        return Json(new { results = insureddetails.Insureds });
                    }
                }
                else
                {
                    ViewBag.Status = "Failure";

                }
                if (customersearch.insuredName != null || customersearch.policyNo != null || customersearch.phoneNo != null)
                {
                    TempData["ErrorMessage"] = "No data found.";
                    return RedirectToAction("AdvancedCustomerSearch", "Customer", customersearch);
                }
            }
            return View(customersearch);
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["apiKey"] != null)
            {
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (Session["UserName"] != null)
            {

            }
            return View();
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> ChangePassword(ChangePasswordDetails changepassword)
        {
            if (Session["apiKey"] != null)
            {
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            LogInDetailsClass logincls = new LogInDetailsClass();
            LogInDetails logindetailsmodel = new LogInDetails();
            ChangePasswordDetailsRef changepasswordref = new ChangePasswordDetailsRef();
            string strEncrypt = string.Empty;
            if (Session["UserName"] != null && Session["UserName"] != "")
            {
                string strDecrypt = string.Empty;
                string UserName = string.Empty;
                UserName = Session["UserName"].ToString();
                string PlainTextEncrpted = string.Empty;
                string NewPassword = string.Empty;
                string loginKey = string.Empty;
                int IyId = 9262;
                string EncrptForLogin = String.Format("{0:ddddyyyyMMdd}", DateTime.UtcNow);
                PlainTextEncrpted = IyId + "|" + UserName + "|InsureThatDirect";
                loginKey = LogInDetailsClass.Encrypt(PlainTextEncrpted, EncrptForLogin);
                HttpClient hclient = new HttpClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                hclient.BaseAddress = new Uri(url);
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                loginKey = loginKey.Replace("+", "%2B");
                HttpResponseMessage Res = await hclient.GetAsync("Api/Login?loginKey=" + loginKey + "");
                //change controller name and field name
                //changepassword.Email = Session["Email"].ToString();
                if (Session["UserName"] != null && Session["UserName"] != " ")
                {
                    changepassword.UserName = Session["UserName"].ToString();
                }
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    logindetailsmodel = JsonConvert.DeserializeObject<LogInDetails>(EmpResponse);
                    //strEncrypt = LogInDetailsClass.Encrypt(changepassword.NewPassword, "TimsFirstEncryptionKey");//encrypt password method
                    strDecrypt = LogInDetailsClass.Decrypt(logindetailsmodel.EncryptedPassword, "TimsFirstEncryptionKey");//decrypt password method
                    if (logindetailsmodel.EncryptedPassword != null && strDecrypt == changepassword.Password)
                    {
                        NewPassword = LogInDetailsClass.Encrypt(changepassword.NewPassword, "TimsFirstEncryptionKey");//encrypt new password method
                        changepassword.EncryptedPassword = NewPassword;
                        StringContent content = new StringContent(JsonConvert.SerializeObject(changepassword), Encoding.UTF8, "application/json");
                        var responses = await hclient.PostAsync("Login?UserName=", content);
                        var result = await responses.Content.ReadAsStringAsync();
                        if (result != null)
                        {
                            StatusChangePassword changepass = new StatusChangePassword();
                            changepass = JsonConvert.DeserializeObject<StatusChangePassword>(result);
                            if (changepass != null && changepass.status == "Success")
                            {
                                return RedirectToAction("AgentLogin", "Login");
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "Failed to update New password, try later.";
                            }

                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Old password does not match.";
                    }
                }
            }
            //ViewBag.ErrorMessage = "Old Password does not match.";
            return View();
        }
        #region Display Policy List Based on Insured Id

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> InsuredPolicys(int? InsuredId, int? cid)
        {
            string apikey = null;
            if (Session["ApiKey"] != null)
            {
                apikey = Session["ApiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            Session["Policylocal"] = null;
            GetNewPolicyDetailsRef policydetails = new GetNewPolicyDetailsRef();
            PolicyList policylist = new PolicyList();
            policydetails.PolicyData = new List<PolicyDetails>();
            //InsuredId = 108454;
            Session["UnitId"] = null;
            if (Session["InsuredId"] != null)
            {
                InsuredId = Convert.ToInt32(Session["InsuredId"]);
            }
            if (InsuredId.HasValue && InsuredId > 0)
            {
                //  insureddetails.InsuredID = Convert.ToInt32(Session["InsuredId"]);
                HttpClient hclient = new HttpClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                hclient.BaseAddress = new Uri(url);
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await hclient.GetAsync("PolicyDetails?ApiKey=" + apikey + "&IyId=9262&InsuredID=" + InsuredId);/*insureddetails.InsuredID */
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                policydetails = JsonConvert.DeserializeObject<GetNewPolicyDetailsRef>(EmpResponse);
                ViewBag.CustomerId = cid;
                if (policydetails.PolicyData != null && policydetails.PolicyData.Count() > 0)
                {
                    return View(policydetails);
                }
                else
                {
                    return RedirectToAction("NewPolicy", "Customer", new { cid = cid });
                }
            }
            else
            {
                return RedirectToAction("AdvancedCustomerSearch", "Customer");
            }
        }
        #endregion

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> ViewUpdatePolicyDetails(int? cid, int? PcId, int? policytype, int? step)
        {
            var db = new MasterDataEntities();
            // PolicyNo = "ITRD00075330";
            if (Session["apiKey"] != null)
            {
                Session["apiKey"] = Session["apiKey"];
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            ////PcId = 54611;
            // PcId = 54693;
            Session["UnitId"] = null;
            string policyid = PcId.ToString();
            if (PcId != null && PcId > 0)
            {
                ViewEditPolicyDetails model = new ViewEditPolicyDetails();
                //var policyinclusion = db.IT_GetPolicyInclusions(cid, policyid, policytype).FirstOrDefault();
                //if (policyinclusion != null && policyinclusion.PolicyInclusions != null)
                //{
                //    model.PolicyInclusions = policyinclusion.PolicyInclusions;
                //}
                model.PcId = PcId.ToString();
                HttpClient hclient = new HttpClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                hclient.BaseAddress = new Uri(url);
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string PlainTextEncrpted = string.Empty;
                string ApiKey = string.Empty;
                if (cid != null)
                {
                    ViewBag.cid = cid;
                    model.CustomerId = cid.Value;
                    Session["cid"] = cid.Value;
                }
                else
                {
                    ViewBag.cid = model.CustomerId;
                }
                model.ErrorMessage = new List<string>();
                ApiKey = Session["apiKey"].ToString();
                HttpResponseMessage Res = await hclient.GetAsync("PolicyDetails?ApiKey=" + ApiKey + "&Action=Retrieve&PcId=" + PcId + "&TrId=0&PrId=&InsuredId=&EffectiveDate=&Reason=");/*insureddetails.InsuredID */
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                {
                    return RedirectToAction("AgentLogin", "Login");
                }
                if (model.ErrorMessage != null && model.Status == "Failure")
                {
                    if (model.ErrorMessage != null && model.ErrorMessage.Count() > 0)
                    {
                        for (int i = 0; i < model.ErrorMessage.Count(); i++)
                        {
                            TempData["ErrorMsg"] = model.ErrorMessage[0];
                        }
                    }
                }
                if (model != null)
                {
                    if (model.ApiKey != null)
                    {
                        Session["apiKey"] = model.ApiKey;
                    }
                    else
                    {
                        Session["apiKey"] = ApiKey;
                    }
                    if (model.PolicyData != null && (model.UnitData != null && model.UnitData.Count() > 0))
                    {
                        var insertpolicydetails = db.IT_dt_Insert_PolicyDetails(model.PolicyData.PolicyNumber, model.PolicyData.InsuredId, model.PolicyData.TransactionNumber, model.PolicyData.PcId, model.PolicyData.TrId, model.PolicyData.TermNumber, model.PolicyData.AccountManagerID,
                            model.PolicyData.PolicyStatus, model.PolicyData.CoverPeriod, model.PolicyData.CoverPeriodUnit, model.PolicyData.InceptionDate, model.PolicyData.ExpiryDate, model.PolicyData.EffectiveDate, model.PolicyData.PrId, model.PolicyData.IyId,
                            model.PolicyData.InsuredName, model.PolicyData.RemoveStampDuty, model.PolicyData.CreatedbyUserId, model.PolicyData.Timecreated, model.PolicyData.IsFloodCoverRequired, model.PolicyData.HasMadeAClaim, model.PolicyData.Reason, model.Status).SingleOrDefault();

                        if (insertpolicydetails > 1)
                        {
                            if (model.UnitData != null && model.UnitData.Count > 0)
                            {
                                Session["Policylocal"] = insertpolicydetails;
                                if (model.PolicyData.PolicyNumber != null)
                                {
                                    Session["PolicyNo"] = model.PolicyData.PolicyNumber;
                                }
                                var getunits = db.usp_GetUnit(null, PcId, null).ToList();
                                if (getunits != null && getunits.Count() > 0)
                                {
                                    var deleteunits = db.IT_dt_Delete_Unit(null, insertpolicydetails, null, null, null, null, null, null, null);
                                }
                                var getunitss = db.usp_GetUnit(null, PcId, null).ToList();
                                for (int i = 0; i < model.UnitData.Count; i++)
                                {
                                    var insertunits = db.IT_dt_Insert_Unit(insertpolicydetails, model.UnitData[i].Component, model.UnitData[i].Name, model.UnitData[i].UnId, model.UnitData[i].UnitNumber,
                                        model.UnitData[i].UnitStatus, model.UnitData[i].ProfileUnId, model.ReferralList, model.Status).SingleOrDefault();
                                }
                            }
                        }
                    }
                    if (model.UnitData != null && model.UnitData.Count > 0)
                    {
                        model.PolicyInclusions = null;

                        for (int pi = 0; pi < model.UnitData.Count; pi++)
                        {
                            if (pi < model.UnitData.Count)
                            {
                                if (model.PolicyData.PrId == 1029)
                                {
                                    if (model.UnitData[pi].Name == "Home")
                                    {
                                        model.PolicyInclusions = "HB";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.RLS), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Farm Property")
                                    {
                                        model.PolicyInclusions = "HC";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.RLS), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Valuables")
                                    {
                                        model.PolicyInclusions = "HV";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.RLS), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Home Contents")
                                    {
                                        model.PolicyInclusions = "HFP";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.RLS), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Travels")
                                    {
                                        model.PolicyInclusions = "HL";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.RLS), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Liability")
                                    {
                                        model.PolicyInclusions = "HT";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.RLS), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Boat")
                                    {
                                        model.PolicyInclusions = "HB";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.RLS), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Motors")
                                    {
                                        model.PolicyInclusions = "HM";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.RLS), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Pets")
                                    {
                                        model.PolicyInclusions = "HP";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.RLS), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    return RedirectToAction("HomeBilding", "PolicyDetails", new { cid = cid, PcId = PcId });
                                }
                                if (model.PolicyData.PrId == 1021)
                                {
                                    if (model.UnitData[pi].Name == "Home")
                                    {
                                        model.PolicyInclusions = "FH";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.FarmPolicy), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Farm")
                                    {
                                        model.PolicyInclusions = "FF";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.FarmPolicy), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Valuables")
                                    {
                                        model.PolicyInclusions = "FV";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.FarmPolicy), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Fixed Farm Property")
                                    {
                                        model.PolicyInclusions = "FFP";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.FarmPolicy), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Home Buildings")
                                    {
                                        model.PolicyInclusions = "FHB";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.FarmPolicy), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Motor Personal Liability")
                                    {
                                        model.PolicyInclusions = "MPL";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.FarmPolicy), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Livestock")
                                    {
                                        model.PolicyInclusions = "FL";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.FarmPolicy), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Motors")
                                    {
                                        model.PolicyInclusions = "FM";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.FarmPolicy), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Mobile Farm Property")
                                    {
                                        model.PolicyInclusions = "MFP";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.RLS), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Motor Personal Liability")
                                    {
                                        model.PolicyInclusions = "MPL";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.FarmPolicy), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Livestock")
                                    {
                                        model.PolicyInclusions = "FL";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.FarmPolicy), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData[pi].Name == "Motors")
                                    {
                                        model.PolicyInclusions = "FM";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.FarmPolicy), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }
                                    if (model.UnitData.Exists(p => p.Name == "Fixed Farm Property"))
                                    {
                                        return RedirectToAction("FixedFarmProperty", "PolicyDetails", new { cid = cid, PcId = PcId });
                                    }
                                    else if (model.UnitData.Exists(p => p.Name == "Mobile Farm Property"))
                                    {
                                        return RedirectToAction("MobileFarmProperty", "PolicyDetails", new { cid = cid, PcId = PcId });
                                    }
                                    else if (model.UnitData.Exists(p => p.Name == "Home Buildings"))
                                    {
                                        return RedirectToAction("FarmHomeBilding", "PolicyDetails", new { cid = cid, PcId = PcId });
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return RedirectToAction("AdvancedCustomerSearch", "Customer");
        }

        //[HttpPost]
        //public async System.Threading.Tasks.Task<ActionResult> ViewUpdatePolicyDetails(int? PcId, int? step)
        //{
        //    // PolicyNo = "ITRD00075330";
        //    if (Session["apiKey"] != null)
        //    {
        //        Session["apiKey"] = Session["apiKey"];
        //    }
        //    else
        //    {
        //        return RedirectToAction("AgentLogin", "Login");
        //    }
        //    PcId = 54611;
        //    if (PcId != null && PcId > 0)
        //    {
        //        ViewEditPolicyDetails model = new ViewEditPolicyDetails();
        //        HttpClient hclient = new HttpClient();
        //        hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        string PlainTextEncrpted = string.Empty;
        //        string ApiKey = string.Empty;

        //        ApiKey = Session["apiKey"].ToString();
        //        HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/PolicyDetails?apiKey=" + ApiKey + "&action=Retrieve&pcId=" + PcId + "&trId=0&effectiveDate=1900-01-01&reason=");/*insureddetails.InsuredID */
        //        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

        //        //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword

        //        model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);

        //        if (model.RiskData != null && model.RiskData.Count > 0)
        //        {
        //            if (model.RiskData.Select(o => o.Name).FirstOrDefault() == "Home" && step == 1)
        //            {
        //                return View(model);
        //            }
        //            if (model.RiskData.Select(o => o.Name).FirstOrDefault() == "Home" && step == 2)
        //            {
        //                return PartialView("ViewOccupancyIPHBuilding", model);
        //            }
        //            if (model.RiskData.Exists(o => o.Name == "Home Contents") && step == 3)
        //            {
        //                //model.RiskData = model.RiskData.Where(p => p.Name == "Home Contents").Select(o => o.Elements).ToList();
        //                return PartialView("ViewHomeContent", model);
        //            }
        //            if (model.RiskData.Exists(o => o.Name == "Valuables") && step == 4)
        //            {
        //                return PartialView("ViewValuables", model);
        //            }
        //            if (model.RiskData.Exists(o => o.Name == "Farm Property") && step == 5)
        //            {
        //                return PartialView("ViewFarmProperty", model);
        //            }
        //            if (model.RiskData.Exists(o => o.Name == "Farm Property") && step == 6)
        //            {
        //                return PartialView("ViewFarmMachinery", model);
        //            }
        //            if (model.PremiumData != null && model.PremiumData.Count > 0)
        //            {
        //                return PartialView("ViewQuotation", model);
        //            }
        //            if (model.RiskData.Select(p => p.Name).First() == "Home Building")
        //            {
        //                // return Json(new { content = RenderRazorViewToString("_UpdateEditRolePermissionList", manageusers), rolesddl = manageusers.RoleList, rolenames = manageusers.RoleName, ViewBag.messages, status = true, msg = "Role assigned successfully." });
        //            }
        //            return View(model);
        //        }


        //    }

        //    return RedirectToAction("InsuredPolicys", "Customer", new { InsuredId = 108454, CustomerId = 1 });
        //}


        [HttpGet]
        public ActionResult NewPolicy(int? cid, int? insureId)
        {
            if (Session["ApiKey"] != null)
            {
                if (insureId != null && insureId > 0)
                {
                    Session["unId"] = null;
                    Session["profileId"] = null;
                    Session["HProfileId"] = null;
                    Session["UnitId"] = null;
                    Session["FProfileId"] = null;
                }

                else { Session["CustomerType"] = "New"; }
                ViewBag.CustomerId = cid;
                Session["Actn"] = null;
                Session["profileId"] = null;
                Session["unId"] = null;
                Session["UnitId"] = null;
                Session["Policyinclustions"] = null;
                Session["controller"] = null;
                Session["Actn"] = null;
                Session["Home2"] = null;
                Session["MprofileId"] = null;
                Session["PolicyNo"] = null;
                Session["PrId"] = null;
                PolicyTypes model = new PolicyTypes();
                if (insureId.HasValue)
                {
                    model.InsureId = insureId.Value;
                }
                model.cid = cid;
                return View(model);
            }
            return RedirectToAction("Agentlogin", "login");
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> NewPolicy(int? cid, PolicyTypes model)
        {
            var db = new MasterDataEntities();
            ////if (model.InsureId != null && model.InsureId > 0 && model.cid != null)
            ////{
            string apikey = null;
            if (Session["ApiKey"] != null)
            {
                apikey = Session["ApiKey"].ToString();
            }
            // model.InsureId = insureId;
            if (model.PolicyType == null || model.PolicyType == 0)
            {
                model.PolicyType = 1029;
                return View(model);
            }
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await hclient.GetAsync("PolicyDetails?apiKey=" + apikey + "&action=New&pcId=&trId=&prId=" + model.PolicyType + "&InsuredId=" + model.InsureId + "&effectiveDate=&reason=");/*insureddetails.InsuredID */
                                                                                                                                                                                                                 //   HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/PolicyDetails?apiKey=" + apikey + "&action=New&pcId=&trId=&prId=1029&InsuredId=94357&effectiveDate=&reason=");/*insureddetails.InsuredID */
            var EmpResponse = Res.Content.ReadAsStringAsync().Result;
            PolicyDetails pds = new PolicyDetails();
            ViewEditPolicyDetails pd = new ViewEditPolicyDetails();
            pd = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
            if (pd != null && pd.PolicyData != null && pd.PolicyData.PcId == -1)
            {
                if (pd.ApiKey != null)
                {
                    Session["ApiKey"] = pd.ApiKey;
                }
                var insertresult = db.IT_dt_Insert_PolicyDetails(pd.PolicyData.PolicyNumber, model.InsureId, pd.PolicyData.TransactionNumber, pd.PolicyData.PcId, pd.PolicyData.TrId, pd.PolicyData.TermNumber, pd.PolicyData.AccountManagerID, pd.PolicyData.PolicyStatus,
                    pd.PolicyData.CoverPeriod, pd.PolicyData.CoverPeriodUnit, pd.PolicyData.InceptionDate, pd.PolicyData.ExpiryDate, pd.PolicyData.EffectiveDate, pd.PolicyData.PrId, pd.PolicyData.IyId, pd.PolicyData.InsuredName, pd.PolicyData.RemoveStampDuty, pd.PolicyData.CreatedbyUserId,
                    pd.PolicyData.Timecreated, pd.PolicyData.IsFloodCoverRequired, pd.PolicyData.HasMadeAClaim, pd.Reason, pd.Status).SingleOrDefault();
                Session["PrId"] = pd.PolicyData.PrId;
            }
            else
            {
                return View(model);
            }
            if (pd.UnitData != null && pd.UnitData.Count > 0)
            {

            }
            if (cid != null)
            {
                ViewBag.CustomerId = cid;
                model.cid = cid;
            }
            else if (model.cid != null)
            {
                ViewBag.CustomerId = model.cid;
                cid = model.cid;
            }
            string CustomerType = null;
            Session["PolicyType"] = model.PolicyType;
            if (Session["CustomerType"] != null)
            {
                CustomerType = Session["CustomerType"].ToString();
                if (CustomerType != null && CustomerType == "New")
                {
                    //  return RedirectToAction("CustomerRegistration", "Customer");
                }
            }
            // return RedirectToAction("PolicyInclustions", new { cid = cid, type = model.PolicyType });
            return RedirectToAction("FloodArea", "Flood", new { cid = cid, type = model.PolicyType });
        }
        [HttpGet]
        public ActionResult PolicyInclustions(int? cid, int? type)
        {
            if (Session["ApiKey"] != null)
            {

            }
            else
            {
                return RedirectToAction("Agentlogin", "login");
            }
            Session.Remove("Policyinclustions");
            //   Session["Policyinclustions"] = null;
            PolicyInclustions model = new PolicyInclustions();
            model.PolicyInclusion = new List<IT_GetPolicyInclusions_Result>();
            if (type == 1029)
            {

            }
            else if (type == 1021)
            {

            }
            else
            {
                type = 1029;
            }
            var db = new MasterDataEntities();
            var inclustionslist = db.IT_GetPolicyInclusions(cid, null, type).ToList();
            Session["newattach"] = null;
            //  var inclustionslist = db.usp_GetUnit(null, null, null).ToList();
            if (inclustionslist != null && inclustionslist.Count > 0)
            {
                model.PolicyInclusion = inclustionslist;
            }
            model.PolicyType = type;
            ViewBag.CustomerId = cid;
            model.CustomerId = cid;
            return View(model);
        }
        [HttpPost]
        public ActionResult PolicyInclustions(int? cid, PolicyInclustions model)
        {
            if (cid != null && cid > 0)
            {
                model.CustomerId = cid;
            }
            else { cid = model.CustomerId; }
            var db = new MasterDataEntities();
            if (model.PolicyInclusion != null && model.PolicyInclusion.Count() > 0)
            {
                //  if(model.PolicyInclusion.Exists(p=>p.))
            }
            List<SessionModel> PolicyinslustionsList = new List<SessionModel>();
            string policyid = null;

            if (model.PolicyType == 1029)
            {
                SessionModel sm = new SessionModel();
                sm.name = "Home Buildings";
                PolicyinslustionsList.Add(sm);
                SessionModel sms123 = new SessionModel();
                sms123.name = "Liability";
                PolicyinslustionsList.Add(sms123);
                if (PolicyinslustionsList == null || PolicyinslustionsList.Count() == 0 && model.Valuables == false && model.FarmProperty == false && model.Liability == false && model.Travels == false && model.Boat == false && model.Pet == false)
                {
                    ViewBag.ErrorMsg = "Please select any inclustion to continue.";
                    return View(model);
                }
                else
                {
                    //if (model.HomeBuilding == true)
                    //{

                    //}
                    if (model.HomeContents == true)
                    {
                        SessionModel sms = new SessionModel();
                        sms.name = "Home Contents";
                        PolicyinslustionsList.Add(sms);
                    }
                    if (model.Valuables == true)
                    {
                        SessionModel sms1 = new SessionModel();
                        sms1.name = "Valuables";
                        PolicyinslustionsList.Add(sms1);

                    }
                    if (model.FarmProperty == true)
                    {
                        SessionModel sms12 = new SessionModel();
                        sms12.name = "Farm Property";
                        PolicyinslustionsList.Add(sms12);
                    }
                    //if (model.Liability == true)
                    //{

                    //}
                    if (model.Travels == true)
                    {
                        SessionModel sms1232 = new SessionModel();
                        sms1232.name = "Travel";
                        PolicyinslustionsList.Add(sms1232);
                    }
                    if (model.Boat == true)
                    {
                        SessionModel sms12321 = new SessionModel();
                        sms12321.name = "Boat";
                        PolicyinslustionsList.Add(sms12321);

                    }
                    if (model.Motor == true)
                    {
                        SessionModel sms123210 = new SessionModel();
                        sms123210.name = "Motor";
                        PolicyinslustionsList.Add(sms123210);

                    }
                    if (model.Pet == true)
                    {
                        SessionModel smsa0 = new SessionModel();
                        smsa0.name = "Pet";
                        PolicyinslustionsList.Add(smsa0);
                    }
                }
                Session["Policyinclustions"] = PolicyinslustionsList;

            }
            else if (model.PolicyType == 1021)
            {
                if ((PolicyinslustionsList != null && PolicyinslustionsList.Count() <= 0) && (model.MobileFarmProperty == false && model.FixedFarmProperty == false && model.FarmInteruption == false && model.FarmLiability == false
                    && model.Burglary == false && model.Electronics == false && model.Money == false && model.Transit == false && model.ValuablesFarm == false
                    && model.LiveStockFarm == false && model.PersonalLiabilitiesFarm == false && model.HomeBuildingFarm == false && model.HomeContent == false &&
                    model.Machinery == false && model.MotorFarm == false))
                {
                    ViewBag.ErrorMsg = "Please select any inclustion to continue.";
                    return View(model);
                }
                else
                {
                    if (model.FixedFarmProperty == true)
                    {
                        SessionModel sms123 = new SessionModel();
                        sms123.name = "Fixed Farm Property";
                        PolicyinslustionsList.Add(sms123);
                    }
                    SessionModel sms12 = new SessionModel();
                    sms12.name = "Fixed Farm Property";
                    PolicyinslustionsList.Add(sms12);
                    if (model.MobileFarmProperty == true)
                    {
                        SessionModel sms1 = new SessionModel();
                        sms1.name = "Mobile Farm Property";
                        PolicyinslustionsList.Add(sms1);
                    }
                    if (model.FarmInteruption == true)
                    {
                        SessionModel smsa = new SessionModel();
                        smsa.name = "Farm Interuption";
                        PolicyinslustionsList.Add(smsa);
                    }
                    if (model.FarmLiability == true)
                    {
                        SessionModel smsaa = new SessionModel();
                        smsaa.name = "Farm Liability";
                        PolicyinslustionsList.Add(smsaa);
                    }
                    if (model.Burglary == true)
                    {
                        SessionModel smsq = new SessionModel();
                        smsq.name = "Burglary";
                        PolicyinslustionsList.Add(smsq);
                    }
                    if (model.Electronics == true)
                    {
                        SessionModel smsq1 = new SessionModel();
                        smsq1.name = "Electronics";
                        PolicyinslustionsList.Add(smsq1);
                    }
                    if (model.Money == true)
                    {
                        SessionModel smsq12 = new SessionModel();
                        smsq12.name = "Money";
                        PolicyinslustionsList.Add(smsq12);
                    }
                    if (model.Transit == true)
                    {
                        SessionModel smsq2 = new SessionModel();
                        smsq2.name = "Transit";
                        PolicyinslustionsList.Add(smsq2);
                    }
                    if (model.ValuablesFarm == true)
                    {
                        SessionModel smsq23 = new SessionModel();
                        smsq23.name = "Valuables";
                        PolicyinslustionsList.Add(smsq23);
                    }
                    if (model.LiveStockFarm == true)
                    {
                        SessionModel smsq234 = new SessionModel();
                        smsq234.name = "LiveStock";
                        PolicyinslustionsList.Add(smsq234);
                    }
                    if (model.PersonalLiabilitiesFarm == true)
                    {
                        SessionModel smsq24 = new SessionModel();
                        smsq24.name = "Personal Liabilities Farm";
                        PolicyinslustionsList.Add(smsq24);
                    }
                    if (model.HomeBuildingFarm == true)
                    {
                        SessionModel smsq243 = new SessionModel();
                        smsq243.name = "Home Buildings";
                        PolicyinslustionsList.Add(smsq243);
                    }
                    if (model.HomeContent == true)
                    {
                        SessionModel smsq23 = new SessionModel();
                        smsq23.name = "Home Contents";
                        PolicyinslustionsList.Add(smsq23);
                    }
                    if (model.Machinery == true)
                    {
                        SessionModel smsq213 = new SessionModel();
                        smsq213.name = "Machinery";
                        PolicyinslustionsList.Add(smsq213);
                    }
                    if (model.MotorFarm == true)
                    {
                        SessionModel sms213 = new SessionModel();
                        sms213.name = "Motor";
                        PolicyinslustionsList.Add(sms213);
                    }
                }
                SessionObject obj = new SessionObject();
                SessionModel objmodel = new SessionModel();
                obj.PolicyIncList = new List<SessionModel>();
                Session["Policyinclustions"] = PolicyinslustionsList;
            }

            if (PolicyinslustionsList != null && PolicyinslustionsList.Count > 0)
            {
                //for (int unitselected = 0; unitselected <= PolicyinslustionsList.Count(); unitselected++)
                //{
                //    //var policyinclusions = db.IT_InsertPolicyInclusions(cid, PolicyinslustionsList[unitselected], policyid, model.PolicyType, null, null, null).SingleOrDefault();

                //}
                if (model.PolicyType == (int)PolicyType.RLS)
                {
                    return RedirectToAction("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = policyid });
                }
                else if (model.PolicyType == (int)PolicyType.FarmPolicy)
                {
                    return RedirectToAction("FarmDetails", "FarmPolicyProperty", new { cid = cid, PcId = policyid });
                }
                return RedirectToAction("NewPolicy", "RuralLifeStyle", new { cid = cid });
            }
            return View(model);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> InsuredList(int cid, string PolicyNumber)
        {
            if (Session["apiKey"] != null)
            {
                Session["apiKey"] = Session["apiKey"];
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (PolicyNumber != null && !string.IsNullOrEmpty(PolicyNumber))
            {
                PolicyHistory model = new PolicyHistory();
                HttpClient hclient = new HttpClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                hclient.BaseAddress = new Uri(url);
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string ApiKey = string.Empty;
                ViewBag.cid = cid;
                ApiKey = Session["apiKey"].ToString();
                HttpResponseMessage Res = await hclient.GetAsync("PolicyHistory?apiKey=" + ApiKey + "&policyNumber=" + PolicyNumber);
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<PolicyHistory>(EmpResponse);
                }

                return View(model);
            }
            return View();
        }

        public ActionResult newcustomer(string name, string email, string phonenumber)
        {
            if (Request.IsAjaxRequest())
            {
                var db = new MasterDataEntities();
                int insureId = 1;
                var insertcust = db.IT_InsertCustomerMaster(email, insureId, null, null, name, null).SingleOrDefault();
                int? insureinsert = db.IT_CC_Insert_InsuredDetails(null, null, null, null, name, null, null, null, null, null, 1, 1, phonenumber, null, null, email).SingleOrDefault();
            }
            return Json(new { results = "" });
        }
        #region Ajax API calls on each element
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> ElementDetails(int PrId, int ElId, string Value, int? ItId)
        {
            if (Request.IsAjaxRequest())
            {
                HttpClient hclient = new HttpClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                hclient.BaseAddress = new Uri(url);
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ViewEditPolicyDetails modell = new ViewEditPolicyDetails();
                string ApiKey = string.Empty;
                ElementDetails model = new Models.ElementDetails();
                ValueDatass vd = new ValueDatass();
                vd.Element = new Elementss();
                StateData sd = new StateData();
                sd.Element = new Elements();
                int UnId = Convert.ToInt32(Session["unId"]);
                int ProfileId = Convert.ToInt32(Session["profileId"]);
                if (ProfileId == null || ProfileId == 0)
                {
                    ProfileId = Convert.ToInt32(Session["profileId"]);
                }
                List<ValueDatass> valueData = new List<ValueDatass>();
                List<StateData> stateData = new List<StateData>();
                List<RowsourceDatas> RowsourceData = new List<RowsourceDatas>();
                if (Session["ApiKey"] != null)
                {
                    ApiKey = Session["apiKey"].ToString();
                    model.ApiKey = ApiKey;
                    model.ElId = ElId;
                    model.ItId = ItId ?? 0;
                    model.ProfileUnId = ProfileId;
                    model.SectionUnId = UnId;
                    model.Value = Value;
                }
                else
                {
                    return Json(new { Status = false, data = "login" });
                    return RedirectToAction("AgentLogin", "Login");
                }
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var responses = await hclient.PostAsync("" + "ElementDetails", content);
                var result = await responses.Content.ReadAsStringAsync();
                if (result != null)
                {
                    modell = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(result);
                    if (modell.Status == "Failure")
                    {
                        return Json(new { Status = false });
                    }
                    if (modell.ElementData != null && modell.ElementData.RowsourceData != null && modell.ElementData.RowsourceData.Count() > 0)
                    {
                        for (int i = 0; i < modell.ElementData.RowsourceData.Count(); i++)
                        {
                            RowsourceDatas rd = new RowsourceDatas();
                            Elements el = new Elements();
                            rd.Element = new Elements();
                            rd.Options = new List<Option>();
                            rd.Element.ElId = modell.ElementData.RowsourceData[i].Element.ElId;
                            rd.Element.ItId = modell.ElementData.RowsourceData[i].Element.ItId;
                            List<Option> oplist = new List<Option>();
                            if (modell.ElementData.RowsourceData[i].Options != null && modell.ElementData.RowsourceData[i].Options.Count() > 0)
                            {
                                for (int j = 0; j < modell.ElementData.RowsourceData[i].Options.Count(); j++)
                                {
                                    Option op = new Option();
                                    op.DataText = modell.ElementData.RowsourceData[i].Options[j].DataText;
                                    op.DataValue = modell.ElementData.RowsourceData[i].Options[j].DataValue;
                                    oplist.Add(op);
                                }
                            }
                            rd.Options = oplist;
                            RowsourceData.Add(rd);
                        }
                    }
                    if (modell != null && modell.ReferralList != null)
                    {
                        string ReferralList = modell.ReferralList;
                    }
                    if (modell.ElementData != null && modell.ElementData.ValueData != null)
                    {
                        if (modell.ElementData.ValueData != null && modell.ElementData.ValueData.Count() > 0)
                        {
                            for (int j = 0; j < modell.ElementData.ValueData.Count(); j++)
                            {
                                int? ELLId = modell.ElementData.ValueData[j].Element.ElId;
                                vd.Element.ElId = ELLId ?? 0;
                                int? itiid = modell.ElementData.ValueData[j].Element.ItId;
                                vd.Element.ItId = itiid ?? 0;
                                string Val = modell.ElementData.ValueData[j].Value;
                                vd.Value = Val;
                                valueData.Add(vd);
                            }
                        }
                        if (modell.ElementData != null && modell.ElementData.StateData != null && modell.ElementData.StateData.Count > 0)
                        {
                            string statevalue = "0";
                            for (int j = 0; j < modell.ElementData.StateData.Count(); j++)
                            {
                                int? ELLId = modell.ElementData.StateData[j].Element.ElId;
                                sd.Element.ElId = ELLId ?? 0;
                                statevalue = modell.ElementData.StateData[j].State;
                                if (statevalue == "1")
                                {
                                    sd.Value = "Invisible";
                                }
                                else if (statevalue == "2")
                                {
                                    sd.Value = "Disabled";
                                }
                                else if (statevalue == "4")
                                {
                                    sd.Value = "Read Only";
                                }
                                else if (statevalue == "8")
                                {
                                    sd.Value = "Strike Through";
                                }
                                else if (statevalue == "16")
                                {
                                    sd.Value = "Required";
                                }
                                else if (statevalue == "32")
                                {
                                    sd.Value = "Not Rendered";
                                }
                                stateData.Add(sd);
                            }
                        }
                    }
                    return Json(new { Status = true, rowsorcedata = RowsourceData, valuedata = valueData, stateData = stateData, referrallist = modell.ReferralList, error = modell.ErrorMessage, status = modell.Status, usermessage = modell.ElementData.UserMessage, referrallists = modell.ElementData.ReferralList });
                }
            }
            return Json(new { Status = false });
        }

        #endregion

        #region AJAX CALL for PREMIUM DETAILS
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> PremiumDetails(int? cid, int? PcId)
        {
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();
            if (Session["ApiKey"] != null)
            {
                MasterDataEntities db = new MasterDataEntities();
                if (PcId != null && PcId.HasValue)
                {
                    var policyinclusions = db.usp_GetUnit(null, PcId, null).ToList();
                    model.PolicyInclusion = new List<usp_GetUnit_Result>();
                    model.PolicyInclusion = policyinclusions;
                }

                string apikey = Session["ApiKey"].ToString();
                HttpClient hclient = new HttpClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                hclient.BaseAddress = new Uri(url);
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await hclient.GetAsync("PremiumDetails?ApiKey=" + apikey);/*insureddetails.InsuredID */
                                                                                                    //   HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/PolicyDetails?apiKey=" + apikey + "&action=New&pcId=&trId=&prId=1029&InsuredId=94357&effectiveDate=&reason=");/*insureddetails.InsuredID */
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                PremiumDetail pd = new PremiumDetail();
                pd.PremiumData = new List<PremiumDetails>();
                if (cid.HasValue && cid > 0)
                {
                    ViewBag.cid = cid;
                    model.CustomerId = cid.Value;
                }

                if (Session["Policyinclustions"] != null)
                {
                    model.PolicyInc = new List<SessionModel>();
                    var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                    model.PolicyInc = Policyincllist;
                }
                pd = JsonConvert.DeserializeObject<PremiumDetail>(EmpResponse);
                if (pd != null)
                {
                    if (pd.PremiumData != null)
                    {
                        model.PremiumData = pd.PremiumData;
                        model.UnderWritterFee = pd.UnderwriterFee;
                        model.GSTonFee = pd.FeeGst;
                        model.InvoiceAmount = pd.InvoiceTotal;
                    }

                }

            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (PcId != null && PcId.HasValue)
            {
                model.PcId = PcId.Value.ToString();
            }
            return View(model);

        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> PremiumDetails()
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["ApiKey"] != null)
                {
                    string apikey = Session["ApiKey"].ToString();
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await hclient.GetAsync("PremiumDetails?ApiKey=" + apikey);/*insureddetails.InsuredID */
                                                                                                        //   HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/PolicyDetails?apiKey=" + apikey + "&action=New&pcId=&trId=&prId=1029&InsuredId=94357&effectiveDate=&reason=");/*insureddetails.InsuredID */
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    PremiumDetail pd = new PremiumDetail();
                    pd.PremiumData = new List<PremiumDetails>();

                    pd = JsonConvert.DeserializeObject<PremiumDetail>(EmpResponse);
                    if (pd != null)
                    {
                        if (pd.PremiumData != null)
                        {

                        }
                        return Json(new
                        {
                            Status = true,
                            Premium = pd.PremiumData,
                            UnderwriterFee = pd.UnderwriterFee,
                            FeeGst = pd.FeeGst,
                            InvoiceTotal = pd.InvoiceTotal
                        });
                    }

                }
                else
                {
                    return RedirectToAction("AgentLogin", "Login");
                }
            }
            ViewEditPolicyDetails model = new ViewEditPolicyDetails();

            return Json(new { Status = false });
        }
        #endregion
        [HttpPost]
        #region AJAX Unit Details saving
        [ValidateInput(false)]
        public async System.Threading.Tasks.Task<ActionResult> AddUnit(string unit, int? cid, int? pcid, int? Prid)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["ApiKey"] != null)
                {
                    var db = new MasterDataEntities();
                    ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
                    string apikey = Session["ApiKey"].ToString();
                    string profile = "";
                    if (unit == "HomeBuildings")
                    {
                        unit = "Home Buildings";
                    }
                    else if (unit == "HomeContents")
                    {
                        unit = "Home Contents";
                    }
                    else if (unit == "FarmProperty")
                    {
                        unit = "Farm Property";
                    }
                    if (Prid == 1029 && (unit == "Home Contents" || unit == "Valuables"))
                    {
                        if (Session["HprofileId"] != null)
                        {
                            profile = Session["HprofileId"].ToString();
                        }
                    }
                    if (pcid != null && pcid > 0)
                    {
                        HttpClient hclient = new HttpClient();
                        string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                        hclient.BaseAddress = new Uri(url);
                        hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=" + unit + "&SectionUnId=&ProfileUnId=" + profile);
                        var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                        if (EmpResponse != null)
                        {
                            unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                            if (unitdetails.Status == "Failure")
                            {
                                return Json(new
                                {
                                    Status = false
                                });
                            }
                            if (unitdetails.SectionData != null)
                            {
                                int? policynu = -1;
                                Session["unId"] = unitdetails.SectionData.UnId;
                                Session["profileId"] = unitdetails.SectionData.ProfileUnId;
                                if (Session["Policylocal"] != null)
                                {
                                    policynu = Convert.ToInt32(Session["Policylocal"]);
                                }
                                var insertunits = db.IT_dt_Insert_Unit(policynu, "Section", unit, unitdetails.SectionData.UnId, unitdetails.SectionData.UnitNumber,
                                         "True", unitdetails.SectionData.ProfileUnId, unitdetails.ReferralList, unitdetails.Status).SingleOrDefault();
                            }
                        }
                    }
                    else //Adding to Session 
                    {
                        if (Session["Policyinclustions"] != null)
                        {
                            SessionModel sess = new SessionModel();
                            List<SessionModel> values = new List<SessionModel>();
                            values = (List<SessionModel>)Session["Policyinclustions"];
                            //Add new one
                            if (values.Exists(p => p.name == unit))
                            {
                                var fullvaluelist = values.Select(p => p.name).ToList();
                                // var name = fullvaluelist.FindLast(unit);
                            }
                            sess.name = unit;
                            values.Add(sess);
                            //if (unit == "Home Buildings")
                            //{
                            //    SessionModel ss = new SessionModel();
                            //    ss.name = "Liability";
                            //    ss.UnitId = null;
                            //    ss.ProfileId = null;
                            //    values.Add(ss);
                            //}
                            Session["Policyinclustions"] = values;
                            Session["unId"] = null;
                            Session["profileId"] = null;
                        }
                    }
                    if (Prid.HasValue && Prid == 1029)
                    {
                        if (unit == "Home Buildings")
                        {
                            Session["HprofileId"] = null;
                            Session["Home2"] = 1;
                            return Json(Url.Action("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = pcid }));
                            // return RedirectToAction("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = pcid });
                        }
                        else if (unit == "Home Content" || unit == "Home Contents")
                        {
                            Session["profileId"] = Session["HprofileId"].ToString();
                            return Json(Url.Action("HomeContent", "HomeContentValuable", new { cid = cid, PcId = pcid }));
                            return RedirectToAction("HomeContent", "HomeContentValuable", new { cid = cid, PcId = pcid });
                        }
                        else if (unit == "Valuables")
                        {
                            Session["profileId"] = Session["HprofileId"].ToString();
                            return Json(Url.Action("Valuables", "HomeContentValuable", new { cid = cid, PcId = pcid }));
                            return RedirectToAction("Valuables", "HomeContentValuable", new { cid = cid, PcId = pcid });
                        }
                        else if (unit == "Farm Property")
                        {
                            return Json(Url.Action("FarmContents", "Farm", new { cid = cid, PcId = pcid }));
                            return RedirectToAction("FarmContents", "Farm", new { cid = cid, PcId = pcid });
                        }
                        else if (unit == "Liability")
                        {
                            return Json(Url.Action("LiabilityCover", "Liabilities", new { cid = cid, PcId = pcid }));
                            return RedirectToAction("LiabilityCover", "Liabilities", new { cid = cid, PcId = pcid });
                        }
                        else if (unit == "Pets" || unit == "Pet")
                        {
                            Session["profileId"] = 0;
                            return Json(Url.Action("PetsCover", "Pets", new { cid = cid, PcId = pcid }));
                            return RedirectToAction("PetsCover", "Pets", new { cid = cid, PcId = pcid });
                        }
                        else if (unit == "Motor" || unit == "Motors")
                        {
                            Session["profileId"] = 0;
                            return Json(Url.Action("VehicleDescription", "MotorCover", new { cid = cid, PcId = pcid }));
                            return RedirectToAction("VehicleDescription", "MotorCover", new { cid = cid, PcId = pcid });
                        }
                        else if (unit == "Boat")
                        {
                            Session["profileId"] = 0;
                            return Json(Url.Action("BoatDetails", "Boat", new { cid = cid, PcId = pcid }));
                            return RedirectToAction("BoatDetails", "Boat", new { cid = cid, PcId = pcid });
                        }
                        else if (unit == "Travel")
                        {
                            return Json(Url.Action("TravelCover", "Travel", new { cid = cid, PcId = pcid }));
                            return RedirectToAction("TravelCover", "Travel", new { cid = cid, PcId = pcid });
                        }
                    }
                    else if (Prid != null && Prid.HasValue && Prid == 1021)
                    {
                        if (unit == "Fixed Farm Property")
                        {
                            return Json(Url.Action("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = pcid }));
                        }
                        else if (unit == "Mobile Farm Property")
                        {
                            return Json(Url.Action("FarmContents", "MobileFarm", new { cid = cid, PcId = pcid }));
                        }
                        else if (unit == "Farm Interuption")
                        {
                            return Json(Url.Action("FarmInterruption", "FarmPolicyFarmInterruption", new { cid = cid, PcId = pcid }));
                        }
                        else if (unit == "Farm Liability")
                        {
                            return Json(Url.Action("FarmLiability", "FarmPolicyFarmLiability", new { cid = cid, PcId = pcid }));
                        }
                        else if (unit == "Burglary")
                        {
                            return Json(Url.Action("Burglary", "FarmPolicyBurglary", new { cid = cid, PcId = pcid }));
                        }
                        else if (unit == "Electronics")
                        {
                            return Json(Url.Action("Electronics", "FarmPolicyElectronics", new { cid = cid, PcId = pcid }));
                        }
                        else if (unit == "Money")
                        {
                            return Json(Url.Action("Money", "FarmPolicyMoney", new { cid = cid, PcId = pcid }));
                        }
                        else if (unit == "Transit")
                        {
                            return Json(Url.Action("Transit", "FarmPolicyTransit", new { cid = cid, PcId = pcid }));
                        }
                        else if (unit == "Valuables")
                        {
                            return Json(Url.Action("Valuables", "FarmPolicyValuables", new { cid = cid, PcId = pcid }));
                        }
                        else if (unit == "LiveStock")
                        {
                            return Json(Url.Action("Livestock", "FarmPolicyLivestock", new { cid = cid, PcId = pcid }));
                        }
                        else if (unit == "Personal Liabilities Farm")
                        {
                            return Json(Url.Action("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid, PcId = pcid }));
                        }
                        else if (unit == "Home Buildings")
                        {
                            return Json(Url.Action("MainDetails", "FarmPolicyHome", new { cid = cid, PcId = pcid }));
                        }
                        else if (unit == "Home Contents")
                        {
                            return Json(Url.Action("HomeContents", "FarmPolicyHomeContent", new { cid = cid, PcId = pcid }));
                        }
                        else if (unit == "Machinery")
                        {
                            return Json(Url.Action("Machinery", "FarmPolicyMachinery", new { cid = cid, PcId = pcid }));
                        }
                        else if (unit == "Motor")
                        {
                            return Json(Url.Action("VehicleDescription", "FarmPolicyMotor", new { cid = cid, PcId = pcid }));
                        }
                    }
                }
                else
                {
                    return Json(Url.Action("AgentLogin", "Login"));
                }
            }
            return Json(new
            {
                Status = false
            });
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> DeleteUnit(int? unid, int? cid, int? pcid, string unit, int? profileid, int? trid, string policystatus, int? unitid)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["ApiKey"] != null)
                {
                    var db = new MasterDataEntities();
                    ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
                    string apikey = Session["ApiKey"].ToString();
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (pcid != null && pcid.HasValue && pcid > 0)
                    {
                        HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                        HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=Delete&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                        HttpResponseMessage savepolicy = await hclient.GetAsync("PolicyDetails?apiKey=" + apikey + "&action=Save&pcId=&trId&prId=&InsuredId=&effectiveDate=&reason=");
                        var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                        if (EmpResponse != null)
                        {
                            unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                            if (unitdetails.Status == "Success")
                            {
                                int delete = db.IT_dt_Delete_Unit(unitid, null, null, null, unid, null, null, null, null);
                                if (delete == 1)
                                {
                                    return Json(new
                                    {
                                        Status = true
                                    });
                                }
                                else
                                {
                                    return Json(new
                                    {
                                        Status = false
                                    });
                                }
                            }
                        }
                    }
                    else //Adding to Session 
                    {
                        if (Session["Policyinclustions"] != null)
                        {
                            List<SessionModel> values = new List<SessionModel>();
                            values = (List<SessionModel>)Session["Policyinclustions"];
                            //Add new one
                            if (unid != null && profileid != null)
                            {
                                HttpResponseMessage Res = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=Existing&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                                HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=Delete&SectionName=&SectionUnId=" + unid + "&ProfileUnId=" + profileid);
                                HttpResponseMessage savepolicy = await hclient.GetAsync("PolicyDetails?apiKey=" + apikey + "&action=Save&pcId=&trId&prId=&InsuredId=&effectiveDate=&reason=");
                                var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                                if (EmpResponse != null)
                                {
                                    unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                                    if (unitdetails.Status == "Success")
                                    {

                                    }
                                }
                            }
                            if (values.Exists(p => p.name == unit && p.UnitId == unid && p.ProfileId == profileid))
                            {
                                var valueselected = values.Where(p => p.name == unit && p.UnitId == unid && p.ProfileId == profileid).First();
                                values.Remove(valueselected);
                            }
                            Session["Policyinclustions"] = values;
                            return Json(new
                            {
                                Status = true
                            });
                        }
                    }
                }
                else
                {
                    return RedirectToAction("AgentLogin", "Login");
                }
            }
            return Json(new
            {
                Status = false
            });
        }

        #endregion

        [HttpPost]
        #region AJAX call for Endorsing Policy
        public async System.Threading.Tasks.Task<ActionResult> ViewSection(string name, int? PcId, int? cid, int? trId, int? unid, int? profileid, string policyStatus, int prId, int unitid)
        {
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            try
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["ApiKey"] != null)
                    {
                        Session["UnitId"] = unitid;
                        Session["unId"] = unid;
                        Session["profileId"] = profileid;
                        // Session["UnitId"] = unitid;
                        if (prId != null && prId > 0 && prId == 1029)
                        {
                            if (name == "Home" || name == "Home Buildings")
                            {
                                return Json(Url.Action("HomeBilding", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Home Content" || name == "Home Contents")
                            {
                                return Json(Url.Action("HomeContents", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Valuables")
                            {
                                return Json(Url.Action("Valuables", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Farm Property")
                            {
                                return Json(Url.Action("FarmProperty", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Liability")
                            {
                                return Json(Url.Action("Liability", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Pets")
                            {
                                return Json(Url.Action("Pets", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Motor")
                            {
                                return Json(Url.Action("Motors", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Boat")
                            {
                                return Json(Url.Action("Boat", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Travel")
                            {
                                return Json(Url.Action("Travels", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                        }
                        else if (prId != null && prId > 0 && prId == 1021)
                        {
                            if (name == "Home Buildings")
                            {
                                return Json(Url.Action("FarmHomeBilding", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Home Content" || name == "Home Contents")
                            {
                                return Json(Url.Action("FarmHomeContents", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Valuables")
                            {
                                return Json(Url.Action("FarmValuables", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Fixed Farm Property")
                            {
                                return Json(Url.Action("FixedFarmProperty", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Personal Liability")
                            {
                                return Json(Url.Action("PersonalLiability", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Livestock")
                            {
                                return Json(Url.Action("FarmLivestock", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Motor")
                            {
                                return Json(Url.Action("FarmMotors", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Mobile Farm Property")
                            {
                                return Json(Url.Action("MobileFarmProperty", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Farm Liability")
                            {
                                return Json(Url.Action("FarmLiability", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Burglary")
                            {
                                return Json(Url.Action("Burglary", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Farm Electronics")
                            {
                                return Json(Url.Action("FarmElectronics", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Farm Interruption")
                            {
                                return Json(Url.Action("FarmInterruption", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Machinery")
                            {
                                return Json(Url.Action("FarmMachinery", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Farm Money")
                            {
                                return Json(Url.Action("FarmMoney", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Transit")
                            {
                                return Json(Url.Action("FarmTransit", "PolicyDetails", new { cid = cid, PcId = PcId }));
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Status = false

                });
            }
            return Json(new
            {
                Status = false

            });
        }

        #endregion

        [HttpPost]
        #region AJAX call for Endorsing Policy
        public async System.Threading.Tasks.Task<ActionResult> EndorsePolicy(string name, int? PcId, int? cid, int? trId, int? unid, int? profileid, string policyStatus, int prId, int? calltype, string effcdate)
        {
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            try
            {
                if (Request.IsAjaxRequest())
                {
                    if (Session["ApiKey"] != null)
                    {
                        var db = new MasterDataEntities();
                        string apikey = Session["ApiKey"].ToString();
                        if (policyStatus == "NP" || policyStatus == "AP" || policyStatus == "RP")
                        {

                        }
                        else
                        {
                            Session["unId"] = unid;
                            Session["profileId"] = profileid;
                        }
                        if (calltype == null && policyStatus == "NP" || policyStatus == "AP" || policyStatus == "RP")
                        {
                            HttpClient hclient = new HttpClient();
                            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                            hclient.BaseAddress = new Uri(url);
                            var dateAndTime = DateTime.UtcNow;
                            var date = dateAndTime.Date.ToString("yyyy-MM-dd");
                            if (effcdate != null)
                            {
                                date = effcdate;
                            }
                            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage endorse = await hclient.GetAsync("PolicyDetails?apiKey=" + apikey + "&action=Endorse&pcId=" + PcId + "&trId=" + trId
                                + "&prId=&InsuredId=&effectiveDate=" + date + "&reason=");/*insureddetails.InsuredID */
                            var EmpResponse = endorse.Content.ReadAsStringAsync().Result;
                            if (EmpResponse != null)
                            {
                                unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                                if (unitdetails.Status == "Success")
                                {
                                    Session["ApiKey"] = unitdetails.ApiKey;
                                }
                                if (unitdetails.ErrorMessage != null && unitdetails.ErrorMessage.Count() > 0)
                                {
                                    return Json(new
                                    {
                                        Status = false,
                                        error = "unitdetails.ErrorMessage"

                                    });
                                }
                                if (unitdetails.UnitData != null)
                                {
                                    if (unitdetails.UnitData.Exists(p => p.Name == "Home Buildings"))
                                    {
                                        Session["unId"] = unitdetails.UnitData.Where(p => p.Name == "Home Buildings").Select(p => p.UnId).FirstOrDefault();
                                        Session["profileId"] = unitdetails.UnitData.Where(p => p.Name == "Home Buildings").Select(p => p.ProfileUnId).FirstOrDefault();
                                        Session["HprofileId"] = unitdetails.UnitData.Where(p => p.Name == "Home Buildings").Select(p => p.ProfileUnId).FirstOrDefault();
                                    }
                                }
                            }
                            if (prId != null && prId > 0 && prId == 1029)
                            {
                                return Json(Url.Action("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = PcId }));
                            }
                            else if (prId != null && prId > 0 && prId == 1021)
                            {
                                return Json(Url.Action("FarmLocationDetails", "FarmPolicyProperty", new { cid = cid, PcId = PcId }));
                            }
                        }
                        if ((unitdetails != null && unitdetails.PolicyData != null && unitdetails.PolicyData.PrId == (int)PolicyType.RLS || (prId != null && prId == 1029)))//unitdetails != null && unitdetails.PolicyData != null &&
                        {
                            Session["unId"] = unid;
                            Session["profileId"] = profileid;
                            if (name == "Home")
                            {
                                return Json(Url.Action("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = PcId }));
                                return RedirectToAction("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = PcId });
                            }
                            if (name == "Home Buildings")
                            {
                                return Json(Url.Action("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = PcId }));
                                return RedirectToAction("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = PcId });
                            }
                            else if (name == "Home Content" || name == "Home Contents")
                            {
                                return Json(Url.Action("HomeContent", "HomeContentValuable", new { cid = cid, PcId = PcId }));
                                return RedirectToAction("HomeContent", "HomeContentValuable", new { cid = cid, PcId = PcId });
                            }
                            else if (name == "Valuables")
                            {
                                return Json(Url.Action("Valuables", "HomeContentValuable", new { cid = cid, PcId = PcId }));
                                return RedirectToAction("Valuables", "HomeContentValuable", new { cid = cid, PcId = PcId });
                            }
                            else if (name == "Farm Property")
                            {
                                return Json(Url.Action("FarmContents", "Farm", new { cid = cid, PcId = PcId }));
                                return RedirectToAction("FarmContents", "Farm", new { cid = cid, PcId = PcId });
                            }
                            else if (name == "Liability")
                            {
                                return Json(Url.Action("LiabilityCover", "Liabilities", new { cid = cid, PcId = PcId }));
                                return RedirectToAction("LiabilityCover", "Liabilities", new { cid = cid, PcId = PcId });
                            }
                            else if (name == "Pets" || name == "Pet")
                            {
                                return Json(Url.Action("PetsCover", "Pets", new { cid = cid, PcId = PcId }));
                                return RedirectToAction("PetsCover", "Pets", new { cid = cid, PcId = PcId });
                            }
                            else if (name == "Motor" || name == "Motors")
                            {
                                return Json(Url.Action("VehicleDescription", "MotorCover", new { cid = cid, PcId = PcId }));
                                return RedirectToAction("VehicleDescription", "MotorCover", new { cid = cid, PcId = PcId });
                            }
                            else if (name == "Boat")
                            {
                                return Json(Url.Action("BoatDetails", "Boat", new { cid = cid, PcId = PcId }));
                                return RedirectToAction("BoatDetails", "Boat", new { cid = cid, PcId = PcId });
                            }
                            else if (name == "Travel")
                            {
                                return Json(Url.Action("TravelCover", "Travel", new { cid = cid, PcId = PcId }));
                                return RedirectToAction("TravelCover", "Travel", new { cid = cid, PcId = PcId });
                            }
                        }
                        else if (unitdetails != null && unitdetails.PolicyData != null && unitdetails.PolicyData.PrId == (int)PolicyType.FarmPolicy || (prId != null && prId == 1021))
                        {
                            Session["unId"] = unid;
                            Session["profileId"] = profileid;
                            if (name == "Home Buildings")
                            {
                                return Json(Url.Action("MainDetails", "FarmPolicyHome", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Home Content" || name == "Home Contents")
                            {
                                return Json(Url.Action("HomeContent", "FarmPolicyHomeContent", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Valuables")
                            {
                                return Json(Url.Action("Valuables", "FarmPolicyValuables", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Fixed Farm Property")
                            {
                                return Json(Url.Action("FarmDetails", "FarmPolicyProperty", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Personal Liability")
                            {
                                return Json(Url.Action("PersonalLiability", "FarmPolicyPersonalLiability", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Livestock")
                            {
                                return Json(Url.Action("Livestock", "FarmPolicyLivestock", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Motor")
                            {
                                return Json(Url.Action("VehicleDescription", "FarmMotors", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Mobile Farm Property")
                            {
                                return Json(Url.Action("FarmContents", "MobileFarm", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Farm Liability")
                            {
                                return Json(Url.Action("FarmLiability", "FarmPolicyFarmLiability", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Burglary")
                            {
                                return Json(Url.Action("Burglary", "FarmPolicyBurglary", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Electronics")
                            {
                                return Json(Url.Action("Electronics", "FarmPolicyElectronics", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Farm Interuption")
                            {
                                return Json(Url.Action("FarmInterruption", "FarmPolicyFarmInterruption", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Machinery")
                            {
                                return Json(Url.Action("Machinery", "FarmPolicyMachinery", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Farm Money")
                            {
                                return Json(Url.Action("Money", "FarmPolicyMoney", new { cid = cid, PcId = PcId }));
                            }
                            else if (name == "Transit")
                            {
                                return Json(Url.Action("Transit", "FarmPolicyTransit", new { cid = cid, PcId = PcId }));
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("AgentLogin", "Login");
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Status = false

                });

            }
            return Json(new
            {
                Status = false

            });
        }

        #endregion

        #region AJAX call for Cancel Policy
        public async System.Threading.Tasks.Task<ActionResult> CancelPolicy(int PcId, int cid, string trId, string Reson)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["ApiKey"] != null)
                {
                    var db = new MasterDataEntities();
                    ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    string apikey = Session["ApiKey"].ToString();
                    if (Reson != null && !string.IsNullOrEmpty(Reson))
                    {

                    }
                    else
                    {
                        Reson = "Nothing";
                    }
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage cancelpolicy = await hclient.GetAsync("PolicyDetails?apiKey=" + apikey + "&action=Cancel&pcId=" + PcId + "&trId&prId=&InsuredId=&effectiveDate=" + DateTime.UtcNow.ToString("dd-MM-yyyy hh:mm:ss.fff") + "&reason=" + Reson);
                    HttpResponseMessage savepolicy = await hclient.GetAsync("PolicyDetails?apiKey=" + apikey + "&action=Save&pcId=&trId&prId=&InsuredId=&effectiveDate=&reason=");
                    var EmpResponse = cancelpolicy.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails.Status == "Success")
                        {
                            Session["ApiKey"] = unitdetails.ApiKey;
                        }
                        if (unitdetails.PolicyData.Status == "CP")
                        {
                            return Json(Url.Action("InsuredPolicys", "Customer", new { cid = cid }));
                            return Json(new
                            {
                                Status = "Success"
                            });
                        }
                        else
                        {
                            return Json(new
                            {
                                Status = "Failure",
                                message = "Failed to cancel the policy, please try again."
                            });
                        }
                    }
                }
                else
                {
                    return RedirectToAction("AgentLogin", "Login");
                }
            }
            return Json(new
            {
                Status = false
            });
        }

        #endregion

        #region AJAX Action to Delete Section
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> DeleteSection(int cid, int unid)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["ApiKey"] != null)
                {
                    var db = new MasterDataEntities();
                    ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    string apikey = Session["ApiKey"].ToString();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage deleteS = await hclient.GetAsync("UnitDetails?apiKey=" + apikey + "&Action=Delete&SectionName=&SectionUnId=" + unid + "&ProfileUnId=");
                    var EmpResponse = deleteS.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails.Status == "Success")
                        {
                            Session["ApiKey"] = unitdetails.ApiKey;
                        }
                        //if (unitdetails. == "CP")
                        //{
                        //    return Json(new
                        //    {
                        //        Status = "Success"
                        //    });
                        //}
                        //else
                        //{
                        //    return Json(new
                        //    {
                        //        Status = "Failure",
                        //        message = "Failed to delete section, please try again."
                        //    });
                        //}
                    }
                }
                else
                {
                    return RedirectToAction("AgentLogin", "Login");
                }
            }
            return Json(new
            {
                Status = false
            });
        }

        #endregion

        #region AJAX Action to Bind Quotation GET
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> BindCover(int? cid, int? PcId)
        {
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            if (Session["ApiKey"] != null)
            {
                var db = new MasterDataEntities();
                HttpClient hclient = new HttpClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                hclient.BaseAddress = new Uri(url);
                string apikey = Session["ApiKey"].ToString();
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage bindcover = await hclient.GetAsync("PolicyDetails?apiKey=" + apikey + "&action=Save&pcId=&trId=&prId=&InsuredId=&effectiveDate=&reason=");
                var EmpResponse = bindcover.Content.ReadAsStringAsync().Result;
                if (EmpResponse != null)
                {
                    unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                    if (unitdetails.Status == "Success")
                    {
                        Session["ApiKey"] = unitdetails.ApiKey;
                    }
                    if (cid != null)
                    {
                        ViewBag.cid = cid;
                        unitdetails.CustomerId = cid.Value;
                    }
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            return RedirectToAction("PolicySuccess", "Customer");
        }

        #endregion

        #region AJAX Action to Bind Quotation
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> BindCover(int cid, int unid)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["ApiKey"] != null)
                {
                    var db = new MasterDataEntities();
                    ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    string apikey = Session["ApiKey"].ToString();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage deleteS = await hclient.GetAsync("PolicyDetails?apiKey=" + apikey + "&action=Bind&pcId=&trId=&prId=&InsuredId=&effectiveDate=&reason=");
                    var EmpResponse = deleteS.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails.Status == "Success")
                        {
                            Session["ApiKey"] = unitdetails.ApiKey;
                        }
                        //if (unitdetails. == "CP")
                        //{
                        //    return Json(new
                        //    {
                        //        Status = "Success"
                        //    });
                        //}
                        //else
                        //{
                        //    return Json(new
                        //    {
                        //        Status = "Failure",
                        //        message = "Failed to delete section, please try again."
                        //    });
                        //}
                        return Json(new
                        {
                            Status = true
                        });
                    }
                }
                else
                {
                    return RedirectToAction("AgentLogin", "Login");
                }
            }

            return Json(new
            {
                Status = false
            });
        }

        #endregion

        #region AJAX Action to Reffered
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Reffer()
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["ApiKey"] != null)
                {
                    var db = new MasterDataEntities();
                    ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    string apikey = Session["ApiKey"].ToString();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage reffer = await hclient.GetAsync("PolicyDetails?apiKey=" + apikey + "&action=Refer&pcId=&trId=&prId=&InsuredId=&effectiveDate=&reason=");
                    var EmpResponse = reffer.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails.Status == "Success")
                        {
                            Session["ApiKey"] = unitdetails.ApiKey;
                            return Json(new
                            {
                                Status = true
                            });
                        }
                        else
                        {
                            return Json(new
                            {
                                Status = false
                            });
                        }
                    }
                }
                else
                {
                    return Json(Url.Action("AgentLogin", "Login"));
                    return RedirectToAction("AgentLogin", "Login");
                }
            }

            return Json(new
            {
                Status = false
            });
        }

        #endregion

        #region Policy Success
        public ActionResult PolicySuccess()
        {
            return View();
        }
        #endregion

        #region Action Add Home Profile
        public async System.Threading.Tasks.Task<ActionResult> AddHomeProfile(string Add, string suburb, string state, string postcode)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["ApiKey"] != null)
                {
                    var db = new MasterDataEntities();
                    HomeProfile homeprofile = new HomeProfile();
                    string apikey = Session["ApiKey"].ToString();
                    if (Session["HprofileId"] != null)
                    {
                        int Unid = Convert.ToInt32(Session["HprofileId"]);
                        homeprofile.UnId = Unid;
                        if (Session["ProfileId"] != null)
                        {
                            int? profileid = Convert.ToInt32(Session["ProfileId"]);
                            if (Session["MprofileId"] != null && profileid == 0)
                            {
                                homeprofile.UnId = Convert.ToInt32(Session["MprofileId"]);
                            }
                        }
                    }
                    if (Session["FprofileId"] != null)
                    {
                        int Unid = Convert.ToInt32(Session["FprofileId"]);
                        homeprofile.UnId = Unid;
                        if (Session["ProfileId"] != null)
                        {
                            int? profileid = Convert.ToInt32(Session["ProfileId"]);
                        }
                    }
                    homeprofile.ApiKey = Session["ApiKey"].ToString();
                    homeprofile.AddressLine = Add;
                    homeprofile.Suburb = suburb.Trim();
                    homeprofile.State = state.Trim();
                    homeprofile.Postcode = postcode.Trim();
                    homeprofile.AddressID = 0;
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(JsonConvert.SerializeObject(homeprofile), Encoding.UTF8, "application/json");
                    var responses = await hclient.PostAsync("UnitDetails", content);
                    var result = await responses.Content.ReadAsStringAsync();
                    HomeProfile homepr = new HomeProfile();
                    if (result != null)
                    {
                        homepr = JsonConvert.DeserializeObject<HomeProfile>(result);
                        if (homepr != null)
                        {

                        }
                    }
                    return Json(new
                    {
                        Status = true,
                        addressId = homepr.AddressData.AddressID
                    });
                }
                else
                {
                    return RedirectToAction("AgentLogin", "Login");
                }
            }
            return Json(new
            {
                Status = false
            });
        }
        #endregion

        #region
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> CustomerRegistration(int? cid, int? type, int? InsuredId, string actions)
        {
            InsuredDetails model = new InsuredDetails();
            if (actions != null && (actions == "attach" || actions == "attachs"))
            {
                if (actions == "attach")
                {
                    Session["InsuredId"] = null;
                }
                if (actions == "attachs")
                {
                    Session["newattach"] = "newcus";
                }
                Session["EmailId"] = null;
                Session["Actn"] = "New";
            }
            model.PolicyType = type;
            InsuredDetailsSerobj obj = new InsuredDetailsSerobj();
            string ApiKey = null;
            model.CustomerId = cid;
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            if (Session["ApiKey"] != null)
            {
                ApiKey = Session["ApiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (Session["InsuredId"] != null)
            {
                int? InsuredI = Convert.ToInt32(Session["InsuredId"]);
                HttpResponseMessage insured = await hclient.GetAsync("InsuredDetails?ApiKey=" + ApiKey + "&Action=Retrieve&InsuredID=" + InsuredI);
            }
            if (Session["EmailId"] != null)
            {
                model.EmailID = Session["EmailId"].ToString();
                if (Session["InsuredId"] != null)
                {
                    model.InsuredID = Convert.ToInt32(Session["InsuredId"]);
                }
            }
            if (Session["InsuredName"] != null)
            {
                model.FirstName = Session["InsuredName"].ToString();
                if (Session["InsuredId"] != null)
                {
                    model.InsuredID = Convert.ToInt32(Session["InsuredId"]);
                }
            }
            else
            {
                model.FirstName = null;
            }
            GetInsuredDetailsRef refmodel = new GetInsuredDetailsRef();
            refmodel.AddressData = new List<AddressData>();
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (model.EmailID != null || model.FirstName != null)
            {
                //On Submit
                HttpResponseMessage Ress = await hclient.GetAsync("InsuredDetails?ApiKey=" + ApiKey + "&iyId=" + 9262 + "&policyNo=&insuredName=" + model.FirstName + "&emailId=" + model.EmailID + "&phoneNo=");
                if (Ress.IsSuccessStatusCode)
                {
                    var EmpResponse = Ress.Content.ReadAsStringAsync().Result;
                    refmodel = JsonConvert.DeserializeObject<GetInsuredDetailsRef>(EmpResponse);
                    if (refmodel != null)
                    {
                        model.PolicyType = type.Value;
                        model = refmodel.Insureds.First();
                        if (refmodel.AddressData != null && refmodel.AddressData.Count() > 0)
                        {
                            model.AddressDatas = refmodel.AddressData.First();
                        }
                        else
                        {
                            TempData["ErrorMsg"] = "Address is not provided for this insurer.";
                            return RedirectToAction("AdvancedCustomerSearch", "Customer");
                        }
                        model.state = model.AddressDatas.State;
                        model.suburb = model.AddressDatas.Suburb;
                        model.Address = model.AddressDatas.AddressLine1;
                        model.AddressID = model.AddressDatas.AddressID;
                        model.postcode = model.AddressDatas.Postcode;
                        model.Title = refmodel.Insureds.Select(p => p.Title).FirstOrDefault();
                        if (refmodel.PostalAddressData != null && refmodel.PostalAddressData.Count() > 0)
                        {
                            model.PostalState = refmodel.PostalAddressData.Select(p=>p.State).FirstOrDefault();
                            model.PostalSuburb = refmodel.PostalAddressData.Select(p => p.Suburb).FirstOrDefault();
                            model.PostalAddressLine = refmodel.PostalAddressData.Select(p => p.AddressLine1).FirstOrDefault();
                            model.PostalAddressID = refmodel.PostalAddressData.Select(p => p.AddressID).FirstOrDefault().ToString();
                            model.PostalPostcode = refmodel.PostalAddressData.Select(p => p.Postcode).FirstOrDefault();
                        }
                        else
                        {
                            TempData["ErrorMsg"] = "Address is not provided for this insurer.";
                            return RedirectToAction("AdvancedCustomerSearch", "Customer");
                        }                    
                    }
                }
                obj.ABN = model.ABN;
                obj.Title = model.Title;
                obj.AddressLine = model.Address;
                obj.ApiKey = model.ApiKey;
                obj.ClientType = model.ClientType;
                obj.CompanyBusinessName = model.CompanyBusinessName;
                obj.CustomerId = model.CustomerId;
                obj.DOB = model.DOB;
                obj.EmailID = model.EmailID;
                obj.FirstName = model.FirstName;
                obj.LastName = model.LastName;
                obj.MiddleName = model.MiddleName;
                obj.MobileNo = model.MobileNo;
                obj.PhoneNo = model.PhoneNo;
                obj.PolicyNumbers = model.PolicyNumbers;
                obj.postcode = model.postcode;
                obj.suburb = model.suburb;
                obj.state = model.state;
                obj.Status = model.Status;
                obj.PostalState=model.PostalState;
                obj.PostalSuburb=model.PostalSuburb;
                obj.PostalAddressLine=model.PostalAddressLine;
                obj.PostalAddressLine=model.PostalAddressID;
                obj.PostalPostcode = model.PostalPostcode;
                if (obj.suburb != null && obj.state != null && obj.postcode != null)
                {
                    obj.suburb = obj.suburb + ", " + obj.state + ", " + obj.postcode;
                }
                if (obj.PostalSuburb != null && obj.PostalState != null && obj.PostalPostcode != null)
                {
                    obj.PostalSuburb = obj.PostalSuburb + ", " + obj.PostalState + ", " + obj.PostalPostcode;
                }
                obj.TradingName = model.TradingName;
            }
            obj.PolicyType = model.PolicyType ?? type;
            return View(obj);
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CustomerRegistration(InsuredDetailsSerobj model)
        {
            string apikey = null;
            int? cid = 0;
            if (model.suburb != null)
            {
                string[] sublist = model.suburb.Split(',');
                if (sublist[0] != null && sublist.Count() > 1)
                {
                    model.suburb = sublist[0].Trim();
                    model.state = sublist[1].Trim();
                    model.postcode = sublist[2].Trim();
                }
                else
                {
                    string postcode = model.suburb.Substring(model.suburb.Length - 4);
                    model.postcode = postcode.Trim();
                    string remainingstr = model.suburb.Substring(0, model.suburb.Length - 4).TrimEnd();
                    string state = remainingstr.Substring(remainingstr.Length - 3);
                    model.state = state.Trim();
                    string remaining = remainingstr.Substring(0, remainingstr.Length - 3).TrimEnd();
                    model.suburb = remaining.Trim();
                }
            }
            if (model.PostalSuburb != null)
            {
                string[] psublist = model.PostalSuburb.Split(',');
                if (psublist[0] != null && psublist.Count() > 1)
                {
                    model.PostalSuburb = psublist[0].Trim();
                    model.PostalState = psublist[1].Trim();
                    model.PostalPostcode = psublist[2].Trim();
                }
                else
                {
                    string ppostcode = model.PostalSuburb.Substring(model.PostalSuburb.Length - 4);
                    model.PostalPostcode = ppostcode.Trim();
                    string remainingstrs = model.PostalSuburb.Substring(0, model.PostalSuburb.Length - 4).TrimEnd();
                    string states = remainingstrs.Substring(remainingstrs.Length - 3);
                    model.PostalState = states.Trim();
                    string remainings = remainingstrs.Substring(0, remainingstrs.Length - 3).TrimEnd();
                    model.PostalSuburb = remainings.Trim();
                }
            }
            if (model.DOB != null)
            {

            }
            else
            {
                model.DOB = DateTime.UtcNow;
            }
            if (Session["ApiKey"] != null)
            {
                apikey = Session["ApiKey"].ToString();
                GetInsuredDetailsRef refmodel = new GetInsuredDetailsRef();
                refmodel.AddressData = new List<AddressData>();
                if (model != null)
                {
                    var db = new MasterDataEntities();
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    model.ApiKey = apikey;
                    if (Session["InsuredId"] != null && (Session["InsuredName"] != null || Session["EmailId"] != null))
                    {
                        model.InsuredID = Convert.ToInt32(Session["InsuredId"]);
                        cid = model.CustomerId;
                    }
                    else
                    {
                        model.AddressID = 0;
                        model.InsuredID = -1;
                    }
                    if (model.ABN != null && !string.IsNullOrEmpty(model.ABN))
                    {
                        #region ABN Validation
                        if (model.ABN.Length == 11)
                        {
                            char[] ABN = model.ABN.ToCharArray();

                            string message = model.ABN;
                            string[] result11 = new string[message.Length];
                            char[] temp = new char[message.Length];
                            int SumOfABN = 0;
                            int DivideBy = 89;
                            int Reminder = 0;
                            List<int> ABNList = new List<int>();
                            temp = message.ToCharArray();
                            int abnfrst = 0;
                            for (int i = 0; i < message.Length; i++)
                            {
                                int abnfrst1 = 0;
                                int abnfrst2 = 0;
                                result11[i] = Convert.ToString(temp[i]);
                                if (i == 0)
                                {
                                    abnfrst = Convert.ToInt32(result11[i]);
                                    abnfrst1 = abnfrst - 1;
                                    abnfrst2 = abnfrst1 * 10;
                                }
                                if (i == 1)
                                {
                                    abnfrst = Convert.ToInt32(result11[i]);

                                    abnfrst2 = abnfrst * 1;
                                }
                                if (i == 2)
                                {
                                    abnfrst = Convert.ToInt32(result11[i]);

                                    abnfrst2 = abnfrst * 3;
                                }
                                if (i == 3)
                                {
                                    abnfrst = Convert.ToInt32(result11[i]);

                                    abnfrst2 = abnfrst * 5;
                                }
                                if (i == 4)
                                {
                                    abnfrst = Convert.ToInt32(result11[i]);

                                    abnfrst2 = abnfrst * 7;
                                }
                                if (i == 5)
                                {
                                    abnfrst = Convert.ToInt32(result11[i]);

                                    abnfrst2 = abnfrst * 9;
                                }
                                if (i == 6)
                                {
                                    abnfrst = Convert.ToInt32(result11[i]);

                                    abnfrst2 = abnfrst * 11;
                                }
                                if (i == 7)
                                {
                                    abnfrst = Convert.ToInt32(result11[i]);

                                    abnfrst2 = abnfrst * 13;
                                }
                                if (i == 8)
                                {
                                    abnfrst = Convert.ToInt32(result11[i]);

                                    abnfrst2 = abnfrst * 15;
                                }
                                if (i == 9)
                                {
                                    abnfrst = Convert.ToInt32(result11[i]);

                                    abnfrst2 = abnfrst * 17;
                                }
                                if (i == 10)
                                {
                                    abnfrst = Convert.ToInt32(result11[i]);

                                    abnfrst2 = abnfrst * 19;
                                }
                                ABNList.Add(abnfrst2);
                            }
                            if (ABNList != null && ABNList.Count > 0)
                            {
                                SumOfABN = ABNList.Sum(x => Convert.ToInt32(x));
                            }
                            if (SumOfABN > 0)
                            {
                                Reminder = SumOfABN % DivideBy;
                            }
                            if (Reminder == 0)
                            {

                            }
                            else
                            {
                                ViewBag.Error = "ABN is not valid";
                                return View(model);
                            }
                        }
                        #endregion
                    }
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    AddressData ad = new AddressData();
                    // model.AddressData = model.suburb + model.state + model.postcode;
                    DataTable dt = new DataTable();
                    Type objectType = typeof(InsuredDetailsSerobj);
                    // Loop over properties.
                    foreach (System.Reflection.PropertyInfo property in objectType.GetProperties())
                    {
                        if (property.GetValue(model, null) == null)
                        {
                            if (property.PropertyType.Name == "String")
                                property.SetValue(model, "", null);
                            else if (property.PropertyType.Name == "Nullable`1")
                                property.SetValue(model, 0, null);
                            else if (property.PropertyType.Name == "DateTime")
                                property.SetValue(model, new List<string>(), null);
                            else if (property.PropertyType.Name == "List`1")
                                property.SetValue(model, new List<string>(), null);
                        }
                    }
                    GetInsuredDetailsRef modells = new GetInsuredDetailsRef();
                    modells.AddressData = new List<AddressData>();
                    modells.InsuredData = new InsuredDetails();
                    modells.Insureds = new List<InsuredDetails>();
                    StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    var responses = await hclient.PostAsync("InsuredDetails", content);
                    var result = await responses.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        modells = JsonConvert.DeserializeObject<GetInsuredDetailsRef>(result);
                        if (modells.Status != "Failure")
                        {
                            if (Session["CustomerType"] != null && Session["CustomerType"] == "New")
                            {
                                HttpResponseMessage attach = await hclient.GetAsync("InsuredDetails?apiKey=" + apikey + "&Action=Attach&InsuredId=" + modells.InsuredData.InsuredID);
                                var results = await responses.Content.ReadAsStringAsync();
                            }
                            if (modells != null && modells.InsuredData.InsuredID != null && (modells.InsuredData.InsuredID < 0 || modells.InsuredData.InsuredID > 0))
                            {
                                int? customerid = db.IT_InsertCustomerMaster(model.EmailID, modells.InsuredData.InsuredID, model.PolicyNumbers.FirstOrDefault(), null, model.FirstName, null).SingleOrDefault();
                                cid = customerid.Value;
                                Session["cid"] = cid.Value;
                            }
                        }
                        else if (modells.Status == "Failure")
                        {
                            return ViewBag(modells);
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (Session["newattach"] != null && Session["newattach"] == "newcus")
            {
                return RedirectToAction("CustomerRegistration", "Customer", new { cid = cid, type = model.PolicyType });
            }
            if (Session["Actn"] != null && Session["Actn"] == "New")
            {
                return RedirectToAction("PremiumDetails", "Customer", new { cid = cid, type = model.PolicyType });
            }
            return RedirectToAction("PolicyInclustions", new { cid = cid, type = model.PolicyType });
        }
        #endregion

        #region Address Suburb State and Postcode
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AddressData(string Fragment)
        {
            string apikey = null;
            if (Request.IsAjaxRequest())
            {
                if (Session["ApiKey"] != null)
                {
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    apikey = Session["ApiKey"].ToString();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage cancelpolicy = await hclient.GetAsync("Locality?apiKey=" + apikey + "&Fragment=" + Fragment);
                    var EmpResponse = cancelpolicy.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        return Json(new { status = true, result = EmpResponse, ErrorMessage = "" });
                    }
                    else
                    {
                        return Json(new { status = false, result = "", ErrorMessage = "" });
                    }
                }
                else
                {
                    return RedirectToAction("AgentLogin", "Login");
                }
            }
            return View();
        }


        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> LocationData(string Fragment)
        {
            string apikey = null;
            if (Request.IsAjaxRequest())
            {
                if (Session["ApiKey"] != null)
                {
                    HttpClient hclient = new HttpClient();
                    LocationList ld = new LocationList();
                    ld.LocalityData = new List<Location>();
                    List<string> ldlist = new List<string>();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    apikey = Session["ApiKey"].ToString();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage location = await hclient.GetAsync("Locality?apiKey=" + apikey + "&Fragment=" + Fragment);
                    var EmpResponse = location.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        ld = JsonConvert.DeserializeObject<LocationList>(EmpResponse);
                        if (ld != null && ld.LocalityData != null && ld.LocalityData.Count() > 0)
                        {
                            for (int i = 0; i < ld.LocalityData.Count(); i++)
                            {
                                ldlist.Add(ld.LocalityData[i].Suburb + " " + ld.LocalityData[i].State.Trim() + " " + ld.LocalityData[i].Postcode);
                            }
                        }
                        return Json(ldlist, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = "", JsonRequestBehavior.AllowGet });
                    }
                }
                else
                {
                    return RedirectToAction("AgentLogin", "Login");
                }
            }
            return View();
        }
        #endregion

        #region Polisy Saving
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> SavePolicy(int PrId)
        {
            if (Request.IsAjaxRequest())
            {
                if (Session["ApiKey"] != null)
                {
                    var db = new MasterDataEntities();
                    ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
                    Identifiers idntfrs = new Identifiers();
                    unitdetails.IdentifierUpdates = new List<Identifiers>();
                    HttpClient hclient = new HttpClient();
                    int? insuredId = 0;
                    if (Session["InsuredId"] != null)
                    {
                        insuredId = Convert.ToInt32(Session["InsuredId"]);
                    }
                    else
                    {
                        insuredId = -1;
                    }
                    int? un = null;
                    int? pr = null;
                    if (Session["UnId"] != null)
                    {
                        un = Convert.ToInt32(Session["UnId"]);
                    }
                    if (Session["profileId"] != null)
                    {
                        pr = Convert.ToInt32(Session["profileId"]);
                    }
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    string apikey = Session["ApiKey"].ToString();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage savepolicy = await hclient.GetAsync("PolicyDetails?apiKey=" + apikey + "&action=Save&pcId=&trId&prId=&InsuredId=&effectiveDate=&reason=");
                    var EmpResponse = savepolicy.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails != null && unitdetails.Status == "Success")
                        {
                            if (unitdetails.PolicyData.PolicyNumber != null)
                            {
                                Session["PolicyNo"] = unitdetails.PolicyData.PolicyNumber;
                            }
                            if (Session["Policyinclustions"] != null)
                            {
                                var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
                                if (Policyincllist != null)
                                {
                                    if (unitdetails.IdentifierUpdates != null && unitdetails.IdentifierUpdates.Count() > 0)
                                    {
                                        int? ProfileId = 0;
                                        for (int i = 0; i < unitdetails.IdentifierUpdates.Count(); i++)
                                        {
                                            if (unitdetails.IdentifierUpdates[i].Name == "UnId")
                                            {
                                                for (int j = 0; j < Policyincllist.Count; j++)
                                                {
                                                    if (Policyincllist[j].ProfileId == unitdetails.IdentifierUpdates[i].OldId)
                                                    {
                                                        if (Policyincllist[j].ProfileId == pr)
                                                        {
                                                            Session["profileId"] = unitdetails.IdentifierUpdates[i].NewId;
                                                            Session["HprofileId"] = unitdetails.IdentifierUpdates[i].NewId;
                                                        }
                                                        ProfileId = unitdetails.IdentifierUpdates[i].NewId;
                                                        Policyincllist[j].ProfileId = ProfileId;
                                                    }
                                                    if (Policyincllist[j].UnitId == unitdetails.IdentifierUpdates[i].OldId)
                                                    {
                                                        if (Policyincllist[j].UnitId == un)
                                                        {
                                                            Session["unId"] = unitdetails.IdentifierUpdates[i].NewId;
                                                        }
                                                        Policyincllist[j].UnitId = unitdetails.IdentifierUpdates[i].NewId;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    Session["Policyinclustions"] = Policyincllist;
                                }
                            }
                        }
                        if (unitdetails.Status == "Failure")
                        {
                            return Json(new { status = false, result = "Failed to save, please try later." });
                        }
                        return Json(new { status = true, result = "Saved successfully and policy number is: " + unitdetails.PolicyData.PolicyNumber });
                    }
                    else
                    {
                        return Json(new { status = false, result = "Failed to save, please try after some time." });

                    }
                }
                else
                {
                    return RedirectToAction("AgentLogin", "Login");
                }
            }
            return View();
        }
        #endregion

        #region Cliams Question
        [HttpGet]
        public ActionResult ClaimsQ(int? cid, int? type, int? insureId)
        {
            Floodarea model = new Floodarea();
            if (Session["Apikey"] != null)
            {

            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            model.CustomerId = cid;
            model.policyType = type;
            model.insureId = insureId;
            return View(model);
        }
        [HttpPost]
        public ActionResult ClaimsQ(Floodarea model)
        {
            if (model != null)
            {
                if (model.HasMadeAClaim == 0)
                {
                    return RedirectToAction("PremiumDetails", "Customer", new { cid = model.CustomerId, type = model.policyType });
                }
                else if (model.HasMadeAClaim == 1)
                {
                    return RedirectToAction("ClaimsDetails", "Claims", new { cid = model.CustomerId, type = model.policyType });
                    //ViewBag.error = "Sorry we currently don’t offer flood cover at this time? Click NO to continue without flood cover.";
                }
            }
            return View(model);
        }

        #endregion

        #region
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Attach()
        {
            string ApiKey = null;
            if (Session["ApiKey"] != null)
            {
                ApiKey = Session["ApiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            string apikey = Session["ApiKey"].ToString();
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            int? InsuredId = -1;
            if (Session["InsuredId"] != null)
            {
                InsuredId = Convert.ToInt32(Session["InsuredId"]);
            }
            HttpResponseMessage attach = await hclient.GetAsync("InsuredDetails?apiKey=" + apikey + "&Action=Attach&InsuredId=" + InsuredId);

            // HttpResponseMessage attach = await hclient.GetAsync("PolicyDetails?apiKey=" + ApiKey + "&action=Attach&pcId=&trId=&prId=1029&InsuredId=&effectiveDate=&reason=");
            //if(Session["newattach"]!=null && Session["newattach"]=="Attachs")
            //{
            //    return Json(Url.Action("CustomerRegistration", "Customer"));
            //}
            return Json(Url.Action("PremiumDetails", "Customer"));
            //return RedirectToAction("", "");
            //var EmpResponse = savepolicy.Content.ReadAsStringAsync().Result;
            //if (EmpResponse != null)
            //{
            //    unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
            //    return Json(Url.Action("PremiumDetails", "Customer"));
            //}
            //else
            //{
            //    return Json(new { status = false, result = "Failed to save, please try after some time." });

            //}
            //return Json(new { status = false, result = "Failed to save, please try after some time." });
        }
        #endregion

        #region Logout
        [HttpPost]
        public ActionResult Logout()
        {
            return Json(Url.Action("AgentLogin", "Login"));

        }
        #endregion

        #region View Document
        public async System.Threading.Tasks.Task<ActionResult> ViewDocument()
        {
            string apikey = null;
            List<SelectListItem> DocList = new List<SelectListItem>();
            if (Request.IsAjaxRequest())
            {
                if (Session["ApiKey"] != null)
                {
                    PrintDocument pd = new Models.PrintDocument();
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    apikey = Session["ApiKey"].ToString();
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage documentslist = await hclient.GetAsync("PrintDetails?ApiKey=" + apikey);
                    var EmpResponse = documentslist.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        pd = JsonConvert.DeserializeObject<PrintDocument>(EmpResponse);
                        return Json(new { status = true, dt = pd.PrintData, ErrorMessage = "" });
                    }
                }
            }
            return Json(new { status = false, ErrorMessage = "" });
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> PrintDocument(int PrintId)
        {
            string apikey = null;
            //if (Request.IsAjaxRequest())
            //{
            if (Session["ApiKey"] != null)
            {
                HttpClient hclient = new HttpClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                hclient.BaseAddress = new Uri(url);
                apikey = Session["ApiKey"].ToString();
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage documentslist = await hclient.GetAsync("PrintDetails?ApiKey=" + apikey + "&&PrintId=" + PrintId);
                var EmpResponse = documentslist.Content.ReadAsStringAsync().Result;
                DocumentDetails details = new DocumentDetails();
                details = JsonConvert.DeserializeObject<DocumentDetails>(EmpResponse);
                //string urls=EmpResponse.
                if (details != null && details.Status == "Success" && details.DocumentUrl != null)
                {
                    return Json(new { status = true, result = details.DocumentUrl, ErrorMessage = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, ErrorMessage = details.ErrorMessage.FirstOrDefault() }, JsonRequestBehavior.AllowGet);

                }
            }
            //}
            return Json(new { status = false, ErrorMessage = "" }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region DELETE TABLE format row
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> DeleteRow(int? unid, int? profileid, List<int> ElId, int? ItId)
        {
            string apikey = null;
            //if (Request.IsAjaxRequest())
            //{
            if (Session["ApiKey"] != null)
            {
                DeleteRow deleterow = new Models.DeleteRow();
                HttpClient hclient = new HttpClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                hclient.BaseAddress = new Uri(url);
                apikey = Session["ApiKey"].ToString();
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                int j = 0;
                if (Session["unId"] != null)
                {
                    unid = Convert.ToInt32(Session["unId"]);
                }
                if (Session["profileId"] != null)
                {
                    profileid = Convert.ToInt32(Session["profileId"]);
                }
                ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
                Identifiers idntfrs = new Identifiers();
                unitdetails.IdentifierUpdates = new List<Identifiers>();
                List<Identifiers> identifierlist = new List<Identifiers>();
                List<SelectListItem> identiferslist = new List<SelectListItem>();
                if (ElId != null && ElId.Count() > 0)
                {
                    for (int i = 0; i < ElId.Count(); i++)
                    {
                        int ELd = ElId[i];
                        HttpResponseMessage documentslist = await hclient.GetAsync("ElementDetails?ApiKey=" + apikey + "&Action=Delete&SectionUnId=" + unid + "&ProfileUnId=" + profileid + "&ElId=" + ELd + "&ItId=" + ItId);
                        var EmpResponse = documentslist.Content.ReadAsStringAsync().Result;
                        //DeleteRow details = new DeleteRow();
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        //if (unitdetails != null && unitdetails.Status == "Success")
                        //{
                        //    j++;
                        //}
                        if (unitdetails != null && unitdetails.ElementData != null && unitdetails.ElementData.Identifiers != null && unitdetails.ElementData.Identifiers.Count > 0)
                        {
                            identifierlist = unitdetails.ElementData.Identifiers;
                        }
                        break;
                    }
                    if (identifierlist != null && identifierlist.Count() > 0)
                    {
                        for (int i = 0; i < identifierlist.Count(); i++)
                        {
                            identiferslist.Add(new SelectListItem { Value = identifierlist[i].NewId.ToString(), Text = identifierlist[i].OldId.ToString() });
                        }
                    }
                    //if (j != null && j > 0)
                    //{
                    return Json(new { status = true, ErrorMessage = "", identifiers = identiferslist }, JsonRequestBehavior.AllowGet);
                    // }
                }
            }
            return Json(new { status = false, ErrorMessage = "" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Go Back
        public ActionResult GoBack()
        {
            string ActionName = null;
            string Controller = null;
            int? cid = 0;
            if (Session["cid"] != null)
            {
                cid = Convert.ToInt32(Session["cid"]);
            }
            if (Session["ActionName"] != null)
            {
                ActionName = Session["ActionName"].ToString();
            }
            if (Session["Controller"] != null)
            {
                Controller = Session["Controller"].ToString();
            }
            return Json(Url.Action(ActionName, Controller, new { cid = cid }));
        }
        #endregion

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            //if (ModelState.IsValid)
            //{
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("Prashanthi@mindtech.labs.com"));  // replace with valid value 
            message.From = new MailAddress("helpdesk@insurethat.com.au");  // replace with valid value
            message.Subject = "Forget Password";
            message.Body = string.Format(body, "abc", "fd", "Message");
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "outbound@SAU-EE237-OR.servercontrol.com.au",  // replace with valid value
                    Password = "$#5U09sfrdtyj34wqy"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 465;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
                return View();
            }
            // }
            return View(model);
        }
    }


}