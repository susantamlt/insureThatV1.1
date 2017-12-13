using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsureThatAPI.Models;

namespace InsureThatAPI.CommonMethods
{
    public class EnumPolicyDetails
    {
        //Enum 
        #region Enum for Insured Details 
        public enum PolicyResult
        {
            Exception = -1,
            EmailAlreadyExists = -3,
            InsertedSuccess = 1,
            UpdatedSuccess = 2,
            NoAction = 0,
            PhoneNumberLength = 9
        };
        #endregion
    }
    public class PolicyInsuredIDClass
    {
        #region GET METHOD FOR POLICYINSUREDID DETAILS
        public PolicyInsuredIDRef GetPolicyInsuredDetails(int ID)
        {
            PolicyInsuredIDRef policyInsuredRef = new PolicyInsuredIDRef();
            try
            {
                policyInsuredRef.PolicyInsureIDData = new List<PolicyInsuredID>();
                PolicyInsuredID policyInsuredIDModel = new PolicyInsuredID();
                MasterDataEntities db = new MasterDataEntities();
                var str = db.IT_CC_GET_PolicyInsured(ID).ToList();
                policyInsuredRef.ErrorMessage = new List<string>();
                if (str.Count > 0)
                {
                    foreach (var item in str)
                    {
                        policyInsuredIDModel = new PolicyInsuredID();
                        policyInsuredIDModel.PcId = item.PcId;
                        policyInsuredIDModel.TrId = item.TrId;
                        policyInsuredIDModel.FirstName = item.FirstName;
                        policyInsuredIDModel.LastName = item.LastName;
                        policyInsuredIDModel.MiddleName = item.MiddleName;
                        policyInsuredIDModel.PhoneNumber = item.PhoneNo;
                        policyInsuredIDModel.MobileNumber = item.MobileNo;
                        policyInsuredIDModel.EmailID = item.EmailID;
                        policyInsuredIDModel.DOB = item.DOB;
                        policyInsuredIDModel.PolicyInsurID = item.InsuredID;
                       
                        policyInsuredRef.PolicyInsureIDData.Add(policyInsuredIDModel);
                        policyInsuredRef.Status = "Success";
                    }
                }
                else
                {
                    policyInsuredRef.ErrorMessage.Add("No Data Available");
                    policyInsuredRef.Status = "Failure";
                }
            }
            catch (Exception xp)
            {
                policyInsuredRef.ErrorMessage.Add(xp.Message);
                policyInsuredRef.Status = "Failure";
            }
            finally
            {

            }
            return policyInsuredRef;
        }
        #endregion

        #region UPDATE METHOD FOR POLICYINSUREDID DETAILS

        public int UpdatePolicyInsuredDetails(int ID, PolicyInsuredID policyInsuredIDModel)
        {
            int icount = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                icount = db.IT_CC_UPDATE_PolicyInsured(ID, policyInsuredIDModel.PcId, policyInsuredIDModel.TrId);
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

        #region INSERT METHOD FOR POLICYINSUREDID DETAILS

        public int InsertPolicyInsuredDetails(PolicyInsuredID policyInsuredIDModel)
        {
            int icount = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                icount = db.IT_CC_InsertPolicyInsured(policyInsuredIDModel.PcId, policyInsuredIDModel.TrId, policyInsuredIDModel.PolicyInsurID);

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