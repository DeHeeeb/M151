using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace m151_api.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public Organisation Organisation { get; set; }
        public string Name { get; set; }
        public int Tariff { get; set; }
        public string Age { get; set; }
        public Publisher Publisher { get; set; }
        public bool IsAvailable { get; set; }
    }
}