using MyServer.Interfaces;

namespace PlainDotnetHTTP.Controllers;

public record User(string FirstName, string SecondName);

public class UsersController : IController
{
    public User[] Index()
    {
        Thread.Sleep(5000);
        
        return new[]
        {
            new User("User", "SecondName"),
            new User("Random", "Dude"),
            new User(Environment.CurrentManagedThreadId.ToString(), "Dude"),
        };
    }
}