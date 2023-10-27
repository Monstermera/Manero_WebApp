using Manero_WebApp.Contexts;
using Manero_WebApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Manero_WebApp.Helpers.Repositories.MainRepo;

public class MainDbRepo<TEntity> where TEntity : class
{
    private readonly DataContext _db;

    public MainDbRepo(DataContext db)
    {
        _db = db;
    }

    //Gets all object from a table in the database 
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            return await _db.Set<TEntity>().ToListAsync();
        }
        catch (Exception) { return null!; }
    }


    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            return await _db.Set<TEntity>().Where(expression).ToListAsync();
        }
        catch (Exception) { return null!; }
    }


    ////Gets one object from a table in the database 
    //public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
    //{
    //    try
    //    {
    //        var item = await _db.Set<TEntity>().FirstOrDefaultAsync(expression);
    //        return item!;
    //    }
    //    catch (Exception) { return null!; }
    //}


    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
    {
        try
        {
            var query = _db.Set<TEntity>().AsQueryable();

            // Apply the filter
            query = query.Where(expression);

            // Apply includes
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var item = await query.FirstOrDefaultAsync();
            return item!;
        }
        catch (Exception)
        {
            return null!;
        }
    }



    //Adds one object to a table in the database 
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        try
        {
            _db.Set<TEntity>().Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
        catch (Exception) { return null!; }

    }

    //Updates an object in a table in the database
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        try
        {
            _db.Set<TEntity>().Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
        catch (Exception) { return null!; }
    }

    //Removes an object from a table in the database
    public virtual async Task<bool> RemoveAsync(TEntity entity)
    {
        try
        {
            _db.Set<TEntity>().Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        catch { };
        return false;
    }
}
