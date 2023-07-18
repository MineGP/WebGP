using Microsoft.EntityFrameworkCore;

namespace WebGP.Infrastructure.DataBase;

public class DonateDbContext : DbContext
{
    public DonateDbContext() { }
    public DonateDbContext(DbContextOptions<DonateDbContext> options) : base(options) { }
}
