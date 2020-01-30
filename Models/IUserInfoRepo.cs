
namespace MyResume.WebApp.Models
{
    public interface IUserInfoRepo
    {
       UserInformation CreateDefault(ApplicationUser user);
       UserInformation Read(string userId);
       UserInformation Update(UserInformation userInformation);
       UserInformation Delete(string id);
    }
}
