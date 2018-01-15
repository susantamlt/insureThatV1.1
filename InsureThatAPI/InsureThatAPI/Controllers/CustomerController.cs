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
                    return Json(new { results = insureddetails.Insureds.Select(p => new InsuredListDDL() { id = p.InsuredID, text = p.CompanyBusinessName + p.FirstName + " " + p.MiddleName + " " + p.LastName }).ToList() });
                }
                else if (insureddetails.Insureds != null && insureddetails.Insureds.Count() > 0)
                {
                    int? customerid = db.IT_InsertCustomerMaster(customersearch.emailId, customersearch.InsuredId, customersearch.policyNo, null, customersearch.insuredName, null).SingleOrDefault();
                    return RedirectToAction("InsuredPolicys", "Customer", new { InsuredId = insureddetails.Insureds.Select(p => p.InsuredID).FirstOrDefault(), CustomerId = customerid });
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
        public ActionResult AdvancedCustomerSearch(CustomerSearch customersearch)
        {
            if (Session["apiKey"] != null)
            {
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            return View(customersearch);
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AdvCustomerSearch(CustomerSearch customersearch, string InsuredName, string PolicyNo, string PhoneNo, string EmailId)
        {
            if (((customersearch.policyNo == null || string.IsNullOrWhiteSpace(customersearch.policyNo)) && (customersearch.emailId == null || string.IsNullOrWhiteSpace(customersearch.emailId)) && (customersearch.phoneNo == null || string.IsNullOrWhiteSpace(customersearch.phoneNo))) && ((PolicyNo == null || String.IsNullOrWhiteSpace(PolicyNo)) && (InsuredName == null || String.IsNullOrWhiteSpace(InsuredName)) && (EmailId == null || String.IsNullOrWhiteSpace(EmailId)) && (PhoneNo == null || String.IsNullOrWhiteSpace(PhoneNo))))
            {
                TempData["ErrorMsg"] = "Enter any details to search";
                return RedirectToAction("AdvancedCustomerSearch", "Customer");
            }
            string ApiKey = " ";
            //else if ((PolicyNo ==null || String.IsNullOrWhiteSpace(PolicyNo))  && (InsuredName==null || String.IsNullOrWhiteSpace(InsuredName)) && (EmailId==null || String.IsNullOrWhiteSpace(EmailId)) && (PhoneNo==null || String.IsNullOrWhiteSpace(PhoneNo)))
            //{
            //    TempData["ErrorMsg"] = "Enter any details to search";
            //    return RedirectToAction("AdvancedCustomerSearch", "Customer");

            //}
            HttpClient hclient = new HttpClient();
            if (Session["IyId"] != null && Session["IyId"] != "")
            {
                customersearch.iyId = Session["IyId"].ToString();
            }
            if (Session["ApiKey"] != null)
            {
                ApiKey = Session["ApiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            InsuredDetails insuredetails = new InsuredDetails();
            MasterDataEntities db = new MasterDataEntities();
            customersearch.iyId = 9262.ToString();//testing should remove//9262 is raci
                                                  //  StringContent content = new StringContent(JsonConvert.SerializeObject(customersearch), Encoding.UTF8, "application/json");
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (customersearch != null && (customersearch.emailId != null || customersearch.phoneNo != null))
            {
                //On Submit
                HttpResponseMessage Ress = await hclient.GetAsync("InsuredDetails?ApiKey=" + ApiKey + "&iyId=" + customersearch.iyId + "&policyNo=" + customersearch.policyNo + "&insuredName=" + null + "&emailId=" + customersearch.emailId + "&phoneNo=" + customersearch.phoneNo);
                if (Ress.IsSuccessStatusCode)
                {
                    GetInsuredDetailsRef insureddetails = new GetInsuredDetailsRef();
                    var EmpResponse = Ress.Content.ReadAsStringAsync().Result;
                    insureddetails = JsonConvert.DeserializeObject<GetInsuredDetailsRef>(EmpResponse);
                    if (insureddetails.Insureds != null)
                    {
                        ViewBag.InsuredId = insureddetails.Insureds.Select(p => p.InsuredID).FirstOrDefault();
                        // int cid=db.IT_CC_Insert_InsuredDetails()
                        int? customerid = db.IT_InsertCustomerMaster(customersearch.emailId, customersearch.InsuredId, customersearch.policyNo, null, customersearch.insuredName, null).SingleOrDefault();
                        return RedirectToAction("InsuredPolicys", "Customer", new { InsuredId = ViewBag.InsuredId, cid = customerid });

                    }
                }
            }
            if (Request.IsAjaxRequest())
            {
                //On Auto Search
                HttpResponseMessage Res = await hclient.GetAsync("InsuredDetails?ApiKey=" + ApiKey + "&iyId=" + customersearch.iyId + "&policyNo=" + PolicyNo + "&insuredName=" + InsuredName + "&emailId=" + EmailId + "&phoneNo=" + PhoneNo);

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
            return View();
        }
        #region Display Policy List Based on Insured Id

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> InsuredPolicys(int? InsuredId, int? cid)
        {
            string apikey = null;
            if (Session["apiKey"] != null)
            {
                apikey = Session["ApiKey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            GetNewPolicyDetailsRef policydetails = new GetNewPolicyDetailsRef();
            PolicyList policylist = new PolicyList();
            policydetails.PolicyData = new List<PolicyDetails>();
            //InsuredId = 108454;
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
            PcId = 54611;
            // PcId = 54693;
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
                }
                else
                {
                    ViewBag.cid = model.CustomerId;
                }
                ApiKey = Session["apiKey"].ToString();
                HttpResponseMessage Res = await hclient.GetAsync("PolicyDetails?ApiKey=" + ApiKey + "&Action=Retrieve&PcId=" + PcId + "&TrId=&PrId=&InsuredId=&EffectiveDate=&Reason=");/*insureddetails.InsuredID */
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                {
                    return RedirectToAction("AgentLogin", "Login");
                }
                if (model != null)
                {
                    //  Session["apiKey"] = model.ApiKey;
                    if (model.PolicyData != null)
                    {
                        var insertpolicydetails = db.IT_dt_Insert_PolicyDetails(model.PolicyData.PolicyNumber, null, model.PolicyData.TransactionNumber, model.PolicyData.PcId, model.PolicyData.TrId, model.PolicyData.TermNumber, model.PolicyData.AccountManagerID,
                            model.PolicyData.PolicyStatus, model.PolicyData.CoverPeriod, model.PolicyData.CoverPeriodUnit, model.PolicyData.InceptionDate, model.PolicyData.ExpiryDate, model.PolicyData.EffectiveDate, model.PolicyData.PrId, model.PolicyData.IyId,
                            model.PolicyData.InsuredName, model.PolicyData.RemoveStampDuty, model.PolicyData.CreatedbyUserId, model.PolicyData.Timecreated, model.PolicyData.IsFloodCoverRequired, model.PolicyData.HasMadeAClaim, model.PolicyData.Reason, model.Status).SingleOrDefault();

                        if (insertpolicydetails > 1)
                        {
                            if (model.UnitData != null && model.UnitData.Count > 0)
                            {
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
                                    if (model.UnitData[pi].Name == "Mobile Farm Property")
                                    {
                                        model.PolicyInclusions = "MFP";
                                        var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.RLS), model.UnitData[pi].UnId, model.UnitData[pi].UnitNumber, model.UnitData[pi].UnitStatus).SingleOrDefault();
                                    }

                                    return RedirectToAction("FarmHomeBilding", "PolicyDetails", new { cid = cid, PcId = PcId });
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
            return RedirectToAction("InsuredPolicys", "Customer", new
            {
                InsuredId = 108454,
                CustomerId = cid
            });
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

                }
                ViewBag.CustomerId = cid;
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
            if (model.InsureId != null && model.InsureId > 0 && model.cid != null)
            {
                string apikey = null;
                if (Session["ApiKey"] != null)
                {
                    apikey = Session["ApiKey"].ToString();
                }
                // model.InsureId = insureId;
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
                if (pd != null)
                {
                    if (pd.ApiKey != null)
                    {
                        Session["ApiKey"] = apikey;
                    }
                    var insertresult = db.IT_dt_Insert_PolicyDetails(pd.PolicyData.PolicyNumber, model.InsureId, pd.PolicyData.TransactionNumber, pd.PolicyData.PcId, pd.PolicyData.TrId, pd.PolicyData.TermNumber, pd.PolicyData.AccountManagerID, pd.PolicyData.PolicyStatus,
                        pd.PolicyData.CoverPeriod, pd.PolicyData.CoverPeriodUnit, pd.PolicyData.InceptionDate, pd.PolicyData.ExpiryDate, pd.PolicyData.EffectiveDate, pd.PolicyData.PrId, pd.PolicyData.IyId, pd.PolicyData.InsuredName, pd.PolicyData.RemoveStampDuty, pd.PolicyData.CreatedbyUserId,
                        pd.PolicyData.Timecreated, pd.PolicyData.IsFloodCoverRequired, pd.PolicyData.HasMadeAClaim, pd.Reason, pd.Status).SingleOrDefault();
                }
                if (pd.UnitData != null && pd.UnitData.Count > 0)
                {

                }

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
            return RedirectToAction("PolicyInclustions", new { cid = cid, type = model.PolicyType });
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
            List<string> PolicyinslustionsList = new List<string>();
            string policyid = null;

            if (model.PolicyType == 1029)
            {
                if (model.HomeBuilding == false && model.HomeContents == false && model.Valuables == false && model.FarmProperty == false && model.Liability == false && model.Travels == false && model.Boat == false && model.Pet == false)
                {
                    ViewBag.ErrorMsg = "Please select any inclustion to continue.";
                    return View(model);
                }
                else
                {
                    if (model.HomeBuilding == true)
                    {
                        PolicyinslustionsList.Add("Home Buildings");
                    }
                    if (model.HomeContents == true)
                    {
                        PolicyinslustionsList.Add("Home Contents");
                    }
                    if (model.Valuables == true)
                    {
                        PolicyinslustionsList.Add("Valuables");
                    }
                    if (model.FarmProperty == true)
                    {
                        PolicyinslustionsList.Add("Farm Property");
                    }
                    if (model.Liability == true)
                    {
                        PolicyinslustionsList.Add("Liability");
                    }
                    if (model.Travels == true)
                    {
                        PolicyinslustionsList.Add("Travel");
                    }
                    if (model.Boat == true)
                    {
                        PolicyinslustionsList.Add("Boat");
                    }
                    if (model.Motor == true)
                    {
                        PolicyinslustionsList.Add("Motor");
                    }
                    if (model.Pet == true)
                    {
                        PolicyinslustionsList.Add("Pet");
                    }
                }
                Session["Policyinclustions"] = PolicyinslustionsList;

            }
            else if (model.PolicyType == 1021)
            {
                if (model.MobileFarmProperty == false && model.FixedFarmProperty == false && model.FarmInteruption == false && model.FarmLiability == false
                    && model.Burglary == false && model.Electronics == false && model.Money == false && model.Transit == false && model.ValuablesFarm == false
                    && model.LiveStockFarm == false && model.PersonalLiabilitiesFarm == false && model.HomeBuildingFarm == false && model.HomeContent == false &&
                    model.Machinery == false && model.MotorFarm == false)
                {
                    ViewBag.ErrorMsg = "Please select any inclustion to continue.";
                    return View(model);
                }
                else
                {
                    if (model.MobileFarmProperty == true)
                    {
                        PolicyinslustionsList.Add("Mobile Farm Property");
                    }
                    if (model.FixedFarmProperty == true)
                    {
                        PolicyinslustionsList.Add("Fixed Farm Property");
                    }
                    if (model.FarmInteruption == true)
                    {
                        PolicyinslustionsList.Add("Farm Interuption");
                    }
                    if (model.FarmLiability == true)
                    {
                        PolicyinslustionsList.Add("Farm Liability");
                    }
                    if (model.Burglary == true)
                    {
                        PolicyinslustionsList.Add("Burglary");
                    }
                    if (model.Electronics == true)
                    {
                        PolicyinslustionsList.Add("Electronics");
                    }
                    if (model.Money == true)
                    {
                        PolicyinslustionsList.Add("Money");
                    }
                    if (model.Transit == true)
                    {
                        PolicyinslustionsList.Add("Transit");
                    }
                    if (model.ValuablesFarm == true)
                    {
                        PolicyinslustionsList.Add("Valuables");
                    }
                    if (model.LiveStockFarm == true)
                    {
                        PolicyinslustionsList.Add("LiveStock");
                    }
                    if (model.PersonalLiabilitiesFarm == true)
                    {
                        PolicyinslustionsList.Add("Personal Liabilities Farm");
                    }
                    if (model.HomeBuildingFarm == true)
                    {
                        PolicyinslustionsList.Add("Home Building");
                    }
                    if (model.HomeContent == true)
                    {
                        PolicyinslustionsList.Add("Home Content");
                    }
                    if (model.Machinery == true)
                    {
                        PolicyinslustionsList.Add("Machinery");
                    }
                    if (model.MotorFarm == true)
                    {
                        PolicyinslustionsList.Add("Motor");
                    }
                }
                Session["Policyinclustions"] = PolicyinslustionsList;
            }

            if (PolicyinslustionsList != null && PolicyinslustionsList.Count > 0)
            {
                for (int unitselected = 0; unitselected <= PolicyinslustionsList.Count(); unitselected++)
                {
                    //var policyinclusions = db.IT_InsertPolicyInclusions(cid, PolicyinslustionsList[unitselected], policyid, model.PolicyType, null, null, null).SingleOrDefault();

                }
                if (model.PolicyType == (int)PolicyType.RLS)
                {
                    return RedirectToAction("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = policyid });
                }
                else if (model.PolicyType == (int)PolicyType.FarmPolicy)
                {
                    return RedirectToAction("FarmContents", "MobileFarm", new { cid = cid });
                }
                return RedirectToAction("NewPolicy", "RuralLifeStyle", new { cid = cid });
            }
            return View(model);
        }



        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> InsuredList(int cid,string PolicyNumber)
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
                ViewEditPolicyDetails modell = new ViewEditPolicyDetails();
                string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                hclient.BaseAddress = new Uri(url);
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string ApiKey = string.Empty;
                ElementDetails model = new Models.ElementDetails();
                //string unid = Session["ProfileUnid"].ToString();
                //string SectionunId= Session["ProfileUnid"].ToString();
                ValueDatass vd = new ValueDatass();
                vd.Element = new Elementss();
                StateData sd = new StateData();
                sd.Element = new Elements();
                List<ValueDatass> valueData = new List<ValueDatass>();
                List<StateData> stateData = new List<StateData>();
                if (Session["ApiKey"] != null)
                {
                    ApiKey = Session["apiKey"].ToString();
                    model.ApiKey = ApiKey;
                    model.ElId = ElId;
                    model.ItId = 0;
                    model.ProfileUnId = -1;
                    model.SectionUnId = -2;
                    model.Value = Value;

                }
                else
                {
                    return RedirectToAction("AgenLogin", "Login");
                }
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var responses = await hclient.PostAsync("" +
                    "ElementDetails", content);
                var result = await responses.Content.ReadAsStringAsync();
                if (result != null)
                {
                    modell = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(result);
                    if (modell.ReferralList != null)
                    {
                        string ReferralList = modell.ReferralList;
                    }
                    if (modell.ElementData.ValueData != null)
                    {
                        if (modell.ElementData.ValueData != null && modell.ElementData.ValueData.Count() > 0)
                        {
                            for (int j = 0; j < modell.ElementData.ValueData.Count(); j++)
                            {
                                int? ELLId = modell.ElementData.ValueData[j].Element.ElId;
                                vd.Element.ElId = ELLId ?? 0;
                                string Val = modell.ElementData.ValueData[j].Value;
                                vd.Value = Val;
                                valueData.Add(vd);
                            }
                        }
                        if (modell.ElementData.StateData != null && modell.ElementData.StateData.Count > 0)
                        {
                            int statevalue = 0;
                            for (int j = 0; j < modell.ElementData.StateData.Count(); j++)
                            {
                                int? ELLId = modell.ElementData.StateData[j].Element.ElId;
                                sd.Element.ElId = ELLId ?? 0;
                                statevalue = modell.ElementData.StateData[j].State;
                                if (statevalue == 1)
                                {
                                    sd.Value = "Invisible";
                                }
                                else if (statevalue == 2)
                                {
                                    sd.Value = "Disabled";
                                }
                                else if (statevalue == 4)
                                {
                                    sd.Value = "Read Only";
                                }
                                else if (statevalue == 8)
                                {
                                    sd.Value = "Strike Through";
                                }
                                else if (statevalue == 16)
                                {
                                    sd.Value = "Required";
                                }
                                else if (statevalue == 32)
                                {
                                    sd.Value = "Not Rendered";
                                }
                                stateData.Add(sd);
                            }
                        }
                    }
                    return Json(new { Status = true, valuedata = valueData, stateData = stateData, referrallist = modell.ReferralList });
                }
            }
            return Json(new { Status = false });
        }

        #endregion

        #region AJAX CALL for PREMIUM DETAILS
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
                    return RedirectToAction("AgenLogin", "Login");
                }
            }
            return Json(new { Status = false });
        }
        #endregion

        #region AJAX Unit Details saving
        public async System.Threading.Tasks.Task<ActionResult> AddUnit(string unit)
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
                    HttpResponseMessage getunit = await hclient.GetAsync("UnitDetails?ApiKey=" + apikey + "&Action=New&SectionName=" + unit + "&SectionUnId=&ProfileUnId=");
                    var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
                    if (EmpResponse != null)
                    {
                        unitdetails = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                        if (unitdetails.SectionData != null)
                        {
                            var insertunits = db.IT_dt_Insert_Unit(4, "Section", unit, unitdetails.SectionData.UnId, unitdetails.SectionData.UnitNumber,
                                     "True", unitdetails.SectionData.ProfileUnId, unitdetails.ReferralList, unitdetails.Status).SingleOrDefault();
                            return Json(new
                            {
                                Status = true
                            });

                        }
                    }
                }
                else
                {
                    return RedirectToAction("AgenLogin", "Login");
                }
            }
            return Json(new
            {
                Status = false
            });
        }

        #endregion

        #region AJAX call for Endorsing Policy
        public async System.Threading.Tasks.Task<ActionResult> EndorsePolicy(string name,int PcId, int cid, int trId, int unid, int profileid, string policyStatus, int prId)
        {
            ViewEditPolicyDetails unitdetails = new ViewEditPolicyDetails();
            if (Request.IsAjaxRequest())
            {
                if (Session["ApiKey"] != null)
                {
                    var db = new MasterDataEntities();
                  
                    string apikey = Session["ApiKey"].ToString();
                    if(policyStatus=="NP"||policyStatus=="AP"||policyStatus=="CP")
                    {

                    }
                    else
                    {
                        Session["unId"] = unid;
                        Session["profileId"] = profileid;
                        if (prId==1029)
                        {
                            if (name == "Home")
                            {
                                return RedirectToAction("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = PcId });

                            }
                            if (name=="Home Building")
                            {
                                return RedirectToAction("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = PcId });
                            }
                            else if(name=="Home Content")
                            {
                                return RedirectToAction("HomeContent", "HomeContentValuable", new { cid = cid,PcId=PcId });
                            }
                            else if(name=="valuables")
                            {
                                return RedirectToAction("Valuables", "HomeContentValuable", new { cid = cid,PcId=PcId });
                            }
                            else if(name=="Farm Property")
                            {
                                return RedirectToAction("FarmContents", "Farm", new { cid = cid, PcId = PcId });
                            }
                            else if(name=="Liability")
                            {
                                return RedirectToAction("LiabilityCover", "Liabilities", new { cid = cid, PcId = PcId });
                            }
                            else if(name=="Pets")
                            {
                                return RedirectToAction("PetsCover", "Pets", new { cid = cid ,PcId=PcId});

                            }
                            else if(name=="Motor")
                            {
                                return RedirectToAction("VehicleDescription", "MotorCover", new { cid = cid, PcId = PcId });
                            }
                            else if(name=="Boat")
                            {
                                return RedirectToAction("BoatDetails", "Boat", new { cid = cid ,PcId=PcId});

                            }
                            else if(name=="Travel")
                            {
                                return RedirectToAction("TravelCover", "Travel", new { cid = cid,PcId=PcId });
                            }

                        }
                        else if(prId==1021)
                        {

                        }
                    }
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    var dateAndTime = DateTime.UtcNow;
                    var date = dateAndTime.Date.ToString("yyyy-MM-dd");
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
                        if (unitdetails.PolicyData!=null && unitdetails.PolicyData.PrId == (int)PolicyType.RLS)
                        {
                            Session["unId"] = unid;
                            Session["profileId"] = profileid;
                            return RedirectToAction("HomeDescription", "RuralLifeStyle", new { cid = cid, PcId = PcId });
                        }
                        else if(unitdetails.PolicyData!=null)
                        {
                            return RedirectToAction("FarmContents", "MobileFarm", new { cid = cid });
                        }
                        if(unitdetails.ErrorMessage!=null)
                        {
                            return Json(new
                            {
                                Status = false,error= "unitdetails.ErrorMessage"

                            });
                        }
                    }
                }
                else
                {
                    return RedirectToAction("AgenLogin", "Login");
                }
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
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage cancelpolicy = await hclient.GetAsync("PolicyDetails?apiKey=" + apikey + "&action=Cancel&pcId=" + PcId + "&trId" + trId + "&prId=&InsuredId=&effectiveDate=" + DateTime.UtcNow + "&reason=" + Reson);
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
                    return RedirectToAction("AgenLogin", "Login");
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
        public async System.Threading.Tasks.Task<ActionResult> DeleteSection (int cid, int unid)
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
                    HttpResponseMessage deleteS = await hclient.GetAsync("UnitDetails?apiKey=" + apikey + "&Action=Delete&SectionName=&SectionUnId="+unid+"&ProfileUnId=");
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
                    return RedirectToAction("AgenLogin", "Login");
                }
            }
            return Json(new
            {
                Status = false
            });
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
                    HttpResponseMessage deleteS = await hclient.GetAsync("PolicyDetails?apiKey="+apikey+"&action=Bind&pcId=&trId=&prId=&InsuredId=&effectiveDate=&reason=");
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
                    return RedirectToAction("AgenLogin", "Login");
                }
            }
            return Json(new
            {
                Status = false
            });
        }

        #endregion
    }


}