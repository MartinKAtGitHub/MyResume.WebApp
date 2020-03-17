using MyResume.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.ModelView
{
    public class UserSearchViewModel
    {
        public string SearchString { get; set; }
        public List<UserInformation> UsersResult{ get; set; }
        public string DefaultAvatarImg { get; set; }

        public UserSearchViewModel()
        {
            UsersResult = new List<UserInformation>();
        }

    }
}
