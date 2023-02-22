using BLL.DTO;
using BLL.Interfaces;
using BLL.MappingProfiles;
using BLL.Services;
using DAL;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class Configuration
    {
        public static void ConfigureBLL(this IServiceCollection services , string connection)
        {
            services.ConfigureDAL(connection);
            services.AddAutoMapper(
                typeof(DoctorProfile),
                typeof(ServiceProfile),
                typeof(ServiceCategoryProfile),
                typeof(ScheduleServ),
                typeof(SubscribeServ));

            services.AddTransient<IServ<DoctorDTO>, DoctorServ>();
            services.AddTransient<IServ<ServiceDTO>, ServiceServ>();
            services.AddTransient<IServ<ServiceCategoryDTO>,ServiceCategoryServ>();
            services.AddTransient<IServ<ScheduleDTO>,ScheduleServ>();
            services.AddTransient < IServ < SubscribeDTO >, SubscribeServ > ();
        }
    }
}
