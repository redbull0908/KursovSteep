namespace DAL.Interfaces
{
    public interface IRepository<T> where T : IModel
    {
        void Create(T item);

        void Delete(T item);

        void Update(T item);

        List<T> GetAll();
    }
}
