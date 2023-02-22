using BLL.Interfaces;

namespace BLL.DTO
{
    public class SubscribeDTO:IDTO
    {
        public int Id { get; set; }
        public string? DoctorName { get; set; }
        public string? ServiceName { get; set; }
        public string? Time { get; set; }
        public DateTime Date { get; set; }
        public string? UserName { get; set; }
        public string? UserFullName { get; set; }
    }
}
