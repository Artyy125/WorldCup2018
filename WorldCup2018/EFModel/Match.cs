using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorldCup2018.EFModel
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public int? Team1Score { get; set; }
        public int? Team2Score { get; set; }
        public int? Team1PenaltyScore { get; set; }
        public int? Team2PenaltyScore { get; set; }
        public string Winner { get; set; }
        public string Team1FlagUrl { get; set; }
        public string Team2FlagUrl { get; set; }
        public DateTime MatchDateTime { get; set; }
    }
}