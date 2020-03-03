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


        /// <summary>
        /// Holds the full file path to the avatar image. The property also gets a default value when a user is first created see UserInfoRepo.CreateDefault()
        /// </summary>
        public string AvatarImgPath { get; set; }
       
        [Display(Name ="Avatar image")]
        public IFormFile AvatarImage { get; set; }
        public string Summary { get; set; }
        public string MainText { get; set; }
        public bool AvailableForContact { get; set; }
    }
}
