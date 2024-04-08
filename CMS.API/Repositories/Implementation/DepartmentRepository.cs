using CMS.API.Data;
using CMS.API.Model.Domain;
using CMS.API.Repositories.Interfaces;

namespace CMS.API.Repositories.Implementation
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(CMSDbContext _dbContext) : base(_dbContext)
        {
        }
    }
}
