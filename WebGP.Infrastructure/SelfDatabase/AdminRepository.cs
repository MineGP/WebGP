using Microsoft.EntityFrameworkCore;
using WebGP.Application.Common.Interfaces;
using WebGP.Domain.SelfEntities;

namespace WebGP.Infrastructure.SelfDatabase;

public class AdminRepository : RepositoryAsync<Admin>, IAdminRepository
{
    public AdminRepository(SelfDbContext dbContext) : base(dbContext) { }

    public async ValueTask<Admin?> GetByTokenAsync(string token)
    {
        return await DbContext.Admins.Where(a => a.Token == token).SingleOrDefaultAsync();
    }
}

