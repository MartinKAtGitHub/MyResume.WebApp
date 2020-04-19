using Microsoft.AspNetCore.Mvc;
using MyResume.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.ViewComponents
{
    public class ExperienceDataDisplayViewComponent: ViewComponent
    {
        private readonly IExperienceRepo _experienceRepo;

        public ExperienceDataDisplayViewComponent(IExperienceRepo experienceRepo)
        {
            _experienceRepo = experienceRepo;
        }

        public IViewComponentResult Invoke(Guid userInfoId)
        {
            var result = _experienceRepo.ReadAll(userInfoId).ToList();
            return View(result);
        }
    }
}
