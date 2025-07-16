using Microsoft.AspNetCore.Mvc;

namespace SecurityMasterClass.ViewComponents
{
    public class _MobileMenuUserLayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
