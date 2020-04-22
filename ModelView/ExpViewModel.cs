using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MyResume.WebApp.ModelView
{
    public class ExpViewModel
    {

        public ExpViewModel()
        {
            ExpPoints = new List<ExpPoint>();
        }

        public string UserId { get; set; }


        [Required(ErrorMessage = "Experience title is required")]
        [MaxLength(30)]
        public string Title { get; set; }

        [MinLength(1, ErrorMessage ="You need at least 1 highlight in your experience")] // expViewModel 21 -> TODO need error ErrorMessage to display in the validation summery 
        public List<ExpPoint> ExpPoints { get; set; }
        
        
        
        // MOD ----------------------------
        public string Id { get; set; }
        public bool MarkForDeletion { get; set; }


        public class ExpPoint 
        {
            public ExpPoint()
            {
                Descriptions = new List<Descriptions>();
            }
            
          //  [Required(ErrorMessage = "Experience highlight title is required")]
            [MaxLength(30)]
            public string PointTitle { get; set; }
            
            //Dates
            [MinLength(1, ErrorMessage ="You need at least 1 description in your experience highlight")]
            public List<Descriptions> Descriptions { get; set; } // TODO Descriptions only has client side validation, we need to make sure for every creation a description is given ?
           
            //MOD----------------------------------------------
            public bool MarkForDeletion { get; set; }
            public string Id { get; set; }
        }

        public class Descriptions
        {
            //[Required(ErrorMessage ="Description is required")]
            [MaxLength(60)]
            public string Desc { get; set; }

            //MOD---------------------------------------------
            public bool MarkForDeletion { get; set; }
        }
    }




}
