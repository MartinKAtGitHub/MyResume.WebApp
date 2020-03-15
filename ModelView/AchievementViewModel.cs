using Microsoft.AspNetCore.Http;
using MyResume.WebApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyResume.WebApp.ModelView
{
    public class AchievementViewModel
    {


        [Required]
        [MaxLength(35)]
        public string Title { get; set; }

        [Required]
        [MaxLength(380)]
        public string Summary { get; set; }

        [MaxLength(600)]
        public string MainText { get; set; }



        public IFormFile Thumbnail { get; set; }
        public IFormFile GallaryImage_1 { get; set; }
        public IFormFile GallaryImage_2 { get; set; }
        public IFormFile GallaryImage_3 { get; set; }
        public IFormFile GallaryImage_4 { get; set; }
        public IFormFile GallaryImage_5 { get; set; }


        [Display(Name = "Order position")]
        [Range(1, 6, ErrorMessage = "Value for Order position must be between 1 and 6.")]
        public int OrderPosition { get; set; }
        public bool EnableComments { get; set; }
        public bool EnableRating { get; set; }
        public List<string> ImageSrcPaths { get; set; }
    }



}

