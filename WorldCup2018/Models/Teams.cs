using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCup2018.Models
{
    public class Teams
    {
        public int Id { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string Team1LogoUrl { get; set; }
        public string Team2LogoUrl { get; set; }
        public int? Team1Score { get; set; }
        public int? Team2Score { get; set; }
        public string Winner { get; set; }
        public DateTime MatchDate { get; set; }
    }
}