using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class FarmPolicyMachinery
    {
    }
    public class FPMachinery
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
        public UnSpecTypeOfMachineryFP UnSpecTypeOfMachineryFPObj { get; set; }
        public List<ValueDatas> UnSpecTypeOfMachineryFPList { get; set; }
        public UnSpecPowerFP UnSpecPowerFPObj { get; set; }
        public List<ValueDatas> UnSpecPowerFPList { get; set; }
        public UnSpecMachNoOfUnitsFP UnSpecMachNoOfUnitsFPObj { get; set; }
        public List<ValueDatas> UnSpecMachNoOfUnitsFPList { get; set; }
        public UnSpecMachSumInsuredFP UnSpecMachSumInsuredFPObj { get; set; }
        public List<ValueDatas> UnSpecMachSumInsuredFPList { get; set; }
        public UnSpecMachTotalSumInsuredFP UnSpecMachTotalSumInsuredFPObj { get; set; }
        public List<ValueDatas> UnSpecMachTotalSumInsuredFPList { get; set; }
        public MilkingVolumeOfVatFP MilkingVolumeOfVatFPObj { get; set; }
        public List<ValueDatas> MilkingVolumeOfVatFPList { get; set; }
        public MilkingNoOfVatsFP MilkingNoOfVatsFPObj { get; set; }
        public List<ValueDatas> MilkingNoOfVatsFPList { get; set; }
        public MilkingSumInsuredFP MilkingSumInsuredFPObj { get; set; }
        public List<ValueDatas> MilkingSumInsuredFPList { get; set; }
        public MilkingTotalSumInsuredFP MilkingTotalSumInsuredFPObj { get; set; }
        public List<ValueDatas> MilkingTotalSumInsuredFPList { get; set; }
        public ShearingNoOfStandsFP ShearingNoOfStandsFPObj { get; set; }
        public List<ValueDatas> ShearingNoOfStandsFPList { get; set; }
        public ShearingSumInsuredFP ShearingSumInsuredFPObj { get; set; }
        public List<ValueDatas> ShearingSumInsuredFPList { get; set; }
        public ShearingTotalSumInsuredFP ShearingTotalSumInsuredFPObj { get; set; }
        public List<ValueDatas> ShearingTotalSumInsuredFPList { get; set; }
        public ExcessMachineryFP ExcessMachineryFPObj { get; set; }
        public SpecifiedMachineryType SMTypeObj { get; set; }
        public List<ValueDatas> SMTypeList { get; set; }
        public SpecifiedMachineryRatedpower SMRatedpowerObj { get; set; }
        public List<ValueDatas> SMRatedpowerList { get; set; }
        public SpecifiedMachineryNumberOfUnits SMNumberOfUnitsObj { get; set; }
        public List<ValueDatas> SMNumberOfUnitsList { get; set; }
        public SpecifiedMachinerySumInsuredPerUnit SMSumInsuredPerUnitObj { get; set; }
        public List<ValueDatas> SMSumInsuredPerUnitList { get; set; }
        public SpecifiedMachineryTotalSumInsured SMTotalSumInsuredObj { get; set; }
        public List<ValueDatas> SMTotalSumInsuredList { get; set; }
        public BolierTypeOfUnitFP BolierTypeOfUnitFPObj { get; set; }
        public List<ValueDatas> BolierTypeOfUnitFPList { get; set; }
        public BolierMakeAndModelFP BolierMakeAndModelFPObj { get; set; }
        public List<ValueDatas> BolierMakeAndModelFPList { get; set; }
        public BolierRatedPowerFP BolierRatedPowerFPObj { get; set; }
        public List<ValueDatas> BolierRatedPowerFPList { get; set; }
        public BolierPipeLengthFP BolierPipeLengthFPObj { get; set; }
        public List<ValueDatas> BolierPipeLengthFPList { get; set; }
        public BolierNoOfUnitsFP BolierNoOfUnitsFPObj { get; set; }
        public List<ValueDatas> BolierNoOfUnitsFPList { get; set; }
        public BolierSpecMachSumInsuredFP BolierSumInsuredFPObj { get; set; }
        public List<ValueDatas> BolierSumInsuredFPList { get; set; }
        public BolierTotalSumInsuredFP BolierTotalSumInsuredFPObj { get; set; }
        public List<ValueDatas> BolierTotalSumInsuredFPList { get; set; }
        public ExcessBolierFP ExcessBolierFPObj { get; set; }
        public CoverMilkInVatsFP CoverMilkInVatsFPObj { get; set; }
        public CoverAllOtherProduceFP CoverAllOtherProduceFPObj { get; set; }
        public ExcessCoverFP ExcessCoverFPObj { get; set; }
        public AddressMAddress AddressObj { get; set; }
    }
    public class SpecifiedMachineryType
    {
        public IEnumerable<SelectListItem> TypeSMList { get; set; }
        public string Typesm { get; set; }
        public int EiId { get; set; }
    }
    public class SpecifiedMachineryRatedpower
    {
        public string Ratedpowersm { get; set; }
        public int EiId { get; set; }
    }
    public class SpecifiedMachineryNumberOfUnits
    {
        public string Numberunitssm { get; set; }
        public int EiId { get; set; }
    }
    public class SpecifiedMachinerySumInsuredPerUnit
    {
        public string SumInsuredperunit { get; set; }
        public int EiId { get; set; }
    }
    public class SpecifiedMachineryTotalSumInsured
    {
        public string SumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class UnSpecTypeOfMachineryFP
    {
        public IEnumerable<SelectListItem> TypeofMachineryList { get; set; }
        public string TypeofMachinery { get; set; }
        public int EiId { get; set; }
    }
    public class UnSpecPowerFP
    {
        public IEnumerable<SelectListItem> UnSpecPowerList { get; set; }
        public string Power { get; set; }
        public int EiId { get; set; }
    }
    public class UnSpecMachNoOfUnitsFP
    {
        public string NoOfUnits { get; set; }
        public int EiId { get; set; }
    }
    public class AddressMAddress
    {
        public string Address { get; set; }
        public int EiId { get; set; }
    }
    public class UnSpecMachSumInsuredFP
    {
        public string SumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class UnSpecMachTotalSumInsuredFP
    {
        public string TotalSumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class MilkingVolumeOfVatFP
    {
        public IEnumerable<SelectListItem> VolumeOfVatList { get; set; }
        public string VolumeOfVat { get; set; }
        public int EiId { get; set; }
    }
    public class MilkingNoOfVatsFP
    {
        public string NoOfVats { get; set; }
        public int EiId { get; set; }
    }
    public class MilkingSumInsuredFP
    {
        public string SumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class MilkingTotalSumInsuredFP
    {
        public string TotalSumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class ShearingNoOfStandsFP
    {
        public string NoOfStands { get; set; }
        public int EiId { get; set; }
    }
    public class ShearingSumInsuredFP
    {
        public string SumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class ShearingTotalSumInsuredFP
    {
        public string TotalSumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessMachineryFP
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class BolierTypeOfUnitFP
    {
        public IEnumerable<SelectListItem> TypeofUnitList { get; set; }
        public string TypeofUnit { get; set; }
        public int EiId { get; set; }
    }
    public class BolierMakeAndModelFP
    {
        public string MakeAndModel { get; set; }
        public int EiId { get; set; }
    }
    public class BolierRatedPowerFP
    {
        public string RatedPower { get; set; }
        public int EiId { get; set; }
    }
    public class BolierPipeLengthFP
    {
        public string PipeLength { get; set; }
        public int EiId { get; set; }
    }
    public class BolierNoOfUnitsFP
    {
        public string NoOfUnits { get; set; }
        public int EiId { get; set; }
    }
    public class BolierSpecMachSumInsuredFP
    {
        public string SumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class BolierTotalSumInsuredFP
    {
        public string TotalSumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessBolierFP
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class CoverMilkInVatsFP
    {
        public string MilkInVats { get; set; }
        public int EiId { get; set; }
    }
    public class CoverAllOtherProduceFP
    {
        public string AllOtherProduce { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessCoverFP
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
}