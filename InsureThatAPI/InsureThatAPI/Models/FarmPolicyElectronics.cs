using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class FarmPolicyElectronics
    {
    }

    public class FPElectronics
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
        public ElectronicsLocationsCoveredFP ElectronicsLocationsCoveredFPObj { get; set; }
        public TypeOfUnitFP TypeOfUnitFPObj { get; set; }
        public MakeAndModelFP MakeAndModelFPObj { get; set; }
        public SerialNumberFP SerialNumberFPObj { get; set; }
        public NoOfUnitsFP NoOfUnitsFPObj { get; set; }
        public OptPortableItemsFP OptPortableItemsFPObj { get; set; }
        public SumInsuredPerUnitFP SumInsuredPerUnitFPObj { get; set; }
        public TotalSumInsuredFP TotalSumInsuredFPObj { get; set; }
        public ExcessElectronicsFP ExcessElectronicsFPObj { get; set; }
        public CoverLossOfDataFP CoverLossOfDataFPObj { get; set; }
        public ExcessCoverLossOfDataFP ExcessCoverLossOfDataFPObj { get; set; }
    }

    public class ElectronicsLocationsCoveredFP
    {
        public string LocationsCovered { get; set; }
        public int EiId { get; set; }
    }

    public class TypeOfUnitFP
    {
        public IEnumerable<SelectListItem> ElectronicsTypeofUnitList { get; set; }
        public string TypeofUnit { get; set; }
        public int EiId { get; set; }
    }

    public class MakeAndModelFP
    {
        public string MakeandModel { get; set; }
        public int EiId { get; set; }
    }

    public class SerialNumberFP
    {
        public string SerialNumber { get; set; }
        public int EiId { get; set; }
    }

    public class NoOfUnitsFP
    {
        public string NoOfUnits { get; set; }
        public int EiId { get; set; }
    }

    public class OptPortableItemsFP
    {
        public string PortableItemsOption { get; set; }
        public int EiId { get; set; }
    }

    public class SumInsuredPerUnitFP
    {
        public string SumInsuredPerUnit { get; set; }
        public int EiId { get; set; }
    }

    public class TotalSumInsuredFP
    {
        public string TotalSumInsured { get; set; }
        public int EiId { get; set; }
    }

    public class ExcessElectronicsFP
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }

    public class CoverLossOfDataFP
    {
        public string CoverLossofData { get; set; }
        public int EiId { get; set; }
    }

    public class ExcessCoverLossOfDataFP
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }

}