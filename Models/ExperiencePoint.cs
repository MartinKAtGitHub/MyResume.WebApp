using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyResume.WebApp.Models
{
    public class ExperiencePoint
    {

        // REMEBER TO SET LIMITS AND SHIT <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        public string Id { get; set; }

        [Required]
        public string ExperienceId { get; set; }
        public Experience Experience { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
     
        public List<ExperiencePointDescription> Descriptions { get; set; }
    }
}