using WebGP.Application.Common.Interfaces;

namespace WebGP.Java.Frames;

public record WriteLineFrame(string Text) : IFrame
{
    public string FormatLine => $"L:{Text}";
    public bool IsRequired(IFrame? lastFrame) => true;
}
