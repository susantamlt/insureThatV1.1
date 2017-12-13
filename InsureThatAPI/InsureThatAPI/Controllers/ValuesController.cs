using InsureThatAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InsureThatAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            MasterDataEntities db = new MasterDataEntities();
            ////var list = db.GetCustomerDetails(null).ToList();
            ////List<string> names = new List<string>();
            ////if(list!=null && list.Any())
            ////{
            ////    names = list.Select(l => l.CustomerName).ToList();
            ////    return names;
            ////}
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
