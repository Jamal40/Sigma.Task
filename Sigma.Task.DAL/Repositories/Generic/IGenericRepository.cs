namespace Sigma.Task.DAL;

public interface IGenericRepository<T> where T : class
{
    List<T> GetAll();
    void AddOrUpdate(T entity);
    void SaveChanges();
}
