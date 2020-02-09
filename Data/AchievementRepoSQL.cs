using MyResume.WebApp.Models;
using MyResume.WebApp.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Data
{
    public class AchievementRepoSQL : IAchievementRepo
    {
        private readonly AppDbContext _appDbContext;
        
        public AchievementRepoSQL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Achievement Create(AchivementViewModel model, UserInformation userInfo) // Can make Async
        {
            var newAchievement = new Achievement
            {
                UserInformationId = userInfo.UserInformationId,
                
                Title = model.Title,
                Summary = model.Summary,
                MainText = model.MainText,
                OrderPosition = model.OrderPosition,
                EnableComments = model.EnableComments,
                EnableRating = model.EnableRating
            };

            _appDbContext.Achievements.Add(newAchievement);
            _appDbContext.SaveChanges();


            return newAchievement;

        }

        public Achievement Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Achievement Read(string Id)
        {
            throw new NotImplementedException();
        }

        public Achievement Update(Achievement newAchievement)
        {
            throw new NotImplementedException();
        }
    }
}
