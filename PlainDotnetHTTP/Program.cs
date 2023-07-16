using MyServer;
using MyServer.Handlers;

// Server server = new Server(
//     new StaticFilesHandler(Path.Combine(Environment.CurrentDirectory, "static"))
// );

Server server = new Server(
    new ControllersHandler(typeof(Program).Assembly)
);

await server.Start();