using System.Text;
using TaskManager.IRepository;
using TaskManager.Model;
using Task = TaskManager.Model.Task;

namespace TaskManager.Service;


public class TaskCommentaryService : BaseSevice<TaskCommentary, ITaskCommentaryRepository>
{
    protected ITaskUpdateHistoryRepository taskUpdateHistoryRepository;
    public TaskCommentaryService(ITaskCommentaryRepository taskCommentaryRepository,
                                 ITaskUpdateHistoryRepository taskUpdateHistoryRepository) : base(taskCommentaryRepository)
    {
        this.taskUpdateHistoryRepository = taskUpdateHistoryRepository;
    }

    public IEnumerable<TaskCommentary> GetByTask(Task task)
    {
        ITaskCommentaryRepository taskCommentaryRepository = (ITaskCommentaryRepository)base.repository;
        return taskCommentaryRepository.GetByTask(task);
    }

    public override ServiceOperationResult Insert(TaskCommentary taskCommentary)
    {
        taskUpdateHistoryRepository.Insert(new TaskUpdateHistory(taskCommentary));
        return base.Insert(taskCommentary);
    }

    public override ServiceOperationResult Update(TaskCommentary taskCommentary)
    {
        taskUpdateHistoryRepository.Insert(new TaskUpdateHistory(taskCommentary));
        return base.Update(taskCommentary);
    }
}
