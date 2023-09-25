using Java.Mapper.App;
using System.Runtime.CompilerServices;
using WebGP.Application.Common.Interfaces;
using WebGP.Java.DataLogger;
using WebGP.Java.Frames;

namespace WebGP.Java;

public class JavaService : IJavaService
{
    private readonly ITimedStorage _timedStorage;
    public JavaService(ITimedStorage timedStorage)
        => _timedStorage = timedStorage;

    public async Task<bool> CheckVersion(string gameVersion, int buildVersion, CancellationToken cancellationToken)
    {
        await foreach ((string _gameVersion, int _buildVersion) in MainApp.GetAllVersions(cancellationToken))
            if (gameVersion == _gameVersion && buildVersion == _buildVersion)
                return true;
        return false;
    }

    private static async IAsyncEnumerable<IFrame> ExecuteFrameEnumerableAsync(Task task, JavaDataLogger logger, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (!task.IsCompleted)
        {
            await Task.Delay(100, cancellationToken);
            while (logger.TryDequeueFrame(out IFrame? frame))
                yield return frame;
        }

        await Task.Delay(100, cancellationToken);
        while (logger.TryDequeueFrame(out IFrame? frame))
            yield return frame;
    }
    public IAsyncEnumerable<IFrame> UpdateVersion(string gameVersion, int buildVersion, CancellationToken cancellationToken)
    {
        JavaDataLogger logger = new JavaDataLogger();
        Task task = MainApp.UnMapper(logger, gameVersion, buildVersion, null, cancellationToken).AsTask();
        return ExecuteFrameEnumerableAsync(task, logger, cancellationToken);
    }
    public IAsyncEnumerable<IFrame> ApplyVersion(string gameVersion, int buildVersion, Stream inputFile, CancellationToken cancellationToken)
    {
        JavaDataLogger logger = new JavaDataLogger();
        string outputFileName = Guid.NewGuid().ToString("N");
        async IAsyncEnumerable<IFrame> Loader([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            using MemoryStream outputStream = new MemoryStream();
            Task task = MainApp.Mapper(logger, inputFile, outputStream, gameVersion, buildVersion, cancellationToken).AsTask();
            await foreach (IFrame frame in ExecuteFrameEnumerableAsync(task, logger, cancellationToken))
                yield return frame;

            outputStream.Seek(0, SeekOrigin.Begin);
            await _timedStorage.WriteFileAsync($"java:{outputFileName}", outputStream, TimeSpan.FromMinutes(5), cancellationToken);
            yield return new ShareFrame(outputFileName);
        }
        return Loader(cancellationToken);
    }
    public Task<Stream?> GetApplyVersion(string name, CancellationToken cancellationToken)
        => _timedStorage.ReadFileAsync($"java:{name}", true, cancellationToken);
}