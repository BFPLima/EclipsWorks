
using System;
using System.Net.Http.Json;
using FluentAssertions;
using TaskManager.WebApi.Model.Request;
using TaskManager.WebApi.Model.Response;

namespace TaskManager.Test.integration;


public class TaskControllerTest
{

    [Fact]
    public async Task CreateTaskTest()
    {
        var application = new TaskManagerWebApiFactory();

        var projectResponse = Util.CreateProject(application);

        var createTaskResquest = new CreateTaskResquest()
        {
            Title = "Título da Tarefa",
            Description = "Descrição da Tarefa",
            DueDateTime = DateTime.Now,
            Priority = Model.TaskPriority.Medium,
            Status = Model.TaskStatus.InProgress,
            ProjectId = projectResponse.Id
        };

        var client = application.CreateClient();

        var response = await client.PostAsJsonAsync("/api/task", createTaskResquest);

        response.EnsureSuccessStatusCode();

        var createTaskResponse = await response.Content.ReadFromJsonAsync<CreateTaskResponse>();
        createTaskResponse?.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task CreateTaskProjectNotFoundTest()
    {
        var application = new TaskManagerWebApiFactory();

        var createTaskResquest = new CreateTaskResquest()
        {
            Title = "Título da Tarefa",
            Description = "Descrição da Tarefa",
            DueDateTime = DateTime.Now,
            Priority = Model.TaskPriority.Medium,
            Status = Model.TaskStatus.InProgress,
            ProjectId = Guid.NewGuid()
        };

        var client = application.CreateClient();

        var response = await client.PostAsJsonAsync("/api/task", createTaskResquest);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateTaskNumberOfTaskExceedByProjectBadRequestTest()
    {
        var application = new TaskManagerWebApiFactory();

        var projectResponse = Util.CreateProject(application);

        var createTaskResquest = new CreateTaskResquest()
        {
            Title = "Título da Tarefa",
            Description = "Descrição da Tarefa",
            DueDateTime = DateTime.Now,
            Priority = Model.TaskPriority.Medium,
            Status = Model.TaskStatus.InProgress,
            ProjectId = projectResponse.Id
        };

        int numberToExeeced = 20;

        for (int i = 0; i <= numberToExeeced; i++)
        {
            var client = application.CreateClient();

            var response = await client.PostAsJsonAsync("/api/task", createTaskResquest);

            if (i == numberToExeeced)
            {
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            }
            else
            {
                response.EnsureSuccessStatusCode();
                var createTaskResponse = await response.Content.ReadFromJsonAsync<CreateTaskResponse>();
                createTaskResponse?.Id.Should().NotBeEmpty();
            }
        }

    }


    [Fact]
    public async Task DeleteTaskTest()
    {
        var application = new TaskManagerWebApiFactory();

        var projectResponse = Util.CreateProject(application);

        var task = Util.CreateTask(application, projectResponse.Id, Model.TaskStatus.Pending);

        var client = application.CreateClient();

        var response = await client.DeleteAsync($"/api/task/{task.Id}");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteTaskNotFoundTest()
    {
        var application = new TaskManagerWebApiFactory();

        var client = application.CreateClient();

        var response = await client.DeleteAsync($"/api/task/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task UpdateTaskTest()
    {
        var application = new TaskManagerWebApiFactory();

        var projectResponse = Util.CreateProject(application);

        var task = Util.CreateTask(application, projectResponse.Id, Model.TaskStatus.Pending);

        var client = application.CreateClient();

        var dueDateTime = DateTime.Now;

        var updateTaskRequest = new UpdateTaskResquest()
        {
            Description = "Nova descrição",
            DueDateTime = dueDateTime,
            Status = Model.TaskStatus.Completed,
            Title = "Novo Titulo"
        };

        var response = await client.PutAsJsonAsync($"/api/task/{task.Id}", updateTaskRequest);

        response.EnsureSuccessStatusCode();

        var updateTaskResponse = await response.Content.ReadFromJsonAsync<UpdatedTaskResponse>();
        updateTaskResponse?.DueDateTime.Should().Be(updateTaskRequest.DueDateTime);
        updateTaskResponse?.DueDateTime.Should().Be(updateTaskRequest.DueDateTime);
        updateTaskResponse?.Status.Should().Be(updateTaskRequest.Status);
        updateTaskResponse?.Title.Should().Be(updateTaskRequest.Title);
    }

    [Fact]
    public async Task UpdateTaskNotFoundTest()
    {
        var application = new TaskManagerWebApiFactory();

        var client = application.CreateClient();

        var updateTaskRequest = new UpdateTaskResquest()
        {
            Description = "Nova descrição",
            DueDateTime = DateTime.Now,
            Status = Model.TaskStatus.Completed,
            Title = "Novo Titulo"
        };

        var response = await client.PutAsJsonAsync($"/api/task/{Guid.NewGuid()}", updateTaskRequest);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaskHistoryTest()
    {
        var application = new TaskManagerWebApiFactory();

        var projectResponse = Util.CreateProject(application);

        var task = Util.CreateTask(application, projectResponse.Id, Model.TaskStatus.Pending);

        var client = application.CreateClient();

        for (int i = 0; i < 5; i++)
        {
            var updateTaskRequest = new UpdateTaskResquest()
            {
                Description = "Nova descrição",
                DueDateTime = DateTime.Now,
                Status = Model.TaskStatus.Completed,
                Title = "Novo Titulo"
            };

            var responseUpdateTaskRequest = await client.PutAsJsonAsync($"/api/task/{task.Id}", updateTaskRequest);

            responseUpdateTaskRequest.EnsureSuccessStatusCode();
        }

        var response = await client.GetAsync($"/api/task/{task.Id}/history");

        response.EnsureSuccessStatusCode();

        var getTaskHistoryByTaskResponse = await response.Content.ReadFromJsonAsync<IList<GetTaskHistoryByTaskResponse>>();
        getTaskHistoryByTaskResponse?.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetTaskHistoryNotFoundTest()
    {
        var application = new TaskManagerWebApiFactory();

        var client = application.CreateClient();

        var response = await client.GetAsync($"/api/task/{Guid.NewGuid()}/history");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}