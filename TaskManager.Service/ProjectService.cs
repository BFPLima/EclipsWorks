using TaskManager.IRepository;
using TaskManager.Model;

namespace TaskManager.Service;


public class ProjectService : BaseSevice<Project, IProjectRepository>
{

    protected TaskService taskService;
    public ProjectService(IProjectRepository repository, TaskService taskService) : base(repository)
    {
        this.taskService = taskService;
    }

    public IEnumerable<Project> GetByUser(User user)
    {
        IProjectRepository projectRepository = (IProjectRepository)base.repository;
        return projectRepository.GetByUser(user);
    }

    public override ServiceOperationResult Delete(Project project)
    {
        var hasPendingTasks = taskService.GetByProject(project).Any(t => t.Status == Model.TaskStatus.Pending);

        if (hasPendingTasks)
        {
            return new ServiceOperationResult("Não é possível remover o Projeto pois existem Tarefas pendentes associadas a ele. Para tal, é necessário concluir as Tarefas pendentes ou removê-las.", false);
        }

        return base.Delete(project);
    }
}
