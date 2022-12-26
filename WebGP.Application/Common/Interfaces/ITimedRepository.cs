using WebGP.Application.Common.VM;

namespace WebGP.Application.Common.Interfaces
{
    public interface ITimedRepository
    {
        Task<IEnumerable<TimedVM>> GetTimedAsync();
    }
}
