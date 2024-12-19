using Microsoft.EntityFrameworkCore;
using TaskManager.Model;
using Task = TaskManager.Model.Task;

namespace TaskManager.EFRepository;

public class TaskManagerContext : DbContext
{
    public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
           : base(options)
    {

    }
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Task> Tasks { get; set; } = null!;
    public DbSet<TaskUpdateHistory> TaskUpdateHistories { get; set; } = null!;
    public DbSet<TaskCommentary> TaskCommentaries { get; set; } = null!;
}
