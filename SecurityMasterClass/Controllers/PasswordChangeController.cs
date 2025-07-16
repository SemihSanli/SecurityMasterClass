using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SecurityMasterClass.Entities;
using SecurityMasterClass.Models.ForgotPassword;


namespace SecurityMasterClass.Controllers
{
    public class PasswordChangeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public PasswordChangeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);
            string passwordResetToken=await _userManager.GeneratePasswordResetTokenAsync(user); //KULLANICIYA RESET TOKEN SAĞLAR

            var passwordResetTokenLink = Url.Action("ResetPassword", "PasswordChange", new
            {
                userId = user.Id,
                token = passwordResetToken
            }, HttpContext.Request.Scheme);
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Notika Support", "YOUR_GOOGLE_ACCOUNT");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User",forgotPasswordViewModel.Email);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Şifre Sıfırlama Bağlantınız: " + passwordResetTokenLink;
            mimeMessage.Body=bodyBuilder.ToMessageBody();
            mimeMessage.Subject = "Şifre Kurtarma Talebi";
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("YOUR_GOOGLE_ACCOUNT", "YOUR_GOOGLE_API");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string userId,string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var userid = TempData["userId"];
            var token = TempData["token"];
            if (userid == null || token==null)
            {
                ViewBag.Error = "Bir Sorunla Karşılaşıldı";
            }
            var user = await _userManager.FindByIdAsync(userid.ToString());
            var result = await _userManager.ResetPasswordAsync(user, token.ToString(), resetPasswordViewModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Login");
            }
            return View();
        }
    }
}
