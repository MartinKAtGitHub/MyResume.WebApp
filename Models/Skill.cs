using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class Skill
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength:20, MinimumLength = 1)]
        public string TagName { get; set; }

        [Required]
        [Range(1,10)]
        public int Level { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
