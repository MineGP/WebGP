using Java.Mapper.DataLogger;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using WebGP.Application.Common.Interfaces;
using WebGP.Java.Frames;

namespace WebGP.Java.DataLogger;

public record JavaDataLogger : IDataLogger
{
    private readonly ConcurrentQueue<IFrame> frames = new ConcurrentQueue<IFrame>();

    public bool TryDequeueFrame([NotNullWhen(true)] out IFrame? frame) => frames.TryDequeue(out frame);

    public void Write(string text) => frames.Enqueue(new WriteBeginFrame(text));
    public void WriteEnd(string text) => frames.Enqueue(new WriteEndFrame(text));
    public void WriteError(Exception exception) => frames.Enqueue(new WriteFatalFrame(exception.Message));
    public void WriteError(string message) => frames.Enqueue(new WriteFatalFrame(message));
    public void WriteStep(string text) => frames.Enqueue(new WriteStepFrame("", text));
    public void WriteStep(string? key, string text) => frames.Enqueue(new WriteStepFrame(key ?? "", text));

    public void WriteLine(string text) => frames.Enqueue(new WriteLineFrame(text));
}
