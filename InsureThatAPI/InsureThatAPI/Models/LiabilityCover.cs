using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class LiabilityCover
    {
        public int CustomerId { get; set; }
        public ExcessLC ExcessLCObj { get; set; }
        public FarmLiabiltys FarmliabiltyObj { get; set; }
        public LimitofIndemnity LimitindemnityObj { get; set; }
    }
    public class LimitofIndemnity
    {
        public string Limitindemnity { get; set; }
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