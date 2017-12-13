using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{

    public class PolicyTypes
    {
        public int PolicyType { get; set; }
    }
    public class PolicyInclustions
    {
        public int? PolicyType { get; set; }
        public string PolicyInclusions { get; set; }
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
        public int TrId { get; set; }
        public int? IyId { get; set; }
        public string InsuredName { get; set; }
        public string IsFloodCoverRequired { get; set; }
        public string HasMadeAClaim { get; set; }
        public string PolicyInclusions { get; set; }
        //public PolicyDetails PolicyData { get; set; }
        public List<InsuredDetails> InsuredDetails { get; set; }
        //public List<RiskDetails> RiskData { get; set; }
        //public List<PremiumDetails> PremiumData { get; set; }
        public List<UnitDatas> UnitData { get; set; }
        public string Status { get; set; }
        //public List<string> ErrorMessage { get; set; }
        //public List<string> ReferralList { get; set; }
        public string Reason { get; set; }
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
    public class PolicyList
    {
        public List<PolicyDetails> PolicyListDetails { get; set; }
    }
    public class ViewEditPolicyDetails
    {
        public int CustomerId { get; set; }
        public string PcId { get; set; }
        public string PolicyInclusions { get; set; }
        public PolicyDetails PolicyData { get; set; }
        public List<InsuredDetails> InsuredDetails { get; set; }
        public List<RiskDetails> RiskData { get; set; }
        public List<PremiumDetails> PremiumData { get; set; }
        public List<UnitDatas> UnitData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
        public string ReferralList { get; set; }
        public List<string> ReferralFullList { get; set; }
        public string Reason { get; set; }
    }
    public class UnitDatas
    {
        public string Component { get; set; }
        public string Name{ get; set; }
        public int UnId { get; set; }
        public string UnitNumber { get; set; }
        public string UnitStatus { get; set; }
        public string ProfileUnId { get; set; }
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