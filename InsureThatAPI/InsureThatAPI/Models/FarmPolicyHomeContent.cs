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
        public OptCoverAccidentalDamageFP OptCoverAccidentalDamageFPObj { get; set; }
        public CoverForUnspecifiedContentsFP CoverForUnspecifiedContentsFPObj { get; set; }
        public DescriptionsFP DescriptionFPObj { get; set; }
        public SumInsuredFP SumInsuredFPObj { get; set; }
        public OptHCcoverOptionsFP OptHCcoverOptionsFPObj { get; set; }
        public OptHCLastPaidInsuranceFP OptHCLastPaidInsuranceFPObj { get; set; }
        public ExcessFPHContent ExcessFPHContentObj { get; set; }
    }

    public class CoverForUnspecifiedContentsFP
    {
        public string CoverUnspecifiedContent { get; set; }
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

    public class OptHCcoverOptionsFP
    {
        public string CoverOptions { get; set; }
        public int EiId { get; set; }
    }

    public class OptHCLastPaidInsuranceFP
    {
        public string LastpaidInsurance { get; set; }
        public int EiId { get; set; }

    }
    public class OptCoverAccidentalDamageFP
    {
        public string AccidentalDamage { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessFPHContent
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
}