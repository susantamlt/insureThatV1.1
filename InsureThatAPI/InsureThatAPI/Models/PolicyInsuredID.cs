using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class PolicyInsuredID
    {
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DOB { get; set; }
        public string EmailID { get; set; }

        public string PhoneNumber { get; set; }

        public string MobileNumber { get; set; }

        public int PcId { get; set; }
        public int TrId { get; set; }
        public int PolicyInsurID { get; set; }
        public int MyProperty { get; set; }
    }

    public class PolicyInsuredIDRef
    {
        public List<PolicyInsuredID> PolicyInsureIDData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
    }

}