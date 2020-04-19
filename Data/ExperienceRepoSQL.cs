using Microsoft.EntityFrameworkCore;
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

        public Experience DeleteExp(Experience experienceToDelete)
        {

            _appDbContext.Remove(experienceToDelete);
            _appDbContext.SaveChanges();
            return experienceToDelete;
        }

        public ExperiencePoint DeleteExpPoint(ExperiencePoint experiencePointToDelete)
        {

            _appDbContext.Remove(experiencePointToDelete);
            _appDbContext.SaveChanges();
            return experiencePointToDelete;
        }

        public ExperiencePointDescription DeleteExpPointDesc(ExperiencePointDescription experiencePointDescToDelete)
        {

            _appDbContext.Remove(experiencePointDescToDelete);
            _appDbContext.SaveChanges();
            return experiencePointDescToDelete;
        }


        public Experience Read(string id)
        {
            var result = _appDbContext.Experiences.Include(x => x.ExperiencePoints).ThenInclude(x => x.Descriptions).FirstOrDefault(x => x.Id == id);
            return result;
        }

        public IEnumerable<Experience> ReadAll(Guid userId)
        {
            var result = _appDbContext.Experiences.Include(x => x.ExperiencePoints).ThenInclude(x => x.Descriptions).Where(x => x.UserInformationId == userId);
            return result;
         
        }

        public List<Experience> UpdateAll(List<Experience> updatedExperiences)
        {

            _appDbContext.Experiences.UpdateRange(updatedExperiences);
            _appDbContext.SaveChanges();
            return updatedExperiences;
        }
    }
}
