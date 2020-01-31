using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyResume.WebApp.Models;
using MyResume.WebApp.ModelView;

namespace MyResume.WebApp.Controllers
{

    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserInfoRepo _userInfoRepo;

        public HomeController(UserManager<ApplicationUser> userManager, IUserInfoRepo userInfoRepo)
        {
            _userManager = userManager;
            _userInfoRepo = userInfoRepo;
        }

        public IActionResult Index(string searchString)
        {
            var UserSearchResult = new UserSearchViewModel();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filterdUsers = _userManager.Users.Where(
               t => t.UserName.Contains(searchString)).ToList();

                UserSearchResult.UsersResult = filterdUsers;
            }

            return View(UserSearchResult);
        }

        public IActionResult UserResume(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.ErrorTitle = "Can't find Resume ";
                @ViewBag.ErrorMessage = "No user is selected";
                return View("Error");
            }

            var model = new UserResumeViewModel
            {
                UserInfo = _userInfoRepo.Read(id)
            };

            if (_userManager.GetUserId(User) == id)
            {
                model.EnableOwnerOptions = true;
                //userResumeViewModel.UserInfo = Context.getInfo
                //userResumeViewModel.Achivemtns = Context.getallAchivements
                return View(model);
            }

            model.EnableOwnerOptions = false;
            //userResumeViewModel.UserInfo = Context.getInfo
            //userResumeViewModel.Achivemtns = Context.getallAchivements

            return View(model);
        }


        [HttpGet]
        [Authorize]
        public IActionResult EditUserInfo()
        {
            var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));

            // For sec reason it is recommended we create a ViewModel only exposing the properties we want to edit
            var model = new EditUserInfoViewModel
            {
                Summary = userInfo.Summary,
                MainText = userInfo.MainText,
                AvailableForContact = userInfo.AvailableForContact
            };

            return View(model);
        }


        [HttpPost]
        [Authorize]
        public IActionResult EditUserInfo(EditUserInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userInfo = _userInfoRepo.Read(_userManager.GetUserId(User));

                userInfo.Summary = model.Summary;
                userInfo.MainText = model.MainText;
                userInfo.AvailableForContact = model.AvailableForContact;

                _userInfoRepo.Update(userInfo);
                return View(model);
            }

            return View();
        }
    }
}