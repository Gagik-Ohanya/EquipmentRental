using System.ComponentModel.DataAnnotations;

namespace EquipmentRental.BLL.Models.Request
{
    public class CustomerRequestModel
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Loyalty { get; set; }
    }
}