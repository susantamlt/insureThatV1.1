using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InsureThatAPI.Models;
using InsureThatAPI.CommonMethods;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InsureThatAPI.Controllers
{
    public class ChangePasswordDetailsController : ApiController
    {
        // GET: api/ChangePasswordDetails
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/ChangePasswordDetails/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ChangePasswordDetails
        public void Post([FromBody]ChangePasswordDetails value)
        {

        }

        #region Update change password method for user details


        // PUT: api/ChangePasswordDetails/5
        public async Task<HttpResponseMessage> Put([FromBody]ChangePasswordDetails value)
        {
            ChangePasswordDetailsClass changedetails = new ChangePasswordDetailsClass();
            ChangePasswordDetailsRef changepwdref = new ChangePasswordDetailsRef();
            List<string> Errors = new List<string>();
            try
            {
                changepwdref.ErrorMessage = new List<string>();
                if (value != null)
                {
                    var regexSpace = new Regex(@"\s");
                    if (regexSpace.IsMatch(value.UserName.Trim()))
                    {
                        Errors.Add("UserName having space,Name does not accept space");
                    }
                    if (regexSpace.IsMatch(value.Password.Trim()))
                    {
                        Errors.Add("Password having space,Password does not accept space");
                    }
                    if (regexSpace.IsMatch(value.NewPassword.Trim()))
                    {
                        Errors.Add("NewPassword having space,NewPassword does not accept space");
                    }
                    if (regexSpace.IsMatch(value.RePassword.Trim()))
                    {
                        Errors.Add("RePassword having space,RePassword does not accept space");
                    }
                    if (value.UserName.Trim() == string.Empty || value.UserName.Trim() == null || string.IsNullOrWhiteSpace(value.UserName.Trim()))
                    {
                        if (value.Email.Trim() == string.Empty || value.Email.Trim() == null || string.IsNullOrWhiteSpace(value.Email.Trim()))
                        {
                            Errors.Add("UserName Or Email any one is required");
                        }
                    }
                    if (value.Email.Trim() != null || value.Email.Trim() != string.Empty || value.UserName.Trim() != null || value.UserName.Trim() != string.Empty)
                    {
                        string specialCharacters = @"%!#$%^&*(-)?/><,:;'\|}]{[~`+=" + "\"";
                        char[] specialCharactersArray = specialCharacters.ToCharArray();
                        int index = value.Email.IndexOfAny(specialCharactersArray);
                        //index == -1 no special characters
                        if (index == -1)
                        {

                        }
                        else
                        {
                            Errors.Add("UserName & EmailID allows only  '_' '.' '@' ");
                        }
                        if (regexSpace.IsMatch(value.Email))
                        {
                            Errors.Add("Email having space,Email does not accept space");
                        }
                    }
                    if (value.Password.Trim() == value.NewPassword)
                    {
                        Errors.Add("Old and New password are equal,please give another password");
                    }
                    if (value.NewPassword.Trim() == value.RePassword)
                    {
                        Errors.Add("New and Re password are Not equal,please give equal password");
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
 
        #endregion

                   
               
    }
}
