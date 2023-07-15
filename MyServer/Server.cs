using System.Net;
using System.Net.Sockets;

namespace MyServer;

public class Server
{
    public void Start()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();

            using NetworkStream networkStream = client.GetStream();
            using StreamReader reader = new StreamReader(networkStream);
            using StreamWriter writer = new StreamWriter(networkStream);

            string line = reader.ReadLine(); 
            while (line != string.Empty)
            {
                Console.WriteLine(reader.ReadLine());
            }

            writer.WriteLine("Hello World!");
        }
    }
}