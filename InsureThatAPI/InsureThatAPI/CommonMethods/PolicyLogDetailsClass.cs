using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsureThatAPI.Models;

namespace InsureThatAPI.CommonMethods
{
   
    public class PolicyLogDetailsClass
    {
        #region GET METHOD FOR POLICYLOG DETAILS
        public PolicyLogRef GetPolicyLogDetails(int ID)
        {
            PolicyLogRef policylogref = new PolicyLogRef();
            try
            {
                PolicyLogDetails policylogmodel = new PolicyLogDetails();
                MasterDataEntities db = new MasterDataEntities();
                var str = db.IT_CC_GET_PolicyLogDetails(ID).ToList();
                if (str.Count > 0)
                {
                    foreach (var item in str)
                    {
                        policylogmodel = new PolicyLogDetails();
                        policylogmodel.Details = item.Details;
                        policylogmodel.PolicyLogID = item.PolicyLogID;
                        policylogmodel.UserID = item.UserID;
                        policylogmodel.Timestamp = item.Timestamp;
                        policylogref.PolicyLogData = policylogmodel;
                        policylogref.Status = "Success";
                    }
                }
                else
                {
                    policylogref.ErrorMessage.Add("No Data Available");
                    policylogref.Status = "Failure";
                }
            }
            catch (Exception xp)
            {
                policylogref.ErrorMessage.Add(xp.Message);
                policylogref.Status = "Failure";
            }
            finally
            {

            }
            return policylogref;
        }
        #endregion

        #region UPDATE METHOD FOR POLICYLOG DETAILS

        public int UpdatePolicyLogDetails(int ID, PolicyLogDetails policylogmodel)
        {
            int icount = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                icount = db.IT_CC_UPDATE_PolicyLogDetails(ID, policylogmodel.PolicyLogID, policylogmodel.Details, policylogmodel.Timestamp);
            }
            catch (Exception xp)
            {

            }
            finally
            {

            }
            return icount;
        }
        #endregion

        #region INSERT METHOD FOR POLICYLOG DETAILS

        public int InsertPolicyLogDetails(PolicyLogDetails policylogmodel)
        {
            int icount = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                icount = db.IT_CC_Insert_PolicyLogDetails(policylogmodel.PolicyNumber, policylogmodel.PolicyLogID, policylogmodel.Details, policylogmodel.UserID, policylogmodel.Timestamp);
            }
            catch (Exception xp)
            {

            }
            finally
            {

            }
            return icount;
        }

        #endregion
    }
}