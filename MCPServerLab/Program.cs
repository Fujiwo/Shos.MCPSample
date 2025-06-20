using MCPServerLab;
using ModelContextProtocol.Server;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMcpServer().WithTools<WeatherForecastTool>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapGet("/", () => "My MCP server.");
app.MapMcpSse();
app.Run();

[McpServerToolType, Description("天気を予報する")]
class WeatherForecastTool
{
    [McpServerTool, Description("指定した場所の天気を予報します。")]
    public static string GetWeatherForecast(
        [Description("天気を予報したいと都道府県名")]
        string location) => location switch {
            "北海道" => "晴れ",
            "東京都" => "曇り",
            "石川県" => "雨"  ,
            "福井県" => "雪"  ,
            _       => "晴か曇りか雨か雪か霙か霰か何か"
        };
}