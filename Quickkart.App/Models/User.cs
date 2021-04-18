using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Quickkart.App.Models
{
    public class User
    {
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Mobile { get; set; }
        [ScaffoldColumn(false)]
        public int RoleId { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}