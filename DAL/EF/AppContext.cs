using Microsoft.EntityFrameworkCore;

namespace DAL
{
    internal class AppContext : DbContext
    {
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public AppContext() { }
        public AppContext(DbContextOptions<AppContext> options) : base(options){ }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=MedicalCenter;Integrated Security=True");
        }
    }
}
