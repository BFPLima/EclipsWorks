using Microsoft.EntityFrameworkCore;
using TaskManager.IRepository;
using TaskManager.Model;
using Task = TaskManager.Model.Task;

namespace TaskManager.EFRepository;

public class TaskEFRepository : EFRepositoryBase<Task>, ITaskRepository
{

    public TaskEFRepository(TaskManagerContext taskManagerContext) : base(taskManagerContext)
    {

    }

    public override void Delete(Task task)
    {
        taskManagerContext.Tasks.Remove(task);
        taskManagerContext.SaveChangesAsync();
    }

    public override Task Find(Guid id)
    {
        return taskManagerContext.Tasks
        .Include(p => p.Project)
        .Include(p => p.Project.User)
        .FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Task> GetByProject(Project project)
    {
        return taskManagerContext.Tasks.Where(t => t.Project == project);
    }

    public IEnumerable<TaskUpdateHistory> GetTaskUpdateHistoryByTask(Task task)
    {
        return taskManagerContext.TaskUpdateHistories
        .Include(o => o.CreatedBy)
        .Include(o => o.Task)
        .Where(t => t.Task == task)
        .OrderBy(o => o.CreatedAt);
    }

    public override Task Insert(Task task)
    {
        taskManagerContext.Tasks.Add(task);
        taskManagerContext.SaveChangesAsync();
        return task;
    }

    public override Task Update(Task task)
    {
        taskManagerContext.Tasks.Update(task);
        taskManagerContext.SaveChangesAsync();
        return task;
    }
}
