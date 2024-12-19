using Microsoft.EntityFrameworkCore;
using TaskManager.IRepository;
using TaskManager.Model;

namespace TaskManager.EFRepository;

public class UserEFRepository : EFRepositoryBase<User>, IUserRepository
{
    public UserEFRepository(TaskManagerContext taskManagerContext) : base(taskManagerContext)
    {

    }

    public override void Delete(User user)
    {
        taskManagerContext.Users.Remove(user);
        taskManagerContext.SaveChanges();
    }

    public override User Find(Guid id)
    {
        return taskManagerContext.Users.Find(id);
    }

    public override User Insert(User user)
    {
        taskManagerContext.Users.Add(user);
        taskManagerContext.SaveChanges();
        return user;
    }

    public override User Update(User user)
    {
        taskManagerContext.Users.Update(user);
        taskManagerContext.SaveChanges();
        return user;
    }
}
