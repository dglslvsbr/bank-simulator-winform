namespace BankSimulator.Models
{
    internal class CreditCard
    {
        public int Id { get; set; }
        public double Limite { get; set; }
        public string Flag { get; set; } = null!;
        public string Number { get; set; } = null!;
        public DateTime Validate { get; set; }
        public int CVV { get; set; }
        public int ClientId { get; set; }
    }
}