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
            return NotFound("Usuário não encontrado.");

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
}
