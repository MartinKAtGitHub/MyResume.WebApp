using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.ModelView
{
    public class EditUserInfoViewModel
    {

        // string FirstName
        // string MiddelName
        // string LastName
        // string profession
        public string AvatarImgPath { get; set; }
        public string Summary { get; set; }
        public string MainText { get; set; }
        public bool AvailableForContact { get; set; }
    }
}
