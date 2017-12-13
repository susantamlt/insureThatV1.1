using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsureThatAPI.Models;
namespace InsureThatAPI.CommonMethods
{
    public class EnumValuablesDetails
    {
        //Enum 
        #region Enum for Insured Details 
        public enum ValuableResult
        {
            Exception = -1,
            InsertedSuccess = 1,
            UpdatedSuccess = 2,
            NoAction = 0
        };
        #endregion
    }
    public class ValuableDetailsClass
    {
        /// <summary>
        /// GET METHOD FOR VALUABLEDETAILS
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 

        #region GET METHOD FOR VALUABLEDETAILS
        public ValuableDetailsRef GetValuablesDetails(int Id)
        {
            ValuableDetailsRef valuableDetailRef = new ValuableDetailsRef();
            try
            {
                ValuableDetails valuableDetailsModel = new ValuableDetails();
                MasterDataEntities db = new MasterDataEntities();
                var str = db.IT_CC_GET_ValuablesDetails(Id).ToList();
                if (str.Count > 0)
                {
                    foreach (var item in str)
                    {
                        valuableDetailsModel = new ValuableDetails();
                        valuableDetailsModel.ValuablesUnspecifiedSumInsured = item.ValuablesUnspecifiedSumInsured;
                        valuableDetailsModel.ValuablesExcess = item.ValuablesExcess;
                        valuableDetailRef.ValuableDetailsData = valuableDetailsModel;
                        valuableDetailRef.Status = "Success";
                    }
                }
                else
                {
                    valuableDetailRef.ErrorMessage.Add("No Data Available");
                    valuableDetailRef.Status = "Failure";
                }
            }
            catch (Exception xp)
            {
                valuableDetailRef.ErrorMessage.Add(xp.Message);
                valuableDetailRef.Status = "Failure";
            }
            finally
            {

            }
            return valuableDetailRef;
        }

        #endregion

        #region UPDATE METHOD FOR VALUABLE DETAILS

        public int UpdateValuablesDetails(int ID, ValuableDetails valuableDetailsModel)
        {
            int icount = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                icount = db.IT_CC_UPDATE_ValuablesDetails(ID, valuableDetailsModel.ValuablesUnspecifiedSumInsured, valuableDetailsModel.ValuablesExcess);

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

        #region INSERT METHOD FOR VALUABLE DETAILS
        public int InsertValuablesDetails(ValuableDetails valuableDetailsModel)
        {
            int details = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                details = db.IT_CC_Insert_ValuablesDetails(valuableDetailsModel.PcId, valuableDetailsModel.TrId, valuableDetailsModel.HomeID, valuableDetailsModel.ValuablesUnspecifiedSumInsured, valuableDetailsModel.ValuablesExcess, valuableDetailsModel.PremiumId);

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