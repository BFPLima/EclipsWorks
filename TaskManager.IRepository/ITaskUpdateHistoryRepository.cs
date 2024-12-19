using TaskManager.Model;
using Task = TaskManager.Model.Task;

namespace TaskManager.IRepository;

public interface ITaskUpdateHistoryRepository : IRepositoryBase<TaskUpdateHistory>
{
    public IEnumerable<TaskUpdateHistory> GetByTask(Task task);
}