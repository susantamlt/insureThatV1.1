using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InsureThatAPI.Models;
using InsureThatAPI.CommonMethods;
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;
using System.Text.RegularExpressions;

namespace InsureThatAPI.Controllers
{
    public class UserDetailsController : ApiController
    {
        // GET: api/UserDetails
        // GET: api/UserDetails
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        ///  GET USER DETAILS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        #region  GET METHOD FOR USER DETAILS
        // GET: api/UserDetails/5
        public GetUserDetailsRef Get(int id)
        {
            GetUserDetailsRef userDetailsref = new GetUserDetailsRef();
            UserDetailsClass userDetails = new UserDetailsClass();
            try
            {
                userDetailsref = userDetails.GetUserDetails(id);
                return userDetailsref;
            }
            catch (Exception xp)
            {

            }
            finally
            {

            }
            return userDetailsref;
        }
        #endregion


        #region INSERT USER DETAILS POST METHOD
        // POST: api/UserDetails
        public UserDetailsRef Post([FromBody]UserDetails value)
        {
            UserDetailsRef userDetailsref = new UserDetailsRef();
            UserDetailsClass userDetails = new UserDetailsClass();
            List<string> Errors = new List<string>();
            Regex regemail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$"); // for confirm special charectors or not
            userDetailsref.ErrorMessage = new List<string>();
            if (value.UserName == null || value.UserName == string.Empty || string.IsNullOrWhiteSpace(value.UserName.Trim()))
            {
                Errors.Add("User Name is required");
            }
            else
            {
                string justNumber = new String(value.UserName.Trim().Where(Char.IsDigit).ToArray());
                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                if (value.UserName.Length > 20)
                {
                    Errors.Add("User Name should not be greater than 20 characters.");
                }
                if (!regexItem.IsMatch(value.UserName.Trim()))
                {
                    Errors.Add("Special characters are not allowed in User Name");

                }

                if (justNumber != null && justNumber != string.Empty)
                {
                    Errors.Add("User Name does not allow numerc values.");
                }
            }
            if (value.FirstName == null || value.FirstName == string.Empty || string.IsNullOrWhiteSpace(value.FirstName.Trim()))
            {
                Errors.Add("First Name is required");
            }
            else
            {
                string justNumber = new String(value.FirstName.Trim().Where(Char.IsDigit).ToArray());
                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                if (value.FirstName.Length > 20)
                {
                    Errors.Add("First Name should not be greater than 20 characters.");
                }
                if (!regexItem.IsMatch(value.FirstName.Trim()))
                {
                    Errors.Add("Special characters are not allowed in First Name");

                }

                if (justNumber != null && justNumber != string.Empty)
                {
                    Errors.Add("First Name does not allow numerc values.");
                }
            }
            if (value.LastName == null || value.LastName == string.Empty || string.IsNullOrWhiteSpace(value.LastName.Trim()))
            {
                Errors.Add("Last Name is required");
            }
            else
            {
                string justNumber = new String(value.LastName.Trim().Where(Char.IsDigit).ToArray());
                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                if (value.LastName.Length > 20)
                {
                    Errors.Add("Last Name should not be greater than 20 characters.");
                }
                if (!regexItem.IsMatch(value.LastName.Trim()))
                {
                    Errors.Add("Special characters are not allowed in Last Name");

                }

                if (justNumber != null && justNumber != string.Empty)
                {
                    Errors.Add("Last Name does not allow numerc values.");
                }
            }
            if (value.AddressID == null || value.AddressID <= 0)
            {
                Errors.Add("AddressID is required");
            }
            else
            {
                if (!regexItem.IsMatch(value.AddressID.ToString()))
                {
                    Errors.Add("Special characters are not allowed in AddressID ");
                }
            }
            if (value.PostalAddressID == null || value.PostalAddressID <= 0)
            {
                Errors.Add("Postal AddressID is required");
            }
            else
            {

                if (!regexItem.IsMatch(value.PostalAddressID.ToString()))
                {
                    Errors.Add("Special characters are not allowed in Postal AddressID ");
                }
            }
            if (value.PhoneNo == null || value.PhoneNo == string.Empty || string.IsNullOrWhiteSpace(value.PhoneNo.Trim()))
            {
                Errors.Add("Phone Number is required");
                if (value.PhoneNo.Count() > (int)InsuredResult.PhoneNumberLength || value.PhoneNo.Count() < (int)InsuredResult.PhoneNumberLength)
                {
                    Errors.Add("Phone Number is required, must not be more than 9 digits and less than 9 digits.");
                }

            }
            else
            {
                var regexSpace = new Regex(@"\s");
                if (regexSpace.IsMatch(value.PhoneNo))
                {
                    Errors.Add("Phone Number Having Space, must not be more than 9 digits and less than 9 digits.");
                }

                string justNumber = new String(value.PhoneNo.Trim().Where(Char.IsDigit).ToArray());
                string justStrings = new String(value.PhoneNo.Trim().Where(Char.IsLetter).ToArray());
                if (justStrings != null && justNumber.Length != 9 && value.PhoneNo.Length != 9)
                {
                    Errors.Add("Phone number allows only numerc values, must not be more than 9 digits and less than 9 digits.");
                }

                if (!regexItem.IsMatch(value.PhoneNo.Trim()))
                {
                    Errors.Add("Special characters are not allowed in Phone number");
                }
                if (justStrings != "")
                {
                    Errors.Add("Phone number allows only numerc values.");
                }

                if (value.PhoneNo.Count() > (int)InsuredResult.PhoneNumberLength || value.PhoneNo.Count() < (int)InsuredResult.PhoneNumberLength)
                {
                    Errors.Add("Phone Number is required, must not be more than 9 digits and less than 9 digits.");
                }
            }
            if (value.MobileNo == null || value.MobileNo == string.Empty || string.IsNullOrWhiteSpace(value.MobileNo.Trim()))
            {

                Errors.Add("Mobile Number is required");
                if (value.MobileNo.Count() > (int)InsuredResult.PhoneNumberLength || value.MobileNo.Count() < (int)InsuredResult.PhoneNumberLength)
                {
                    Errors.Add("Mobile Number is required, must not be more than 9 digits and less than 9 digits.");
                }
            }
            else
            {
                var regexSpace = new Regex(@"\s");
                if (regexSpace.IsMatch(value.MobileNo))
                {
                    Errors.Add("Mobile Number Having Space, must not be more than 9 digits and less than 9 digits.");
                }
                string justNumber = new String(value.MobileNo.Trim().Where(Char.IsDigit).ToArray());
                string justStrings = new String(value.MobileNo.Trim().Where(Char.IsLetter).ToArray());
                if (justStrings != null && justNumber.Length != (int)InsuredResult.PhoneNumberLength && value.MobileNo.Length != (int)InsuredResult.PhoneNumberLength)
                {
                    Errors.Add("mobile number allows only numerc values, must not be more than 9 digits and less than 9 digits.");
                }

                if (!regexItem.IsMatch(value.MobileNo.Trim()))
                {
                    Errors.Add("Special characters are not allowed in Mobile number");
                }
                if (justStrings != "")
                {
                    Errors.Add("Mobile number allows only numerc values.");
                }
                if (value.MobileNo.Count() > (int)InsuredResult.PhoneNumberLength || value.MobileNo.Count() < (int)InsuredResult.PhoneNumberLength)
                {
                    Errors.Add("Mobile Number is required, must not be more than 9 digits and less than 9 digits.");
                }
            }
            if (value.DOB == null)
            {
                Errors.Add("DOB is required");
            }
            else
            {
                //if (value.DOB == DateTime.Now.Date)
                //{

                //}
                //else
                //{
                //    Errors.Add("DOB should not be greater than todays date.");
                //}
                string justStrings = value.DOB.ToString();
                if (justStrings == default(DateTime).ToString())
                {
                    Errors.Add("DOB is not valid. DOB format is MM/dd/yyyy");
                }
                else
                {
                    string inputString = justStrings; //value.DOB.ToString("MM/dd/yyyy");
                    DateTime dDate;
                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        //valid
                    }
                    else
                    {
                        Errors.Add("DOB is not valid. DOB format is MM/dd/yyyy");
                    }
                }
            }
            if (value.MemberOf == null || value.MemberOf == string.Empty || string.IsNullOrWhiteSpace(value.MemberOf.Trim()))
            {
                Errors.Add("MemberOf is required");
            }
            else
            {
                string justNumber = new String(value.MemberOf.Trim().Where(Char.IsDigit).ToArray());
                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                if (value.MemberOf.Length > 20)
                {
                    Errors.Add("MemberOf should not be greater than 20 characters.");
                }
                if (!regexItem.IsMatch(value.MemberOf.Trim()))
                {
                    Errors.Add("Special characters are not allowed in MemberOf");

                }

                if (justNumber != null && justNumber != string.Empty)
                {
                    Errors.Add("MemberOf does not allow numerc values.");
                }
            }

