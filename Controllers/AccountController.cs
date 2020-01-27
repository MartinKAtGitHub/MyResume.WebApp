using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyResume.WebApp.Models;
using MyResume.WebApp.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet][HttpPost]
        public async Task<IActionResult> IsUserNameInUse(string userName) // Server side method. Not a view/webpage. Used for instant Validation
        {
            var user = await _userManager.FindByNameAsync(userName);

            if(user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"User name {userName} is taken");
            }

        }

        [HttpGet][HttpPost]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is taken");
            }

        }

       [HttpPost] // to avoid malicious attempts use Post
       [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        //[AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && !user.EmailConfirmed && (await _userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName,
                                     model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }

                if (result.IsLockedOut)
                {
                    ViewBag.LockOutTime = user.LockoutEnd.Value.LocalDateTime.ToString("HH:mm:ss");
                    //ViewBag.LockOutTime = (user.LockoutEnd - DateTime.Now).Value.Duration(); // gives me 2 min cuz it ticks down from 3
                    // var LockoutEnd = user.LockoutEnd.Value.Subtract(user.LockoutEnd.Value);
                    return View("AccountLocked");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,

                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
                    //await messageService.SendEmailAsync(user.UserName, user.Email, "Email confirmation", confirmationLink);

                    //if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    //{
                    //    return RedirectToAction("ListUsers", "Administration");
                    //}

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors) // This will be added to the asp-validation-summary
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
    }
}
