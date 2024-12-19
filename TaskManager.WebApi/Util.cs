
using Microsoft.Extensions.Primitives;
using TaskManager.Model;
using TaskManager.Service;

namespace TaskManager.WebApi.Utilities;

public class Util
{

    /// <summary>
    /// Este método é uma simulação de extração do USER no HEADER da requisição.
    /// Em um ambiante produtivo seria extraído do Token.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="userService"></param>
    /// <returns></returns>
    protected internal static User GetUser(HttpRequest request, UserService userService)
    {
        request.Headers.TryGetValue("USER_ROLE", out var userRole);

        var strUserRole = "MANAGER";

        if (!StringValues.IsNullOrEmpty(userRole))
        {
            strUserRole = userRole[0];
        }

        if (strUserRole == "MANAGER")
        {
            var userId = new Guid("ffc3859b-25a8-4834-99f6-f7f92f28ffda");
            var user = userService.Find(userId);
            if (user != null)
            {
                return user;
            }
            else
            {
                user = new User()
                {
                    Id = userId,
                    Name = "Usuário Gerente",
                    Type = UserType.Manager
                };
                userService.Insert(user);
                return user;
            }
        }
        else
        {
            var userId = new Guid("398e085a-70b3-45cd-83d2-6d06736f0638");
            var user = userService.Find(userId);
            if (user != null)
            {
                return user;
            }
            else
            {
                user = new User()
                {
                    Id = userId,
                    Name = "Usuário Analista",
                    Type = UserType.Analyst
                };
                userService.Insert(user);
                return user;
            }
        }
    }
}