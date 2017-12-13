using InsureThatAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.CommonMethods
{
    public class NewPolicyDetailsClass
    {
        #region GET METHOD FOR NEW POLICY DETAILS

        public GetNewPolicyDetailsRef GetPolicyDetails(int ID)
        {
            GetNewPolicyDetailsRef policyDetailsRef = new GetNewPolicyDetailsRef();
            try
            {
                PolicyDetails policyDetailsModel = new PolicyDetails();
                MasterDataEntities db = new MasterDataEntities();
                policyDetailsRef.ErrorMessage = new List<string>();

                var item = db.IT_CC_GET_NewPolicyDetails(ID).FirstOrDefault();
                if (item != null)
                {

                    policyDetailsModel = new PolicyDetails();
                    policyDetailsModel.PcId = item.PcId.ToString();
                    policyDetailsModel.TrId = item.TrId;
                    policyDetailsModel.PolicyNumber = item.ProductID.ToString();
                   // policyDetailsModel.Timestamp = item.TimeStamp;
                    //policyDetailsModel.d = item.RemoveStampDuty;
                    policyDetailsModel.PolicyStatus = item.PolicyStatus.ToString();
                    policyDetailsModel.PolicyNumber = item.PolicyNumber;
                    policyDetailsModel.InceptionDate = item.InceptionDate;
                    policyDetailsModel.IsFloodCoverRequired = item.FloodCover.ToString();
                    policyDetailsModel.ExpiryDate = item.ExpiryDate;
                    policyDetailsModel.EffectiveDate = item.EffectiveDate;
                    policyDetailsModel.AccountManagerID = item.createdByUserID;
                    policyDetailsModel.CoverPeriodUnit = item.CoverPeriodUnit;
                    policyDetailsModel.CoverPeriod = item.CoverPeriod.ToString();
                    policyDetailsModel.HasMadeAClaim = item.Is_claimed;
                    policyDetailsModel.AccountManagerID = item.AccountManagerID.ToString();
                    policyDetailsModel.Reason = item.Reason_for_cancelletion;
                    policyDetailsModel.AccountManagerID = item.Broker;
               

                    policyDetailsRef.PolicyData.Add(policyDetailsModel);
                    policyDetailsRef.Status = "Success";

                }
                else
                {
                    policyDetailsRef.ErrorMessage.Add("No Data Available");
                    policyDetailsRef.Status = "Failure";
                }
            }
            catch (Exception xp)
            {
                policyDetailsRef.ErrorMessage.Add(xp.Message);
                policyDetailsRef.Status = "Failure";
            }
            finally
            {

            }
            return policyDetailsRef;
        }

        #endregion
        #region UPDATE METHOD FOR NEW POLICY DETAILS
        public int UpdatePolicyDetails(int ID, PolicyDetails policyDetailsModel)
        {
            int icount = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                icount = db.IT_CC_UPDATE_NewPolicyDetails(ID, policyDetailsModel.EffectiveDate, policyDetailsModel.Reason, Convert.ToInt32(policyDetailsModel.IsFloodCoverRequired));
            }
            catch (Exception xp)
            {

            }
            finally
            {

            }
            return icount;
        }

        #endregion
        #region INSERT METHOD FOR NEW POLICY DETAILS

        public int InsertPolicyDetails(PolicyDetails policyDetailsModel)
        {
            int? icount = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                icount = db.IT_CC_InsertNewPolicyDetails(Convert.ToInt32(policyDetailsModel.PcId), Convert.ToInt32(policyDetailsModel.TrId), policyDetailsModel.PolicyNumber, policyDetailsModel.AccountManagerID, Convert.ToInt32(policyDetailsModel.AccountManagerID), Convert.ToInt32(policyDetailsModel.PolicyStatus), Convert.ToInt32(policyDetailsModel.CoverPeriod), policyDetailsModel.CoverPeriodUnit, policyDetailsModel.InceptionDate, policyDetailsModel.ExpiryDate, policyDetailsModel.EffectiveDate, Convert.ToInt32(policyDetailsModel.PrId), Convert.ToInt32(policyDetailsModel.IsFloodCoverRequired), policyDetailsModel.HasMadeAClaim, null, policyDetailsModel.Reason, policyDetailsModel.AccountManagerID).FirstOrDefault();

            }
            catch (Exception xp)
            {

            }
            finally
            {

            }
            return icount.Value;
        }

        #endregion
        #region breed for DogList
        public List<SelectListItem> breedListDog()
        {
            List<SelectListItem> dogList = new List<SelectListItem>();
            dogList.Add(new SelectListItem { Value = "", Text = "Select dog breed" });
            dogList.Add(new SelectListItem { Value = "1", Text = "Affenpinscher" });
            dogList.Add(new SelectListItem { Value = "2", Text = "Afghan Hound" });
            dogList.Add(new SelectListItem { Value = "3", Text = "Airedale Terrier" });
            dogList.Add(new SelectListItem { Value = "4", Text = "Akita " });
            dogList.Add(new SelectListItem { Value = "5", Text = "Alaskan Malamute" });
            dogList.Add(new SelectListItem { Value = "401", Text = "American Pitt Bull Terrier" });
            dogList.Add(new SelectListItem { Value = "6", Text = "American Staffordshire Terrier" });
            dogList.Add(new SelectListItem { Value = "9", Text = "Anatolian Shepherd Dog" });
            dogList.Add(new SelectListItem { Value = "402", Text = "Argentinean Mastiff" });
            dogList.Add(new SelectListItem { Value = "10", Text = "Australian Cattle Dog" });
            dogList.Add(new SelectListItem { Value = "7", Text = "Australian Kelpie" });
            dogList.Add(new SelectListItem { Value = "11", Text = "Australian Shepherd" });
            dogList.Add(new SelectListItem { Value = "8", Text = "Australian Silky Terrier" });
            dogList.Add(new SelectListItem { Value = "12", Text = "Australian Terrier" });
            dogList.Add(new SelectListItem { Value = "13", Text = "Basenji" });
            dogList.Add(new SelectListItem { Value = "15", Text = "Basset Fauvre de Bretagne" });
            dogList.Add(new SelectListItem { Value = "14", Text = "Basset Hound" });
            dogList.Add(new SelectListItem { Value = "16", Text = "Beagle" });
            dogList.Add(new SelectListItem { Value = "17", Text = "Bearded Collie" });
            dogList.Add(new SelectListItem { Value = "18", Text = "Bedlington Terrier" });
            dogList.Add(new SelectListItem { Value = "19", Text = "Belgian Malinois" });
            dogList.Add(new SelectListItem { Value = "20", Text = "Belgian Sheepdog" });
            dogList.Add(new SelectListItem { Value = "21", Text = "Belgian Tervuren" });
            dogList.Add(new SelectListItem { Value = "22", Text = "Bernese Mountain Dog" });
            dogList.Add(new SelectListItem { Value = "23", Text = "Bichon Frisé" });
            dogList.Add(new SelectListItem { Value = "24", Text = "Bloodhound" });
            dogList.Add(new SelectListItem { Value = "25", Text = "Border Collie" });
            dogList.Add(new SelectListItem { Value = "26", Text = "Border Terrier" });
            dogList.Add(new SelectListItem { Value = "27", Text = "Borzoi" });
            dogList.Add(new SelectListItem { Value = "28", Text = "Boston Terrier" });
            dogList.Add(new SelectListItem { Value = "29", Text = "Bouvier des Flandres" });
            dogList.Add(new SelectListItem { Value = "30", Text = "Boxer" });
            dogList.Add(new SelectListItem { Value = "31", Text = "Bracco Italiano" });
            dogList.Add(new SelectListItem { Value = "32", Text = "Briard" });
            dogList.Add(new SelectListItem { Value = "33", Text = "Brittany" });
            dogList.Add(new SelectListItem { Value = "34", Text = "Bull Terrier" });
            dogList.Add(new SelectListItem { Value = "35", Text = "Bull Terrier (Miniature)" });
            dogList.Add(new SelectListItem { Value = "36", Text = "Bulldog" });
            dogList.Add(new SelectListItem { Value = "37", Text = "Bullmastiff" });
            dogList.Add(new SelectListItem { Value = "51", Text = "Cairn Terrier" });
            dogList.Add(new SelectListItem { Value = "38", Text = "Cardigan Welsh Corgi" });
            dogList.Add(new SelectListItem { Value = "39", Text = "Cavalier King Charles Spaniel" });
            dogList.Add(new SelectListItem { Value = "40", Text = "Cavoodle" });
            dogList.Add(new SelectListItem { Value = "41", Text = "Cesky Terrier" });
            dogList.Add(new SelectListItem { Value = "42", Text = "Chesapeake Bay Retriever" });
            dogList.Add(new SelectListItem { Value = "43", Text = "Chihuahua" });
            dogList.Add(new SelectListItem { Value = "44", Text = "Chinese Crested Dog" });
            dogList.Add(new SelectListItem { Value = "45", Text = "Chinese Shar Pei" });
            dogList.Add(new SelectListItem { Value = "46", Text = "Chow Chow" });
            dogList.Add(new SelectListItem { Value = "47", Text = "Clumber Spaniel" });
            dogList.Add(new SelectListItem { Value = "48", Text = "Collie" });
            dogList.Add(new SelectListItem { Value = "49", Text = "Corgi" });
            dogList.Add(new SelectListItem { Value = "50", Text = "Curly-Coated Retriever" });
            dogList.Add(new SelectListItem { Value = "53", Text = "Dachshund- miniature" });
            dogList.Add(new SelectListItem { Value = "52", Text = "Dachshund- standard" });
            dogList.Add(new SelectListItem { Value = "54", Text = "Dalmatian" });
            dogList.Add(new SelectListItem { Value = "56", Text = "Dandie Dinmont Terrier" });
            dogList.Add(new SelectListItem { Value = "55", Text = "Deerhound" });
            dogList.Add(new SelectListItem { Value = "403", Text = "Dingo" });
            dogList.Add(new SelectListItem { Value = "57", Text = "Doberman Pinscher" });
            dogList.Add(new SelectListItem { Value = "404", Text = "Dogo Argentino" });
            dogList.Add(new SelectListItem { Value = "58", Text = "Dogue de Bordeaux" });
            dogList.Add(new SelectListItem { Value = "59", Text = "English Cocker Spaniel" });
            dogList.Add(new SelectListItem { Value = "60", Text = "English Foxhound" });
            dogList.Add(new SelectListItem { Value = "61", Text = "English Setter" });
            dogList.Add(new SelectListItem { Value = "62", Text = "English Springer Spaniel" });
            dogList.Add(new SelectListItem { Value = "63", Text = "English Toy Spaniel" });
            dogList.Add(new SelectListItem { Value = "64", Text = "Field Spaniel" });
            dogList.Add(new SelectListItem { Value = "405", Text = "Fila Brasileiro" });
            dogList.Add(new SelectListItem { Value = "65", Text = "Finnish Lapphund" });
            dogList.Add(new SelectListItem { Value = "66", Text = "Finnish Spitz" });
            dogList.Add(new SelectListItem { Value = "67", Text = "Flat-Coated Retriever" });
            dogList.Add(new SelectListItem { Value = "69", Text = "Fox Terrier" });
            dogList.Add(new SelectListItem { Value = "68", Text = "Foxhound" });
            dogList.Add(new SelectListItem { Value = "70", Text = "French Bulldog" });
            dogList.Add(new SelectListItem { Value = "71", Text = "German Shepherd Dog" });
            dogList.Add(new SelectListItem { Value = "72", Text = "German Shorthaired Pointer" });
            dogList.Add(new SelectListItem { Value = "73", Text = "German Spitz" });
            dogList.Add(new SelectListItem { Value = "74", Text = "German Wirehaired Pointer" });
            dogList.Add(new SelectListItem { Value = "75", Text = "Golden Retriever" });
            dogList.Add(new SelectListItem { Value = "76", Text = "Gordon Setter" });
            dogList.Add(new SelectListItem { Value = "77", Text = "Great Dane" });
            dogList.Add(new SelectListItem { Value = "78", Text = "Great Pyrenees" });
            dogList.Add(new SelectListItem { Value = "79", Text = "Greyhound" });
            dogList.Add(new SelectListItem { Value = "80", Text = "Harrier" });
            dogList.Add(new SelectListItem { Value = "81", Text = "Havanese" });
            dogList.Add(new SelectListItem { Value = "82", Text = "Ibizan Hound" });
            dogList.Add(new SelectListItem { Value = "83", Text = "Irish Red and White Setter" });
            dogList.Add(new SelectListItem { Value = "84", Text = "Irish Setter" });
            dogList.Add(new SelectListItem { Value = "85", Text = "Irish Terrier" });
            dogList.Add(new SelectListItem { Value = "86", Text = "Irish Water Spaniel" });
            dogList.Add(new SelectListItem { Value = "87", Text = "Irish Wolfhound" });
            dogList.Add(new SelectListItem { Value = "88", Text = "Italian Greyhound" });
            dogList.Add(new SelectListItem { Value = "90", Text = "Japanese Akita" });
            dogList.Add(new SelectListItem { Value = "89", Text = "Japanese Chin" });
            dogList.Add(new SelectListItem { Value = "91", Text = "Japanese Spitz" });
            dogList.Add(new SelectListItem { Value = "406", Text = "Japanese Tosa" });
            dogList.Add(new SelectListItem { Value = "92", Text = "Jug" });
            dogList.Add(new SelectListItem { Value = "93", Text = "Keeshond" });
            dogList.Add(new SelectListItem { Value = "94", Text = "Kerry Blue Terrier" });
            dogList.Add(new SelectListItem { Value = "95", Text = "Komondor," });
            dogList.Add(new SelectListItem { Value = "97", Text = "Labradoodle" });
            dogList.Add(new SelectListItem { Value = "96", Text = "Labrador Retriever" });
            dogList.Add(new SelectListItem { Value = "98", Text = "Lakeland Terrier" });
            dogList.Add(new SelectListItem { Value = "99", Text = "Leonberger" });
            dogList.Add(new SelectListItem { Value = "100", Text = "Lhasa Apso" });
            dogList.Add(new SelectListItem { Value = "101", Text = "Löwchen" });
            dogList.Add(new SelectListItem { Value = "102", Text = "Maltese" });
            dogList.Add(new SelectListItem { Value = "103", Text = "Manchester Terrier" });
            dogList.Add(new SelectListItem { Value = "104", Text = "Maremma Sheepdog" });
            dogList.Add(new SelectListItem { Value = "105", Text = "Mastiff" });
            dogList.Add(new SelectListItem { Value = "106", Text = "Miniature Bull Terrier" });
            dogList.Add(new SelectListItem { Value = "107", Text = "Miniature Pinscher" });
            dogList.Add(new SelectListItem { Value = "108", Text = "Miniature Schnauzer" });
            dogList.Add(new SelectListItem { Value = "109", Text = "Moodle" });
            dogList.Add(new SelectListItem { Value = "110", Text = "Neapolitan Mastiff" });
            dogList.Add(new SelectListItem { Value = "111", Text = "Newfoundland" });
            dogList.Add(new SelectListItem { Value = "112", Text = "Norfolk Terrier" });
            dogList.Add(new SelectListItem { Value = "113", Text = "Norwich Terrier" });
            dogList.Add(new SelectListItem { Value = "114", Text = "Nova Scotia Duck-Tolling Retriever" });
            dogList.Add(new SelectListItem { Value = "115", Text = "Old English Sheepdog" });
            dogList.Add(new SelectListItem { Value = "301", Text = "Other" });
            dogList.Add(new SelectListItem { Value = "116", Text = "Papillon," });
            dogList.Add(new SelectListItem { Value = "117", Text = "Parson Russell Terrier" });
            dogList.Add(new SelectListItem { Value = "118", Text = "Pekingese" });
            dogList.Add(new SelectListItem { Value = "119", Text = "Pembroke Welsh Corgi" });
            dogList.Add(new SelectListItem { Value = "120", Text = "Pharaoh Hound" });
            dogList.Add(new SelectListItem { Value = "407", Text = "Pitt Bull Terrier" });
            dogList.Add(new SelectListItem { Value = "121", Text = "Pointer" });
            dogList.Add(new SelectListItem { Value = "122", Text = "Pomeranian" });
            dogList.Add(new SelectListItem { Value = "124", Text = "Poodle- miniature" });
            dogList.Add(new SelectListItem { Value = "123", Text = "Poodle- standard" });
            dogList.Add(new SelectListItem { Value = "125", Text = "Portuguese Water Dog" });
            dogList.Add(new SelectListItem { Value = "408", Text = "Presa Canario" });
            dogList.Add(new SelectListItem { Value = "126", Text = "Pug" });
            dogList.Add(new SelectListItem { Value = "127", Text = "Pugalier" });
            dogList.Add(new SelectListItem { Value = "128", Text = "Puli" });
            dogList.Add(new SelectListItem { Value = "129", Text = "Rhodesian Ridgeback" });
            dogList.Add(new SelectListItem { Value = "130", Text = "Rottweiler" });
            dogList.Add(new SelectListItem { Value = "131", Text = "Russell Terrier (Jack)" });
            dogList.Add(new SelectListItem { Value = "133", Text = "Saluki" });
            dogList.Add(new SelectListItem { Value = "134", Text = "Samoyed" });
            dogList.Add(new SelectListItem { Value = "136", Text = "Schipperke" });
            dogList.Add(new SelectListItem { Value = "137", Text = "Schnoodle" });
            dogList.Add(new SelectListItem { Value = "135", Text = "Scoodle" });
            dogList.Add(new SelectListItem { Value = "138", Text = "Scottish Terrier" });
            dogList.Add(new SelectListItem { Value = "139", Text = "Shar Pei " });
            dogList.Add(new SelectListItem { Value = "140", Text = "Shetland Sheepdog" });
            dogList.Add(new SelectListItem { Value = "141", Text = "Shiba Inu" });
            dogList.Add(new SelectListItem { Value = "142", Text = "Shih Tzu" });
            dogList.Add(new SelectListItem { Value = "143", Text = "Siberian Husky" });
            dogList.Add(new SelectListItem { Value = "144", Text = "Silky Terrier" });
            dogList.Add(new SelectListItem { Value = "145", Text = "Skye Terrier" });
            dogList.Add(new SelectListItem { Value = "146", Text = "Sloughi" });
            dogList.Add(new SelectListItem { Value = "147", Text = "Soft-Coated Wheaten Terrier" });
            dogList.Add(new SelectListItem { Value = "148", Text = "Spoodle" });
            dogList.Add(new SelectListItem { Value = "132", Text = "St. Bernard" });
            dogList.Add(new SelectListItem { Value = "149", Text = "Staffordshire Bull Terrier" });
            dogList.Add(new SelectListItem { Value = "150", Text = "Standard Schnauzer" });
            dogList.Add(new SelectListItem { Value = "151", Text = "Sussex Spaniel" });
            dogList.Add(new SelectListItem { Value = "152", Text = "Swedish Vallhund" });
            dogList.Add(new SelectListItem { Value = "153", Text = "Tenterfield Terrier" });
            dogList.Add(new SelectListItem { Value = "154", Text = "Tibetan Mastiff" });
            dogList.Add(new SelectListItem { Value = "155", Text = "Tibetan Spaniel" });
            dogList.Add(new SelectListItem { Value = "156", Text = "Tibetan Terrier" });
            dogList.Add(new SelectListItem { Value = "157", Text = "Toy Fox Terrier" });
            dogList.Add(new SelectListItem { Value = "158", Text = "Vizsla" });
            dogList.Add(new SelectListItem { Value = "159", Text = "Weimaraner" });
            dogList.Add(new SelectListItem { Value = "160", Text = "Welsh Corgi" });
            dogList.Add(new SelectListItem { Value = "161", Text = "Welsh Springer Spaniel" });
            dogList.Add(new SelectListItem { Value = "162", Text = "Welsh Terrier" });
            dogList.Add(new SelectListItem { Value = "163", Text = "West Highland White Terrier" });
            dogList.Add(new SelectListItem { Value = "164", Text = "Whippet" });
            dogList.Add(new SelectListItem { Value = "165", Text = "Yorkshire Terrier" });
            return dogList;
        }
        #endregion
        #region breed for CatList
        public List<SelectListItem> breedListCat()
        {
            List<SelectListItem> catList = new List<SelectListItem>();
            catList.Add(new SelectListItem { Value = "", Text = "Select cat breed" });
            catList.Add(new SelectListItem { Value = "1", Text = "Asian" });
            catList.Add(new SelectListItem { Value = "2", Text = "Asian Semi-longhair" });
            catList.Add(new SelectListItem { Value = "3", Text = "Australian Mist" });
            catList.Add(new SelectListItem { Value = "4", Text = "Balinese" });
            catList.Add(new SelectListItem { Value = "5", Text = "Bambino" });
            catList.Add(new SelectListItem { Value = "6", Text = "Bengal" });
            catList.Add(new SelectListItem { Value = "7", Text = "Birman" });
            catList.Add(new SelectListItem { Value = "8", Text = "Bombay" });
            catList.Add(new SelectListItem { Value = "9", Text = "Brazilian Shorthair" });
            catList.Add(new SelectListItem { Value = "10", Text = "British Longhair" });
            catList.Add(new SelectListItem { Value = "11", Text = "British Semi-longhair" });
            catList.Add(new SelectListItem { Value = "12", Text = "British Shorthair" });
            catList.Add(new SelectListItem { Value = "13", Text = "Burmese" });
            catList.Add(new SelectListItem { Value = "14", Text = "Burmilla" });
            catList.Add(new SelectListItem { Value = "15", Text = "California Spangled" });
            catList.Add(new SelectListItem { Value = "16", Text = "Chantilly-Tiffany" });
            catList.Add(new SelectListItem { Value = "17", Text = "Chartreux" });
            catList.Add(new SelectListItem { Value = "18", Text = "Chausie" });
            catList.Add(new SelectListItem { Value = "19", Text = "Cheetoh" });
            catList.Add(new SelectListItem { Value = "20", Text = "Colorpoint Shorthair" });
            catList.Add(new SelectListItem { Value = "21", Text = "Cornish Rex" });
            catList.Add(new SelectListItem { Value = "22", Text = "Cymric/Manx Longhair" });
            catList.Add(new SelectListItem { Value = "23", Text = "Cyprus" });
            catList.Add(new SelectListItem { Value = "24", Text = "Devon Rex" });
            catList.Add(new SelectListItem { Value = "25", Text = "Donskoy" });
            catList.Add(new SelectListItem { Value = "26", Text = "Dragon Li" });
            catList.Add(new SelectListItem { Value = "27", Text = "Dwarf cat/Dwelf" });
            catList.Add(new SelectListItem { Value = "28", Text = "Egyptian Mau" });
            catList.Add(new SelectListItem { Value = "29", Text = "European Shorthair" });
            catList.Add(new SelectListItem { Value = "30", Text = "Exotic Shorthair" });
            catList.Add(new SelectListItem { Value = "31", Text = "Foldex Cat" });
            catList.Add(new SelectListItem { Value = "32", Text = "German Rex" });
            catList.Add(new SelectListItem { Value = "33", Text = "Havana Brown" });
            catList.Add(new SelectListItem { Value = "34", Text = "Highlander" });
            catList.Add(new SelectListItem { Value = "35", Text = "Himalayan/Colorpoint Persian" });
            catList.Add(new SelectListItem { Value = "36", Text = "Japanese Bobtail" });
            catList.Add(new SelectListItem { Value = "37", Text = "Javanese" });
            catList.Add(new SelectListItem { Value = "38", Text = "Khao Manee" });
            catList.Add(new SelectListItem { Value = "39", Text = "Korat" });
            catList.Add(new SelectListItem { Value = "40", Text = "Korean Bobtail" });
            catList.Add(new SelectListItem { Value = "41", Text = "Korn Ja" });
            catList.Add(new SelectListItem { Value = "42", Text = "Kurilian Bobtail" });
            catList.Add(new SelectListItem { Value = "43", Text = "Kurilian Bobtail/Kuril Islands Bobtail" });
            catList.Add(new SelectListItem { Value = "44", Text = "LaPerm" });
            catList.Add(new SelectListItem { Value = "45", Text = "Lykoi" });
            catList.Add(new SelectListItem { Value = "46", Text = "Maine Coon" });
            catList.Add(new SelectListItem { Value = "47", Text = "Manx" });
            catList.Add(new SelectListItem { Value = "48", Text = "Mekong Bobtail" });
            catList.Add(new SelectListItem { Value = "49", Text = "Minskin" });
            catList.Add(new SelectListItem { Value = "50", Text = "Munchkin" });
            catList.Add(new SelectListItem { Value = "51", Text = "Napoleon" });
            catList.Add(new SelectListItem { Value = "52", Text = "Nebelung" });
            catList.Add(new SelectListItem { Value = "53", Text = "Norwegian Forest Cat" });
            catList.Add(new SelectListItem { Value = "54", Text = "Ocicat" });
            catList.Add(new SelectListItem { Value = "55", Text = "Ojos Azules" });
            catList.Add(new SelectListItem { Value = "56", Text = "or Don Sphynx" });
            catList.Add(new SelectListItem { Value = "57", Text = "Oregon Rex" });
            catList.Add(new SelectListItem { Value = "58", Text = "Oriental Bicolor" });
            catList.Add(new SelectListItem { Value = "59", Text = "Oriental Longhair" });
            catList.Add(new SelectListItem { Value = "60", Text = "Oriental Shorthair" });
            catList.Add(new SelectListItem { Value = "301", Text = "Other" });
            catList.Add(new SelectListItem { Value = "61", Text = "PerFold Cat" });
            catList.Add(new SelectListItem { Value = "62", Text = "Persian (Modern Persian Cat)" });
            catList.Add(new SelectListItem { Value = "63", Text = "Persian (Traditional Persian Cat)" });
            catList.Add(new SelectListItem { Value = "64", Text = "Peterbald" });
            catList.Add(new SelectListItem { Value = "65", Text = "Pixie-bob" });
            catList.Add(new SelectListItem { Value = "66", Text = "Raas" });
            catList.Add(new SelectListItem { Value = "67", Text = "Ragamuffin" });
            catList.Add(new SelectListItem { Value = "68", Text = "Ragdoll" });
            catList.Add(new SelectListItem { Value = "69", Text = "Russian Blue" });
            catList.Add(new SelectListItem { Value = "70", Text = "Russian White, Black and Tabby" });
            catList.Add(new SelectListItem { Value = "71", Text = "Sam Sawet" });
            catList.Add(new SelectListItem { Value = "72", Text = "Savannah" });
            catList.Add(new SelectListItem { Value = "73", Text = "Scottish Fold" });
            catList.Add(new SelectListItem { Value = "74", Text = "Selkirk Rex" });
            catList.Add(new SelectListItem { Value = "75", Text = "Serengeti" });
            catList.Add(new SelectListItem { Value = "76", Text = "Serrade petit" });
            catList.Add(new SelectListItem { Value = "77", Text = "Siamese" });
            catList.Add(new SelectListItem { Value = "78", Text = "Siberian" });
            catList.Add(new SelectListItem { Value = "79", Text = "Singapura" });
            catList.Add(new SelectListItem { Value = "80", Text = "Snowshoe" });
            catList.Add(new SelectListItem { Value = "81", Text = "Sokoke" });
            catList.Add(new SelectListItem { Value = "82", Text = "Somali" });
            catList.Add(new SelectListItem { Value = "83", Text = "Sphynx" });
            catList.Add(new SelectListItem { Value = "84", Text = "Suphalak" });
            catList.Add(new SelectListItem { Value = "85", Text = "Thai" });
            catList.Add(new SelectListItem { Value = "86", Text = "Tonkinese" });
            catList.Add(new SelectListItem { Value = "87", Text = "Toyger" });
            catList.Add(new SelectListItem { Value = "88", Text = "Turkish Angora" });
            catList.Add(new SelectListItem { Value = "89", Text = "Turkish Van" });
            catList.Add(new SelectListItem { Value = "90", Text = "Ukrainian Levkoy" });
            return catList;
        }
        #endregion
        #region Description for farm content
        public List<SelectListItem> descriptionListFC()
        {
            List<SelectListItem> descriptionList = new List<SelectListItem>();
            descriptionList.Add(new SelectListItem { Value = "2", Text = "Dairy" });
            descriptionList.Add(new SelectListItem { Value = "3", Text = "Garage" });
            descriptionList.Add(new SelectListItem { Value = "4", Text = "General purpose shed" });
            descriptionList.Add(new SelectListItem { Value = "5", Text = "Hay shed" });
            descriptionList.Add(new SelectListItem { Value = "6", Text = "Horse shelters" });
            descriptionList.Add(new SelectListItem { Value = "7", Text = "Machinery shed" });
            descriptionList.Add(new SelectListItem { Value = "1", Text = "Pump shed" });
            descriptionList.Add(new SelectListItem { Value = "8", Text = "Shearing shed" });
            descriptionList.Add(new SelectListItem { Value = "9", Text = "Stables" });
            descriptionList.Add(new SelectListItem { Value = "10", Text = "Tack Room" });
            descriptionList.Add(new SelectListItem { Value = "11", Text = "Workshop" });
            descriptionList.Add(new SelectListItem { Value = "12", Text = "Other" });
            return descriptionList;
        }
        #endregion
        #region Description for farm Construction Materials
        public List<SelectListItem> constructionType()
        {
            List<SelectListItem> constructionList = new List<SelectListItem>();
            constructionList.Add(new SelectListItem { Value = "1", Text = "Metal walls on Metal frame" });
            constructionList.Add(new SelectListItem { Value = "2", Text = "Metal walls on Timber frrame" });
            constructionList.Add(new SelectListItem { Value = "3", Text = "Timber walls on Timber frame" });
            constructionList.Add(new SelectListItem { Value = "4", Text = "Timber walls on Iron frame" });
            constructionList.Add(new SelectListItem { Value = "5", Text = "Sandwich panel – EPS" });
            constructionList.Add(new SelectListItem { Value = "6", Text = "Open sides – metal frame" });
            constructionList.Add(new SelectListItem { Value = "7", Text = "Open sides – timber frame" });
            constructionList.Add(new SelectListItem { Value = "8", Text = "Brick" });
            return constructionList;
        }
        #endregion
        #region excess for you wish to pay
        public List<SelectListItem> excessRate()
        {
            List<SelectListItem> excessList = new List<SelectListItem>();
            excessList.Add(new SelectListItem { Value = "250", Text = "$250" });
            excessList.Add(new SelectListItem { Value = "350", Text = "$350" });
            excessList.Add(new SelectListItem { Value = "450", Text = "$450" });
            excessList.Add(new SelectListItem { Value = "550", Text = "$550" });
            return excessList;
        }
        #endregion
        #region Description for live stock
        public List<SelectListItem> descriptionLS()
        {
            List<SelectListItem> descriptionList = new List<SelectListItem>();
            descriptionList.Add(new SelectListItem { Value = "1", Text = "Alpacas" });
            descriptionList.Add(new SelectListItem { Value = "2", Text = "Bulls" });
            descriptionList.Add(new SelectListItem { Value = "3", Text = "Calves" });
            descriptionList.Add(new SelectListItem { Value = "4", Text = "Cattle" });
            descriptionList.Add(new SelectListItem { Value = "5", Text = "Deer" });
            descriptionList.Add(new SelectListItem { Value = "6", Text = "Ewes" });
            descriptionList.Add(new SelectListItem { Value = "7", Text = "Goats" });
            descriptionList.Add(new SelectListItem { Value = "8", Text = "Heifers" });
            descriptionList.Add(new SelectListItem { Value = "9", Text = "Hogs" });
            descriptionList.Add(new SelectListItem { Value = "10", Text = "Horses" });
            descriptionList.Add(new SelectListItem { Value = "11", Text = "Lambs" });
            descriptionList.Add(new SelectListItem { Value = "12", Text = "Ostriches" });
            descriptionList.Add(new SelectListItem { Value = "13", Text = "Pigs" });
            descriptionList.Add(new SelectListItem { Value = "14", Text = "Poultry" });
            descriptionList.Add(new SelectListItem { Value = "15", Text = "Rams" });
            descriptionList.Add(new SelectListItem { Value = "16", Text = "Sheep" });
            descriptionList.Add(new SelectListItem { Value = "17", Text = "Sows" });
            descriptionList.Add(new SelectListItem { Value = "18", Text = "Steers" });
            descriptionList.Add(new SelectListItem { Value = "19", Text = "Vealers" });
            descriptionList.Add(new SelectListItem { Value = "20", Text = "Wethers" });
            return descriptionList;
        }
        #endregion
        #region Type Of Boat List for Boat
        public List<SelectListItem> TypeOfBoatList()
        {
            List<SelectListItem> TypeBoatList = new List<SelectListItem>();
            TypeBoatList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            TypeBoatList.Add(new SelectListItem { Value = "1", Text = "Canoe" });
            TypeBoatList.Add(new SelectListItem { Value = "2", Text = "Catamaran" });
            TypeBoatList.Add(new SelectListItem { Value = "3", Text = "Cruiser" });
            TypeBoatList.Add(new SelectListItem { Value = "4", Text = "Dinghy" });
            TypeBoatList.Add(new SelectListItem { Value = "5", Text = "Half cabin cruiser" });
            TypeBoatList.Add(new SelectListItem { Value = "6", Text = "Houseboat" });
            TypeBoatList.Add(new SelectListItem { Value = "7", Text = "Punt" });
            TypeBoatList.Add(new SelectListItem { Value = "8", Text = "Runabout" });
            TypeBoatList.Add(new SelectListItem { Value = "9", Text = "Ski boat" });
            TypeBoatList.Add(new SelectListItem { Value = "10", Text = "Speed boat" });
            TypeBoatList.Add(new SelectListItem { Value = "11", Text = "Trimaran" });
            TypeBoatList.Add(new SelectListItem { Value = "12", Text = "Windsurfer" });
            TypeBoatList.Add(new SelectListItem { Value = "13", Text = "Yacht" });
            return TypeBoatList;
        }
        #endregion
        #region Type Of Boat List for Boat
        public List<SelectListItem> HullMeterialList()
        {
            List<SelectListItem> HullMeterialsList = new List<SelectListItem>();
            HullMeterialsList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            HullMeterialsList.Add(new SelectListItem { Value = "1", Text = "Aluminium" });
            HullMeterialsList.Add(new SelectListItem { Value = "2", Text = "Concrete" });
            HullMeterialsList.Add(new SelectListItem { Value = "3", Text = "Fibreglass" });
            HullMeterialsList.Add(new SelectListItem { Value = "4", Text = "Kevlar" });
            HullMeterialsList.Add(new SelectListItem { Value = "5", Text = "Plastic" });
            HullMeterialsList.Add(new SelectListItem { Value = "6", Text = "Rubber" });
            HullMeterialsList.Add(new SelectListItem { Value = "7", Text = "Steel" });
            HullMeterialsList.Add(new SelectListItem { Value = "8", Text = "Timber" });
            return HullMeterialsList;
        }
        #endregion
        #region Fuel Type List for Boat
        public List<SelectListItem> FuelType()
        {
            List<SelectListItem> FuelTypeList = new List<SelectListItem>();
            FuelTypeList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            FuelTypeList.Add(new SelectListItem { Value = "1", Text = "Canoe" });
            FuelTypeList.Add(new SelectListItem { Value = "2", Text = "Catamaran" });
            FuelTypeList.Add(new SelectListItem { Value = "3", Text = "Cruiser" });
            FuelTypeList.Add(new SelectListItem { Value = "4", Text = "Dinghy" });
            FuelTypeList.Add(new SelectListItem { Value = "5", Text = "Half cabin cruiser" });
            FuelTypeList.Add(new SelectListItem { Value = "6", Text = "Houseboat" });
            FuelTypeList.Add(new SelectListItem { Value = "7", Text = "Punt" });
            FuelTypeList.Add(new SelectListItem { Value = "8", Text = "Runabout" });
            FuelTypeList.Add(new SelectListItem { Value = "9", Text = "Ski boat" });
            FuelTypeList.Add(new SelectListItem { Value = "10", Text = "Speed boat" });
            FuelTypeList.Add(new SelectListItem { Value = "11", Text = "Trimaran" });
            FuelTypeList.Add(new SelectListItem { Value = "12", Text = "Windsurfer" });
            FuelTypeList.Add(new SelectListItem { Value = "13", Text = "Yacht" });
            return FuelTypeList;
        }
        #endregion
        #region Motor Position List for Boat
        public List<SelectListItem> MotorPosition()
        {
            List<SelectListItem> MotorPositionList = new List<SelectListItem>();
            MotorPositionList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            MotorPositionList.Add(new SelectListItem { Value = "1", Text = "Aluminium" });
            MotorPositionList.Add(new SelectListItem { Value = "2", Text = "Concrete" });
            MotorPositionList.Add(new SelectListItem { Value = "3", Text = "Fibreglass" });
            MotorPositionList.Add(new SelectListItem { Value = "4", Text = "Kevlar" });
            MotorPositionList.Add(new SelectListItem { Value = "5", Text = "Plastic" });
            MotorPositionList.Add(new SelectListItem { Value = "6", Text = "Rubber" });
            MotorPositionList.Add(new SelectListItem { Value = "7", Text = "Steel" });
            MotorPositionList.Add(new SelectListItem { Value = "8", Text = "Timber" });
            return MotorPositionList;
        }
        #endregion
        #region Drive Type List for Boat
        public List<SelectListItem> DriveType()
        {
            List<SelectListItem> DriveTypeList = new List<SelectListItem>();
            DriveTypeList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            DriveTypeList.Add(new SelectListItem { Value = "1", Text = "Aluminium" });
            DriveTypeList.Add(new SelectListItem { Value = "2", Text = "Concrete" });
            DriveTypeList.Add(new SelectListItem { Value = "3", Text = "Fibreglass" });
            DriveTypeList.Add(new SelectListItem { Value = "4", Text = "Kevlar" });
            DriveTypeList.Add(new SelectListItem { Value = "5", Text = "Plastic" });
            DriveTypeList.Add(new SelectListItem { Value = "6", Text = "Rubber" });
            DriveTypeList.Add(new SelectListItem { Value = "7", Text = "Steel" });
            DriveTypeList.Add(new SelectListItem { Value = "8", Text = "Timber" });
            return DriveTypeList;
        }
        #endregion
        #region Boat Operator List for Boat
        public List<SelectListItem> BoatOperatorLists()
        {
            List<SelectListItem> BoatOperatorList = new List<SelectListItem>();
            BoatOperatorList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            BoatOperatorList.Add(new SelectListItem { Value = "1", Text = "Peter" });
            BoatOperatorList.Add(new SelectListItem { Value = "2", Text = "Merlinm" });
            return BoatOperatorList;
        }
        #endregion
        #region excess for Farm Structure
        public List<SelectListItem> ExcessRateFS()
        {
            List<SelectListItem> ExcessListFS = new List<SelectListItem>();
            ExcessListFS.Add(new SelectListItem { Value = "5000", Text = "$5000" });
            ExcessListFS.Add(new SelectListItem { Value = "10000", Text = "$10000" });
            return ExcessListFS;
        }
        #endregion
        #region Roof made of for Farm policy home building
        public List<SelectListItem> ExternalWallsMadeList()
        {
            List<SelectListItem> ExtWallMadeList = new List<SelectListItem>();
            ExtWallMadeList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            ExtWallMadeList.Add(new SelectListItem { Value = "1", Text = "Asbestos cement sheet or plank" });
            ExtWallMadeList.Add(new SelectListItem { Value = "2", Text = "Brick Veneer" });
            ExtWallMadeList.Add(new SelectListItem { Value = "3", Text = "Cladding *" });
            ExtWallMadeList.Add(new SelectListItem { Value = "4", Text = "Concrete" });
            ExtWallMadeList.Add(new SelectListItem { Value = "5", Text = "Concrete Block" });
            ExtWallMadeList.Add(new SelectListItem { Value = "6", Text = "Double Brick" });
            ExtWallMadeList.Add(new SelectListItem { Value = "7", Text = "Metel" });
            ExtWallMadeList.Add(new SelectListItem { Value = "8", Text = "Mud Brick" });
            ExtWallMadeList.Add(new SelectListItem { Value = "9", Text = "Other*" });
            ExtWallMadeList.Add(new SelectListItem { Value = "10", Text = "Rock/Stone" });
            ExtWallMadeList.Add(new SelectListItem { Value = "11", Text = "Rockcote" });
            ExtWallMadeList.Add(new SelectListItem { Value = "12", Text = "Timber" });
            return ExtWallMadeList;
        }
        #endregion
        #region External walls made for Farm policy home building
        public List<SelectListItem> RoofMadesList()
        {
            List<SelectListItem> RoofMadeList = new List<SelectListItem>();
            RoofMadeList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            RoofMadeList.Add(new SelectListItem { Value = "1", Text = "Cement tiles" });
            RoofMadeList.Add(new SelectListItem { Value = "2", Text = "Colourbond/zincalume" });
            RoofMadeList.Add(new SelectListItem { Value = "3", Text = "Corrugated Iron* (if selected a further question to ask if roof has any rust?" });
            RoofMadeList.Add(new SelectListItem { Value = "4", Text = "Fibro/ asbestos cement – this will require approval before completion of the quote." });
            RoofMadeList.Add(new SelectListItem { Value = "5", Text = "Metel Tiles" });
            RoofMadeList.Add(new SelectListItem { Value = "6", Text = "Other*" });
            RoofMadeList.Add(new SelectListItem { Value = "7", Text = "Terracotta tiles" });
            return RoofMadeList;
        }
        #endregion
        #region Electronic Items Type of equipment
        public List<SelectListItem> ElectronicTypeOfUnit()
        {
            List<SelectListItem> TypeofUnitList = new List<SelectListItem>();
            TypeofUnitList.Add(new SelectListItem { Value = "1", Text = "Audio visual" });
            TypeofUnitList.Add(new SelectListItem { Value = "2", Text = "Computer Peripheral" });
            TypeofUnitList.Add(new SelectListItem { Value = "3", Text = "Desktop Computer" });
            TypeofUnitList.Add(new SelectListItem { Value = "4", Text = "Electronic Cash Register" });
            TypeofUnitList.Add(new SelectListItem { Value = "5", Text = "Electronic Controller" });
            TypeofUnitList.Add(new SelectListItem { Value = "6", Text = "Electronic Scales" });
            TypeofUnitList.Add(new SelectListItem { Value = "7", Text = "Fax Machine" });
            TypeofUnitList.Add(new SelectListItem { Value = "8", Text = "Laser Level" });
            TypeofUnitList.Add(new SelectListItem { Value = "9", Text = "Laser Theodolite" });
            TypeofUnitList.Add(new SelectListItem { Value = "10", Text = "Notebook Computer" });
            TypeofUnitList.Add(new SelectListItem { Value = "11", Text = "Photocopier" });
            TypeofUnitList.Add(new SelectListItem { Value = "12", Text = "Printer" });
            TypeofUnitList.Add(new SelectListItem { Value = "13", Text = "Telephone Equipment" });
            return TypeofUnitList;
        }
        #endregion
        #region Class of Animal at Livestock
        public List<SelectListItem> ClassofAnimalLivestock()
        {
            List<SelectListItem> ClassofAnimalList = new List<SelectListItem>();
            ClassofAnimalList.Add(new SelectListItem { Value = "1", Text = "CATTLE" });
            ClassofAnimalList.Add(new SelectListItem { Value = "2", Text = "DEER" });
            ClassofAnimalList.Add(new SelectListItem { Value = "3", Text = "GOAT" });
            ClassofAnimalList.Add(new SelectListItem { Value = "4", Text = "HORSE" });
            ClassofAnimalList.Add(new SelectListItem { Value = "5", Text = "PIG" });
            ClassofAnimalList.Add(new SelectListItem { Value = "6", Text = "SHEEP" });
            return ClassofAnimalList;
        }
        #endregion
        #region Type of Animal at Livestock
        public List<SelectListItem> TypeofAnimalLivestock()
        {
            List<SelectListItem> TypeofAnimalList = new List<SelectListItem>();
            TypeofAnimalList.Add(new SelectListItem { Value = "1", Text = "BUCK" });
            TypeofAnimalList.Add(new SelectListItem { Value = "2", Text = "DOE" });
            TypeofAnimalList.Add(new SelectListItem { Value = "3", Text = "GOAT" });
            TypeofAnimalList.Add(new SelectListItem { Value = "4", Text = "HORSE" });
            TypeofAnimalList.Add(new SelectListItem { Value = "5", Text = "PIG" });
            TypeofAnimalList.Add(new SelectListItem { Value = "6", Text = "SHEEP" });
            return TypeofAnimalList;
        }
        #endregion
        #region Age of Animal at Livestock
        public List<SelectListItem> AgeofAnimalLivestock()
        {
            List<SelectListItem> AgeofAnimalList = new List<SelectListItem>();
            AgeofAnimalList.Add(new SelectListItem { Value = "1", Text = "1" });
            AgeofAnimalList.Add(new SelectListItem { Value = "2", Text = "2" });
            AgeofAnimalList.Add(new SelectListItem { Value = "3", Text = "3" });
            AgeofAnimalList.Add(new SelectListItem { Value = "4", Text = "4" });
            AgeofAnimalList.Add(new SelectListItem { Value = "5", Text = "5" });
            AgeofAnimalList.Add(new SelectListItem { Value = "6", Text = "6" });
            return AgeofAnimalList;
        }
        #endregion
        #region Use of Animal at Livestock
        public List<SelectListItem> UseofAnimalLivestock()
        {
            List<SelectListItem> UseofAnimalList = new List<SelectListItem>();
            UseofAnimalList.Add(new SelectListItem { Value = "1", Text = "1" });
            UseofAnimalList.Add(new SelectListItem { Value = "2", Text = "2" });
            UseofAnimalList.Add(new SelectListItem { Value = "3", Text = "3" });
            UseofAnimalList.Add(new SelectListItem { Value = "4", Text = "4" });
            UseofAnimalList.Add(new SelectListItem { Value = "5", Text = "5" });
            UseofAnimalList.Add(new SelectListItem { Value = "6", Text = "6" });
            return UseofAnimalList;
        }
        #endregion
        #region Limit of Indemnity
        public List<SelectListItem> LimitOfIndemnity()
        {
            List<SelectListItem> LimitOfIndemnityList = new List<SelectListItem>();
            LimitOfIndemnityList.Add(new SelectListItem { Value = "5000000", Text = "$5,000,000" });
            LimitOfIndemnityList.Add(new SelectListItem { Value = "10000000", Text = "$10,000,000" });
            LimitOfIndemnityList.Add(new SelectListItem { Value = "20000000", Text = "$20,000,000" });
            return LimitOfIndemnityList;
        }
        #endregion
        #region Type of Accommodation Farm Liability
        public List<SelectListItem> TypeOfAccommodation()
        {
            List<SelectListItem> LimitOfIndemnityList = new List<SelectListItem>();
            LimitOfIndemnityList.Add(new SelectListItem { Value = "1", Text = "Your Home" });
            LimitOfIndemnityList.Add(new SelectListItem { Value = "2", Text = "A Seperate home on the farm" });
            LimitOfIndemnityList.Add(new SelectListItem { Value = "3", Text = "Caravans" });
            LimitOfIndemnityList.Add(new SelectListItem { Value = "4", Text = "Camping Facilities" });
            LimitOfIndemnityList.Add(new SelectListItem { Value = "5", Text = "Other" });
            return LimitOfIndemnityList;
        }
        #endregion
        #region Listed Vehicle Make from Motor Cover
        public List<SelectListItem> VehicleMake()
        {
            List<SelectListItem> VehicleMakeList = new List<SelectListItem>();
            VehicleMakeList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            VehicleMakeList.Add(new SelectListItem { Value = "1", Text = "Abarth" });
            VehicleMakeList.Add(new SelectListItem { Value = "2", Text = "Alpha Romeo" });
            VehicleMakeList.Add(new SelectListItem { Value = "3", Text = "Astin Martin " });
            VehicleMakeList.Add(new SelectListItem { Value = "4", Text = "Audi etc" });
            return VehicleMakeList;
        }
        #endregion
        #region What is the family from Motor Cover
        public List<SelectListItem> MotorCoverFamily()
        {
            List<SelectListItem> FamilyList = new List<SelectListItem>();
            FamilyList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            FamilyList.Add(new SelectListItem { Value = "1", Text = "HD45" });
            FamilyList.Add(new SelectListItem { Value = "2", Text = "HD55" });
            FamilyList.Add(new SelectListItem { Value = "3", Text = "HD65" });
            FamilyList.Add(new SelectListItem { Value = "4", Text = "HD75" });
            return FamilyList;
        }
        #endregion
        #region Description from motorcover/coverdetails
        public List<SelectListItem> MCCDDescription()
        {
            List<SelectListItem> MCCDDescriptionList = new List<SelectListItem>();
            MCCDDescriptionList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "74", Text = "4 in 1 bucket" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "1", Text = "ABS Brakes" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "86", Text = "Air compressor" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "2", Text = "Air Conditioning" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "3", Text = "Alarm System" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "70", Text = "Auger" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "67", Text = "Blade" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "4", Text = "Body Kit" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "5", Text = "Bonnet Protector" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "6", Text = "Boot Liner" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "68", Text = "Bucket" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "7", Text = "Bull Bar" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "8", Text = "Bullwing doors" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "9", Text = "Canopy" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "10", Text = "Car Refrigerator" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "11", Text = "Car Telephone" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "12", Text = "Cargo Barrier" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "13", Text = "Cargo tray" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "14", Text = "CB Radio" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "15", Text = "CD Player/Radio" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "16", Text = "Central Locking" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "75", Text = "Chain digger" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "17", Text = "Cruise Control" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "18", Text = "Dash mats" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "19", Text = "Decorated Panel" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "20", Text = "Diff Locks" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "82", Text = "Drag blade" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "21", Text = "Driving Lights" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "22", Text = "Driving Seats" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "23", Text = "Dual Batteries" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "24", Text = "Dual Fuel Tanks" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "77", Text = "Duel fuel filters" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "25", Text = "Electric brakes" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "26", Text = "Electric Windows" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "27", Text = "Electronic rust protectio" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "90", Text = "Exhaust system" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "88", Text = "External sun visor" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "28", Text = "Fire Extinguisher" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "29", Text = "Fixed Tool Box" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "30", Text = "Flares" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "31", Text = "Floor mats" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "69", Text = "Fork" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "32", Text = "Gas Conversion" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "33", Text = "GPS" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "89", Text = "Hand controls" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "80", Text = "Hay fork" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "34", Text = "Head Light Protectors" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "65", Text = "Hydraulic Lift" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "35", Text = "Large rear view vision mi" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "36", Text = "Leather and paint protect" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "37", Text = "Leather Seats" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "81", Text = "Lift fork" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "38", Text = "Mag Wheels" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "92", Text = "Metallic Paint " });
            MCCDDescriptionList.Add(new SelectListItem { Value = "39", Text = "Mud flaps" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "40", Text = "Nudge Bar" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "41", Text = "Overhead console" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "73", Text = "Pallet fork" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "71", Text = "Post hole digger" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "42", Text = "Reverse Parking Sensor" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "84", Text = "Reversing camera" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "83", Text = "Ripper" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "43", Text = "Roof Rack" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "44", Text = "Rust proofing" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "45", Text = "Seat Covers" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "46", Text = "Side Steps" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "47", Text = "Signwriting" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "72", Text = "Silage grab" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "48", Text = "Snorkel" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "87", Text = "Solar panels" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "49", Text = "Sound System" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "50", Text = "Spoiler" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "51", Text = "Sports Steering Wheel" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "52", Text = "Spot Lights" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "53", Text = "SS Scruff Plates" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "76", Text = "Stock crate" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "54", Text = "Stone Guard" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "55", Text = "Sun Roof" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "79", Text = "Supercharger" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "56", Text = "Suspension Kit" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "57", Text = "Tinted Windows" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "58", Text = "Tourneau Cover" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "59", Text = "Tow Bar" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "66", Text = "Tray" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "60", Text = "Turbocharger" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "61", Text = "Two Way Radio" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "62", Text = "UHF Radio" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "203", Text = "Unspecified accessories - sublimit $10,000" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "201", Text = "Unspecified accessories - sublimit $2,000" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "202", Text = "Unspecified accessories - sublimit $5,000" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "85", Text = "Water tank" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "63", Text = "Winch" });
            MCCDDescriptionList.Add(new SelectListItem { Value = "64", Text = "X Pack Protection" });
            return MCCDDescriptionList;
        }
        #endregion
        #region Address for Motor Cover
        public List<SelectListItem> MCADAddress()
        {
            List<SelectListItem> AddressList = new List<SelectListItem>();
            AddressList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            return AddressList;
        }
        #endregion
        #region VIN/chassis number for Motor Cover
        public List<SelectListItem> MCADVinNumber()
        {
            List<SelectListItem> VNList = new List<SelectListItem>();
            VNList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            return VNList;
        }
        #endregion
        #region Engine Number for Motor Cover
        public List<SelectListItem>MCADEngineNumber()
        {
            List<SelectListItem> ENList = new List<SelectListItem>();
            ENList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            return ENList;
        }
        #endregion
    }
}