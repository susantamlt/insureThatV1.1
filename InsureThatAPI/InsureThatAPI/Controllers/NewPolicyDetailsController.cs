using InsureThatAPI.CommonMethods;
using InsureThatAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;

namespace InsureThatAPI.Controllers
{
    public class NewPolicyDetailsController : ApiController
    {
        // GET: api/NewPolicyDetails
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/NewPolicyDetails


        // GET: api/NewPolicyDetails/5
        public GetNewPolicyDetailsRef Get(int id)
        {
            GetNewPolicyDetailsRef policyDetailsRef = new GetNewPolicyDetailsRef();
            NewPolicyDetailsClass policyDetailsClass = new NewPolicyDetailsClass();
            try
            {
                if (id > 0)
                {
                    policyDetailsRef = policyDetailsClass.GetPolicyDetails(id);
               
                }
                else
                {
                    policyDetailsRef.Status = "Failure";
                    policyDetailsRef.ErrorMessage.Add("Failed to fetch details, enter valid Policy Details ID.");
                }
            }
            catch (Exception xp)
            {
                policyDetailsRef.Status = "Failure";
                policyDetailsRef.ErrorMessage.Add(xp.Message);
            }
            finally
            {

            }
            return policyDetailsRef;
        }

        // POST: api/NewPolicyDetails
        //public NewPolicyDetailsRef Post([FromBody]PolicyDetails value)
        //{
        //    NewPolicyDetailsClass policyDetailsClass = new NewPolicyDetailsClass();
        //    var regexItem = new Regex("^[a-zA-Z0-9 ]*$"); // for confirm special charectors or not
        //    NewPolicyDetailsRef policyDetailsRef = new NewPolicyDetailsRef();
        //    List<string> Errors = new List<string>();
        //    policyDetailsRef.ErrorMessage = new List<string>();
        //    try
        //    {
        //        if (value != null)
        //        {
        //            if (value.PcId == null || value.PcId =="")
        //            {
        //                Errors.Add("Pc Id is required.");
        //            }
        //            else
        //            {
        //                if (!regexItem.IsMatch(value.PcId.ToString()))
        //                {
        //                    Errors.Add("Special characters are not allowed in PC ID ");
        //                }
        //            }
        //            if (value.TrId == null || value.TrId =="")
        //            {
        //                Errors.Add("TR ID is required");
        //            }
        //            else
        //            {
        //                if (!regexItem.IsMatch(value.TrId.ToString()))
        //                {
        //                    Errors.Add("Special characters are not allowed in TR ID ");
        //                }
        //            }
        //            if (value.PolicyNumber == null || value.PolicyNumber == string.Empty || string.IsNullOrWhiteSpace(value.PolicyNumber.Trim()))
        //            {

        //                Errors.Add("Mobile Number is required");


        //            }
        //            else
        //            {
        //                if (value.PolicyNumber.Length > 20)
        //                {
        //                    Errors.Add("Policy number accepts only 20 charactrers.");
        //                }
        //                if (!regexItem.IsMatch(value.PolicyNumber.Trim()))
        //                {
        //                    Errors.Add("Special characters are not allowed in Policy number");
        //                }
        //            }
        //            if (value.Broker == null || value.Broker == string.Empty || string.IsNullOrWhiteSpace(value.Broker.Trim()))
        //            {
        //                Errors.Add("Broker is required");
        //            }
        //            else
        //            {
        //                if (value.Broker.Length > 50)
        //                {
        //                    Errors.Add("Broker accepts only 50 charactrers.");
        //                }
        //                if (!regexItem.IsMatch(value.Broker.Trim()))
        //                {
        //                    Errors.Add("Special characters are not allowed in Broker");
        //                }
        //            }
        //            if (value.AccountManagerID == null || value.AccountManagerID =="")
        //            {
        //                Errors.Add("Account Manager ID is required");
        //            }
        //            else
        //            {

