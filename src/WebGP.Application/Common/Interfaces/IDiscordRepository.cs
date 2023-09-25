using WebGP.Application.Common.VM;

namespace WebGP.Application.Common.Interfaces;

public interface IDiscordRepository
{
    Task<IDictionary<long, DiscordVm>> GetAllDiscordsAsync(ContextType database, CancellationToken cancellationToken);
    Task<DiscordVm?> GetByDiscordIdAsync(ContextType database, long discordId, CancellationToken cancellationToken);
}