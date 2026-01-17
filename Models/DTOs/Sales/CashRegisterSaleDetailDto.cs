namespace FerreteríaWeb_Backend.Models.DTOs.Sales
{
    public class CashRegisterSaleDetailDto
    {
        public DateTime Date { get; set; }
        public string PaymentMethod { get; set; } = "";
        public decimal Total { get; set; }

        public List<CashRegisterProductDetailDto> Products { get; set; } = [];
    }
}