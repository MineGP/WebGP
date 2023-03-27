using WebGP.Application.Common.VM;

namespace WebGP.Application.Common.Interfaces;

public interface IDiscordRepository
{
    Task<IDictionary<long, DiscordVm>> GetAllDiscordsAsync(CancellationToken cancellationToken);
    Task<DiscordVm?> GetByDiscordIdAsync(long discordId, CancellationToken cancellationToken);
}