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

        /// <summary>
        /// The DB dose not store/retrieve data in the same order in went inn. so we cache the index so we can sort it back into the order the images was loaded inn
        /// </summary>
        public int GalleryIndex { get; set; }
      
        [Required]
        public Achievement Achievement { get; set; }
    }
}
