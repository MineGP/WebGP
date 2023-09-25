using WebGP.Application.Common.Interfaces;

namespace WebGP.Java.Frames;

public record WriteEndFrame(string Text) : IFrame
{
    public string FormatLine => $"E:{Text}";
    public bool IsRequired(IFrame? lastFrame) => true;
}
