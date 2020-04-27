using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyResume.WebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Data
{
    public class UserInfoRepoSQL : IUserInfoRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;

        public UserInfoRepoSQL(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        public UserInformation CreateDefault(ApplicationUser user) // Can make Async
        {
            var defaultEntery = new UserInformation
            {
                //Id = Guid.NewGuid() // this should be Automatic
                ApplicationUserId = user.Id,
                //UserName = user.UserName,
                Summary = "Summary text is empty",
                //MainText = "Text empty",
                //AvailableForContact = false,

                //AvatarImgPath = _config.GetValue<string>("FileUploadSettings:DefaultAvatarImgFilePath")    // AvatarImgPath = "~/images/MyResumeDefaultAvatar.png"
                AvatarImgPath = null // We this because you can remove the default images with the above code.

            };
            _appDbContext.UserInformation.Add(defaultEntery);
            _appDbContext.SaveChanges();

            return defaultEntery;
        }

        public UserInformation Delete(string id) //TODO UserInformation has no DELETE operation  -> We could make this a RESET instead because its pretty much AppUser
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
                    .Include(info => info.ApplicationUser)
                    .Where(t =>
                       t.ApplicationUser.UserName.Contains(searchString) ||
                       t.ApplicationUser.Email.Contains(searchString) ||
                       t.FirstName.Contains(searchString) ||
                       t.MiddelName.Contains(searchString) ||
                       t.LasttName.Contains(searchString)).ToList(); // Double check if this creates another query


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
