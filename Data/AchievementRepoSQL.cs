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
            _appDbContext.Achievements.Add(newAchievement);
            _appDbContext.SaveChanges();

            return newAchievement;
        }

        public Achievement Delete(Achievement achievement)
        {
                _appDbContext.Remove(achievement);
                _appDbContext.SaveChanges();
            
            return achievement;
        }

        public Achievement Read(Guid id)
        {
            var item = _appDbContext.Achievements.Include(achievement => achievement.ItemGalleryImageFilePaths).FirstOrDefault(achievement => id == achievement.AchievementId);
            item.ItemGalleryImageFilePaths = item.ItemGalleryImageFilePaths.OrderBy(x => x.GalleryIndex).ToList();
            return item;
        }

        /// <summary>
        /// Returns a sorted list of all the items the current user has, the list is sorted based on order position row. Lower value higher position
        /// </summary>
        public IEnumerable<Achievement> ReadAll(Guid userInfoId)
        {
            // Just testing this style of LINQ
            var QueryResult = 
                        from a in _appDbContext.Achievements.Include(x => x.ItemGalleryImageFilePaths)
                        where a.UserInformationId == userInfoId
                        select a;

            foreach (var item in QueryResult)
            {
                item.ItemGalleryImageFilePaths = item.ItemGalleryImageFilePaths.OrderBy(x => x.GalleryIndex).ToList();
            }


            return (QueryResult.OrderBy(x => x.OrderPosition)).ToList();
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
