using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class ValuableDetails
    {
        public int PcId { get; set; }
        public int TrId { get; set; }
        public int HomeID { get; set; }
        public decimal ValuablesUnspecifiedSumInsured { get; set; }
        public decimal ValuablesExcess { get; set; }
        public int PremiumId { get; set; }
        public int ValuablesDetailID { get; set; }
    }

    public class ValuableDetailsRef
    {
        public ValuableDetails ValuableDetailsData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }

    }
}