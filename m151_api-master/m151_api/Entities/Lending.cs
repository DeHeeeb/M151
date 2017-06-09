using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace m151_api.Entities
{
    public class Lending
    {
        public int Number { get; set; }
        public Customer Customer { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<Game> Games { get; set; }
    }
}