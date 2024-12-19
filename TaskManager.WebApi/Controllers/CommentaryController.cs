using Microsoft.AspNetCore.Mvc;
using TaskManager.Model;
using TaskManager.Service;
using TaskManager.WebApi.Model.Request;
using TaskManager.WebApi.Model.Response;
using TaskManager.WebApi.Utilities;

namespace TaskManager.WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CommentaryController : ControllerBase
{

    private UserService userService;

    private TaskService taskService;
    private TaskCommentaryService taskCommentaryService;

    public CommentaryController(UserService userService,
                                TaskService taskService,
                                TaskCommentaryService taskCommentaryService)
    {
        this.userService = userService;
        this.taskService = taskService;
        this.taskCommentaryService = taskCommentaryService;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<GetCommentaryResponse>> Get(Guid id)
    {
        var commentary = taskCommentaryService.Find(id);

        if (commentary == null)
        {
            return NotFound();
        }

        return new GetCommentaryResponse()
        {
            Id = commentary.Id,
            Comment = commentary.Comment,
            CreatedBy = commentary.CreatedBy.Name,
            CreatedAt = commentary.CreatedAt,
            ModifiedBy = commentary.ModifiedBy.Name,
            ModifiedAt = commentary.ModifiedAt,
            TaskTitle = commentary.Task.Title,
            TaskId = commentary.Task.Id
        };
    }
    [HttpGet("task/{id}")]
    public async Task<ActionResult<IEnumerable<GetCommentaryByTaskResponse>>> GetByTask(Guid id)
    {
        var task = taskService.Find(id);

        if (task == null)
        {
            return NotFound("Tarefa não encontrada.");
        }

        var listCommentaries = taskCommentaryService.GetByTask(task);

        var list = new List<GetCommentaryByTaskResponse>();

        foreach (var commentary in listCommentaries)
        {
            list.Add(new GetCommentaryByTaskResponse()
            {
                Id = commentary.Id,
                Comment = commentary.Comment,
                CreatedBy = commentary.CreatedBy.Name,
                CreatedAt = commentary.CreatedAt,
                ModifiedBy = commentary.ModifiedBy.Name,
                ModifiedAt = commentary.ModifiedAt
            });
        }

        return list;
    }

    [HttpPost]
    public async Task<ActionResult<CreateCommentaryResponse>> Create(CreateCommentaryResquest createCommentaryResquest)
    {
        var task = taskService.Find(createCommentaryResquest.TaskId);

        if (task == null)
        {
            return NotFound("Tarefa não encontrada.");
        }

        var user = Util.GetUser(Request, userService);

        var now = DateTime.Now;

        var taskCommentary = new TaskCommentary()
        {
            Id = new Guid(),
            Comment = createCommentaryResquest.Comment,
            CreatedBy = user,
            ModifiedBy = user,
            CreatedAt = now,
            ModifiedAt = now,
            Task = task
        };

        taskCommentaryService.Insert(taskCommentary);

        return new CreateCommentaryResponse()
        {
            Id = taskCommentary.Id,
            Comment = taskCommentary.Comment,
            CreatedBy = taskCommentary.CreatedBy.Name,
            CreatedAt = taskCommentary.CreatedAt
        };
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateCommentaryResponse>> Updated(Guid id, UpdateCommentaryResquest updateCommentaryResquest)
    {

        var taskCommentary = taskCommentaryService.Find(id);

        if (taskCommentary == null)
        {
            return NotFound();
        }

        var user = Util.GetUser(Request, userService);

        taskCommentary.Comment = updateCommentaryResquest.Comment;
        taskCommentary.ModifiedBy = user;
        taskCommentary.ModifiedAt = DateTime.Now;

        taskCommentaryService.Update(taskCommentary);

        return new UpdateCommentaryResponse()
        {
            Id = taskCommentary.Id,
            Comment = taskCommentary.Comment,
            CreatedBy = taskCommentary.CreatedBy.Name,
            CreatedAt = taskCommentary.CreatedAt,
            ModifiedBy = taskCommentary.ModifiedBy.Name,
            ModifiedAt = taskCommentary.ModifiedAt
        };
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var taskCommentary = taskCommentaryService.Find(id);

        if (taskCommentary == null)
        {
            return NotFound();
        }

        var serviceOperationResult = taskCommentaryService.Delete(taskCommentary);

        if (!serviceOperationResult.Successful)
        {
            return BadRequest(serviceOperationResult.Message);
        }

        return Ok();
    }
}
