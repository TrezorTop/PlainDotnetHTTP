namespace MyServer.Utils;

internal static class Request
{
    internal static Types.Request ParseStream(Stream networkStream)
    {
        using StreamReader reader = new StreamReader(networkStream, leaveOpen: true);

        return ParseHeaders(reader.ReadLine());
    }

    private static Types.Request ParseHeaders(string header)
    {
        string[] split = header.Split(' '); // GET / HTTP/1.1

        return new Types.Request(split[1], new HttpMethod(split[0]));
    }
}