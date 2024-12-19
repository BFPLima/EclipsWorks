using TaskManager.Model;
using Task = TaskManager.Model.Task;

namespace TaskManager.IRepository;

public interface ITaskRepository : IRepositoryBase<Task>
{
    public IEnumerable<Task> GetByProject(Project project);

    public IEnumerable<TaskUpdateHistory> GetTaskUpdateHistoryByTask(Task task);
}