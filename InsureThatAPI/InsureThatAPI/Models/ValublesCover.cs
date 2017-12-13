using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class ValublesCover
    {
        public int CustomerId { get; set; }
        public LocationVC LocationObj { get; set; }
        public UnspecificValuables UnspecificvaluablesObj { get; set; }
        public ExcessesVC ExcessObj { get; set; }
    }
    public class ExcessesVC
    {
        public string Excess { get; set; }
        public string EiId { get; set; }
    }
    public class UnspecificValuables
    {
        public string Unspecificvaluables { get; set; }
        public string EiId { get; set; }
    }
    public class LocationVC
    {
        public string Location { get; set; }
        public string EiId { get; set; }
    }
}