using Moq;
using TaskManager.IRepository;
using TaskManager.Model;
using TaskManager.Service;

namespace TaskManager.Test.Unit.Service;

public class TaskCommentaryServiceTest
{
    [Fact]
    public void GetByTaskTest()
    {
        var mockTaskCommentaryRepository = new Mock<ITaskCommentaryRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        var task = new Model.Task()
        {
            Title = "Project Title"
        };

        mockTaskCommentaryRepository.Setup(o => o.GetByTask(task))
                                  .Returns(new List<TaskCommentary>(){
                                    new TaskCommentary(){
                                         Task = task
                                    }
                                  });

        TaskCommentaryService taskCommentaryService = new TaskCommentaryService(mockTaskCommentaryRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var tasks = taskCommentaryService.GetByTask(task);

        Assert.NotEmpty(tasks);
    }

    [Fact]
    public void CreateSuccessfulTaskCommentaryTest()
    {
        var mockTaskCommentaryRepository = new Mock<ITaskCommentaryRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        TaskCommentaryService taskService = new TaskCommentaryService(mockTaskCommentaryRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var task = new Model.Task();

        var taskToCreate = new Model.TaskCommentary()
        {
            Task = task
        };

        var taskCreated = new Model.TaskCommentary()
        {
            Task = task,
            Id = Guid.NewGuid()
        };

        mockTaskUpdateHistoryRepository.Setup(o => o.Insert(new TaskUpdateHistory(taskToCreate)));

        mockTaskCommentaryRepository.Setup(o => o.Insert(taskToCreate))
                                     .Returns(taskCreated);

        var serviceOperationResult = taskService.Insert(taskCreated);

        Assert.True(serviceOperationResult.Successful);
    }

    [Fact]
    public void CreateFaildTaskTest()
    {
        var mockTaskCommentaryRepository = new Mock<ITaskCommentaryRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        TaskCommentaryService taskService = new TaskCommentaryService(mockTaskCommentaryRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var task = new Model.Task();

        var taskToCreate = new Model.TaskCommentary()
        {
            Task = task
        };

        mockTaskUpdateHistoryRepository.Setup(o => o.Insert(new TaskUpdateHistory(taskToCreate)));

        mockTaskCommentaryRepository.Setup(o => o.Insert(taskToCreate))
                             .Throws(new Exception());

        var serviceOperationResult = taskService.Insert(taskToCreate);

        Assert.False(serviceOperationResult.Successful);
    }

    [Fact]
    public void UpdateSuccessfulTaskCommentaryTest()
    {
        var mockTaskCommentaryRepository = new Mock<ITaskCommentaryRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        TaskCommentaryService taskService = new TaskCommentaryService(mockTaskCommentaryRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var task = new Model.Task();

        var taskToUpdate = new Model.TaskCommentary()
        {
            Task = task
        };

        var taskUpdated = new Model.TaskCommentary()
        {
            Task = task,
            Id = Guid.NewGuid()
        };

        mockTaskUpdateHistoryRepository.Setup(o => o.Insert(new TaskUpdateHistory(taskToUpdate)));

        mockTaskCommentaryRepository.Setup(o => o.Update(taskToUpdate))
                                     .Returns(taskUpdated);

        var serviceOperationResult = taskService.Update(taskToUpdate);

        Assert.True(serviceOperationResult.Successful);
    }


    [Fact]
    public void UpdateFaildTaskTest()
    {
        var mockTaskCommentaryRepository = new Mock<ITaskCommentaryRepository>();

        var mockTaskUpdateHistoryRepository = new Mock<ITaskUpdateHistoryRepository>();

        TaskCommentaryService taskService = new TaskCommentaryService(mockTaskCommentaryRepository.Object, mockTaskUpdateHistoryRepository.Object);

        var task = new Model.Task();

        var taskToCreate = new Model.TaskCommentary()
        {
            Task = task
        };

        mockTaskUpdateHistoryRepository.Setup(o => o.Insert(new TaskUpdateHistory(taskToCreate)));

        mockTaskCommentaryRepository.Setup(o => o.Update(taskToCreate))
                                     .Throws(new Exception());

        var serviceOperationResult = taskService.Update(taskToCreate);

        Assert.False(serviceOperationResult.Successful);
    }


}
