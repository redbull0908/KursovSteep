namespace BLL.Interfaces
{
    public interface IServ<T> where T : IDTO
    {
        void Add(T item);
        void DeleteById(int id);
        void Update(T item);
        T FindById(int id);
        List<T> GetAll();
    }
}
