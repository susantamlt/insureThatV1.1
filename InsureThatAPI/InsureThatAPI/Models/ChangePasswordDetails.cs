using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class ChangePasswordDetails
    {

        public int? UserId{ get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "New Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string RePassword { get; set; }
        public string EncryptedPassword { get; set; }
    }

    public class ChangePasswordDetailsRef
    {
        public ChangePasswordDetails ChangePWDData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
        public string EncrptedPassword { get; set; }
    }

}