using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentRental.DAL.Entities
{
    public class RentalType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Fee { get; set; }
    }
}