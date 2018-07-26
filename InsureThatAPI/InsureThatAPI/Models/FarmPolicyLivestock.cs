using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class FarmPolicyLivestock
    {
    }

    public class FPLivestock
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
        public ClassOfAnimalFP ClassOfAnimalFPObj { get; set; }
        public List<ValueDatas> ClassOfAnimalFPObjList { get; set; }
        public TypeOfAnimalFP TypeOfAnimalFPObj { get; set; }
        public List<ValueDatas> TypeOfAnimalFPObjList { get; set; }
        public BreedOfAnimalFP BreedOfAnimalFPObj { get; set; }
        public List<ValueDatas> BreedOfAnimalFPObjList { get; set; }
        public AgeOfAnimalFP AgeOfAnimalFPObj { get; set; }
        public List<ValueDatas> AgeOfAnimalFPObjList { get; set; }
        public ColourOfAnimalFP ColourOfAnimalFPObj { get; set; }
        public List<ValueDatas> ColourOfAnimalFPObjList { get; set; }
        public UseOfAnimalFP UseOfAnimalFPObj { get; set; }
        public List<ValueDatas> UseOfAnimalFPObjList { get; set; }
        public DescBrandOfAnimalFP DescBrandOfAnimalFPObj { get; set; }
        public List<ValueDatas> DescBrandOfAnimalFPObjList { get; set; }
        public DescMarksOfAnimalFP DescMarksOfAnimalFPObj { get; set; }
        public List<ValueDatas> DescMarksOfAnimalFPObjList { get; set; }
        public OptSoundHealthofAnimalFP OptSoundHealthofAnimalFPObj { get; set; }
        public List<ValueDatas> OptSoundHealthofAnimalFPObjList { get; set; }
        public DescSoundHealthofAnimalFP DescSoundHealthofAnimalFPObj { get; set; }
        public List<ValueDatas> DescSoundHealthofAnimalFPObjList { get; set; }
        public OptDiseaseOfAnimalFP OptDiseaseOfAnimalFPObj { get; set; }
        public List<ValueDatas> OptDiseaseOfAnimalFPObjList { get; set; }
        public DescDiseaseOfAnimalFP DescDiseaseOfAnimalFPObj { get; set; }
        public List<ValueDatas> DescDiseaseOfAnimalFPObjList { get; set; }
        public OptAnimalSyndicatedFP OptAnimalSyndicatedFPObj { get; set; }
        public List<ValueDatas> OptAnimalSyndicatedFPObjList { get; set; }
        public DescAnimalSyndicatedFP DescAnimalSyndicatedFPObj { get; set; }
        public List<ValueDatas> DescAnimalSyndicatedFPObjList { get; set; }
        public SumInsuredLivestockFP SumInsuredLivestockFPObj { get; set; }
        public List<ValueDatas> SumInsuredLivestockFPObjList { get; set; }
        public OptInfertilityFP OptInfertilityFPObj { get; set; }
        public OptLossofUseLivestockFP OptLossofUseLivestockFPObj { get; set; }
        public OptTheftLivestockFP OptTheftLivestockFPObj { get; set; }
        public OptUnbornFoalFP OptUnbornFoalFPObj { get; set; }
        public ExcessLivestockFP ExcessLivestockFPObj { get; set; }
        public NoOfContainersFP NoOfContainersFPObj { get; set; }
        public MaxStrawsandAmpoulesFP MaxStrawsandAmpoulesFPObj { get; set; }
        public MaxValOneContainerFP MaxValOneContainerFPObj { get; set; }
        public AnnualStrawsandAmpoulesFP AnnualStrawsandAmpoulesFPObj { get; set; }
        public CoverforsemenLS CoverforsemenLSObj { get; set; }
        public ExcessLivestockFP ExcessLivestockFPBObj { get; set; }
    }
    public class CoverforsemenLS
    {
        public string Coverforsemen { get; set; }
        public int EiId { get; set; }
    }

    public class ClassOfAnimalFP
    {
        public IEnumerable<SelectListItem> ClassOfAnimalList { get; set; }
        public string ClassofAnimal { get; set; }
        public int EiId { get; set; }
    }

    public class TypeOfAnimalFP
    {
        public IEnumerable<SelectListItem> TypeOfAnimalList { get; set; }
        public string TypeofAnimal { get; set; }
        public int EiId { get; set; }
    }

    public class BreedOfAnimalFP
    {
        public string BreedofAnimal { get; set; }
        public int EiId { get; set; }
    }

    public class AgeOfAnimalFP
    {
        public IEnumerable<SelectListItem> AgeOfAnimalList { get; set; }
        public string AgeofAnimal { get; set; }
        public int EiId { get; set; }
    }

    public class ColourOfAnimalFP
    {
        public string ColourofAnimal { get; set; }
        public int EiId { get; set; }
    }

    public class UseOfAnimalFP
    {
        public IEnumerable<SelectListItem> UseOfAnimalList { get; set; }
        public string UseofAnimal { get; set; }
        public int EiId { get; set; }
    }

    public class DescBrandOfAnimalFP
    {
        public string BrandofAnimal { get; set; }
        public int EiId { get; set; }
    }

    public class DescMarksOfAnimalFP
    {
        public string MarkingsofAnimal { get; set; }
        public int EiId { get; set; }
    }

    public class OptSoundHealthofAnimalFP
    {
        public string HealthofAnimal { get; set; }
        public int EiId { get; set; }
    }

    public class OptDiseaseOfAnimalFP
    {
        public string DiseaseofAnimal { get; set; }
        public int EiId { get; set; }
    }

    public class DescSoundHealthofAnimalFP
    {
        public string Description { get; set; }
        public int EiId { get; set; }
    }

    public class DescDiseaseOfAnimalFP
    {
        public string Description { get; set; }
        public int EiId { get; set; }
    }

    public class OptAnimalSyndicatedFP
    {
        public string AnimalSyndicated { get; set; }
        public int EiId { get; set; }
    }

    public class DescAnimalSyndicatedFP
    {
        public string Description { get; set; }
        public int EiId { get; set; }
    }

    public class SumInsuredLivestockFP
    {
        public string SumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class OptInfertilityFP
    {
        public string Infertility { get; set; }
        public int EiId { get; set; }
    }

    public class OptLossofUseLivestockFP
    {
        public string LossofUse { get; set; }
        public int EiId { get; set; }
    }

    public class OptTheftLivestockFP
    {
        public string TheftOption { get; set; }
        public int EiId { get; set; }
    }
    public class OptUnbornFoalFP
    {
        public string UnbornFoal { get; set; }
        public int EiId { get; set; }
    }

    public class ExcessLivestockFP
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }

    public class NoOfContainersFP
    {
        public string NumberOfContainers { get; set; }
        public int EiId { get; set; }
    }

    public class MaxStrawsandAmpoulesFP
    {
        public string StrawAndAmpoules { get; set; }
        public int EiId { get; set; }
    }
    public class MaxValOneContainerFP
    {
        public string MaxValoneContainer { get; set; }
        public int EiId { get; set; }
    }

    public class AnnualStrawsandAmpoulesFP
    {
        public string AnnualStrawandAmpoules { get; set; }
        public int EiId { get; set; }
    }
}