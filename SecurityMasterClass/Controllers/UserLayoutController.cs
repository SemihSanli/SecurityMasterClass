using Microsoft.AspNetCore.Mvc;

namespace SecurityMasterClass.Controllers
{
    public class UserLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
