using Moq;
using TaskManager.IRepository;
using TaskManager.Model;
using TaskManager.Service;

namespace TaskManager.Test.Unit.Service;

public class ProjectServiceTest
{
    [Fact]
    public void CreateSuccessfulProjectTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var mockProjectRepository = new Mock<IProjectRepository>();

        var projectToCreate = new Project()
        {
            Title = "Project Title"
        };

        var projectToReturn = new Project()
        {
            Title = "Project Title",
            Id = Guid.NewGuid()
        };

        mockProjectRepository.Setup(o => o.Insert(projectToCreate))
                             .Returns(projectToReturn);

        ProjectService projectService = new ProjectService(mockProjectRepository.Object, taskService);

        var serviceOperationResult = projectService.Insert(projectToCreate);

        Assert.True(serviceOperationResult.Successful);
    }

    [Fact]
    public void CreateFaildProjectTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var mockProjectRepository = new Mock<IProjectRepository>();

        var projectToCreate = new Project()
        {
            Title = "Project Title"
        };

        var projectToReturn = new Project()
        {
            Title = "Project Title",
            Id = Guid.NewGuid()
        };

        mockProjectRepository.Setup(o => o.Insert(projectToCreate))
                             .Throws(new Exception());

        ProjectService projectService = new ProjectService(mockProjectRepository.Object, taskService);

        var serviceOperationResult = projectService.Insert(projectToCreate);

        Assert.False(serviceOperationResult.Successful);
    }

    [Fact]
    public void UpdateSuccessfulProjectTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var mockProjectRepository = new Mock<IProjectRepository>();

        var projectToUpdate = new Project()
        {
            Title = "Project Title"
        };

        mockProjectRepository.Setup(o => o.Insert(projectToUpdate))
                             .Returns(projectToUpdate);

        ProjectService projectService = new ProjectService(mockProjectRepository.Object, taskService);

        var serviceOperationResult = projectService.Update(projectToUpdate);

        Assert.True(serviceOperationResult.Successful);
    }

    [Fact]
    public void UpdateFaildProjectTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var mockProjectRepository = new Mock<IProjectRepository>();

        var projectToUpdate = new Project()
        {
            Title = "Project Title"
        };

        mockProjectRepository.Setup(o => o.Update(projectToUpdate))
                                .Throws(new Exception());

        ProjectService projectService = new ProjectService(mockProjectRepository.Object, taskService);

        var serviceOperationResult = projectService.Update(projectToUpdate);

        Assert.False(serviceOperationResult.Successful);
    }

    [Fact]
    public void DeleteSuccessfulProjectTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        var mockProjectRepository = new Mock<IProjectRepository>();

        var projectToDelete = new Project()
        {
            Title = "Project Title"
        };

        mockTaskRepository.Setup(o => o.GetByProject(projectToDelete))
                                  .Returns(new List<Model.Task>());

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        mockProjectRepository.Setup(o => o.Delete(projectToDelete));

        ProjectService projectService = new ProjectService(mockProjectRepository.Object, taskService);

        var serviceOperationResult = projectService.Delete(projectToDelete);

        Assert.True(serviceOperationResult.Successful);
    }


    [Fact]
    public void DeleteFailedProjectTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        var mockProjectRepository = new Mock<IProjectRepository>();

        var projectToDelete = new Project()
        {
            Title = "Project Title"
        };

        mockTaskRepository.Setup(o => o.GetByProject(projectToDelete))
                                  .Returns(new List<Model.Task>());

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        mockProjectRepository.Setup(o => o.Delete(projectToDelete))
                             .Throws(new Exception());

        ProjectService projectService = new ProjectService(mockProjectRepository.Object, taskService);

        var serviceOperationResult = projectService.Delete(projectToDelete);

        Assert.False(serviceOperationResult.Successful);
    }

    [Fact]
    public void DeleteFailedProjectByPendingTasksTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        var mockProjectRepository = new Mock<IProjectRepository>();

        var projectToDelete = new Project()
        {
            Title = "Project Title"
        };

        mockTaskRepository.Setup(o => o.GetByProject(projectToDelete))
                                  .Returns(new List<Model.Task>(){
                                    new Model.Task(){
                                         Status =  Model.TaskStatus.Pending
                                    }
                                  });

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        mockProjectRepository.Setup(o => o.Delete(projectToDelete));

        ProjectService projectService = new ProjectService(mockProjectRepository.Object, taskService);

        var serviceOperationResult = projectService.Delete(projectToDelete);

        Assert.False(serviceOperationResult.Successful);
        Assert.True(string.Equals(serviceOperationResult.Message,
                                  "Não é possível remover o Projeto pois existem Tarefas pendentes associadas a ele. Para tal, é necessário concluir as Tarefas pendentes ou removê-las."));


    }

    [Fact]
    public void FindProjectTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        var mockProjectRepository = new Mock<IProjectRepository>();

        var id = Guid.NewGuid();

        var project = new Project()
        {
            Title = "Project Title",
            Id = id
        };

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        mockProjectRepository.Setup(o => o.Find(id))
        .Returns(project);

        ProjectService projectService = new ProjectService(mockProjectRepository.Object, taskService);

        var projectReturned = projectService.Find(id);

        Assert.True(project.Equals(projectReturned));
    }


    [Fact]
    public void GetProjectsByUserTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        var mockProjectRepository = new Mock<IProjectRepository>();

        var user = new User()
        {
            Id = Guid.NewGuid()
        };

        var id = Guid.NewGuid();

        var project = new Project()
        {
            Title = "Project Title",
            Id = id,
            User = user
        };

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        mockProjectRepository.Setup(o => o.GetByUser(user))
                                .Returns(new List<Project>(){
                                    project
                                });

        ProjectService projectService = new ProjectService(mockProjectRepository.Object, taskService);

        var projectsReturned = projectService.GetByUser(user);

        Assert.True(projectsReturned.Count() == 1);
    }
}
