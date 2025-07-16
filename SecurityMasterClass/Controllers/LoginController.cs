using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecurityMasterClass.Context;
using SecurityMasterClass.Entities;
using SecurityMasterClass.Models;
using System.Security.Claims;

namespace SecurityMasterClass.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailContext _emailContext;

        public LoginController(SignInManager<AppUser> signInManager, EmailContext emailContext, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _emailContext = emailContext;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginViewModel userLoginViewModel)
        {

            var value = _emailContext.Users.Where(x => x.UserName == userLoginViewModel.Username).FirstOrDefault();
            if (value == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı Bulunamadı.");
                return View(userLoginViewModel);
            }
            if (!value.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Email Adresiniz Henüz Onaylanmamış.");
                return View(userLoginViewModel);
            }


            var result = await _signInManager.PasswordSignInAsync(userLoginViewModel.Username, userLoginViewModel.Password, true, true);
            if (result.Succeeded)
            {
                return RedirectToAction("EditProfile", "Profile");
            }
            ModelState.AddModelError(string.Empty, "Kullanıcı Adı Veya Şifre Yanlış");
            return View(userLoginViewModel);



        }


        [HttpGet]
        public IActionResult LoginWithGoogle()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ExternalLogin(string provider, string? returnURL = null)
        {
            var redirectURL = Url.Action("ExternalLoginCallBack", "Login", new { returnURL });
            // ConfigureExternalAuthenticationProperties dış sağlayıcıya yönlendirmeyi sağlar
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, returnURL);

            return Challenge(properties, provider);

        }
        
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallBack(string? returnURL = null, string? remoteError = null)
        {
            //returnURL Boş değilse aynı kalır fakat boşsa  Url.Content("~/"); burayı atar
            // ?? null durumu varsa belirtilen değeri ona ata anlamına gelir
            returnURL ??= Url.Content("~/");

            if(remoteError != null)
            {
                ModelState.AddModelError("", $"External Provider Error : {remoteError}");
                return RedirectToAction("SignIn");
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return RedirectToAction("SignIn");
            }
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,info.ProviderKey,false);

            if (result.Succeeded)
            {
                return RedirectToAction("Inbox","Message");
            }

            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var user = new AppUser()
                {
                    UserName = email,
                    Email = email,
                    Name=info.Principal.FindFirstValue(ClaimTypes.GivenName)?? "Google",
                    Surname = info.Principal.FindFirstValue(ClaimTypes.Surname) ?? "User"
                };
                var identityResult = await _userManager.CreateAsync(user);

                if (identityResult.Succeeded)
                {
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Inbox", "Message");
                }
                return RedirectToAction("SignIn");
            }

        }


    }
}
