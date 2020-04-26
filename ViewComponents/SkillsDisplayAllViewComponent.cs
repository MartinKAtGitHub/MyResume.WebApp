using Microsoft.AspNetCore.Mvc;
using MyResume.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.ViewComponents
{
    public class SkillsDisplayAllViewComponent : ViewComponent
    {
        private readonly ISkillRepo _skillRepo;

        public SkillsDisplayAllViewComponent(ISkillRepo skillRepo)
        {
            _skillRepo = skillRepo;
        }

        public IViewComponentResult Invoke(string appUserId)
        {
            var allSkills = _skillRepo.ReadAll(appUserId).ToList();
            return View(allSkills);
        }
    }

    
}
