using Microsoft.AspNetCore.Mvc;
using MyResume.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.ViewComponents
{
    public class ExperienceDisplayViewComponent : ViewComponent
    {
        private readonly IExperienceRepo _experienceRepo;

        public ExperienceDisplayViewComponent (IExperienceRepo experienceRepo)
        {
            _experienceRepo = experienceRepo;
        }

        public IViewComponentResult Invoke()
        {
           
            var result = _experienceRepo.ReadAll(Guid.Parse("208a8d57-229e-41cf-7bfd-08d7e07c732b")).ToList();
            //result.Add(new Models.Experience { Title = message});
            return View(result);
        }
    }
}
