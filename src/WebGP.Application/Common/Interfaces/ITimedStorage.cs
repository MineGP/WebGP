namespace WebGP.Application.Common.Interfaces;

public interface ITimedStorage
{
    Task<Stream?> ReadFileAsync(string fileName, bool delete, CancellationToken cancellationToken);
    Task WriteFileAsync(string fileName, Stream data, TimeSpan timeout, CancellationToken cancellationToken);
}
