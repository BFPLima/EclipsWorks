using Microsoft.EntityFrameworkCore;
using TaskManager.IRepository;
using TaskManager.Model;
using Task = TaskManager.Model.Task;

namespace TaskManager.EFRepository;

public abstract class EFRepositoryBase<T> : IRepositoryBase<T> where T : TaskManagerModel
{
    protected TaskManagerContext taskManagerContext;
    public EFRepositoryBase(TaskManagerContext taskManagerContext)
    {
        this.taskManagerContext = taskManagerContext;
    }

    public abstract void Delete(T entity);

    public abstract T Find(Guid id);

    public IDictionary<string, (object, object)> GetModifications(T entity)
    {
        var dic = new Dictionary<string, (object, object)>();

        foreach (var entry in taskManagerContext.Entry(entity).Properties)
        {
            if (entry.IsModified)
            {
                dic.Add(entry.Metadata.Name, (entry.OriginalValue, entry.CurrentValue));
            }
        }

        return dic;
    }

    public bool HasModifications(T entity)
    {
        return taskManagerContext.Entry(entity).State == EntityState.Modified;
    }

    public abstract T Insert(T entity);

    public abstract T Update(T Entity);
}
