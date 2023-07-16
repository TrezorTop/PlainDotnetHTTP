namespace MyServer.Interfaces;

public interface IHandler
{
    void Handle(Stream stream);
}