using WebGP.Application.Common.VM;
using WebGP.Domain.Entities;

namespace WebGP.Application.Common.Interfaces
{
    public interface IOnlineRepository
    {
        Task<IEnumerable<OnlineVM>> GetOnlineListAsync();

        Task<IEnumerable<OnlineLog>> GetOnlineLogListByUserNameAsync(string userName, DateTime from, DateTime to);
        Task<IEnumerable<OnlineLog>> GetOnlineLogListByUuidAsync(string uuid, DateTime from, DateTime to);
        Task<IEnumerable<OnlineLog>> GetOnlineLogListByStaticIDAsync(uint staticID, DateTime from, DateTime to);

        Task<int> GetOnlineCountAsync();
    }
}
