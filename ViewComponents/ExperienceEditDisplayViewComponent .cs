using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyResume.WebApp.Data;
using MyResume.WebApp.Models;
using MyResume.WebApp.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MyResume.WebApp.ModelView.ExpViewModel;

namespace MyResume.WebApp.ViewComponents
{
    public class ExperienceEditDisplayViewComponent : ViewComponent // Experience Edit
    {
        private readonly IExperienceRepo _experienceRepo;
        // private readonly IUserInfoRepo _userInfoRepo;
        //  private readonly UserManager<ApplicationUser> _userManager;

        public ExperienceEditDisplayViewComponent(IExperienceRepo experienceRepo /*, IUserInfoRepo userInfoRepo, UserManager<ApplicationUser> userManager*/)
        {
            _experienceRepo = experienceRepo;
            //  _userInfoRepo = userInfoRepo;
            // _userManager = userManager;
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


            var experienceList = _experienceRepo.ReadAll(userInfoId).ToList();
            var model = new List<ExpViewModel>();


            for (int i = 0; i < experienceList.Count; i++)
            {
                model.Add(new ExpViewModel()
                {
                    Id = experienceList[i].Id,
                    Title = experienceList[i].Title,
                    //StartDate = experience.StartDate,
                    //EndDate = experience.EndDate,
                    ExpPoints = new List<ExpPoint>()
                });

                for (int j = 0; j < experienceList[i].ExperiencePoints.Count; j++)
                {
                    model[i].ExpPoints.Add(new ExpPoint()
                    {
                        Id = experienceList[i].ExperiencePoints[j].Id,
                        PointTitle = experienceList[i].ExperiencePoints[j].Title,
                        //StartDate = experience.StartDate,
                        //EndDate = experience.EndDate,
                        Descriptions = new List<Descriptions>()

                    });

                    for (int k = 0; k < experienceList[i].ExperiencePoints[j].Descriptions.Count; k++)
                    {
                        model[i].ExpPoints[j].Descriptions.Add(new Descriptions()
                        {
                            Desc = experienceList[i].ExperiencePoints[j].Descriptions[k].Discription
                        });
                    }
                }
            }

            //return View(experienceList);
            return View(model);
        }
    }
}
