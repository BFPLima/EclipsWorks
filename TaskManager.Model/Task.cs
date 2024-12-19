namespace TaskManager.Model;

public class Task : BaseModel
{
    public String Title { get; set; }

    public String Description { get; set; }

    public TaskStatus Status { get; set; }

    public DateTime DueDateTime { get; set; }

    public Project Project { get; set; }

    public TaskPriority Priority { get; set; }

}

