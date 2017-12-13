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
        public int CustomerId { get; set; }

        public OptCoverAccidentalDamageFP OptCoverAccidentalDamageFPObj { get; set; }

        public CoverForUnspecifiedContentsFP CoverForUnspecifiedContentsFPObj { get; set; }

        public DescriptionsFP DescriptionFPObj { get; set; }

        public SumInsuredFP SumInsuredFPObj { get; set; }

        public OptHCcoverOptionsFP OptHCcoverOptionsFPObj { get; set; }

        public OptHCLastPaidInsuranceFP OptHCLastPaidInsuranceFPObj { get; set; }

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
}