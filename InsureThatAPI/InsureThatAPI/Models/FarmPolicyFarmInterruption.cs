using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class FPFarmInterruption
    {
        public int CustomerId { get; set; }
        public ExpFarmIncomeNextYearFP ExpFarmIncomeNextYearFPObj { get; set; }
        public OptFarmIncomeIndemnityPerFP OptFarmIncomeIndemnityPerFPObj { get; set; }
        public SumInsuredFarmIncomeFP SumInsuredFarmIncomeFPObj { get; set; }
        public ExcessFarmIncomeFP ExcessFarmIncomeFPObj { get; set; }
        public ExpAgistIncomeNextYearFP ExpAgistIncomeNextYearFPObj { get; set; }
        public OptAgistIncomeIndemnityPerFP OptAgistIncomeIndemnityPerFPObj { get; set; }
        public SumInsuredAgistIncomeFP SumInsuredAgistIncomeFPObj { get; set; }
        public ExcessAgistIncomeFP ExcessAgistIncomeFPObj { get; set; }
        public OptExtraCostIndemnityPerFP OptExtraCostIndemnityPerFPObj { get; set; }
        public SumInsuredExtraCostFP SumInsuredExtraCostFPObj { get; set; }
        public ExcessExtraCostFP ExcessExtraCostFPObj { get; set; }
        public SumInsuredShearingDelayFP SumInsuredShearingDelayFPObj { get; set; }
        public ExcessShearingDelayFP ExcessShearingDelayFPObj { get; set; }
    }

    public class ExpFarmIncomeNextYearFP
    {
        public string FarmIncomeNextYear { get; set; }
        public int EiId { get; set; }
    }

    public class OptFarmIncomeIndemnityPerFP
    {
        public string OptIndemnityPeriod { get; set; }
        public int EiId { get; set; }
    }
    public class SumInsuredFarmIncomeFP
    {
        public string SumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessFarmIncomeFP
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class ExpAgistIncomeNextYearFP
    {
        public string AgistIncomeNextYear { get; set; }
        public int EiId { get; set; }
    }

    public class OptAgistIncomeIndemnityPerFP
    {
        public string OptIndemnityPeriod { get; set; }
        public int EiId { get; set; }
    }
    public class SumInsuredAgistIncomeFP
    {
        public string SumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessAgistIncomeFP
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class OptExtraCostIndemnityPerFP
    {
        public string OptIndemnityPeriod { get; set; }
        public int EiId { get; set; }
    }
    public class SumInsuredExtraCostFP
    {
        public string SumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessExtraCostFP
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class SumInsuredShearingDelayFP
    {
        public string SumInsured { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessShearingDelayFP
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
}