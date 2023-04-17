using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegistrationAndLogin.Models
{
    public class UserLogin
    {
         [Display(Name ="Student ID")]
         [Required(AllowEmptyStrings =false, ErrorMessage ="Email ID required")]
         public string EmailID { get; set; } 

      /*  [Display(Name = "Student ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Student ID required")]
        public string StudentID { get; set; } */

        [Required(AllowEmptyStrings =false, ErrorMessage ="Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}