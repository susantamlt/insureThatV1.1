using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InsureThatAPI.Models;
using InsureThatAPI.CommonMethods;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InsureThatAPI.Controllers
{
    public class ForgotPasswordDetailsController : ApiController
    {
        // GET: api/ForgotPasswordDetails
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ForgotPasswordDetails/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ForgotPasswordDetails
        public async Task<HttpResponseMessage> Post([FromBody]ForgotPasswordDetails value)
        {
            try
            {
                ForgotPasswordDetailsRef FgDetailsRef = new ForgotPasswordDetailsRef();
                ForgotPasswordClass fgClass = new ForgotPasswordClass();
                var authorization = Request.Headers.Authorization;
                string UserName = string.Empty;
                if (authorization == null || authorization.Scheme != "Bearer")
                    return Request.CreateResponse<ForgotPasswordDetailsRef>(HttpStatusCode.BadRequest, FgDetailsRef);

                if (string.IsNullOrEmpty(authorization.Parameter))
                {
                    return Request.CreateResponse<ForgotPasswordDetailsRef>(HttpStatusCode.BadRequest, FgDetailsRef);
                }
                var token = authorization.Parameter;
                bool strbool = CommonUseFunctionClass.ValidateToken(token, out UserName);
                if (strbool == true)
                {
                    FgDetailsRef = await fgClass.GetFogotPasswordDetails(value.Email, value.Password, value.ConfirmPassword, value.reCaptcha);
                    if (FgDetailsRef != null)
                    {
                        return Request.CreateResponse<ForgotPasswordDetailsRef>(HttpStatusCode.OK, FgDetailsRef);
                    }
                    else
                    {
                        FgDetailsRef.Status = "Failure";
                        return Request.CreateResponse<ForgotPasswordDetailsRef>(HttpStatusCode.BadRequest, FgDetailsRef);
                    }
                }
                else
                {
                    FgDetailsRef.Status = "Failure";
                    FgDetailsRef.ErrorMessage.Add("Token Is Not valid");
                    return Request.CreateResponse<ForgotPasswordDetailsRef>(HttpStatusCode.BadRequest, FgDetailsRef);
                }
            }
            catch (Exception xp)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, xp.Message);
            }
            finally
            {

            }
            return null;
        }

        // PUT: api/ForgotPasswordDetails/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/ForgotPasswordDetails/5
        public void Delete(int id)
        {

        }
    }
}
