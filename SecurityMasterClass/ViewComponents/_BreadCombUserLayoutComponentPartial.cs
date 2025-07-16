using Microsoft.AspNetCore.Mvc;

namespace SecurityMasterClass.ViewComponents
{
    public class _BreadCombUserLayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
