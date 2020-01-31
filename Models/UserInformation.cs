using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class UserInformation // Maybe add this in AppUser instead
    {
        [BindNever]
        public Guid Id { get; set; } 
        public string UserId { get; set; } // This could be a PK since only 1 entry per user
        
        [MaxLength(60)]
        public string UserName { get; set; }
        // string FirstName
        // string MiddelName
        // string LastName
        // string profession
        public string AvatarImgPath { get; set; }
        public string Summary { get; set; }
        public string MainText { get; set; }
        public bool AvailableForContact { get; set; }
      //  public string AvatarImagePath { get; set; }
    }
}
