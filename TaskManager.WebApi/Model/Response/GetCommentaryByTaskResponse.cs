
namespace TaskManager.WebApi.Model.Response;
public class GetCommentaryByTaskResponse : CommentaryResponse
{
    public String ModifiedBy { get; set; }
    public DateTime ModifiedAt { get; set; }
}