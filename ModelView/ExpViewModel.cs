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


        [Required]
        public string ExpUserInfoID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [MinLength(1, ErrorMessage ="You need at least 1 highlight in your experience")] // expViewModel 21 -> TODO need error ErrorMessage to display in the validation summery 
        public List<ExpPoint> ExpPoints { get; set; }

        public class ExpPoint // Because i am calling this a DESC is beeing called as welleven thought i don tmake feild for it
        {
            public ExpPoint()
            {
                Descriptions = new List<Descriptions>();
            }
            
            [Required]
            [MaxLength(30)]
            public string PointTitle { get; set; }
            
            //Dates
            [MinLength(1, ErrorMessage ="You need at least 1 description in your experience highlight")]
            public List<Descriptions> Descriptions { get; set; } // TODO Descriptions only has client side validation, we need to make sure for every creation a description is given ?

        }

        public class Descriptions
        {
            [Required]
            [MaxLength(60)]
            public string Desc { get; set; }
        }
    }




}
