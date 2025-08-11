namespace BankSimulator.Models
{
    internal class InvoiceItems
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public int InvoiceId { get; set; }
    }
}