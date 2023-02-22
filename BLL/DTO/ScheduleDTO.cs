using BLL.Interfaces;

namespace BLL.DTO
{
    public class ScheduleDTO:IDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DoctorId { get; set; }
    }
}
