using System;
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
                GetData dataCheck = GetTeamData();
                foreach (var item in results)
                {
                    DateTime matchDate = dataCheck.Teams.Where(r => r.Id == Convert.ToInt32(item)).Select(r => r.MatchDate).FirstOrDefault();
                    string[] t = results[item.ToString()].ToString().Split(',');
                    if (!string.IsNullOrEmpty(t[0]) && !string.IsNullOrEmpty(t[1]) && matchDate.ToUniversalTime() >= DateTime.UtcNow)
                    {
                        UserInput ur = new UserInput();
                        ur.UserName = userName;
                        ur.MatchId = Int32.Parse(item.ToString());
                        ur.Team1Score = Int32.Parse(t[0].ToString());
                        ur.Team2Score = Int32.Parse(t[1].ToString());
                        ur.DateTime = DateTime.Now;
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
                            insertedData.DateTime = DateTime.Now;
                        }
                        _db.SaveChanges();
                        TempData["InsertedResult"] = "Your data is updated!";
                    }
                }
                GetData data = GetTeamData();
                return View(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private GetData GetTeamData(bool isResult=false)
        {
            string userName = User.Identity.Name;
            List<Teams> teams = _db.Matches.Select(r => new Teams
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
            if (isResult== false)
            {
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
                        else
                        {
                            sc.Team1Score = null;
                            sc.Team2Score = null;
                        }
                    });
                }
            }
            
            
            return data;
        }
        public ActionResult Ranking()
        {
            string userName = User.Identity.Name;
            List<Teams> teams = _db.Matches.Select(r => new Teams
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
                                if (RealResult.Team1Score == result.Team1Score || RealResult.Team2Score == result.Team2Score)
                                {
                                    rank.ThreePoints += 1;
                                    rank.Score += 3;
                                }
                                else
                                {
                                    rank.OnePoints += 1;
                                    rank.Score += 1;
                                }
                            }
                            else
                            {
                                rank.NoPoint += 1;
                            }

                        }
                    }
                }
                var userInfo = _appDb.Users.Where(r => r.UserName == user).FirstOrDefault();
                rank.FirstName = userInfo.FirstName;
                rank.LastName = userInfo.LastName;
                rankingList.Add(rank);
                rankingList = rankingList.OrderByDescending(r => r.Score).ToList();
            }
            return View(rankingList);
        }

        public ActionResult Results()
        {
            GetData data = GetTeamData(true);
            return View(data);
        }
    }
}