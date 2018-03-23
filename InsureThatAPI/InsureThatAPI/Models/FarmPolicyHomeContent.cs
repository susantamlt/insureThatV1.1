using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class FarmPolicyHomeContent
    {
    }
    public class FPHomeContents
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
        public CoverOptionFP CoveroptionFPObj { get; set; }
        public UnspecifiedFP UnspecifiedFPObj { get; set; }
        public DescriptionsFP DescriptionFPObj { get; set; }
        public List<ValueDatas> DescriptionFPList { get; set; }
        public SumInsuredFP SumInsuredFPObj { get; set; }
        public List<ValueDatas> SumInsuredFPList { get; set; }
        public ClaimFreePeriodFP ClaimperiodFPObj { get; set; }
        public NoClaimDiscountFP discountFPObj { get; set; }
        public ExcessFPHContent ExcessFPHContentObj { get; set; }
        public AgeDiscountFP AgediscountObj { get; set; }
    }
    public class AgeDiscountFP
    {
        public string Agediscount { get; set; }
        public int EiId { get; set; }
    }
    public class UnspecifiedFP
    {
        public string Unspecified { get; set; }
        public int EiId { get; set; }
    }

    public class DescriptionsFP
    {
        public IEnumerable<SelectListItem> DescriptionList { get; set; }
        public string Description { get; set; }
        public int EiId { get; set; }
    }

    public class SumInsuredFP
    {
        public string SumInsured { get; set; }
        public int EiId { get; set; }
    }

    public class ClaimFreePeriodFP
    {
        public string Claimfreeperiod { get; set; }
        public int EiId { get; set; }
    }

    public class NoClaimDiscountFP
    {
        public string discount { get; set; }
        public int EiId { get; set; }

    }
    public class CoverOptionFP
    {
        public string Coveroption { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessFPHContent
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
}