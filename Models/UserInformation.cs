using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class UserInformation // Maybe add this in AppUser instead
    {
        //[BindNever] // this is not recommended(?) its preferred to create a ViewModel and populate it with data you need.
        public Guid Id { get; set; } 
        public string UserId { get; set; } // This could be a PK since only 1 entry per user BUT if we make an list of Achievements they get a 1 - many relationship
        
        [MaxLength(30)]
        public string UserName { get; set; } // We can get this from UserIdentity(But do i manually add this or should EF core handle it)
        [MaxLength(40)]
        public string FirstName { get; set; }
        [MaxLength(40)]
        public string MiddelName { get; set; }
        [MaxLength(40)]
        public string LasttName { get; set; }
        [MaxLength(30)]
        public string Profession { get; set; }

        [MaxLength(380)]
        public string Summary { get; set; }

        [MaxLength(5000)]
        public string MainText { get; set; }
        public bool AvailableForContact { get; set; }

        public string AvatarImgPath { get; set; }
        public List<Achievement> Achievements { get; set; } // Creates a 1 to many relationship

    }
}
