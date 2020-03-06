using Microsoft.EntityFrameworkCore;
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

        public Achievement Create(Achievement newAchievement) // Can make Async
        {
           
            if(newAchievement.ThumbnailImgPath == null)
            {
                newAchievement.ThumbnailImgPath = "~/images/ThumbnailDefault.png";
            }

            _appDbContext.Achievements.Add(newAchievement);
            _appDbContext.SaveChanges();

            return newAchievement;
        }

        public Achievement Delete(Achievement achievement)
        {
            if (achievement != null)
            {
                _appDbContext.Remove(achievement);
                _appDbContext.SaveChanges();
            }
            return achievement;
        }

        public Achievement Read(Guid id)
        {
            return _appDbContext.Achievements.Include(achievement => achievement.itemGalleryImageFilePaths).SingleOrDefault(achievement=> id == achievement.AchievementId);
        }

        public IEnumerable<Achievement> ReadAll(Guid userInfoId)
        {
            // Just testing this style of LINQ
            var QueryResult = 
                        from a in _appDbContext.Achievements
                        where a.UserInformationId == userInfoId
                        select a;

            return QueryResult.ToList();
        }

        
        public Achievement Update(Achievement itemUpdates)
        {
            var item = _appDbContext.Achievements.Attach(itemUpdates);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _appDbContext.SaveChanges();
            return itemUpdates;
        }
    }
}
