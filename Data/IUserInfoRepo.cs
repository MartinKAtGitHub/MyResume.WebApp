
using MyResume.WebApp.Models;
using System.Collections.Generic;

namespace MyResume.WebApp.Data
{
    public interface IUserInfoRepo
    {
        UserInformation CreateDefault(ApplicationUser user);
        UserInformation Read(string userId);
        UserInformation Update(UserInformation userInformation);
        UserInformation Delete(string id);
        IEnumerable<UserInformation> Search(string searchString);
    }
}
