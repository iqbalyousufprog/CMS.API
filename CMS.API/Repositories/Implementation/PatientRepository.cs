using CMS.API.Data;
using CMS.API.Model;
using CMS.API.Model.Domain;
using CMS.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Linq.Expressions;

namespace CMS.API.Repositories.Implementation
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(CMSDbContext _dbContext) : base(_dbContext)
        {
        }

        public override async Task<Patient?> UpdateAsync(Patient model)
        {
            //return await base.UpdateAsync(model, p => p.Diseases);
            var existingPatient = await dbContext.Patients.IncludeMultiple(x => x.Diseases)
              .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (existingPatient == null)
                return null;

            //update Patient
            dbContext.Entry(existingPatient).CurrentValues.SetValues(model);

            //update Diseases
            existingPatient.Diseases = model.Diseases;
            await dbContext.SaveChangesAsync();

            return model;
        }

        public override async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await base.GetAllAsync(p => p.Diseases);
        }

        public override async Task<Patient?> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id, p => p.Diseases, p => p.Consultations);
        }

        public async Task<IEnumerable<Patient>> GetAllAsync(PatientSearchFilter patientSearchFilter)
        {
            IQueryable<Patient> patientsQuery = GetQuerable().IncludeMultiple(p => p.Diseases);

            if (patientSearchFilter != null)
            {
                if (!string.IsNullOrEmpty(patientSearchFilter.Name))
                    patientsQuery = patientsQuery.Where(p => p.Name.Contains(patientSearchFilter.Name));

                if (patientSearchFilter.Age > 0)
                    patientsQuery = patientsQuery.Where(p => p.Age == patientSearchFilter.Age);

                if (!string.IsNullOrEmpty(patientSearchFilter.Gender))
                    patientsQuery = patientsQuery.Where(p => p.Gender == patientSearchFilter.Gender);

                if (!string.IsNullOrEmpty(patientSearchFilter.PhoneNumber))
                    patientsQuery = patientsQuery.Where(p => p.PhoneNumber == patientSearchFilter.PhoneNumber);
            }

            return await patientsQuery.ToListAsync();
        }
    }
}
