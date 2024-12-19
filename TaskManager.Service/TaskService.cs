using System.Text;
using TaskManager.IRepository;
using TaskManager.Model;
using Task = TaskManager.Model.Task;

namespace TaskManager.Service;


public class TaskService : BaseSevice<Task, ITaskRepository>
{
    protected ITaskUpdateHistoryRepository taskUpdateHistoryRepository;
    public TaskService(ITaskRepository repository,
                       ITaskUpdateHistoryRepository taskUpdateHistoryRepository) : base(repository)
    {
        this.taskUpdateHistoryRepository = taskUpdateHistoryRepository;
    }

    public IEnumerable<Task> GetByProject(Project project)
    {
        ITaskRepository taskRepository = (ITaskRepository)base.repository;
        return taskRepository.GetByProject(project);
    }

    public IEnumerable<TaskUpdateHistory> GetTaskUpdateHistoryByTask(Task task)
    {
        ITaskRepository taskRepository = (ITaskRepository)base.repository;
        return taskRepository.GetTaskUpdateHistoryByTask(task);
    }

    public override ServiceOperationResult Insert(Task task)
    {
        var numberOfTasks = GetByProject(task.Project).Count();

        if (numberOfTasks >= 20)
        {
            return new ServiceOperationResult("Não é possível adicionar mais Tarefas ao Projeto pois a quantidade máxima permitida foi atingida.", false);
        }

        return base.Insert(task);
    }

    public override ServiceOperationResult Update(Task task)
    {
        if (repository.HasModifications(task))
        {
            var modifications = repository.GetModifications(task);

            if (modifications.ContainsKey("Priority"))
            {
                return new ServiceOperationResult("Não é permitido alterar a Prioriade de uma Tarefa.", false);
            }

            taskUpdateHistoryRepository.Insert(new TaskUpdateHistory(task, modifications));
        }

        return base.Update(task);
    }
}
