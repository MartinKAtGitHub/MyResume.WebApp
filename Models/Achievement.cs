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
        public Guid Id{ get; set; }
      
        //[ForeignKey("UserId")]
        public string UserId{ get; set; } // assign this as you create a new/edit Achievement
        
        [MaxLength(40)]
        public string Title { get; set; }

        [MaxLength(380)]
        public string Summary { get; set; }
       
        [MaxLength(900)]
        public string MainText { get; set; }
        public int OrderPosition { get; set; }
        public bool EnableComments { get; set; }
        public bool EnableRating { get; set; }

        public Guid UserInformationId { get; set; }

        public UserInformation UserInfo { get; set; }
      
        //public int ImagesMaxLimit { get; set; } // Moved to appsettings
        //public List<string> ImagePaths { get; set; } // We want to set a max // Maybe make its own tbl
    }
}
