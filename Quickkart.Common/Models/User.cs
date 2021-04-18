using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickkart.Common.Models
{
    public class User
    {
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public int RoleId { get; set; } = 0;
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
