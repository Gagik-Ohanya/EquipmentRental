﻿namespace EquipmentRental.BLL.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Loyalty { get; set; }
    }
}