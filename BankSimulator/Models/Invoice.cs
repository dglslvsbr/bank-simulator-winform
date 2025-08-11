namespace BankSimulator.Models
{
    internal class Invoice
    {
        public int Id { get; set; }
        public double TotalValue { get; set; }
        public DateTime Maturity { get; set; }
        public int ClientId { get; set; }
        public ICollection<InvoiceItems>? InvoiceItems { get; set; }
    }
}