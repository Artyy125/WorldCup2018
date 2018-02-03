﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorldCup2018.EFModel
{
    public class UserInput
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int MatchId { get; set; }
        public int? Team1Score { get; set; }
        public int? Team2Score { get; set; }
        public string Winner { get; set; }
    }
}