        //                if (!regexItem.IsMatch(value.AccountManagerID.ToString()))
        //                {
        //                    Errors.Add("Special characters are not allowed in AccountManagerID");
        //                }
        //            }
        //            if (value.PolicyStatus == null || value.PolicyStatus =="" || string.IsNullOrWhiteSpace(value.PolicyStatus.ToString().Trim()))
        //            {
        //                Errors.Add("PolicyStatus is required");
        //            }
        //            else
        //            {
        //                string justStrings = new String(value.PolicyStatus.ToString().Trim().Where(Char.IsLetter).ToArray());
        //                if (!regexItem.IsMatch(value.PolicyStatus.ToString().Trim()))
        //                {
        //                    Errors.Add("Special characters are not allowed in PolicyStatus ");
        //                }
        //                if (justStrings != "")
        //                {
        //                    Errors.Add("Policy Status allows only numeric values.");
        //                }
        //            }

        //            if (value.CoverPeriod == null || value.CoverPeriod =="" || string.IsNullOrWhiteSpace(value.CoverPeriod.ToString().Trim()))
        //            {
        //                Errors.Add("CoverPeriod is required");
        //            }
        //            else
        //            {
        //                string justStrings = new String(value.CoverPeriod.ToString().Trim().Where(Char.IsLetter).ToArray());
        //                if (!regexItem.IsMatch(value.CoverPeriod.ToString().Trim()))
        //                {
        //                    Errors.Add("Special characters are not allowed in CoverPeriod ");
        //                }
        //                if (justStrings != "")
        //                {
        //                    Errors.Add("Cover Period allows only numeric values.");
        //                }
        //            }
        //            if (value.CoverPeriodUnit == null || value.CoverPeriodUnit == string.Empty || string.IsNullOrWhiteSpace(value.CoverPeriodUnit.Trim()))
        //            {
        //                Errors.Add("Cover Period Unit is required");
        //            }
        //            else
        //            {
        //                string justNumber = new String(value.CoverPeriodUnit.Trim().Where(Char.IsDigit).ToArray());
        //                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
        //                if (value.CoverPeriodUnit.Length > 20)
        //                {
        //                    Errors.Add("Cover Period Unit should not be greater than 20 characters.");
        //                }
        //                if (!regexItem.IsMatch(value.CoverPeriodUnit.Trim()))
        //                {
        //                    Errors.Add("Special characters are not allowed in Cover Period Unit");

        //                }
        //                if (justNumber != null && justNumber != string.Empty)
        //                {
        //                    Errors.Add("Cover Period Unit does not allow numeric values.");
        //                }
        //            }

        //            if (value.InceptionDate == null || string.IsNullOrWhiteSpace(value.InceptionDate.ToString().Trim()))
        //            {
        //                Errors.Add("Inception Date is required");
        //            }
        //            else
        //            {
        //                string justStrings = value.InceptionDate.ToString().Trim();
        //                if (justStrings == default(DateTime).ToString())
        //                {
        //                    Errors.Add("Inception Date is not valid. Inception Date format is MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    string inputString = justStrings; //value.DOB.ToString("MM/dd/yyyy");
        //                    DateTime dDate;
        //                    if (DateTime.TryParse(inputString, out dDate))
        //                    {
        //                        //valid
        //                    }
        //                    else
        //                    {
        //                        Errors.Add("Inception Date is not valid. Inception Date format is MM/dd/yyyy");
        //                    }
        //                }
        //            }

