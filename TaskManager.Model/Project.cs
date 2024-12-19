namespace TaskManager.Model;

public class Project : BaseModel
{
    public String Title { get; set; }

    public User User { get; set; }

}
