using Microsoft.EntityFrameworkCore;
using WebGP.Application.Common.Interfaces;
using WebGP.Domain;

namespace WebGP.Infrastructure.SelfDatabase;

public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class, IAuditableEntity
{
    protected readonly SelfDbContext DbContext;

    public RepositoryAsync(SelfDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public IQueryable<T> Entities => DbContext.Set<T>();

    public async Task<T> AddAsync(T entity)
    {
        await DbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public Task DeleteAsync(T entity)
    {
        DbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await DbContext
            .Set<T>()
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await DbContext.Set<T>().FindAsync(id);
    }

    public Task UpdateAsync(T entity)
    {
        var exist = DbContext.Set<T>().Find(entity.Id)!;
        DbContext.Entry(exist).CurrentValues.SetValues(entity);
        return Task.CompletedTask;
    }
}