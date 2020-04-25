using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.ModelView
{
    public class SkillViewModel
    {
        [Required (ErrorMessage ="You need a tag name to create a tag")]
        [StringLength(maximumLength: 20, MinimumLength = 1)]
        [Display(Name ="Tag name")]
        public string TagName { get; set; }

        [Required(ErrorMessage ="You need to specify your proficiency")]
        [Range(1, 10)]
        [Display(Name = "Proficiency")]
        public int Level { get; set; }

        public int MaxLevelLimit { get => 10; }
    }
}
