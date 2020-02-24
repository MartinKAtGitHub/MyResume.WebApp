using Microsoft.EntityFrameworkCore;
using MyResume.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Data
{
    public class UserInfoRepoSQL : IUserInfoRepo
    {
        private readonly AppDbContext _appDbContext;

        public UserInfoRepoSQL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public UserInformation CreateDefault(ApplicationUser user) // Can make Async
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

        public UserInformation Delete(string id) // We could make this a RESET insted
        {
            // CreateDefault()
            throw new NotImplementedException();
        }

        public UserInformation Read(string userId)
        {
            return _appDbContext.UserInformation.Include(user => user.ApplicationUser).FirstOrDefault(info => info.ApplicationUserId == userId);
        }

        public IEnumerable<UserInformation> Search(string searchString)
        {

            if (!string.IsNullOrEmpty(searchString))
            {
                // we load the FK data(ApplicationUser) using include. Then we search the user name 

                var filterdUsers = _appDbContext.UserInformation
                    .Include( info => info.ApplicationUser)
                    .Where( t => t.ApplicationUser.UserName.Contains(searchString)).ToList(); // Double check if this creates another query


                return filterdUsers;
            }

            return new List<UserInformation>();
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
