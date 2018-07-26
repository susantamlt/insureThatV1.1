using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{

    public class TravellersToBeCoveredG
    {
        public string Travellerscovered { get; set; }
        public int EiId { get; set; }
    }
    public class DataOfBirthsTCG
    {
        public string Dataofbirth { get; set; }
        public int EiId { get; set; }
    }
    public class TravelCover
    {
        public TravellersToBeCoveredG TravellerscoveredGObj { get; set; }
        public List<ValueDatas> TravellerscoveredGObjList { get; set; }
        public DataOfBirthsTCG DataofbirthGObj { get; set; }
        public List<ValueDatas> DataofbirthGObjList { get; set; }
        public List<string> NewSections { get; set; }
        public List<string> Referels { get; set; }
        public string ReferralList { get; set; }
        public int? PcId { get; set; }
        public bool SelectedInclusion { get; set; }
        public List<SessionModel> PolicyInclusions { get; set; }
        public List<usp_GetUnit_Result> PolicyInclusion { get; set; }
        public string PolicyStatus { get; set; }
        public int CustomerId { get; set; }
        public LocationTC LocationObj { get; set; }
        public UnspecificValuablesTC UnspecificvaluablesObj { get; set; }
        public List<usp_GetUnit_Result> ExistingPolicyInclustions { get; set; }
        public NumberOfTravelers NumbertravelersObj { get; set; }
        public TravellersToBeCovered TravellerscoveredObj { get; set; }
        public List<ValueDatas> TravellerscoveredObjList { get; set; }
        public DataOfBirthsTC DataofbirthObj { get; set; }
        public List<ValueDatas> DataofbirthObjList { get; set; }
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