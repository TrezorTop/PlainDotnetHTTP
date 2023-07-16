using System.Net;
using System.Net.Sockets;
using MyServer.Interfaces;

namespace MyServer;

public class Server
{
    private readonly IHandler _handler;

    public Server(IHandler handler)
    {
        _handler = handler;
    }

    public void Start()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();

        while (true)
        {
            using TcpClient client = listener.AcceptTcpClient();
            using NetworkStream networkStream = client.GetStream();

            _handler.Handle(networkStream);
        }
    }
}