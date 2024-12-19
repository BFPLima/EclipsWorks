
namespace TaskManager.WebApi.Model.Response;
public class GetCommentaryResponse : CommentaryResponse
{
    public String ModifiedBy { get; set; }
    public DateTime ModifiedAt { get; set; }
    public String TaskTitle { get; set; }
    public Guid TaskId { get; set; }
}