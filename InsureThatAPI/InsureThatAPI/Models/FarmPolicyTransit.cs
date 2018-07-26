using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class FarmPolicyTransit
    {
    }

    public class FPTransit
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
        public LivestockMaximumValOfOneload LivestockMaxValOneLoadObj{ get; set; }
        public FarmProduceMaxValOfOneLoad FarmProduceMaxValOneLoadObj{ get; set; }
        public ExcessFPTransit ExcessFPTransitObj { get; set; }
        public AddressTsAddress AddressObj { get; set; }
    }
    public class AddressTsAddress
    {
        public string Address { get; set; }
        public int EiId { get; set; }
    }

    public class LivestockMaximumValOfOneload
    {
        public string LivestockMaxValoneload { get; set; }
        public int EiId { get; set; }
    }

    public class FarmProduceMaxValOfOneLoad
    {
        public string farmproduceMaxValoneload { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessFPTransit
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
}