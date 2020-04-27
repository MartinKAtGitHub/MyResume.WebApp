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
        public Guid UserInformationId { get; set; }

        //[MaxLength(30)] // What is the limit on this in a IdentityUser ? Can this be a FK ?
        // public string UserName { get; set; } 

        [MaxLength(30)]
        public string FirstName { get; set; }
        
        [MaxLength(30)]
        public string MiddelName { get; set; }
        
        [MaxLength(30)]
        public string LasttName { get; set; } // Misspelling Last - extra T
        
        //[MaxLength(30)]
        //public string Profession { get; set; }

        [MaxLength(600)]
        public string Summary { get; set; }

        //[MaxLength(3000)]
        //public string MainText { get; set; }

        //public bool AvailableForContact { get; set; }
        public string AvatarImgPath { get; set; }

        public int AchievementCount { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public List<Achievement> Achievements { get; set; }

    }
}
