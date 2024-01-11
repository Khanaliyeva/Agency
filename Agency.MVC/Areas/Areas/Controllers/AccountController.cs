using Microsoft.AspNetCore.Mvc;

namespace Agency.MVC.Areas.Areas.Controllers
{

    [Area("Manage")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }

}
