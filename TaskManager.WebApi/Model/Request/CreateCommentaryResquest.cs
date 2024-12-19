using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TaskManager.WebApi.Model.Request;

public class CreateCommentaryResquest
{
    public String Comment { get; set; }

    public Guid TaskId { get; set; }

}