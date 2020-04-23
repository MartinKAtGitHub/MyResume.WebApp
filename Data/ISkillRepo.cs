using MyResume.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Data
{
    public interface ISkillRepo
    {
        Skill Create(Skill newSkill);
        Skill Read(Guid id);
        IEnumerable<Skill> ReadAll(string appUserId);
        Skill Update(Skill updatedSkill);
        Skill Delete(Skill skill);
    }
}
