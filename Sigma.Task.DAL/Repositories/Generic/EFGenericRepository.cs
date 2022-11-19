using System.ComponentModel.DataAnnotations;

namespace Sigma.Task.DAL;

public class EFGenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly CandidatesContext _context;

    public EFGenericRepository(CandidatesContext context)
    {
        _context = context;
    }

    public void AddOrUpdate(T entity)
    {
        var key = GetKeyValue(entity);
        var existingEntity = _context.Set<T>().Find(key);
        if (existingEntity is null)
        {
            _context.Set<T>().Add(entity);
            return;
        }
        _context.Entry(existingEntity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        _context.Set<T>().Update(entity);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    #region Helper

    private object? GetKeyValue(T entity)
    {
        var keyProperty = typeof(T).GetProperties()
            .First(p => p.CustomAttributes
                .Select(a => a.AttributeType)
                .Contains(typeof(KeyAttribute)));
        return keyProperty.GetValue(entity, null);
    }

    #endregion
}
