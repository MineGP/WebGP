using WebGP.Domain;

namespace WebGP.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAdminRepository AdminRepository { get; }
    Task<int> Commit();
    Task Rollback();
}