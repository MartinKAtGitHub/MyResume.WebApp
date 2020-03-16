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
            
        }

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

        public int OrderPosMaxLimit { get; set; }
        public string OrderPosLbl { get => $"Order position (1 - {OrderPosMaxLimit})"; } //TODO change hard coded range to use config max limit
        [DataType(DataType.Text)]
        [Range(1, 6, ErrorMessage = "Value out of range.")] 
        public int OrderPosition { get; set; }
        public bool EnableComments { get; set; }
        public bool EnableRating { get; set; }
        public List<string> ImageSrcPaths { get; set; }

    }



}

