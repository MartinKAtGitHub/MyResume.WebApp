using MyResume.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Data
{
    public class SkillRepoSQL : ISkillRepo
    {
        private readonly AppDbContext _appDbContext;

        public SkillRepoSQL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public Skill Create(Skill newSkill)
        {
            _appDbContext.Skills.Add(newSkill);
            _appDbContext.SaveChanges();
            return newSkill;
        }

        public Skill Delete(Skill skill)
        {
            _appDbContext.Skills.Remove(skill);
            _appDbContext.SaveChanges();
            return skill;
                
        }

        public Skill Read(Guid id)
        {
            return _appDbContext.Skills.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Skill> ReadAll(string appUserId)
        {
          return  _appDbContext.Skills.Where(x => x.ApplicationUserId == appUserId);
        }

        public Skill Update(Skill updatedSkill)
        {
            _appDbContext.Skills.Update(updatedSkill);
            _appDbContext.SaveChanges();
            return updatedSkill;
        }
    }
}
