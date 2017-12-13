using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class HomeContent
    {
        public int CustomerId { get; set; }
        public LocationNew LocationObj { get; set; }
        public CostToReplaces CosttoreplaceObj { get; set; }
        public RequireCovers RequirecoverObj  { get; set; }
        public YearClaims YearclaimObj { get; set; }
        public ExcessesPay ExcesspayObj { get; set; }
        public Imposednew ImposedObj { get; set; }
        public Descriptions DescriptionObj { get; set; }
        public SumInsures SuminsuredObj { get; set; }
        public TotalCovers TotalcoverObj { get; set; }
        public IEnumerable<SelectListItem> SubUrb { get; set; }
    }
    public class ValuablesHC
    {
        public int CustomerId { get; set; }
        public LocationNew LocationObj { get; set; }
        public ExcessesPay ExcesspayObj { get; set; }
        public SumInsures SuminsuredObj { get; set; }
        public Descriptions DescriptionObj { get; set; }
        public TotalCovers TotalcoverObj { get; set; }
        public Unspecifics UnspecificObj { get; set; }
        public IEnumerable<SelectListItem> SubUrb { get; set; }
    }
    public class Unspecifics
    {
        public string Unspecific { get; set; }
        public int EiId { get; set; }
    }
    public class TotalCovers
    {
        public string Totalcover { get; set; }
        public int EiId { get; set; }
    }
    public class SumInsures
    {
        public string Suminsured { get; set; }
        public int EiId { get; set; }
    }
    public class Descriptions
    {
        public string Description { get; set; }
        public int EiId { get; set; }
    }
    public class Imposednew
    {
        public bool Imposed { get; set; }
        public int EiId { get; set; }
    }
    public class ExcessesPay
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }
    public class YearClaims
    {
        public string Yearclaim { get; set; }
        public int EiId { get; set; }
    }
    public class RequireCovers
    {
        public List<string> Requirecover { get; set; }
        public int EiId { get; set; }
    }
    public class CostToReplaces
    {
        public string Costtoreplaces { get; set; }
        public int EiId { get; set; }
    }
    public class LocationNew
    {
        public string Location { get; set; }
        public int EiId { get; set; }
    }
}