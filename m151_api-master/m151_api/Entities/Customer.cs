using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace m151_api.Entities
{
    public class Customer
    {
        public string Email { get; set; }
        public int Number { get; set; }
        public string Surname { get; set; }
        public string Prename { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public bool IsPremiumMember { get; set; }
    }
}