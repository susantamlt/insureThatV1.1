using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class InsuredDisclosure
    {
        public int PcId { get; set; }
        public int TrId { get; set; }
        public int PreviousInsurer { get; set; }
        public string RDBValue1 { get; set; }
        public string RDBValue2 { get; set; }
        public string RDBValue3 { get; set; }
        public string RDBValue4 { get; set; }
        public string RDBValue5 { get; set; }
        public string RDBValue6 { get; set; }
        public string RDBValue7 { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string Description { get; set; }
        public int InsurerDisclosureID { get; set; }
    }

    public class InsureDisclosureRef
    {
        public InsuredDisclosure InsuredDisclosureData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
}