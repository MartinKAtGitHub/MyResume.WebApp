using Microsoft.AspNetCore.Http;
using MyResume.WebApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyResume.WebApp.ModelView
{
    public class AchievementViewModel
    {
        public AchievementViewModel()
        {
            GalleryImages = new List<IFormFile>();
            GalleryImagesArray = new BindRetardation[6];
        }

        [Required]
        [MaxLength(40)]
        public string Title { get; set; }
        [Required]
        [MaxLength(380)]
        public string Summary { get; set; }

        [MaxLength(600)]
        public string MainText { get; set; }
        public IFormFile ThumbnailImage { get; set; }
        public List<IFormFile> GalleryImages { get; set; }
        public BindRetardation[] GalleryImagesArray { get; set; }

        [Display(Name = "Order position")]
        public int OrderPosition { get; set; }
        public bool EnableComments { get; set; }
        public bool EnableRating { get; set; }

        public class BindRetardation // This works but not when i use the IFormFile[]
        {
            public IFormFile GalleryImage { get; set; }
        }
    }



}

