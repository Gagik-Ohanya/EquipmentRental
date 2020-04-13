namespace EquipmentRental.BLL.Models
{
    public class EquipmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public bool IsAvailable { get; set; }
    }
}