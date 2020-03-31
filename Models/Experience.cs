using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MyResume.WebApp.Models
{
    public class Experience
    {
        public string Id { get; set; }

        //public ApplicationUser ApplicationUserId { get; set; }
        //public  ApplicationUser ApplicationUser { get; set; }
        public Guid UserInformationId { get; set; }
        public UserInformation UserInformation { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<ExperiencePoint> ExperiencePoints { get; set; }
    }
}
