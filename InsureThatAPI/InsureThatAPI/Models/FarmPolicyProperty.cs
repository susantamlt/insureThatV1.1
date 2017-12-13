using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InsureThatAPI.Models
{
    public class FarmPolicyProperty
    {
    }
    public class FarmLocationDetails
    {
        public int CustomerId { get; set; }
        public string completionTrackPFP { get; set; }
    }
    public class FarmDetails
    {
        public int CustomerId { get; set; }
        public string completionTrackPFP { get; set; }
        [DataType(DataType.MultilineText)]
        public string  Aboutfarmstructures { get; set; }
        public DetailedDescription DescriptionFBObj { get; set; }
        public YearConstructedFB YearconstructedFBObj { get; set; }
        public ConstructionFB ConstructionFBObj { get; set; }
        public ContainCoolroomFB ContaincoolroomObj { get; set; }
        public SumInsuredsFB SuminsuredFBObj { get; set; }
        public UnrepairedDamageFS UnrepaireddamageObj { get; set; }
    }
    public class FarmStructures
    {
        public int CustomerId { get; set; }
        public string completionTrackPFP { get; set; }
        public SubLimitFarmFencing SublimitObj { get; set; }
        public TotalCoverFarmFencing TotalcoverObj { get; set; }
        public OtherFarmStructuresFC OtherstructurefcObj { get; set; }
        public RoofAndWallsFS RoofwallsObj { get; set; }
        public UnrepairedDamageFS UnrepaireddamageObj { get; set; }
        public HarvestedCropsExcess ExcesshcObj { get; set; }
        public int Imposed { get; set; }
    }
    public class HarvestedCrops
    {
        public int CustomerId { get; set; }
        public string completionTrackPFP { get; set; }
        public HarvestedCropsSumInsured SuminsuredhcObj { get; set; }
        public HarvestedCropsExcess ExcesshcObj { get; set; }
        public int Imposed { get; set; }
    }
    public class InterestedPartiesHC
    {
        public int CustomerId { get; set; }
        public string completionTrackPFP { get; set; }
        public InterestedPartyName PartynameObj { get; set; }
        public InterestedPartyLocation PartylocationObj { get; set; }
        public InterestedTotalSumInsured TotalsuminsuredObj { get; set; }
    }
    public class DetailedDescription
    {
        public string Description { get; set; }
        public int EiId { get; set; }
    }
    public class YearConstructedFB
    {
        public string Year { get; set; }
        public int EiId { get; set; }
    }
    public class ConstructionFB
    {
        public IEnumerable<SelectListItem> ConstructionHCList { get; set; }
        public string Construction { get; set; }
        public int EiId { get; set; }
    }
    public class ContainCoolroomFB
    {
        public string Coolroom { get; set; }
        public int EiId { get; set; }
    }
    public class SumInsuredsFB
    {
        public string Suminsured { get; set; }
        public int EiId { get; set; }
    }
    public class SubLimitFarmFencing
    {
        public string Sublimit { get; set; }
        public int EiId { get; set; }
    }
    public class TotalCoverFarmFencing
    {
        public string Totalcover { get; set; }
        public int EiId { get; set; }
    }
    public class OtherFarmStructuresFC
    {
        public string Otherstructure { get; set; }
        public int EiId { get; set; }
    }
    public class RoofAndWallsFS
    {
        public string Roofwalls { get; set; }
        public int EiId { get; set; }
    }
    public class UnrepairedDamageFS
    {
        public string Unrepaireddamage { get; set; }
        public int EiId { get; set; }
    }
    public class HarvestedCropsSumInsured
    {
        public string Suminsured { get; set; }
        public int EiId { get; set; }
    }
    public class HarvestedCropsExcess
    {
        public IEnumerable<SelectListItem> ExcessHCList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class InterestedPartyName
    {
        public string Name { get; set; }
        public int EiId { get; set; }
    }
    public class InterestedPartyLocation
    {
        public string Location { get; set; }
        public int EiId { get; set; }
    }
    public class InterestedTotalSumInsured
    {
        public string Totalsuminsured { get; set; }
        public int EiId { get; set; }
    }
}