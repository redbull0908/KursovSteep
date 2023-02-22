using BLL.Interfaces;
using DAL;

namespace BLL.DTO
{
    public class ServiceDTO : IDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int ServiceCategoryId { get; set; }
    }
}
