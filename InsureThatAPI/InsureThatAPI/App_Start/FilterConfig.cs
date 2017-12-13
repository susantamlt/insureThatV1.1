using System.Web;
using System.Web.Mvc;
using System.Web.Http.Filters;

namespace InsureThatAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new RecaptchaFilter());
        }
    }
}
