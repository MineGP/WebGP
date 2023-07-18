using Microsoft.EntityFrameworkCore;

namespace WebGP.Infrastructure.DataBase;

public class DonateDbContext : DbContext
{
    public DonateDbContext(DbContextOptions options) : base(options)
    {
    }

    public DonateDbContext()
    {
    }
}
