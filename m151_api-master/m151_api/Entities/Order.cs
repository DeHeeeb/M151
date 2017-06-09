using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace m151_api.Entities
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public Publisher Publisher { get; set; }
    }
}