using Microsoft.AspNetCore.Http;
using MyResume.WebApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyResume.WebApp.ModelView
{
    public class AchievementViewModel
    {
        //public AchievementViewModel() { }

        //public AchievementViewModel(int maxImageLimit)
        //{
        //    GalleryImagesArray = new BidingIformFileBridge[maxImageLimit];
        //   // ImagePaths = new List<string>();
        //}





        [Required]
        [MaxLength(40)]
        public string Title { get; set; }
        [Required]
        [MaxLength(380)]
        public string Summary { get; set; }

        [MaxLength(600)]
        public string MainText { get; set; }

        public List<string> ImageSrcPaths { get; set; }
        //public string[] ImageSrcPaths { get; set; }
        public BidingIformFileBridge[] GalleryImagesArray { get; set; }

        [Display(Name = "Order position")]
        public int OrderPosition { get; set; }
        public bool EnableComments { get; set; }
        public bool EnableRating { get; set; }

        public class BidingIformFileBridge // This works but not when i use the IFormFile[]
        {
            public IFormFile GalleryImage { get; set; }
        }
    }



}

