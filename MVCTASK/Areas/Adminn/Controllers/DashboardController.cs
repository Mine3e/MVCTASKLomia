using Microsoft.AspNetCore.Mvc;

namespace MVCTASK.Areas.Adminn.Controllers
{
    public class DashboardController : Controller
    {
        [Area("Adminn")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
