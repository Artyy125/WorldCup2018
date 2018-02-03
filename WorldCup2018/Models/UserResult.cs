using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCup2018.Models
{
    public class UserResult
    {
        public int MatchId { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public string Winner { get; set; }
        public string UserName { get; set; }
    }
}