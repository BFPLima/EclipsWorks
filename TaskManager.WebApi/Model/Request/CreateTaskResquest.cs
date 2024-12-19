using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TaskManager.WebApi.Model.Request;
public class CreateTaskResquest
{
    [Required]
    public String Title { get; set; }

    [Required]
    public String Description { get; set; }

    [Required]
    public DateTime DueDateTime { get; set; }

    [Range(1, 3)]
    public TaskManager.Model.TaskStatus Status { get; set; }

    [Range(1, 3)]
    public TaskManager.Model.TaskPriority Priority { get; set; }

    public Guid ProjectId { get; set; }
}