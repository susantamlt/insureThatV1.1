using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class FarmPolicyMoney
    {
    }

    public class FPMoney
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
        public AtTheLocation AtTheLocationObj { get; set; }
        public LockedSafeAtLocation LockedSafeAtLocationObj { get; set; }
        public BankorOtherFinanInst BankorOtherFinanInstObj { get; set; }
        public ExcessFPMoney ExcessFPMoneyObj { get; set; }

    }
    public class AtTheLocation
    {
        public string atLocation { get; set; }
        public int EiId { get; set; }
    }
    public class LockedSafeAtLocation
    {
        public string lockedsafeatlocation { get; set; }
        public int EiId { get; set; }
    }
    public class BankorOtherFinanInst
    {
        public string bankorotherFinanInst { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessFPMoney
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
}