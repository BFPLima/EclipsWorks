
using System;
using System.Net.Http.Json;
using FluentAssertions;
using TaskManager.WebApi.Model.Request;
using TaskManager.WebApi.Model.Response;

namespace TaskManager.Test.integration;


public class RetporControllerTest
{

    [Fact]
    public async Task GetReportTest()
    {
        var application = new TaskManagerWebApiFactory();

        var projectResponse = Util.CreateProject(application);

        for (int i = 0; i < 5; i++)
        {
            Util.CreateTask(application, projectResponse.Id, Model.TaskStatus.Completed);
        }

        var client = application.CreateClient();

        var response = await client.GetAsync($"/api/report?reportName=ConpletedTasksByUserInTheLast30Days");

        response.EnsureSuccessStatusCode();

        var reportResponse = await response.Content.ReadFromJsonAsync<IList<Object[]>>();

        reportResponse?.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetReportNoDataDoDisplayTest()
    {
        var application = new TaskManagerWebApiFactory();

        var projectResponse = Util.CreateProject(application);

        for (int i = 0; i < 5; i++)
        {
            Util.CreateTask(application, projectResponse.Id, Model.TaskStatus.InProgress);
        }

        var client = application.CreateClient();

        var response = await client.GetAsync($"/api/report?reportName=ConpletedTasksByUserInTheLast30Days");

        response.EnsureSuccessStatusCode();

        var reportResponse = await response.Content.ReadFromJsonAsync<IList<Object[]>>();

        reportResponse?.Should().BeEmpty();
    }

    [Fact]
    public async Task GetReportNotFoundTest()
    {
        var application = new TaskManagerWebApiFactory();

        var client = application.CreateClient();

        var response = await client.GetAsync($"/api/report?reportName=XYZXPTO");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

}