using Microsoft.AspNetCore.Mvc;
using TaskManager.Model;
using TaskManager.Service;
using TaskManager.WebApi.Model.Request;
using TaskManager.WebApi.Model.Response;
using TaskManager.WebApi.Utilities;

namespace TaskManager.WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{

    private ProjectService projectService;
    private UserService userService;

    private TaskService taskService;

    public TaskController(ProjectService projectService,
                          UserService userService,
                          TaskService taskService)
    {
        this.projectService = projectService;
        this.userService = userService;
        this.taskService = taskService;
    }

    [HttpGet("project/{id}")]
    public async Task<ActionResult<IEnumerable<GetTaskByProjectResponse>>> GetTasksByProject(Guid id)
    {
        var project = projectService.Find(id);

        if (project == null)
            return BadRequest("Projeto não encontrado.");


        var list = new List<GetTaskByProjectResponse>();

        foreach (var task in taskService.GetByProject(project))
        {
            list.Add(new GetTaskByProjectResponse()
            {
                Id = task.Id,
                Description = task.Description,
                DueDateTime = task.DueDateTime,
                Status = task.Status,
                Title = task.Title,
                Priority = task.Priority
            });
        }

        return list;
    }


    [HttpPost()]
    public async Task<ActionResult<CreateTaskResponse>> Create(CreateTaskResquest createTaskRequest)
    {
        var project = projectService.Find(createTaskRequest.ProjectId);

        if (project == null)
            return BadRequest("Não foi possível criar a Tarefa pois o Projeto informado não encontrado.");

        var user = Util.GetUser(Request, userService);

        var now = DateTime.Now;

        var task = new TaskManager.Model.Task()
        {
            Title = createTaskRequest.Title,
            Description = createTaskRequest.Description,
            DueDateTime = createTaskRequest.DueDateTime,
            Status = createTaskRequest.Status,
            Priority = createTaskRequest.Priority,
            Project = project,
            CreatedBy = user,
            ModifiedBy = user,
            CreatedAt = now,
            ModifiedAt = now
        };

        var serviceOperationResult = taskService.Insert(task);

        if (!serviceOperationResult.Successful)
        {
            return BadRequest(serviceOperationResult.Message);
        }

        return Ok(new CreateTaskResponse()
        {
            Id = task.Id,
            Description = task.Description,
            DueDateTime = task.DueDateTime,
            Status = task.Status,
            Title = task.Title,
            Priority = task.Priority,
            Project = new GetProjectResponse()
            {
                Id = project.Id,
                Title = project.Title,
                UserName = project.User.Name,
                UserId = project.User.Id.ToString()
            }
        });
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(Guid id)
    {
        var task = taskService.Find(id);

        if (task == null)
            return BadRequest("Task não encontrada.");

        taskService.Delete(task);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdatedTaskResponse>> UpdateTask(Guid id, UpdateTaskResquest updateTaskRequest)
    {
        var task = taskService.Find(id);

        if (task == null)
            return BadRequest("Task não encontrada.");


        var user = Util.GetUser(Request, userService);

        task.Title = updateTaskRequest.Title;
        task.Description = updateTaskRequest.Description;
        task.DueDateTime = updateTaskRequest.DueDateTime;
        task.Status = updateTaskRequest.Status;
        task.ModifiedBy = user;
        task.ModifiedAt = DateTime.Now;

        var serviceOperationResult = taskService.Update(task);

        if (!serviceOperationResult.Successful)
        {
            return BadRequest(serviceOperationResult.Message);
        }

        return Ok(new UpdatedTaskResponse()
        {
            Id = task.Id,
            Description = task.Description,
            DueDateTime = task.DueDateTime,
            Status = task.Status,
            Title = task.Title,
            Priority = task.Priority,
            Project = new GetProjectResponse()
            {
                Id = task.Project.Id,
                Title = task.Project.Title,
                UserName = task.Project.User.Name,
                UserId = task.Project.User.Id.ToString()
            }
        });
    }


    [HttpGet("{id}/history")]
    public async Task<ActionResult<IEnumerable<GetTaskHistoryByTaskResponse>>> GetTasksHistories(Guid id)
    {
        var task = taskService.Find(id);

        if (task == null)
            return BadRequest("Task não encontrada.");


        var list = new List<GetTaskHistoryByTaskResponse>();

        foreach (var taskHistory in taskService.GetTaskUpdateHistoryByTask(task))
        {
            list.Add(new GetTaskHistoryByTaskResponse()
            {
                History = taskHistory.History,
                CreatedAt = taskHistory.CreatedAt,
                CreatedBy = taskHistory.CreatedBy.Name,
                Type = taskHistory.Type
            });
        }

        return list;
    }
}