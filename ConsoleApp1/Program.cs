using BLL;
using BLL.DTO;
using BLL.Interfaces;
using DAL;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=MedicalCenter;Integrated Security=True";
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureBLL(connectionString);

            var serviceprovider = serviceCollection.BuildServiceProvider();

            var doctor = serviceprovider.GetService<IServ<DoctorDTO>>().GetAll();

            doctor.ForEach(el =>
            {
                Console.WriteLine(el.FullName+"\n"+el.Description);
            });
        }
    }
}