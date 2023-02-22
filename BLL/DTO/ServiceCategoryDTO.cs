using BLL.Interfaces;

namespace BLL.DTO
{
    public class ServiceCategoryDTO : IDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

    }
}
