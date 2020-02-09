using MyResume.WebApp.Models;
using MyResume.WebApp.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Data
{
    interface IAchievementRepo
    {
        Achievement Create(AchivementViewModel model, UserInformation userInfo);
        Achievement Read(string Id);
        Achievement Update(Achievement newAchievement);
        Achievement Delete(string id);
    }
}
