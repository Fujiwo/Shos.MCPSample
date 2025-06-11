using MCPServerLab;
using ModelContextProtocol.Server;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);
// MCP 関連のサービスの追加と EchoTool の追加
builder.Services.AddMcpServer().WithTools<WeatherForecastTool>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapGet("/", () => "My sample MCP server.");
// 先ほど定義した MapmcpSse メソッドを呼び出す
app.MapMcpSse();
app.Run();

// 天気予報を取得するツール
[McpServerToolType, Description("天気予報を取得するツール")]
class WeatherForecastTool
{
    [McpServerTool, Description("指定した場所の天気予報を返します。")]
    public static string GetWeatherForecast(
        [Description("天気を取得したい場所の名前")]
        string location) => location switch {
            "東京" => "晴れ",
            "大阪" => "曇り",
            "札幌" => "雪",
            _ => "空からカエルが降る異常気象",
        };
}