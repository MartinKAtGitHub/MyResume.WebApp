using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class ExperiencePointDescription
    {
        public string Id { get; set; }
       
        [Required]
        public ExperiencePoint ExperiencePoint { get; set; }
        
        [Required]
        [MaxLength(60)]
        public string Discription { get; set; }
    }
}
