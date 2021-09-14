using System.Linq;
using System.Web.Http;
using University.BL.DTOs;
using University.BL.Models;

namespace University.API.Controllers
{
    public class StudentsController : ApiController
    {
        private readonly UniversityEntities constext = new UniversityEntities();
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var students = constext.Student.ToList();
            var studentsDTO = students.Select(x => new StudentDTO
            {
                ID = x.ID,
                FirstMidName = x.FirstMidName,
                LastName = x.LastName,
                EnrollmentDate = x.EnrollmentDate.Value
                
            }).ToList();

        
            return Ok(studentsDTO);
        }
    }
}
