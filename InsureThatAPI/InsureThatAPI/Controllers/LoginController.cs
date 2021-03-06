﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;
using Newtonsoft.Json;
using System.Text;
using InsureThatAPI.CommonMethods;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace InsureThatAPI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult AgentLogin()
        {
            LogInDetails loginmodel = new LogInDetails();
            loginmodel.Errors = new List<string>();
            try
            {
                Session["newattach"] = null;
                Session["UnId"] = null;
                Session["ProfileId"] = null;
                Session["Email"] = null;
                Session["ApiKey"] = null;
                Session["HprofileId"] = null;
                Session["profileId"] = null;
                Session["Home2"] = null;
                Session["Policyinclustions"] = null;
                Session["EmailId"] = null;
                Session["InsuredId"] = null;
                Session["CustomerType"] = null;
                Session["Actn"] = null;
                Session["Policyinclustions"] = null;
                Session["UnitId"] = null;
                Session["controller"] = null;
                Session["Actname"] = null;
                Session["Policylocal"] = null;
                Session["InsuredName"] = null;
                Session["MprofileId"] = null;
                Session["hombud"] = null;
                Session["PolicyNo"] = null;
                Session["PrId"] = null;
                Session["cid"] = null;
            }
            catch (Exception ex)
            {

            }
            return View(loginmodel);
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AgentLogin(string UserName, string Password)
        {
            try
            {
                LoginDetailsRef loginref = new LoginDetailsRef();
                LogInDetails loginmodel = new LogInDetails();
                List<string> Errors = new List<string>();
                loginref.ErrorMessage = new List<string>();
                loginmodel.Errors = new List<string>();
                var regexSpace = new Regex(@"\s");
                if (UserName == null || UserName == string.Empty || string.IsNullOrWhiteSpace(UserName.Trim()) || Password == null || Password == string.Empty || string.IsNullOrWhiteSpace(Password.Trim()))
                {
                    if (UserName == null || UserName == string.Empty || string.IsNullOrWhiteSpace(UserName.Trim()))
                    {
                        Errors.Add("User Name is required");
                    }
                    if (Password == null || Password == string.Empty || string.IsNullOrWhiteSpace(Password.Trim()))
                    {
                        Errors.Add("Password is required");
                    }
                }
                else
                {
                    string specialCharacters = @"%!#$%^&*(-)?/><,:;'\|}]{[~`+=" + "\"";
                    char[] specialCharactersArray = specialCharacters.ToCharArray();
                    int index = UserName.IndexOfAny(specialCharactersArray);
                    //index == -1 no special characters
                    if (index == -1)
                    {
                    }
                    else
                    {
                        Errors.Add("User Name allows only three special characters '_' '.' '@' ");
                    }
                    if (regexSpace.IsMatch(UserName.Trim()))
                    {
                        Errors.Add("User Name should not have space");
                    }
                    string justNumber = new String(UserName.Trim().Where(Char.IsDigit).ToArray());
                    string justStrings = new String(UserName.Trim().Where(Char.IsLetter).ToArray());

                    if (justStrings == null || justStrings == string.Empty && UserName.Length <= 20)
                    {
                        Errors.Add("User Name does not allow only numeric values");
                    }
                }
                if (regexSpace.IsMatch(Convert.ToString(Password).Trim()))
                {
                    Errors.Add("Password should not have space");
                }

                if (Errors != null && Errors.Count() > 0)
                {
                    loginref.Status = "Failure";
                    loginref.ErrorMessage = Errors;
                    return View();
                }
                else
                {
                    //#region Remove
                    //LogInDetailsClass ld = new LogInDetailsClass();

                    //LoginDetailsRef LoginDetailsRef = new LoginDetailsRef();
                    //LoginDetailsRef.ErrorMessage = new List<string>();
                    //LoginDetailsRef = await ld.GetLogInDetailsPage(UserName, Password);
                    //{
                    //    if(LoginDetailsRef.ErrorMessage!=null && LoginDetailsRef.ErrorMessage.Count()>0)
                    //    {
                    //        loginmodel.Errors = LoginDetailsRef.ErrorMessage;
                    //        return View(loginmodel);
                    //    }
                    //}
                    //#endregion
                    if (await CommonUseFunctionClass.CheckUser(UserName, Password))
                    {
                        string str = CommonUseFunctionClass.GenerateToken(UserName, 20);
                        if (str != string.Empty || str != null || string.IsNullOrWhiteSpace(str.Trim()))
                        {
                            Session["IyId"] = 9262;
                            loginref.Status = "Success";
                            loginref.Access_Token = str;
                            loginmodel.UserName = UserName;
                            loginref.LogInData = loginmodel;
                            return RedirectToAction("CustomerSearch", "Customer");
                        }
                        else
                        {
                            loginref.Status = "Failure";
                            loginref.ErrorMessage.Add("Token is not generated");
                            ViewBag.ErrorMessage = "";
                            return View();
                        }
                    }
                    else
                    {
                        loginref.Status = "Failure";
                        loginref.ErrorMessage.Add("User Name or Password are not valid.");
                        ViewBag.ErrorMessage = "User Name or Password are not valid.";
                        return View();
                    }
                }
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "User Name or Password are not valid.";
                return View();
            }

            return View();
        }
        [HttpGet]
        public ActionResult ResetPassword(string guid)
        {
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                int? result = db.IT_GetForgetPasswordStatus(guid).FirstOrDefault();
                if (result == 0)
                {
                    ViewBag.ErrorMessage = "Already used, URL is not valid.";
                }
                else if (result == 1)
                {

                }
                else
                {
                    ViewBag.ErrorMessage = "Already used, URL is not valid.";
                }
                string Email = string.Empty;
                if (Session["Email"] == null || Session["Email"] != "")
                {
                    Email = Session["Email"].ToString();
                    Session["Email"] = Email;
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> ResetPassword(string guid, string Email, string Password)
        {
            MasterDataEntities db = new MasterDataEntities();
            ForgotPasswordDetailsRef FGDetailsRef = new ForgotPasswordDetailsRef();
            try
            {
                if (Session["Email"] == null || Session["Email"] != "")
                {
                    Email = Session["Email"].ToString();
                }
                else
                {
                    return RedirectToAction("ForgetPassword", "Login");
                }

                string strEncrypt = string.Empty;
                strEncrypt = LogInDetailsClass.Encrypt(Password, "TimsFirstEncryptionKey");
                ForgotPasswordDetails fgModel = new ForgotPasswordDetails();
                fgModel.Email = "";
                fgModel.EncryptedPassword = strEncrypt;
                fgModel.UserName = Email;

                HttpClient hclient = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(fgModel), Encoding.UTF8, "application/json");

                if (Email != null && Email != "")
                {
                    var responses = hclient.BaseAddress = new Uri("https://api.insurethat.com.au/");
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/InsuredDetails?iyId=9262&policyNo=&insuredName=wick&emailId=" + Email + "&phoneNo=");
                    if (Res.IsSuccessStatusCode)
                    {
                        var response = await hclient.PostAsync("https://api.insurethat.com.au/Api/Login", content);
                        var result = await response.Content.ReadAsStringAsync();
                        if (result != null)
                        {
                            FGDetailsRef = JsonConvert.DeserializeObject<ForgotPasswordDetailsRef>(result);
                            int? results = db.IT_InsertForgetPasswordCode(guid.ToString(), 0).SingleOrDefault();
                        }
                        else
                        {
                            FGDetailsRef.Status = "Failure";
                            FGDetailsRef.ErrorMessage.Add("Enter valid details");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Email ID is not valid";
                    }
                }



            }
            catch (Exception xp)
            {
                FGDetailsRef.Status = "Failure";
                FGDetailsRef.ErrorMessage.Add(xp.Message);
            }
            finally
            {

            }
            return View(FGDetailsRef);
        }
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            try
            {
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> ForgetPassword(ForgotPasswordDetails frwd)
        {
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                Guid guid = Guid.NewGuid();
                ChangePasswordDetails cpsd = new ChangePasswordDetails();
                string confirmationLink = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + Url.Action("ResetPassword", "Account", new { key = guid });
                //ForgotPasswordClass fpd = new ForgotPasswordClass();
                //fpd.SendEmail(frwd.Email, confirmationLink);
                //int? result = db.IT_InsertForgetPasswordCode(guid.ToString(), 1).SingleOrDefault();
                string recepientEmail = frwd.Email;
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UName"]);
                    mailMessage.Subject = "Forget Password";
                    mailMessage.Body = confirmationLink;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(recepientEmail));
                    SmtpClient smtpp = new SmtpClient();
                    smtpp.Host = ConfigurationManager.AppSettings["Host"];
                    smtpp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                    System.Net.NetworkCredential NetworkCreda = new System.Net.NetworkCredential();
                    NetworkCreda.UserName = ConfigurationManager.AppSettings["UserName"];
                    NetworkCreda.Password = ConfigurationManager.AppSettings["Password"];
                    smtpp.UseDefaultCredentials = true;
                    smtpp.Credentials = NetworkCreda;
                    smtpp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                    smtpp.Send(mailMessage);
                }

                //using (MailMessage mm = new MailMessage("prashanthireddy.eda@gmail.com","nreddy472@gmail.com"))
                //{
                //    mm.Subject = "FP";
                //    mm.Body = confirmationLink;                   
                //    mm.IsBodyHtml = false;
                //    SmtpClient smtps = new SmtpClient("smtp.gmail.com", 587);
                //    //smtps.Host = "smtp.gmail.com";
                //    //smtps.Host = "smtp.pacific.net.au";
                //    smtps.EnableSsl = true;
                //    NetworkCredential NetworkCreda = new NetworkCredential("prashanthireddy.eda@gmail.com", "sarkar23");
                //    smtps.UseDefaultCredentials = true;
                //    smtps.Credentials = NetworkCreda;
                //    smtps.Port = 587;
                //    smtps.Send(mm);
                //   // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
                //}
                //MailMessage mail = new MailMessage();


                //mail.IsBodyHtml = false;
                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = "smtp.gmail.com";
                //smtp.EnableSsl = true;
                //NetworkCredential NetworkCred = new NetworkCredential("prashanthireddy.eda@gmail.com", "sarkar23");
                //mail.Subject = "Forget Password";
                //mail.Body = confirmationLink;
                //mail.From =new MailAddress("prashanthireddy.eda@gmail.com");
                //mail.To.Add("prashanthi@mindtechlabs.com");
                //smtp.UseDefaultCredentials = true;
                //smtp.Credentials = NetworkCred;
                //smtp.Port = 587;
                //smtp.Send(mail);



                //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                //mail.From = new MailAddress("helpdesk@insurethat.com.au");
                //mail.To.Add("prashanthireddy.eda@gmail.com");
                //mail.Subject = "Test Mail";
                //mail.Body = "This is for testing SMTP mail from GMAIL";
                //SmtpServer.Port = 587;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("outbound@SAU-EE237-OR.servercontrol.com.au", "$#5U09sfrdtyj34wqy");
                //SmtpServer.EnableSsl = true;
                //SmtpServer.Send(mail);
                Session["Email"] = frwd.Email;
                ViewBag.SuccessMessage = "Email is been send successfully.";
                return View();

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to sent email.";
                return View();
            }
        }

    }
}