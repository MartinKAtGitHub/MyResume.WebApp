using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyResume.WebApp.ModelView
{
    public class EditUserInfoViewModel
    {
        // ADD VALIDATION TO THIS

        [MaxLength(15, ErrorMessage = "First name exceeds character limit")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(15, ErrorMessage = "Middle name exceeds character limit")]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [MaxLength(15, ErrorMessage = "Last name exceeds character limit")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        // string profession
        public string AvatarImgPath { get; set; }
       
        [Display(Name ="Avatar image")]
        public IFormFile AvatarImage { get; set; }
        public string Summary { get; set; }
        public string MainText { get; set; }
        public bool AvailableForContact { get; set; }
    }
}
