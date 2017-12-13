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
using InsureThatAPI.CommonMethods;


namespace InsureThatAPI.Controllers
{
    public class LogInDetailsController : ApiController
    {
        // GET: api/LogInDetails
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
       
        #region Post Method for Customer/Call Center Login Details

        [HttpPost]
        // GET: api/LogInDetails/5
        public async Task<HttpResponseMessage> Post([FromBody]LogInDetails value)
        {
            try
            {
                string UserName = value.UserName;
                string Password = value.Password;
                LoginDetailsRef loginref = new LoginDetailsRef();
                LogInDetails loginmodel = new LogInDetails();
                List<string> Errors = new List<string>();
                loginref.ErrorMessage = new List<string>();
                var regexSpace = new Regex(@"\s");
                if (UserName == null || UserName == string.Empty || string.IsNullOrWhiteSpace(UserName.Trim())|| Password == null || Password == string.Empty || string.IsNullOrWhiteSpace(Password.Trim()))
                {
                    if (UserName == null || UserName == string.Empty || string.IsNullOrWhiteSpace(UserName.Trim()))
                    {
                        Errors.Add("UserName is required");
                    }
                    if(Password == null || Password == string.Empty || string.IsNullOrWhiteSpace(Password.Trim()))
                    {
                        Errors.Add("Password is required");
                    }
                }
                else
                {
                    string specialCharacters = @"%!#$%^&*(-)?/><,:;'\|}]{[~`+=" + "\"";
                    char[] specialCharactersArray = specialCharacters.ToCharArray();
                    int index = UserName.IndexOfAny(specialCharactersArray);
                    //index == -1 no special characters
                    if (index == -1)
                    {
                    }
                    else
                    {
                        Errors.Add("UserName allows only three special characters '_' '.' '@' ");
                    }
                    if (regexSpace.IsMatch(UserName.Trim()))
                    {
                        Errors.Add("UserName should not have space");
                    }
                    string justNumber = new String(UserName.Trim().Where(Char.IsDigit).ToArray());
                    string justStrings = new String(UserName.Trim().Where(Char.IsLetter).ToArray());

                    if (justStrings == null || justStrings == string.Empty && UserName.Length <= 20)
                    {
                        Errors.Add("UserName does not allow only numeric values");
                    }
                }
                if (regexSpace.IsMatch(Convert.ToString(Password).Trim()))
                {
                    Errors.Add("Password should not have space");
                }
                       
                if (Errors != null && Errors.Count() > 0)
                {
                    loginref.Status = "Failure";
                    loginref.ErrorMessage = Errors;
                    return Request.CreateResponse<LoginDetailsRef>(HttpStatusCode.BadRequest, loginref);
                }
                else
                {
                    if (await CommonUseFunctionClass.CheckUser(UserName, Password))
                    {
                        string str = CommonUseFunctionClass.GenerateToken(UserName, 20);
                        if (str != string.Empty || str != null || string.IsNullOrWhiteSpace(str.Trim()))
                        {
                            loginref.Status = "Success";
                            loginref.Access_Token = str;
                            loginmodel.UserName = UserName;
                            loginref.LogInData = loginmodel;
                            return Request.CreateResponse<LoginDetailsRef>(HttpStatusCode.OK, loginref);
                        }
                        else
                        {
                            loginref.Status = "Failure";
                            loginref.ErrorMessage.Add("Token Is Not generated");
                            return Request.CreateResponse<LoginDetailsRef>(HttpStatusCode.BadRequest, loginref);
                        }
                    }
                    else
                    {
                        loginref.Status = "Failure";
                        loginref.ErrorMessage.Add("UserName or Password are not valid.");
                        return Request.CreateResponse<LoginDetailsRef>(HttpStatusCode.BadRequest, loginref);
                    }
                }
            }
            catch (Exception xp)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, xp.Message);
            }
            return null;
        }

        #endregion

        #region Token Generated Methods

        //  private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        //public static string GenerateToken(string username, int expireMinutes = 20)
        //{
        //    var symmetricKey = Convert.FromBase64String(Secret);
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    var now = DateTime.UtcNow;
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //                {
        //                new Claim(ClaimTypes.Name, username)
        //            }),

        //        Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var stoken = tokenHandler.CreateToken(tokenDescriptor);
        //    var token = tokenHandler.WriteToken(stoken);

        //    return token;
        //}

        //public bool CheckUser(string username, int password)
        //{
        //    try
        //    {
        //        LoginDetailsRef loginRef = new LoginDetailsRef();
        //        LogInDetailsClass logincls = new LogInDetailsClass();
        //        loginRef = logincls.GetLogInDetailsPage(username, password);
        //        if (loginRef.LogInData.UserName.Trim() != string.Empty || loginRef.LogInData.Password.Value != 0 || string.IsNullOrWhiteSpace(Convert.ToString(loginRef.LogInData.Password).Trim()))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception xp)
        //    {

        //    }
        //    finally
        //    {

        //    }
        //    return false;
        //}



        #endregion



        // POST: api/LogInDetails
        //public void Post([FromBody]string value)
        //{

        //}

        #region Token Validation Method

        //private static bool ValidateToken(string token, out string username)
        //{
        //    username = null;
        //    var simplePrinciple = GetPrincipal(token);
        //    var identity = simplePrinciple.Identity as ClaimsIdentity;

        //    if (identity == null)
        //        return false;

        //    if (!identity.IsAuthenticated)
        //        return false;

        //    var usernameClaim = identity.FindFirst(ClaimTypes.Name);
        //    username = usernameClaim?.Value;

        //    if (string.IsNullOrEmpty(username))
        //        return false;

        //    // More validate to check whether username exists in system

        //    return true;
        //}

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

        //public static ClaimsPrincipal GetPrincipal(string token)
        //{
        //    try
        //    {
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        //        if (jwtToken == null)
        //            return null;

        //        var symmetricKey = Convert.FromBase64String(Secret);

        //        var validationParameters = new TokenValidationParameters()
        //        {
        //            RequireExpirationTime = true,
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
        //        };

        //        SecurityToken securityToken;
        //        var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

        //        return principal;
        //    }
        //    catch (Exception xp)
        //    {
        //        //should write log
        //        return null;
        //    }
        //}

        #endregion

        // PUT: api/LogInDetails/5
        public HttpResponseMessage Put(int id, [FromBody]string value)
        {
            LoginDetailsRef loginref = new LoginDetailsRef();
            try
            {
                var authorization = Request.Headers.Authorization;
                string UserName = string.Empty;
                if (authorization == null || authorization.Scheme != "Bearer")
                    return Request.CreateResponse<LoginDetailsRef>(HttpStatusCode.BadRequest, loginref);

                if (string.IsNullOrEmpty(authorization.Parameter))
                {
                    // context.ErrorResult = new AuthenticationFailureResult("Missing Jwt Token", request);
                    return Request.CreateResponse<LoginDetailsRef>(HttpStatusCode.BadRequest, loginref);
                }

                var token = authorization.Parameter;

                bool strbool = CommonUseFunctionClass.ValidateToken(token, out UserName);

                if (strbool == true)
                {
                    loginref.Status = "Success";
                    return Request.CreateResponse<LoginDetailsRef>(HttpStatusCode.OK, loginref);
                }
                else
                {
                    loginref.Status = "Failure";
                    return Request.CreateResponse<LoginDetailsRef>(HttpStatusCode.BadRequest, loginref);
                }
            }
            catch (Exception xp)
            {
                loginref.Status = "Failure";
                return Request.CreateResponse<LoginDetailsRef>(HttpStatusCode.BadRequest, loginref);
            }
            finally
            {

            }
            return null;

        }

        // DELETE: api/LogInDetails/5
        public void Delete(int id)
        {
        }
    }
}
