using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class ItemGalleryImageFilePath
    { 
        // !!!!!!!!!!!!! >>>>> WARNING THIS IS A DATA BASE CLASS -> DONT CHANGE NAMES OR ADD STUFF WITHOUT UPDATING DB <<<<<  !!!!!!!!!!!

        public string Id { get; set; }
        public string GalleryImageFilePath { get; set; }

        [Required]
        public Achievement Achievement { get; set; }
    }
}
