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
            var db = new MasterDataEntities();
            if (Session["apiKey"] != null)
            {
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
            var response = hclient.BaseAddress = new Uri("https://api.insurethat.com.au/");
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/InsuredDetails?iyId=" + customersearch.iyId + "&policyNo=" + customersearch.policyNo + "&insuredName=" + term + "&emailId=" + customersearch.emailId + "&phoneNo=" + customersearch.phoneNo);

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

                    //  return RedirectToAction("InsuredPolicys", "Customer", customersearch);
                    //  return Json(new { results = insureddetails.Insureds.Select(p => new InsuredListDDL() { id = p.InsuredID, text = p.CompanyBusinessName + p.FirstName + " " + p.MiddleName + " " + p.LastName }).ToList() });
                }
                else if (insureddetails.ErrorMessage != null && insureddetails.ErrorMessage.Count() > 0)
                {
                    ViewBag.Status = "Failure";
                    return View(customersearch);
                }
                //if (insureddetails.Insureds != null)
                //{
                //    Session["InsuredId"] = insureddetails.Insureds.Select(p => p.InsuredID).FirstOrDefault();
                //}

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
            //return RedirectToAction("PolicyList", "Customer", new { InsuredId = ViewBag.InsuredId, CustomerId = 1 });
            if (((customersearch.policyNo == null || string.IsNullOrWhiteSpace(customersearch.policyNo)) && (customersearch.emailId == null || string.IsNullOrWhiteSpace(customersearch.emailId)) && (customersearch.phoneNo == null || string.IsNullOrWhiteSpace(customersearch.phoneNo))) && ((PolicyNo == null || String.IsNullOrWhiteSpace(PolicyNo)) && (InsuredName == null || String.IsNullOrWhiteSpace(InsuredName)) && (EmailId == null || String.IsNullOrWhiteSpace(EmailId)) && (PhoneNo == null || String.IsNullOrWhiteSpace(PhoneNo))))
            {
                TempData["ErrorMsg"] = "Enter any details to search";
                return RedirectToAction("AdvancedCustomerSearch", "Customer");

            }
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
            if (Session["apiKey"] != null)
            {
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            InsuredDetails insuredetails = new InsuredDetails();
            MasterDataEntities db = new MasterDataEntities();
            customersearch.iyId = 9262.ToString();//testing should remove//9262 is raci
                                                  //  StringContent content = new StringContent(JsonConvert.SerializeObject(customersearch), Encoding.UTF8, "application/json");
            var response = hclient.BaseAddress = new Uri("https://api.insurethat.com.au/");
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (customersearch != null && (customersearch.emailId != null || customersearch.phoneNo != null))
            {
                //On Submit
                HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/InsuredDetails?iyId=" + customersearch.iyId + "&policyNo=" + customersearch.policyNo + "&insuredName=" + null + "&emailId=" + customersearch.emailId + "&phoneNo=" + customersearch.phoneNo);
                if (Ress.IsSuccessStatusCode)
                {
                    GetInsuredDetailsRef insureddetails = new GetInsuredDetailsRef();
                    var EmpResponse = Ress.Content.ReadAsStringAsync().Result;
                    insureddetails = JsonConvert.DeserializeObject<GetInsuredDetailsRef>(EmpResponse);
                    if (insureddetails.Insureds != null)
                    {
                        ViewBag.InsuredId = insureddetails.Insureds.Select(p => p.InsuredID).FirstOrDefault();
                        int? customerid = db.IT_InsertCustomerMaster(customersearch.emailId, customersearch.InsuredId, customersearch.policyNo, null, customersearch.insuredName, null).SingleOrDefault();
                        return RedirectToAction("InsuredPolicys", "Customer", new { InsuredId = ViewBag.InsuredId, cid = customerid });

                    }

                }
            }

            if (Request.IsAjaxRequest())
            {
                //On Auto Search
                HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/InsuredDetails?iyId=" + customersearch.iyId + "&policyNo=" + PolicyNo + "&insuredName=" + InsuredName + "&emailId=" + EmailId + "&phoneNo=" + PhoneNo);

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
        //[HttpGet]
        //public ActionResult ChangePassword()
        //{
        //    if (Session["apiKey"] != null)
        //    {
        //    }
        //    else
        //    {
        //        return RedirectToAction("AgentLogin", "Login");
        //    }
        //    if (Session["UserName"] != null)
        //    {

        //    }
        //    return View();
        //}
        //[HttpPost]
        //public async System.Threading.Tasks.Task<ActionResult> ChangePassword(ChangePasswordDetails changepassword)
        //{
        //    if (Session["apiKey"] != null)
        //    {
        //    }
        //    else
        //    {
        //        return RedirectToAction("AgentLogin", "Login");
        //    }
        //    LogInDetailsClass logincls = new LogInDetailsClass();
        //    LogInDetails logindetailsmodel = new LogInDetails();
        //    ChangePasswordDetailsRef changepasswordref = new ChangePasswordDetailsRef();
        //    string strEncrypt = string.Empty;
        //    if (Session["UserName"] != null && Session["UserName"] != "")
        //    {
        //        string strDecrypt = string.Empty;
        //        string UserName = string.Empty;
        //        UserName = Session["UserName"].ToString();
        //        string PlainTextEncrpted = string.Empty;
        //        string NewPassword = string.Empty;
        //        string loginKey = string.Empty;
        //        int IyId = 9262;
        //        string EncrptForLogin = String.Format("{0:ddddyyyyMMdd}", DateTime.UtcNow);
        //        PlainTextEncrpted = IyId + "|" + UserName + "|InsureThatDirect";
        //        loginKey = LogInDetailsClass.Encrypt(PlainTextEncrpted, EncrptForLogin);
        //        HttpClient hclient = new HttpClient();
        //        hclient.BaseAddress = new Uri("https://api.insurethat.com.au/");
        //        hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        loginKey = loginKey.Replace("+", "%2B");
        //        HttpResponseMessage Res = await hclient.GetAsync("Api/Login?loginKey=" + loginKey + "");
        //        //change controller name and field name
        //        //changepassword.Email = Session["Email"].ToString();
        //        if (Session["UserName"] != null && Session["UserName"] != " ")
        //        {
        //            changepassword.UserName = Session["UserName"].ToString();
        //        }
        //        if (Res.IsSuccessStatusCode)
        //        {
        //            //  Storing the response details recieved from web api
        //            var EmpResponse = Res.Content.ReadAsStringAsync().Result;

        //            //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
        //            logindetailsmodel = JsonConvert.DeserializeObject<LogInDetails>(EmpResponse);
        //            strEncrypt = LogInDetailsClass.Encrypt(changepassword.Password, "TimsFirstEncryptionKey");//encrypt password method
        //                                                                                                      // strDecrypt = Decrypt(strEncrypt, "TimsFirstEncryptionKey");//decrypt password method
        //            if (logindetailsmodel.EncryptedPassword != null && strEncrypt == logindetailsmodel.EncryptedPassword.ToString())
        //            {
        //                NewPassword = LogInDetailsClass.Encrypt(changepassword.NewPassword, "TimsFirstEncryptionKey");//encrypt new password method
        //                HttpResponseMessage Resp = await hclient.GetAsync("https://api.insurethat.com.au/Api/Login?UserName=" + changepassword.UserName + "&EmailID=" + changepassword.Email + "&EncryptedPassword=" + NewPassword);
        //                if (Res.IsSuccessStatusCode)
        //                {
        //                    TempData["Success"] = "Password is updated, please login with new password.";
        //                    return RedirectToAction("AgentLogin", "Login");
        //                }
        //                else
        //                {
        //                    ViewBag.ErrorMessage = "Failed to update New password, try later.";
        //                }
        //            }
        //            else
        //            {
        //                ViewBag.ErrorMessage = "Old password does not match.";
        //            }
        //        }
        //    }
        //    return View();
        //}
        #region Display Policy List Based on Insured Id
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> PolicyList(int? InsuredId, int? CustomerId)
        {
            if (Session["apiKey"] != null)
            {
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            GetNewPolicyDetailsRef policydetails = new GetNewPolicyDetailsRef();
            //  return View(policydetails);
            PolicyList policylist = new PolicyList();
            policydetails.PolicyData = new List<PolicyDetails>();
            InsuredId = 108454;
            if (InsuredId.HasValue && InsuredId > 0)
            {
                //  insureddetails.InsuredID = Convert.ToInt32(Session["InsuredId"]);
                HttpClient hclient = new HttpClient();
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await hclient.GetAsync(" https://api.insurethat.com.au/Api/PolicyDetails?iyId=9262&paId=" + InsuredId + "");/*insureddetails.InsuredID */
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                policydetails = JsonConvert.DeserializeObject<GetNewPolicyDetailsRef>(EmpResponse);
                ViewBag.CustomerId = CustomerId;
                if (policydetails.PolicyData != null && policydetails.PolicyData.Count() > 0)
                {
                    return View(policydetails);
                }
                else
                {
                    return RedirectToAction("NewPolicy", "Customer", new { CustomerId = 1 });
                }
            }
            else
            {
                return RedirectToAction("AdvancedCustomerSearch", "Customer");
            }
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> InsuredPolicys(int? InsuredId, int? cid)
        {
            if (Session["apiKey"] != null)
            {
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
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await hclient.GetAsync(" https://api.insurethat.com.au/Api/PolicyDetails?iyId=9262&paId=" + InsuredId + "");/*insureddetails.InsuredID */
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
            //else if(CustomerId.HasValue && CustomerId>0)
            //{
            //    return View(policydetails);
            //}
            else
            {
                return RedirectToAction("AdvancedCustomerSearch", "Customer");
            }
        }
        #endregion

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> ViewUpdatePolicyDetails(int? cid, int? PcId, int? step)
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
            int policytype = 1;
            string policyid = PcId.ToString();
            if (PcId != null && PcId > 0)
            {
                ViewEditPolicyDetails model = new ViewEditPolicyDetails();
                var policyinclusion = db.IT_GetPolicyInclusions(cid, policyid, policytype).FirstOrDefault();
                if (policyinclusion != null && policyinclusion.PolicyInclusions != null)
                {
                    model.PolicyInclusions = policyinclusion.PolicyInclusions;
                }
                model.PcId = PcId.ToString();
                HttpClient hclient = new HttpClient();
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
                HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/PolicyDetails?ApiKey=" + ApiKey + "&Action=Retrieve&PcId=" + PcId + "&TrId=&PrId=&EffectiveDate=&Reason=");/*insureddetails.InsuredID */
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword

                model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
                {
                    return RedirectToAction("AgentLogin", "Login");
                }
                if(model.UnitData!=null && model.UnitData.Count>0)
                {
                    model.PolicyInclusions = null;
                    if (policytype == 1)
                    {
                        string policyincl = "0-0-0-0-0-0-0-0-0";
                        string[] policystringarray = policyincl.Split('-');
                        for (int pi = 0;   model.UnitData.Count<= policystringarray.Length; pi++)
                        {
                            if (pi < model.UnitData.Count)
                            {
                                if (model.UnitData[pi].Name == "Home")
                                {
                                    policystringarray[0] = "1";
                                }
                                if (model.UnitData[pi].Name == "Farm Property")
                                {
                                    policystringarray[4] = "1";
                                }
                                if (model.UnitData[pi].Name == "Valuables")
                                {
                                    policystringarray[3] = "1";
                                }
                                if (model.UnitData[pi].Name == "Home Contents")
                                {
                                    policystringarray[1] = "1";
                                }
                                if (model.UnitData[pi].Name == "Travels")
                                {
                                    policystringarray[2] = "1";
                                }
                                if (model.UnitData[pi].Name == "Liability")
                                {
                                    policystringarray[5] = "1";
                                }
                                if (model.UnitData[pi].Name == "Boat")
                                {
                                    policystringarray[6] = "1";
                                }
                                if (model.UnitData[pi].Name == "Motors")
                                {
                                    policystringarray[7] = "1";
                                }
                                if (model.UnitData[pi].Name == "Pets")
                                {
                                    policystringarray[8] = "1";
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        model.PolicyInclusions = policystringarray.ToString();
                        var insertpolicy = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, PcId.ToString(), Convert.ToInt32(PolicyType.RLS)).SingleOrDefault();
                        if (policystringarray[0] == "1")
                        {
                            return RedirectToAction("HomeDescription", "RuralLifeStyle", new {cid=cid, PcId = PcId, });
                        }
                    }

                }
             

                //if (model.RiskData != null && model.RiskData.Count > 0)
                //{
                //    if (model.RiskData.Select(o => o.Name).FirstOrDefault() == "Home" && step == 1)
                //    {
                //        return View(model);
                //    }
                //    if (model.RiskData.Select(o => o.Name).FirstOrDefault() == "Home" && step == 2)
                //    {
                //        return PartialView("ViewOccupancyIPHBuilding", model);

                //    }
                //    if (model.RiskData.Exists(o => o.Name == "Home Contents") && step == 3)
                //    {
                //        //model.RiskData = model.RiskData.Where(p => p.Name == "Home Contents").Select(o => o.Elements).ToList();
                //        return PartialView("ViewHomeContent", model);

                //    }
                //    if (model.RiskData.Exists(o => o.Name == "Valuables") && step == 4)
                //    {
                //        return PartialView("ViewValuables", model);

                //    }
                //    if (model.RiskData.Exists(o => o.Name == "Farm Property") && step == 5)
                //    {
                //        return PartialView("ViewFarmProperty", model);

                //    }
                //    if (model.RiskData.Exists(o => o.Name == "Farm Property") && step == 6)
                //    {
                //        return PartialView("ViewFarmMachinery", model);

                //    }
                //    if (model.PremiumData != null && model.PremiumData.Count > 0 && step == 8)
                //    {
                //        return PartialView("ViewQuotation", model);

                //    }
                //    if (model.RiskData.Select(p => p.Name).First() == "Home Building")
                //    {
                //        // return Json(new { content = RenderRazorViewToString("_UpdateEditRolePermissionList", manageusers), rolesddl = manageusers.RoleList, rolenames = manageusers.RoleName, ViewBag.messages, status = true, msg = "Role assigned successfully." });
                //    }
                //    return View(model);
                //}
            }
            return RedirectToAction("InsuredPolicys", "Customer", new { InsuredId = 108454, CustomerId = cid });
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> ViewUpdatePolicyDetails(int? PcId, int? step)
        {
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
            if (PcId != null && PcId > 0)
            {
                ViewEditPolicyDetails model = new ViewEditPolicyDetails();
                HttpClient hclient = new HttpClient();
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string PlainTextEncrpted = string.Empty;
                string ApiKey = string.Empty;

                ApiKey = Session["apiKey"].ToString();
                HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/PolicyDetails?apiKey=" + ApiKey + "&action=Retrieve&pcId=" + PcId + "&trId=0&effectiveDate=1900-01-01&reason=");/*insureddetails.InsuredID */
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword

                model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);

                if (model.RiskData != null && model.RiskData.Count > 0)
                {
                    if (model.RiskData.Select(o => o.Name).FirstOrDefault() == "Home" && step == 1)
                    {
                        return View(model);
                    }
                    if (model.RiskData.Select(o => o.Name).FirstOrDefault() == "Home" && step == 2)
                    {
                        return PartialView("ViewOccupancyIPHBuilding", model);
                    }
                    if (model.RiskData.Exists(o => o.Name == "Home Contents") && step == 3)
                    {
                        //model.RiskData = model.RiskData.Where(p => p.Name == "Home Contents").Select(o => o.Elements).ToList();
                        return PartialView("ViewHomeContent", model);
                    }
                    if (model.RiskData.Exists(o => o.Name == "Valuables") && step == 4)
                    {
                        return PartialView("ViewValuables", model);
                    }
                    if (model.RiskData.Exists(o => o.Name == "Farm Property") && step == 5)
                    {
                        return PartialView("ViewFarmProperty", model);
                    }
                    if (model.RiskData.Exists(o => o.Name == "Farm Property") && step == 6)
                    {
                        return PartialView("ViewFarmMachinery", model);
                    }
                    if (model.PremiumData != null && model.PremiumData.Count > 0)
                    {
                        return PartialView("ViewQuotation", model);
                    }
                    if (model.RiskData.Select(p => p.Name).First() == "Home Building")
                    {
                        // return Json(new { content = RenderRazorViewToString("_UpdateEditRolePermissionList", manageusers), rolesddl = manageusers.RoleList, rolenames = manageusers.RoleName, ViewBag.messages, status = true, msg = "Role assigned successfully." });
                    }
                    return View(model);
                }


            }

            return RedirectToAction("InsuredPolicys", "Customer", new { InsuredId = 108454, CustomerId = 1 });
        }

        //[HttpGet]
        //public async System.Threading.Tasks.Task<ActionResult> HomeBuilding(int? cid, int? PcId)
        //{
        //    var db = new MasterDataEntities();
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
        //    int policytype = 1;
        //    string policyid = PcId.ToString();
        //    if (PcId != null && PcId > 0)
        //    {
        //        ViewEditPolicyDetails model = new ViewEditPolicyDetails();
        //        var policyinclusion = db.IT_GetPolicyInclusions(cid, policyid, policytype).FirstOrDefault();
        //        if (policyinclusion != null && policyinclusion.PolicyInclusions != null)
        //        {
        //            model.PolicyInclusions = policyinclusion.PolicyInclusions;
        //        }
        //        model.PcId = PcId.ToString();
        //        HttpClient hclient = new HttpClient();
        //        hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        string PlainTextEncrpted = string.Empty;
        //        string ApiKey = string.Empty;
        //        if (cid != null)
        //        {
        //            ViewBag.cid = cid;
        //            model.CustomerId = cid.Value;
        //        }
        //        else
        //        {
        //            ViewBag.cid = model.CustomerId;
        //        }
        //        ApiKey = Session["apiKey"].ToString();
        //        HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/UnitDetails?ApiKey=" + ApiKey + "&Action=New&SectionName=Home Buildings&SectionUnId=1&ProfileUnId=");/*insureddetails.InsuredID */
        //        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
        //        //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword

        //        model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
        //        if (model.ErrorMessage != null && model.ErrorMessage.Count > 0 && model.ErrorMessage.Contains("API Session Expired"))
        //        {
        //            return RedirectToAction("AgentLogin", "Login");
        //        }
        //        if (model.UnitData != null && model.UnitData.Count > 0)
        //        {
                    

        //        }
        //        return View();
        //    }
        //    return View();
        //}
        [HttpGet]
        public ActionResult NewPolicy(int? cid)
        {
            ViewBag.CustomerId = cid;
            return View();
        }
        [HttpPost]
        public ActionResult NewPolicy(int? cid, PolicyTypes model)
        {
            ViewBag.CustomerId = cid;
            return View();
        }
        [HttpGet]
        public ActionResult PolicyInclustions(int? cid, int? type)
        {
            Session.Remove("Policyinclustions");
            //   Session["Policyinclustions"] = null;
            PolicyInclustions model = new PolicyInclustions();
            if (type == 1)
            {

            }
            else if (type == 2)
            {

            }
            else
            {
                type = 1;
            }
            var db = new MasterDataEntities();
            var inclustionslist = db.IT_GetPolicyInclusions(cid, null, type).SingleOrDefault();
            if (inclustionslist != null && inclustionslist.PolicyInclusions != null)
            {
                model.PolicyInclusions = inclustionslist.PolicyInclusions;
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
            List<string> PolicyinslustionsList = new List<string>();
            string policyid = null;
            if (model.PolicyType == 1)
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
                        model.PolicyInclusions = "1";
                        PolicyinslustionsList.Add("Home");
                    }
                    else
                    {
                        model.PolicyInclusions = "0";
                    }
                    if (model.HomeContents == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions+"-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-1";
                        }
                        PolicyinslustionsList.Add("HomeContents");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.Valuables == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-1";
                        }
                        PolicyinslustionsList.Add("Valuables");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.FarmProperty == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-1";
                        }
                        PolicyinslustionsList.Add("FarmProperty");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.Liability == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-1";
                        }
                        PolicyinslustionsList.Add("Liability");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.Travels == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-0-1";
                        }
                        PolicyinslustionsList.Add("Travel");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.Boat == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-0-0-1";
                        }
                        PolicyinslustionsList.Add("Boat");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.Motor == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-0-0-0-1";
                        }
                        PolicyinslustionsList.Add("Motor");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.Pet == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-0-0-0-0-1";
                        }
                        PolicyinslustionsList.Add("Pet");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                }
                Session["Policyinclustions"] = PolicyinslustionsList;
                var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.RLS));
                return RedirectToAction("HomeDescription", "RuralLifeStyle", new { cid = cid , PcId=policyid });
            }
            else if (model.PolicyType == 2)
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
                        model.PolicyInclusions = "1";
                        PolicyinslustionsList.Add("MobileFarmProperty");
                    }
                    else
                    {
                        model.PolicyInclusions = "0";
                    }
                    if (model.FixedFarmProperty == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-1";
                        }
                        PolicyinslustionsList.Add("FixedFarmProperty");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.FarmInteruption == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-1";
                        }
                        PolicyinslustionsList.Add("FarmInteruption");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.FarmLiability == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-1";
                        }
                        PolicyinslustionsList.Add("FarmLiability");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.Burglary == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-1";
                        }
                        PolicyinslustionsList.Add("Burglary");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.Electronics == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-0-1";
                        }
                        PolicyinslustionsList.Add("Electronics");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.Money == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-0-0-1";
                        }
                        PolicyinslustionsList.Add("Money");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.Transit == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-0-0-0-1";
                        }
                        PolicyinslustionsList.Add("Transit");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.ValuablesFarm == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-0-0-0-1";
                        }
                        PolicyinslustionsList.Add("ValuablesFarm");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.LiveStockFarm == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-0-0-0-0-1";
                        }
                        PolicyinslustionsList.Add("LiveStockFarm");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.PersonalLiabilitiesFarm == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-0-0-0-0-0-1";
                        }
                        PolicyinslustionsList.Add("PersonalLiabilitiesFarm");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.HomeBuildingFarm == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-0-0-0-0-0-0-1";
                        }
                        PolicyinslustionsList.Add("HomeBuildingFarm");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.HomeContent == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-0-0-0-0-0-0-0-1";
                        }
                        
                        PolicyinslustionsList.Add("HomeContent");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.Machinery == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-0-0-0-0-0-0-0-0-1";
                        }
                        PolicyinslustionsList.Add("Machinery");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                    if (model.MotorFarm == true)
                    {
                        if (model.PolicyInclusions != null || string.IsNullOrWhiteSpace(model.PolicyInclusions))
                        {
                            model.PolicyInclusions = model.PolicyInclusions + "-1";
                        }
                        else
                        {
                            model.PolicyInclusions = "0-0-0-0-0-0-0-0-0-0-0-0-1";
                        }
                        PolicyinslustionsList.Add("MotorFarm");
                    }
                    else
                    {
                        model.PolicyInclusions = model.PolicyInclusions + "-0";
                    }
                }
                Session["Policyinclustions"] = PolicyinslustionsList;
                var policyinclusions = db.IT_InsertPolicyInclusions(cid, model.PolicyInclusions, policyid, Convert.ToInt32(PolicyType.FarmPolicy));
                return RedirectToAction("FarmLocationDetails", "FarmPolicyProperty", new { cid = cid });
            }

            return RedirectToAction("NewPolicy", "RuralLifeStyle", new { cid = cid });

        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> InsuredList(string PolicyNumber)
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
                var response = hclient.BaseAddress = new Uri("https://api.insurethat.com.au/");
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string ApiKey = string.Empty;

                ApiKey = Session["apiKey"].ToString();
                HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/api/PolicyHistory?apiKey=" + ApiKey + "&policyNumber=" + PolicyNumber);
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<PolicyHistory>(EmpResponse);


                }

                return View(model);
            }
            return View();
        }
    }


}