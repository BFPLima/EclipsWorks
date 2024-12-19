using Microsoft.EntityFrameworkCore;
using TaskManager.IRepository;
using TaskManager.Model;
using Task = TaskManager.Model.Task;

namespace TaskManager.EFRepository;

public class TaskUpdateHistoryEFRepository : EFRepositoryBase<TaskUpdateHistory>, ITaskUpdateHistoryRepository
{

    public TaskUpdateHistoryEFRepository(TaskManagerContext taskManagerContext) : base(taskManagerContext)
    {

    }

    public override void Delete(TaskUpdateHistory taskUpdateHistory)
    {
        taskManagerContext.TaskUpdateHistories.Remove(taskUpdateHistory);
        taskManagerContext.SaveChangesAsync();
    }

    public override TaskUpdateHistory Find(Guid id)
    {
        return taskManagerContext.TaskUpdateHistories
        .Include(p => p.Task)
        .Include(p => p.CreatedBy)
        .FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<TaskUpdateHistory> GetByTask(Task task)
    {
        return taskManagerContext.TaskUpdateHistories.Where(t => t.Task == task);
    }

    public override TaskUpdateHistory Insert(TaskUpdateHistory taskUpdateHistory)
    {
        taskManagerContext.TaskUpdateHistories.Add(taskUpdateHistory);
        return taskUpdateHistory;
    }

    public override TaskUpdateHistory Update(TaskUpdateHistory task)
    {
        throw new NotImplementedException("É proibida a atualizaçao de Hitórico de Task");
    }
}
