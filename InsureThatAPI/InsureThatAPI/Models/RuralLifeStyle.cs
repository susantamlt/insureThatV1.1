using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class RuralLifeStyle
    {
    }
    public class HB2HomeDescription
    {
        public int CustomerId { get; set; }
        public string ApiKey { get; set; }
        public string CompletionTrack { get; set; }
        public PropertyTypes PropertytypeObj { get; set; }
        public Locations LocationObj { get; set; }
        public DescribeAddresses DescribeaddressObj { get; set; }
        public Addresses AddressObj { get; set; }
        public Areapropertys AreapropertyObj { get; set; }
        public IsBuildingLocateds IsbuildinglocatedObj { get; set; }
        public IEnumerable<SelectListItem> SubUrb { get; set; }
        public IEnumerable<SelectListItem> QList { get; set; }
        public SectionD SectionDatas { get; set; }

    }
    public class SectionD
    {
        public SectionDatas SectionData { get; set; }
        public string ReferralList { get; set; }
        public ProfileDatas ProfileData { get; set; }
        public List<AddressDatas> AddressData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
    public class ProfileDatas
    {
        public int UnId { get; set; }
        public string Name { get; set; }
        public int UnitNumber { get; set; }
        public string AddressID { get; set; }
        public int? ProfileUnId { get; set; }
        public List<RowsourceDatas> RowsourceData { get; set; }
        public List<ValueDatas> ValueData { get; set; }
        public List<StateDatas> StateData { get; set; }

    }
    public class AddressDatas
    {
        public int AddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public int Postcode { get; set; } 
    }
    public class SectionDatas
    {
        public int UnId { get; set; }
        public string Name { get; set; }
        public int UnitNumber { get; set; }
        public string AddressID { get; set; }
        public int ProfileUnId { get; set; }
        public List<RowsourceDatas> RowsourceData { get; set; }
        public List<ValueDatas> ValueData { get; set; }
        public List<StateDatas> StateData { get; set; }
    }
    public class StateDatas{
        public Elements Element { get; set; }
        public int State { get; set; }
    }
    public class ValueDatas{
        public Elements Element { get; set; }
        public string Value { get; set; }
    }
    public class RowsourceDatas {

        public Elements Element { get; set; }
      //  public Elements Element { get; set; }
        public List<Option> Options { get; set; }
    }
    public class Elements
    {
        public int ElId { get; set; }
        public int ItId { get; set; }
    }
    public class Option {
        public string DataText { get; set; }
        public int DataValue { get; set; }
    }

    public class HB2ConstructionDetails
    {
        public int CustomerId { get; set; }
        public string ApiKey { get; set; }
        public string CompletionTrack { get; set; }
        //public TypeBuildings TypebuildingObj { get; set; }
        //public DescribeBuildings DescribebuildingObj { get; set; }
        public ExtWallsMades ExtwallsmadeObj { get; set; }
        public Describeexternalwalls DescribeexternalwallsObj { get; set; }
        public DescribeRoofMadeof DescribeRoofMadeOffObj { get; set; }
        public RoofMades RoofmadeObj { get; set; }
        public YearOfBuilt YearofBuiltObj { get; set; }
        public Watertights WatertightObj { get; set; }
        public HeritageLegislations HeritagelegislationObj { get; set; }
        public UnderConstructions UnderconstructionObj { get; set; }
        public DomesticDwellings DomesticdwellingObj { get; set; }
    }
    public class HB2OccupancyDetails
    {
        public int CustomerId { get; set; }
        public string CompletionTrack { get; set; }
        public WhoLives WholivesObj { get; set; }
        public IsBuildings IsbuildingObj { get; set; }
        public Consecutivedays ConsecutivedayObj { get; set; }
        public IsusedBusinesses IsusedbusinessObj { get; set; }
        public DescribeBusinesses DescribebusinessObj { get; set; }
        public Premiums PremiumObj { get; set; }
    }
    public class HB2SecurityFireSafteyDetails
    {
        public OperatedDeadlocks OperateddeadlockObj { get; set; }
        public OperatedLocks OperatedlockObj { get; set; }
        public GroundFloors GroundfloorObj { get; set; }
        public AllLevels AlllevelObj { get; set; }
        public LocalSirens LocalsirenObj { get; set; }
        public BurglarAlarms BurglaralarmObj { get; set; }
        public LocalSmokAlarms LocalsmokalarmsObj { get; set; }
        public BaseSmokeAlarms BasesmokealarmObj { get; set; }
    }
    public class Describeexternalwalls
    {
        public string Describeexternalwall { get; set; }
        public int EiId { get; set; }
    }
    public class HB2InterestedParties
    {
        public string CompletionTrack { get; set; }
        public int CustomerId { get; set; }
        public Locations LocationObj { get; set; }
        public CoverHomeBuildings CoverhomebuildingObj { get; set; }
    }
    public class PolicyHistory
    {
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
        public List<PolicyData> PolicyData { get; set; }
      
    }
    public class PolicyData
    {
        public List<InsuredDetails> InsuredDetails { get; set; }
        public string TransactionNumber { get; set; }
        //  public int PrId { get; set; }
        public DateTime InceptionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string PolicyNumber { get; set; }
        public int PcId { get; set; }
        public int TrId { get; set; }
        public string PolicyStatus { get; set; }
        // public string PolicyAction { get; set; }
         public string InsuredName { get; set; }
        public string PolicyType { get; set; }
    }
    public class HB2HomeBuilding
    {
        public int CustomerId { get; set; }
        public string completiontrack { get; set; }
        public Locations LocationObj { get; set; }
        public CostForRebuilding CostforRebuildingObj { get; set; }
        public CoverHomeBuildings CoverhomebuildingObj { get; set; }
        public ClaimFreePeriods ClaimfreeperiodObj { get; set; }
        public Excesses ExcessObj { get; set; }
        public Imposes ImposedObj { get; set; }
        public NoClaimDiscounts NoclaimdiscountObj { get; set; }
        public AgeDiscounts AgediscountObj { get; set; }
        public Premiums PremiumObj { get; set; }
    }
    public class Imposes
    {
        public string Imposed { get; set; }
        public int EiId { get; set; }
    }

    public class YearOfBuilt
    {
        public string YearBuilt { get; set; }
        public int EiId { get; set; }
    }

    public class DescribeRoofMadeof
    {
        public string DescribeRoofMade { get; set; }
        public int EiId { get; set; }
    }
    public class CostForRebuilding
    {
        public int EiId { get; set; }
        public int CostforRebuilding { get; set; }
    }
    public class BaseSmokeAlarms
    {
        public bool Basesmokealarm { get; set; }
        public int EiId { get; set; }
    }
    public class LocalSmokAlarms
    {
        public bool Localsmokalarms { get; set; }
        public int EiId { get; set; }
    }
    public class BurglarAlarms
    {
        public bool Burglaralarm { get; set; }
        public int EiId { get; set; }
    }
    public class LocalSirens
    {
        public bool Localsiren { get; set; }
        public int EiId { get; set; }
    }
    public class AllLevels
    {
        public bool Alllevel { get; set; }
        public int EiId { get; set; }
    }
    public class GroundFloors
    {
        public bool Groundfloor { get; set; }
        public int EiId { get; set; }
    }
    public class OperatedLocks
    {
        public int Operatedlock { get; set; }
        public int EiId { get; set; }
    }
    public class OperatedDeadlocks
    {
        public int Operateddeadlock { get; set; }
        public int EiId { get; set; }
    }
    public class DescribeBusinesses
    {
        public int Describebusiness { get; set; }
        public int EiId { get; set; }
    }
    public class IsusedBusinesses
    {
        public int Isusedbusiness { get; set; }
        public int EiId { get; set; }
    }
    public class Consecutivedays
    {
        public int Consecutiveday { get; set; }
        public int EiId { get; set; }
    }
    public class Premiums
    {
        public string Premium { get; set; }
        public int EiId { get; set; }
    }
    public class AgeDiscounts
    {
        public string Agediscount { get; set; }
        public int EiId { get; set; }
    }
    public class NoClaimDiscounts
    {
        public string Noclaimdiscount { get; set; }
        public int EiId { get; set; }
    }
    public class Excesses
    {
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class ClaimFreePeriods
    {
        public string Claimfreeperiod { get; set; }
        public int EiId { get; set; }
    }
    public class CoverHomeBuildings
    {
        public string Coverhomebuilding { get; set; }
        public int EiId { get; set; }
    }
    public class WhoLives
    {
        public int Wholives { get; set; }
        public int EiId { get; set; }
    }
    public class DomesticDwellings
    {
        public bool Domesticdwelling { get; set; }
        public int EiId { get; set; }
    }
    public class UnderConstructions
    {
        public bool Underconstruction { get; set; }
        public int EiId { get; set; }
    }
    public class HeritageLegislations
    {
        public bool Heritagelegislation { get; set; }
        public int EiId { get; set; }
    }
    public class Watertights
    {
        public bool Watertight { get; set; }
        public int EiId { get; set; }
    }
    public class RoofMades
    {
        public IEnumerable<SelectListItem> RoofmadeList { get; set; }
        public string Roofmade { get; set; }
        public int EiId { get; set; }

    }
    public class ExtWallsMades
    {
        public IEnumerable<SelectListItem> ExtwallsmadeList { get; set; }
        public string Extwallsmade { get; set; }
        public int EiId { get; set; }
    }
    public class DescribeBuildings
    {
        public string Describebuilding { get; set; }
        public int EiId { get; set; }
    }
    public class TypeBuildings
    {
        public string Typebuilding { get; set; }
        public int EiId { get; set; }
    }
    public class IsBuildings
    {
        public int Isbuilding { get; set; }
        public int EiId { get; set; }
    }
    public class IsBuildingLocateds
    {
        public int? Isbuildinglocated { get; set; }
        public int EiId { get; set; }
    }
    public class Areapropertys
    {
        [Required(ErrorMessage = "Area of Property is required.")]
        public int? Areaproperty { get; set; }
        public int EiId { get; set; }
    }
    public class Addresses
    {
        public string Address { get; set; }
        public int EiId { get; set; }
    }
    public class DescribeAddresses
    {
        public int? Describeaddress { get; set; }
        public int EiId { get; set; }
    }
    public class Locations
    {
        public string Location { get; set; }
        public int EiId { get; set; }
    }
    public class PropertyTypes
    {
        public string Propertytype { get; set; }
        public int EiId { get; set; }
    }
    public class Valuables
    {
        public int Coverforunspecifiedvaluables { get; set; }
        public string Description { get; set; }
        public decimal SumInsured { get; set; }
        public int Excess { get; set; }
        public List<Questions> ValuablesQuestionsList { get; set; }
    }
    public class Questions
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}