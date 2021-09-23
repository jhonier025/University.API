using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class StudentRepository : GenericRepository<Student>,IStudentRepository

    {
        public StudentRepository(UniversityEntities conrext) : base(conrext)
        {
        
        }
    }
}
