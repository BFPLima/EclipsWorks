using TaskManager.Model;

namespace TaskManager.IRepository;

public interface IRepositoryBase<T> where T : TaskManagerModel
{
    public T Insert(T entity);
    public T Find(Guid id);
    public T Update(T entity);
    public void Delete(T entity);
    public bool HasModifications(T entity);
    public IDictionary<string, (object, object)> GetModifications(T entity);
}
