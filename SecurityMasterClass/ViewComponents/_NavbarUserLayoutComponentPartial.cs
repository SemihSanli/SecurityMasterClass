using Microsoft.AspNetCore.Mvc;

namespace SecurityMasterClass.ViewComponents
{
    public class _NavbarUserLayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
