namespace WebGP.Application.Common.Interfaces;

public interface IFrame
{
    string FormatLine { get; }
    bool IsRequired(IFrame? lastFrame);
}
