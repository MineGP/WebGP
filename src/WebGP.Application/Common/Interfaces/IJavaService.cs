namespace WebGP.Application.Common.Interfaces;

public interface IJavaService
{
    Task<bool> CheckVersion(string gameVersion, int buildVersion, CancellationToken cancellationToken);
    Task ClearVersion(string gameVersion, int buildVersion, CancellationToken cancellationToken);
    IAsyncEnumerable<IFrame> UpdateVersion(string gameVersion, int buildVersion, CancellationToken cancellationToken);
    IAsyncEnumerable<IFrame> ApplyVersion(string gameVersion, int buildVersion, Stream inputFile, CancellationToken cancellationToken);
    Task<Stream?> GetApplyVersion(string name, CancellationToken cancellationToken);
    Task<Stream?> GetSourceVersion(string gameVersion, int buildVersion, CancellationToken cancellationToken);
}
