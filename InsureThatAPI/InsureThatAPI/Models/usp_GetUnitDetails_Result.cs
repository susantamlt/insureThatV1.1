//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InsureThatAPI.Models
{
    using System;
    
    public partial class usp_GetUnitDetails_Result
    {
        public int UnitDetailsId { get; set; }
        public int UnitId { get; set; }
        public string Component_Type { get; set; }
        public string Unit_Type { get; set; }
        public Nullable<int> UnId { get; set; }
        public Nullable<int> ElementId { get; set; }
        public string RowSourceData { get; set; }
        public string ValueData { get; set; }
        public string StateData { get; set; }
        public string ReferralList { get; set; }
        public string Status { get; set; }
        public Nullable<int> AddressId { get; set; }
    }
}
