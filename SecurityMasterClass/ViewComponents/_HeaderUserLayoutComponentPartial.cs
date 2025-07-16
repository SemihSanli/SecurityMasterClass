using Microsoft.AspNetCore.Mvc;

namespace SecurityMasterClass.ViewComponents
{
    public class _HeaderUserLayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
