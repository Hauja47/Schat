namespace Schat.Domain.Interfaces;

using System.Linq.Expressions;
using Entities;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetById(Guid id);
    Task<IEnumerable<T>> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);
}