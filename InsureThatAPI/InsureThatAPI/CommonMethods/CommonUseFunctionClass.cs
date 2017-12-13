using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using InsureThatAPI.Models;
using System.Text.RegularExpressions;
using System.Web.Http.Filters;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Text;
using System.Web.Configuration;

namespace InsureThatAPI.CommonMethods
{
    public class CommonUseFunctionClass
    {

        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        public static string GenerateToken(string username, int expireMinutes = 20)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                        new Claim(ClaimTypes.Name, username)
                    }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public async static Task<bool> CheckUser(string username, string password)
        {
            try
            {
                LoginDetailsRef loginRef = new LoginDetailsRef();
                LogInDetailsClass logincls = new LogInDetailsClass();
                loginRef = await logincls.GetLogInDetailsPage(username, password);
                if (loginRef.LogInData!=null && loginRef.LogInData.UserName != string.Empty || loginRef.LogInData.Password !=string.Empty || string.IsNullOrWhiteSpace(Convert.ToString(loginRef.LogInData.Password)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception xp)
            {
                return false;
            }
            finally
            {

            }
           
        }

        public static bool ValidateToken(string token, out string username)
        {
            username = null;
            var simplePrinciple = GetPrincipal(token);
            var identity = simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim?.Value;

            if (string.IsNullOrEmpty(username))
                return false;

            // More validate to check whether username exists in system

            return true;
        }

        //protected Task<IPrincipal> AuthenticateJwtToken(string token)
        //{
        //    string username;
        //    if (ValidateToken(token, out username))
        //    {
        //        // based on username to get more information from database in order to build local identity
        //        var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, username)
        //        // Add more claims if needed: Roles, ...
        //    };
        //        var identity = new ClaimsIdentity(claims, "Jwt");
        //        IPrincipal user = new ClaimsPrincipal(identity);
        //        return Task.FromResult(user);
        //    }
        //    return Task.FromResult<IPrincipal>(null);
        //}


        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }
            catch (Exception xp)
            {
                //should write log
                return null;
            }
        }
       

    }
    public class ActionFilter: System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.RequestContext.HttpContext.Request["g-recaptcha-response"] != null)
            {

                string privatekey = WebConfigurationManager.AppSettings["RecaptchaPrivateKey"];
                string response = filterContext.RequestContext.HttpContext.Request["g-recaptcha-response"];
                filterContext.ActionParameters["CaptchaValid"] = LogInDetailsClass.Validate(response, privatekey);
            }
        }
    }
    public static class HtmlHelpers
    {

        public static IHtmlString ReCaptcha(this HtmlHelper helper)
        {
            StringBuilder sb = new StringBuilder();
            string publickey = WebConfigurationManager.AppSettings["RecaptchaPublicKey"];
            sb.AppendLine("<script type=\"text/javascript\" src = 'https://www.google.com/recaptcha/api.js' ></ script > ");

            sb.AppendLine("");
            sb.AppendLine("<div class=\"g-recaptcha\" data-sitekey =\"" + publickey + "\"></div>");
            return MvcHtmlString.Create(sb.ToString());
        }

       
    }
}