using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class Pets
    {
        public int CustomerId { get; set; }
        public Speciesed SpeciesObj { get; set; }
        public Breeds BreedObj { get; set; }
        public OtherBreeds OtherbreedObj { get; set; }
        public Names NameObj { get; set; }
        public DateOfBirth DatebirthObj { get; set; }
        public PreExistings PreexistingObj { get; set; }
        public DescriptionOfIllness DescriptionillnessObj { get; set; }
        public AnnualCoverLimits AnnualcoverlimitObj { get; set; }
        public ExcessPets ExcessPetObj { get; set; }
        public BoardingFees BoardingfeeObj { get; set; }
        public AnnualLimitsBF AnnuallimitbfObj { get; set; }
        public DeathFromIllness DeathillnessObj { get; set; }
        public AnnualLimitsDT AnnuallimitdtObj { get; set; }
        public DeathFromInjury DeathinjuryObj { get; set; }
        public AnnualLimitsIJ AnnuallimitijObj { get; set; }
        public PremiumPts PremiumptsObj { get; set; }
        public ImposedPets ImposedpetsObj { get; set; }
    }
    public class ImposedPets
    {
        public string Imposed { get; set; }
        public int EiId { get; set; }
    }
    public class PremiumPts
    {
        public string Premium { get; set; }
        public int EiId { get; set; }
    }
    public class DeathFromInjury
    {
        public string Deathinjury { get; set; }
        public int EiId { get; set; }
    }
    public class AnnualLimitsBF
    {
        public string Annuallimitbf { get; set; }
        public int EiId { get; set; }
    }
    public class AnnualLimitsIJ
    {
        public string Annuallimitij { get; set; }
        public int EiId { get; set; }
    }
    public class DeathFromIllness
    {
        public string Deathillness { get; set; }
        public int EiId { get; set; }
    }
    public class AnnualLimitsDT
    {
        public string Annuallimit { get; set; }
        public int EiId { get; set; }
    }
    public class BoardingFees
    {
        public string Boardingfee { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessPets
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class AnnualCoverLimits
    {
        public string Annualcoverlimit { get; set; }
        public int EiId { get; set; }
    }
    public class DescriptionOfIllness
    {
        public string Descriptionillness { get; set; }
        public int EiId { get; set; }
    }
    public class PreExistings
    {
        public string Preexisting { get; set; }
        public int EiId { get; set; }
    }
    public class DateOfBirth
    {
        public string Datebirth { get; set; }
        public int EiId { get; set; }
    }
    public class Names
    {
        public string Name { get; set; }
        public int EiId { get; set; }
    }
    public class OtherBreeds
    {
        public string Otherbreed { get; set; }
        public int EiId { get; set; }
    }
    public class Breeds
    {
        public IEnumerable<SelectListItem> BreedList { get; set; }
        public string Breed { get; set; }
        public int EiId { get; set; }
    }
    public class Speciesed
    {
        public IEnumerable<SelectListItem> SpeciesList { get; set; }
        public string Species { get; set; }
        public int EiId { get; set; }
    }
}