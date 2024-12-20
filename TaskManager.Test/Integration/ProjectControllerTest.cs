
using System;
using System.Net.Http.Json;
using FluentAssertions;
using TaskManager.WebApi.Model.Request;
using TaskManager.WebApi.Model.Response;

namespace TaskManager.Test.integration;


public class ProjectControllerTest
{

    [Fact]
    public async Task CreateProjectTest()
    {
        var application = new TaskManagerWebApiFactory();
        var createProjectRequest = new CreateProjectResquest()
        {
            Title = "Título do Projeto"
        };

        var client = application.CreateClient();

        var response = await client.PostAsJsonAsync("/api/project", createProjectRequest);

        response.EnsureSuccessStatusCode();

        var createProjectResponse = await response.Content.ReadFromJsonAsync<CreateProjectResponse>();
        createProjectResponse?.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetProjectTest()
    {
        var application = new TaskManagerWebApiFactory();

        var projectResponse = Util.CreateProject(application);

        var client = application.CreateClient();

        var response = await client.GetAsync($"/api/project/{projectResponse.Id}");

        response.EnsureSuccessStatusCode();

        var getProjectResponse = await response.Content.ReadFromJsonAsync<GetProjectResponse>();

        getProjectResponse?.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetProjectNotFoundTest()
    {
        var application = new TaskManagerWebApiFactory();

        var client = application.CreateClient();

        var response = await client.GetAsync($"/api/project/{Guid.NewGuid().ToString()}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetProjectByUserTest()
    {
        var application = new TaskManagerWebApiFactory();

        var projectResponse = Util.CreateProject(application);

        var client = application.CreateClient();

        var response = await client.GetAsync($"/api/project/user/{projectResponse.UserId}");

        response.EnsureSuccessStatusCode();

        var projectsResponse = await response.Content.ReadFromJsonAsync<List<GetProjectByUserResponse>>();

        projectsResponse?.Should().NotBeEmpty();
    }


    [Fact]
    public async Task GetProjectByUserNotFoundTest()
    {
        var application = new TaskManagerWebApiFactory();

        var client = application.CreateClient();

        var response = await client.GetAsync($"/api/project/user/{Guid.NewGuid().ToString()}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateProjectNotFoundTest()
    {
        var application = new TaskManagerWebApiFactory();

        var client = application.CreateClient();

        var updateProjectResquest = new UpdateProjectResquest()
        {
            Title = "Título do Projeto"
        };

        var response = await client.PutAsJsonAsync($"/api/project/{Guid.NewGuid().ToString()}", updateProjectResquest);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateProjectTest()
    {
        var application = new TaskManagerWebApiFactory();

        var client = application.CreateClient();

        var projectResponse = Util.CreateProject(application);

        var updateProjectResquest = new UpdateProjectResquest()
        {
            Title = "Título do Projeto 2"
        };

        var response = await client.PutAsJsonAsync($"/api/project/{projectResponse.Id}", updateProjectResquest);

        response.EnsureSuccessStatusCode();

        var updateProjectResponse = response.Content.ReadFromJsonAsync<CreateProjectResponse>().Result;

        updateProjectResponse.Title.Should().Be(updateProjectResquest.Title);
    }

    [Fact]
    public async Task DeleteProjectTest()
    {
        var application = new TaskManagerWebApiFactory();

        var client = application.CreateClient();

        var projectResponse = Util.CreateProject(application);

        var response = await client.DeleteAsync($"/api/project/{projectResponse.Id}");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteProjectNotFoundTest()
    {
        var application = new TaskManagerWebApiFactory();

        var client = application.CreateClient();

        var response = await client.DeleteAsync($"/api/project/{Guid.NewGuid().ToString()}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task DeleteProjectShouldNotDeleteByPendingTaskTest()
    {
        var application = new TaskManagerWebApiFactory();

        var client = application.CreateClient();

        var projectResponse = Util.CreateProject(application);
        var taskResponse = Util.CreateTask(application, projectResponse.Id, Model.TaskStatus.Pending);

        var response = await client.DeleteAsync($"/api/project/{projectResponse.Id}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}