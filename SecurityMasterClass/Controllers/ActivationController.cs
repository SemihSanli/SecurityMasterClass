using Microsoft.AspNetCore.Mvc;
using SecurityMasterClass.Context;

namespace SecurityMasterClass.Controllers
{
    public class ActivationController : Controller
    {
        private readonly EmailContext _emailContext;

        public ActivationController(EmailContext emailContext)
        {
            _emailContext = emailContext;
        }

        [HttpGet]
        public IActionResult UserActivation()
        {
            var email = TempData["EmailKEY"];
            TempData["code"] = email;
            return View();
        }
        [HttpPost]
        public IActionResult UserActivation(int userCodeParameter)
        {
            string email = TempData["code"].ToString();

            var code = _emailContext.Users.Where(x=>x.Email==email).Select(y=>y.ActivationCode).FirstOrDefault();

            if (userCodeParameter == code)
            {
                var value = _emailContext.Users.Where(x => x.Email == email).FirstOrDefault();
                value.EmailConfirmed = true;
                _emailContext.SaveChanges();
                return RedirectToAction("SignIn", "Login");
            }

            return View("Onay Kodu Hatalı !!!");

        }
    }
}
