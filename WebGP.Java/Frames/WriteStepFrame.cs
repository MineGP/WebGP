using WebGP.Application.Common.Interfaces;

namespace WebGP.Java.Frames;

public record WriteStepFrame(string Key, string Text) : IFrame
{
    public string FormatLine => $"S:{Key.Replace(':', '-')}:{Text}";
    public bool IsRequired(IFrame? lastFrame) => string.IsNullOrWhiteSpace(Key) || lastFrame is not WriteStepFrame step || step.Key != Key;
}
