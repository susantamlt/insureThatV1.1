using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsureThatAPI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Mail;
using System.ComponentModel;

namespace InsureThatAPI.CommonMethods
{
    public class ChangePasswordDetailsClass
    {
        #region  Forget Password
        public static void SendEmail(MailMessage emailMessage, int emId)
        {
            SmtpClient client = new SmtpClient();
            // Set the method that is called back when the send operation ends.
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            client.SendAsync(emailMessage, emId);
        }
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            int emId = Convert.ToInt32(e.UserState);
            string serverResponse = (e.Error != null ? e.Error.ToString() : "Send OK");

           // EmailBo.UpdateStore(emId, serverResponse);
        }

        private static void UpdateStore(int emId, string serverResponse)
        {
           // EmailDb.UpdateStore(emId, serverResponse);
        }
        #endregion
        #region Update change password details 

        public async Task<int?> UpdatePasswordDetails(ChangePasswordDetails values)
        {
            int result = 0;
            ChangePasswordDetails changepwdmodel = new ChangePasswordDetails();
            try
            {
                HttpClient hclient = new HttpClient();
                hclient.BaseAddress = new Uri("http://insurethatapi.us-east-2.elasticbeanstalk.com/");
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await hclient.GetAsync("api/ListOfPoliciesDetails?InsuredID=" + values.UserId + "");// Change Controller Name and fields
                //   LogInDetails loginmodel = new LogInDetails();
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    changepwdmodel = JsonConvert.DeserializeObject<ChangePasswordDetails>(EmpResponse);

                    MasterDataEntities db = new MasterDataEntities();
                    if (values.UserId.HasValue && values.UserId> 0)
                    {
                       // result = db.IT_IDC_UPDATE_USERPASSWORDDETAILS(values.UserID, values.Password, values.NewPassword);
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception xp)
            {

            }
            finally
            {

            }
            return result;
        }


        #endregion

    }
}