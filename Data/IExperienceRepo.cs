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
        List<Experience> UpdateAll(List<Experience> updatedExperiences);
        Experience DeleteExp(Experience experienceToDelete);
        ExperiencePoint DeleteExpPoint(ExperiencePoint experiencePointToDelete);
        ExperiencePointDescription DeleteExpPointDesc(ExperiencePointDescription experiencePointDescToDelete);



        IEnumerable<Experience> ReadAll(Guid userId);

    }
}
