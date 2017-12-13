using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class Boat
    {
    }
    public class BoatDetails
    {
        public int CustomerId { get; set; }
        public string CompletionTrackB { get; set; }
        public BoatNames BoatnameObj { get; set; }
        public RegistrationDetails RegistrationdetailObj { get; set; }
        public Makes MakeObj { get; set; }
        public ModelsB ModelbObj { get; set; }
        public YearOfManufacture YearmanufactureObj { get; set; }
        public LengthInMetres LengthmetreObj { get; set; }
        public TypeOfBoat TypeboatObj { get; set; }
        public HullMeterials HullmeterialObj { get; set; }
        public Speeds SpeedObj { get; set; }
        public Detectors DetectorObj { get; set; }
        public TypeOfMooringStorage MooringstorageObj { get; set; }
        public OtherPleaseDetails otherpleasedetailObj { get; set; }
        public AddressesBD AddressObj { get; set; }
    }
    public class MotorDetails
    {
        public int CustomerId { get; set; }
        public string CompletionTrackB { get; set; }
        public YearOfManufacture YearmanufactureObj { get; set; }
        public MakeAndModel MakemodelObj { get; set; }
        public SerialNumbersMD SerialnumberObj { get; set; }
        public FuelType FueltypeObj { get; set; }
        public MotorPosition MotorpositionObj { get; set; }
        public Detectors DetectorObj { get; set; }
        public DriveType DrivetypeObj { get; set; }
        public Powers PowerObj { get; set; }
        public MarketValues MarketvalueObj { get; set; }
    }
    public class BoatOperator
    {
        public int CustomerId { get; set; }
        public string CompletionTrackB { get; set; }
        public NameBOs NameboObj { get; set; }
        public YearsExperienced YearsexperienceObj { get; set; }
        public TypesofBoat TypesboatObj { get; set; }
    }
    public class CoverDetails
    {
        public int CustomerId { get; set; }
        public string CompletionTrackB { get; set; }
        public MarketValues MarketvalueObj { get; set; }
        public MotorValues MotorvalueObj { get; set; }
        public AccessoryDescription AccessorydescriptionObj { get; set; }
        public AccessorySumInsured AccessorysuminsureObj { get; set; }
        public string Coverforaccessories { get; set; }
        public string Totalcoverboat { get; set; }
        public LiabilityCD LiabilityObj { get; set; }
        public ExcessCD ExcesscdObj { get; set; }
        public ClaimFreePeriod FreeperiodObj { get; set; }
        public NoClaimDiscount NodiscountObj { get; set; }
        public string Totalsumassured { get; set; }
    }
    public class Options
    {
        public int CustomerId { get; set; }
        public string CompletionTrackB { get; set; }
        public Waterways WaterwayObj { get; set; }
        public LimitSeawardTravel LimitseawardObj { get; set; }
        public SailBoats SailboatObj { get; set; }
    }
    public class InterestedPartiesBoat
    {
        public int CustomerId { get; set; }
        public string CompletionTrackB { get; set; }
        public NameOfInstitutions InstitutionsObj { get; set; }
        public LocationsIPB LocationObj { get; set; }
    }
    public class NoClaimDiscount
    {
        public string Nodiscount { get; set; }
        public int EiId { get; set; }
    }
    public class ClaimFreePeriod
    {
        public string Freeperiod { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessCD
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class LiabilityCD
    {
        public string Liability { get; set; }
        public int EiId { get; set; }
    }
    public class AccessorySumInsured
    {
        public string Suminsured { get; set; }
        public int EiId { get; set; }
    }
    public class AccessoryDescription
    {
        public string Description { get; set; }
        public int EiId { get; set; }
    }
    public class MotorValues
    {
        public string Motorvalue { get; set; }
        public int EiId { get; set; }
    }
    public class LocationsIPB
    {
        public string Location { get; set; }
        public int EiId { get; set; }
    }
    public class NameOfInstitutions
    {
        public string Name { get; set; }
        public int EiId { get; set; }
    }
    public class SailBoats
    {
        public string Sailboat { get; set; }
        public int EiId { get; set; }
    }
    public class LimitSeawardTravel
    {
        public string Seaward { get; set; }
        public int EiId { get; set; }
    }
    public class Waterways
    {
        public string Waterway { get; set; }
        public int EiId { get; set; }
    }
    public class TypesofBoat
    {
        public string Type { get; set; }
        public int EiId { get; set; }
    }
    public class YearsExperienced
    {
        public string Year { get; set; }
        public int EiId { get; set; }
    }
    public class NameBOs
    {
        public IEnumerable<SelectListItem> NameBOList { get; set; }
        public string Name { get; set; }
        public int EiId { get; set; }
    }
    public class MarketValues
    {
        public string Marketvalue { get; set; }
        public int EiId { get; set; }
    }
    public class Powers
    {
        public string Power { get; set; }
        public int EiId { get; set; }
    }
    public class DriveType
    {
        public IEnumerable<SelectListItem> DriveList { get; set; }
        public string Drivetype { get; set; }
        public int EiId { get; set; }
    }
    public class MotorPosition
    {
        public IEnumerable<SelectListItem> MotorList { get; set; }
        public string Position { get; set; }
        public int EiId { get; set; }
    }
    public class FuelType
    {
        public IEnumerable<SelectListItem> FualList { get; set; }
        public string Type { get; set; }
        public int EiId { get; set; }
    }
    public class SerialNumbersMD
    {
        public string Serialnumber { get; set; }
        public int EiId { get; set; }

    }
    public class MakeAndModel
    {
        public string Makemodel { get; set; }
        public int EiId { get; set; }
    }
    public class AddressesBD
    {
        public IEnumerable<SelectListItem> AddressList { get; set; }
        public string Address { get; set; }
        public int EiId { get; set; }
    }
    public class OtherPleaseDetails
    {
        public string Other { get; set; }
        public int EiId { get; set; }
    }
    public class TypeOfMooringStorage
    {
        public string Mooringorstorage { get; set; }
        public int EiId { get; set; }
    }
    public class Detectors
    {
        public string Detector { get; set; }
        public int EiId { get; set; }
    }
    public class Speeds
    {
        public string Speed { get; set; }
        public int EiId { get; set; }
    }
    public class HullMeterials
    {
        public IEnumerable<SelectListItem> MeterialList { get; set; }
        public string Meterials { get; set; }
        public int EiId { get; set; }
    }
    public class TypeOfBoat
    {
        public IEnumerable<SelectListItem> TypeList { get; set; }
        public string Type { get; set; }
        public int EiId { get; set; }
    }
    public class LengthInMetres
    {
        public string Metres { get; set; }
        public int EiId { get; set; }
    }
    public class YearOfManufacture
    {
        public string Year { get; set; }
        public int EiId { get; set; }
    }
    public class ModelsB
    {
        public string Modelb { get; set; }
        public int EiId { get; set; }
    }
    public class Makes
    {
        public string Make { get; set; }
        public int EiId { get; set; }
    }
    public class RegistrationDetails
    {
        public string Registration { get; set; }
        public int EiId { get; set; }
    }
    public class BoatNames
    {
        public string Name { get; set; }
        public int EiId { get; set; }
    }
}