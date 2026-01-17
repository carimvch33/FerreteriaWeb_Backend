namespace FerreteríaWeb_Backend.Models.DTOs.Sales
{
    public class CashRegisterProductDetailDto
    {
        public string ProductName { get; set; } = "";
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}
