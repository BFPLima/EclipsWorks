using TaskManager.IRepository;
using TaskManager.Model;

namespace TaskManager.Service;


public class ServiceOperationResult
{
    public ServiceOperationResult(String message, bool isSuccessful)
    {
        Message = message;
        Successful = isSuccessful;
    }
    public String Message { get; private set; }

    public bool Successful { get; private set; }
}
