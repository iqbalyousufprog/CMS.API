using CMS.API.Data;
using CMS.API.Model.Domain;
using CMS.API.Repositories.Interfaces;

namespace CMS.API.Repositories.Implementation
{
    public class ConsultationRepository : BaseRepository<Consultation>, IConsultationRepository
    {
        public ConsultationRepository(CMSDbContext _dbContext) : base(_dbContext)
        {
        }
        public override Task<IEnumerable<Consultation>> GetAllAsync()
        {
            return base.GetAllAsync(p => p.Patient, p => p.Doctor);
        }

        public override Task<Consultation?> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id, p => p.Patient, p => p.Doctor);
        }
    }
}
