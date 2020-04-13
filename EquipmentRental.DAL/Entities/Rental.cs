using System;

namespace EquipmentRental.DAL.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int EquipmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal Fee { get; set; }
        public int Status { get; set; }


        public Customer Customer { get; set; }
        public Equipment Equipment { get; set; }
    }
}