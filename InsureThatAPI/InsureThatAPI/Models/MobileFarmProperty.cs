using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class MobileFarmProperty
    {
    }

    public class MobileFarmContents
    {
        public List<string> Referels { get; set; }
        public string ReferralList { get; set; }
        public int? PcId { get; set; }
        public int CustomerId { get; set; }
        public bool SelectedInclusion { get; set; }
        public string PolicyStatus { get; set; }
        public List<usp_GetUnit_Result> PolicyInclusion { get; set; }
        public List<usp_GetUnit_Result> ExistingPolicyInclustions { get; set; }
        public List<SessionModel> PolicyInclusions { get; set; }
        public string ApiKey { get; set; }
        public string PolicyId { get; set; }
        public string CompletionTrackFPC { get; set; }
        public FarmContentsSumInsuredFP FarmContentsSumInsuredFPObj { get; set; }
        public OptPortableItemsFarmContentFP OptPortableItemsFarmContentFPObj { get; set; }
        public ExcessFarmContentFP ExcessFarmContentFPObj { get; set; }
        public FPUnspecifiedMachineryFM FPUnspecifiedMachineryFMObj { get; set; }
        public FPExcessforUMFM FPExcessforUMFMObj { get; set; }
        public FPDescriptionsFM FPDescriptionsFMObj { get; set; }
        public List<ValueDatas> FPDescriptionsFMList { get; set; }
        public FPYearFM FPYearFMObj { get; set; }
        public List<ValueDatas> FPYearFMList { get; set; }
        public FPSerialNumberFM FPSerialNumberFMObj { get; set; }
        public List<ValueDatas> FPSerialNumberFMList { get; set; }
        public FPExcessFM FPExcessFMObj { get; set; }
        public List<ValueDatas> FPExcessFMList { get; set; }
        public FPSumOfInsuredFM FPSumOfInsuredFMObj { get; set; }
        public List<ValueDatas> FPSumOfInsuredFMList { get; set; }
        public FPTotalSpecifiedItemsFM FPTotalSpecifiedItemsFMObj { get; set; }
        public FPDescriptionLivestock FPDescriptionLivestockObj { get; set; }
        public List<ValueDatas> FPDescriptionLivestockList { get; set; }
        public FPNumberOfAnimalsLivestock FPNumberOfAnimalsLivestockObj { get; set; }
        public List<ValueDatas> FPNumberOfAnimalsLivestockList { get; set; }
        public FPSumInsuredPerAnimalsLivestock FPSumInsuredPerAnimalsLivestockObj { get; set; }
        public List<ValueDatas> FPSumInsuredPerAnimalsLivestockList { get; set; }
        public FPTotalSumOfInsuredLivestock FPTotalSumOfInsuredLivestockObj { get; set; }
        public List<ValueDatas> FPTotalSumOfInsuredLivestockList { get; set; }
        public OptDogAttackLivestock OptDogAttackLivestockObj { get; set; }
        public FPExcessLivestock FPExcessLivestockObj { get; set; }
        public FPSumOfInsuredPerDog FPSumOfInsuredPerDogObj { get; set; }
        public FPNoOfWorkingDogs FPNoOfWorkingDogsObj { get; set; }
        public FPTotalSumInsuredWDB FPTotalSumInsuredWDBObj { get; set; }
        public FPExcessWorkingDogs FPExcessWorkingDogsObj { get; set; }
        public FPBeehivesSumInsured FPBeehivesSumInsuredObj { get; set; }
        public FPNumberOfHives FPNumberOfHivesObj { get; set; }
        public FPExcessBeehives FPExcessBeehivesObj { get; set; }
        public LocatioForFP LocationObj { get; set; }
        public AddressForFP AddressObj { get; set; }

    }

    public class MobileFarmMachinery
    {

        public int CustomerId { get; set; }
        public string CompletionTrackFPC { get; set; }
        public string PolicyInclusions { get; set; }
        public FPUnspecifiedMachineryFM FPUnspecifiedMachineryFMObj { get; set; }
        public FPExcessforUMFM FPExcessforUMFMObj { get; set; }
        public FPDescriptionsFM FPDescriptionsFMObj { get; set; }
        public FPYearFM FPYearFMObj { get; set; }
        public FPSerialNumberFM FPSerialNumberFMObj { get; set; }
        public FPExcessFM FPExcessFMObj { get; set; }
        public FPSumOfInsuredFM FPSumOfInsuredFMObj { get; set; }
        public FPTotalSpecifiedItemsFM FPTotalSpecifiedItemsFMObj { get; set; }

    }
    public class MobileLivestock
    {
        public int CustomerId { get; set; }
        public string CompletionTrackFPC { get; set; }
        public FPDescriptionLivestock FPDescriptionLivestockObj { get; set; }
        public FPNumberOfAnimalsLivestock FPNumberOfAnimalsLivestockObj { get; set; }
        public FPSumInsuredPerAnimalsLivestock FPSumInsuredPerAnimalsLivestockObj { get; set; }
        public FPTotalSumOfInsuredLivestock FPTotalSumOfInsuredLivestockObj { get; set; }
        public OptDogAttackLivestock OptDogAttackLivestockObj { get; set; }
        public FPExcessLivestock FPExcessLivestockObj { get; set; }

    }
    public class MobileWorkingDogsBeehives
    {
        public int CustomerId { get; set; }
        public string CompletionTrackFPC { get; set; }
        public FPSumOfInsuredPerDog FPSumOfInsuredPerDogObj { get; set; }
        public FPNoOfWorkingDogs FPNoOfWorkingDogsObj { get; set; }
        public FPTotalSumInsuredWDB FPTotalSumInsuredWDBObj { get; set; }
        public FPExcessWorkingDogs FPExcessWorkingDogsObj { get; set; }
        public FPBeehivesSumInsured FPBeehivesSumInsuredObj { get; set; }
        public FPNumberOfHives FPNumberOfHivesObj { get; set; }
        public FPExcessBeehives FPExcessBeehivesObj { get; set; }

    }

    public class FarmContentsSumInsuredFP
    {
        public string SumInsuredFC { get; set; }
        public int EiId { get; set; }
    }
    public class OptPortableItemsFarmContentFP
    {
        public string OptPortalableItems { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessFarmContentFP
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class FPUnspecifiedMachineryFM
    {
        public string UnspecifiedMachinery { get; set; }
        public int EiId { get; set; }
    }
    public class FPExcessforUMFM
    {
        public IEnumerable<SelectListItem> ExcessUMList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class FPDescriptionsFM
    {
        public string DescriptionFM { get; set; }
        public int EiId { get; set; }
    }
    public class FPYearFM
    {
        public string YearFM { get; set; }
        public int EiId { get; set; }
    }
    public class FPSerialNumberFM
    {
        public string SerialNumberFM { get; set; }
        public int EiId { get; set; }
    }
    public class FPExcessFM
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class FPSumOfInsuredFM
    {
        public string SuminsuredFM { get; set; }
        public int EiId { get; set; }
    }
    public class FPTotalSpecifiedItemsFM
    {
        public string TotalSpecifiedFM { get; set; }
        public int EiId { get; set; }
    }
    public class FPDescriptionLivestock
    {
        public IEnumerable<SelectListItem> DescriptionList { get; set; }
        public string Description { get; set; }
        public int EiId { get; set; }
    }
    public class FPNumberOfAnimalsLivestock
    {
        public string NumberOfanimals { get; set; }
        public int EiId { get; set; }
    }
    public class FPSumInsuredPerAnimalsLivestock
    {
        public string SumInsuredPerAnimal { get; set; }
        public int EiId { get; set; }
    }
    public class FPTotalSumOfInsuredLivestock
    {
        public string TotalSumOfInsured { get; set; }
        public int EiId { get; set; }
    }
    public class OptDogAttackLivestock
    {
        public string OptDogAttack { get; set; }
        public int EiId { get; set; }
    }
    public class FPExcessLivestock
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class FPSumOfInsuredPerDog
    {
        public string SumInsuredPerDog { get; set; }
        public int EiId { get; set; }
    }
    public class FPNoOfWorkingDogs
    {
        public string NoOfWorkingDogs { get; set; }
        public int EiId { get; set; }
    }
    public class FPTotalSumInsuredWDB
    {
        public string TotalSumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class FPExcessWorkingDogs
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class FPBeehivesSumInsured
    {
        public string BeehivesSumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class FPNumberOfHives
    {
        public string NumberOfHives { get; set; }
        public int EiId { get; set; }
    }
    public class FPExcessBeehives
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    
}