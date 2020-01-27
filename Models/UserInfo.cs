using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string Summary { get; set; }
        public string MainText { get; set; }
        public bool AvailableForContact { get; set; }
      //  public string AvatarImagePath { get; set; }
    }
}
