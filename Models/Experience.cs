using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyResume.WebApp.Models
{
    public class Experience
    {
        public string Id { get; set; }
        
        [Required]
        public  ApplicationUser User  { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<ExperiencePoint> ExperiencePoint { get; set; }
    }
}
