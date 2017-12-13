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
        public int CustomerId { get; set; }

       // public LocationsCovered LocationsCoveredObj { get; set; }

        public LivestockMaximumValOfOneload LivestockMaxValOneLoadObj{ get; set; }

        public FarmProduceMaxValOfOneLoad FarmProduceMaxValOneLoadObj{ get; set; }

        public ExcessFPTransit ExcessFPTransitObj { get; set; }

    }

    public class LivestockMaximumValOfOneload
    {
        public string livestockMaxValoneload { get; set; }
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