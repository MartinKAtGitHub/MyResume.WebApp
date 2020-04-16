using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyResume.WebApp.Data;
using MyResume.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.ViewComponents
{
    public class ExperienceDisplayViewComponent : ViewComponent
    {
        private readonly IExperienceRepo _experienceRepo;
        private readonly IUserInfoRepo _userInfoRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExperienceDisplayViewComponent (IExperienceRepo experienceRepo,IUserInfoRepo userInfoRepo ,UserManager<ApplicationUser> userManager)
        {
            _experienceRepo = experienceRepo;
            _userInfoRepo = userInfoRepo;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(Guid userId)
        {
            var activeUserId = _userManager.GetUserId(UserClaimsPrincipal);


            if ( _userInfoRepo.Read(activeUserId).UserInformationId != userId) // TODO ExperienceDisplayViewComponent i am making an extra trip to the DB because i use userinfoId instead of appuserId 
            {
               // Response.StatusCode = 403;
                //ViewBag.ErrorTitle = "Wrong user";
                //ViewBag.ErrorMessage = "Please login with the correct user to create this experience section";
                 // Ideally i would redirect them but i am not sure if it is the correct way considering this is a view comp

                return View(new List<Experience>());
            }


            var result = _experienceRepo.ReadAll(userId).ToList();
            //result.Add(new Models.Experience { Title = message});
            return View(result);
        }
    }
}
