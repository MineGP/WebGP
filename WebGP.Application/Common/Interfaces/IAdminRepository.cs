using WebGP.Domain.SelfEntities;

namespace WebGP.Application.Common.Interfaces;
public interface IAdminRepository : IRepositoryAsync<Admin>
{
    public ValueTask<Admin?> GetByTokenAsync(string token);
}
