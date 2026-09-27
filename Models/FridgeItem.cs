namespace MKitchen.Models
{
    public class FridgeItem
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public DateOnly ExpirationDate { get; set; }
    }
}
