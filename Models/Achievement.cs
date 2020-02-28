﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class Achievement
    {
        public Guid AchievementId { get; set; }
   
        [MaxLength(40)]
        public string Title { get; set; }

        [MaxLength(380)]
        [Required]
        public string Summary { get; set; }
       
        [MaxLength(600)]
        public string MainText { get; set; }

        public int OrderPosition { get; set; }
        public bool EnableComments { get; set; }
        public bool EnableRating { get; set; }
        public string ThumbnailImgPath { get; set; }
        public Guid UserInformationId { get; set; }
        public UserInformation UserInformation { get; set; }
      
        //public int ImagesMaxLimit { get; set; } // Moved to appsettings
        //public List<string> ImagePaths { get; set; } // We want to set a max // Maybe make its own tbl
    }
}
