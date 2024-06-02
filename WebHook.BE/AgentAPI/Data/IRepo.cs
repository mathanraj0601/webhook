namespace AgentAPI.Data
{
    public interface IRepo<T,K>
    {
        Task<IQueryable<T>> GetAll();
        Task<T> Get(K id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(K id);
    }
}
