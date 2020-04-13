using System;

namespace EquipmentRental.BLL.Models
{
    public class RentalModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int EquipmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal Fee { get; set; }
        public int Status { get; set; }
    }
}