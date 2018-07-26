using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class Claims
    {
    }
    public class ClaimsDetails
    {
        public int? PcId { get; set; }
        public int CustomerId { get; set; }
        public bool SelectedInclusion { get; set; }
        public string PolicyStatus { get; set; }
        public List<usp_GetUnit_Result> PolicyInclusion { get; set; }
        public List<usp_GetUnit_Result> ExistingPolicyInclustions { get; set; }
        public List<string> PolicyInclusions { get; set; }
        public string ApiKey { get; set; }
        public string PolicyId { get; set; }
        public List<RowsourceDatas> ClaimTypeRowsourceOptions { get; set; }
        public ClaimType ClaimtypeObj { get; set; }
        public List<ValueDatas> ClaimtypeobjList { get; set; }
        public DetailsOfClaim DetailsclaimObj { get; set; }
        public List<ValueDatas> DetailsclaimobjList { get; set; }
        public ClaimValue ClaimvalueObj { get; set; }
        public List<ValueDatas> ClaimvalueobjList { get; set; }
       
        public ClaimYear YearObj { get; set; }
        public List<ValueDatas> yearobjList { get; set; }
        public ClaimInsurer InsurerObj { get; set; }
        public List<ValueDatas> InsurerobjList { get; set; }
        public ClaimDriver DriverObj { get; set; }
        public List<ValueDatas> DriverobjList { get; set; }
        public int? PrId { get; set; }
        public int? TrId { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
   

        public List<ValueData> ValueData { get; set; }
    }
    public class ClaimType
    {
        public IEnumerable<SelectListItem> ClaimTypeList { get; set; }
        public string Type { get; set; }
        public int EiId { get; set; }
    }
    public class DetailsOfClaim
    {
        public string Details { get; set; }
        public int EiId { get; set; }
    }
    public class ClaimValue
    {
        public string Cvalue { get; set; }
        public int EiId { get; set; }
    }
    public class ClaimYear
    {
        public string Year { get; set; }
        public int EiId { get; set; }
    }
    public class ClaimInsurer
    {
        public string Insurer { get; set; }
        public int EiId { get; set; }
    }
    public class ClaimDriver
    {
        public string Driver { get; set; }
        public int EiId { get; set; }
    }
}