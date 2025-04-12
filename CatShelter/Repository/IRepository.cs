namespace CatShelter.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T? GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        void Save();
    }
}
