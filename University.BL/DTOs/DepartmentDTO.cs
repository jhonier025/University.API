using System;

namespace University.BL.DTOs
{
    public class DepartmentDTO
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public double Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int InstructorID { get; set; }
        public InstructorDTO Instructor { get; set; }
    }
}
