using DAL.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    internal class ServiceCategoryRepository:IRepository<ServiceCategory>
    {
        public AppContext Data { get; set; }

        public ServiceCategoryRepository(AppContext data)
        {
            Data = data;
        }

        public void Create(ServiceCategory item)
        {
            Data.ServiceCategories.Add(item);
            Data.SaveChanges();
        }

        public void Delete(ServiceCategory item)
        {
            Data.ServiceCategories.Add(item);
            Data.SaveChanges();
        }

        public void Update(ServiceCategory item)
        {
            Data.ServiceCategories.Add(item);
            Data.SaveChanges();
        }

        public List<ServiceCategory> GetAll() => Data.ServiceCategories.AsNoTracking().ToList();
    }
}
