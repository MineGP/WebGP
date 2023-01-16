using Microsoft.EntityFrameworkCore;
using WebGP.Interfaces.DataBase.Self;

namespace WebGP.Models.DataBase.Self
{
    public class SqlContext : DbContext, ISqlContext
    {
        public DbSet<ClientAPI> Clients { get; set; } = null!;

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
