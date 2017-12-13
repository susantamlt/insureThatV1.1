using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class RiskDetails
    {
        public string Name { get; set; }
        public int UnId { get; set; }
        public int UnitNumber { get; set; }
        public int AdId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public List<RiskElementDetails> Elements { get; set; }
        public List<RiskListItemDetails> ListItems { get; set; }
        public List<Referral> Referrals { get; set; }


        //public void Populate(string name, int unId, int unitNumber, int adId, string addressLine1, string addressLine2, string suburb, string state, string postcode, List<RiskElementDetails> elements, List<RiskListItemDetails> listItems, List<Referral> referrals)
        //{
        //    this.Name = name;
        //    this.UnId = unId;
        //    this.UnitNumber = unitNumber;
        //    this.AdId = adId;
        //    this.AddressLine1 = addressLine1;
        //    this.AddressLine2 = addressLine2;
        //    this.Suburb = suburb;
        //    this.State = state;
        //    this.Postcode = postcode;
        //    this.Elements = elements;
        //    this.ListItems = listItems;
        //    this.Referrals = referrals;
        //}

        //public RiskDetails() { }
    }


    public class RiskElementDetails
    {
        public int ElId { get; set; }
        public string Value { get; set; }
        public int DataType { get; set; }

        //public RiskElementDetails(int elId, string value)
        //{
        //    this.ElId = elId;
        //    this.Value = value;
        //}

        //public RiskElementDetails(int elId, string value, int dataType)
        //{
        //    this.ElId = elId;
        //    this.Value = value;
        //    this.DataType = dataType;
        //}
    }


    public class RiskListItemDetails
    {
        public int ElId { get; set; }
        public int ItId { get; set; }
        public string Value { get; set; }
        public int DataType { get; set; }

        //public RiskListItemDetails(int elId, int itId, string value)
        //{
        //    th        is.ItId = itId;
        //    this.ElId = elId;
        //    this.Value = value;
        //}

        //public RiskListItemDetails(int elId, int itId, string value, int dataType)
        //{
        //    this.ItId = itId;
        //    this.ElId = elId;
        //    this.Value = value;
        //    this.DataType = dataType;
        //}
    }


    public class PremiumDetails
    {
        public string SectionName { get; set; }
        public decimal Base { get; set; }
        public decimal Fsl { get; set; }
        public decimal Gst { get; set; }
        public decimal StampDuty { get; set; }
        public decimal Gross
        {
            get
            {
                return this.Base + this.Fsl + this.Gst + this.StampDuty;
            }
        }


        //public PremiumDetails(string sectionName, decimal basePremium, decimal fsl, decimal gst, decimal stampDuty)
        //{
        //    this.SectionName = sectionName;
        //    this.Base = basePremium;
        //    this.Fsl = fsl;
        //    this.Gst = gst;
        //    this.StampDuty = stampDuty;
        //}
    }


    public class Referral
    {
        public int RfId { get; set; }
        public int UcId { get; set; }
        public string EnteredValue { get; set; }
        public string LimitingValue { get; set; }
        public int DataType { get; set; }
        public string DisplayMessage { get; set; }
        public string Status { get; set; }          //Pending, Approved, Rejected


        //public Referral(int rfId, int ucId, string enteredValue, string limitingValue, int dataType, string displayMessage, string status)
        //{
        //    this.RfId = rfId;
        //    this.UcId = ucId;
        //    this.EnteredValue = enteredValue;
        //    this.LimitingValue = limitingValue;
        //    this.DataType = dataType;
        //    this.DisplayMessage = displayMessage;
        //    this.Status = status;
        //}
    }








    public class RiskDetailsResponse
    {
        public RiskDetails RiskData { get; set; } 
        public PremiumDetails Premium { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
    }

    public class GetRiskDetailsResponse
    {
        public RiskDetails RiskData { get; set; }
        public PremiumDetails Premium { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }

        //public GetRiskDetailsResponse Populate(string status, List<string> errorMessage)
        //{
        //    GetRiskDetailsResponse getPolicyDetailsResponse = new GetRiskDetailsResponse();
        //    this.Status = status;
        //    this.ErrorMessage = errorMessage;
        //    return getPolicyDetailsResponse;
        //}
    }
}