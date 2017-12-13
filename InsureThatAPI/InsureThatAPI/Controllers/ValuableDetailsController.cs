using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InsureThatAPI.Models;
using InsureThatAPI.CommonMethods;
using static InsureThatAPI.CommonMethods.EnumValuablesDetails;

namespace InsureThatAPI.Controllers
{
    public class ValuableDetailsController : ApiController
    {
        // GET: api/ValuableDetails
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// GET METHOD FOR VALUABLE DETAILS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        #region GET METHOD FOR VALUABLE DETAILS

        // GET: api/ValuableDetails/5
        public ValuableDetailsRef Get(int id)
        {
            ValuableDetailsRef valuableDetailsRef = new ValuableDetailsRef();
            try
            {
                ValuableDetailsClass valuableClass = new ValuableDetailsClass();
                if (id > 0)
                {
                    valuableDetailsRef = valuableClass.GetValuablesDetails(id);
                }
                else
                {
                    valuableDetailsRef.Status = "Failure";
                    valuableDetailsRef.ErrorMessage.Add("No Data available..");
                }
            }
            catch (Exception xp)
            {
                valuableDetailsRef.Status = "Failure";
                valuableDetailsRef.ErrorMessage.Add(xp.Message);
            }
            finally
            {

            }
            return valuableDetailsRef;
        }

        #endregion

        #region  INSERT METHOD FOR VALUABLE DETAILS

        // POST: api/ValuableDetails
        public ValuableDetailsRef Post([FromBody]Valuables value)
        {
            ValuableDetailsRef valuableDetailsRef = new ValuableDetailsRef();
            ValuableDetailsClass valuableClass = new ValuableDetailsClass();
            try
            {
                List<string> Errors = new List<string>();
                //   valuableDetailsRef.ErrorMessage = new List<string>();
              if(value.Coverforunspecifiedvaluables==null)
             
                {
                    Errors.Add("Cover for un specified valuables is required");
                }
                //if (value.TrId == null || value.TrId <= 0)
                //{
                //    Errors.Add("TransactionID is required");
                //}
                //if (value.HomeID == null || value.HomeID <= 0)
                //{
                //    Errors.Add("HomeID is required");
                //}
                //if (value.PremiumId == null || value.PremiumId <= 0)
                //{
                //    Errors.Add("PremiumID is required");
                //}
                //if (value.ValuablesDetailID == null || value.ValuablesDetailID <= 0)
                //{
                //    Errors.Add("ValuableID is required");
                //}
                //if (value.ValuablesUnspecifiedSumInsured == null || value.ValuablesUnspecifiedSumInsured <= 0)
                //{
                //    Errors.Add("UnspecifiedsumInsured is required");
                //}
                //if (value.ValuablesExcess == null || value.ValuablesExcess <= 0)
                //{
                //    Errors.Add("Excess is required");
                //}
                //if (Errors != null && Errors.Count() > 0)
                //{
                //    valuableDetailsRef.Status = "Failure";
                //    valuableDetailsRef.ErrorMessage = Errors;
                //    return valuableDetailsRef;
                //}
                //else
                //{
                //    int? result = valuableClass.InsertValuablesDetails(value);
                //    if (result.HasValue && result > 0)
                //    {
                //        valuableDetailsRef.Status = "Success";
                //        valuableDetailsRef.ValuableDetailsData.ValuablesDetailID = result.Value;
                //    }
                //    else if (result.HasValue && result == (int)ValuableResult.Exception)
                //    {
                //        valuableDetailsRef.Status = "Failure";
                //        valuableDetailsRef.ErrorMessage.Add("Failed to insert.");

                //    }
                //}
            }
            catch (Exception xp)
            {
                valuableDetailsRef.Status = "Failure";
                valuableDetailsRef.ErrorMessage.Add(xp.Message);
            }
            finally
            {

            }
            return valuableDetailsRef;
        }

        #endregion

        #region UPDATE METHOD FOR VALUABLE DETAILS

        // PUT: api/ValuableDetails/5
        public ValuableDetailsRef Put(int id, [FromBody]ValuableDetails value)
        {
            ValuableDetailsRef valuableDetailsRef = new ValuableDetailsRef();
            ValuableDetailsClass valuableClass = new ValuableDetailsClass();
            try
            {
                List<string> Errors = new List<string>();
                valuableDetailsRef.ErrorMessage = new List<string>();
                if (value.PcId == null || value.PcId <= 0)
                {
                    Errors.Add("PolicyID is required");
                }
                if (value.TrId == null || value.TrId <= 0)
                {
                    Errors.Add("TransactionID is required");
                }
                if (value.HomeID == null || value.HomeID <= 0)
                {
                    Errors.Add("HomeID is required");
                }
                if (value.PremiumId == null || value.PremiumId <= 0)
                {
                    Errors.Add("PremiumID is required");
                }
                if (value.ValuablesDetailID == null || value.ValuablesDetailID <= 0)
                {
                    Errors.Add("ValuableID is required");
                }
                if (value.ValuablesUnspecifiedSumInsured == null || value.ValuablesUnspecifiedSumInsured <= 0)
                {
                    Errors.Add("UnspecifiedsumInsured is required");
                }
                if (value.ValuablesExcess == null || value.ValuablesExcess <= 0)
                {
                    Errors.Add("Excess is required");
                }
                if (Errors != null && Errors.Count() > 0)
                {
                    valuableDetailsRef.Status = "Failure";
                    valuableDetailsRef.ErrorMessage = Errors;
                    return valuableDetailsRef;
                }
                else
                {
                    int? result = valuableClass.UpdateValuablesDetails(id, value);
                    if (result.HasValue && result > 0)
                    {
                        valuableDetailsRef.Status = "Success";
                        valuableDetailsRef.ValuableDetailsData.ValuablesDetailID = result.Value;
                    }
                    else if (result.HasValue && result == (int)ValuableResult.Exception)
                    {
                        valuableDetailsRef.Status = "Failure";
                        valuableDetailsRef.ErrorMessage.Add("Failed to insert.");

                    }
                }
            }
            catch (Exception xp)
            {
                valuableDetailsRef.Status = "Failure";
                valuableDetailsRef.ErrorMessage.Add(xp.Message);
            }
            finally
            {

            }
            return valuableDetailsRef;
        }

        #endregion

        // DELETE: api/ValuableDetails/5
        public void Delete(int id)
        {
        }
    }
}
