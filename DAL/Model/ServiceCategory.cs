using DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class ServiceCategory : IModel
    {
        [Key]
        public int Id { get ; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public virtual List<Service>? Services { get; set; }
        public virtual List<Doctor>? Doctors { get; set; }

    }
}