            if (value.MembershipNumber == null || value.MembershipNumber == string.Empty || string.IsNullOrWhiteSpace(value.MembershipNumber.Trim()))
            {
                Errors.Add("MembershipNumber is required");
            }
            else
            {
                string justNumber = new String(value.MembershipNumber.Trim().Where(Char.IsDigit).ToArray());
                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                if (value.MembershipNumber.Length > 20)
                {
                    Errors.Add("MembershipNumber should not be greater than 20 characters.");
                }
                if (!regexItem.IsMatch(value.MembershipNumber.Trim()))
                {
                    Errors.Add("Special characters are not allowed in MembershipNumber");

                }

                if (justNumber != null && justNumber != string.Empty)
                {
                    Errors.Add("MembershipNumber does not allow numerc values.");
                }
            }
            if (value.EmailID == null || value.EmailID == string.Empty || string.IsNullOrWhiteSpace(value.EmailID.Trim()))
            {
                Errors.Add("EmailID is required");
            }
            if (value.EmailID != null && !regemail.IsMatch(value.EmailID))
            {
                Errors.Add("EmailID is not valid");
            }
            if (value.EmailID != null && value.EmailID.Length > 50)
            {
                Errors.Add("Length of EmailId should not exceed 50 characters.");
            }

            if (Errors != null && Errors.Count() > 0)
            {
                userDetailsref.Status = "Failure";
                userDetailsref.ErrorMessage = Errors;
                return userDetailsref;
            }
            else
            {
                int? result = userDetails.InsertUpdateUserDetails(null, value);
                if (result.HasValue && result > 0)
                {

                    userDetailsref.Status = "Success";
                    userDetailsref.UserID = result.Value;
                }
                else if (result.HasValue && result == (int)InsuredResult.Exception)
                {
                    userDetailsref.Status = "Failure";
                    userDetailsref.ErrorMessage.Add("Failed to insert.");

                }

                else if (result.HasValue && result == (int)InsuredResult.EmailAlreadyExists)
                {
                    userDetailsref.Status = "Failure";
                    userDetailsref.ErrorMessage.Add("Email Id already exists.");

                }
            }
            return userDetailsref;
        }

