using CMS.API.Model.Domain;
using System.Linq.Expressions;

namespace CMS.API.Repositories.Interfaces
{
    public interface IBaseRepository<T>
        where T : BaseEntity, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T model);
        Task<T?> GetByIdAsync(int id);
        Task<T?> UpdateAsync(T model);
        //Task<T?> UpdateAsync(T model, params Expression<Func<T, object>>[] includes);
        Task<T?> DeleteAsync(int id);
    }
}
