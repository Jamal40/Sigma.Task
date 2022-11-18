namespace Sigma.Task.DAL;

public interface IGenericRepository<T> where T : class
{
    T? GetByKey(T key);
    List<T> GetAll();
    void AddOrUpdate(T key);
    void Delete(T key);
}
