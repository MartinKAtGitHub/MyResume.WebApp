using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MyResume.WebApp.Models;
namespace MyResume.WebApp.Data
{
    public interface IExperienceRepo
    {
        // CRUD
        Experience Create(Experience model);
        Experience Read(string id);
        Experience Update(Experience newAchievement);
        Experience Delete(Experience experienceToDelete);
       
        IEnumerable<Experience> ReadAll(Guid userId);

    }
}
