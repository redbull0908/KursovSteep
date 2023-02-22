using DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    public class Service : IModel
    {
        [Key]
        public int Id { get  ; set ; }
        public string? Name { get ; set ; }
        public double Price { get ; set ; }
        public int ServiceCategoryId { get; set ; }
        public ServiceCategory? ServiceCategory { get; set ; }
    }
}
