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

        public Achievement Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Achievement Read(Guid id)
        {
            return _appDbContext.Achievements.Find(id); // This works on all the Unique columns like id Username Email
        }

        public IEnumerable<Achievement> ReadAll(Guid userInfoId)
        {
            var QueryResult = 
                        from a in _appDbContext.Achievements
                        where a.UserInformationId == userInfoId
                        select a;

            return QueryResult.ToList();
        }

        
        public Achievement Update(Achievement newAchievement)
        {
            throw new NotImplementedException();
        }
    }
}
