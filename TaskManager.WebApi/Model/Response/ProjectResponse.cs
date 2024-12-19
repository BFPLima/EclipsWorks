
namespace TaskManager.WebApi.Model.Response;

public abstract class ProjectResponse
{
    public Guid Id { get; set; }
    public String Title { get; set; }

    public String UserName { get; set; }

    public String UserId { get; set; }
}