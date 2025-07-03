namespace UrlShortener.Domain.Interfaces.Repositories;

public interface IRepository<T, in TId> where T : class
{
    Task<T?> GetByIdAsync(TId id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    Task DeleteAsync(TId id);
}