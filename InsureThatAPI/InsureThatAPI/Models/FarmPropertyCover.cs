using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class FarmPropertyCover
    {
    }
    public class FarmContents
    {
        public int CustomerId { get; set; }
        public string CompletionTrackFPC { get; set; }
        public DescriptionsFC DescriptionFCObj { get; set; }
        public YearFPC YearObj { get; set; }
        public MaterialsFC MaterialsObj { get; set; }
        public CoolroomsFC CoolroomFcObj { get; set; }
        public SumOfInsured SuminsuredObj { get; set; }
        public confirmFarmStructures confirmfsObj { get; set; }
        public FarmFencingFC FarmfencingObj { get; set; }
        public FarmFencingTC FarmcencingTcObj { get; set; }
        public OtherFarmStructures FarmstructuresObj { get; set; }
        public FarmContentsFC FarmContentFcObj{ get; set; }
        public ExcessesFPC ExcessesFpcObj { get; set; }
    }
    public class FarmMachinery
    {
        public int CustomerId { get; set; }
        public string CompletionTrackFPC { get; set; }
        public FarmContentsFC FarmContentFcObj { get; set; }
        public ExcessforUM ExcessUMObj { get; set; }
        public DescriptionsFM DescriptionFmObj { get; set; }
        public YearFPC YearObj { get; set; }
        public SerialNumbers SerialnumberObj { get; set; }        
        public ExcessesFPC ExcessesFpcObj { get; set; }
        public SumOfInsured SuminsuredObj { get; set; }
        public TotalSpecifiedItems TotalspecifieditemObj { get; set; }
    }
    public class Livestock
    {
        public int CustomerId { get; set; }
        public string CompletionTrackFPC { get; set; }
        public DescriptionsFC DescriptionFCObj { get; set; }
        public NumberOfAnimals NumberanimalObj { get; set; }
        public SumInsuredPerAnimals SuminsuredperObj { get; set; }
        public SumOfInsured SuminsuredObj { get; set; }
        public TotalForLiveStock TotallivestockObj { get; set; }
        public DogAttackOption DogattackObj { get; set; }
        public ExcessesFPC ExcessesFpcObj { get; set; }
    }
    public class HarvestedCropsBeehives
    {
        public int CustomerId { get; set; }
        public string CompletionTrackFPC { get; set; }
        public SumOfInsured SuminsuredObj { get; set; }
        public ExcessesFPC ExcessesFpcObj { get; set; }
        public SumOfInsuredHCB SuminsuredHbcObj { get; set; }
        public NumberOfHives NumberhiveObj { get; set; }
        public ExcessesBeehives ExcessBObj { get; set; }

    }
    public class ExcessesBeehives
    {
        public IEnumerable<SelectListItem> ExcessBList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class NumberOfHives
    {
        public string Numberhive { get; set; }
        public int EiId { get; set; }
    }
    public class SumOfInsuredHCB
    {
        public string Suminsured { get; set; }
        public int EiId { get; set; }
    }
    public class DogAttackOption
    {
        public string Dogattack { get; set; }
        public int EiId { get; set; }
    }
    public class TotalForLiveStock
    {
        public string Totallivestock { get; set; }
        public int EiId { get; set; }
    }
    public class NumberOfAnimals
    {
        public string Numberanimal { get; set; }
        public int EiId { get; set; }
    }
    public class SumInsuredPerAnimals
    {
        public string Suminsuredper { get; set; }
        public int EiId { get; set; }
    }
    public class TotalSpecifiedItems
    {
        public string Totalspecifieditem { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessforUM
    {
        public List<SelectListItem> ExcessumList { get; set; }
        public string Excessum { get; set; }
        public int EiId { get; set; }
    }
    public class SerialNumbers
    {
        public string Serialnumber { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessesFPC
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class FarmContentsFC
    {
        public string Farmcontents { get; set; }
        public int EiId { get; set; }
    }
    public class OtherFarmStructures
    {
        public string Farmstructures { get; set; }
        public int EiId { get; set; }
    }
    public class FarmFencingTC
    {
        public string Totalcover { get; set; }
        public int EiId { get; set; }
    }
    public class FarmFencingFC
    {
        public string Farmfencing { get; set; }
        public int EiId { get; set; }
    }
    public class DescriptionsFM
    {
        public string Description { get; set; }
        public int EiId { get; set; }
    }
    public class DescriptionsFC
    {
        public IEnumerable<SelectListItem> DescriptionList { get; set; }
        public string Description { get; set; }
        public int EiId { get; set; }
    }
    public class YearFPC
    {
        public string Year { get; set; }
        public int EiId { get; set; }
    }
    public class MaterialsFC
    {
        public IEnumerable<SelectListItem> MaterialsList { get; set; }
        public string Materials { get; set; }
        public int EiId { get; set; }

    }
    public class CoolroomsFC
    {
        public string Coolroom { get; set; }
        public int EiId { get; set; }
    }
    public class SumOfInsured
    {
        public string Suminsured { get; set; }
        public int EiId { get; set; }
    }
    public class confirmFarmStructures
    {
        public string Confirm { get; set; }
        public int EiId { get; set; }
    }
}