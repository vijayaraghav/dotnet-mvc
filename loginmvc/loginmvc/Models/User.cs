using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loginmvc.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "invalid email address")]
        [Required(ErrorMessage ="this field is required --- Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "this field is required ---  pass")]
        public string Password { get; set; }
        public string Mobile { get; set; }

        [Required(ErrorMessage = "this field is required")]
        public string otp { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [Required(ErrorMessage = "this field is required")]
        public string confirmPassword { get; set; }
        public byte fileByte { get; set; }
    }

    public class userdetails
    {
        public static string uname { get; set; }
    }

    public class userfile
    {
        public string UserId { get; set; }
        public string file1 { get; set; }
        public string file2 { get; set; }
        public string file3 { get; set; }
    }
}