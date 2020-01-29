using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class Achievement
    {
      //  [ForeignKey("UserId")]
        public string UserId{ get; set; } // assign this as you create a new/edit Achievement
        public string Title { get; set; }
        public string Summary { get; set; }
        public string MainText { get; set; }
        
        //public int ImagesMaxLimit { get; set; } // Moved to appsettings
        //public List<string> ImagePaths { get; set; } // We want to set a max // Maybe make its own tbl
    }
}
