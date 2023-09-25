using WebGP.Application.Common.Interfaces;

namespace WebGP.Infrastructure.SelfDatabase;

public class UnitOfWork : IUnitOfWork
{
    private readonly SelfDbContext _db;
    private bool _disposed;

    public UnitOfWork(SelfDbContext db)
    {
        _db = db;
        _adminRepository = new Lazy<IAdminRepository>(() => new AdminRepository(_db), true);
    }

    private readonly Lazy<IAdminRepository> _adminRepository;
    public IAdminRepository AdminRepository => _adminRepository.Value;

    public async Task<int> Commit()
    {
        return await _db.SaveChangesAsync();
    }

    public Task Rollback()
    {
        _db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        if (!_disposed) _db.Dispose();
        _disposed = true;

        GC.SuppressFinalize(this);
    }
}