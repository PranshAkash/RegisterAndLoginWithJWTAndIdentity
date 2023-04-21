
namespace Data.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        bool Update(T entity);
        Task<bool> DeleteAsync(T entity);
    }

    public interface IRow
    {

    }

    public interface IColumn
    {

    }

    public interface IFilter
    {

    }
}
