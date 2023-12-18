using Microsoft.EntityFrameworkCore;
using SchoolManagement.Common.Exceptions;
using SchoolManagement.DAL.Interfaces;

namespace SchoolManagement.DAL.Repositories;

public abstract class BaseRepository<TEntity> : ICrudRepository<TEntity> where TEntity : class
{
    private readonly SchoolDbContext _dbContext;

    public BaseRepository(SchoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Error occurred while creating entity.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Error occurred while deleting entity with ID: {id}.", ex);
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            var result = await _dbContext.Set<TEntity>().ToListAsync();

            return result;
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Error occurred while retrieving all entities.", ex);
        }
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        try
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Error occurred while retrieving entity with ID: {id}.", ex);
        }
    }

    public async Task<TEntity> UpdateAsync(int id, TEntity entity)
    {
        try
        {
            var existingEntity = await _dbContext.Set<TEntity>().FindAsync(id);

            if (existingEntity == null)
            {
                return null;
            }

            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();

            return existingEntity;
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Error occurred while updating entity with ID: {id}.", ex);
        }
    }
}
