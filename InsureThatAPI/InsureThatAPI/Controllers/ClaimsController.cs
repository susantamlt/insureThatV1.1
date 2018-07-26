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
        public async System.Threading.Tasks.Task<ActionResult> ClaimsDetails(int? cid,int? PcId)
        {
            NewPolicyDetailsClass Calimmodel = new NewPolicyDetailsClass();
            List<SelectListItem> ClaimTypeList = new List<SelectListItem>();
            ClaimTypeList = Calimmodel.ClaimTypeRular(); 
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
            if (ClaimsDetails.ValueData.Exists(p => p.Element.ElId == ClaimsDetails.ClaimtypeObj.EiId))
            {
                string val = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.ClaimtypeObj.EiId).Select(p => p.Value).FirstOrDefault();
                ClaimsDetails.ClaimtypeObj.Type = val;
            }
            if (ClaimsDetails.ValueData.Exists(p => p.Element.ElId == ClaimsDetails.ClaimvalueObj.EiId))
            {
                string val = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.ClaimvalueObj.EiId).Select(p => p.Value).FirstOrDefault();
                ClaimsDetails.ClaimvalueObj.Cvalue = val;
            }
            if (ClaimsDetails.ValueData.Exists(p => p.Element.ElId == ClaimsDetails.DetailsclaimObj.EiId))
            {
                string val = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.DetailsclaimObj.EiId).Select(p => p.Value).FirstOrDefault();
                ClaimsDetails.DetailsclaimObj.Details = val;
            }
            if (ClaimsDetails.ValueData.Exists(p => p.Element.ElId == ClaimsDetails.DriverObj.EiId))
            {
                string val = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.DriverObj.EiId).Select(p => p.Value).FirstOrDefault();
                ClaimsDetails.DriverObj.Driver = val;
            }
            if (ClaimsDetails.ValueData.Exists(p => p.Element.ElId == ClaimsDetails.InsurerObj.EiId))
            {
                string val = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.InsurerObj.EiId).Select(p => p.Value).FirstOrDefault();
                ClaimsDetails.InsurerObj.Insurer = val;
            }
            ClaimsDetails.ClaimtypeobjList = new List<ValueDatas>();

            if (ClaimsDetails.ValueData!=null && ClaimsDetails.ValueData.Count()>0)
            {               
                if (ClaimsDetails.ValueData.Count() > 1)
                {
                    List<ValueDatas> elmnts = new List<ValueDatas>();
                    var power = ClaimsDetails.ValueData.ToList();
                    for (int i = 0; i < power.Count(); i++)
                    {
                        ValueDatas vds = new ValueDatas();
                        vds.Element = new Elements();
                        vds.Element.ElId = power[i].Element.ElId;
                        vds.Element.ItId = power[i].Element.ItId;
                        vds.Value = power[i].Value;
                        elmnts.Add(vds);
                    }
                    ClaimsDetails.ClaimtypeobjList = elmnts;
                }
                if (ClaimsDetails.ValueData.Exists(p => p.Element.ElId == ClaimsDetails.ClaimtypeObj.EiId))
                {
                    string val = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.ClaimtypeObj.EiId).Select(p => p.Value).FirstOrDefault();
                    if (val != null && !string.IsNullOrEmpty(val))
                    {
                        ClaimsDetails.ClaimtypeObj.Type = val;
                    }
                    if (ClaimsDetails.ValueData.Select(p => p.Element.ElId == ClaimsDetails.ClaimtypeObj.EiId).Count() > 1)
                    {
                        List<ValueDatas> elmnt = new List<ValueDatas>();
                        var DescriptionList = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.ClaimtypeObj.EiId).Select(p => p.Element.ItId).ToList();
                        for (int i = 0; i < DescriptionList.Count(); i++)
                        {
                            ValueDatas vds = new ValueDatas();
                            vds.Element = new Elements();
                            vds.Element.ElId = 73;
                            vds.Element.ItId = DescriptionList[i];
                            vds.Value = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.ClaimtypeObj.EiId && p.Element.ItId == DescriptionList[i]).Select(p => p.Value).FirstOrDefault();
                            elmnt.Add(vds);
                        }
                        ClaimsDetails.ClaimtypeobjList = elmnt;
                    }
                }
                if (ClaimsDetails.ValueData.Exists(p => p.Element.ElId == ClaimsDetails.ClaimvalueObj.EiId))
                {
                    string val = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.ClaimvalueObj.EiId).Select(p => p.Value).FirstOrDefault();
                    if (val != null && !string.IsNullOrEmpty(val))
                    {
                        ClaimsDetails.ClaimvalueObj.Cvalue = val;
                    }
                    if (ClaimsDetails.ValueData.Select(p => p.Element.ElId == ClaimsDetails.ClaimvalueObj.EiId).Count() > 1)
                    {
                        List<ValueDatas> elmn = new List<ValueDatas>();
                        var DescriptionList = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.ClaimvalueObj.EiId).Select(p => p.Element.ItId).ToList();
                        for (int i = 0; i < DescriptionList.Count(); i++)
                        {
                            ValueDatas vds = new ValueDatas();
                            vds.Element = new Elements();
                            vds.Element.ElId = 75;
                            vds.Element.ItId = DescriptionList[i];
                            vds.Value = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.ClaimvalueObj.EiId && p.Element.ItId == DescriptionList[i]).Select(p => p.Value).FirstOrDefault();
                            elmn.Add(vds);
                        }
                        ClaimsDetails.ClaimvalueobjList = elmn;
                    }
                }
                if (ClaimsDetails.ValueData.Exists(p => p.Element.ElId == ClaimsDetails.DetailsclaimObj.EiId))
                {
                    string val = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.DetailsclaimObj.EiId).Select(p => p.Value).FirstOrDefault();
                    if (val != null && !string.IsNullOrEmpty(val))
                    {
                        ClaimsDetails.DetailsclaimObj.Details = val;
                    }
                    if (ClaimsDetails.ValueData.Select(p => p.Element.ElId == ClaimsDetails.DetailsclaimObj.EiId).Count() > 1)
                    {
                        List<ValueDatas> elmn = new List<ValueDatas>();
                        var DescriptionList = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.DetailsclaimObj.EiId).Select(p => p.Element.ItId).ToList();
                        for (int i = 0; i < DescriptionList.Count(); i++)
                        {
                            ValueDatas vds = new ValueDatas();
                            vds.Element = new Elements();
                            vds.Element.ElId = 74;
                            vds.Element.ItId = DescriptionList[i];
                            vds.Value = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.DetailsclaimObj.EiId && p.Element.ItId == DescriptionList[i]).Select(p => p.Value).FirstOrDefault();
                            elmn.Add(vds);
                        }
                        ClaimsDetails.DetailsclaimobjList = elmn;
                    }
                }
                if (ClaimsDetails.ValueData.Exists(p => p.Element.ElId == ClaimsDetails.DriverObj.EiId))
                {
                    string val = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.DriverObj.EiId).Select(p => p.Value).FirstOrDefault();
                    if (val != null && !string.IsNullOrEmpty(val))
                    {
                        ClaimsDetails.DriverObj.Driver = val;
                    }
                    if (ClaimsDetails.ValueData.Select(p => p.Element.ElId == ClaimsDetails.DriverObj.EiId).Count() > 1)
                    {
                        List<ValueDatas> elmn = new List<ValueDatas>();
                        var DescriptionList = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.DriverObj.EiId).Select(p => p.Element.ItId).ToList();
                        for (int i = 0; i < DescriptionList.Count(); i++)
                        {
                            ValueDatas vds = new ValueDatas();
                            vds.Element = new Elements();
                            vds.Element.ElId = 79;
                            vds.Element.ItId = DescriptionList[i];
                            vds.Value = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.DriverObj.EiId && p.Element.ItId == DescriptionList[i]).Select(p => p.Value).FirstOrDefault();
                            elmn.Add(vds);
                        }
                        ClaimsDetails.DriverobjList = elmn;
                    }
                }
                if (ClaimsDetails.ValueData.Exists(p => p.Element.ElId == ClaimsDetails.InsurerObj.EiId))
                {
                    string val = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.InsurerObj.EiId).Select(p => p.Value).FirstOrDefault();
                    if (val != null && !string.IsNullOrEmpty(val))
                    {
                        ClaimsDetails.InsurerObj.Insurer = val;
                    }
                    if (ClaimsDetails.ValueData.Select(p => p.Element.ElId == ClaimsDetails.InsurerObj.EiId).Count() > 1)
                    {
                        List<ValueDatas> elmn = new List<ValueDatas>();
                        var DescriptionList = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.InsurerObj.EiId).Select(p => p.Element.ItId).ToList();
                        for (int i = 0; i < DescriptionList.Count(); i++)
                        {
                            ValueDatas vds = new ValueDatas();
                            vds.Element = new Elements();
                            vds.Element.ElId = 77;
                            vds.Element.ItId = DescriptionList[i];
                            vds.Value = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.InsurerObj.EiId && p.Element.ItId == DescriptionList[i]).Select(p => p.Value).FirstOrDefault();
                            elmn.Add(vds);
                        }
                        ClaimsDetails.InsurerobjList = elmn;
                    }
                }
                if (ClaimsDetails.ValueData.Exists(p => p.Element.ElId == ClaimsDetails.YearObj.EiId))
                {
                    string val = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.YearObj.EiId).Select(p => p.Value).FirstOrDefault();
                    if (val != null && !string.IsNullOrEmpty(val))
                    {
                        ClaimsDetails.YearObj.Year = val;
                    }
                    if (ClaimsDetails.ValueData.Select(p => p.Element.ElId == ClaimsDetails.YearObj.EiId).Count() > 1)
                    {
                        List<ValueDatas> elmn = new List<ValueDatas>();
                        var DescriptionList = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.YearObj.EiId).Select(p => p.Element.ItId).ToList();
                        for (int i = 0; i < DescriptionList.Count(); i++)
                        {
                            ValueDatas vds = new ValueDatas();
                            vds.Element = new Elements();
                            vds.Element.ElId = 76;
                            vds.Element.ItId = DescriptionList[i];
                            vds.Value = ClaimsDetails.ValueData.Where(p => p.Element.ElId == ClaimsDetails.YearObj.EiId && p.Element.ItId == DescriptionList[i]).Select(p => p.Value).FirstOrDefault();
                            elmn.Add(vds);
                        }
                        ClaimsDetails.yearobjList = elmn;
                    }
                    ClaimsDetails.PcId = PcId;
                    Session["PcId"] = PcId;
                }
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
            int? PcId = null;
            ClaimsDetails ClaimsDetails = new ClaimsDetails();
            if (Session["cid"] != null)
            {
                cid = Convert.ToInt32(Session["cid"]);
            }
            if(Session["PcId"]!=null)
            {
                PcId = Convert.ToInt32(Session["PcId"]);
                ClaimsDetails.PcId = PcId;
            }              
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
                return Json(Url.Action("PremiumDetails", "Customer", new { cid = cid, PcId = ClaimsDetails.PcId }));
            }
            return RedirectToAction("PremiumDetails", "Customer", new { cid = ClaimsDetails.CustomerId ,PcId= ClaimsDetails.PcId});
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