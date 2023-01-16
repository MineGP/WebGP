using WebGP.Domain.Entities;

namespace WebGP.Application.Common.Interfaces;

public interface IContext
{
    IQueryable<Discord> Discords { get; }
    IQueryable<Online> Onlines { get; }
    IQueryable<OnlineLog> OnlineLogs { get; }
    IQueryable<RoleWorkReadonly> RoleWorkReadonlies { get; }
    IQueryable<User> Users { get; }
}