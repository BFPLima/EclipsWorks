namespace TaskManager.Model;

public class TaskCommentary : BaseModel
{
    public String Comment { get; set; }

    public Task Task { get; set; }

}

