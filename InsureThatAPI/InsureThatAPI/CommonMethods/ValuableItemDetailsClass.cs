using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsureThatAPI.Models;
namespace InsureThatAPI.CommonMethods
{
    public class ValuableItemsDetailsClass
    {

        #region GET METHOD FOR VALUABLE ITEM DETAILS
        public ValuableItemDetailsRef GetValuablesItemDetails(int Id)
        {
            ValuableItemDetailsRef valuableItemRef = new ValuableItemDetailsRef();
            try
            {
                ValuableItemDetails valuableItemModel = new ValuableItemDetails();
                MasterDataEntities db = new MasterDataEntities();
                var str = db.IT_CC_GET_ValuablesItemDetails(Id).ToList();
                if (str.Count > 0)
                {
                    foreach (var item in str)
                    {
                        valuableItemModel = new ValuableItemDetails();
                        valuableItemModel.ValuablesSumInsured = item.ValuablesSumInsured;
                        valuableItemModel.ValuablesDescription = item.ValuablesDescription;
                        valuableItemRef.ValuableItemDetailsData = valuableItemModel;
                        valuableItemRef.Status = "Success";
                    }
                }
                else
                {
                    valuableItemRef.ErrorMessage.Add("No Data Available");
                    valuableItemRef.Status = "Failure";
                }
            }
            catch (Exception xp)
            {
                valuableItemRef.ErrorMessage.Add(xp.Message);
                valuableItemRef.Status = "Failure";
            }
            finally
            {

            }
            return valuableItemRef;
        }

        #endregion

        #region UPDATE METHOD FOR VALUABLE ITEM DETAILS

        public int UpdateValuablesItemDetails(int ID, ValuableItemDetails valuableItemModel)
        {
            int icount = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                icount = db.IT_CC_UPDATE_ValuablesItemDetails(ID, valuableItemModel.ValuablesDescription);
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

        #region INSERT METHOD FOR VALUABLE ITEM DETAILS
        public int InsertValuablesItemDetails(ValuableItemDetails valuableItemModel)
        {
            int details = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                details = db.IT_CC_Insert_ValuablesItems(valuableItemModel.PcId, valuableItemModel.TrId, valuableItemModel.HomeID, valuableItemModel.ValuablesItemID, valuableItemModel.ValuablesDescription, valuableItemModel.ValuablesSumInsured);

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