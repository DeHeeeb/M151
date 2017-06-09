using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace m151_api.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public Organisation Organisation { get; set; }
        public string Surname { get; set; }
        public string Prename { get; set; }
    }
}