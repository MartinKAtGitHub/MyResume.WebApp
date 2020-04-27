using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class ExperiencePointDescription
    {
      
        public string Id { get; set; }

        [Required]
        public string ExperiencePointId { get; set; }
        public ExperiencePoint ExperiencePoint { get; set; }

        [Required]
        public int Index { get; set; }
        //[Required(ErrorMessage = "Description is required")]
        [MaxLength(120)]
        public string Discription { get; set; } // TODO fix type Description 

        [NotMapped]
        public bool MarkForDeletion { get; set; }
    }
}
