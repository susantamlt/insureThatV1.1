using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class PolicyLogDetails
    {
        public int UserID { get; set; }
        public int PolicyNumber { get; set; }
        public int PolicyLogID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Details { get; set; }
    }

    public class PolicyLogRef
    {
        public PolicyLogDetails PolicyLogData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
    }

}