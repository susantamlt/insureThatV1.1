using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class LiabilityCover
    {
        public List<string> Referels { get; set; }
        public string ReferralList { get; set; }
        public int? PcId { get; set; }
        public int CustomerId { get; set; }
        public bool SelectedInclusion { get; set; }
        public string PolicyStatus { get; set; }
        public List<usp_GetUnit_Result> PolicyInclusion { get; set; }
        public List<SessionModel> PolicyInclusions { get; set; }
        public ExcessLC ExcessLCObj { get; set; }
        public FarmLiabiltys FarmliabiltyObj { get; set; }
        public LimitofIndemnity LimitindemnityObj { get; set; }
        public List<usp_GetUnit_Result> ExistingPolicyInclustions { get; set; }
    }
    public class LimitofIndemnity
    {
        public int? Limitindemnity { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessLC
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class FarmLiabiltys
    {
        public string Farmliabilty { get; set; }
        public int EiId { get; set; }
    }
}