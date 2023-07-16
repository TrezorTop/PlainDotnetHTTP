namespace MyServer.Types;

internal record Request(string Path, HttpMethod Method);