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
        [MaxLength(35)]
        public string Title { get; set; }
      
        [Required]
        [MaxLength(380)]
        public string Summary { get; set; }

        [MaxLength(600)]
        public string MainText { get; set; }

        // i dont use this info in the back end so information binded to this is just thrown
        public List<string> ImageSrcPaths { get; set; } 
        public BidingBridgeIFormFile[] GalleryImagesArray { get; set; }

        [Display(Name = "Order position")]
        public int OrderPosition { get; set; }
        public bool EnableComments { get; set; }
        public bool EnableRating { get; set; }

        public class BidingBridgeIFormFile // This works but not when i use the IFormFile[]
        {
            public IFormFile GalleryImage { get; set; }
        }
    }



}

