using TaskManager.IRepository;
using TaskManager.Model;

namespace TaskManager.Service;


public class UserService : BaseSevice<User, IUserRepository>
{
    public UserService(IUserRepository repository) : base(repository)
    {

    }
}
