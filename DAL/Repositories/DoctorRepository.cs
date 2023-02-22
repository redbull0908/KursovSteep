using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    internal class DoctorRepository : IRepository<Doctor>
    {
        public AppContext Data { get; set; }

        public DoctorRepository(AppContext data)
        {
            Data = data;
        }

        public void Create(Doctor item)
        {
            Data.Doctors.Add(item);
            Data.SaveChanges();
        }

        public void Delete(Doctor item)
        {
            Data.Doctors.Remove(item);
            Data.SaveChanges();
        }

        public List<Doctor> GetAll() => Data.Doctors.AsNoTracking().ToList();

        public void Update(Doctor item)
        {
            Data.Doctors.Update(item);
            Data.SaveChanges();
        }
    }
}
