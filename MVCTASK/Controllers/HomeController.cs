using Data.DAL;
using Microsoft.AspNetCore.Mvc;
using MVCTASK.Models;
using System.Diagnostics;

namespace MVCTASK.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContxt;
        public HomeController (AppDbContext dbContxt)
        {
            _dbContxt = dbContxt;
        }

        public IActionResult Index()
        {
            var  teams = _dbContxt.Teams.ToList();
            return View(teams);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
