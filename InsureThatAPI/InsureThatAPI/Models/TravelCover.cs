using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class TravelCover
    {
        public int CustomerId { get; set; }
        public LocationTC LocationObj { get; set; }
        public UnspecificValuablesTC UnspecificvaluablesObj { get; set; }
        public NumberOfTravelers NumbertravelersObj { get; set; }
        public TravellersToBeCovered  TravellerscoveredObj  { get; set; }
        public DataOfBirthsTC DataofbirthObj { get; set; }
        public YourTrips YourtripObj { get; set; }
        public WinterSports WintersportObj { get; set; }
        public ExcessesTC ExcessObj { get; set; }
        public ImposedTC ImposedObj { get; set; }
    }
    public class TravellersToBeCovered
    {
        public string Travellerscovered { get; set; }
        public int EiId { get; set; }
    }
    public class DataOfBirthsTC
    {
        public string Dataofbirth { get; set; }
        public int EiId { get; set; }
    }
    public class WinterSports
    {
        public string Wintersport { get; set; }
        public int EiId { get; set; }
    }
    public class YourTrips
    {
        public string Yourtrip { get; set; }
        public int EiId { get; set; }
    }
    public class NumberOfTravelers
    {
        public string Numbertravelers { get; set; }
        public int EiId { get; set; }
    }
    public class LocationTC
    {
        public string Location { get; set; }
        public int EiId { get; set; }
    }
    public class UnspecificValuablesTC
    {
        public string Unspecificvaluables { get; set; }
        public int EiId { get; set; }
    }
    public class ImposedTC
    {
        public bool Imposed { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessesTC
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
}