using System.Net;
using System.Reflection;
using MyServer.Interfaces;
using MyServer.Utils;
using Request = MyServer.Types.Request;

namespace MyServer.Handlers;

public class ControllersHandler : IHandler
{
    private readonly Dictionary<string, Func<object>> _controllersDictionary;

    public ControllersHandler(Assembly controllersAssembly)
    {
        _controllersDictionary = controllersAssembly
            .GetTypes()
            .Where(x => typeof(IController).IsAssignableFrom(x))
            .SelectMany(controller => controller.GetMethods().Select(
                method => new { Controller = controller, Method = method }
            ))
            .ToDictionary(
                key => GetPath(key.Controller, key.Method),
                value => GetEndpointMethod(value.Controller, value.Method)
            );
    }

    private Func<object> GetEndpointMethod(Type controller, MethodInfo method)
    {
        return () => method.Invoke(Activator.CreateInstance(controller), Array.Empty<object>());
    }

    private string GetPath(Type controller, MethodInfo method)
    {
        string name = controller.Name;
        if (name.EndsWith("controller", StringComparison.InvariantCultureIgnoreCase))
            name = name.Substring(0, name.Length - "controller".Length).ToLower();

        if (method.Name.Equals("Index", StringComparison.InvariantCultureIgnoreCase))
            return "/" + name;

        return "/" + name + "/" + method.Name;
    }

    public void Handle(Stream networkStream)
    {
        Request request = Utils.Request.ParseStream(networkStream);

        if (!_controllersDictionary.TryGetValue(request.Path, out var method))
            Response.WriteStatus(HttpStatusCode.NotFound, networkStream);
        else
        {
            Response.WriteStatus(HttpStatusCode.OK, networkStream);
            Response.WritePayload(method(), networkStream);
        }
    }
}