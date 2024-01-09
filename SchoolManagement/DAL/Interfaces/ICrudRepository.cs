using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.DAL.Interfaces;

public interface ICrudRepository<TEntity>
{
    Task<TEntity> CreateAsync(TEntity entity);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity> GetByIdAsync(int id);

    Task<TEntity> UpdateAsync(int id, TEntity entity);

    Task<bool> DeleteAsync(int id);
}