        #endregion


        #region UPDATE USER DETAILS PUT METHOD
        // PUT: api/UserDetails/5
        public UserDetailsRef Put(int id, [FromBody]UserDetails value)
        {
            UserDetailsRef userDetailsref = new UserDetailsRef();
            UserDetailsClass userDetails = new UserDetailsClass();
            List<string> Errors = new List<string>();
            userDetailsref.ErrorMessage = new List<string>();
            Regex regemail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$"); // for confirm special charectors or not
            if (value.UserName == null || value.UserName == string.Empty || string.IsNullOrWhiteSpace(value.UserName.Trim()))
            {
                Errors.Add("User Name is required");
            }
            else
            {
                string justNumber = new String(value.UserName.Trim().Where(Char.IsDigit).ToArray());
                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                if (value.UserName.Length > 20)
                {
                    Errors.Add("User Name should not be greater than 20 characters.");
                }
                if (!regexItem.IsMatch(value.UserName.Trim()))
                {
                    Errors.Add("Special characters are not allowed in User Name");

                }

                if (justNumber != null && justNumber != string.Empty)
                {
                    Errors.Add("User Name does not allow numerc values.");
                }
            }
            if (value.FirstName == null || value.FirstName == string.Empty || string.IsNullOrWhiteSpace(value.FirstName.Trim()))
            {
                Errors.Add("First Name is required");
            }
            else
            {
                string justNumber = new String(value.FirstName.Trim().Where(Char.IsDigit).ToArray());
                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                if (value.FirstName.Length > 20)
                {
                    Errors.Add("First Name should not be greater than 20 characters.");
                }
                if (!regexItem.IsMatch(value.FirstName.Trim()))
                {
                    Errors.Add("Special characters are not allowed in First Name");

                }

                if (justNumber != null && justNumber != string.Empty)
                {
                    Errors.Add("First Name does not allow numerc values.");
                }
            }
            if (value.LastName == null || value.LastName == string.Empty || string.IsNullOrWhiteSpace(value.LastName.Trim()))
            {
                Errors.Add("Last Name is required");
            }
            else
            {
                string justNumber = new String(value.LastName.Trim().Where(Char.IsDigit).ToArray());
                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                if (value.LastName.Length > 20)
                {
                    Errors.Add("Last Name should not be greater than 20 characters.");
                }
                if (!regexItem.IsMatch(value.LastName.Trim()))
                {
                    Errors.Add("Special characters are not allowed in Last Name");

                }

                if (justNumber != null && justNumber != string.Empty)
                {
                    Errors.Add("Last Name does not allow numerc values.");
                }
            }
            if (value.AddressID == null || value.AddressID <= 0)
            {
                Errors.Add("AddressID is required");
            }
            else
            {
                if (!regexItem.IsMatch(value.AddressID.ToString()))
                {
                    Errors.Add("Special characters are not allowed in AddressID ");
                }
            }
            if (value.PostalAddressID == null || value.PostalAddressID <= 0)
            {
                Errors.Add("Postal AddressID is required");
            }
            else
            {

                if (!regexItem.IsMatch(value.PostalAddressID.ToString()))
                {
                    Errors.Add("Special characters are not allowed in Postal AddressID ");
                }
            }

            if (value.PhoneNo == null || value.PhoneNo == string.Empty || string.IsNullOrWhiteSpace(value.PhoneNo.Trim()))
            {
                Errors.Add("Phone Number is required");
                if (value.PhoneNo.Count() > (int)InsuredResult.PhoneNumberLength || value.PhoneNo.Count() < (int)InsuredResult.PhoneNumberLength)
                {
                    Errors.Add("Phone Number is required, must not be more than 9 digits and less than 9 digits.");
                }

            }
            else
            {
                var regexSpace = new Regex(@"\s");
                if (regexSpace.IsMatch(value.PhoneNo))
                {
                    Errors.Add("Phone Number Having Space, must not be more than 9 digits and less than 9 digits.");
                }

                string justNumber = new String(value.PhoneNo.Trim().Where(Char.IsDigit).ToArray());
                string justStrings = new String(value.PhoneNo.Trim().Where(Char.IsLetter).ToArray());
                if (justStrings != null && justNumber.Length != 9 && value.PhoneNo.Length != 9)
                {
                    Errors.Add("Phone number allows only numerc values, must not be more than 9 digits and less than 9 digits.");
                }

                if (!regexItem.IsMatch(value.PhoneNo.Trim()))
                {
                    Errors.Add("Special characters are not allowed in Phone number");
                }
                if (justStrings != "")
                {
                    Errors.Add("Phone number allows only numerc values.");
                }

                if (value.PhoneNo.Count() > (int)InsuredResult.PhoneNumberLength || value.PhoneNo.Count() < (int)InsuredResult.PhoneNumberLength)
                {
                    Errors.Add("Phone Number is required, must not be more than 9 digits and less than 9 digits.");
                }
            }

            if (value.MobileNo == null || value.MobileNo == string.Empty || string.IsNullOrWhiteSpace(value.MobileNo.Trim()))
            {

                Errors.Add("Mobile Number is required");
                if (value.MobileNo.Count() > (int)InsuredResult.PhoneNumberLength || value.MobileNo.Count() < (int)InsuredResult.PhoneNumberLength)
                {
                    Errors.Add("Mobile Number is required, must not be more than 9 digits and less than 9 digits.");
                }
            }
            else
            {
                var regexSpace = new Regex(@"\s");
                if (regexSpace.IsMatch(value.MobileNo))
                {
                    Errors.Add("Mobile Number Having Space, must not be more than 9 digits and less than 9 digits.");
                }
                string justNumber = new String(value.MobileNo.Trim().Where(Char.IsDigit).ToArray());
                string justStrings = new String(value.MobileNo.Trim().Where(Char.IsLetter).ToArray());
                if (justStrings != null && justNumber.Length != (int)InsuredResult.PhoneNumberLength && value.MobileNo.Length != (int)InsuredResult.PhoneNumberLength)
                {
                    Errors.Add("mobile number allows only numerc values, must not be more than 9 digits and less than 9 digits.");
                }

                if (!regexItem.IsMatch(value.MobileNo.Trim()))
                {
                    Errors.Add("Special characters are not allowed in Mobile number");
                }
                if (justStrings != "")
                {
                    Errors.Add("Mobile number allows only numeric values.");
                }
                if (value.MobileNo.Count() > (int)InsuredResult.PhoneNumberLength || value.MobileNo.Count() < (int)InsuredResult.PhoneNumberLength)
                {
                    Errors.Add("Mobile Number is required, must not be more than 9 digits and less than 9 digits.");
                }
            }
            if (value.DOB == null)
            {
                Errors.Add("DOB is required");
            }
            else
            {
                //if (value.DOB == DateTime.Now.Date)
                //{

                //}
                //else
                //{
                //    Errors.Add("DOB should not be greater than todays date.");
                //}
                string justStrings = value.DOB.ToString();
                if (justStrings == default(DateTime).ToString())
                {
                    Errors.Add("DOB is not valid. DOB format is MM/dd/yyyy");
                }
                else
                {
                    string inputString = justStrings; //value.DOB.ToString("MM/dd/yyyy");
                    DateTime dDate;
                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        //valid
                    }
                    else
                    {
                        Errors.Add("DOB is not valid. DOB format is MM/dd/yyyy");
                    }
                }
            }

