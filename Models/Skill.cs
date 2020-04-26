using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class Skill
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 1)]
        public string TagName { get; set; }

        [Required]
        [Range(1, 10)] // MaxLevelLimit
        public int Level { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
        public int Index { get; set; }
        [NotMapped]
        public int MaxLevelLimit { get => 10; }


        [NotMapped]
        public string[] ProficiencyLevelsText
        {
            get { return new string[] { "Novice Low", "Novice Mid", "Novice High",
                "Intermediate Low", "Intermediate Mid", "Intermediate High",
                "Advanced Low","Advanced Mid","Advanced High","Expert", }; 
            }
        }
    }
}
