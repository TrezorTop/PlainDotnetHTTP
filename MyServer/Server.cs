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

    public async Task Start()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();

        while (true)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();

            ProcessClient(client);
        }
    }

    private async Task ProcessClient(TcpClient client)
    {
        using (client)
        {
            await using NetworkStream networkStream = client.GetStream();

            await Task.Delay(5000);

            _handler.Handle(networkStream);
        }
    }
}