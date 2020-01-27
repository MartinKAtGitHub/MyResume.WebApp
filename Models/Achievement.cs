using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class Achievement
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string MainText { get; set; }

        //public List<string> ImagePaths { get; set; } // We want to set a max // Maybe make its own tbl
    }
}
