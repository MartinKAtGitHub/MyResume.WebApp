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
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(UserManager<ApplicationUser> userManager )
        {
            this.userManager = userManager;
        }

        public IActionResult Index(string searchString)
        {
            var UserSearchResult = new UserSearchViewModel();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filterdUsers = userManager.Users.Where(
               t => t.UserName.Contains(searchString)).ToList();

                UserSearchResult.UsersResult = filterdUsers;
            }

            return View(UserSearchResult);
        }


        public IActionResult UserResume(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                ViewBag.ErrorTitle = "Can't find Resume ";
                @ViewBag.ErrorMessage = "No user is selected";
                return View("Error");
            }

            UserResumeViewModel model = new UserResumeViewModel();

            if(userManager.GetUserId(User) == id)
            {
                model.EnableEditing = true;
                //userResumeViewModel.UserInfo = Context.getInfo
                //userResumeViewModel.Achivemtns = Context.getallAchivements
                return View(model);
            }

            model.EnableEditing = false;
            //userResumeViewModel.UserInfo = Context.getInfo
            //userResumeViewModel.Achivemtns = Context.getallAchivements

            return View(model);
        }
    }
}