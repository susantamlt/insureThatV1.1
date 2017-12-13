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
    public class PolicyInsuredIDController : ApiController
    {
        // GET: api/PolicyInsuredID
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /// <summary>
        /// GET POLICYINSUREDID DETAILS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region GET METHOD FOR POLICYINSUREDID DETAILS
        // GET: api/PolicyInsuredID/5
        public PolicyInsuredIDRef Get(int id)
        {
            PolicyInsuredIDRef policyInsuredIDRef = new PolicyInsuredIDRef();
            policyInsuredIDRef.ErrorMessage = new List<string>();
            policyInsuredIDRef.PolicyInsureIDData = new List<PolicyInsuredID>();
           PolicyInsuredIDClass policyInsuredClass = new PolicyInsuredIDClass();
            try
            {
                if (id > 0)
                {
                    policyInsuredIDRef = policyInsuredClass.GetPolicyInsuredDetails(id);
                }
                else
                {
                    policyInsuredIDRef.Status = "Failure";
                    policyInsuredIDRef.ErrorMessage.Add("Failed to getting policyInsuredID details.");
                }
            }
            catch (Exception xp)
            {

            }
            finally
            {

            }
            return policyInsuredIDRef;
        }
        #endregion


        #region INSERT METHOD FOR POLICYINSUREDID DETAILS
        // POST: api/PolicyInsuredID
        public PolicyInsuredIDRef Post([FromBody]PolicyInsuredID value)
        {
            PolicyInsuredIDRef policyInsuredIDRef = new PolicyInsuredIDRef();
            PolicyInsuredIDClass policyInsuredClass = new PolicyInsuredIDClass();
            List<string> Errors = new List<string>();
            policyInsuredIDRef.ErrorMessage = new List<string>();
            try
            {
                if (value.PcId == null || value.PcId <= 0)
                {
                    Errors.Add("PcId is Required.");
                }
                if (value.TrId == null || value.TrId <= 0)
                {
                    Errors.Add("TrId is Required.");
                }
                if (value.PolicyInsurID == null || value.PolicyInsurID <= 0)
                {
                    Errors.Add("TrId is Required.");
                }
                if (Errors != null && Errors.Count() > 0)
                {
                    policyInsuredIDRef.Status = "Failure";
                    policyInsuredIDRef.ErrorMessage = Errors;
                    return policyInsuredIDRef;
                }
                else
                {
                    int? result = policyInsuredClass.InsertPolicyInsuredDetails(value);
                    if (result.HasValue && result > 0)
                    {
                        policyInsuredIDRef.Status = "Success";
                       // policyInsuredIDRef.PolicyInsurIDData.PolicyInsurID = result.Value;
                    }
                    else if (result.HasValue && result == (int)PolicyResult.Exception)
                    {
                        policyInsuredIDRef.Status = "Failure";
                        policyInsuredIDRef.ErrorMessage.Add("Failed to insert.");
                    }
                }
            }
            catch (Exception xp)
            {
                policyInsuredIDRef.Status = "Failure";
                policyInsuredIDRef.ErrorMessage.Add("Failed to insert.");
            }
            finally
            {

            }
            return policyInsuredIDRef;
        }
        #endregion


        #region UPDATE METHOD FOR POLICYINSUREDID DETAILS
        // PUT: api/PolicyInsuredID/5
        public PolicyInsuredIDRef Put(int id, [FromBody]PolicyInsuredID value)
        {
            PolicyInsuredIDRef policyInsuredIDRef = new PolicyInsuredIDRef();
            PolicyInsuredIDClass policyInsuredClass = new PolicyInsuredIDClass();
            List<string> Errors = new List<string>();
            policyInsuredIDRef.ErrorMessage = new List<string>();
            try
            {
                if (value.PcId == null || value.PcId <= 0)
                {
                    Errors.Add("PcId is Required.");
                }
                if (value.TrId == null || value.TrId <= 0)
                {
                    Errors.Add("TrId is Required.");
                }
                if (value.PolicyInsurID == null || value.PolicyInsurID <= 0)
                {
                    Errors.Add("TrId is Required.");
                }
                if (Errors != null && Errors.Count() > 0)
                {
                    policyInsuredIDRef.Status = "Failure";
                    policyInsuredIDRef.ErrorMessage = Errors;
                    return policyInsuredIDRef;
                }
                else
                {
                    int? result = policyInsuredClass.InsertPolicyInsuredDetails(value);
                    if (result.HasValue && result > 0)
                    {
                        policyInsuredIDRef.Status = "Success";
                      //  policyInsuredIDRef.PolicyInsurIDData.PolicyInsurID = result.Value;
                    }
                    else if (result.HasValue && result == (int)PolicyResult.Exception)
                    {
                        policyInsuredIDRef.Status = "Failure";
                        policyInsuredIDRef.ErrorMessage.Add("Failed to update.");
                    }
                }
            }
            catch (Exception xp)
            {
                policyInsuredIDRef.Status = "Failure";
                policyInsuredIDRef.ErrorMessage.Add("Failed to update.");
            }
            finally
            {

            }
            return policyInsuredIDRef;
        }
        #endregion


        // DELETE: api/PolicyInsuredID/5
        public void Delete(int id)
        {
        }
    }
}
