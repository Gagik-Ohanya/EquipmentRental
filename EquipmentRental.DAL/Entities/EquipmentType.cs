using System.Collections.Generic;

namespace EquipmentRental.DAL.Entities
{
    public class EquipmentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LoyaltyPoints { get; set; }


        public virtual IEnumerable<Equipment> Equipments { get; set; }
    }
}