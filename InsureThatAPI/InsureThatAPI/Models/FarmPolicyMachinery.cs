﻿using System;
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
        public int CustomerId { get; set; }

        public UnSpecTypeOfMachineryFP UnSpecTypeOfMachineryFPObj { get; set; }

        public UnSpecPowerFP UnSpecPowerFPObj { get; set; }

        public UnSpecMachNoOfUnitsFP UnSpecMachNoOfUnitsFPObj { get; set; }

        public UnSpecMachSumInsuredFP UnSpecMachSumInsuredFPObj { get; set; }

        public UnSpecMachTotalSumInsuredFP UnSpecMachTotalSumInsuredFPObj { get; set; }

        public MilkingVolumeOfVatFP MilkingVolumeOfVatFPObj { get; set; }

        public MilkingNoOfVatsFP MilkingNoOfVatsFPObj { get; set; }

        public MilkingSumInsuredFP MilkingSumInsuredFPObj { get; set; }

        public MilkingTotalSumInsuredFP MilkingTotalSumInsuredFPObj { get; set; }

        public ShearingNoOfStandsFP ShearingNoOfStandsFPObj { get; set; }

        public ShearingSumInsuredFP ShearingSumInsuredFPObj { get; set; }

        public ShearingTotalSumInsuredFP ShearingTotalSumInsuredFPObj { get; set; }

        public ExcessMachineryFP ExcessMachineryFPObj { get; set; }
        public BolierTypeOfUnitFP BolierTypeOfUnitFPObj { get; set; }

        public BolierMakeAndModelFP BolierMakeAndModelFPObj { get; set; }

        public BolierRatedPowerFP BolierRatedPowerFPObj { get; set; }

        public BolierPipeLengthFP BolierPipeLengthFPObj { get; set; }

        public BolierNoOfUnitsFP BolierNoOfUnitsFPObj { get; set; }

        public BolierSpecMachSumInsuredFP BolierSumInsuredFPObj { get; set; }

        public BolierTotalSumInsuredFP BolierTotalSumInsuredFPObj { get; set; }

        public ExcessBolierFP ExcessBolierFPObj { get; set; }

        public CoverMilkInVatsFP CoverMilkInVatsFPObj { get; set; }

        public CoverAllOtherProduceFP CoverAllOtherProduceFPObj { get; set; }
        public ExcessCoverFP ExcessCoverFPObj { get; set; }
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