using System.Text;

namespace TaskManager.Model;

public class TaskUpdateHistory : TaskManagerModel
{

    public TaskUpdateHistory()
    {

    }
    public TaskUpdateHistory(Task task, IDictionary<string, (object, object)> modifications)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var modification in modifications)
        {
            sb.Append($"Atributo '{modification.Key}' " +
                        $"Valor antigo: '{modification.Value.Item1}' " +
                        $"Valor atualizado: '{modification.Value.Item2}'");

            sb.Append(System.Environment.NewLine);
        }

        History = sb.ToString();
        CreatedAt = task.ModifiedAt;
        CreatedBy = task.ModifiedBy;
        Task = task;
        Type = TaskUpdateHistoryType.Task;
    }

    public TaskUpdateHistory(TaskCommentary taskCommentary)
    {
        CreatedAt = taskCommentary.ModifiedAt;
        CreatedBy = taskCommentary.ModifiedBy;
        Task = taskCommentary.Task;
        Type = TaskUpdateHistoryType.Commentary;
        History = taskCommentary.Comment;
    }
    public String History { get; set; }

    public DateTime CreatedAt { get; set; }

    public User CreatedBy { get; set; }

    public Task Task { get; set; }

    public TaskUpdateHistoryType Type { get; set; }

}

