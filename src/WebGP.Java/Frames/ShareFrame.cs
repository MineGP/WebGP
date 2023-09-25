using WebGP.Application.Common.Interfaces;

namespace WebGP.Java.Frames;
public record ShareFrame(string Name) : IFrame
{
    public string FormatLine => $"N:{Name}";
    public bool IsRequired(IFrame? lastFrame) => true;
}
