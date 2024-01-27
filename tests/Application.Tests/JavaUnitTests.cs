using WebGP.Application.Common.Interfaces;

namespace Application.Tests;

[TestFixture]
public class JavaUnitTests
{
    /*
    private ITimedStorage _timedStorage;
    private IJavaService _javaService;

    [SetUp]
    public void SetUp()
    {
        _timedStorage = new MemoryTimedStorage();
        _javaService = new JavaService(_timedStorage);
    }

    /*
    [Test]
    public async Task Java_UpdateCheckVersion()
    {
        await TestContext.Progress.WriteLineAsync("START");
        CancellationToken cancellationToken = CancellationToken.None;

        string gameVersion = "1.20.1";
        int buildVersion = 194;

        if (await _javaService.CheckVersion(gameVersion, buildVersion, cancellationToken))
            await _javaService.ClearVersion(gameVersion, buildVersion, cancellationToken);
        Assert.That(await _javaService.CheckVersion(gameVersion, buildVersion, cancellationToken), Is.False, "Version exist");
        await foreach (IFrame frame in _javaService.UpdateVersion(gameVersion, buildVersion, cancellationToken))
        {
            await TestContext.Progress.WriteLineAsync(frame.FormatLine);
        }
        await TestContext.Progress.WriteLineAsync("CHECK");
        Assert.That(await _javaService.CheckVersion(gameVersion, buildVersion, cancellationToken), Is.True, "Vesion not founded");
    }
    */
}
