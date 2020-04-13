using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentRental.DAL.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Loyalty { get; set; }


        public virtual IEnumerable<Rental> RentalHistory { get; set; }
    }
}