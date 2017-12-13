using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InsureThatAPI.Models;
using InsureThatAPI.CommonMethods;
using static InsureThatAPI.CommonMethods.EnumInsuredDisclosureDetails;

namespace InsureThatAPI.Controllers
{
    public class InsuredDisclosureController : ApiController
    {
        // GET: api/InsuredDisclosure
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /// <summary>
        /// GET METHOD FOR INSUREDDISCLOSURE DETAILS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region GET METHOD FOR INSUREDDISCLOSURE DETAILS
        // GET: api/InsuredDisclosure/5
        public InsureDisclosureRef Get(int id)
        {
            InsureDisclosureRef insuredDisclosureref = new InsureDisclosureRef();
            InsuredDisclosureClass insuredDisclosureClass = new InsuredDisclosureClass();
            try
            {
                if (id > 0)
                {
                    insuredDisclosureref = insuredDisclosureClass.GetInsuredDisclosureDetails(id);
                }
                else
                {
                    insuredDisclosureref.Status = "Failure";
                    insuredDisclosureref.ErrorMessage.Add("Failed to getting InsuredDisclosure data");
                }
            }
            catch (Exception xp)
            {
                insuredDisclosureref.Status = "Failure";
                insuredDisclosureref.ErrorMessage.Add(xp.Message);
            }
            finally
            {

            }
            return insuredDisclosureref;
        }

        #endregion

        #region INSERT METHOD FOR INSUREDDISCLOSURE DETAILS
        // POST: api/InsuredDisclosure
        public InsureDisclosureRef Post([FromBody]InsuredDisclosure value)
        {
            InsuredDisclosureClass insuredDisclosuredetails = new InsuredDisclosureClass();
            InsureDisclosureRef insuredDisclosureref = new InsureDisclosureRef();
            List<string> Errors = new List<string>();
            insuredDisclosureref.ErrorMessage = new List<string>();
            try
            {
                if (value.PcId == null || value.PcId <= 0)
                {
                    Errors.Add("PolicyId is required");
                }
                if (value.TrId == null || value.TrId <= 0)
                {
                    Errors.Add("Transaction Id is required");
                }
                if (value.PreviousInsurer == null || value.PreviousInsurer <= 0)
                {
                    Errors.Add("PreviousInsurer is required");
                }
                if (string.IsNullOrWhiteSpace(value.Description.Trim()))
                {
                    Errors.Add("Description is required");
                }
                if (value.DischargeDate == null)
                {
                    Errors.Add("DischargeDate is required");
                }
                if (Errors != null && Errors.Count() > 0)
                {
                    insuredDisclosureref.Status = "Failure";
                    insuredDisclosureref.ErrorMessage = Errors;
                    return insuredDisclosureref;
                }
                else
                {
                    int? result = insuredDisclosuredetails.InsertInsuredDisclosureDetails(value);
                    if (result.HasValue && result > 0)
                    {
                        insuredDisclosureref.Status = "Success";
                        insuredDisclosureref.InsuredDisclosureData.InsurerDisclosureID = result.Value;
                    }
                    else if (result.HasValue && result == (int)InsuredDisclosureResult.Exception)
                    {
                        insuredDisclosureref.Status = "Failure";
                        insuredDisclosureref.ErrorMessage.Add("Failed to insert.");
                    }
                }
            }
            catch (Exception xp)
            {
                insuredDisclosureref.Status = "Failure";
                insuredDisclosureref.ErrorMessage.Add(xp.Message);
            }
            finally
            {

            }
            return insuredDisclosureref;
        }
        #endregion

        #region UPDATE METHOD FOR INSUREDDISCLOSURE DETAILS

        // PUT: api/InsuredDisclosure/5
        public InsureDisclosureRef Put(int id, [FromBody]InsuredDisclosure value)
        {
            InsuredDisclosureClass insuredDisclosuredetails = new InsuredDisclosureClass();
            InsureDisclosureRef insuredDisclosureref = new InsureDisclosureRef();
            List<string> Errors = new List<string>();
            insuredDisclosureref.ErrorMessage = new List<string>();
            try
            {
                if (value.PcId == null || value.PcId <= 0)
                {
                    Errors.Add("PolicyId is required");
                }
                if (value.TrId == null || value.TrId <= 0)
                {
                    Errors.Add("Transaction Id is required");
                }
                if (value.PreviousInsurer == null || value.PreviousInsurer <= 0)
                {
                    Errors.Add("PreviousInsurer is required");
                }
                if (string.IsNullOrWhiteSpace(value.Description.Trim()))
                {
                    Errors.Add("Description is required");
                }
                if (value.DischargeDate == null)
                {
                    Errors.Add("DischargeDate is required");
                }
                if (Errors != null && Errors.Count() > 0)
                {
                    insuredDisclosureref.Status = "Failure";
                    insuredDisclosureref.ErrorMessage = Errors;
                    return insuredDisclosureref;
                }
                else
                {
                    int? result = insuredDisclosuredetails.UpdateInsuredDisclosureDetails(id, value);
                    if (result.HasValue && result > 0)
                    {
                        insuredDisclosureref.Status = "Success";
                        insuredDisclosureref.InsuredDisclosureData.InsurerDisclosureID = result.Value;
                    }
                    else if (result.HasValue && result == (int)InsuredDisclosureResult.Exception)
                    {
                        insuredDisclosureref.Status = "Failure";
                        insuredDisclosureref.ErrorMessage.Add("Failed to update.");
                    }
                }
            }
            catch (Exception xp)
            {
                insuredDisclosureref.Status = "Failure";
                insuredDisclosureref.ErrorMessage.Add(xp.Message);
            }
            finally
            {

            }
            return insuredDisclosureref;
        }

        #endregion
        // DELETE: api/InsuredDisclosure/5
        public void Delete(int id)
        {
        }
    }
}
