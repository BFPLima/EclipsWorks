
namespace TaskManager.WebApi.Model.Response;
public class CommentaryResponse
{
    public Guid Id { get; set; }
    public String Comment { get; set; }
    public String CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}