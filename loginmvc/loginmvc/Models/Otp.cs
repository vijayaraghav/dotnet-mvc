using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loginmvc.Models
{
    public class Otp
    {
        public string otpId { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string otp { get; set; }
        public int UserId { get; set; }

    }
}