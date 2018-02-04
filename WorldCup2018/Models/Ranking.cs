using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCup2018.Models
{
    public class Ranking
    {
        public int Score { get; set; }
        public int SevenPoints { get; set; }
        public int FivePoints { get; set; }
        public int ThreePoints { get; set; }
        public int OnePoints { get; set; }
        public int Win { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}