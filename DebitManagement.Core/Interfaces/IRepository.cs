using DebitManagement.Data.Entities;

namespace DebitManagement.Data.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task Create(T entity);
    Task Update(Guid id, T entity);
    Task Delete(Guid id);
}