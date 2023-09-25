using WebGP.Application.Common.Interfaces;

namespace WebGP.Java.Frames;

public record WriteBeginFrame(string Text) : IFrame
{
    public string FormatLine => $"B:{Text}";
    public bool IsRequired(IFrame? lastFrame) => true;
}
