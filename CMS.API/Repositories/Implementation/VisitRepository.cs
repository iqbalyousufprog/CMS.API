using CMS.API.Data;
using CMS.API.Model.Domain;
using CMS.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CMS.API.Repositories.Implementation
{
    public class VisitRepository : BaseRepository<Visit>, IVisitRepository
    {
        public VisitRepository(CMSDbContext _dbContext) : base(_dbContext)
        {
        }

        public override async Task<Visit?> UpdateAsync(Visit model)
        {
            //return base.UpdateAsync(model, p => p.Patient);

            var existingVisit = await dbContext.Visits.IncludeMultiple(x => x.Patient)
              .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (existingVisit == null)
                return null;

            //update Patient
            dbContext.Entry(existingVisit).CurrentValues.SetValues(model);

            existingVisit.Patient = model.Patient;
            await dbContext.SaveChangesAsync();

            return model;
        }

        public override async Task<IEnumerable<Visit>> GetAllAsync()
        {
            return await base.GetAllAsync(p => p.Patient);
        }

        public override async Task<Visit?> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id, p => p.Patient);
        }
    }
}
