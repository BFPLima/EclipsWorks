using Moq;
using TaskManager.IRepository;
using TaskManager.Model;
using TaskManager.Service;

namespace TaskManager.Test.Unit.Service;

public class TaskServiceTest
{
    [Fact]
    public void GetByProjectTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        var project = new Project()
        {
            Title = "Project Title"
        };

        mockTaskRepository.Setup(o => o.GetByProject(project))
                                  .Returns(new List<Model.Task>(){
                                    new Model.Task(){
                                         Status =  Model.TaskStatus.Pending
                                    }
                                  });

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var tasks = taskService.GetByProject(project);

        Assert.NotEmpty(tasks);
    }

    [Fact]
    public void CreateSuccessfulTaskTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var taskToCreate = new Model.Task()
        {
            Title = "Task Title"
        };

        var taskCreated = new Model.Task()
        {
            Title = "Task Title",
            Id = Guid.NewGuid()
        };

        mockTaskRepository.Setup(o => o.Insert(taskToCreate))
                               .Returns(taskCreated);

        var serviceOperationResult = taskService.Insert(taskCreated);

        Assert.True(serviceOperationResult.Successful);
    }

    [Fact]
    public void CreateFaildTaskTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var taskToCreate = new Model.Task()
        {
            Title = "Task Title"
        };

        mockTaskRepository.Setup(o => o.Insert(taskToCreate))
                             .Throws(new Exception());


        var serviceOperationResult = taskService.Insert(taskToCreate);

        Assert.False(serviceOperationResult.Successful);
    }


    [Fact]
    public void CreateFaildTaskByExceedLimitTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var project = new Project()
        {
            Title = "Project Title"
        };

        var taskToCreate = new Model.Task()
        {
            Title = "Task Title",
            Project = project
        };

        mockTaskRepository.Setup(o => o.Insert(taskToCreate))
                           .Throws(new Exception());

        var listTasks = new List<Model.Task>();

        for (int i = 0; i < 21; i++)
        {
            listTasks.Add(new Model.Task()
            {
                Project = project
            });
        }

        mockTaskRepository.Setup(o => o.GetByProject(project))
                    .Returns(listTasks);


        var serviceOperationResult = taskService.Insert(taskToCreate);

        Assert.False(serviceOperationResult.Successful);
        Assert.True(string.Equals(serviceOperationResult.Message,
                             "Não é possível adicionar mais Tarefas ao Projeto pois a quantidade máxima permitida foi atingida."));
    }

    [Fact]
    public void UpdateSuccessfulTaskTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var task = new Model.Task()
        {
            Title = "Título da Task"
        };

        mockTaskRepository.Setup(o => o.HasModifications(task))
                             .Returns(false);


        var serviceOperationResult = taskService.Update(task);

        Assert.True(serviceOperationResult.Successful);
    }

    [Fact]
    public void UpdateSuccessfulTaskWithModificationsTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var task = new Model.Task()
        {
            Title = "Título da Task"
        };

        mockTaskRepository.Setup(o => o.HasModifications(task))
                             .Returns(true);

        var dicModifications = new Dictionary<string, (object, object)>();
        dicModifications.Add("Tile", ("Old Tiltle", "New Title"));

        mockTaskRepository.Setup(o => o.GetModifications(task))
                                   .Returns(dicModifications);

        var taskUpdateHistory = new TaskUpdateHistory(task, dicModifications);

        mockTaskUpdateHistoryRepository.Setup(o => o.Insert(taskUpdateHistory));

        var serviceOperationResult = taskService.Update(task);

        Assert.True(serviceOperationResult.Successful);
    }


    [Fact]
    public void UpdateFailedTaskTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var task = new Model.Task()
        {
            Title = "Título da Task"
        };

        mockTaskRepository.Setup(o => o.HasModifications(task))
                             .Returns(false);

        mockTaskRepository.Setup(o => o.Update(task))
       .Throws(new Exception());

        var serviceOperationResult = taskService.Update(task);

        Assert.False(serviceOperationResult.Successful);
    }


    [Fact]
    public void UpdateFailedTaskPriorityChangedTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var task = new Model.Task()
        {
            Title = "Título da Task"
        };

        mockTaskRepository.Setup(o => o.HasModifications(task))
                      .Returns(true);

        var dicModifications = new Dictionary<string, (object, object)>();
        dicModifications.Add("Priority", ("Old value", "New value"));

        mockTaskRepository.Setup(o => o.GetModifications(task))
                                   .Returns(dicModifications);

        mockTaskRepository.Setup(o => o.HasModifications(task))
                             .Returns(true);


        var serviceOperationResult = taskService.Update(task);

        Assert.False(serviceOperationResult.Successful);
        Assert.True(string.Equals(serviceOperationResult.Message,
                     "Não é permitido alterar a Prioriade de uma Tarefa."));
    }

    //fdfdf///

    [Fact]
    public void DeleteSuccessfulTaskTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        var task = new Model.Task()
        {
            Title = "Título da Tarefa"
        };

        mockTaskRepository.Setup(o => o.Delete(task));

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var serviceOperationResult = taskService.Delete(task);

        Assert.True(serviceOperationResult.Successful);
    }


    [Fact]
    public void DeleteFailedTaskTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        var task = new Model.Task()
        {
            Title = "Título da Tarefa"
        };

        mockTaskRepository.Setup(o => o.Delete(task))
        .Throws(new Exception());

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var serviceOperationResult = taskService.Delete(task);

        Assert.False(serviceOperationResult.Successful);
    }


    [Fact]
    public void FindTaskTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        var id = Guid.NewGuid();

        var task = new Model.Task()
        {
            Title = "Título da Tarefa",
            Id = id
        };

        mockTaskRepository.Setup(o => o.Find(id))
        .Returns(task);

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var taskReturned = taskService.Find(id);

        Assert.True(task.Equals(taskReturned));
    }


    [Fact]
    public void GetUpdatedHistoryByTaskTest()
    {
        var mockTaskRepository = new Mock<ITaskRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        var id = Guid.NewGuid();

        var task = new Model.Task()
        {
            Title = "Título da Tarefa",
            Id = id
        };

        var taskUpdatedHistoryList = new List<Model.TaskUpdateHistory>() {
            new TaskUpdateHistory(){
            Task = task
            }};

        mockTaskRepository.Setup(o => o.GetTaskUpdateHistoryByTask(task))
        .Returns(taskUpdatedHistoryList);

        TaskService taskService = new TaskService(mockTaskRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var taskUpdatedHistoryReturnedList = taskService.GetTaskUpdateHistoryByTask(task);

        Assert.NotEmpty(taskUpdatedHistoryReturnedList);
    }
}
