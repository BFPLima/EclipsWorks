
namespace TaskManager.WebApi.Model.Response;
public class UpdateCommentaryResponse : CommentaryResponse
{
    public String ModifiedBy { get; set; }
    public DateTime ModifiedAt { get; set; }
}