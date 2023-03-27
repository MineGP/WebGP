using WebGP.Application.Common.VM;
using WebGP.Domain.Entities;

namespace WebGP.Application.Common.Interfaces;

public interface IOnlineRepository
{
    Task<OnlineVm?> GetOnlineByTimedIdAsync(int timedId, CancellationToken cancellationToken);
    Task<IDictionary<int, OnlineVm>> GetAllOnlineByTimedIdAsync(CancellationToken cancellationToken);
    Task<OnlineVm?> GetOnlineByUuidAsync(string uuid, CancellationToken cancellationToken);
    Task<IDictionary<string, OnlineVm>> GetAllOnlineByUuidAsync(CancellationToken cancellationToken);
    Task<OnlineVm?> GetOnlineByStaticIdAsync(uint staticId, CancellationToken cancellationToken);
    Task<IDictionary<uint, OnlineVm>> GetAllOnlineByStaticIdAsync(CancellationToken cancellationToken);

    Task<int> GetOnlineCountAsync(CancellationToken cancellationToken);
}