using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class FarmPolicyValuables
    {
    }

    public class FPValuables
    {
        public int CustomerId { get; set; }

        public CoverForUnspecifiedValuables CoverUnspecifiedValuablesObj { get; set; }
        
        public SpecifiedItemDescription SpecifiedItemDescriptionObj { get; set; }
        
        public SpecifiedItemSumInsured SpecifiedItemSumInsuredObj { get; set; }

        public ExcessValuables ExcessValuablesObj { get; set; }

    }

    public class CoverForUnspecifiedValuables
    {
        public string CoverUnspecifiedValuables { get; set; }
        public int EiId { get; set; }
    }

    public class SpecifiedItemDescription
    {
        public IEnumerable<SelectListItem> SpecItemDescriptionList { get; set; }
        public string ItemDescription { get; set; }
        public int EiId { get; set; }
    }


    public class SpecifiedItemSumInsured
    {
        public string ItemSumInsured { get; set; }
        public int EiId { get; set; }
    }

    public class ExcessValuables
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
}