using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace InsureThatAPI.Models
{
    public class Disclosure
    {
    }
    public class DisclosureDetails
    {
        public List<SessionModel> PolicyInclusions { get; set; }
        public int? PcId { get; set; }
        public int CustomerId { get; set; }
        public bool SelectedInclusion { get; set; }
        public string PolicyStatus { get; set; }
        public List<usp_GetUnit_Result> PolicyInclusion { get; set; }
        public List<usp_GetUnit_Result> ExistingPolicyInclustions { get; set; }
        //public List<string> PolicyInclusions { get; set; }
        public string ApiKey { get; set; }
        public string PolicyId { get; set; }
        public int? PrId { get; set; }
        public List<ValueData> ValueData { get; set; }
        public PreviousInsurer PreviousinsurerObj { get; set; }
        public RenewPolicy RenewpolicyObj { get; set; }
        public CriminalOffence CriminaloffenceObj { get; set; }
        public PrisonSentence  PrisonsentenceObj  { get; set; }
        public Undischarged UndischargedObj { get; set; }
        public Bankrupts BankruptObj { get; set; }
        public DischargeDate DateObj { get; set; }
        public ImmediateDanger ImmediatedangerObj { get; set; }
        public DutyOfDisclosure DutydisclosureObj { get; set; }
        public DetailsBox DetailsboxObj { get; set; }
        public string ProvideDetails { get; set; }
    }
    public class PreviousInsurer
    {
        public string Pinsurer { get; set; }
        public int EiId { get; set; }
    }
    public class RenewPolicy
    {
        public string Rpolicy { get; set; }
        public int EiId { get; set; }
    }
    public class CriminalOffence
    {
        public string Offence { get; set; }
        public int EiId { get; set; }
    }
    public class PrisonSentence
    {
        public string Sentence  { get; set; }
        public int EiId { get; set; }
    }
    public class Undischarged
    {
        public string Undischarge { get; set; }
        public int EiId { get; set; }
    }
    public class Bankrupts
    {
        public string Bankrupt { get; set; }
        public int EiId { get; set; }
    }
    public class DischargeDate
    {
        public string Date { get; set; }
        public int EiId { get; set; }
    }
    public class ImmediateDanger
    {
        public string Danger { get; set; }
        public int EiId { get; set; }
    }
    public class DutyOfDisclosure
    {
        public string Dutydisclosure { get; set; }
        public int EiId { get; set; }
    }
    public class DetailsBox
    {
        public string Details { get; set; }
        public int EiId { get; set; }
    }
}