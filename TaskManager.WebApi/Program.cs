
namespace TaskManager.WebApi;

using Microsoft.EntityFrameworkCore;
using TaskManager.EFRepository;
using TaskManager.IRepository;
using TaskManager.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddDbContext<EFRepository.TaskManagerContext>(opt =>
        opt.UseInMemoryDatabase("TaskManagerDB"));

        builder.Services.AddScoped<IProjectRepository, ProjectEFRepository>();
        builder.Services.AddScoped<IUserRepository, UserEFRepository>();
        builder.Services.AddScoped<ITaskRepository, TaskEFRepository>();
        builder.Services.AddScoped<ITaskUpdateHistoryRepository, TaskUpdateHistoryEFRepository>();
        builder.Services.AddScoped<ITaskCommentaryRepository, TaskCommentaryEFRepository>();
        builder.Services.AddScoped<ProjectService>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<TaskService>();
        builder.Services.AddScoped<TaskCommentaryService>();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
