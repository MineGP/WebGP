using System.Collections.Concurrent;
using WebGP.Application.Common.Interfaces;

namespace WebGP.Infrastructure.Identity;

public class MemoryTimedStorage : ITimedStorage
{
    private class MemoryFile
    {
        private readonly byte[] bytes;
        private readonly DateTime removeTime;

        public MemoryFile(byte[] bytes, DateTime removeTime)
        {
            this.bytes = bytes;
            this.removeTime = removeTime;
        }
        public MemoryFile(byte[] bytes, TimeSpan timeout) : this(bytes, DateTime.Now.Add(timeout)) { }

        public bool IsRemove(DateTime now) => now > removeTime;
        public Stream OpenRead() => new MemoryStream(bytes, false);
    }

    private readonly ConcurrentDictionary<string, MemoryFile> files = new ConcurrentDictionary<string, MemoryFile>();
    private void ClearFilesTick()
    {
        List<KeyValuePair<string, MemoryFile>> removeList = new();
        DateTime now = DateTime.Now;

        foreach (var kv in files)
            if (kv.Value.IsRemove(now))
                removeList.Add(kv);

        foreach (var kv in removeList)
            files.TryRemove(kv);
    }

    public Task<Stream?> ReadFileAsync(string fileName, bool delete, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ClearFilesTick();
        cancellationToken.ThrowIfCancellationRequested();
        Stream? stream = (delete
            ? files.TryRemove(fileName, out MemoryFile? file)
            : files.TryGetValue(fileName, out file)) ? file.OpenRead() : null;
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(stream);
    }
    public async Task WriteFileAsync(string fileName, Stream data, TimeSpan timeout, CancellationToken cancellationToken)
    {
        ClearFilesTick();
        using MemoryStream stream = new MemoryStream();
        await data.CopyToAsync(stream, cancellationToken);
        stream.Seek(0, SeekOrigin.Begin);
        files[fileName] = new MemoryFile(stream.ToArray(), timeout);
    }
}
