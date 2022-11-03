using CoinApi.Context;

namespace CoinApi.Services
{
    public abstract class Service<T> : IService<T>
    {
        protected readonly CoinApiContext context;

        protected Service(CoinApiContext context)
        {
            this.context = context;
        }

        public abstract T Create(T entity);
        public abstract bool Delete(int id);
        public abstract List<T> GetAll();

        public abstract List<T> GetBy(Func<T, bool> predicate);

        public abstract T GetById(int id);
        public abstract bool Update(T entity);
    }
}
