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
    public class InsuredDetailsController : ApiController
    {
        // GET: api/InsuredDetails
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "Enter valid URL", "Enter search parameters emailId,name,Phoneno" };
        //}
        /// <summary>
        /// Get customer details by searching through email id
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        // GET: api/InsuredDetails/5
        #region GET CUSTOMER DETAILS BY SEARCHING THROUGH EMAILID

        //[HttpGet]
        //public GetInsuredDetailsRef Get(string emailId)
        //{
        //    //Insured insured = InsuredBo.Api_GetInsuredDetails(emailId, "", "");

        //    //InsuredDetails insuredDetails = new InsuredDetails(insured.PaId, (int)insured.EntityTypeId, insured.Title, insured.FirstName, insured.MiddleName, insured.LastName, insured.CompanyName, insured.TradingName, insured.ABN, insured.AdId, insured.PostalAdId, insured.Mobile, insured.Phone, insured.DateOfBirth, insured.EmailAddress);

        //    //string status = (insured.PaId == 0 ? "Failure" : "Success");

        //    //List<string> errorMessages = new List<string>();
        //    //if (status == "Failure")
        //    //    errorMessages.Add("No data available");
        //    GetInsuredDetailsRef model = new GetInsuredDetailsRef();
        //    //GetInsuredDetailsRef insuredDetailsRef = new GetInsuredDetailsRef(insuredDetails, status, errorMessages);
        //    // GetInsuredDetailsRef insuredref = new GetInsuredDetailsRef();
        //    string name = null;
        //    string phoneno = null;
        //    InsuredDetailsClass insureddetails = new InsuredDetailsClass();
        //    model = insureddetails.GetInsuredDetails(emailId, name, phoneno);
        //    //    return insuredref;

        //    return model;
        //}


        [HttpGet]
        public HttpResponseMessage Get(string emailId = "", string name = "", string phoneno = "")
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$"); // for confirm special charectors or not
            Regex regemail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            GetInsuredDetailsRef insuredref = new GetInsuredDetailsRef();
            List<string> Errors = new List<string>();
            insuredref.ErrorMessage = new List<string>();
            if (emailId != null && emailId != "" && !regemail.IsMatch(emailId))
            {
                Errors.Add("Email ID is not valid");
            }
            else if (emailId != null && emailId.Length > 50)
            {
                Errors.Add("Length of Email Id should not exceed 50 characters.");
            }
            string specialCharacters = @"%!#$%^&*()?/><,:;'\|}]{[~`+=" + "\"";
            char[] specialCharactersArray = specialCharacters.ToCharArray();
            int index = emailId.IndexOfAny(specialCharactersArray);
            //index == -1 no special characters
            if (index == -1)
            {

            }
            else
            {
                Errors.Add("Email ID allows only  '_' '.' '-' '@' ");
            }


            // Regex sWhitespace = new Regex(@"\s+");
            //  string str = sWhitespace.Replace(UserName, string.Empty);
            if (phoneno != null && phoneno != "")
            {
                var regexSpace = new Regex(@"\s");
                if (regexSpace.IsMatch(phoneno))
                {
                    Errors.Add("Phone Number having space, must not be more than 10 digits and less than 10 digits.");
                }
                string justNumber = new String(phoneno.Trim().Where(Char.IsDigit).ToArray());
                string justStrings = new String(phoneno.Trim().Where(Char.IsLetter).ToArray());
                if (justStrings != null && justStrings != string.Empty && justNumber.Length != 10 && phoneno.Length != 10)
                {
                    Errors.Add("Phone number allows only numerc values, must not be more than 10 digits and less than 10 digits.");
                }

                if (!regexItem.IsMatch(phoneno.Trim()))
                {
                    Errors.Add("Special characters are not allowed in Phone number");
                }
                if (justStrings != "")
                {
                    Errors.Add("Phone number allows only numeric values.");
                }

                if (phoneno.Count() > (int)InsuredResult.PhoneNumberLength || phoneno.Count() < (int)InsuredResult.PhoneNumberLength)
                {
                    Errors.Add("Phone Number is required, must not be more than 10 digits and less than 10 digits.");
                }
            }
            if ((emailId != null && emailId != "") || (name != null && name != "") || (phoneno != null && phoneno != ""))
            {
            }
            else
            {
                Errors.Add("Email Id or Phone Number or Name any one are mandatory for searching Insured Details");
            }
            if (Errors != null && Errors.Count() > 0)
            {
                insuredref.Status = "Failure";
                insuredref.ErrorMessage = Errors;
                return Request.CreateResponse<GetInsuredDetailsRef>(HttpStatusCode.BadRequest, insuredref);
            }
            else
            {
                InsuredDetailsClass insureddetails = new InsuredDetailsClass();
                if ((emailId != null && emailId != "") || (name != null && name != "") || (phoneno != null && phoneno != ""))
                {
                    insuredref = insureddetails.GetInsuredDetails(emailId, name, phoneno);
                }
                else
                { Errors.Add("Email Id or Phone Number or Name any one are mandatory for searching Insured Details"); }
                return Request.CreateResponse<GetInsuredDetailsRef>(HttpStatusCode.OK, insuredref);
            }
            return null;
        }


        //[HttpGet]
        //public GetInsuredDetailsRef Get(string emailId="", string name="", string phoneno="")
        //{

        //    GetInsuredDetailsRef insuredref = new GetInsuredDetailsRef();
        //    InsuredDetailsClass insureddetails = new InsuredDetailsClass();
        //    insuredref = insureddetails.GetInsuredDetails(emailId, name, phoneno);
        //    return insuredref;
        //}
        #endregion

        // POST: api/InsuredDetails
        public HttpResponseMessage Post([FromBody]InsuredDetails value)
        {
            try
            {
                InsuredDetailsClass insureddetails = new InsuredDetailsClass();
                EnumInsuredDetails.InsuredResult resultEnum = new EnumInsuredDetails.InsuredResult();
                Regex regemail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                var regexItem = new Regex("^[a-zA-Z0-9 ]*$"); // for confirm special charectors or not
                InsuredDetailsRef insuredref = new InsuredDetailsRef();
                MasterDataEntities db = new MasterDataEntities();
                List<string> Errors = new List<string>();
                insuredref.ErrorMessage = new List<string>();
                if (value != null)
                {

                    if (value.ABN == null || value.ABN == string.Empty || string.IsNullOrWhiteSpace(value.ABN.Trim()))
                    {
                        Errors.Add("ABN is required");
                    }
                    else
                    {

                        string justStrings = new String(value.ABN.Trim().Where(Char.IsLetter).ToArray());
                        if (!regexItem.IsMatch(value.ABN.Trim()))
                        {
                            Errors.Add("Special characters are not allowed in ABN");
                        }
                        if (justStrings != "")
                        {
                            Errors.Add("ABN allows only numeric values.");
                        }
                        if (value.ABN.Length > 11 || value.ABN.Length < 11)
                        {
                            Errors.Add("ABN should not be greater than 11 characters or less than 11.");
                        }
                        var regexSpace = new Regex(@"\s");
                        if (regexSpace.IsMatch(value.ABN))
                        {
                            Errors.Add("ABN should not have space");
                        }
                        #region ABN Validation
                        if (justStrings == "" && value.ABN.Length == 11)
                        {
                            char[] ABN = value.ABN.ToCharArray();

                            string message = value.ABN;
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
                                Errors.Add("ABN is not valid.");
                            }
                        }
                        #endregion
                    }

                    if (value.EmailID == null || value.EmailID == string.Empty || string.IsNullOrWhiteSpace(value.EmailID.Trim()))
                    {
                        Errors.Add("EmailID is required");
                    }
                    else
                    {
                        if (value.EmailID != null && !regemail.IsMatch(value.EmailID))
                        {
                            Errors.Add("EmailID is not valid");
                        }
                        else if (value.EmailID != null && value.EmailID.Length > 50)
                        {
                            Errors.Add("Length of EmailId should not exceed 50 characters.");
                        }
                        string specialCharacters = @"%!#$%^&*()?/><,:;'\|}]{[~`+=" + "\"";
                        char[] specialCharactersArray = specialCharacters.ToCharArray();
                        int index = value.EmailID.IndexOfAny(specialCharactersArray);
                        //index == -1 no special characters
                        if (index == -1)
                        {

                        }
                        else
                        {
                            Errors.Add("EmailID allows only  '_' '.' '-' '@' ");
                        }
                    }
                    if (value.ClientType == null || value.ClientType <= 0)
                    {
                        Errors.Add("Client Type is required, allows only numeric value.");
                    }

                    //if (value.Title == null || value.Title == string.Empty || string.IsNullOrWhiteSpace(value.Title.Trim()))
                    //{
                    //    Errors.Add("Title is required");
                    //}
                    //else
                    if (value.Title != null && value.Title != string.Empty)
                    {
                        if (value.ClientType==2 && value.Title != null && value.Title != string.Empty)
                        {
                            Errors.Add("Company should not have Title.");
                        }
                        string justNumber = new String(value.Title.Trim().Where(Char.IsDigit).ToArray());
                        // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                        if (value.Title == "MR" || value.Title == "Mr" || value.Title == "mr" || value.Title == "miss" || value.Title == "ms" || value.Title == "dr" || value.Title == "Mrs" || value.Title == "MRS" || value.Title == "Miss" || value.Title == "MISS" || value.Title == "Ms" || value.Title == "MS" || value.Title == "Dr" || value.Title == "DR")
                        {

                        }
                        else
                        {
                            Errors.Add("Title allows only MR, Miss, MRS, MS, DR");
                        }
                        if (!regexItem.IsMatch(value.Title.Trim()))
                        {
                            Errors.Add("Special characters are not allowed in Title");

                        }
                        if (value.Title.Length > 20)
                        {
                            Errors.Add("Title should not be greater than 20 characters.");
                        }
                        if (justNumber != null && justNumber != string.Empty)
                        {
                            Errors.Add("Title does not allow numerc values.");
                        }
                    }
                    if ((value.FirstName == null || value.FirstName == string.Empty || string.IsNullOrWhiteSpace(value.FirstName.Trim())) && value.CompanyBusinessName == null && value.ClientType==1)
                    {
                        Errors.Add("First Name is required");

                    }
                    else
                    {
                        if (value.ClientType==2 && (value.FirstName != null && value.FirstName != string.Empty))
                        {
                            Errors.Add("Company should not have First Name.");
                        }
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
                    if ((value.LastName == null || value.LastName == string.Empty || string.IsNullOrWhiteSpace(value.LastName.Trim())) && value.CompanyBusinessName == null && value.ClientType == 1)
                    {
                        Errors.Add("Last Name is required");
                    }
                    else
                    {
                        if (value.ClientType==2 &&(value.LastName != null && value.LastName != string.Empty))
                        {
                            Errors.Add("Company should not have Last Name.");
                        }
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
                    //if (value.MiddleName == null || value.MiddleName == string.Empty || string.IsNullOrWhiteSpace(value.MiddleName.Trim()))
                    //{
                    //    Errors.Add("Middle Name is required");
                    //}
                    //else
                    if (value.MiddleName != null && value.MiddleName != string.Empty && (value.CompanyBusinessName == null && value.CompanyBusinessName == string.Empty && value.ClientType == 1))
                    {

                        string justNumber = new String(value.MiddleName.Trim().Where(Char.IsDigit).ToArray());
                        // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                        if (value.MiddleName.Length > 20)
                        {
                            Errors.Add("Middle Name should not be greater than 20 characters.");
                        }
                        if (!regexItem.IsMatch(value.LastName.Trim()))
                        {
                            Errors.Add("Special characters are not allowed in Middle Name");

                        }

                        if (justNumber != null && justNumber != string.Empty)
                        {
                            Errors.Add("Middle Name does not allow numerc values.");
                        }
                    }
                    else if (value.MiddleName != null && value.MiddleName != string.Empty && value.ClientType == 2)
                    {
                        Errors.Add("Company should not have middle name.");
                    }


                    if ((value.CompanyBusinessName == null || value.CompanyBusinessName == string.Empty || string.IsNullOrWhiteSpace(value.CompanyBusinessName.Trim())) && (value.ClientType == 2))
                    {
                        Errors.Add("Company Business Name is required");
                    }
                    else
                    {
                        if (value.ClientType==1 && (value.CompanyBusinessName != null && value.CompanyBusinessName != string.Empty))
                        {
                            Errors.Add("In person should not have Company Name.");
                        }
                        string justNumber = new String(value.CompanyBusinessName.Trim().Where(Char.IsDigit).ToArray());
                        // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                        if (value.CompanyBusinessName.Length > 20)
                        {
                            Errors.Add("Company Business Name should not be greater than 20 characters.");
                        }
                        if (!regexItem.IsMatch(value.CompanyBusinessName.Trim()))
                        {
                            Errors.Add("Special characters are not allowed in Company Business Name");
                        }
                        if (justNumber != null && justNumber != string.Empty)
                        {
                            Errors.Add("Company Business Name does not allow numerc values.");
                        }
                    }
                    if (value.TradingName != null && value.TradingName != string.Empty && (value.CompanyBusinessName != null && value.CompanyBusinessName != string.Empty))
                    {
                        string justNumber = new String(value.TradingName.Trim().Where(Char.IsDigit).ToArray());
                        // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                        if (value.TradingName.Length > 20)
                        {
                            Errors.Add("Trading Name should not be greater than 20 characters.");
                        }
                        if (!regexItem.IsMatch(value.TradingName.Trim()))
                        {
                            Errors.Add("Special characters are not allowed in Trading Name");
                        }
                        if (justNumber != null && justNumber != string.Empty)
                        {
                            Errors.Add("Trading Name does not allow numerc values.");
                        }
                    }
                    else if (value.TradingName != null && value.TradingName != string.Empty && (value.ClientType==1))
                    {
                        Errors.Add("In Person should not have Trading Name.");
                    }
                    if ((value.AddressID == null || value.AddressID <= 0) && value.PostalAddressID == null)
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
                    if ((value.PostalAddressID == null || value.PostalAddressID <= 0) && value.AddressID == null)
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
                    if ((value.PhoneNo == null || value.PhoneNo == string.Empty || string.IsNullOrWhiteSpace(value.PhoneNo.Trim())) && (value.MobileNo == null || value.MobileNo == string.Empty))
                    {
                        Errors.Add("Phone Number is required");

                    }
                    else if (value.PhoneNo != null && value.PhoneNo != string.Empty)
                    {

                        var regexSpace = new Regex(@"\s");
                        if (regexSpace.IsMatch(value.PhoneNo))
                        {
                            Errors.Add("Phone Number having space, must not be more than 10 digits and less than 10 digits.");
                        }
                        string justNumber = new String(value.PhoneNo.Trim().Where(Char.IsDigit).ToArray());
                        string justStrings = new String(value.PhoneNo.Trim().Where(Char.IsLetter).ToArray());
                        if (justStrings != null && justStrings != string.Empty && justNumber.Length != 10 && value.PhoneNo.Length != 10)
                        {
                            Errors.Add("Phone number allows only numerc values, must not be more than 10 digits and less than 10 digits.");
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
                            Errors.Add("Phone Number is required, must not be more than 10 digits and less than 10 digits.");
                        }
                    }


                    if ((value.MobileNo == null || value.MobileNo == string.Empty || string.IsNullOrWhiteSpace(value.MobileNo.Trim())) && (value.PhoneNo == null || value.PhoneNo == string.Empty))
                    {

                        Errors.Add("Mobile Number is required");


                    }
                    else if (value.MobileNo != null && value.MobileNo != string.Empty)
                    {
                        var regexSpace = new Regex(@"\s");
                        if (regexSpace.IsMatch(value.MobileNo))
                        {
                            Errors.Add("Mobile Number having Space, must not be more than 10 digits and less than 10 digits.");
                        }
                        string justNumber = new String(value.MobileNo.Trim().Where(Char.IsDigit).ToArray());
                        string justStrings = new String(value.MobileNo.Trim().Where(Char.IsLetter).ToArray());
                        if (justStrings != null && justStrings != string.Empty && justNumber.Length != (int)InsuredResult.PhoneNumberLength && value.MobileNo.Length != (int)InsuredResult.PhoneNumberLength)
                        {
                            Errors.Add("Mobile number allows only numerc values, must not be more than 10 digits and less than 10 digits.");
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
                            Errors.Add("Mobile Number is required, must not be more than 10 digits and less than 10 digits.");
                        }
                    }
                    if (value.DOB == null)
                    {
                        Errors.Add("DOB is required");
                    }
                    else
                    {
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
                            DateTime dateExp = DateTime.Now.Date;
                            int resultd = DateTime.Compare(value.DOB, dateExp);
                            if (resultd < 0)
                            {
                            }
                            else if (resultd == 0)
                            {
                            }
                            else
                            {
                                Errors.Add("Date of Birth should not be future date.");
                            }
                        }

                    }
                    if (Errors != null && Errors.Count() > 0)
                    {
                        insuredref.Status = "Failure";
                        insuredref.ErrorMessage = Errors;
                        return Request.CreateResponse<InsuredDetailsRef>(HttpStatusCode.BadRequest, insuredref);

                    }
                    else
                    {
                        int? result = insureddetails.InsertUpdateInsuredDetails(null, value);
                        if (result.HasValue && result > 0)
                        {

                            insuredref.Status = "Success";
                            insuredref.InsuredID = result.Value;
                            return Request.CreateResponse<InsuredDetailsRef>(HttpStatusCode.OK, insuredref);

                        }
                        if (result.HasValue && result == 2)
                        {

                            insuredref.Status = "Success";
                            insuredref.InsuredID = value.InsuredID;
                            return Request.CreateResponse<InsuredDetailsRef>(HttpStatusCode.OK, insuredref);

                        }
                        else if (result.HasValue && result == -4)
                        {

                            insuredref.Status = "Failure";
                            insuredref.ErrorMessage.Add("Email Id and Phone number already exist.");
                            return Request.CreateResponse<InsuredDetailsRef>(HttpStatusCode.NotAcceptable, insuredref);

                        }

                        else if (result.HasValue && result == (int)InsuredResult.Exception)
                        {

                            insuredref.Status = "Failure";
                            insuredref.ErrorMessage.Add("Failed to insert.");
                            return Request.CreateResponse<InsuredDetailsRef>(HttpStatusCode.NotImplemented, insuredref);

                        }

                        else if (result.HasValue && result == (int)InsuredResult.EmailAlreadyExists)
                        {

                            insuredref.Status = "Failure";
                            insuredref.ErrorMessage.Add("Email Id already exists.");
                            return Request.CreateResponse<InsuredDetailsRef>(HttpStatusCode.NotAcceptable, insuredref);

                        }
                    }

                }

                else
                {
                    insuredref.Status = "Failure";
                    return Request.CreateResponse<InsuredDetailsRef>(HttpStatusCode.NotFound, insuredref);

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
            return null;
        }

        // PUT: api/InsuredDetails/5
        public HttpResponseMessage Put(int id, [FromBody]InsuredDetails value)
        {
            int? result = 0;
            InsuredDetailsClass insureddetails = new InsuredDetailsClass();
            InsuredDetailsRef insuredref = new InsuredDetailsRef();
            List<string> Errors = new List<string>();
            Regex regemail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$"); // for confirm special charectors or not
            insuredref.ErrorMessage = new List<string>();

            if (value != null && id > 0)
            {

                if (value.ABN == null || value.ABN == string.Empty || string.IsNullOrWhiteSpace(value.ABN.Trim()))
                {
                    Errors.Add("ABN is required");
                }
                else
                {
                    string justStrings = new String(value.ABN.Trim().Where(Char.IsLetter).ToArray());
                    if (!regexItem.IsMatch(value.ABN.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in ABN");
                    }
                    if (justStrings != "")
                    {
                        Errors.Add("ABN allows only numerc values.");
                    }
                    if (value.ABN.Length > 11 && value.ABN.Length < 11)
                    {
                        Errors.Add("ABN should not be greater than 11 characters or less than 11.");
                    }
                    #region ABN Validation
                    if (justStrings == "" && value.ABN.Length == 11)
                    {
                        char[] ABN = value.ABN.ToCharArray();

                        string message = value.ABN;
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
                            Errors.Add("ABN is not valid.");
                        }

                    }
                    #endregion
                }

                if (value.EmailID == null || value.EmailID == string.Empty || string.IsNullOrWhiteSpace(value.EmailID.Trim()))
                {
                    Errors.Add("EmailID is required");
                }
                else
                {
                    string specialCharacters = @"%!#$%^&*()?/><,:;'\|}]{[~`+=" + "\"";
                    char[] specialCharactersArray = specialCharacters.ToCharArray();
                    int index = value.EmailID.IndexOfAny(specialCharactersArray);
                    //index == -1 no special characters
                    if (index == -1)
                    {

                    }
                    else
                    {
                        Errors.Add("EmailID allows only  '_' '.' '-' '@' ");
                    }
                    if (value.EmailID != null && !regemail.IsMatch(value.EmailID))
                    {
                        Errors.Add("EmailID is not valid");
                    }
                    if (value.EmailID != null && value.EmailID.Length > 50)
                    {
                        Errors.Add("Length of EmailId should not exceed 50 characters.");
                    }
                }

                if (value.Title != null && value.Title != string.Empty)
                {
                    if (value.CompanyBusinessName != null && value.CompanyBusinessName != string.Empty)
                    {
                        Errors.Add("Company should not have Title.");
                    }
                    string justNumber = new String(value.Title.Trim().Where(Char.IsDigit).ToArray());
                    // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());

                    if (value.Title == "MR" || value.Title == "Mr" || value.Title == "mr" || value.Title == "miss" || value.Title == "ms" || value.Title == "dr" || value.Title == "Mrs" || value.Title == "MRS" || value.Title == "Miss" || value.Title == "MISS" || value.Title == "Ms" || value.Title == "MS" || value.Title == "Dr" || value.Title == "DR")
                    {

                    }
                    else
                    {
                        Errors.Add("Title allows only MR, Miss, MRS, MS, DR");
                    }
                    if (!regexItem.IsMatch(value.Title.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in Title");

                    }
                    if (value.Title.Length > 20)
                    {
                        Errors.Add("Title should not be greater than 20 characters.");
                    }
                    if (justNumber != null && justNumber != string.Empty)
                    {
                        Errors.Add("Title does not allow numeric values.");
                    }
                }

                if ((value.FirstName == null || value.FirstName == string.Empty || string.IsNullOrWhiteSpace(value.FirstName.Trim())) && value.CompanyBusinessName == null)
                {
                    Errors.Add("First Name is required");

                }
                else
                {
                    if (value.CompanyBusinessName != null && value.CompanyBusinessName != string.Empty && !string.IsNullOrWhiteSpace(value.CompanyBusinessName.Trim()) && (value.FirstName != null && !string.IsNullOrWhiteSpace(value.FirstName.Trim())))
                    {
                        Errors.Add("Company should not have First Name.");
                    }
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
                if ((value.LastName == null || value.LastName == string.Empty || string.IsNullOrWhiteSpace(value.LastName.Trim())) && value.CompanyBusinessName == null)
                {
                    Errors.Add("Last Name is required");
                }
                else
                {
                    if (value.CompanyBusinessName != null && value.CompanyBusinessName != string.Empty && !string.IsNullOrWhiteSpace(value.CompanyBusinessName.Trim()) && (value.LastName != null && !string.IsNullOrWhiteSpace(value.LastName.Trim())))
                    {
                        Errors.Add("Company should not have Last Name.");
                    }
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
                if (value.MiddleName != null && value.MiddleName != string.Empty && (value.CompanyBusinessName == null && value.CompanyBusinessName == string.Empty))
                {

                    string justNumber = new String(value.MiddleName.Trim().Where(Char.IsDigit).ToArray());
                    // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                    if (value.MiddleName.Length > 20)
                    {
                        Errors.Add("Middle Name should not be greater than 20 characters.");
                    }
                    if (!regexItem.IsMatch(value.LastName.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in Middle Name");

                    }

                    if (justNumber != null && justNumber != string.Empty)
                    {
                        Errors.Add("Middle Name does not allow numerc values.");
                    }
                }
                else if (value.MiddleName != null && value.MiddleName != string.Empty && (value.CompanyBusinessName != null && value.CompanyBusinessName != string.Empty))
                {
                    Errors.Add("Company Name should not have middle name.");
                }


                if ((value.CompanyBusinessName == null || value.CompanyBusinessName == string.Empty || string.IsNullOrWhiteSpace(value.CompanyBusinessName.Trim())) && (value.FirstName == null || value.FirstName == string.Empty))
                {
                    Errors.Add("Company Business Name is required");
                }
                else
                {
                    if ((value.CompanyBusinessName != null && value.CompanyBusinessName != string.Empty && !string.IsNullOrWhiteSpace(value.CompanyBusinessName.Trim())) && value.FirstName != null && value.FirstName != string.Empty && !string.IsNullOrWhiteSpace(value.FirstName.Trim()))
                    {
                        Errors.Add("In person should not have Company Name.");
                    }
                    string justNumber = new String(value.CompanyBusinessName.Trim().Where(Char.IsDigit).ToArray());
                    // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                    if (value.CompanyBusinessName.Length > 20)
                    {
                        Errors.Add("Company Business Name should not be greater than 20 characters.");
                    }
                    if (!regexItem.IsMatch(value.CompanyBusinessName.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in Company Business Name");
                    }
                    if (justNumber != null && justNumber != string.Empty)
                    {
                        Errors.Add("Company Business Name does not allow numerc values.");
                    }
                }


                //if (value.TradingName == null || value.TradingName == string.Empty || string.IsNullOrWhiteSpace(value.TradingName.Trim()))
                //{
                //    Errors.Add("Trading Name is required");
                //}
                //else
                if (value.TradingName != null && value.TradingName != string.Empty && value.CompanyBusinessName != null && value.CompanyBusinessName != string.Empty)
                {
                    string justNumber = new String(value.TradingName.Trim().Where(Char.IsDigit).ToArray());
                    // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                    if (value.TradingName.Length > 20)
                    {
                        Errors.Add("Trading Name should not be greater than 20 characters.");
                    }
                    if (!regexItem.IsMatch(value.TradingName.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in Trading Name");
                    }
                    if (justNumber != null && justNumber != string.Empty)
                    {
                        Errors.Add("Trading Name does not allow numerc values.");
                    }
                }
                else if (value.TradingName != null && value.TradingName != string.Empty && value.CompanyBusinessName == null && value.CompanyBusinessName == string.Empty)
                {
                    Errors.Add("Company Name should be provided for Trading Name.");
                }
                if ((value.AddressID == null || value.AddressID <= 0) && value.PostalAddressID == null)
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
                if ((value.PostalAddressID == null || value.PostalAddressID <= 0) && value.AddressID == null)
                {
                    Errors.Add("Postal AddressID is required");
                }
                else
                {

                    if (!regexItem.IsMatch(value.AddressID.ToString()))
                    {
                        Errors.Add("Special characters are not allowed in Postal AddressID ");
                    }
                }
                if ((value.PhoneNo == null || value.PhoneNo == string.Empty || string.IsNullOrWhiteSpace(value.PhoneNo.Trim())) && (value.MobileNo == null || value.MobileNo == string.Empty))
                {
                    Errors.Add("Phone Number is required");

                }
                else if (value.PhoneNo != null && value.PhoneNo != string.Empty)
                {
                    var regexSpace = new Regex(@"\s");
                    if (regexSpace.IsMatch(value.PhoneNo))
                    {
                        Errors.Add("Phone Number Having Space, must not be more than 10 digits and less than 10 digits.");
                    }
                    string justNumber = new String(value.PhoneNo.Trim().Where(Char.IsDigit).ToArray());
                    string justStrings = new String(value.PhoneNo.Trim().Where(Char.IsLetter).ToArray());
                    if (justStrings != null && justNumber.Length != 10 && value.PhoneNo.Length != 10)
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
                string justNumbers = new String(value.PhoneNo.Trim().Where(Char.IsDigit).ToArray());// for getting digits from string

                if ((value.MobileNo == null || value.MobileNo == string.Empty || string.IsNullOrWhiteSpace(value.MobileNo.Trim())) && value.PhoneNo == null)
                {

                    Errors.Add("Mobile Number is required");


                }
                else if (value.MobileNo != null && value.MobileNo != string.Empty)
                {
                    var regexSpace = new Regex(@"\s");
                    if (regexSpace.IsMatch(value.MobileNo))
                    {
                        Errors.Add("Mobile Number Having Space, must not be more than 10 digits and less than 10 digits.");
                    }
                    string justNumber = new String(value.MobileNo.Trim().Where(Char.IsDigit).ToArray());
                    string justStrings = new String(value.MobileNo.Trim().Where(Char.IsLetter).ToArray());
                    if (justStrings != null && justNumber.Length != (int)InsuredResult.PhoneNumberLength && value.MobileNo.Length != (int)InsuredResult.PhoneNumberLength)
                    {
                        Errors.Add("mobile number allows only numerc values, must not be more than 10 digits and less than 10 digits.");
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
                        Errors.Add("Mobile Number is required, must not be more than 10 digits and less than 10 digits.");
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
                        DateTime dateExp = DateTime.Now.Date;
                        int resultd = DateTime.Compare(value.DOB, dateExp);
                        if (resultd < 0)
                        {
                        }
                        else if (resultd == 0)
                        {
                        }
                        else
                        {
                            Errors.Add("Date of Birth should not be future date.");
                        }
                    }

                }
                if (Errors != null && Errors.Count() > 0)
                {
                    insuredref.Status = "Failure";
                    insuredref.ErrorMessage = Errors;
                    return Request.CreateResponse<InsuredDetailsRef>(HttpStatusCode.BadRequest, insuredref);
                }
                else
                {
                    if (id > 0)
                    {
                        result = insureddetails.InsertUpdateInsuredDetails(id, value);
                        if (result == (int)InsuredResult.UpdatedSuccess)
                        {
                            insuredref.Status = "Success";
                            return Request.CreateResponse<InsuredDetailsRef>(HttpStatusCode.OK, insuredref);

                        }
                        if (result == (int)InsuredResult.Exception)
                        {
                            insuredref.Status = "Failure";
                            insuredref.ErrorMessage.Add("Failed to insert.");
                            return Request.CreateResponse<InsuredDetailsRef>(HttpStatusCode.NotImplemented, insuredref);


                        }
                        if (result == -5)
                        {
                            insuredref.Status = "Failure";
                            insuredref.ErrorMessage.Add("EmailId is already exist.");
                            return Request.CreateResponse<InsuredDetailsRef>(HttpStatusCode.NotAcceptable, insuredref);

                        }
                        if (result == -6)
                        {
                            insuredref.Status = "Failure";
                            insuredref.ErrorMessage.Add("Insured Id is not valid.");
                            return Request.CreateResponse<InsuredDetailsRef>(HttpStatusCode.NotAcceptable, insuredref);

                        }
                    }
                    else
                    {
                        insuredref.Status = "Failure";
                        insuredref.ErrorMessage.Add("Insured ID is required.");
                        return Request.CreateResponse<InsuredDetailsRef>(HttpStatusCode.NotAcceptable, insuredref);

                    }
                }
            }
            else
            {

                insuredref.Status = "Failure";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
            return null;

        }

        // DELETE: api/InsuredDetails/5
        public void Delete(int id)
        {
        }
    }
}
