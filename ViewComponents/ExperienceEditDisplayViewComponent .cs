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
    public class ExperienceEditDisplayViewComponent : ViewComponent // Experience Edit
    {
        private readonly IExperienceRepo _experienceRepo;
        private readonly IUserInfoRepo _userInfoRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExperienceEditDisplayViewComponent(IExperienceRepo experienceRepo, IUserInfoRepo userInfoRepo, UserManager<ApplicationUser> userManager)
        {
            _experienceRepo = experienceRepo;
            _userInfoRepo = userInfoRepo;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(Guid userInfoId)
        {

            // ! this code to check if the person viewing is the owner is unnecessary because we just render another VC based on the bool isOWNER when loading in the resume page
            //var activeUserId = _userManager.GetUserId(UserClaimsPrincipal);
            //if (_userInfoRepo.Read(activeUserId).UserInformationId != userInfoId) // TODO ExperienceDisplayViewComponent i am making an extra trip to the DB because i use userinfoId instead of appuserId 
            //{
            //    // Response.StatusCode = 403;
            //    //ViewBag.ErrorTitle = "Wrong user";
            //    //ViewBag.ErrorMessage = "Please login with the correct user to create this experience section";
            //    // Ideally i would redirect them but i am not sure if it is the correct way considering this is a view comp

            //    return View(new List<Experience>());
            //}


            var result = _experienceRepo.ReadAll(userInfoId).ToList();
            return View(result);
        }
    }
}