        //            if (value.ExpiryDate == null || string.IsNullOrWhiteSpace(value.ExpiryDate.ToString().Trim()))
        //            {
        //                Errors.Add("Expiry Date is required");
        //            }
        //            else
        //            {
        //                string justStrings = value.ExpiryDate.ToString().Trim();
        //                if (justStrings == default(DateTime).ToString())
        //                {
        //                    Errors.Add("Expiry Date is not valid. Expiry Date format is MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    string inputString = justStrings; //value.DOB.ToString("MM/dd/yyyy");
        //                    DateTime dDate;
        //                    if (DateTime.TryParse(inputString, out dDate))
        //                    {
        //                        //valid
        //                        DateTime dateIncep = new DateTime(value.InceptionDate.Year, value.InceptionDate.Month, value.InceptionDate.Day, value.InceptionDate.Hour, value.InceptionDate.Minute, value.InceptionDate.Millisecond);
        //                        DateTime dateExp = new DateTime(value.ExpiryDate.Year, value.ExpiryDate.Month, value.ExpiryDate.Day, value.ExpiryDate.Hour, value.ExpiryDate.Minute, value.ExpiryDate.Millisecond);
        //                        DateTime dateEff = new DateTime(value.EffectiveDate.Year, value.EffectiveDate.Month, value.EffectiveDate.Day, value.EffectiveDate.Hour, value.EffectiveDate.Minute, value.EffectiveDate.Millisecond);
        //                        int resultd = DateTime.Compare(dateIncep, dateExp);
        //                        if (resultd < 0)
        //                        {
        //                          //error
        //                        }
        //                        else if (resultd == 0)
        //                        {
        //                            Errors.Add("Inception date and Expiry date should not be equal");
        //                        }
        //                        else
        //                        {
        //                            Errors.Add("Expiry date should be greater than Inception date");
        //                        }

        //                        int resultd2 = DateTime.Compare(dateEff, dateExp);
        //                        if (resultd2 < 0)
        //                        {
        //                            // Errors.Add("");
        //                        }
        //                        else if (resultd2 == 0)
        //                        {
        //                            Errors.Add("Effective date and Expiry date should not be equal");
        //                        }
        //                        else
        //                        {
        //                            Errors.Add("Effective date should be greater than Expiry date");
        //                        }

        //                        int resultd3 = DateTime.Compare(dateIncep, dateEff);
        //                        if (resultd3 < 0)
        //                        {
        //                            // Errors.Add("");
        //                        }
        //                        else if (resultd3 == 0)
        //                        {
        //                           // Errors.Add("Effective date and Inception date should not be equal");
        //                        }
        //                        else
        //                        {
        //                            Errors.Add("Effective date should be greater than Inception date");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Errors.Add("Expiry Date is not valid. Expiry Date format is MM/dd/yyyy");
        //                    }
        //                }
        //            }
        //            if (value.EffectiveDate == null || string.IsNullOrWhiteSpace(value.EffectiveDate.ToString().Trim()))
        //            {
        //                Errors.Add("Effective Date is required");
        //            }
        //            else
        //            {
        //                string justStrings = value.EffectiveDate.ToString().Trim();
        //                if (justStrings == default(DateTime).ToString())
        //                {
        //                    Errors.Add("Effective Date is not valid. Effective Date format is MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    string inputString = justStrings; //value.DOB.ToString("MM/dd/yyyy");
        //                    DateTime dDate;
        //                    if (DateTime.TryParse(inputString, out dDate))
        //                    {
        //                        //valid
        //                    }
        //                    else
        //                    {
        //                        Errors.Add("Effective Date is not valid. Effective Date format is MM/dd/yyyy");
        //                    }
        //                }
        //            }
        //            if (value.ProductID.ToString().Trim() == null || value.ProductID =="" || string.IsNullOrWhiteSpace(value.ProductID.ToString().Trim()))
        //            {
        //                Errors.Add("Product ID is required");
        //            }
        //            else
        //            {
        //                string justStrings = new String(value.ProductID.ToString().Trim().Where(Char.IsLetter).ToArray());
        //                if (!regexItem.IsMatch(value.ProductID.ToString().Trim()))
        //                {
        //                    Errors.Add("Special characters are not allowed in ProductID ");
        //                }
        //                if (justStrings != "")
        //                {
        //                    Errors.Add("Product ID allows only numeric values.");
        //                }
        //            }

        //            if (value.FloodCover.ToString().Trim() == null || value.FloodCover =="" || string.IsNullOrWhiteSpace(value.FloodCover.ToString().Trim()))
        //            {
        //                Errors.Add("Flood Cover is required");
        //            }
        //            else
        //            {
        //                string justStrings = new String(value.FloodCover.ToString().Trim().Where(Char.IsLetter).ToArray());
        //                if (!regexItem.IsMatch(value.FloodCover.ToString().Trim()))
        //                {
        //                    Errors.Add("Special characters are not allowed in FloodCover ");
        //                }
        //                if (justStrings != "")
        //                {
        //                    Errors.Add("Flood Cover allows only numeric values.");
        //                }
        //            }

