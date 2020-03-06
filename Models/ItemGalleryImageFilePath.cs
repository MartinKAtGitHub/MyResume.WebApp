using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{
    public class ItemGalleryImageFilePath
    {
        public string Id { get; set; }
        public string FilePath { get; set; }

        [Required]
        public Achievement Achievement { get; set; }
    }
}
