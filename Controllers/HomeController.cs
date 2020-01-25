using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        public IActionResult Index()
        {
            var empty = new UserSearchViewModel();
            return View(empty);
        }

        [HttpPost]
        public IActionResult Index(UserSearchViewModel model)
        {
            //model.UsersResult = userManager.Users.Where(
            //    t => t.UserName == model.UserNameSearch).ToList();

            model.UsersResult = (from b in userManager.Users 
                                 where b.UserName.Contains(model.UserNameSearch) 
                                 select b).ToList();


                return View(model);
        }
    }
}