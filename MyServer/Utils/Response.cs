using System.Net;
using System.Text.Json;

namespace MyServer.Utils;

internal static class Response
{
    internal static void WriteStatus(HttpStatusCode httpStatusCode, Stream stream)
    {
        using StreamWriter streamWriter = new StreamWriter(stream, leaveOpen: true);

        streamWriter.WriteLine($"HTTP/1.0 {(int)httpStatusCode} {httpStatusCode}");
        streamWriter.WriteLine();
    }

    internal static void WritePayload(object response, Stream stream)
    {
        switch (response)
        {
            case string str:
            {
                using StreamWriter writer = new StreamWriter(stream, leaveOpen: true);
                writer.Write(str);
                break;
            }
            case byte[] buffer:
            {
                stream.Write(buffer, 0, buffer.Length);
                break;
            }
            default:
            {
                WritePayload(JsonSerializer.Serialize(response), stream);
                break;
            }
        }
    }
}