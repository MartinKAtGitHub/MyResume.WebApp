using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class UserResumePage
    {
        public string Id { get; set; }
        // img thumbnail
        // img[] carousel
        public string PageTitle { get; set; }
        public string Summery { get; set; }
        public string MainText { get; set; }
        public bool EnableComments { get; set; }
        public bool EnableRating { get; set; }

    }
}
