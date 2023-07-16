using System.Net;
using MyServer.Interfaces;
using MyServer.Utils;
using Request = MyServer.Types.Request;

namespace MyServer.Handlers;

public class StaticFilesHandler : IHandler
{
    private readonly string _path;

    public StaticFilesHandler(string path)
    {
        _path = path;
    }

    public void Handle(Stream networkStream)
    {
        Request request = Utils.Request.ParseStream(networkStream);

        string filePath = Path.Combine(_path, request.Path.Substring(1));

        if (!File.Exists(filePath))
        {
            Response.WriteStatus(HttpStatusCode.NotFound, networkStream);
        }
        else
        {
            Response.WriteStatus(HttpStatusCode.OK, networkStream);
            using FileStream fileStream = File.OpenRead(filePath);
            fileStream.CopyTo(networkStream);
        }
    }
}