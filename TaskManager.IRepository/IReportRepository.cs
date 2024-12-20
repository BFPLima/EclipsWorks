using TaskManager.Model;

namespace TaskManager.IRepository;

public interface IReportRepository
{
    public IEnumerable<Object[]> GetConpletedTasksByUserInTheLast30Days();
}
