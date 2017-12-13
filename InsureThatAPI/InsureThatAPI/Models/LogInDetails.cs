using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class LogInDetails
    {
        [Required(ErrorMessage = "User Name is required.")]
       
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string EncryptedPassword { get; set; }
        public string apiKey { get; set; }
    }
    public class LoginDetailsRef
    {
        public LogInDetails LogInData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
        public string Access_Token { get; set; }
    }

}