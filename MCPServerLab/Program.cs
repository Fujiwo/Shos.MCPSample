using MCPServerLab;
using ModelContextProtocol.Server;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);
// MCP �֘A�̃T�[�r�X�̒ǉ��� EchoTool �̒ǉ�
builder.Services.AddMcpServer().WithTools<WeatherForecastTool>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapGet("/", () => "My sample MCP server.");
// ��قǒ�`���� MapmcpSse ���\�b�h���Ăяo��
app.MapMcpSse();
app.Run();

// �V�C�\����擾����c�[��
[McpServerToolType, Description("�V�C�\����擾����c�[��")]
class WeatherForecastTool
{
    [McpServerTool, Description("�w�肵���ꏊ�̓V�C�\���Ԃ��܂��B")]
    public static string GetWeatherForecast(
        [Description("�V�C���擾�������ꏊ�̖��O")]
        string location) => location switch {
            "����" => "����",
            "���" => "�܂�",
            "�D�y" => "��",
            _ => "�󂩂�J�G�����~��ُ�C��",
        };
}