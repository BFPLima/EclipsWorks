using Microsoft.EntityFrameworkCore;
using TaskManager.IRepository;
using TaskManager.Model;

namespace TaskManager.EFRepository;

public class ProjectEFRepository : EFRepositoryBase<Project>, IProjectRepository
{
    public ProjectEFRepository(TaskManagerContext taskManagerContext) : base(taskManagerContext)
    {

    }

    public override void Delete(Project project)
    {
        taskManagerContext.Projects.Remove(project);
        taskManagerContext.SaveChangesAsync();
    }

    public override Project Find(Guid id)
    {
        return taskManagerContext.Projects.Include(p => p.User).FirstOrDefault(p => p.Id == id);
    }

    public override Project Insert(Project project)
    {
        taskManagerContext.Projects.Add(project);
        taskManagerContext.SaveChangesAsync();
        return project;
    }

    public override Project Update(Project project)
    {
        taskManagerContext.Projects.Update(project);
        taskManagerContext.SaveChangesAsync();
        return project;
    }

    public IEnumerable<Project> GetByUser(User user)
    {
        return taskManagerContext.Projects.Where(p => p.User == user).ToList<Project>();
    }
}
