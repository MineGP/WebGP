using Java.Mapper.App;
using Java.Mapper.DataLogger;
using Java.Mapper.DataLogger.Frame;
using System.Runtime.CompilerServices;
using WebGP.Application.Common.Interfaces;
using IWebFrame = WebGP.Application.Common.Interfaces.IFrame;
using IJavaFrame = Java.Mapper.DataLogger.Frame.IFrame;

namespace WebGP.Java;

public class JavaService : IJavaService
{
    private readonly ITimedStorage _timedStorage;
    public JavaService(ITimedStorage timedStorage)
        => _timedStorage = timedStorage;

    public async Task<bool> CheckVersion(string gameVersion, int buildVersion, CancellationToken cancellationToken)
    {
        await foreach ((string _gameVersion, int _buildVersion) in PaperApp.GetAllVersions(cancellationToken))
            if (gameVersion == _gameVersion && buildVersion == _buildVersion)
                return true;
        return false;
    }
    public Task ClearVersion(string gameVersion, int buildVersion, CancellationToken cancellationToken)
        => PaperApp.ClearVersion(gameVersion, buildVersion, cancellationToken).AsTask();

    private static async IAsyncEnumerable<IWebFrame> ExecuteFrameEnumerableAsync(Task task, FrameDataLogger logger, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (!task.IsCompleted)
        {
            await Task.Delay(100, cancellationToken);
            while (logger.TryDequeueFrame(out IJavaFrame? frame))
                yield return new ProxyFrame(frame);
        }

        await Task.Delay(100, cancellationToken);
        while (logger.TryDequeueFrame(out IJavaFrame? frame))
            yield return new ProxyFrame(frame);
    }
    public IAsyncEnumerable<IWebFrame> UpdateVersion(string gameVersion, int buildVersion, CancellationToken cancellationToken)
    {
        FrameDataLogger logger = new FrameDataLogger();
        Task task = PaperApp.UnMapper(logger, gameVersion, buildVersion, null, cancellationToken).AsTask();
        return ExecuteFrameEnumerableAsync(task, logger, cancellationToken);
    }
    public IAsyncEnumerable<IWebFrame> ApplyVersion(string gameVersion, int buildVersion, Stream inputFile, CancellationToken cancellationToken)
    {
        FrameDataLogger logger = new FrameDataLogger();
        string outputFileName = Guid.NewGuid().ToString("N");
        async IAsyncEnumerable<IWebFrame> Loader([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            using MemoryStream outputStream = new MemoryStream();
            using MemoryStream inputStream = new MemoryStream();
            await inputFile.CopyToAsync(inputStream, cancellationToken);
            inputStream.Seek(0, SeekOrigin.Begin);
            File.WriteAllBytes("test.jar", inputStream.ToArray());
            inputStream.Seek(0, SeekOrigin.Begin);

            Exception? exception = null;

            async Task ErrorTask(CancellationToken cancellationToken)
            {
                try
                {
                    await PluginApp.Mapper(logger, inputStream, outputStream, gameVersion, buildVersion, cancellationToken);
                }
                catch (Exception e)
                {
                    exception = e;
                }
            }

            Task task = ErrorTask(cancellationToken);
            await foreach (IWebFrame frame in ExecuteFrameEnumerableAsync(task, logger, cancellationToken))
                yield return frame;

            if (exception is not null)
                throw new Exception("Error execute apply version", exception);

            outputStream.Seek(0, SeekOrigin.Begin);
            await _timedStorage.WriteFileAsync($"java:{outputFileName}", outputStream, TimeSpan.FromMinutes(5), cancellationToken);
            yield return new ProxyFrame(new ShareFrame(outputFileName));
        }
        return Loader(cancellationToken);
    }
    public Task<Stream?> GetApplyVersion(string name, CancellationToken cancellationToken)
        => _timedStorage.ReadFileAsync($"java:{name}", true, cancellationToken);
    public Task<Stream?> GetFullVersion(string gameVersion, int buildVersion, CancellationToken cancellationToken)
        => PaperApp.GetFullVersion(gameVersion, buildVersion, cancellationToken).AsTask();
}