        //            if (value.IsClaimed == null || value.IsClaimed == string.Empty || string.IsNullOrWhiteSpace(value.IsClaimed.Trim()))
        //            {
        //                Errors.Add("IsClaimed is required");
        //            }
        //            else
        //            {
        //                string justNumber = new String(value.IsClaimed.Trim().Where(Char.IsDigit).ToArray());
        //                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
        //                if (value.IsClaimed.Length > 1)
        //                {
        //                    Errors.Add("IsClaimed should not be greater than 1 characters.");
        //                }
        //                if (!regexItem.IsMatch(value.IsClaimed.Trim()))
        //                {
        //                    Errors.Add("Special characters are not allowed in IsClaimed");

        //                }
        //                if (justNumber != null && justNumber != string.Empty)
        //                {
        //                    Errors.Add("IsClaimed does not allow numeric values.");
        //                }
        //            }

        //            if (value.RemoveStampDuty == null || value.RemoveStampDuty == string.Empty || string.IsNullOrWhiteSpace(value.RemoveStampDuty.Trim()))
        //            {
        //                Errors.Add("Remove Stamp Duty is required");
        //            }
        //            else
        //            {
        //                string justNumber = new String(value.RemoveStampDuty.Trim().Where(Char.IsDigit).ToArray());
        //                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
        //                if (value.RemoveStampDuty.Length > 10)
        //                {
        //                    Errors.Add("IsClaimed should not be greater than 10 characters.");
        //                }
        //                if (!regexItem.IsMatch(value.RemoveStampDuty.Trim()))
        //                {
        //                    Errors.Add("Special characters are not allowed in RemoveStampDuty");

        //                }
        //                if (justNumber != null && justNumber != string.Empty)
        //                {
        //                    Errors.Add("Remove Stamp Duty does not allow numeric values.");
        //                }
        //            }

        //            if (value.Reason == null || value.Reason == string.Empty || string.IsNullOrWhiteSpace(value.Reason.Trim()))
        //            {
        //                Errors.Add("Reason is required");
        //            }
        //            else
        //            {
        //                string justNumber = new String(value.Reason.Trim().Where(Char.IsDigit).ToArray());
        //                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
        //                if (value.Reason.Length > 20)
        //                {
        //                    Errors.Add("Reason should not be greater than 20 characters.");
        //                }
        //                if (!regexItem.IsMatch(value.Reason.Trim()))
        //                {
        //                    Errors.Add("Special characters are not allowed in Reason");

        //                }
        //                if (justNumber != null && justNumber != string.Empty)
        //                {
        //                    Errors.Add("Reason does not allow numeric values.");
        //                }
        //            }

        //            if (value.CreatedByUserID == null || value.CreatedByUserID == string.Empty || string.IsNullOrWhiteSpace(value.CreatedByUserID.Trim()))
        //            {
        //                Errors.Add("Created By UserID is required");
        //            }
        //            else
        //            {
        //                string justNumber = new String(value.CreatedByUserID.Trim().Where(Char.IsDigit).ToArray());
        //                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
        //                if (value.CreatedByUserID.Length > 20)
        //                {
        //                    Errors.Add("Created By UserID should not be greater than 20 characters.");
        //                }
        //                if (!regexItem.IsMatch(value.CreatedByUserID.Trim()))
        //                {
        //                    Errors.Add("Special characters are not allowed in CreatedByUserID");

        //                }
        //                if (justNumber != null && justNumber != string.Empty)
        //                {
        //                    Errors.Add("Created By UserID does not allow numeric values.");
        //                }
        //            }

