using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsureThatAPI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;
using System.Configuration;

namespace InsureThatAPI.CommonMethods
{
    public class ForgotPasswordClass
    {
        public void SendEmail(string Email, string confirmationLink)
        {
            String username = ConfigurationManager.AppSettings["AWSAccessKey"].ToString();  // Replace with your SMTP username.
            String password = ConfigurationManager.AppSettings["AWSSecretKey"].ToString();  // Replace with your SMTP password.
            String host = "email-smtp.us-west-2.amazonaws.com";
            int port = 25;

            using (var client = new System.Net.Mail.SmtpClient(host, port))
            {
                client.Credentials = new System.Net.NetworkCredential(username, password);
                client.EnableSsl = true;

                client.Send("prashanthi@mindtechlabs.com",  // Replace with the sender address.
                         Email,   // Replace with the recipient address.
                          "Insure That- Reset passwpord",
                         confirmationLink
                );
            }
        }
        public async Task<ForgotPasswordDetailsRef> GetFogotPasswordDetails(string email, string password, string confirmPWD, string captcha)
        {
            ForgotPasswordDetailsRef FGDetailsRef = new ForgotPasswordDetailsRef();
            try
            {
                string strEncrypt = string.Empty;
                strEncrypt = LogInDetailsClass.Encrypt(password, "TimsFirstEncryptionKey");
                ForgotPasswordDetails fgModel = new ForgotPasswordDetails();
                fgModel.Email = "";
                fgModel.EncryptedPassword = strEncrypt;
                fgModel.UserName = email;
                HttpClient hclient = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(fgModel), Encoding.UTF8, "application/json");
                var response = await hclient.PostAsync("https://api.insurethat.com.au/Api/Login", content);
                var result = await response.Content.ReadAsStringAsync();
                if (result != null)
                {
                    FGDetailsRef = JsonConvert.DeserializeObject<ForgotPasswordDetailsRef>(result);
                }
                else
                {
                    FGDetailsRef.Status = "Failure";
                    FGDetailsRef.ErrorMessage.Add("Enter wrong Password");
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
            return FGDetailsRef;
        }
    }
}