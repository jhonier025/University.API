using University.BL.Models;


namespace University.BL.Repositories.Implements
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository

    {
        public DepartmentRepository(UniversityEntities conrext) : base(conrext)
        {

        }
    }
}
