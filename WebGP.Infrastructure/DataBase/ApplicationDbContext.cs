using Microsoft.EntityFrameworkCore;
using WebGP.Application.Common.Interfaces;

namespace WebGP.Infrastructure.DataBase;

public partial class DbContextGPO : BaseApplicationDbContext<DbContextGPO>, IContextGPO
{
    protected override void OnModelCreatingAbstract(ModelBuilder modelBuilder) => OnModelCreatingPartial(modelBuilder);
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
public partial class DbContextGPC : BaseApplicationDbContext<DbContextGPC>, IContextGPC
{
    protected override void OnModelCreatingAbstract(ModelBuilder modelBuilder) => OnModelCreatingPartial(modelBuilder);
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}