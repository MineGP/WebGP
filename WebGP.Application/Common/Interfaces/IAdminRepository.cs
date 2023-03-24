using WebGP.Domain.SelfEntities;

namespace WebGP.Application.Common.Interfaces;
public interface IAdminRepository : IRepositoryAsync<Admin>
{
    ValueTask<Admin?> GetByTokenAsync(string token);
}
