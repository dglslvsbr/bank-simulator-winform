namespace BankSimulator.DTOs
{
    internal class InvoiceItemDTO
    {
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public int InvoiceId { get; set; }
    }
}