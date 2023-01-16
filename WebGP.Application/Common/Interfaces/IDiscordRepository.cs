using WebGP.Application.Common.VM;

namespace WebGP.Application.Common.Interfaces
{
    public interface IDiscordRepository
    {
        Task<IEnumerable<DiscordVM>> GetDiscordListAsync();
    }
}
