using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SSLS.WebUI.Models
{
    public class LoginViewModel
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Admin { get; set; }
     
    }
}