namespace FerreteríaWeb_Backend.Models.DTOs.CashRegister;

public class CashRegisterCutDto
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string GeneratedByEmployeeName { get; set; } = "";
    public int TotalSales { get; set; }
    public decimal CashTotal { get; set; }
    public decimal BankCardTotal { get; set; }
    public decimal TransferTotal { get; set; }
    public decimal GrandTotal { get; set; }
}