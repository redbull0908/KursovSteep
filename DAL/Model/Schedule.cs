using DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class Schedule : IModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DoctorId { get; set; }
    }
}
