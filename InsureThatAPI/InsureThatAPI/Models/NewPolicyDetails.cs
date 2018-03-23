using InsureThatAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class SessionObject
    {
        public List<SessionModel> PolicyIncList { get; set; }
    }
    public class SessionModel
    {
        public int? UnitId { get; set; }
        public int? ProfileId { get; set; }
        public string name { get; set; }
    }
    public class PrintDataList
    {
        public string Name { get; set; }
        public int PrintId { get; set; }
    }
    public class PrintDocument
    {
        public string ApiKey { get; set; }
        public int? PcId { get; set; }
        public int TrId { get; set; }
        public List<PrintDataList> PrintData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
    public class PolicyTypes
    {
        public int InsureId { get; set; }
        public int? cid { get; set; }
        public int PolicyType { get; set; }
        public bool Policy { get; set; }
    }
    public class PolicyInclustions
    {
        public int? PolicyType { get; set; }
        public string PolicyInclusions { get; set; }
        public List<usp_GetUnit_Result> PolicyInclusionssess { get; set; }
        public List<IT_GetPolicyInclusions_Result> PolicyInclusion { get; set; }
        public int? CustomerId { get; set; }
        public bool HomeBuilding { get; set; }
        public bool HomeContents { get; set; }
        public bool Travels { get; set; }
        public bool Valuables { get; set; }
        public bool FarmProperty { get; set; }
        public bool Liability { get; set; }
        public bool Boat { get; set; }
        public bool Motor { get; set; }
        public bool Pet { get; set; }

        public bool MobileFarmProperty { get; set; }
        public bool FixedFarmProperty { get; set; }
        public bool FarmInteruption { get; set; }
        public bool FarmLiability { get; set; }
        public bool Burglary { get; set; }
        public bool Electronics { get; set; }
        public bool Money { get; set; }
        public bool Transit { get; set; }
        public bool ValuablesFarm { get; set; }
        public bool LiveStockFarm { get; set; }
        public bool PersonalLiabilitiesFarm { get; set; }
        public bool HomeBuildingFarm { get; set; }
        public bool HomeContent { get; set; }
        public bool Machinery { get; set; }
        public bool MotorFarm { get; set; }

    }
    public class PolicyDetailss
    {
        public string PcId { get; set; }
        public string TrId { get; set; }
        public string PolicyNumber { get; set; }
        public string AccountManagerID { get; set; }
        public string PolicyStatus { get; set; }
        public string CoverPeriod { get; set; }
        public string CoverPeriodUnit { get; set; }
        public DateTime InceptionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string ProductID { get; set; }
        public string FloodCover { get; set; }
        public string InsuredFullName { get; set; }
        public int PolicyType { get; set; }
        public string RemoveStampDuty { get; set; }
        public string CreatedByUserID { get; set; }
        public DateTime Timestamp { get; set; }
        public string InsuredName { get; set; }

        public string Reason { get; set; }
        public string Broker { get; set; }
        public string PolicyDetailsID { get; set; }
        public string IsClaimed { get; set; }
        public int PrId { get; set; }
        public List<UnitDatas> UnitData { get; set; }
        public List<InsuredDetails> InsuredDetails { get; set; }
        public List<string> ErrorMessage { get; set; }
        public List<string> ReferralList { get; set; }
    }
    public class PolicyDetails
    {
        public string Timecreated { get; set; }
        public int CreatedbyUserId { get; set; }
        public bool RemoveStampDuty { get; set; }
        public int CustomerId { get; set; }
        public int PcId { get; set; }
        public string PolicyNumber { get; set; }
        public string TransactionNumber { get; set; }
        public int TermNumber { get; set; }
        public string AccountManagerID { get; set; }
        public string PolicyStatus { get; set; }
        public int CoverPeriod { get; set; }
        public string CoverPeriodUnit { get; set; }
        public DateTime InceptionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int? PrId { get; set; }
        public int? TrId { get; set; }
        public int? IyId { get; set; }
        public string InsuredName { get; set; }
        public bool IsFloodCoverRequired { get; set; }
        public bool HasMadeAClaim { get; set; }
        public string PolicyInclusions { get; set; }
        //public PolicyDetails PolicyData { get; set; }
        public List<InsuredDetails> InsuredDetails { get; set; }
        //public List<RiskDetails> RiskData { get; set; }
        //public List<PremiumDetails> PremiumData { get; set; }'

        public List<string> IdentifierUpdates { get; set; }
        public string ReferralList { get; set; }
        public string UserMessage { get; set; }
        public List<UnitDatas> UnitData { get; set; }
        public string Status { get; set; }
        //public List<string> ErrorMessage { get; set; }
        //public List<string> ReferralList { get; set; }
        public string Reason { get; set; }
        public int? InsuredId { get; set; }
    }
    public class NewPolicyDetailsRef
    {
        public int PolicyDetailsID { get; set; }

        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
    public class GetNewPolicyDetailsRef
    {

        public List<PolicyDetails> PolicyData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
    public class Location
    {
        public string AddressLine { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
    }
    public class LocationList
    {
        public List<Location> LocalityData { get; set; }
    }
    public class PolicyList
    {
        public List<PolicyDetails> PolicyListDetails { get; set; }
    }
    public class ElementDetails
    {
        public string ApiKey { get; set; }
        public int ProfileUnId { get; set; }
        public int SectionUnId { get; set; }
        public int ElId { get; set; }
        public int ItId { get; set; }
        public string Value { get; set; }
        public int PcId { get; set; }
        public int TrId { get; set; }
        public List<ValueData> ValueData { get; set; }
        public List<StateData> StateData { get; set; }
        public List<RowsourceDatas> RowsourceData { get; set; }
        public FocusElement FocusElement { get; set; }
        public string ReferralList { get; set; }
        public string UserMessage { get; set; }
        public bool Status { get; set; }
        public List<string> ErrorMessage { get; set; }


    }
    public class FocusElement
    {
        public int ELId { get; set; }
        public int ItId { get; set; }
    }
    public class ValueData
    {
        public Elements Element { get; set; }
        public string Value { get; set; }
    }
    public class ValueDatass
    {
        public Elementss Element { get; set; }
        public string Value { get; set; }
    }
    public class StateData
    {
        public Elements Element { get; set; }
        public string State { get; set; }
        public string Value { get; set; }
    }
    public class PremiumDetail
    {

        public string ApiKey { get; set; }
        public int PcId { get; set; }
        public int TrId { get; set; }
      //  public int MyProperty { get; set; }
        public List<PremiumDetails> PremiumData { get; set; }
        public float UnderwriterFee { get; set; }
        public float FeeGst { get; set; }
        public float InvoiceTotal { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }

    }
    public class Floodarea
    {
        public string ApiKey { get; set; }
        public string InceptionDate { get; set; }
        public string ExpiryDate { get; set; }
        public string EffectiveDate { get; set; }
        public int? IsFloodRequired { get; set; }
        public int? HasMadeAClaim { get; set; }
        public int? FldDefault { get; set; }
        public int? CustomerId { get; set; }
        public int? policyType { get; set; }
        public int? insureId { get; set; }
        public string Periodofcover { get; set; }
        public string Isbuildinglocated { get; set; }

    }
    public class HomeProfile
    {
        public string ApiKey { get; set; }
        public int UnId { get; set; }
        public int AddressID { get; set; }
        public string AddressLine { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }


    }
        public class ViewEditPolicyDetails
    {

        public List<AddressData> AddressList { get; set; }
        public string PolicyStatus { get; set; }
        public int? AddressID { get; set; }
        public int CustomerId { get; set; }
        public string PcId { get; set; }
        public string ApiKey { get; set; }
        public List<usp_GetUnit_Result> PolicyInclusion { get; set; }
        public List<SessionModel> PolicyInc { get; set; }
        public List<IT_GetPolicyInclusions_Result> PolicyInclusionssess { get; set; }
        public string PolicyInclusions { get; set; }
        public PolicyDetails PolicyData { get; set; }
        public List<InsuredDetails> InsuredDetails { get; set; }
        public List<RiskDetails> RiskData { get; set; }
        public List<PremiumDetails> PremiumData { get; set; }
        public List<UnitDatas> UnitData { get; set; }
        public List<AddressData> AddressData { get; set; }
        public SectionDatas SectionData { get; set; }
        public ProfileDatas ProfileData { get; set; }
        public string Status { get; set; }
        public bool SelectedInclusion { get; set; }
        public List<string> ErrorMessage { get; set; }
        public string ReferralList { get; set; }
        public float? UnderWritterFee { get; set; }
        public float? GSTonFee { get; set; }
        public float? InvoiceAmount { get; set; }
        public List<string> ReferralFullList { get; set; }
        public string Reason { get; set; }
        public List<Identifiers> IdentifierUpdates { get; set; }
        public List<ValueData> ValueData { get; set; }
        public List<StateData> StateData { get; set; }
        public ElementDetails ElementData { get; set; }
      
        public string UserMessage { get; set; }



    }
    public class Identifiers
    {
        public string Name { get; set; }
        public int? OldId { get; set; }
        public int? NewId { get; set; }
    }
    public class AddressData
    {
        public int? AddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }


    }
    public class UnitDatas
    {
        public string Component { get; set; }
        public string Name { get; set; }
        public int UnId { get; set; }
        public int? UnitNumber { get; set; }
        public string UnitStatus { get; set; }
        public int? ProfileUnId { get; set; }
    }
    public class ViewEditPolicyDetailss
    {
        public int CustomerId { get; set; }
        public string PcId { get; set; }
        public string PolicyNumber { get; set; }
        public string TransactionNumber { get; set; }
        public string TermNumber { get; set; }
        public string AccountManagerID { get; set; }
        public string PolicyStatus { get; set; }
        public string CoverPeriod { get; set; }
        public string CoverPeriodUnit { get; set; }
        public DateTime InceptionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int? PrId { get; set; }
        public int? IyId { get; set; }
        public string InsuredName { get; set; }
        public bool IsFloodCoverRequired { get; set; }
        public bool HasMadeAClaim { get; set; }
        public string PolicyInclusions { get; set; }
        public PolicyDetails PolicyData { get; set; }
        public List<InsuredDetails> InsuredDetails { get; set; }
        //public List<RiskDetails> RiskData { get; set; }
        //public List<PremiumDetails> PremiumData { get; set; }
        public List<UnitDatas> UnitData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
        public List<string> ReferralList { get; set; }
        public string Reason { get; set; }
    }
}