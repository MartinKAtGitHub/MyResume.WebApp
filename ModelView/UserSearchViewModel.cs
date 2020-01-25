using MyResume.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.ModelView
{
    public class UserSearchViewModel
    {
        public string UserNameSearch { get; set; }
        public List<ApplicationUser> UsersResult{ get; set; }

    }
}
