
using System;
using System.Net.Http.Json;
using FluentAssertions;
using TaskManager.WebApi.Model.Request;
using TaskManager.WebApi.Model.Response;

namespace TaskManager.Test.integration;


public class CommentaryControllerTest
{

    [Fact]
    public async Task CreateCommentaryTest()
    {
        var application = new TaskManagerWebApiFactory();

        var projectResponse = Util.CreateProject(application);

        var taskResponse = Util.CreateTask(application, projectResponse.Id, Model.TaskStatus.InProgress);

        var createTaskResquest = new CreateCommentaryResquest()
        {
            Comment = "Comentário ...",
            TaskId = taskResponse.Id
        };

        var client = application.CreateClient();

        var response = await client.PostAsJsonAsync("/api/commentary", createTaskResquest);

        response.EnsureSuccessStatusCode();

        var createTaskCommentaryResponse = await response.Content.ReadFromJsonAsync<CreateCommentaryResponse>();
        createTaskCommentaryResponse?.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task CreateCommentaryTaskNotFoundTest()
    {
        var application = new TaskManagerWebApiFactory();

        var createTaskResquest = new CreateCommentaryResquest()
        {
            Comment = "Comentário ...",
            TaskId = Guid.NewGuid()
        };

        var client = application.CreateClient();

        var response = await client.PostAsJsonAsync("/api/commentary", createTaskResquest);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task GetCommentaryTest()
    {
        var application = new TaskManagerWebApiFactory();

        var project = Util.CreateProject(application);

        var task = Util.CreateTask(application, project.Id, Model.TaskStatus.Pending);

        var commentary = Util.CreateTaskCommentary(application, task.Id);

        var client = application.CreateClient();

        var response = await client.GetAsync($"/api/commentary/{commentary.Id}");

        response.EnsureSuccessStatusCode();

        var getCommentaryResponse = await response.Content.ReadFromJsonAsync<GetCommentaryResponse>();
        getCommentaryResponse?.Id.Should().NotBeEmpty();
    }


    [Fact]
    public async Task GetCommentaryNotFoundTest()
    {
        var application = new TaskManagerWebApiFactory();

        var client = application.CreateClient();

        var response = await client.GetAsync($"/api/commentary/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task GetCommentaryByTaskTest()
    {
        var application = new TaskManagerWebApiFactory();

        var project = Util.CreateProject(application);

        var task = Util.CreateTask(application, project.Id, Model.TaskStatus.Pending);

        var commentary = Util.CreateTaskCommentary(application, task.Id);

        var client = application.CreateClient();

        var response = await client.GetAsync($"/api/commentary/task/{task.Id}");

        response.EnsureSuccessStatusCode();

        var commentariesResponse = await response.Content.ReadFromJsonAsync<IList<GetCommentaryByTaskResponse>>();
        commentariesResponse?.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetCommentaryByTaskNotFoundTest()
    {
        var application = new TaskManagerWebApiFactory();

        var client = application.CreateClient();

        var response = await client.GetAsync($"/api/commentary/task/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task UpdateCommentaryTest()
    {
        var application = new TaskManagerWebApiFactory();

        var project = Util.CreateProject(application);

        var task = Util.CreateTask(application, project.Id, Model.TaskStatus.Pending);

        var commentary = Util.CreateTaskCommentary(application, task.Id);

        var client = application.CreateClient();

        var updateCommentaryResquest = new UpdateCommentaryResquest()
        {
            Comment = "Novo comentário ....."
        };

        var response = await client.PutAsJsonAsync($"/api/commentary/{commentary.Id}", updateCommentaryResquest);

        response.EnsureSuccessStatusCode();

        var commentariesResponse = await response.Content.ReadFromJsonAsync<UpdateCommentaryResponse>();
        commentariesResponse?.Comment.Should().Be(updateCommentaryResquest.Comment);
    }

    [Fact]
    public async Task UpdateCommentaryNotFoundTest()
    {
        var application = new TaskManagerWebApiFactory();

        var client = application.CreateClient();

        var updateCommentaryResquest = new UpdateCommentaryResquest()
        {
            Comment = "Novo comentário ....."
        };

        var response = await client.PutAsJsonAsync($"/api/commentary/{Guid.NewGuid()}", updateCommentaryResquest);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteCommentaryTest()
    {
        var application = new TaskManagerWebApiFactory();

        var project = Util.CreateProject(application);

        var task = Util.CreateTask(application, project.Id, Model.TaskStatus.Pending);

        var commentary = Util.CreateTaskCommentary(application, task.Id);

        var client = application.CreateClient();

        var response = await client.DeleteAsync($"/api/commentary/{commentary.Id}");

        response.EnsureSuccessStatusCode();
    }


    [Fact]
    public async Task DeleteCommentaryNotFoundTest()
    {
        var application = new TaskManagerWebApiFactory();

        var client = application.CreateClient();

        var response = await client.DeleteAsync($"/api/commentary/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

}