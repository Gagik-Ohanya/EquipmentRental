using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentRental.DAL.Entities
{
    public class Equipment
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int TypeId { get; set; }
        public bool IsAvailable { get; set; }


        public EquipmentType EquipmentType { get; set; }

        public virtual IEnumerable<Rental> RentalHistory { get; set; }
    }
}