namespace Schat.Infrastructure.Database.Repositories;

using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T>
    where T : BaseEntity
{
    private readonly AppDbContext context = context;
    protected readonly DbSet<T> db = context.Set<T>();

    public async Task<T?> GetById(Guid id) => await db.FindAsync(id);

    public async Task<IEnumerable<T>> GetAll() => await db.ToListAsync();

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) => db.Where(predicate);

    public void Add(T entity)
    {
        db.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        db.AddRange(entities);
    }

    public void Update(T entity)
    {
        db.Update(entity);
    }

    public void Delete(T entity)
    {
        db.Remove(entity);
    }
}