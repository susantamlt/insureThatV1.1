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
    public class ValuableItemDetailsController : ApiController
    {
        // GET: api/ValuableItemDetails
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /// <summary>
        /// GET METHOD FOR VALUABLE ITEM DETAILS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        #region GET METHOD FOR VALUABLE ITEM DETAILS

        // GET: api/ValuableItemDetails/5
        public ValuableItemDetailsRef Get(int id)
        {
            ValuableItemDetailsRef valuableItemDetailsRef = new ValuableItemDetailsRef();
            try
            {
                ValuableItemsDetailsClass valuableItemClass = new ValuableItemsDetailsClass();
                if (id > 0)
                {
                    valuableItemDetailsRef = valuableItemClass.GetValuablesItemDetails(id);
                }
                else
                {
                    valuableItemDetailsRef.Status = "Failure";
                    valuableItemDetailsRef.ErrorMessage.Add("No Data available..");
                }
            }
            catch (Exception xp)
            {
                valuableItemDetailsRef.Status = "Failure";
                valuableItemDetailsRef.ErrorMessage.Add(xp.Message);
            }
            finally
            {

            }
            return valuableItemDetailsRef;
        }
        #endregion

        #region INSERT METHOD FOR VALUABLE ITEM DETAILS

        // POST: api/ValuableItemDetails
        public ValuableItemDetailsRef Post([FromBody]ValuableItemDetails value)
        {
            ValuableItemDetailsRef valuableItemDetailsRef = new ValuableItemDetailsRef();
            ValuableItemsDetailsClass valuableItemClass = new ValuableItemsDetailsClass();
            try
            {
                List<string> Errors = new List<string>();
                valuableItemDetailsRef.ErrorMessage = new List<string>();
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
                if (string.IsNullOrWhiteSpace(value.ValuablesDescription))
                {
                    Errors.Add("Description is required");
                }
                if (value.ValuablesSumInsured == null || value.ValuablesSumInsured <= 0)
                {
                    Errors.Add("SumInsured is required");
                }
                if (value.ValuablesItemID == null || value.ValuablesItemID <= 0)
                {
                    Errors.Add("ValuableItemID is required");
                }
                if (Errors != null && Errors.Count() > 0)
                {
                    valuableItemDetailsRef.Status = "Failure";
                    valuableItemDetailsRef.ErrorMessage = Errors;
                    return valuableItemDetailsRef;
                }
                else
                {
                    int? result = valuableItemClass.InsertValuablesItemDetails(value);
                    if (result.HasValue && result > 0)
                    {
                        valuableItemDetailsRef.Status = "Success";
                        valuableItemDetailsRef.ValuableItemDetailsData.ValuablesItemID = result.Value;
                    }
                    else if (result.HasValue && result == (int)ValuableResult.Exception)
                    {
                        valuableItemDetailsRef.Status = "Failure";
                        valuableItemDetailsRef.ErrorMessage.Add("Failed to insert.");
                    }
                }
            }
            catch (Exception xp)
            {
                valuableItemDetailsRef.Status = "Failure";
                valuableItemDetailsRef.ErrorMessage.Add(xp.Message);
            }
            finally
            {

            }
            return valuableItemDetailsRef;
        }

        #endregion

        #region UPDATE METHOD FOR VALUABLE ITEM DETAILS

        // PUT: api/ValuableItemDetails/5
        public ValuableItemDetailsRef Put(int id, [FromBody]ValuableItemDetails value)
        {
            ValuableItemDetailsRef valuableItemDetailsRef = new ValuableItemDetailsRef();
            ValuableItemsDetailsClass valuableItemClass = new ValuableItemsDetailsClass();
            try
            {
                List<string> Errors = new List<string>();
                valuableItemDetailsRef.ErrorMessage = new List<string>();
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
                if (string.IsNullOrWhiteSpace(value.ValuablesDescription))
                {
                    Errors.Add("Description is required");
                }
                if (value.ValuablesSumInsured == null || value.ValuablesSumInsured <= 0)
                {
                    Errors.Add("SumInsured is required");
                }
                if (value.ValuablesItemID == null || value.ValuablesItemID <= 0)
                {
                    Errors.Add("ValuableItemID is required");
                }
                if (Errors != null && Errors.Count() > 0)
                {
                    valuableItemDetailsRef.Status = "Failure";
                    valuableItemDetailsRef.ErrorMessage = Errors;
                    return valuableItemDetailsRef;
                }
                else
                {
                    int? result = valuableItemClass.UpdateValuablesItemDetails(id, value);
                    if (result.HasValue && result > 0)
                    {
                        valuableItemDetailsRef.Status = "Success";
                        valuableItemDetailsRef.ValuableItemDetailsData.ValuablesItemID = result.Value;
                    }
                    else if (result.HasValue && result == (int)ValuableResult.Exception)
                    {
                        valuableItemDetailsRef.Status = "Failure";
                        valuableItemDetailsRef.ErrorMessage.Add("Failed to update.");

                    }
                }
            }
            catch (Exception xp)
            {
                valuableItemDetailsRef.Status = "Failure";
                valuableItemDetailsRef.ErrorMessage.Add(xp.Message);
            }
            finally
            {

            }
            return valuableItemDetailsRef;
        }
        #endregion

        // DELETE: api/ValuableItemDetails/5
        public void Delete(int id)
        {
        }
    }
}
