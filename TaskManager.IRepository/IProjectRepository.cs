using TaskManager.Model;

namespace TaskManager.IRepository;

public interface IProjectRepository : IRepositoryBase<Project>
{
    public IEnumerable<Project> GetByUser(User user);
}
