namespace AppoimentsWorkShop.Services
{
    public interface IBaseService<T> where T : class
    {

        IQueryable<T> GetAllAsync(int pageNumber, int resultsPage);
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
       
        bool Exists(int Id);
    }
}
