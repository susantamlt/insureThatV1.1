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
        public List<string> Referels { get; set; }
        public string ReferralList { get; set; }
        public int? PcId { get; set; }
        public int CustomerId { get; set; }
        public bool SelectedInclusion { get; set; }
        public string PolicyStatus { get; set; }
        public List<usp_GetUnit_Result> PolicyInclusion { get; set; }
        public List<usp_GetUnit_Result> ExistingPolicyInclustions { get; set; }
        public List<SessionModel> PolicyInclusions { get; set; }
        public string ApiKey { get; set; }
        public string PolicyId { get; set; }
        public CoverForUnspecifiedValuables CoverUnspecifiedValuablesObj { get; set; }
        public SpecifiedItemDescription SpecifiedItemDescriptionObj { get; set; }
        public List<ValueDatas> SpecifiedItemDescriptionList { get; set; }
        public SpecifiedItemSumInsured SpecifiedItemSumInsuredObj { get; set; }
        public List<ValueDatas> SpecifiedItemSumInsuredList { get; set; }
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