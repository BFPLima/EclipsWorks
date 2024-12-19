namespace TaskManager.Model;

public class BaseModel : TaskManagerModel
{
    public User CreatedBy { get; set; }
    public User ModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

}

