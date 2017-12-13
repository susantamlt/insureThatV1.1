using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsureThatAPI.Models;

namespace InsureThatAPI.CommonMethods
{
    public class EnumInsuredDisclosureDetails
    {
        //Enum 
        #region Enum for Insured Details 
        public enum InsuredDisclosureResult
        {
            Exception = -1,
            InsertedSuccess = 1,
            UpdatedSuccess = 2,
            NoAction = 0
        };
        #endregion
    }

    public class InsuredDisclosureClass
    {
        #region GET METHOD FOR INSUREDDISCLOSURE DETAILS
        public InsureDisclosureRef GetInsuredDisclosureDetails(int Id)
        {
            InsureDisclosureRef insuredisclosureRef = new InsureDisclosureRef();
            try
            {
                InsuredDisclosure insureDisclosureModel = new InsuredDisclosure();
                MasterDataEntities db = new MasterDataEntities();
                var str = db.IT_CC_GET_InsuredDisclosureDetails(Id).ToList();
                if (str.Count > 0)
                {
                    foreach (var item in str)
                    {
                        insureDisclosureModel = new InsuredDisclosure();
                        insureDisclosureModel.PreviousInsurer = item.PreviousInsurer;
                        insureDisclosureModel.RDBValue1 = item.RDBValue1;
                        insureDisclosureModel.RDBValue2 = item.RDBValue2;
                        insureDisclosureModel.RDBValue3 = item.RDBValue3;
                        insureDisclosureModel.RDBValue4 = item.RDBValue4;
                        insureDisclosureModel.RDBValue5 = item.RDBValue5;
                        insureDisclosureModel.RDBValue6 = item.RDBValue6;
                        insureDisclosureModel.RDBValue7 = item.RDBValue7;
                        insureDisclosureModel.DischargeDate = item.DischargeDate;
                        insureDisclosureModel.Description = item.Description;
                        insureDisclosureModel.PcId = item.PcId;
                        insureDisclosureModel.TrId = item.TrId;
                        insuredisclosureRef.InsuredDisclosureData = insureDisclosureModel;
                        insuredisclosureRef.Status = "Success";
                    }
                }
                else
                {
                    insuredisclosureRef.ErrorMessage.Add("No Data Available");
                    insuredisclosureRef.Status = "Failure";
                }
            }
            catch (Exception xp)
            {
                insuredisclosureRef.ErrorMessage.Add(xp.Message);
                insuredisclosureRef.Status = "Failure";
            }
            finally
            {

            }
            return insuredisclosureRef;
        }

        #endregion

        #region UPDATE METHOD FOR INSUREDDISCLOSURE DETAILS

        public int UpdateInsuredDisclosureDetails(int ID, InsuredDisclosure insureDisclosureModel)
        {
            int icount = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                icount = db.IT_CC_Update_InsuredDisclosureDetails(insureDisclosureModel.PreviousInsurer, insureDisclosureModel.DischargeDate, insureDisclosureModel.Description, ID);

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

        #region INSERT METHOD FOR INSUREDDISCLOSURE DETAILS
        public int InsertInsuredDisclosureDetails(InsuredDisclosure insureDisclosureModel)
        {
            int details = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                details = db.IT_CC_Insert_InsuredDisclosureDetails(insureDisclosureModel.PcId, insureDisclosureModel.TrId, insureDisclosureModel.PreviousInsurer, insureDisclosureModel.RDBValue1, insureDisclosureModel.RDBValue2, insureDisclosureModel.RDBValue3, insureDisclosureModel.RDBValue4, insureDisclosureModel.RDBValue5, insureDisclosureModel.RDBValue6, insureDisclosureModel.RDBValue7, insureDisclosureModel.DischargeDate, insureDisclosureModel.Description);
            }
            catch (Exception xp)
            {

            }
            finally
            {

            }
            return details;
        }

        #endregion

    }
}