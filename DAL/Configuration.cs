using DAL.EF;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class Configuration
    {
        public static void ConfigureDAL(this IServiceCollection services, string connection)
        {
            services.AddDbContext<AppContext>(opt => opt.UseSqlServer(connection));

            services.AddScoped<IRepository<Service>, ServiceRepository>();
            services.AddScoped<IRepository<ServiceCategory>, ServiceCategoryRepository>();
            services.AddScoped<IRepository<Doctor>, DoctorRepository>();
            services.AddScoped<IRepository<Schedule>, ScheduleRepository>();
            services.AddScoped<IRepository<Subscribe>,SubscribeRepository>();

           //new DbInitializer(new AppContext()).Init();
        }
    }
}
