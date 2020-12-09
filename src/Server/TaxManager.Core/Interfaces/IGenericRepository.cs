using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaxManager.Core.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> GetEntityById(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpecification(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);

        void Add(T entity);
    }
}