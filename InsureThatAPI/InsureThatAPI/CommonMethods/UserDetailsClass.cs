using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsureThatAPI.Models;

namespace InsureThatAPI.CommonMethods
{
    public class UserDetailsClass
    {
        /// <summary>
        /// GET CUSTOMER DETAILS BY PASSING CUSTOMER ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 



        #region  GET USER DETAILS
        public GetUserDetailsRef GetUserDetails(int Id)
        {
            GetUserDetailsRef userDetailsRef = new GetUserDetailsRef();
            MasterDataEntities db = new MasterDataEntities();
            UserDetails userDetailsModel = new UserDetails();
            userDetailsRef.ErrorMessage = new List<string>();
            try
            {
                var userData = db.IT_CC_GET_UserDetails(Id).FirstOrDefault();
                if (userData != null)
                {
                    userDetailsModel.UserID = userData.UserID;
                    userDetailsModel.UserName = userData.UserName;
                    userDetailsModel.FirstName = userData.FirstName;
                    userDetailsModel.LastName = userData.LastName;
                    userDetailsModel.AddressID = userData.AddressID;
                    userDetailsModel.PostalAddressID = userData.PostalAddressID;
                    userDetailsModel.PhoneNo = userData.PhoneNo;
                    userDetailsModel.MobileNo = userData.MobileNo;
                    userDetailsModel.EmailID = userData.EmailID;
                    userDetailsModel.DOB = userData.DOB;
                    userDetailsModel.MemberOf = userData.MemberOf;
                    userDetailsModel.MembershipNumber = userData.MembershipNumber;
                    userDetailsModel.UserName = userData.UserName;
                    userDetailsRef.UserData = userDetailsModel;
                    userDetailsRef.Status = "Success";
                }
                else
                {
                    userDetailsRef.ErrorMessage.Add("No Data Available");
                    userDetailsRef.Status = "Failure";
                }
            }
            catch (Exception xp)
            {

            }
            finally
            {

            }
            return userDetailsRef;
        }
        #endregion


        #region INSERT AND UPDATE FOR USER DETAILS
        public int? InsertUpdateUserDetails(int? ID, UserDetails userDetailsModel)
        {
            int? result = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                if (ID.HasValue && ID > 0)
                {//UPDATE USER DETAILS                   
                    result = db.IT_IDC_Insert_UserDetails(ID, userDetailsModel.UserName, userDetailsModel.FirstName, userDetailsModel.LastName, userDetailsModel.AddressID, userDetailsModel.PostalAddressID, userDetailsModel.PhoneNo, userDetailsModel.MobileNo, userDetailsModel.EmailID, userDetailsModel.MemberOf, userDetailsModel.MembershipNumber).SingleOrDefault();
                }
                else
                {// INSERT USER DETAILS
                    result = db.IT_IDC_Insert_UserDetails(null, userDetailsModel.UserName, userDetailsModel.FirstName, userDetailsModel.LastName, userDetailsModel.AddressID, userDetailsModel.PostalAddressID, userDetailsModel.PhoneNo, userDetailsModel.MobileNo,  userDetailsModel.EmailID, userDetailsModel.MemberOf, userDetailsModel.MembershipNumber).SingleOrDefault();
                }
            }
            catch (Exception xp)
            {

            }
            finally
            {

            }
            return result;
        }

        #endregion

    }
}