using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginRef
    {
        public Login LogInData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
        public string Access_Token { get; set; }
    }

}