using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class ValuableItemDetails
    {
        public int PcId { get; set; }
        public int TrId { get; set; }
        public int HomeID { get; set; }
        public int ValuablesItemID { get; set; }
        public string ValuablesDescription { get; set; }
        public decimal ValuablesSumInsured { get; set; }
    }

    public class ValuableItemDetailsRef
    {
        public ValuableItemDetails ValuableItemDetailsData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }

    }

}