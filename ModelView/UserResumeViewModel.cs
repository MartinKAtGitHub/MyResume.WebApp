using System.Collections.Generic;
using MyResume.WebApp.Models;
namespace MyResume.WebApp.ModelView
{
    public class UserResumeViewModel
    {
        public UserInformation UserInfo { get; set; }
        public IEnumerable <Achievement> Achievements { get; set; }
        public bool EnableOwnerOptions { get; set; }

        public string DefaultAvatarImage { get; set; }
        public string DefaultGalleryImage { get; set; }


        public UserResumeViewModel()
        {
            Achievements = new List<Achievement>();
        }

    }
}
