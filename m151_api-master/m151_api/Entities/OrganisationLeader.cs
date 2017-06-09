using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace m151_api.Entities
{
    public class OrganisationLeader
    {
        public int Id { get; set; }
        public int EmployeeNumber { get; set; }
        public Organisation Organisation { get; set; }
        public string Surname { get; set; }
        public string Prename { get; set; }
        public bool IsChairman { get; set; }
        public bool IsBoard { get; set; }
    }
}