using Microsoft.AspNetCore.Mvc;
using MyResume.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.ViewComponents
{
    public class SkillEditDisplayViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Skill skill)
        {
            return View(skill);
        }
    }
}
