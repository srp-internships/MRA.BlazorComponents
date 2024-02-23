
namespace MRA.BlazorComponents.SendEmail.Commands;

public class SendEmailCommand
{
    public required IEnumerable<string> Receivers { get; set; }
    public required string Subject { get; set; }
    public required string Text { get; set; }
}