using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class EntollmentRepositoy : GenericRepository<Enrollment>,IEnrollmentRepositoy

    {
        public EntollmentRepositoy(UniversityEntities conrext) : base(conrext)
        {

        }
    }
}
