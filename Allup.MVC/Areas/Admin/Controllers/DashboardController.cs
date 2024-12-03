using Microsoft.AspNetCore.Mvc;

namespace Allup.MVC.Areas.Admin.Controllers
{
    public class DashboardController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
