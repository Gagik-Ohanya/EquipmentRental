using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentRental.BLL.Models.Request
{
    public class RentingRequestModel
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int EquipmentId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}