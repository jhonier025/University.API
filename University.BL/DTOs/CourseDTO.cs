using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class CourseDTO
    {        
        [Required(ErrorMessage = "The ID is required")]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "The Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Credits is required")]        
        public int Credits { get; set; }
    }
}
