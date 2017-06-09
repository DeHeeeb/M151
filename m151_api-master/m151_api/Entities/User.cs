using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace m151_api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public Vice Vice { get; set; }
        public OrganisationLeader OrganisationLeader { get; set; }
    }
}