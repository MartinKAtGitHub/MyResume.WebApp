using System.Collections.Generic;
using MyResume.WebApp.Models;
namespace MyResume.WebApp.ModelView
{
    public class UserResumeViewModel
    {
        public string AppUserId { get; set; }
        public UserInformation UserInfo { get; set; }
        public IEnumerable<Achievement> Achievements { get; set; }
        public IEnumerable<ExpViewModel> Experiences { get; set; }
        //public IEnumerable<Skill> Skills { get; set; }

        public ExpViewModel NewExpGrp { get; set; }
        public SkillViewModel NewSkillViewModel { get; set; }

        public bool EnableOwnerOptions { get; set; }

        public string DefaultAvatarImage { get; set; }
        public string DefaultGalleryImage { get; set; }



        public UserResumeViewModel()
        {
            Achievements = new List<Achievement>();
            Experiences = new List<ExpViewModel>();
        }

    }
}
