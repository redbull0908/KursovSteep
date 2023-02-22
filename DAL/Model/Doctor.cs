using DAL.Interfaces;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    public class Doctor : IModel
    {
        [Key]
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set ; }
        public string? Img { get; set; }
        public int ServiceCategoryId { get; set; }
        public ServiceCategory? ServiceCategory { get; set; }
        public virtual List<Schedule>? Schedules { get; set; }
    }
}
