using WebGP.Application.Common.Interfaces;

namespace WebGP.Java.Frames;

public record WriteFatalFrame(string Text) : IFrame
{
    public string FormatLine => $"F:{Text}";
    public bool IsRequired(IFrame? lastFrame) => true;
}
