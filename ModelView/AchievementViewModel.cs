using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyResume.WebApp.ModelView
{
    public class AchievementViewModel
    {
        [Required]
        [MaxLength(40)]
        public string Title { get; set; }
        [Required]
        [MaxLength(380)]
        public string Summary { get; set; }

        [MaxLength(600)]
        public string MainText { get; set; }

        public string ThumbnailImgPath { get; set; }
        public IFormFile ThumbnailImage { get; set; }


        [Display(Name = "Order position")]
        public int OrderPosition { get; set; }
        public bool EnableComments { get; set; }
        public bool EnableRating { get; set; }

    }
}

