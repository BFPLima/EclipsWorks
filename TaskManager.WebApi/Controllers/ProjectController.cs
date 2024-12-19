using Microsoft.AspNetCore.Mvc;
using TaskManager.Model;
using TaskManager.Service;
using TaskManager.WebApi.Model.Request;
using TaskManager.WebApi.Model.Response;
using TaskManager.WebApi.Utilities;

namespace TaskManager.WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{

    private ProjectService projectService;
    private UserService userService;

    private TaskService taskService;

    public ProjectController(ProjectService projectService,
                             UserService userService,
                             TaskService taskService)
    {
        this.projectService = projectService;
        this.userService = userService;
        this.taskService = taskService;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<GetProjectResponse>> GetProject(Guid id)
    {
        var project = projectService.Find(id);

        if (project == null)
        {
            return NotFound();
        }

        return new GetProjectResponse()
        {
            Id = project.Id,
            Title = project.Title,
            UserName = project.User.Name,
            UserId = project.User.Id.ToString()
        };
    }


    [HttpGet("user/{id}")]
    public async Task<ActionResult<IEnumerable<GetProjectByUserResponse>>> GetProjectsByUser(Guid id)
    {
        User user = userService.Find(id);

        if (user == null)
            return BadRequest("Usuário não encontrado");

        var list = new List<GetProjectByUserResponse>();

        foreach (var project in projectService.GetByUser(user))
        {
            list.Add(new GetProjectByUserResponse()
            {
                Id = project.Id,
                Title = project.Title
            });
        }

        return list;
    }


    [HttpPost]
    public async Task<ActionResult<CreateProjectResponse>> CreateProject(CreateProjectResquest createProjectRequest)
    {
        var user = Util.GetUser(Request, userService);

        var project = new Project()
        {
            Id = new Guid(),
            Title = createProjectRequest.Title,
            User = user,
            CreatedBy = user,
            ModifiedBy = user,
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now
        };

        projectService.Insert(project);


        return new CreateProjectResponse()
        {
            Id = project.Id,
            Title = project.Title,
            UserName = project.User.Name,
            UserId = project.User.Id.ToString()
        };
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateProjectResponse>> UpdatedProject(Guid id, UpdateProjectResquest updateProjectRequest)
    {

        var project = projectService.Find(id);

        if (project == null)
        {
            return NotFound();
        }

        var user = Util.GetUser(Request, userService);

        project.Title = updateProjectRequest.Title;
        project.ModifiedBy = user;
        project.ModifiedAt = DateTime.Now;

        projectService.Update(project);

        return new UpdateProjectResponse()
        {
            Id = project.Id,
            Title = project.Title,
            UserName = project.User.Name,
            UserId = project.User.Id.ToString()
        };
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProject(Guid id)
    {
        var project = projectService.Find(id);

        if (project == null)
        {
            return NotFound();
        }

        var serviceOperationResult = projectService.Delete(project);

        if (!serviceOperationResult.Successful)
        {
            return BadRequest(serviceOperationResult.Message);
        }

        return Ok();
    }


    // [HttpGet("{id}/task")]
    // public async Task<ActionResult<IEnumerable<GetTaskByProjectResponse>>> GetTasksByProject(Guid id)
    // {
    //     var project = projectService.Find(id);

    //     if (project == null)
    //         return BadRequest("Projeto não encontrado.");


    //     var list = new List<GetTaskByProjectResponse>();

    //     foreach (var task in taskService.GetByProject(project))
    //     {
    //         list.Add(new GetTaskByProjectResponse()
    //         {
    //             Id = task.Id,
    //             Description = task.Description,
    //             DueDateTime = task.DueDateTime,
    //             Status = task.Status,
    //             Title = task.Title,
    //             Priority = task.Priority
    //         });
    //     }

    //     return list;
    // }


    // [HttpPost("{id}/task")]
    // public async Task<ActionResult<CreateTaskResponse>> CreateTask(Guid id, [FromBody] CreateTaskResquest createTaskRequest)
    // {
    //     var project = projectService.Find(id);

    //     if (project == null)
    //         return BadRequest("Não foi possível criar a Tarefa pois o Projeto informado não encontrado.");

    //     var user = Util.GetUser(Request, userService);

    //     var now = DateTime.Now;

    //     var task = new TaskManager.Model.Task()
    //     {
    //         Title = createTaskRequest.Title,
    //         Description = createTaskRequest.Description,
    //         DueDateTime = createTaskRequest.DueDateTime,
    //         Status = createTaskRequest.Status,
    //         Priority = createTaskRequest.Priority,
    //         Project = project,
    //         CreatedBy = user,
    //         ModifiedBy = user,
    //         CreatedAt = now,
    //         ModifiedAt = now
    //     };

    //     var serviceOperationResult = taskService.Insert(task);

    //     if (!serviceOperationResult.Successful)
    //     {
    //         return BadRequest(serviceOperationResult.Message);
    //     }

    //     return Ok(new CreateTaskResponse()
    //     {
    //         Id = task.Id,
    //         Description = task.Description,
    //         DueDateTime = task.DueDateTime,
    //         Status = task.Status,
    //         Title = task.Title,
    //         Priority = task.Priority,
    //         Project = new GetProjectResponse()
    //         {
    //             Id = project.Id,
    //             Title = project.Title,
    //             UserName = project.User.Name,
    //             UserId = project.User.Id.ToString()
    //         }
    //     });
    // }


    // [HttpDelete("{id}/task/{taskId}")]
    // public async Task<ActionResult> DeleteTask(Guid id, Guid taskId)
    // {
    //     var project = projectService.Find(id);

    //     if (project == null)
    //         return BadRequest("Projeto não encontrado.");


    //     var task = taskService.Find(taskId);

    //     if (task == null)
    //         return BadRequest("Task não encontrada.");


    //     if (project.Id != task.Project.Id)
    //         return BadRequest("Projeto e Task não possuem relacionamento entre si.");

    //     taskService.Delete(task);

    //     return Ok();
    // }

    // [HttpPut("{id}/task/{taskId}")]
    // public async Task<ActionResult<UpdatedTaskResponse>> UpdateTask(Guid id, Guid taskId, UpdateTaskResquest updateTaskRequest)
    // {
    //     var project = projectService.Find(id);

    //     if (project == null)
    //         return BadRequest("Projeto não encontrado.");


    //     var task = taskService.Find(taskId);

    //     if (task == null)
    //         return BadRequest("Task não encontrada.");


    //     if (project.Id != task.Project.Id)
    //         return BadRequest("Projeto e Task não possuem relacionamento entre si.");


    //     var user = Util.GetUser(Request, userService);

    //     task.Title = updateTaskRequest.Title;
    //     task.Description = updateTaskRequest.Description;
    //     task.DueDateTime = updateTaskRequest.DueDateTime;
    //     task.Status = updateTaskRequest.Status;
    //     task.ModifiedBy = user;
    //     task.ModifiedAt = DateTime.Now;

    //     var serviceOperationResult = taskService.Update(task);

    //     if (!serviceOperationResult.Successful)
    //     {
    //         return BadRequest(serviceOperationResult.Message);
    //     }

    //     return Ok(new UpdatedTaskResponse()
    //     {
    //         Id = task.Id,
    //         Description = task.Description,
    //         DueDateTime = task.DueDateTime,
    //         Status = task.Status,
    //         Title = task.Title,
    //         Priority = task.Priority,
    //         Project = new GetProjectResponse()
    //         {
    //             Id = project.Id,
    //             Title = project.Title,
    //             UserName = project.User.Name,
    //             UserId = project.User.Id.ToString()
    //         }
    //     });
    // }


}
