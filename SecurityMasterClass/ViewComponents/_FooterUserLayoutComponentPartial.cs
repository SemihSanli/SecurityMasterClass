using Microsoft.AspNetCore.Mvc;

namespace SecurityMasterClass.ViewComponents
{
    public class _FooterUserLayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