            if (value.MemberOf == null || value.MemberOf == string.Empty || string.IsNullOrWhiteSpace(value.MemberOf.Trim()))
            {
                Errors.Add("MemberOf is required");
            }
            else
            {
                string justNumber = new String(value.MemberOf.Trim().Where(Char.IsDigit).ToArray());
                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                if (value.MemberOf.Length > 20)
                {
                    Errors.Add("MemberOf should not be greater than 20 characters.");
                }
                if (!regexItem.IsMatch(value.MemberOf.Trim()))
                {
                    Errors.Add("Special characters are not allowed in MemberOf");

                }

                if (justNumber != null && justNumber != string.Empty)
                {
                    Errors.Add("MemberOf does not allow numerc values.");
                }
            }

            if (value.MembershipNumber == null || value.MembershipNumber == string.Empty || string.IsNullOrWhiteSpace(value.MembershipNumber.Trim()))
            {
                Errors.Add("MembershipNumber is required");
            }
            else
            {
                string justNumber = new String(value.MembershipNumber.Trim().Where(Char.IsDigit).ToArray());
                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                if (value.MembershipNumber.Length > 20)
                {
                    Errors.Add("MembershipNumber should not be greater than 20 characters.");
                }
                if (!regexItem.IsMatch(value.MembershipNumber.Trim()))
                {
                    Errors.Add("Special characters are not allowed in MembershipNumber");
                }
                if (justNumber != null && justNumber != string.Empty)
                {
                    Errors.Add("MembershipNumber does not allow numerc values.");
                }
            }
            if (value.EmailID == null || value.EmailID == string.Empty || string.IsNullOrWhiteSpace(value.EmailID.Trim()))
            {
                Errors.Add("EmailID is required");
            }
            if (value.EmailID != null && !regemail.IsMatch(value.EmailID))
            {
                Errors.Add("EmailID is not valid");
            }
            if (value.EmailID != null && value.EmailID.Length > 50)
            {
                Errors.Add("Length of EmailId should not exceed 50 characters.");
            }

            if (Errors != null && Errors.Count() > 0)
            {
                userDetailsref.Status = "Failure";
                userDetailsref.ErrorMessage = Errors;
                return userDetailsref;
            }
            else
            {
                if (id != null && id > 0)
                {
                    int? result = userDetails.InsertUpdateUserDetails(id, value);
                    if (result.HasValue && result == 2)
                    {
                        userDetailsref.Status = "Success";
                        userDetailsref.UserID = id;
                    }
                    else if (result.HasValue && result == (int)InsuredResult.Exception)
                    {
                        userDetailsref.Status = "Failure";
                        userDetailsref.ErrorMessage.Add("Failed to insert.");
                    }
                    else if (result.HasValue && result == 3)
                    {
                        userDetailsref.Status = "Failure";
                        userDetailsref.ErrorMessage.Add("User ID is not valid.");
                    }
                }
                else
                {
                    userDetailsref.Status = "Failure";
                    userDetailsref.ErrorMessage.Add("User ID is required.");

                }

            }
            return userDetailsref;
        }

        #endregion
        // DELETE: api/UserDetails/5
        public void Delete(int id)
        {
        }
    }
}