        //            if (value.Timestamp == null || string.IsNullOrWhiteSpace(value.Timestamp.ToString().Trim()))
        //            {
        //                Errors.Add("Time stamp is required");
        //            }
        //            else
        //            {
        //                string justStrings = value.Timestamp.ToString().Trim();
        //                if (justStrings == default(DateTime).ToString())
        //                {
        //                    Errors.Add("Time stamp is not valid. Timestamp format is MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    string inputString = justStrings; //value.DOB.ToString("MM/dd/yyyy");
        //                    DateTime dDate;
        //                    if (DateTime.TryParse(inputString, out dDate))
        //                    {
        //                        //valid
        //                    }
        //                    else
        //                    {
        //                        Errors.Add("Time stamp is not valid. Timestamp format is MM/dd/yyyy");
        //                    }
        //                }
        //            }
        //            if (value.PolicyDetailsID == null || value.PolicyDetailsID =="" || string.IsNullOrWhiteSpace(value.PolicyDetailsID.ToString().Trim()))
        //            {
        //                Errors.Add("Policy Details ID is required");
        //            }
        //            else
        //            {
        //                string justStrings = new String(value.PolicyDetailsID.ToString().Trim().Where(Char.IsLetter).ToArray());
        //                if (!regexItem.IsMatch(value.PolicyDetailsID.ToString().Trim()))
        //                {
        //                    Errors.Add("Special characters are not allowed in Policy Details ID ");
        //                }
        //                if (justStrings != "")
        //                {
        //                    Errors.Add("Policy DetailsID allows only numeric values.");
        //                }
        //            }
        //            if (Errors != null && Errors.Count() > 0)
        //            {
        //                policyDetailsRef.Status = "Failure";
        //                policyDetailsRef.ErrorMessage = Errors;
        //                return policyDetailsRef;
        //            }
        //            else
        //            {
        //                int? result = policyDetailsClass.InsertPolicyDetails(value);
        //                if (result.HasValue && result > 0)
        //                {

        //                    policyDetailsRef.Status = "Success";
        //                    policyDetailsRef.PolicyDetailsID = result.Value;

        //                }
        //                if (result.HasValue && result == 2)
        //                {

        //                    policyDetailsRef.Status = "Success";
        //                    policyDetailsRef.PolicyDetailsID = result.Value;
                          
        //                }
        //                if(result.HasValue && result==-3)
        //                {
        //                    policyDetailsRef.Status = "Failure";
        //                    policyDetailsRef.ErrorMessage.Add("Pc ID is already exists.");
        //                }
        //                if (result.HasValue && result == -34)
        //                {
        //                    policyDetailsRef.Status = "Failure";
        //                    policyDetailsRef.ErrorMessage.Add("Pc ID and Policy Details ID are already exists.");
        //                }
        //                if (result.HasValue && result == -4)
        //                {
        //                    policyDetailsRef.Status = "Failure";
        //                    policyDetailsRef.ErrorMessage.Add("Policy Details ID is already exists.");
        //                }
        //                else if (result.HasValue && result == (int)InsuredResult.Exception)
        //                {

        //                    policyDetailsRef.Status = "Failure";
        //                    policyDetailsRef.ErrorMessage.Add("Failed to insert.");

        //                }
        //            }
        //        }
        //        else
        //        {
        //            policyDetailsRef.Status = "Failure";
        //        }


        //    }
        //    catch (Exception xp)
        //    {
        //        policyDetailsRef.Status = "Failure";
        //        policyDetailsRef.ErrorMessage.Add(xp.Message);
        //    }
        //    finally
        //    {

        //    }
        //    return policyDetailsRef;
        //}

        // PUT: api/NewPolicyDetails/5
        //public NewPolicyDetailsRef Put(int id, [FromBody]PolicyDetails value)
        //{
        //    NewPolicyDetailsClass policyDetailsClass = new NewPolicyDetailsClass();
        //    var regexItem = new Regex("^[a-zA-Z0-9 ]*$"); // for confirm special charectors or not
        //    NewPolicyDetailsRef policyDetailsRef = new NewPolicyDetailsRef();
         
