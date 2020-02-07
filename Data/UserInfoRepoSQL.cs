using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class UserInfoRepoSQL : IUserInfoRepo
    {
        private readonly AppDbContext _appDbContext;

        public UserInfoRepoSQL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public UserInformation CreateDefault(ApplicationUser user)
        {
            var defaultEntery = new UserInformation
            {
                //Id = Guid.NewGuid() // this should be Automatic
                ApplicationUserId = user.Id,
                //UserName = user.UserName,
                Summary = "Summary text is empty",
                MainText = "Text empty",
                AvailableForContact = false
            };
            _appDbContext.UserInformation.Add(defaultEntery);
            _appDbContext.SaveChanges();

            return defaultEntery;
        }

        public UserInformation Delete(string id)
        {
            throw new NotImplementedException();
        }

        public UserInformation Read(string userId)
        {
           return _appDbContext.UserInformation.FirstOrDefault(info => info.ApplicationUserId == userId);
        }

        public UserInformation Update(UserInformation userInformationChanges)
        {
            var userInfo = _appDbContext.UserInformation.Attach(userInformationChanges);
            userInfo.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _appDbContext.SaveChanges();
            return userInformationChanges;
        }
    }
}
