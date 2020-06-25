using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;



namespace fypPromolac.Models
{
    public class passwordChange
    {

        [Required(ErrorMessage = "Password is required.")]
        [DisplayName("Current Password:")]
        public string password { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DisplayName("New Password:")]
        public string newPassword { get; set; }

        
        [Compare("newPassword", ErrorMessage = "Password and Confirmation Password must match.")]
        [DisplayName("Confirm Password:")]
        public string confirmPassword { get; set; }
    }
}