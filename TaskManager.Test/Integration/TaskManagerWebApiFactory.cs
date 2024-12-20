using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TaskManager.EFRepository;
using TaskManager.WebApi;


namespace TaskManager.Test.integration;

public class TaskManagerWebApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(TaskManagerContext));

            services.AddDbContext<EFRepository.TaskManagerContext>(opt =>
                      opt.UseInMemoryDatabase("TaskManagerDB"));

            var dbContext = CreateDbContext(services);
            dbContext.Database.EnsureDeleted();
        });
    }

    private static TaskManagerContext CreateDbContext(IServiceCollection serviceCollection)
    {
        var seriviceProvider = serviceCollection.BuildServiceProvider();
        var scope = seriviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TaskManagerContext>();
        return dbContext;
    }

}

