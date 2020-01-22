using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    interface IUserResumePageRepository
    {
        UserResumePage Creat(UserResumePage userResumePage);
        UserResumePage Read(string id);
        UserResumePage Update(UserResumePage userResumePage);
        UserResumePage Delete(string id);

        // GetAllPages ?
    }
}
