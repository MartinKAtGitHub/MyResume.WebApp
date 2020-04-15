using MyResume.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Data
{
    public class ExperienceRepoSQL : IExperienceRepo
    {
        private readonly AppDbContext _appDbContext;

        public ExperienceRepoSQL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Experience Create(Experience model)
        {

            _appDbContext.Experiences.Add(model);
            _appDbContext.SaveChanges();
            return model;
        }

        public Experience Delete(Experience experienceToDelete)
        {
            throw new NotImplementedException();
        }

        public Experience Read(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Experience> ReadAll(Guid userId)
        {
            var result = _appDbContext.Experiences.Where(x => x.UserInformationId == userId);
            return result;
            //throw new NotImplementedException();
        }

        public Experience Update(Experience newAchievement)
        {
            throw new NotImplementedException();
        }
    }
}
