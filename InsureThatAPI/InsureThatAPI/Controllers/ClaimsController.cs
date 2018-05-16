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
    public class ClaimsController : Controller
    {
        // GET: Claims
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> ClaimsDetails(int? cid)
        {
            NewPolicyDetailsClass Calimmodel = new NewPolicyDetailsClass();
            List<SelectListItem> ClaimTypeList = new List<SelectListItem>();
            ClaimTypeList = Calimmodel.ClaimTypeRular(); //For Rular Life style
            //ClaimTypeList = Calimmodel.ClaimTypeFarm(); //For Farm
            ClaimsDetails ClaimsDetails = new ClaimsDetails();
            ClaimsDetails.PolicyInclusions = new List<string>();
            Session["Controller"] = "Claims";
            Session["ActionName"] = "ClaimsDetails";
            string apikey = null;
            if(Session["ApiKey"]!=null)
            {
                apikey = Session["ApiKey"].ToString();
            }
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage getunit = await hclient.GetAsync("PreviousClaims?ApiKey=" + apikey);
            var EmpResponse = getunit.Content.ReadAsStringAsync().Result;
            if (EmpResponse != null)
            {
                ClaimsDetails.ClaimTypeRowsourceOptions = new List<RowsourceDatas>();
                ClaimsDetails.ValueData = new List<ValueData>();
                ClaimsDetails = JsonConvert.DeserializeObject<ClaimsDetails>(EmpResponse);
            }
            ClaimsDetails.ClaimtypeObj = new ClaimType();
            ClaimsDetails.ClaimtypeObj.ClaimTypeList = ClaimTypeList;
            ClaimsDetails.ClaimtypeObj.EiId = 73;
            ClaimsDetails.DetailsclaimObj = new DetailsOfClaim();
            ClaimsDetails.DetailsclaimObj.EiId = 74;
            ClaimsDetails.ClaimvalueObj = new ClaimValue();
            ClaimsDetails.ClaimvalueObj.EiId = 75;
            ClaimsDetails.YearObj = new ClaimYear();
            ClaimsDetails.YearObj.EiId = 76;
            ClaimsDetails.InsurerObj = new ClaimInsurer();
            ClaimsDetails.InsurerObj.EiId = 77;
            ClaimsDetails.DriverObj = new ClaimDriver();
            ClaimsDetails.DriverObj.EiId = 79;
            if (cid != null && cid.HasValue)
            {
                ClaimsDetails.CustomerId = cid.Value;
            }
            return View(ClaimsDetails);
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> ClaimsDetails(List<ValueData> data)
        {
            HttpClient hclient = new HttpClient();
            string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
            hclient.BaseAddress = new Uri(url);
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           // string apikey = null;
            List<ValueData> lstvd = new List<ValueData>();
            List<ValueData> vdlst = new List<ValueData>();
            vdlst = data;
            int? cid = 0;
            if (Session["cid"] != null)
            {
                cid = Convert.ToInt32(Session["cid"]);
            }
            ClaimsDetails ClaimsDetails = new ClaimsDetails();
            if (vdlst!=null && vdlst.Count()>0)
            {
                for(int v=0;v<vdlst.Count();v++)
                {
                    ValueData vd = new ValueData();
                    vd.Element = new Elements();
                    vd.Element.ElId = vdlst[v].Element.ElId;
                    vd.Element.ItId = vdlst[v].Element.ItId;
                    vd.Value = vdlst[v].Value;
                    lstvd.Add(vd);
                }
            }            
            ClaimsDetails.ValueData = lstvd;
            if (Session["apiKey"] != null)
            {
                string ApiKey = Session["apiKey"].ToString();
                ClaimsDetails.ApiKey = ApiKey;
            }
            StringContent content = new StringContent(JsonConvert.SerializeObject(ClaimsDetails), Encoding.UTF8, "application/json");
            var responses = await hclient.PostAsync("PreviousClaims", content);
            var result = await responses.Content.ReadAsStringAsync();
            if (result != null)
            {
                return Json(Url.Action("PremiumDetails", "Customer", new { cid = cid }));
            }
            return RedirectToAction("PremiumDetails", "Customer", new { cid = ClaimsDetails.CustomerId });
            //return View(ClaimsDetails);
        }
        [HttpPost]
        public ActionResult ClaimsDetailAjax(int id, string content)
        {
            NewPolicyDetailsClass commonModel = new NewPolicyDetailsClass();
            if (Request.IsAjaxRequest())
            {
                int cid = 1;
                ViewBag.cid = cid;
                if (content == "ClaimsDetail")
                {
                    List<SelectListItem> ClaimTypeList = new List<SelectListItem>();
                    ClaimTypeList = commonModel.ClaimTypeRular(); //For Rular Life style
                    return Json(new { status = true, des = ClaimTypeList });
                }
                else
                {
                    return Json(new { status = false, des = "" });
                }
            }
            return Json(new { status = false, des = "" });
        }
    }
}