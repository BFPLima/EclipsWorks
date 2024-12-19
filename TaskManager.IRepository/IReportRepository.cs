using TaskManager.Model;

namespace TaskManager.IRepository;

public interface IReportRepository : IRepositoryBase<Project>
{
    public IEnumerable<Project> GetByUser(User user);
}
