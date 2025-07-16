using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SecurityMasterClass.Entities;
using SecurityMasterClass.Models;

namespace SecurityMasterClass.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel registerViewModel)
        {
            Random rnd = new Random();
            int code = rnd.Next(100000, 1000000);
            AppUser appUser = new AppUser()
            {
                Name = registerViewModel.Name,
                Email = registerViewModel.Email,
                Surname = registerViewModel.Surname,
                UserName = registerViewModel.Username,
                ActivationCode=code
            };
            var result = await _userManager.CreateAsync(appUser, registerViewModel.Password);
            if (result.Succeeded)
            {
                MimeMessage mimeMessage = new MimeMessage();
                MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "YOUR_GOOGLE_ACCOUNT");
                mimeMessage.From.Add(mailboxAddressFrom);
                MailboxAddress mailboxAddressTo = new MailboxAddress("User", registerViewModel.Email);
                mimeMessage.To.Add(mailboxAddressTo);
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "Hesabınızı Doğrulamak İçin Olan Aktivasyon Kodu : " + code;
                mimeMessage.Body=bodyBuilder.ToMessageBody();

                mimeMessage.Subject = "Notika Identity Aktivasyon Kodu: ";
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Connect("smtp.gmail.com", 587, false);
                smtpClient.Authenticate("YOUR_GOOGLE_ACCOUNT", "YOUR_GOOGLE_API");
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);

                TempData["EmailKEY"] = registerViewModel.Email;
                return RedirectToAction("UserActivation", "Activation");
            }
            else
            {
                //İşlem başarısız olursa hatayı ekrana dönecektir
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View();
        }
    }
}
