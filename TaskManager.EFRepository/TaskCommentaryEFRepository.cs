using Microsoft.EntityFrameworkCore;
using TaskManager.IRepository;
using TaskManager.Model;
using Task = TaskManager.Model.Task;

namespace TaskManager.EFRepository;

public class TaskCommentaryEFRepository : EFRepositoryBase<TaskCommentary>, ITaskCommentaryRepository
{

    public TaskCommentaryEFRepository(TaskManagerContext taskManagerContext) : base(taskManagerContext)
    {

    }

    public override void Delete(TaskCommentary taskCommentary)
    {
        taskManagerContext.TaskCommentaries.Remove(taskCommentary);
        taskManagerContext.SaveChangesAsync();
    }

    public override TaskCommentary Find(Guid id)
    {
        return taskManagerContext.TaskCommentaries
        .Include(p => p.Task)
        .Include(p => p.CreatedBy)
        .Include(p => p.ModifiedBy)
        .FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<TaskCommentary> GetByTask(Task task)
    {
        return taskManagerContext.TaskCommentaries
        .Include(p => p.Task)
        .Include(p => p.CreatedBy)
        .Include(p => p.ModifiedBy)
        .Where(t => t.Task == task);
    }

    public override TaskCommentary Insert(TaskCommentary taskCommentary)
    {
        taskManagerContext.TaskCommentaries.Add(taskCommentary);
        taskManagerContext.SaveChangesAsync();
        return taskCommentary;
    }

    public override TaskCommentary Update(TaskCommentary taskCommentary)
    {
        taskManagerContext.TaskCommentaries.Update(taskCommentary);
        taskManagerContext.SaveChangesAsync();
        return taskCommentary;
    }
}
