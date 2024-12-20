using TaskManager.IRepository;
using TaskManager.Model;

namespace TaskManager.Service;


public class ReportService
{

    protected IReportRepository repository;
    public ReportService(IReportRepository repository)
    {
        this.repository = repository;
    }

    public IEnumerable<Object[]> GetConpletedTasksByUserInTheLast30Days()
    {
        return repository.GetConpletedTasksByUserInTheLast30Days();
    }

}
