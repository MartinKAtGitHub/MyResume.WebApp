using System.Collections.Generic;
using MyResume.WebApp.Models;
namespace MyResume.WebApp.ModelView
{
    public class UserResumeViewModel
    {
        public UserInformation UserInfo { get; set; }
        public List <Achievement> Achievements { get; set; }
        public bool EnableEditing { get; set; }
        public UserResumeViewModel()
        {

        }

    }
}
