using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class UserInformation
    {
        public Guid Id { get; set; } 
        public string UserId { get; set; } // This could be a PK since only 1 entry per user
        public string UserName { get; set; }
        public string AvatarImgPath { get; set; }
        public string Summary { get; set; }
        public string MainText { get; set; }
        public bool AvailableForContact { get; set; }
      //  public string AvatarImagePath { get; set; }
    }
}
