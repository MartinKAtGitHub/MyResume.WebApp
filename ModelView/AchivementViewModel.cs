using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.ModelView
{
    public class AchivementViewModel
    {
        public string Title { get; set; }

        public string Summary { get; set; }
     
        public string MainText { get; set; }
        public int OrderPosition { get; set; }
        public bool EnableComments { get; set; }
        public bool EnableRating { get; set; }

    }
}

