using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using InsureThatAPI.CommonMethods;
using Newtonsoft.Json;
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;
using System.Text;

namespace InsureThatAPI.Controllers
{
    public class DisclosureController : Controller
    {
        // GET: Disclosure
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DisclosureDetails(int? cid, int? PcId)
        {
            string apikey = null;
            if (Session["Apikey"] != null)
            {
                apikey = Session["Apikey"].ToString();
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            DisclosureDetails DisclosureDetails = new DisclosureDetails();
            if (cid.HasValue && cid > 0)
            {
                DisclosureDetails.CustomerId = cid.Value;
            }
            DisclosureDetails.PolicyInclusions = new List<SessionModel>();
            var Policyincllist = Session["Policyinclustions"] as List<SessionModel>;
            DisclosureDetails.PolicyInclusions = Policyincllist;
            DisclosureDetails.PreviousinsurerObj = new PreviousInsurer();
            DisclosureDetails.PreviousinsurerObj.EiId = 2019;
            DisclosureDetails.RenewpolicyObj = new RenewPolicy();
            DisclosureDetails.RenewpolicyObj.EiId = 2007;
            DisclosureDetails.CriminaloffenceObj = new CriminalOffence();
            DisclosureDetails.CriminaloffenceObj.EiId = 2008;
            DisclosureDetails.PrisonsentenceObj = new PrisonSentence();
            DisclosureDetails.PrisonsentenceObj.EiId = 2009;
            DisclosureDetails.UndischargedObj = new Undischarged();
            DisclosureDetails.UndischargedObj.EiId = 2010;
            DisclosureDetails.BankruptObj = new Bankrupts();
            DisclosureDetails.BankruptObj.EiId = 2011;
            DisclosureDetails.DateObj = new DischargeDate();
            DisclosureDetails.DateObj.EiId = 2027;
            DisclosureDetails.ImmediatedangerObj = new ImmediateDanger();
            DisclosureDetails.ImmediatedangerObj.EiId = 2016;
            DisclosureDetails.DutydisclosureObj = new DutyOfDisclosure();
            DisclosureDetails.DutydisclosureObj.EiId = 2018;
            DisclosureDetails.DetailsboxObj = new DetailsBox();
            DisclosureDetails.DetailsboxObj.EiId = 415;
            return View(DisclosureDetails);

        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> DisclosureDetails(DisclosureDetails DisclosureDetails)
        {
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            AddressData ad = new AddressData();
            string apikey = null;
            ValueData vd = new ValueData();
            if (Session["Apikey"] != null)
            {
                apikey = Session["Apikey"].ToString();
            }
            List<ValueData> lstvd = new List<ValueData>();
            vd.Element = new Elements();
            DisclosureDetails.ApiKey = apikey;
            DisclosureDetails.ValueData = new List<ValueData>();
            if (DisclosureDetails != null)
            {
                if (DisclosureDetails.PreviousinsurerObj.EiId != null && DisclosureDetails.PreviousinsurerObj.Pinsurer != null)
                {
                    vd.Element.ElId = DisclosureDetails.PreviousinsurerObj.EiId;
                    vd.Element.ItId = 0;
                    vd.Value = DisclosureDetails.PreviousinsurerObj.Pinsurer;
                    lstvd.Add(vd);
                }
                if (DisclosureDetails.RenewpolicyObj.EiId != null && DisclosureDetails.RenewpolicyObj.Rpolicy != null)
                {
                    ValueData vds = new ValueData();
                    vds.Element = new Elements();
                    vds.Element.ElId = DisclosureDetails.RenewpolicyObj.EiId;
                    vds.Element.ItId = 0;
                    vds.Value = DisclosureDetails.RenewpolicyObj.Rpolicy;
                    lstvd.Add(vds);
                }
                if (DisclosureDetails.CriminaloffenceObj.EiId != null && DisclosureDetails.CriminaloffenceObj.Offence != null)
                {
                    ValueData vdss = new ValueData();
                    vdss.Element = new Elements();
                    vdss.Element.ElId = DisclosureDetails.CriminaloffenceObj.EiId;
                    vdss.Element.ItId = 0;
                    vdss.Value = DisclosureDetails.CriminaloffenceObj.Offence;
                    lstvd.Add(vdss);
                }
                if (DisclosureDetails.PrisonsentenceObj.EiId != null && DisclosureDetails.PrisonsentenceObj.Sentence != null)
                {
                    ValueData vdssss = new ValueData();
                    vdssss.Element = new Elements();
                    vdssss.Element.ElId = DisclosureDetails.PrisonsentenceObj.EiId;
                    vdssss.Element.ItId = 0;
                    vdssss.Value = DisclosureDetails.PrisonsentenceObj.Sentence;
                    lstvd.Add(vdssss);
                }
                //if (DisclosureDetails.UndischargedObj.EiId != null && DisclosureDetails.UndischargedObj.Undischarge != null)
                //{
                //    ValueData vdssy = new ValueData();
                //    vdssy.Element = new Elements();
                //    vdssy.Element.ElId = DisclosureDetails.UndischargedObj.EiId;
                //    vdssy.Element.ItId = 0;
                //    vdssy.Value = DisclosureDetails.UndischargedObj.Undischarge;
                //    lstvd.Add(vdssy);
                //}
                if (DisclosureDetails.BankruptObj.EiId != null && DisclosureDetails.BankruptObj.Bankrupt != null)
                {
                    ValueData vdsy = new ValueData();
                    vdsy.Element = new Elements();
                    vdsy.Element.ElId = DisclosureDetails.BankruptObj.EiId;
                    vdsy.Element.ItId = 0;
                    vdsy.Value = DisclosureDetails.BankruptObj.Bankrupt;
                    lstvd.Add(vdsy);
                }
                //if (DisclosureDetails.DateObj.EiId != null && DisclosureDetails.DateObj.Date != null)
                //{
                //    ValueData vdsa = new ValueData();
                //    vdsa.Element = new Elements();
                //    vdsa.Element.ElId = DisclosureDetails.DateObj.EiId;
                //    vdsa.Element.ItId = 0;
                //    vdsa.Value = DisclosureDetails.DateObj.Date;
                //    lstvd.Add(vdsa);
                //}
                if (DisclosureDetails.ImmediatedangerObj.EiId != null && DisclosureDetails.ImmediatedangerObj.Danger != null)
                {
                    ValueData vdsaa = new ValueData();
                    vdsaa.Element = new Elements();
                    vdsaa.Element.ElId = DisclosureDetails.ImmediatedangerObj.EiId;
                    vdsaa.Element.ItId = 0;
                    vdsaa.Value = DisclosureDetails.ImmediatedangerObj.Danger;
                    lstvd.Add(vdsaa);
                }
                if (DisclosureDetails.DutydisclosureObj.EiId != null && DisclosureDetails.DutydisclosureObj.Dutydisclosure != null)
                {
                    ValueData vdsaaa = new ValueData();
                    vdsaaa.Element = new Elements();
                    vdsaaa.Element.ElId = DisclosureDetails.DutydisclosureObj.EiId;
                    vdsaaa.Element.ItId = 0;
                    vdsaaa.Value = DisclosureDetails.DutydisclosureObj.Dutydisclosure;
                    lstvd.Add(vdsaaa);
                }
                if (DisclosureDetails.DetailsboxObj.EiId != null && DisclosureDetails.DetailsboxObj.Details != null)
                {
                    ValueData vdsaw = new ValueData();
                    vdsaw.Element = new Elements();
                    vdsaw.Element.ElId = DisclosureDetails.DetailsboxObj.EiId;
                    vdsaw.Element.ItId = 0;
                    vdsaw.Value = DisclosureDetails.DetailsboxObj.Details;
                    lstvd.Add(vdsaw);
                }
            }
            DisclosureDetails.ValueData = lstvd;
            // model.AddressData = model.suburb + model.state + model.postcode;
            StringContent content = new StringContent(JsonConvert.SerializeObject(DisclosureDetails), Encoding.UTF8, "application/json");
            var responses = await hclient.PostAsync("DisclosureDetails", content);
            var result = await responses.Content.ReadAsStringAsync();
            if (result != null)
            {
            }
            return RedirectToAction("ClaimsQ", "Customer", new { cid = DisclosureDetails.CustomerId });
            return View(DisclosureDetails);
        }
    }
}