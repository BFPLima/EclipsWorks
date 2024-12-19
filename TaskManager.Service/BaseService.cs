using TaskManager.IRepository;
using TaskManager.Model;

namespace TaskManager.Service;


public class BaseSevice<T, S> where T : TaskManagerModel
                              where S : IRepositoryBase<T>
{


    protected IRepositoryBase<T> repository;

    public BaseSevice(IRepositoryBase<T> repository)
    {
        this.repository = repository;
    }

    public virtual ServiceOperationResult Delete(T entity)
    {
        try
        {
            repository.Delete(entity);
            return new ServiceOperationResult(string.Empty, true);
        }
        catch (System.Exception ex)
        {
            return new ServiceOperationResult(ex.Message, false);
        }
    }

    public virtual T Find(Guid id)
    {
        return repository.Find(id);
    }

    public virtual ServiceOperationResult Insert(T entity)
    {
        try
        {
            repository.Insert(entity);
            return new ServiceOperationResult(string.Empty, true);
        }
        catch (System.Exception ex)
        {
            return new ServiceOperationResult(ex.Message, false);
        }
    }

    public virtual ServiceOperationResult Update(T entity)
    {
        try
        {
            repository.Update(entity);
            return new ServiceOperationResult(string.Empty, true);
        }
        catch (System.Exception ex)
        {
            return new ServiceOperationResult(ex.Message, false);
        }
    }

}
