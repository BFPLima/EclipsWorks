using TaskManager.Model;
using Task = TaskManager.Model.Task;

namespace TaskManager.IRepository;

public interface ITaskCommentaryRepository : IRepositoryBase<TaskCommentary>
{
    public IEnumerable<TaskCommentary> GetByTask(Task task);
}