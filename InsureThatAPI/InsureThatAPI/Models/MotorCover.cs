using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class MotorCover
    {
    }
    public class MCVehicleDescription
    {
        public int CustomerId { get; set; }
        public string completionTrackMC { get; set; }
        public GLVCategory LscategoryObj { get; set; }
        public GLVMake McmakeObj { get; set; }
        public GLVYear McyearObj { get; set; }
        public GLVFamily MCfamilyObj { get; set; }
        public GLVSelectCorDetails MCscdObj { get; set; }
        public int Estimatedrv { get; set; }
        public FMMCMake FmmcmakeObj { get; set; }
        public FMMCYear FmmcyearObj { get; set; }
        public FMMCType FmmctypeObj { get; set; }
        public FMMCSelectCorDetails FmmcscdObj { get; set; }
        public FMOwnersManual OwnersmanualObj { get; set; }
    }
    public class MCAdditionalDetails
    {
        public int CustomerId { get; set; }
        public string completionTrackMC { get; set; }
        public MCADKeptAtNight KeptnightObj { get; set; }
        public MCADAddress AdaddressObj { get; set; }
        public MCADVehicleRegistered VregisterObj { get; set; }
        public MCADRegistrationNumber RnumberObj { get; set; }
        public MCADVinNumber VnumberObj { get; set; }
        public MCADEngineNumber EnumberObj { get; set; }
        public MCADVehicleModified VmodifiedObj { get; set; }
        public MCADdescribeModified DmodifiedObj { get; set; }
        public MCADSecurityFeaturesInstalled SFinstalledObj { get; set; }
        public MCADVehicleUsed VusedObj { get; set; }
        public MCADCarryingCapacity  CcapacityObj  { get; set; }
    }    
    public class MCCoverDetails
    {
        public int CustomerId { get; set; }
        public string completionTrackMC { get; set; }
        public CoverOptionCD CoveroptionObj { get; set; }
        public CoverTypeCD CovertypeObj { get; set; }
        public MaximumMarketValue MaxMarvalObj { get; set; }
        public CaravanAnnex CaravanannexObj { get; set; }
        public UnspecifiedItems UnspecifieditemsObj { get; set; }
        public NonStandardAccessories AccessoriesObj { get; set; }
        public string Vehiclemodified { get; set; }
        public AccessoryDescriptionCD DescriptionObj { get; set; }
        public SumInsuredCD SumnsuredObj { get; set; }
        public string tCAitems { get; set; }
        public LimitOfIndemnityDC LimitindemnityObj { get; set; }
        public RatingDC RatingObj { get; set; }
        public NoClaimBonus NoclaimbonusObj { get; set; }
    }
    public class MCOptionalExtrasExcesses
    {
        public int CustomerId { get; set; }
        public string completionTrackMC { get; set; }
        public HireCarOption CaroptionObj { get; set; }
        public BasicExcess ExcessObj { get; set; }
        public bool MCOEimposed { get; set; }
    }
    public class MCInterestedParties
    {
        public int CustomerId { get; set; }
        public string completionTrackMC { get; set; }
        public MCInterestedPartyName MCPartynameObj { get; set; }
        public MCInterestedPartyLocation MCPartyLocationObj { get; set; }
    }
    public class GLVCategory
    {
        public string Category { get; set; }
        public int EiId { get; set; }
    }
    public class GLVMake
    {
        public IEnumerable<SelectListItem> MakeList { get; set; }
        public string Make { get; set; }
        public int EiId { get; set; }
    }
    public class GLVYear
    {
        public string Year { get; set; }
        public int EiId { get; set; }
    }
    public class GLVFamily
    {
        public IEnumerable<SelectListItem> FamilyList { get; set; }
        public string Family { get; set; }
        public int EiId { get; set; }
    }
    public class GLVSelectCorDetails
    {
        public IEnumerable<SelectListItem> ScdList { get; set; }
        public string Scd { get; set; }
        public int EiId { get; set; }
    }
    public class FMMCMake
    {
        public string FmMake { get; set; }
        public int EiId { get; set; }
    }
    public class FMMCYear
    {
        public string FmYear { get; set; }
        public int EiId { get; set; }
    }
    public class FMMCType
    {
        public IEnumerable<SelectListItem> FmFamilyList { get; set; }
        public string FmFamily { get; set; }
        public int EiId { get; set; }
    }
    public class FMMCSelectCorDetails
    {
        public IEnumerable<SelectListItem> FmScdList { get; set; }
        public string FmScd { get; set; }
        public int EiId { get; set; }
    }
    public class FMOwnersManual
    {
        public string Ownersmanual { get; set; }
        public int EiId { get; set; }
    }
    public class MCADKeptAtNight
    {
        public string Keptnight { get; set; }
        public int EiId { get; set; }
    }
    public class MCADAddress
    {
        public IEnumerable<SelectListItem> AddressList { get; set; }
        public string Address { get; set; }
        public int EiId { get; set; }
    }
    public class MCADVehicleRegistered
    {
        public string Register { get; set; }
        public int EiId { get; set; }
    }
    public class MCADRegistrationNumber
    {
        public string Rnumber { get; set; }
        public int EiId { get; set; }
    }
    public class MCADVinNumber
    {
        public IEnumerable<SelectListItem> VnumberList { get; set; }
        public string Vnumber { get; set; }
        public int EiId { get; set; }
    }
    public class MCADEngineNumber
    {
        public IEnumerable<SelectListItem> EnumberList { get; set; }
        public string Enumber { get; set; }
        public int EiId { get; set; }
    }
    public class MCADVehicleModified
    {
        public string Vmodified { get; set; }
        public int EiId { get; set; }
    }
    public class MCADdescribeModified
    {
        public string Dmodified { get; set; }
        public int EiId { get; set; }
    }
    public class MCADSecurityFeaturesInstalled
    {
        public bool Installed { get; set; }
        public int EiId { get; set; }
    }
    public class MCADVehicleUsed
    {
        public bool Vused { get; set; }
        public int EiId { get; set; }
    }
    public class MCADCarryingCapacity
    {
        public string Ccapacity { get; set; }
        public int EiId { get; set; }
    }
    public class MCDrivers
    {
        public int CustomerId { get; set; }
        public string completionTrackMC { get; set; }
        public DriverName DrivernameObj { get; set; }
        public DriverAge DriverageObj { get; set; }
        public DriverGender DrivergenderObj { get; set; }
        public DriverAmic DriveramicObj { get; set; }
        public UseOfVehicle UsevehicleObj { get; set; }
    }
    public class DriverName
    {
        public string Name { get; set; }
        public int EiId { get; set; }
    }
    public class DriverAge
    {
        public string Age { get; set; }
        public int EiId { get; set; }
    }
    public class DriverGender
    {
        public IEnumerable<SelectListItem> GenderList { get; set; }
        public string Gender { get; set; }
        public int EiId { get; set; }
    }
    public class DriverAmic
    {
        public string Amic { get; set; }
        public int EiId { get; set; }
    }
    public class UseOfVehicle
    {
        public string Usevehicle { get; set; }
        public int EiId { get; set; }
    }
    public class CoverOptionCD
    {
        public string Coveroption { get; set; }
        public int EiId { get; set; }
    }
    public class CoverTypeCD
    {
        public string Covertype { get; set; }
        public int EiId { get; set; }
    }
    public class MaximumMarketValue
    {
        public string Marketvalue { get; set; }
        public int EiId { get; set; }
    }
    public class CaravanAnnex
    {
        public string Annex { get; set; }
        public int EiId { get; set; }
    }
    public class UnspecifiedItems
    {
        public string Item { get; set; }
        public int EiId { get; set; }
    }
    public class NonStandardAccessories
    {
        public string Accessories { get; set; }
        public int EiId { get; set; }
    }
    public class AccessoryDescriptionCD
    {
        public IEnumerable<SelectListItem> DescriptionList { get; set; }
        public string Description { get; set; }
        public int EiId { get; set; }
    }
    public class SumInsuredCD
    {
        public string Suminsured { get; set; }
        public int EiId { get; set; }
    }
    public class LimitOfIndemnityDC
    {
        public string Indemnity { get; set; }
        public int EiId { get; set; }
    }
    public class RatingDC
    {
        public string Rating { get; set; }
        public int EiId { get; set; }
    }
    public class NoClaimBonus
    {
        public string Bonus { get; set; }
        public int EiId { get; set; }
    }
    public class HireCarOption
    {
        public string Caroption { get; set; }
        public int EiId { get; set; }
    }
    public class BasicExcess
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class MCInterestedPartyName
    {
        public string Name { get; set; }
        public int EiId { get; set; }
    }
    public class MCInterestedPartyLocation
    {
        public string Location { get; set; }
        public int EiId { get; set; }
    }
}