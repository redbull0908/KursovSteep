using DAL.Interfaces;
using System.Data.Entity;

namespace DAL.Repositories
{
    internal class SubscribeRepository : IRepository<Subscribe>
    {
        public AppContext Data { get; set; }

        public SubscribeRepository(AppContext data)
        {
            Data = data;
        }

        public void Create(Subscribe item)
        {
            Data.Subscribes.Add(item);
            Data.SaveChanges();
        }

        public void Delete(Subscribe item)
        {
            Data.Subscribes.Remove(item);
            Data.SaveChanges();
        }

        public List<Subscribe> GetAll() => Data.Subscribes.AsNoTracking().ToList();

        public void Update(Subscribe item)
        {
            Data.Subscribes.Update(item);
            Data.SaveChanges();
        }
    }
}
