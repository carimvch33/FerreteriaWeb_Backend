namespace FerreteríaWeb_Backend.Models.DTOs.Sales
{
    public class SaleTicketDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Employee { get; set; } = "";
        public string PaymentMethod { get; set; } = "";
        public decimal Total { get; set; }

        public object PaymentDetails { get; set; } = null!;

        public List<SaleProductDto> Products { get; set; } = [];
    }

}
