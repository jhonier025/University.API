using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.BL.DTOs
{
    public class StudentDTO
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public Nullable<System.DateTime> EnrollmentDate { get; set; }
        public string FullName
            {
                get { return string.Format("{0}, {1}", LastName, FirstMidName); }
            }
        }
}
