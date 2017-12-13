using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class FarmPolicyHome
    {
    }
    public class MainDetails
    {
        public int CustomerId { get; set; }
        public string CompletionTrackFPHB { get; set; }
        public PhysicalAddress PhysicaladdresObj { get; set; }
        public UnclearedNaturalBushland UNBushlandObj { get; set; }
    }
    public class ConstructionDetails
    {
        public int CustomerId { get; set; }
        public string CompletionTrackFPHB { get; set; }
        public ExternalWallsMade ExtwallmadeObj { get; set; }
        public DescribeExternalWalls DescribeextwallObj { get; set; }
        public IsRoofMadeOf RoofmadeObj { get; set; }
        public DescribeTheRoofs DescriberoofObj { get; set; }
        public YearCDFP YearObj { get; set; }
        public LastRewireds LastrewiredObj { get; set; }
        public LastReplumbs LastreplumbedObj { get; set; }
        public WatertightsMaintained WatertightObj { get; set; }
        public HeritageLegislationsCouncil HeritagelegislationObj { get; set; }
        public UnderConstructionsAlteration UnderconstructionObj { get; set; }
        public FitnessDomesticDwellings DomesticdwellingObj { get; set; }
    }
    public class OccupancyDetails
    {
        public int CustomerId { get; set; }
        public string CompletionTrackFPHB { get; set; }
        public WhoLivesHome WholivesObj { get; set; }
        public IsTheBuildingFPHO IsbuildingObj { get; set; }
        public ConsecutiveDaysFPHO ConsecutiveObj { get; set; }
        public UsedForBusinessFPHO UsedbusinessObj { get; set; }
        public DescribeBusinessFPHO DesbusinessObj { get; set; }
    }
    public class HomeBuilding
    {
        public int CustomerId { get; set; }
        public string CompletionTrackFPHB { get; set; }
    }
    public class InterestedPartyFPIP
    {
        public int CustomerId { get; set; }
        public string CompletionTrackFPHB { get; set; }
        public InterestedPartyNameFPIP PartynameObj { get; set; }
        public InterestedPartyLocationFPIP PartylocationObj { get; set; }
    }
    public class PhysicalAddress
    {
        public string Physicaladdres { get; set; }
        public int EiId { get; set; }
    }
    public class UnclearedNaturalBushland
    {
        public string UNBushland { get; set; }
        public int EiId { get; set; }
    }
    public class ExternalWallsMade
    {
        public List<SelectListItem> Extwallmadelist { get; set; }
        public string Extwallmade { get; set; }
        public int EiId { get; set; }
    }
    public class DescribeExternalWalls
    {
        public string Describeextwall { get; set; }
        public int EiId { get; set; }
    }
    public class IsRoofMadeOf
    {
        public List<SelectListItem> Roofmadelist { get; set; }
        public string Roofmade { get; set; }
        public int EiId { get; set; }
    }
    public class DescribeTheRoofs
    {
        public string Describeroof { get; set; }
        public int EiId { get; set; }
    }
    public class YearCDFP
    {
        public string Year { get; set; }
        public int EiId { get; set; }
    }
    public class LastRewireds
    {
        public string Lastrewired { get; set; }
        public int EiId { get; set; }
    }
    public class LastReplumbs
    {
        public string Lastreplumbed { get; set; }
        public int EiId { get; set; }
    }
    public class WatertightsMaintained
    {
        public string Watertight { get; set; }
        public int EiId { get; set; }
    }
    public class HeritageLegislationsCouncil
    {
        public string Heritagelegislation { get; set; }
        public int EiId { get; set; }
    }
    public class UnderConstructionsAlteration
    {
        public string Underconstruction { get; set; }
        public int EiId { get; set; }
    }
    public class FitnessDomesticDwellings
    {
        public string Domesticdwelling { get; set; }
        public int EiId { get; set; }
    }
    public class WhoLivesHome
    {
        public string Wholives { get; set; }
        public int EiId { get; set; }
    }
    public class IsTheBuildingFPHO
    {
        public string Isbuilding { get; set; }
        public int EiId { get; set; }
    }
    public class ConsecutiveDaysFPHO
    {
        public string Consecutive { get; set; }
        public int EiId { get; set; }
    }
    public class UsedForBusinessFPHO
    {
        public string Usedbusiness { get; set; }
        public int EiId { get; set; }
    }
    public class DescribeBusinessFPHO
    {
        public string Desbusiness { get; set; }
        public int EiId { get; set; }
    }
    public class InterestedPartyNameFPIP
    {
        public string Name { get; set; }
        public int EiId { get; set; }
    }
    public class InterestedPartyLocationFPIP
    {
        public string Location { get; set; }
        public int EiId { get; set; }
    }
}