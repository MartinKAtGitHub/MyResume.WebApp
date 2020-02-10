
using MyResume.WebApp.Models;

namespace MyResume.WebApp.Data
{
    public interface IUserInfoRepo
    {
       UserInformation CreateDefault(ApplicationUser user);
       UserInformation Read(string userId);
       UserInformation Update(UserInformation userInformation);
       UserInformation Delete(string id);
    }
}
