using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    internal class ScheduleRepository : IRepository<Schedule>
    {
        public AppContext Data { get; set; }

        public ScheduleRepository(AppContext data)
        {
            Data = data;
        }

        public void Create(Schedule item)
        {
            Data.Schedules.Add(item);
            Data.SaveChanges();
        }

        public void Delete(Schedule item)
        {
            Data.Schedules.Remove(item);
            Data.SaveChanges();
        }

        public List<Schedule> GetAll() => Data.Schedules.AsNoTracking().ToList();

        public void Update(Schedule item)
        {
            Data.Schedules.Update(item);
            Data.SaveChanges();
        }
    }
}
