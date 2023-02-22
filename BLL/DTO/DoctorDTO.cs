using BLL.Interfaces;

namespace BLL.DTO
{
    public class DoctorDTO : IDTO
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int ServiceCategoryId { get; set; }
        public string? Description { get; set; }
        public string? Img { get; set; }
    }
}
