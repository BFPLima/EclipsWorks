


using System.Net.Http.Json;
using TaskManager.WebApi.Model.Request;
using TaskManager.WebApi.Model.Response;

namespace TaskManager.Test.integration;

internal class Util
{

    internal static CreateProjectResponse CreateProject(TaskManagerWebApiFactory application)
    {
        var client = application.CreateClient();

        var response = client.PostAsJsonAsync("/api/project", new CreateProjectResquest()
        {
            Title = "Título do Projeto"
        }).Result;

        return response.Content.ReadFromJsonAsync<CreateProjectResponse>().Result;
    }

    internal static CreateTaskResponse CreateTask(TaskManagerWebApiFactory application, Guid projectId, Model.TaskStatus status)
    {
        var client = application.CreateClient();

        var response = client.PostAsJsonAsync("/api/task", new CreateTaskResquest()
        {
            Title = "Título do Projeto",
            Description = "Descricão da Tarefa",
            DueDateTime = DateTime.Now,
            Priority = Model.TaskPriority.Low,
            Status = status,
            ProjectId = projectId
        }).Result;

        return response.Content.ReadFromJsonAsync<CreateTaskResponse>().Result;
    }

    internal static CreateCommentaryResponse CreateTaskCommentary(TaskManagerWebApiFactory application, Guid taskId)
    {
        var client = application.CreateClient();

        var response = client.PostAsJsonAsync("/api/commentary", new CreateCommentaryResquest()
        {
            Comment = "Comentário ...",
            TaskId = taskId
        }).Result;

        return response.Content.ReadFromJsonAsync<CreateCommentaryResponse>().Result;
    }
}