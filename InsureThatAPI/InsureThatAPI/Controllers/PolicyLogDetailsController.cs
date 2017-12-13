using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InsureThatAPI.Models;
using InsureThatAPI.CommonMethods;
using static InsureThatAPI.CommonMethods.EnumPolicyDetails;

namespace InsureThatAPI.Controllers
{
    public class PolicyLogDetailsController : ApiController
    {
        // GET: api/PolicyLogDetails
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// GET METHOD FOR POLICYLOGDETAILS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        #region GET METHOD FOR POLICYLOG DETAILS
        // GET: api/PolicyLogDetails/5
        public PolicyLogRef Get(int id)
        {
            PolicyLogRef policylogRef = new PolicyLogRef();
            try
            {
                PolicyLogDetailsClass policylogclass = new PolicyLogDetailsClass();
                if (id > 0)
                {
                    policylogRef = policylogclass.GetPolicyLogDetails(id);
                }
                else
                {
                    policylogRef.Status = "Failure";
                    policylogRef.ErrorMessage.Add("Failed to getting policyInsuredID details.");
                }
            }
            catch (Exception xp)
            {
                policylogRef.Status = "Failure";
                policylogRef.ErrorMessage.Add(xp.Message);
            }
            finally
            {

            }
            return policylogRef;
        }

        #endregion

        #region INSERT METHOD FOR POLICYLOG DETAILS

        // POST: api/PolicyLogDetails
        public PolicyLogRef Post([FromBody]PolicyLogDetails value)
        {
            PolicyLogRef policylogRef = new PolicyLogRef();
            PolicyLogDetailsClass policyInsuredClass = new PolicyLogDetailsClass();
            List<string> Errors = new List<string>();
            policylogRef.ErrorMessage = new List<string>();
            try
            {
                if (value.UserID == null || value.UserID <= 0)
                {
                    Errors.Add("User is Required.");
                }
                if (value.PolicyNumber == null || value.PolicyNumber <= 0)
                {
                    Errors.Add("PolicyId is Required.");
                }
                if (string.IsNullOrWhiteSpace(value.Details.Trim()))
                {
                    Errors.Add("PolicyLog Details is Required.");
                }
                if (value.Timestamp == null)
                {
                    Errors.Add("TimeStamp is required");
                }
                if (Errors != null && Errors.Count() > 0)
                {
                    policylogRef.Status = "Failure";
                    policylogRef.ErrorMessage = Errors;
                    return policylogRef;
                }
                else
                {
                    int? result = policyInsuredClass.InsertPolicyLogDetails(value);
                    if (result.HasValue && result > 0)
                    {
                        policylogRef.Status = "Success";
                        policylogRef.PolicyLogData.PolicyLogID = result.Value;
                    }
                    else if (result.HasValue && result == (int)PolicyResult.Exception)
                    {
                        policylogRef.Status = "Failure";
                        policylogRef.ErrorMessage.Add("Failed to insert.");

                    }
                }
            }
            catch (Exception xp)
            {
                policylogRef.Status = "Failure";
                policylogRef.ErrorMessage.Add(xp.Message);
            }
            finally
            {

            }
            return policylogRef;
        }

        #endregion

        #region UPDATE METHOD FOR POLICYLOG DETAILS
        // PUT: api/PolicyLogDetails/5
        public PolicyLogRef Put(int id, [FromBody]PolicyLogDetails value)
        {
            PolicyLogRef policylogRef = new PolicyLogRef();
            PolicyLogDetailsClass policyInsuredClass = new PolicyLogDetailsClass();
            List<string> Errors = new List<string>();
            policylogRef.ErrorMessage = new List<string>();
            try
            {
                if (value.UserID == null || value.UserID <= 0)
                {
                    Errors.Add("User is Required.");
                }
                if (value.PolicyNumber == null || value.PolicyNumber <= 0)
                {
                    Errors.Add("PolicyId is Required.");
                }
                if (string.IsNullOrWhiteSpace(value.Details.Trim()))
                {
                    Errors.Add("PolicyLog Details is Required.");
                }
                if (value.Timestamp == null)
                {
                    Errors.Add("TimeStamp is required");
                }
                if (Errors != null && Errors.Count() > 0)
                {
                    policylogRef.Status = "Failure";
                    policylogRef.ErrorMessage = Errors;
                    return policylogRef;
                }
                else
                {
                    int? result = policyInsuredClass.UpdatePolicyLogDetails(id, value);
                    if (result.HasValue && result > 0)
                    {
                        policylogRef.Status = "Success";
                        policylogRef.PolicyLogData.PolicyLogID = result.Value;
                    }
                    else if (result.HasValue && result == (int)PolicyResult.Exception)
                    {
                        policylogRef.Status = "Failure";
                        policylogRef.ErrorMessage.Add("Failed to update.");
                    }
                }
            }
            catch (Exception xp)
            {
                policylogRef.Status = "Failure";
                policylogRef.ErrorMessage.Add(xp.Message);
            }
            finally
            {

            }
            return policylogRef;
        }
        #endregion
        // DELETE: api/PolicyLogDetails/5
        public void Delete(int id)
        {
        }
    }
}
