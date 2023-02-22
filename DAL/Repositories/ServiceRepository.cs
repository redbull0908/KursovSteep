using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    internal class ServiceRepository : IRepository<Service>
    {
        public AppContext Data { get; set; }

        public ServiceRepository(AppContext data)
        {
            Data = data;
        }

        public void Create(Service item)
        {
            Data.Services.Add(item);
            Data.SaveChanges();
        }

        public void Delete(Service item)
        {
            Data.Services.Remove(item);
            Data.SaveChanges();
        }

        public List<Service> GetAll() => Data.Services.AsNoTracking().ToList();

        public void Update(Service item)
        {
            Data.Services.Update(item);
            Data.SaveChanges();
        }
    }
}
