using InsureThatAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Controllers
{
    public class FloodController : Controller
    {
        // GET: Flood
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FloodArea(int? cid, int? type, int? insureId)
        {
            Floodarea model = new Floodarea();
            if(Session["ApiKey"]!=null)
            {

            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            model.CustomerId = cid;
            model.policyType = type;
            model.insureId = insureId;
            model.IsFloodRequired = -1;
            model.FldDefault = -1;
            return View(model);
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> FloodArea(Floodarea model)
        {
            if (model != null)
            {
                if (model.IsFloodRequired == 0)
                {
                    int? cid = model.CustomerId;
                    int? type = model.policyType;
                    model.HasMadeAClaim = -1;
                    HttpClient hclient = new HttpClient();
                    string url = System.Configuration.ConfigurationManager.AppSettings["APIURL"];
                    hclient.BaseAddress = new Uri(url);
                    if(Session["ApiKey"]!=null)
                    {
                        model.ApiKey = Session["ApiKey"].ToString();
                    }
                    hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    var responses = await hclient.PostAsync("PolicyDetails", content);
                    var result = await responses.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<Floodarea>(result);
                    model.CustomerId = cid;
                    model.policyType = type;
                    return RedirectToAction("CustomerRegistration", "Customer", new { cid = model.CustomerId, type = model.policyType });
                }
                else if (model.IsFloodRequired == 1)
                {
                    ViewBag.error = "Sorry we do not offer flood cover at this time. Click No to continue without flood cover.";
                }

            }
            return View(model);
        }
    }
}