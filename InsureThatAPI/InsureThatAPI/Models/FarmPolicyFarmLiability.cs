using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class FarmPolicyFarmLiability
    {
    }
    public class FPFarmliability
    {
        public int CustomerId { get; set; }
        public GenLiabilityLimitOfIndemnityFP GenLiabilityLimitOfIndemnityFPObj { get; set; }

        public ProdLiabilityLimitOfIndemnityFP ProdLiabilityLimitOfIndemnityFPObj { get; set; }

        public ListOfProductsSoldFP ListOfProductsSoldFPObj { get; set; }

        public OptPayingGuestFP OptPayingGuestFP { get; set; }

        public TypeOfAccomGuestsStayingInFP TypeOfAccomGuestsStayingInFPObj { get; set; }

        public DescriptionOfAccommodationFP DescriptionOfAccommodationFPObj { get; set; }

        public OptAccomComplyRegulationFP OptAccomComplyRegulationFPObj { get; set; }

        public OptHolidayFarmsFP OptHolidayFarmsFPObj { get; set; }

        public ExcessFPFarmLiability ExcessFPFarmLiabilityObj { get; set; }
               
    }

    public class GenLiabilityLimitOfIndemnityFP
    {
        public IEnumerable<SelectListItem> GLLimitOfIndemnityFPList { get; set; }
        public string LimitOfIndemnity { get; set; }
        public int EiId { get; set; }
    }

    public class ProdLiabilityLimitOfIndemnityFP
    {
        public string LimitOfIndemnity { get; set; }
        public int EiId { get; set; }
    }

    public class ListOfProductsSoldFP
    {
        public string ListOfProductsSold { get; set; }
        public int EiId { get; set; }
    }
    public class OptPayingGuestFP
    {
        public string PayingGuestOption { get; set; }
        public int EiId { get; set; }
    }
    public class TypeOfAccomGuestsStayingInFP
    {
        public IEnumerable<SelectListItem> TypeOfAccommodationGuestsStayingInList { get; set; }
        public string TypeOfAccommodation { get; set; }
        public int EiId { get; set; }
    }
    public class DescriptionOfAccommodationFP
    {
        public IEnumerable<SelectListItem> DescOfAccommodationFP { get; set; }
        public string DescOfAccommodation { get; set; }
        public int EiId { get; set; }
    }

    public class OptAccomComplyRegulationFP
    {
        public string AccomComplyRegulations { get; set; }
        public int EiId { get; set; }
    }

    public class OptHolidayFarmsFP
    {
        public string HolidayFarms { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessFPFarmLiability
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
}
