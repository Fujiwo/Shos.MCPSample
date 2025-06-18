using ModelContextProtocol.Client;

const string endPoint = "https://localhost:7052/sse";
var clientTransport = new SseClientTransport(new SseClientTransportOptions { Endpoint = new Uri(endPoint) });
IMcpClient client = await McpClientFactory.CreateAsync(clientTransport)
                                          .ConfigureAwait(false);

await Run(client, new Dictionary<string, object?> { ["location"] = "福井県" });

Console.ReadKey();

///*
client = await McpClientFactory.CreateAsync(
    new StdioClientTransport(new() {
        Command = "dotnet",
        Arguments = ["run", "--project", @"C:\DropBox\Dropbox\Source\GitHub\Repos\Shos.MCPSample\MCPServer.Console\MCPServer.Console.csproj"],
        Name = "TimeTools",
    }));

await Run(client);

Console.ReadKey();
// */

async Task Run(IMcpClient client, IReadOnlyDictionary<string, object?>? arguments = null) {
    // ツールの一覧からツールを取得
    foreach (var tool in await client.ListToolsAsync()) {
        Console.WriteLine($"{tool.Name} ({tool.Description})");
        // ツールを実行
        var response = await client.CallToolAsync(tool.Name, arguments);
        // レスポンスを表示
        foreach (var content in response.Content)
            Console.WriteLine($"tool.Name: {tool.Name}, content.Type: {content.Type}, content.Text: {content.Text}");
    }
}
