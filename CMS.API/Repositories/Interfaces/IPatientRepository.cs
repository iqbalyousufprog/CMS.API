using CMS.API.Model;
using CMS.API.Model.Domain;

namespace CMS.API.Repositories.Interfaces
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<IEnumerable<Patient>> GetAllAsync(PatientSearchFilter patientSearchFilter);
    }
}
