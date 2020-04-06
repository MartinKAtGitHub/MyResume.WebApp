using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MyResume.WebApp.ModelView
{
    public class ExpViewModel
    {
        [Required]
        public string ExpUserInfoID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        // Dates
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
            
            public List<Descriptions> Descriptions { get; set; }

        }

        public class Descriptions
        {
            [Required]
            [MaxLength(60)]
            public string Desc { get; set; }
        }
    }




}
