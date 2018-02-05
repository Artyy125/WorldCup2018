﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldCup2018.EFModel;
using WorldCup2018.Models;

namespace WorldCup2018.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private WorldcupDbContext _db;
        private ApplicationDbContext _appDb;
        public HomeController()
        {
            _db = new WorldcupDbContext();
            _appDb = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            TempData["InsertedResult"] = "";
            GetData data = GetTeamData();
            return View(data);
        }
        [HttpPost]
        public ActionResult Index(FormCollection results)
        {
            try
            {
                TempData["InsertedResult"] = "";
                string userName = User.Identity.Name;
                foreach (var item in results)
                {
                    string[] t = results[item.ToString()].ToString().Split(',');
                    if (!string.IsNullOrEmpty(t[0]) && !string.IsNullOrEmpty(t[1]))
                    {
                        UserInput ur = new UserInput();
                        ur.UserName = userName;
                        ur.MatchId = Int32.Parse(item.ToString());
                        ur.Team1Score = Int32.Parse(t[0].ToString());
                        ur.Team2Score = Int32.Parse(t[1].ToString());
                        int matchId = Int32.Parse(item.ToString());
                        var insertedData = _db.UserInputs.Where(r => r.UserName == userName && r.MatchId == matchId).FirstOrDefault();
                        if (insertedData == null)
                        {
                            _db.UserInputs.Add(ur);
                        }
                        else
                        {
                            insertedData.Team1Score = Int32.Parse(t[0].ToString());
                            insertedData.Team2Score = Int32.Parse(t[1].ToString());
                        }
                    }   
                }
                _db.SaveChanges();
                GetData data = GetTeamData();
                TempData["InsertedResult"] = "Your data is updated!";
                return View(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private GetData GetTeamData()
        {
            string userName = User.Identity.Name;
            List<Teams> teams = _db.Matches.Where(r => r.MatchDateTime > DateTime.Now).Select(r => new Teams
            {
                Team1 = r.Team1,
                Team2 = r.Team2,
                Team1LogoUrl = r.Team1FlagUrl,
                Team2LogoUrl = r.Team2FlagUrl,
                Team1Score = r.Team1Score.Value,
                Team2Score = r.Team2Score.Value,
                MatchDate = r.MatchDateTime,
                Id = r.Id
            }).ToList();
            GetData data = new GetData();
            data.Teams = teams;
            var result = _db.UserInputs.Where(r => r.UserName == userName).ToList();
            if (result?.Count() > 0)
            {
                data.Teams.ForEach(sc =>
                {
                    var oc = result.FirstOrDefault(obj => obj.MatchId == sc.Id);
                    if (oc != null)
                    {
                        sc.Team1Score = oc.Team1Score;
                        sc.Team2Score = oc.Team2Score;
                    }
                });
            }
            
            return data;
        }
        public ActionResult Ranking()
        {
            string userName = User.Identity.Name;
            List<Teams> teams = _db.Matches.Where(r => r.MatchDateTime > DateTime.Now).Select(r => new Teams
            {
                Team1 = r.Team1,
                Team2 = r.Team2,
                Team1LogoUrl = r.Team1FlagUrl,
                Team2LogoUrl = r.Team2FlagUrl,
                Team1Score = r.Team1Score.Value,
                Team2Score = r.Team2Score.Value,
                Winner = r.Winner,
                Id = r.Id
            }).ToList();
            List<string> AllUsers = _db.UserInputs.Select(r => r.UserName).Distinct().ToList();
            List<Ranking> rankingList = new List<Ranking>();
            foreach (var user in AllUsers)
            {
                var results = _db.UserInputs.Where(r => r.UserName == user).ToList();
                Ranking rank = new Ranking();
                foreach (var result in results)
                {
                    var RealResult = teams.Where(r => r.Id == result.MatchId).FirstOrDefault();
                    if (RealResult != null)
                    {
                        if (RealResult.Team1Score != null && RealResult.Team2Score != null)
                        {
                            if (RealResult.Team1Score == result.Team1Score && RealResult.Team2Score == result.Team2Score)
                            {
                                rank.SevenPoints += 1;
                                rank.Score += 7;
                            }
                            else if (RealResult.Team1Score - RealResult.Team2Score == result.Team1Score - result.Team2Score)
                            {
                                rank.FivePoints += 1;
                                rank.Score += 5;
                            }
                            else if ((RealResult.Team1Score > RealResult.Team2Score && result.Team1Score > result.Team2Score) || (RealResult.Team1Score < RealResult.Team2Score && result.Team1Score < result.Team2Score))
                            {
                                if (RealResult. Team1Score == result.Team1Score || RealResult.Team2Score == result.Team2Score)
                                {
                                    rank.ThreePoints += 3;
                                    rank.Score += 3;
                                }
                                else
                                {
                                    rank.ThreePoints += 1;
                                    rank.Score += 1;
                                }
                            }
                            else
                            {

                            }

                        }
                    }
                }
                var userInfo = _appDb.Users.Where(r => r.UserName == user).FirstOrDefault();
                rank.FirstName = userInfo.FirstName;
                rank.LastName = userInfo.LastName;
                rankingList.Add(rank);
            }
            return View(rankingList);
        }

        public ActionResult Results()
        {
            GetData data = GetTeamData();
            return View(data);
        }
    }
}