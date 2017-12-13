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
        public int CustomerId { get; set; }

        public ClassOfAnimalFP ClassOfAnimalFPObj { get; set; }

        public TypeOfAnimalFP TypeOfAnimalFPObj { get; set; }

        public BreedOfAnimalFP BreedOfAnimalFPObj { get; set; }

        public AgeOfAnimalFP AgeOfAnimalFPObj { get; set; }

        public ColourOfAnimalFP ColourOfAnimalFPObj { get; set; }

        public UseOfAnimalFP UseOfAnimalFPObj { get; set; }

        public DescBrandOfAnimalFP DescBrandOfAnimalFPObj { get; set; }

        public DescMarksOfAnimalFP DescMarksOfAnimalFPObj { get; set; }

        public OptSoundHealthofAnimalFP OptSoundHealthofAnimalFPObj { get; set; }

        public DescSoundHealthofAnimalFP DescSoundHealthofAnimalFPObj { get; set; }

        public OptDiseaseOfAnimalFP OptDiseaseOfAnimalFPObj { get; set; }

        public DescDiseaseOfAnimalFP DescDiseaseOfAnimalFPObj { get; set; }

        public OptAnimalSyndicatedFP OptAnimalSyndicatedFPObj { get; set; }

        public DescAnimalSyndicatedFP DescAnimalSyndicatedFPObj { get; set; }

        public SumInsuredLivestockFP SumInsuredLivestockFPObj { get; set; }

        public OptInfertilityFP OptInfertilityFPObj { get; set; }

        public OptLossofUseLivestockFP OptLossofUseLivestockFPObj { get; set; }

        public OptTheftLivestockFP OptTheftLivestockFPObj { get; set; }

        public OptUnbornFoalFP OptUnbornFoalFPObj { get; set; }

        public ExcessLivestockFP ExcessLivestockFPObj { get; set; }

        public NoOfContainersFP NoOfContainersFPObj { get; set; }

        public MaxStrawsandAmpoulesFP MaxStrawsandAmpoulesFPObj { get; set; }

        public MaxValOneContainerFP MaxValOneContainerFPObj { get; set; }

        public AnnualStrawsandAmpoulesFP AnnualStrawsandAmpoulesFPObj { get; set; }

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