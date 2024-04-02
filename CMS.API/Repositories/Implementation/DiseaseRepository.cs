using CMS.API.Data;
using CMS.API.Model.Domain;
using CMS.API.Repositories.Interfaces;

namespace CMS.API.Repositories.Implementation
{
    public class DiseaseRepository : BaseRepository<Disease>, IDiseaseRepository
    {
        public DiseaseRepository(CMSDbContext _dbContext) : base(_dbContext)
        {
        }
    }
}
