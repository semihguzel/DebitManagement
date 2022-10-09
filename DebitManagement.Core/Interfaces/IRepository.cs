using DebitManagement.Core.Entities;

namespace DebitManagement.Core.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(Guid id);
}