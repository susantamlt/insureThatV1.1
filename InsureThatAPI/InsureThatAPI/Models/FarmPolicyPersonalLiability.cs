using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class FarmPolicyPersonalLiability
    {
    }
    public class PersonalLiability
    {
        public int CustomerId { get; set; }
        public LimitOfIndemnity LimitindemnityObj { get; set; }
        public ExcessPL ExcessplObj { get; set; }
    }

    public class LimitOfIndemnity
    {
        public string Limitindemnity { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessPL
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
}