        //    List<string> Errors = new List<string>();
        //    policyDetailsRef.ErrorMessage = new List<string>();
        //    try
        //    {
        //        if (value != null && id > 0)
        //        {
        //            if (value.Reason == null || value.Reason == string.Empty || string.IsNullOrWhiteSpace(value.Reason.Trim()))
        //            {
        //                Errors.Add("Reason is required");
        //            }
        //            else
        //            {
        //                string justNumber = new String(value.Reason.Trim().Where(Char.IsDigit).ToArray());
        //                // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
        //                if (value.Reason.Length > 20)
        //                {
        //                    Errors.Add("Reason should not be greater than 20 characters.");
        //                }
        //                if (!regexItem.IsMatch(value.Reason.Trim()))
        //                {
        //                    Errors.Add("Special characters are not allowed in Reason");

        //                }
        //                if (justNumber != null && justNumber != string.Empty)
        //                {
        //                    Errors.Add("Reason does not allow numeric values.");
        //                }
        //            }

        //            if (value.EffectiveDate == null || string.IsNullOrWhiteSpace(value.EffectiveDate.ToString().Trim()))
        //            {
        //                Errors.Add("Effective  Date is required");
        //            }
        //            else
        //            {
        //                string justStrings = value.EffectiveDate.ToString().Trim();
        //                if (justStrings == default(DateTime).ToString())
        //                {
        //                    Errors.Add("Effective Date is not valid. Effective Date format is MM/dd/yyyy");
        //                }
        //                else
        //                {
        //                    string inputString = justStrings; //value.DOB.ToString("MM/dd/yyyy");
        //                    DateTime dDate;
        //                    if (DateTime.TryParse(inputString, out dDate))
        //                    {
        //                        //valid
        //                    }
        //                    else
        //                    {
        //                        Errors.Add("Effective Date is not valid. Effective Date format is MM/dd/yyyy");
        //                    }
        //                }
        //            }

        //            if (value.FloodCover.ToString().Trim() == null || value.FloodCover =="" || string.IsNullOrWhiteSpace(value.FloodCover.ToString().Trim()))
        //            {
        //                Errors.Add("FloodCover is required");
        //            }
        //            else
        //            {
        //                string justStrings = new String(value.FloodCover.ToString().Trim().Where(Char.IsLetter).ToArray());
        //                if (!regexItem.IsMatch(value.FloodCover.ToString().Trim()))
        //                {
        //                    Errors.Add("Special characters are not allowed in FloodCover ");
        //                }
        //                if (justStrings != "")
        //                {
        //                    Errors.Add("FloodCover allows only numeric values.");
        //                }
        //            }

        //            if (Errors != null && Errors.Count() > 0)
        //            {
        //                policyDetailsRef.Status = "Failure";
        //                policyDetailsRef.ErrorMessage = Errors;
        //                return policyDetailsRef;
        //            }
        //            else
        //            {
        //                int? result = policyDetailsClass.UpdatePolicyDetails(id, value); ;
        //                if (result.HasValue && result > 0)
        //                {

        //                    policyDetailsRef.Status = "Success";
        //                    policyDetailsRef.PolicyDetailsID = id;
                          
        //                }
        //                if (result.HasValue && result == 2)
        //                {

        //                    policyDetailsRef.Status = "Success";
        //                    policyDetailsRef.PolicyDetailsID = id;

        //                }
        //                else if (result.HasValue && result == (int)InsuredResult.Exception)
        //                {

        //                    policyDetailsRef.Status = "Failure";
        //                    policyDetailsRef.ErrorMessage.Add("Failed to insert.");

        //                }
        //            }
        //        }
        //        else
        //        {
        //            policyDetailsRef.Status = "Failure";
        //        }


        //    }
        //    catch (Exception xp)
        //    {
        //        policyDetailsRef.Status = "Failure";
        //        policyDetailsRef.ErrorMessage.Add(xp.Message);
        //    }
        //    finally
        //    {

        //    }
        //    return policyDetailsRef;
        //}

        // DELETE: api/NewPolicyDetails/5
        public void Delete(int id)
        {
        }
    }
}
