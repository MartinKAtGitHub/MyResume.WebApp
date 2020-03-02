using MyResume.WebApp.Models;
using MyResume.WebApp.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Data
{
    public interface IAchievementRepo
    {
        Achievement Create(Achievement model);
        Achievement Read(Guid id);
        IEnumerable<Achievement> ReadAll(Guid userInfoId);
        Achievement Update(Achievement newAchievement);
        Achievement Delete(Achievement id);

    }
}
