using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class UserDetails
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AddressID { get; set; }
        public int PostalAddressID { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public DateTime DOB { get; set; }
        public string EmailID { get; set; }
        public string MemberOf { get; set; }
        public string MembershipNumber { get; set; }
        
        public string UserName { get; set; }

    }
    public class UserDetailsRef
    {
     
        public int UserID { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }

    }
    public class GetUserDetailsRef
    {
        public UserDetails UserData { get; set; }
    
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }

    }
}