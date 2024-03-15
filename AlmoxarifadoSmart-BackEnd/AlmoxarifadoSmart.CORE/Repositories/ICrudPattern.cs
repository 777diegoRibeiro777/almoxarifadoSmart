using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Core.Repositories
{
    public interface ICrudPattern<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
