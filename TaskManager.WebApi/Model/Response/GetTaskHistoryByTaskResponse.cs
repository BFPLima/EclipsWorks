
using TaskManager.Model;

namespace TaskManager.WebApi.Model.Response;
public class GetTaskHistoryByTaskResponse
{
    public String History { get; set; }

    public DateTime CreatedAt { get; set; }

    public String CreatedBy { get; set; }

    public TaskUpdateHistoryType Type { get; set; }
}