using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class OfficeRepository : GenericRepository<OfficeAssignment>,IOfficeRepository
    {

        public OfficeRepository(UniversityEntities conrext) : base(conrext)
        {

        }
    }
}
