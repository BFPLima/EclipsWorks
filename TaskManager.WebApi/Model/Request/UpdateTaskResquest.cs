
namespace TaskManager.WebApi.Model.Request;
public class UpdateTaskResquest
{
    public String Title { get; set; }
    public String Description { get; set; }
    public DateTime DueDateTime { get; set; }
    public TaskManager.Model.TaskStatus Status { get; set; }
}