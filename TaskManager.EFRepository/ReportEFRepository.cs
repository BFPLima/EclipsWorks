using Microsoft.EntityFrameworkCore;
using TaskManager.IRepository;
using TaskManager.Model;

namespace TaskManager.EFRepository;

public class ReportEFRepository : IReportRepository
{

    protected TaskManagerContext taskManagerContext;
    public ReportEFRepository(TaskManagerContext taskManagerContext)
    {
        this.taskManagerContext = taskManagerContext;
    }

    public IEnumerable<object[]> GetConpletedTasksByUserInTheLast30Days()
    {
        var dateTime30DaysAgo = DateTime.Now.AddDays(-30);

        var result = taskManagerContext.Tasks
                    .Include(t => t.CreatedBy)
                    .Where(t => t.Status == Model.TaskStatus.Completed && t.DueDateTime >= dateTime30DaysAgo)
                    .GroupBy(t => t.CreatedBy)
                    .Select(t => new object[] { t.Key.Name, t.Count() })
                    .ToList();

        return result;
    }
}
