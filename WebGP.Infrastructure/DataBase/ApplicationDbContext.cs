using Microsoft.EntityFrameworkCore;
using WebGP.Application.Common.Interfaces;

namespace WebGP.Infrastructure.DataBase;

public partial class DbContextGPO : BaseApplicationDbContext<DbContextGPO>, IContextGPO
{
    public DbContextGPO() { }
    public DbContextGPO(DbContextOptions<DbContextGPO> options) : base(options) { }

    protected override void OnModelCreatingAbstract(ModelBuilder modelBuilder) => OnModelCreatingPartial(modelBuilder);
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
public partial class DbContextGPC : BaseApplicationDbContext<DbContextGPC>, IContextGPC
{
    public DbContextGPC() { }
    public DbContextGPC(DbContextOptions<DbContextGPC> options) : base(options) { }

    protected override void OnModelCreatingAbstract(ModelBuilder modelBuilder) => OnModelCreatingPartial(modelBuilder);
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}