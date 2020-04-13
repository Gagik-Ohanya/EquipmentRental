using System.ComponentModel.DataAnnotations;

namespace EquipmentRental.BLL.Models.Request
{
    public class EquipmentRequestModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
    }
}