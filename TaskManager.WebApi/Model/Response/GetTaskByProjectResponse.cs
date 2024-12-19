
namespace TaskManager.WebApi.Model.Response;
public class GetTaskByProjectResponse
{
    public Guid Id { get; set; }
    public String Title { get; set; }

    public String Description { get; set; }

    public DateTime DueDateTime { get; set; }

    public TaskManager.Model.TaskStatus Status { get; set; }
    public TaskManager.Model.TaskPriority Priority { get; set; }
}