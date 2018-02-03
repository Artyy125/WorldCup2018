using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldCup2018.EFModel;
using WorldCup2018.Models;

namespace WorldCup2018.Controllers
{
    public class HomeController : Controller
    {
        private WorldcupDbContext _db;
        public HomeController()
        {
            _db = new WorldcupDbContext();
        }
        public ActionResult Index()
        {
            List<Teams> teams = _db.Matches.Where(r => r.MatchDateTime > DateTime.Now).Select(r => new Teams
            {
                Team1 = r.Team1,
                Team2 = r.Team2,
                Team1LogoUrl = r.Team1FlagUrl,
                Team2LogoUrl = r.Team2FlagUrl,
                Id=r.Id
            }).ToList();
            GetData data = new GetData();
            data.Teams = teams;
            return View(data);
        }
        [HttpPost]
        public ActionResult Index(FormCollection results)
        {
            try
            {
                string userName = User.Identity.Name;
                foreach (var item in results)
                {
                    string t = results[item.ToString()].ToString();
                }
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}