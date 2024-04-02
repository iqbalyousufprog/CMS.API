using CMS.API.Data;
using CMS.API.Model.Domain;
using CMS.API.Repositories.Interfaces;

namespace CMS.API.Repositories.Implementation
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(CMSDbContext _dbContext) : base(_dbContext)
        {
        }
    }
}
