namespace CoinApi.Services
{
    public interface IService<T>
    {
        T GetById(int id);
        List<T> GetAll();
        bool Delete(int id);
        List<T> GetBy(Func<T, bool> predicate);
        bool Update(T entity);
        T Create(T entity);
    }
}
