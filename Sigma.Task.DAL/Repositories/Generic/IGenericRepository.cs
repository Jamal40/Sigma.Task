namespace Sigma.Task.DAL;

public interface IGenericRepository<T> where T : class
{
    void AddOrUpdate(T entity);
    void SaveChanges();
}
