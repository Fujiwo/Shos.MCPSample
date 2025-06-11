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

[McpServerToolType, Description("�V�C��\�񂷂�")]
class WeatherForecastTool
{
    [McpServerTool, Description("�w�肵���ꏊ�̓V�C��\�񂵂܂��B")]
    public static string GetWeatherForecast(
        [Description("�V�C��\�񂵂����Ɠs���{����")]
        string location) => location switch {
            "�k�C��" => "����",
            "�����s" => "�܂�",
            "�ΐ쌧" => "�J"  ,
            "���䌧" => "��"  ,
            _       => "�����܂肩�J���Ⴉ���ł�����"
        };
}