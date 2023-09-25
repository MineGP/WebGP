using WebGP.Application.Common.VM;

namespace WebGP.Application.Common.Interfaces;

public interface IOnlineRepository
{
    Task<OnlineVm?> GetOnlineByTimedIdAsync(ContextType database, int timedId, CancellationToken cancellationToken);
    Task<IDictionary<int, OnlineVm>> GetAllOnlineByTimedIdAsync(ContextType database, CancellationToken cancellationToken);
    Task<OnlineVm?> GetOnlineByUuidAsync(ContextType database, string uuid, CancellationToken cancellationToken);
    Task<IDictionary<string, OnlineVm>> GetAllOnlineByUuidAsync(ContextType database, CancellationToken cancellationToken);
    Task<OnlineVm?> GetOnlineByStaticIdAsync(ContextType database, uint staticId, CancellationToken cancellationToken);
    Task<IDictionary<uint, OnlineVm>> GetAllOnlineByStaticIdAsync(ContextType database, CancellationToken cancellationToken);

    Task<int> GetOnlineCountAsync(ContextType database, CancellationToken cancellationToken);
}