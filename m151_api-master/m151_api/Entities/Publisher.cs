using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace m151_api.Entities
{
    public class Publisher
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Zip { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
    }
}