using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class UserInformation
    {
        public string UserId { get; set; } // assign this as you create a new/edit User info
        public string UserName { get; set; }
        public string AvatarImgPath { get; set; }
        public string Summary { get; set; }
        public string MainText { get; set; }
        public bool AvailableForContact { get; set; }
      //  public string AvatarImagePath { get; set; }
    }
}
