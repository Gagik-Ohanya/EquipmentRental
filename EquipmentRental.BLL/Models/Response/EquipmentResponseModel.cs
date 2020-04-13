namespace EquipmentRental.BLL.Models.Response
{
    public class EquipmentResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public bool IsAvailable { get; set; }
        public string TypeName { get; set; }
        public int LoyaltyPoints { get; set; }
    }
}