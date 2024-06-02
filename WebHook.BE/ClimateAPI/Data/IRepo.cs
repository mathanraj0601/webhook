namespace ClimateAPI.Data
{
    public interface IRepo<T,K>
    {
        public Task<IQueryable<T>> GetAll();
        public Task<T?> Get(K id);
        public Task<T> Add(T entity);
        public Task<T> Update(T entity);
        public Task<T> Delete(K id);

    }
}
