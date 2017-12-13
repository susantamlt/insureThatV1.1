using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class PetsCover
    {
        public int CustomerId { get; set; }
        public WishInsures WishinsureObj { get; set; }
        public Petnames PetnameObj { get; set; }
        public DataOfBirths DataofbirthObj { get; set; }
        public PreExistingIllness PreexistingillnessObj { get; set; }
        public DescribeTheIllness DescribeillnessObj { get; set; }
        public ExcessesPC ExcessObj { get; set; }
        public ImposedPC ImposedObj { get; set; }

    }
    public class ImposedPC
    {
        public bool Imposed { get; set; }
        public string EiId { get; set; }
    }
    public class ExcessesPC
    {
        public bool Excess { get; set; }
        public string EiId { get; set; }
    }
    public class DescribeTheIllness
    {        
        public string Describeillness { get; set; }
        public string EiId { get; set; }
    }
    public class PreExistingIllness
    {
        public bool Preexistingillness { get; set; }
        public string EiId { get; set; }
    }
    public class DataOfBirths
    {
        public string Dataofbirth { get; set; }
        public string EiId { get; set; }
    }
    public class Petnames
    {
        public string Petname { get; set; }
        public string EiId { get; set; }
    }
    public class WishInsures
    {
        public List<string> Wishinsure { get; set; }
        public string EiId { get; set; }
    }
}